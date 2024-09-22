

using proTaxi.Domain.Entities;
using proTaxi.Domain.Interfaces;
using proTaxi.Domain.Models;
using proTaxi.Persistence.Models.Taxi;

namespace proTaxi.Persistence.Interfaces
{
    public interface ITaxiRepository : IRepository<Taxi, int>
    {
        /// <summary>
        /// Get all Taxis.
        /// </summary>
        /// <returns></returns>
        Task<DataResult<List<TaxiModel>>> GetTaxis();
        /// <summary>
        /// Get viajes by placa
        /// </summary>
        /// <param placa="placa">Placa of taxi</param>
        /// <returns></returns>
        Task<DataResult<TaxiModel>> GetTaxiByPlaca(string placa);

        /// <summary>
        /// Get all taxis by viaje ID
        /// </summary>
        /// <param hasta="viajeId">viaje of taxi</param>
        /// <returns></returns>
        Task<DataResult<List<TaxiModel>>> GetTaxiByViaje(int viajeId);
    }
}
