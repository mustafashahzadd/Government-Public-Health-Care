using Governement_Public_Health_Care.DB_Context;
using Governement_Public_Health_Care.LogErrors;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace Governement_Public_Health_Care
{
    public class GenericRepository<TEntity,TID> : IGenericInterface<TEntity,TID> where TEntity : class
    {
        public HealthCareContext healthCare;
        public ErrorsFile errorsFile;
        private readonly DbSet<TEntity> _DBSet;

        public GenericRepository(HealthCareContext healthCare, ErrorsFile errorsFile) 
        {
            this.healthCare = healthCare;
           this.errorsFile = errorsFile;    
            this._DBSet=healthCare.Set<TEntity>();
        }


        public async Task<bool> Create(TEntity entity)
        {
            try
            {
                
                healthCare.Add(entity);
                var save = await Save();
                return save;
            }
                
            catch(Exception ex)
            {
                errorsFile.ErrorsDetail(ex.Message);
                return false;

            }
        }
           
        public async Task<bool> Delete(TEntity entity)
        {
            try
            {
                healthCare.Remove(entity);
                var Saved = await Save();
                return Saved;
            }
            catch (Exception ex)
            {
                errorsFile.ErrorsDetail(ex.Message);
                return false;

            }

        }

            




      

        public bool Exist(TID id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> GetAll()
        {
            throw new NotImplementedException();
        }

        //public IEnumerable<TEntity> GetAll()
        //{

        //}

        public async Task<TEntity> GetByID(TID id)
        {
            try
            {
                // Assuming _DBSet is of type DbSet<TEntity>
                var entity = await _DBSet.FindAsync(id);
                if (entity == null)
                {
                    // Log the error or handle the case when the entity is not found
                    // Depending on your logging mechanism or error handling strategy
                    errorsFile.ErrorsDetail($"Entity with ID {id} not found.");
                }
                return entity;
            }
            catch (Exception ex)
            {
                // Log the detailed error message
                // Depending on your logging mechanism or error handling strategy
                errorsFile.ErrorsDetail("Error in Retrieving Entity by ID", ex);

                // Rethrow the exception if you want the calling method to handle it
                // Or handle it here and decide what to do (e.g., return null or a default entity)
                throw;
            }
        }


        public async Task< bool> Save()
        {
            try
            {
               var Saved = await healthCare.SaveChangesAsync();
                return Saved > 0;
            }
           catch(Exception ex)
            {
                errorsFile.ErrorsDetail(ex.Message); return false;
            }
        }

        public async Task<bool> Update(TEntity entity)
        {
            try
            {
                healthCare.Update(entity);
                var saved = await Save();
                return saved;
            }
            catch (Exception ex)
            {
                errorsFile.ErrorsDetail(ex.Message);
                return false;
            }
        }

    }
}