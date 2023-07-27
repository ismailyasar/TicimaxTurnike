using TicimaxTurnike.Entity;
using TicimaxTurnike.Entity.Dtos;

namespace TicimaxTurnike.Data.Abstract;

public interface ILastEntryDetailRepository:IRepository<LastEntryDetail>
{

    public void AddOrUpdateLastEntry(LastEntryDetailDto lastEntryDetailDto);
    
    public List<LastEntryDetail> GetLastEntryListByFilter(int? personId,DateTime? startDate,DateTime? endDate);


}