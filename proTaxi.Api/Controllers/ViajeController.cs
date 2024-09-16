using Microsoft.AspNetCore.Mvc;
using proTaxi.Api.Models.Viaje;
using proTaxi.Domain.Models;
using proTaxi.Persistence.Interfaces;
using proTaxi.Persistence.Models.Viaje;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace proTaxi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ViajeController : ControllerBase
    {
        private readonly IViajeRepository viajeRepository;

        public ViajeController(IViajeRepository viajeRepository)
        {
            this.viajeRepository = viajeRepository;
        }
        // GET: api/<ViajeController>
        [HttpGet("GetViajes")]
        public async Task<IActionResult> Get()
        {
            DataResult<List<ViajeModel>> result = new DataResult<List<ViajeModel>>();
            result = await this.viajeRepository.GetViajes();
            
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        // GET api/<ViajeController>/5
        [HttpGet("GetViajeByHasta")]
        public async Task<IActionResult> Get(string name)
        {
            DataResult<ViajeModel> result = new DataResult<ViajeModel>();
            result = await this.viajeRepository.GetViajes(name);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        // POST api/<ViajeController>
        [HttpPost("CreateViaje")]
        public async Task<IActionResult> Post([FromBody] ViajeSaveDto viajeSave)
        {
            bool result = false;

            result = await this.viajeRepository.Save(new Domain.Entities.Viaje()
            {
                FechaInicio = viajeSave.FechaInicio,
                FechaFin = viajeSave.FechaFin,
                Desde = viajeSave.Desde,
                Hasta = viajeSave.Hasta,
                Calificacion = viajeSave.Calificacion,
                TaxiId = viajeSave.TaxiId,
                UsuarioId = viajeSave.UsuarioId,
                CreationDate = viajeSave.CreationDate,
                CreationUser = viajeSave.CreationUser
            });

            if (!result)
            {
                return BadRequest();
            }
            return Ok();
        }

        // PUT api/<ViajeController>/5
        [HttpPost("UpdateViaje")]
        public async Task<IActionResult> Put([FromBody] ViajeUpdateDto viajeUpdate)
        {
            bool result = false;

            result = await this.viajeRepository.Update(new Domain.Entities.Viaje()
            {
                FechaInicio = viajeUpdate.FechaInicio,
                FechaFin = viajeUpdate.FechaFin,
                Desde = viajeUpdate.Desde,
                Hasta = viajeUpdate.Hasta,
                Calificacion = viajeUpdate.Calificacion,
                TaxiId = viajeUpdate.TaxiId,
                UsuarioId = viajeUpdate.UsuarioId,
                CreationDate = viajeUpdate.ModifyDate,
                CreationUser = viajeUpdate.ModifyUser,
                Id = viajeUpdate.Id
            });

            if (!result)
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}
