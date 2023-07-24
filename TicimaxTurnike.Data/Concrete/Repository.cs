using TicimaxTurnike.Data.Abstract;

namespace TicimaxTurnike.Data.Concrete;

public class Repository<T>:IRepository<T> where T:class,new()
{
    public List<T> GetAll()
    {
        throw new NotImplementedException();
    }

    public T Get(int Id)
    {
        throw new NotImplementedException();
    }

    public void Add(T entity)
    {
        throw new NotImplementedException();
    }

    public void Update(T entity)
    {
        throw new NotImplementedException();
    }

    public void Remove(int Id)
    {
        throw new NotImplementedException();
    }
}