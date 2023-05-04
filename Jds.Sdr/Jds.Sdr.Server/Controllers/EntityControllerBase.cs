using AutoMapper;
using Jds.Sdr.Server.Data;
using Jds.Sdr.Server.Interfaces;
using Jds.Sdr.Shared.Interfaces;
using Jds.Sdr.Shared.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Jds.Sdr.Server.Controllers
{
	[ApiController]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	public abstract class EntityControllerBase<TEntity, TView> : ControllerBase, IEntityController<TEntity, TView>
		where TEntity : class, IDataEntity<int>
		where TView : class, IDataEntity<int>
	{
		private readonly ApplicationDbContext dbContext;
		private readonly IMapper mapper;

		protected ApplicationDbContext DbContext
		{
			get => dbContext;
		}

		public EntityControllerBase(ApplicationDbContext dbContext, IMapper mapper)
		{
			this.dbContext = dbContext;
			this.mapper = mapper;
		}

		[HttpGet]
		public virtual async Task<DataServiceResponse<IEnumerable<TView>>> GetAllAsync()
		{
			IEnumerable<TEntity> entities = await GetAllAsQueryable()
				.ToListAsync();
			IEnumerable<TView> views = mapper.Map<IEnumerable<TView>>(entities);
			DataServiceResponse<IEnumerable<TView>> serviceResponse = new(views);
			return serviceResponse;
		}

		[HttpPost]
		public virtual async Task<DataServiceResponse<int>> PostAsync(TView objectToPost)
		{
			TEntity entity = mapper.Map<TEntity>(objectToPost);
			EntityOperationResult<TEntity> operationResult = await BeforePost(entity);
			if (!operationResult.IsSuccessful)
				return new DataServiceResponse<int>(true, operationResult.ResultMessage);
			entity = operationResult.Entity;
			try
			{
				entity = ClearRelatedEntities(entity);
				await dbContext.AddAsync(entity);
				int affectedRegisters = await dbContext.SaveChangesAsync();
				if (affectedRegisters > 0)
				{
					bool afterPostIsSuccessful = (await AfterPost(entity)).IsSuccessful;
					if (afterPostIsSuccessful)
						return new DataServiceResponse<int>(affectedRegisters, entity.Id,
							DataServiceResponseMessages.ItemAdded);
					else
						return new DataServiceResponse<int>(affectedRegisters, entity.Id,
							DataServiceResponseMessages.ItemAddedPartially);
				}
				else
					return new DataServiceResponse<int>(true, DataServiceResponseMessages.ItemNotAdded);
			}
			catch (Exception)
			{
				return new DataServiceResponse<int>(true, ControllerMessages.ErrorOnPost);
			}
		}

		[HttpPut]
		public virtual async Task<DataServiceResponse<int>> PutAsync(TView objectToPut)
		{
			dbContext.ChangeTracker.Clear();
			try
			{
				TEntity entity = await dbContext
					.Set<TEntity>()
					.Where(x => x.Id.Equals(objectToPut.Id))
					.FirstOrDefaultAsync();
				if (entity is null)
					return new DataServiceResponse<int>(true, DataServiceResponseMessages.ItemNotFound);
				mapper.Map<TView, TEntity>(objectToPut, entity);
				dbContext.Update(entity);
				entity = ClearRelatedEntities(entity);
				int affectedRegisters = await dbContext.SaveChangesAsync();
				if (affectedRegisters > 0)
					return new DataServiceResponse<int>(false, DataServiceResponseMessages.ItemUpdated);
				else
					return new DataServiceResponse<int>(true, DataServiceResponseMessages.ItemNotUpdated);
			}
			catch (Exception)
			{
				return new DataServiceResponse<int>(true, ControllerMessages.ErrorOnPut);
			}
		}

		[HttpDelete("{id:int}")]
		public virtual async Task<DataServiceResponse<int>> DeleteAsync(int id)
		{
			try
			{
				TEntity entity = await dbContext
					.Set<TEntity>()
					.Where(x => x.Id.Equals(id))
					.FirstOrDefaultAsync();
				if (entity is null)
					return new DataServiceResponse<int>(true, DataServiceResponseMessages.ItemNotFound);
				dbContext.Remove(entity);
				int affectedRegisters = await dbContext.SaveChangesAsync();
				if (affectedRegisters > 0)
				{
					return new DataServiceResponse<int>(false, DataServiceResponseMessages.ItemDeleted);
				}
				else
					return new DataServiceResponse<int>(true, DataServiceResponseMessages.ItemNotDeleted);
			}
			catch (Exception)
			{
				return new DataServiceResponse<int>(true, ControllerMessages.ErrorOnDelete);
			}
		}

		protected virtual TEntity ClearRelatedEntities(TEntity entity)
		{
			return entity;
		}

		public virtual Task<EntityOperationResult<TEntity>> BeforePost(TEntity objectToPost)
		{
			return Task.FromResult(new EntityOperationResult<TEntity>(objectToPost, true));
		}

		public virtual Task<EntityOperationResult<TEntity>> AfterPost(TEntity objectToPost)
		{
			return Task.FromResult(new EntityOperationResult<TEntity>(objectToPost, true));
		}

		protected abstract IQueryable<TEntity> GetAllAsQueryable();
	}
}
