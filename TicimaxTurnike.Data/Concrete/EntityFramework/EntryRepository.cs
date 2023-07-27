using System.Linq.Expressions;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using TicimaxTurnike.Data.Abstract;
using TicimaxTurnike.Entity;

namespace TicimaxTurnike.Data.Concrete.EntityFramework;

public class EntryRepository:Repository<Entry>,IEntryRepository
{
    public AppDbContext AppDbContext
    {
        get
        {
            return new AppDbContext();
        }
    }
    
    public EntryRepository(AppDbContext context) : base(context)
    {
    }

    public List<Entry> GetEntryListByFilter(int? personId,DateTime? startDate,DateTime? endDate)
    {

        var predicate = PredicateBuilder.True<Entry>();

        if (personId != null)
        {
             predicate = predicate.And(x=>personId==null || x.PersonId == personId);
        }

        if (startDate != null)
        {
            predicate = predicate.And(x=>startDate==null || x.Date >= startDate);
        }

        if (endDate != null)
        {
            predicate = predicate.And(x=>endDate==null || x.Date <= endDate);
        }

        var result =  GetAllByFilter(predicate);
        return result;
    }
}