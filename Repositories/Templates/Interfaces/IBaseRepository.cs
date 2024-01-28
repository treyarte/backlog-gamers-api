namespace backlog_gamers_api.Repositories.Templates.Interfaces;

public interface IBaseRepository<TEntity> where TEntity : class
{
    public Task<TEntity> Get(string id);
    public Task<IEnumerable<TEntity>> GetAll();
    public Task<TEntity> GetByProperty(Type type, string propName, object val);
    public Task<TEntity> Post(TEntity doc);
    public Task<int> PostMultiple(List<TEntity> docs);
    
}