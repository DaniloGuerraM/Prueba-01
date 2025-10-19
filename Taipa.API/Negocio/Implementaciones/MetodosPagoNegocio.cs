using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Taipa.API.Entidades;
using Taipa.API.Modelos.DTO;
using Taipa.API.Negocio.Interface;
using Taipa.API.Repositorio.Interface;

namespace Taipa.API.Negocio.Implementaciones
{
    public class MetodosPagoNegocio : IMetodosPagoNegocio
    {
        private readonly IMetodosPagoRepositorio _metodosPagoRepositorio;
        private readonly IMapper _mapper;
        public MetodosPagoNegocio(IMetodosPagoRepositorio metodosPagoRepositorio , IMapper mapper)
        {
            _mapper = mapper;
            _metodosPagoRepositorio = metodosPagoRepositorio;
        }
        public Task<bool> CrearMetodoPago(MetodosPagoDTO metodoPago)
        {
            var nuevoMetodoPago = _mapper.Map<MetodosPago>(metodoPago);
            return _metodosPagoRepositorio.CrearMetodoPago(nuevoMetodoPago);
        }

        public Task<bool> EliminarMetodoPago(Guid id)
        {
            return _metodosPagoRepositorio.EliminarMetodoPago(id);
        }

        public Task<IEnumerable<MetodosPago>> ObtenerMetodosPago()
        {
            return _metodosPagoRepositorio.ObtenerMetodosPago();
        }
    }
}