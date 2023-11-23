using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FlightPlanner.Data;

namespace FlightPlanner.Services;

public class EntityService<T> : DbService, IEntityService<T> where T : Entity
{
    public EntityService(IFlightPlannerDbContext context) : base(context)
    {
    }

    public void Create(T entity)
    {
        Create<T>(entity);
    }

    public void Delete(T entity)
    {
        Delete<T>(entity);
    }

    public void Update(T entity)
    {
        Update<T>(entity);
    }

    public IEnumerable<T> Get()
    {
        return Get<T>();
    }

    public IQueryable<T> Query()
    {
        return Query<T>();
    }

    public IQueryable<T> QueryById(int id)
    {
        return QueryById<T>(id);
    }

    public T? GetById(int id)
    {
        return GetByID<T>(id);
    }
}

