using System.Linq.Expressions;
using System.Net.Sockets;

namespace TicimaxTurnike.Data.Abstract;

public interface IRepository<T> where T:class,new()
{
    public List<T> GetAll();
    
    public List<T> GetAllByFilter(Expression<Func<T,bool>> filter);
    
    public T Get(Expression<Func<T,bool>> filter = null);
    public void Add(T entity);
    public void Update(T entity);
    public void Remove(T entity);
    
}