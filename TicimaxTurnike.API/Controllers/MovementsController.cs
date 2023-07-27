using System;

using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Microsoft.EntityFrameworkCore;

using TicimaxTurnike.API.Services;
using TicimaxTurnike.Data.Abstract;
using TicimaxTurnike.Entity;
using TicimaxTurnike.Entity.Dtos;

namespace TicimaxTurnike.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovementsController : ControllerBase
    {
        private readonly IEntryRepository _repository;
        private readonly ILastEntryDetailRepository _lastEntryDetailRepository;
        private readonly IMapper _mapper;
        private readonly RabbitMQPublisher _rabbitMqPublisher;
        private DateTime? startDateTime;
        private DateTime? endDateTime;

        public MovementsController(IEntryRepository repository, IMapper mapper, ILastEntryDetailRepository lastEntryDetailRepository, RabbitMQPublisher rabbitMqPublisher)
        {
            _repository = repository;
            _mapper = mapper;
            _lastEntryDetailRepository = lastEntryDetailRepository;
            _rabbitMqPublisher = rabbitMqPublisher;
        }

        [HttpGet("[action]")]
        public IActionResult GetAllEntries()
        {
            return Ok(_repository.GetAll());
        }

        
        [HttpGet("{personId:int?}/{startDate:datetime?}/{endDate:datetime?}")]
        public IActionResult GetEntriesByFilter([FromQuery]int? personId=null,[FromQuery] DateTime? startDate=null, [FromQuery]DateTime? endDate=null)
        {

            return Ok(_repository.GetEntryListByFilter(personId,startDate, endDate));
        }
        
        //Rapor
        [HttpGet("reports/{personId:int?}/{startDate:dateTime?}/{endDate:datetime?}")]
        public IActionResult GetLastEntryDetailsByFilter([FromQuery]int? personId=null,[FromQuery] DateTime? startDate=null, [FromQuery]DateTime? endDate=null)
        {
            
            return Ok(_lastEntryDetailRepository.GetLastEntryListByFilter(personId, startDate, endDate));
        }


        [HttpGet("{Id}")]
        public IActionResult GetEntry(int Id)
        {
            var entry = _repository.Get(x => x.Id == Id);
            if (entry == null)
            {
                return BadRequest($"{Id} id bilgisi ile bir hareket bulunamadı");
            }
            else
            {
                return Ok(entry);
            }
        }

        [HttpPost("[action]")]
        public IActionResult Enter(EntryDto entryDto)
        {
            entryDto.Type = "Enter";

            if (ModelState.IsValid)
            {
                var date = DateTime.Parse(entryDto.Date.ToString());
                var formatDateByDay = date.ToString("yyyy-MM-dd");

                var entry = _mapper.Map<Entry>(entryDto);
                _repository.Add(entry);

                LastEntryDetailDto lastEntryDetailDto = new()
                {
                    PersonId = entryDto.PersonId,
                    Type = entryDto.Type,
                    Date = entryDto.Date,
                    Day = formatDateByDay
                };
                //_lastEntryDetailRepository.AddOrUpdateLastEntry(lastEntryDetailDto);
                
                //RabbitMQ ya gönder
                var serializedObject = JsonSerializer.Serialize(lastEntryDetailDto);
                _rabbitMqPublisher.Publish(serializedObject);
                
                return Ok();
            }
            else
            {
                return BadRequest("Geçersiz veri girişi");
            }
        }
        
        [HttpPost("[action]")]
        public IActionResult Exit(EntryDto entryDto)
        {
            entryDto.Type = "Exit";
            if (ModelState.IsValid)
            {
                var date = DateTime.Parse(entryDto.Date.ToString());
                var formatDateByDay = date.ToString("yyyy-MM-dd");
                
                var entry = _mapper.Map<Entry>(entryDto);
                _repository.Add(entry);
                
                //İlk girişi ve en son çıkışı tut
                LastEntryDetailDto lastEntryDetailDto = new()
                {
                    PersonId = entryDto.PersonId,
                    Type = entryDto.Type,
                    Date = entryDto.Date,
                    Day = formatDateByDay
                };
                //_lastEntryDetailRepository.AddOrUpdateLastEntry(lastEntryDetailDto);
                
                //Rabbit MQ gönderim
                var serializedObject = JsonSerializer.Serialize(lastEntryDetailDto);
                _rabbitMqPublisher.Publish(serializedObject);
                return Ok();
            }
            else
            {
                return BadRequest("Geçersiz veri girişi");
            }
        }
        
    }
}
