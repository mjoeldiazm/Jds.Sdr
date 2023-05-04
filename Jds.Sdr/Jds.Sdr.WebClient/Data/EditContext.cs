using Jds.Sdr.Shared.Interfaces;

namespace Jds.Sdr.WebClient.Data
{
	public class EditContext<T> where T : IDataEntity<int>, IClonable<T>, new()
	{
		public T DataItem { get; set; }

		public T OriginalDataItem { get; set; }

		public bool IsNewRow { get; set; }

		public Action StateHasChanged { get; set; }

		public EditContext(T dataItem)
		{
			DataItem = dataItem.Clone();
			OriginalDataItem = dataItem;
			if (DataItem == null)
			{
				DataItem = new T();
				IsNewRow = true;
			}
		}
	}
}
