using System.Linq.Expressions;
using LinqKit.Core;
using Microsoft.EntityFrameworkCore;
using TicimaxTurnike.Data.Abstract;
using TicimaxTurnike.Entity;

namespace TicimaxTurnike.Data.Concrete;

public class Repository<T>:IRepository<T> where T:class,new()
{
    private readonly DbContext _context;
    public Repository(DbContext context)
    {
        _context = context;
    }
    
    public List<T> GetAll()
    {
        return _context.Set<T>().AsNoTracking().ToList();
    }

    public List<T> GetAllByFilter(Expression<Func<T, bool>> filter)
    {
        return _context.Set<T>().AsExpandable().Where(filter).ToList();
    }

    public T Get(Expression<Func<T,bool>> filter)
    {
        return _context.Set<T>().AsNoTracking().Where(filter).FirstOrDefault();
    }

    public void Add(T entity)
    {
        _context.Set<T>().Add(entity);
        _context.SaveChanges();
    }

    public void Update(T entity)
    {
        _context.Set<T>().Update(entity);
        _context.SaveChanges();
    }

    public void Remove(T entity)
    {
        _context.Set<T>().Remove(entity);
        _context.SaveChanges();
    }
}