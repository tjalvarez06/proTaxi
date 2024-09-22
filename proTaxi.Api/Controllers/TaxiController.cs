using Microsoft.AspNetCore.Mvc;
using proTaxi.Api.Models.Taxi;
using proTaxi.Api.Models.Viaje;
using proTaxi.Domain.Models;
using proTaxi.Persistence.Interfaces;
using proTaxi.Persistence.Models.Taxi;
using proTaxi.Persistence.Models.Viaje;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace proTaxi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaxiController : ControllerBase
    {
        private readonly ITaxiRepository taxiRepository;
        public TaxiController(ITaxiRepository taxiRepository) 
        {
            this.taxiRepository = taxiRepository;
        }
        // GET: api/<TaxiController>
        [HttpGet("GetTaxis")]
        public async Task<IActionResult> Get()
        {
            DataResult<List<TaxiModel>> result = new DataResult<List<TaxiModel>>();
            result = await this.taxiRepository.GetTaxis();

            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        // GET api/<TaxiController>/5
        [HttpGet("GetTaxisByPlaca")]
        public async Task<IActionResult> Get(string placa)
        {
            DataResult<TaxiModel> result = new DataResult<TaxiModel>();
            result = await this.taxiRepository.GetTaxiByPlaca(placa);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        // POST api/<TaxiController>
        [HttpPost("CreateTaxi")]
        public async Task<IActionResult> Post([FromBody] TaxiSaveDto taxiSave)
        {
            bool result = false;

            result = await this.taxiRepository.Save(new Domain.Entities.Taxi()
            {
                Placa = taxiSave.Placa,
                CreationDate = taxiSave.CreationDate,
                CreationUser = taxiSave.CreationUser
            });

            if (!result)
            {
                return BadRequest();
            }
            return Ok();
        }

        // PUT api/<TaxiController>/5
        [HttpPost("UpdateTaxi")]
        public async Task<IActionResult> Put([FromBody] TaxiUpdateDto taxiUpdate)
        {
            bool result = false;

            result = await this.taxiRepository.Update(new Domain.Entities.Taxi()
            {
                Id = taxiUpdate.Id,
                Placa = taxiUpdate.Placa,
                ModifyDate = taxiUpdate.ModifyDate,
                ModifyUser = taxiUpdate.ModifyUser
            });

            if (!result)
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}
