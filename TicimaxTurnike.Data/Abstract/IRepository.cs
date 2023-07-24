using System.Net.Sockets;

namespace TicimaxTurnike.Data.Abstract;

public interface IRepository<T> where T:class,new()
{
    public List<T> GetAll();
    public T Get(int Id);
    public void Add(T entity);
    public void Update(T entity);
    public void Remove(int Id);
    
}