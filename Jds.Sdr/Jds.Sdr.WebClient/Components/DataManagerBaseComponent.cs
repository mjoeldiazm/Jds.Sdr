using DevExpress.Blazor;
using Jds.Sdr.Shared.Interfaces;
using Jds.Sdr.Shared.Services;
using Jds.Sdr.WebClient.Components.Events;
using Jds.Sdr.WebClient.Data;
using Jds.Sdr.WebClient.Extensions;
using Jds.Sdr.WebClient.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;

namespace Jds.Sdr.WebClient.Components
{
	public abstract class DataManagerBaseComponent<TEntity> : ComponentBase
		where TEntity : class, IDataEntity<int>, IClonable<TEntity>, new()
	{
		[Inject]
		protected IDataRepository<TEntity, int> DataRepository { get; set; }

		[Inject]
		protected NavigationManager NavigationManager { get; set; }

		[Inject]
		protected IJSRuntime JSRuntime { get; set; }

		[CascadingParameter]
		private Task<AuthenticationState> AuthenticationStateTask { get; set; } = null;

		protected TEntity SelectedModel { get; set; }
		protected DxDataGrid<TEntity> DataGrid { get; set; }
		protected bool IsAnyRowSelected { get; set; }
		protected bool IsDataAvailable { get; set; }
		protected bool ShowFilterRow { get; set; } = true;
		protected bool ConfirmationDialogVisible { get; set; }
		protected DataOperation DataOperation { get; set; }
		protected EditContext<TEntity> EditContext { get; set; }
		protected bool IsSavingChanges { get; set; }
		protected IEnumerable<TEntity> ModelViews { get; set; }
		protected static PropertyAttributeReader<TEntity> AttributeReader { get; set; }
		protected IEnumerable<int> AllowedPageSizes { get; } = new int[] { 5, 10, 25, 50 };
		protected int DefaultPageSize { get; } = 25;
		protected ToastMessage ToastMessage { get; set; }
		protected AuthenticationState AuthenticationState { get; set; }
		protected EditContext EditFormContext { get; set; }
		protected bool CanEditSelectedModel { get; set; } = true;
		protected bool CanDeleteSelectedModel { get; set; } = true;

		protected override Task OnInitializedAsync()
		{
			AttributeReader = new PropertyAttributeReader<TEntity>();
			return base.OnInitializedAsync();
		}

		protected async override Task OnParametersSetAsync()
		{
			AuthenticationState = await AuthenticationStateTask;
		}

		protected async Task<IEnumerable<TEntity>> GetDataAsync(CancellationToken cancellationToken)
		{
			if (ModelViews is null)
			{
				DataServiceResponse<IEnumerable<TEntity>> serviceResponse = await DataRepository.GetAllAsync();
				ModelViews = await FilterDataResponse(serviceResponse.Response);
				IsDataAvailable = ModelViews.Any();
				if (IsDataAvailable)
					await InvokeAsync(StateHasChanged);
			}
			await LoadRelatedDataAsync();
			return ModelViews;
		}

		protected virtual Task<IEnumerable<TEntity>> FilterDataResponse(IEnumerable<TEntity> entities)
		{
			return Task.FromResult(entities);
		}

		protected virtual Task LoadRelatedDataAsync()
		{
			return Task.CompletedTask;
		}

		protected virtual Task<bool> IsValidModelToInsertAsync()
		{
			return Task.FromResult(true);
		}

		protected virtual Task<bool> IsValidModelToUpdateAsync()
		{
			return Task.FromResult(true);
		}

		protected virtual Task<bool> IsValidModelToDeleteAsync()
		{
			return Task.FromResult(true);
		}

		protected async Task HandleValidSubmitAsync()
		{
			switch (DataOperation)
			{
				case DataOperation.Insert:
					bool isValidModelToInsert = await IsValidModelToInsertAsync();
					if (!isValidModelToInsert) return;
					break;
				case DataOperation.Update:
					bool isValidModelToUpdate = await IsValidModelToUpdateAsync();
					if (!isValidModelToUpdate) return;
					break;
			}
			if (!EditContext.DataItem.Equals(EditContext.OriginalDataItem))
			{
				ConfirmationDialogVisible = true;
				await SaveChangesAsync();
			}
			else
			{
				await OnCancelButtonClick();
			}
		}

		protected Task HandleInvalidSubmitAsync()
		{
			return Task.CompletedTask;
		}

		protected async Task SaveChangesAsync()
		{
			IsSavingChanges = true;
			ConfirmationDialogVisible = false;
			DataServiceResponse<int> operationResult = null;
			switch (DataOperation)
			{
				case DataOperation.Insert:
					operationResult = await DataRepository.PostAsync(EditContext.DataItem);
					break;
				case DataOperation.Update:
					operationResult = await DataRepository.PutAsync(EditContext.DataItem);
					SelectedModel = null;
					break;
			}
			IsSavingChanges = false;
			if (operationResult.HasError)
			{
				await OnModelOperationErrorDetected(EditContext.DataItem, operationResult.Message);
				return;
			}
			else
			{
				if (DataOperation == DataOperation.Insert)
					await AfterSuccessfulInsert();
			}
			ModelViews = null;
			await DataGrid.CancelRowEdit();
			await DataGrid.Refresh();
			await LoadRelatedDataAsync();
			EditContext = null;
			IsDataAvailable = ModelViews.Any();
			IsAnyRowSelected = false;
		}

		protected virtual Task AfterSuccessfulInsert()
		{
			return Task.CompletedTask;
		}

		protected async Task DeleteItemsAsync()
		{
			bool isValidModelToDelete = await IsValidModelToDeleteAsync();
			if (!isValidModelToDelete) return;
			ConfirmationDialogVisible = false;
			ModelViews = null;
			await DataRepository.DeleteAsync(SelectedModel.Id);
			await DataGrid.Refresh();
			SelectedModel = null;
			IsDataAvailable = ModelViews.Any();
			IsAnyRowSelected = false;
			await LoadRelatedDataAsync();
			await InvokeAsync(StateHasChanged);
		}

		protected async Task RefreshGridAsync()
		{
			ModelViews = null;
			await DataGrid.Refresh();
		}

		protected void ChangeToolbarEnabled()
		{
			IsAnyRowSelected = (SelectedModel is not null);
			InvokeAsync(StateHasChanged);
		}

		protected async void OnRowClick(DataGridRowClickEventArgs<TEntity> args)
		{
			if (!CanEditSelectedModel)
				return;
			if (args.MouseEventArgs.Detail == MouseEventInformation.DoubleClick)
				await OnEditClick();
		}

		protected async Task OnAddNewClick()
		{
			await DataGrid.StartRowEdit(null);
			EditContext = new EditContext<TEntity>(new TEntity());
			DataOperation = DataOperation.Insert;
			await OnAddEntity(SelectedModel);
		}

		protected async Task OnEditClick()
		{
			await DataGrid.StartRowEdit(SelectedModel);
			EditContext = new EditContext<TEntity>(SelectedModel);
			DataOperation = DataOperation.Update;
			await OnEditEntity(SelectedModel);
		}

		protected void OnRowEditStarting(TEntity dataItem)
		{
			DataOperation = DataOperation.Update;
			EditContext = new EditContext<TEntity>(dataItem);
			EditContext.StateHasChanged += () => { InvokeAsync(StateHasChanged); };
		}

		protected void OnDeleteClick()
		{
			ConfirmationDialogVisible = true;
		}

		protected async Task OnCancelButtonClick()
		{
			ConfirmationDialogVisible = false;
			await DataGrid.CancelRowEdit();
			EditContext = null;
		}

		protected async void OnShowFilterRowClick(ToolbarItemClickEventArgs e)
		{
			ShowFilterRow = !ShowFilterRow;
			await InvokeAsync(StateHasChanged);
		}

		protected async Task OnUpdateClick()
		{
			await RefreshGridAsync();
		}

		protected virtual Task OnModelOperationErrorDetected(TEntity model, string message)
		{
			return Task.CompletedTask;
		}

		protected virtual Task OnAddEntity(TEntity model)
		{
			return Task.CompletedTask;
		}

		protected virtual Task OnEditEntity(TEntity model)
		{
			return Task.CompletedTask;
		}

		protected async void OnLayoutChanged(IDataGridLayout dataGridLayout)
		{
			object layoutObject = dataGridLayout.SaveLayout();
			if (layoutObject is not null)
				await JSRuntime.SaveInLocalStorageAsync($"{this.GetType().Name}GridLayout", layoutObject.ToString());
		}
		protected async void OnLayoutRestoring(IDataGridLayout dataGridLayout)
		{
			object layoutObject = await JSRuntime.GetFromLocalStorageAsync($"{this.GetType().Name}GridLayout");
			if (layoutObject is not null)
				dataGridLayout.LoadLayout(layoutObject.ToString());
		}

		protected virtual void OnSelectedModelChanged(TEntity model)
		{
			SelectedModel = model;
			ChangeToolbarEnabled();
		}
	}
}
