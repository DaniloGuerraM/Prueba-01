using AutoMapper;
using Taipa.API.Entidades;
using Taipa.API.Modelos.DTO;
using Taipa.API.Modelos.Response;

using Taipa.API.Entidades;
using Taipa.API.Modelos.DTO;
using Taipa.API.Modelos.Response;
using Taipa.App.Entidades;
using Taipa.App.Modelos.DTO;
using Taipa.App.Modelos.Response;


namespace Taipa.App.Perfiles;

public class MapperPerfil : Profile
{

    public MapperPerfil()
    {
        // ------------------------------
        // De DTO a Entidad
        // ------------------------------
        CreateMap<VentasDTO, Venta>()
            .ForMember(dest => dest.MontoTotal, opt => opt.Ignore()) // Lo calculás en backend
            .ForMember(dest => dest.FechaVenta, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.Pago, opt => opt.MapFrom(src => src.Pago)) // <-- Nuevo
            .ForMember(dest => dest.Detalles, opt => opt.MapFrom(src => src.Detalles));

        CreateMap<DetalleVentaDTO, DetalleVenta>();

        CreateMap<PagoCrearDTO, Pago>()
            .ForMember(dest => dest.IdPago, opt => opt.Ignore()); // Lo genera la DB

        CreateMap<MetodosPagoDTO, MetodosPago>()
            .ForMember(dest => dest.IdMetodo, opt => opt.Ignore()); // Lo genera la DB

        // ------------------------------
        // De Entidad a Response
        // ------------------------------
        CreateMap<Venta, VentasResponse>()
            .ForMember(dest => dest.ClienteNombre, opt => opt.MapFrom(src => src.Cliente.NombreApellido))
            .ForMember(dest => dest.Pago, opt => opt.MapFrom(src => src.Pago))
            .ForMember(dest => dest.Detalles, opt => opt.MapFrom(src => src.Detalles));

        CreateMap<DetalleVenta, DetalleVentaResponse>()
            .ForMember(dest => dest.Subtotal, opt => opt.MapFrom(src => src.Subtotal))
            .ForMember(dest => dest.PrecioUnitario, opt => opt.MapFrom(src => src.PrecioUnitario))
            .ForMember(dest => dest.Productos, opt => opt.MapFrom(src => src.Productos.Marca + " " + src.Productos.Descripcion));

        CreateMap<Pago, PagoResponse>()
            .ForMember(dest => dest.NombreMetodo, opt => opt.MapFrom(src => src.MetodoPago.NombreMetodo));

        CreateMap<MetodosPago, MetodosPagoResponse>();


        //Mapeo Productos => ProductoDTO
        CreateMap<Productos, ProductoDTO>()
        .ForMember(dest => dest.Marca, opt => opt.MapFrom(src => src.Marca != null ? src.Marca.Trim() : null))
        .ForMember(dest => dest.Precio, opt => opt.MapFrom(src => Math.Round(src.Precio, 2)));
        //Mapeo ProductoDTO => Productos
        CreateMap<ProductoDTO, Productos>()
        .ForMember(dest => dest.Id, opt => opt.Ignore());
        //Mapeo Productos => ProductoResponse
        CreateMap<Productos, ProductoResponse>();

        // Mapeo Cliente -> ClienteDTO
        CreateMap<Cliente, ClienteDTO>()
            .ForMember(dest => dest.NombreApellido, opt => opt.MapFrom(src => src.NombreApellido.Trim()))
            .ForMember(dest => dest.Saldo, opt => opt.MapFrom(src => Math.Round(src.Saldo, 2)));

        // Mapeo ClienteDTO -> Cliente
        CreateMap<ClienteDTO, Cliente>()
            .ForMember(dest => dest.IdCliente, opt => opt.Ignore())
            .ForMember(dest => dest.FechaUltimaOperacion, opt => opt.Ignore());


        // Mapeo Cliente -> ClienteResponse
        CreateMap<Cliente, ClienteResponse>();

        // ------------------------------
        // Mapeos para Inventario
        // ------------------------------
        CreateMap<InventarioDTO, Inventario>()
            .ForMember(dest => dest.Id_Inventario, opt => opt.Ignore());

        CreateMap<Inventario, InventarioResponse>()
            .ForMember(dest => dest.Producto_Id, opt => opt.MapFrom(src => src.Proveedor_Id));

        // ------------------------------
        // Mapeos para Orden
        // ------------------------------
        CreateMap<Orden, OrdenResponce>()
            .ForMember(dest => dest.NumeroOrden,
    opt => opt.MapFrom(src => $"R-{src.NumeroOrden.ToString().PadLeft(Math.Max(6, src.NumeroOrden.ToString().Length), '0')}"))//.ForMember(dest => dest.NumeroOrden, opt => opt.MapFrom(src => string.Format("R-{0}", src.NumeroOrden.ToString().PadLeft(Math.Max(6, src.NumeroOrden.ToString().Length), '0'))))
            .ForMember(dest => dest.NombreApellido, opt => opt.MapFrom(src => src.Cliente.NombreApellido))
            .ForMember(dest => dest.Domicilio, opt => opt.MapFrom(src => src.Cliente.Direccion))
            .ForMember(dest => dest.Cuit, opt => opt.MapFrom(src => src.Cliente.Cuit));
        /*
        .ForMember(dest => dest.ClienteNombre, opt => opt.MapFrom(src => src.Cliente.NombreApellido))
        .ForMember(dest => dest.ClienteDireccion, opt => opt.MapFrom(src => src.Cliente.Direccion))
        .ForMember(dest => dest.Detalles, opt => opt.MapFrom(src => src.Detalles));*/

        CreateMap<DetalleOrden, DetalleOrdenResponse>()
            .ForMember(dest => dest.Marca, opt => opt.MapFrom(src => src.Productos != null ? src.Productos.Marca : string.Empty))
            .ForMember(dest => dest.Descripcion, opt => opt.MapFrom(src => src.Productos != null ? src.Productos.Descripcion : string.Empty));
        
        /*
            .ForMember(dest => dest.ProductoNombre, opt => opt.MapFrom(src => src.Productos.Marca + " " + src.Productos.Descripcion));
            //.ForMember(dest => dest.PrecioUnitario, opt => opt.MapFrom(src => src.PrecioUnitario));
//            .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.Total));*/

    }
}

