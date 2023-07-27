using System.Linq.Expressions;
using TicimaxTurnike.Entity;

namespace TicimaxTurnike.Data.Abstract;

public interface IEntryRepository:IRepository<Entry>
{
    public List<Entry> GetEntryListByFilter(int? personId,DateTime? startDate,DateTime? endDate);
}