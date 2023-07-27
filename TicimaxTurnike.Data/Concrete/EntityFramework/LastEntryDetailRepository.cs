using LinqKit;
using Microsoft.EntityFrameworkCore;
using TicimaxTurnike.Data.Abstract;
using TicimaxTurnike.Entity;
using TicimaxTurnike.Entity.Dtos;

namespace TicimaxTurnike.Data.Concrete.EntityFramework;

public class LastEntryDetailRepository:Repository<LastEntryDetail>,ILastEntryDetailRepository
{
    
    public AppDbContext AppDbContext
    {
        get
        {
            return new AppDbContext();
        }
    }
    
    public LastEntryDetailRepository(AppDbContext context) : base(context)
    {
    }

    public void AddOrUpdateLastEntry(LastEntryDetailDto lastEntryDetailDto)
    {
        var entryDetail = Get(x => x.PersonId == lastEntryDetailDto.PersonId && 
                                   x.Day == lastEntryDetailDto.Day &&
                                   x.Type == lastEntryDetailDto.Type);

        if (entryDetail != null)
        {
            if (entryDetail.Type == "Exit")
            {
                entryDetail.Date = lastEntryDetailDto.Date;
                Update(entryDetail);
            }
        }
        else
        {
            var lastEntryDetail = new LastEntryDetail()
            {
                Date = lastEntryDetailDto.Date,
                Day = lastEntryDetailDto.Day,
                PersonId = lastEntryDetailDto.PersonId,
                Type = lastEntryDetailDto.Type
            };
            Add(lastEntryDetail);
        }
    }

    public List<LastEntryDetail> GetLastEntryListByFilter(int? personId, DateTime? startDate, DateTime? endDate)
    {
        var predicate = PredicateBuilder.True<LastEntryDetail>();

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