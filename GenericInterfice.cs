using RestSharp;

namespace Governement_Public_Health_Care
{
    public interface IGenericInterface<TEntity,TID> where TEntity : class
    {
        //Get
        Task<TEntity> GetByID(TID id);
         IEnumerable<TEntity> GetAll();
        bool Exist(TID id);

        //Create
        Task<bool> Create(TEntity entity);

        //Delete
       Task<bool> Delete(TEntity entity);  

        //Update
        
        Task<bool> Update(TEntity entity);
        Task<bool> Save();
    }
}
 