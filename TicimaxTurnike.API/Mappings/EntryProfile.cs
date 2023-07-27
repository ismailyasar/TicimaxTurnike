using AutoMapper;
using TicimaxTurnike.Entity;
using TicimaxTurnike.Entity.Dtos;

namespace TicimaxTurnike.API.Mappings;

public class EntryProfile:Profile
{
    public EntryProfile()
    {
        CreateMap<Entry, EntryDto>().ReverseMap();
        CreateMap<LastEntryDetail, LastEntryDetailDto>().ReverseMap();
    }
}