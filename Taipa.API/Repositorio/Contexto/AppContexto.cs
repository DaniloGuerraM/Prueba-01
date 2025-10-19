using Microsoft.EntityFrameworkCore;
using Taipa.API.Entidades;

using Taipa.App.Entidades;  // Asegúrate de que este using apunte a tus entidades

 // Asegúrate de que este using apunte a tus entidades


namespace Taipa.App.Repositorio.Contexto

{
    public class AppContexto : DbContext
    {
        public AppContexto(DbContextOptions<AppContexto> options) : base(options) { }

        // DbSets para cada entidad
        public DbSet<Productos> Productos { get; set; }

        public DbSet<Cliente> Clientes { get; set; }
        // Agrega otros DbSets aquí...
      
        public DbSet<Venta> Ventas { get; set; }
        public DbSet<DetalleVenta> Detalles { get; set; }
        public DbSet<MetodosPago> MetodosPago { get; set; }
        public DbSet<Pago> Pagos { get; set; }
        public DbSet<Inventario> Inventario { get; set; }
        public DbSet<Orden> Ordens { get; set; }
        public DbSet<DetalleOrden> DetalleOrdens { get; set; }
        


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Productos>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Marca)
                    .IsRequired()
                    .HasMaxLength(100);
                entity.Property(p => p.Precio)
                    .HasColumnType("decimal(18,2)")
                    .HasDefaultValue(0.00m);
            });

            // Configuración para Cliente
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(c => c.IdCliente);
                entity.Property(c => c.Saldo).HasDefaultValue(0.00);
                entity.Property(c => c.FechaUltimaOperacion)
                      .HasColumnType("timestamp with time zone");

                // Otras configuraciones...
            });

            modelBuilder.Entity<Venta>(entity =>
          {
              entity.ToTable("ventas");
              entity.HasKey(e => e.IdVenta);

              entity.Property(e => e.MontoTotal)
                    .HasColumnType("numeric(10,2)");
              entity.Property(e => e.CondicionVenta)  // <- agrega esto
                  .HasColumnName("condicion_venta")
                  .IsRequired()
                  .HasMaxLength(50);

              entity.HasOne(e => e.Cliente)
                    .WithMany(c => c.Ventas)
                    .HasForeignKey(e => e.IdCliente)
                    .OnDelete(DeleteBehavior.Restrict);

              entity.HasMany(e => e.Detalles)
                    .WithOne(d => d.Venta)
                    .HasForeignKey(d => d.VentaId)
                    .OnDelete(DeleteBehavior.Cascade);

              entity.HasMany(e => e.Pago)
                    .WithOne(p => p.Venta)
                    .HasForeignKey(p => p.IdVenta)
                    .OnDelete(DeleteBehavior.Cascade); // Si borrás la venta, se borran los pagos
          });

            // DetalleVenta
            modelBuilder.Entity<DetalleVenta>(entity =>
            {
                entity.ToTable("detalles_venta");
                entity.HasKey(e => e.IdDetalleVenta);

                entity.Property(e => e.Cantidad)
                      .IsRequired();

                entity.HasOne(e => e.Productos)
                      .WithMany()
                      .HasForeignKey(e => e.Id)
                      .OnDelete(DeleteBehavior.Restrict);
            });


            // Métodos de Pago
            modelBuilder.Entity<MetodosPago>(entity =>
            {
                entity.ToTable("metodos_pago");
                entity.HasKey(e => e.IdMetodo);

                entity.Property(e => e.NombreMetodo)
                      .IsRequired()
                      .HasMaxLength(50);
            });

            // Pagos
            modelBuilder.Entity<Pago>(entity =>
            {
                entity.ToTable("pagos");
                entity.HasKey(e => e.IdPago);

                entity.Property(e => e.Monto)
                      .HasColumnType("numeric(10,2)")
                      .IsRequired();

                entity.HasOne(p => p.Venta)
                      .WithMany(v => v.Pago)
                      .HasForeignKey(p => p.IdVenta)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(p => p.MetodoPago)
                      .WithMany()
                      .HasForeignKey(p => p.IdMetodo)
                      .OnDelete(DeleteBehavior.Restrict);
            });
            // Configuración para Inventario
            modelBuilder.Entity<Inventario>(entity =>
            {
                entity.ToTable("inventario");
                entity.HasKey(i => i.Id_Inventario);

                entity.Property(i => i.Id_Inventario)
                    .HasColumnName("id_inventario")
                    .IsRequired();

                entity.Property(i => i.Proveedor_Id)
                    .HasColumnName("producto_id")
                    .IsRequired();

                entity.Property(i => i.Cantidad)
                    .HasColumnName("cantidad")
                    .IsRequired();

                // Relación con Productos - CORREGIDO
                entity.HasOne<Taipa.API.Entidades.Productos>()  // <-- Especifica el namespace completo
                    .WithMany()
                    .HasForeignKey(i => i.Proveedor_Id)
                    .OnDelete(DeleteBehavior.Restrict);
            });
            modelBuilder.Entity<Orden>(entity =>
            {
                entity.ToTable("orden");
                entity.HasKey(e => e.IdOrden);

                entity.Property(e => e.Fecha)
                      .HasColumnType("timestamp with time zone")
                      .IsRequired();

                entity.Property(e => e.Observaciones)
                      .HasMaxLength(500);

                entity.Property(e => e.Estado)
                    .HasConversion<string>() // enum como texto
                    .HasMaxLength(50);

                entity.HasOne(e => e.Cliente)
                      .WithMany(c => c.Ordens)
                      .HasForeignKey(e => e.IdCliente)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(e => e.Detalles)
                      .WithOne(d => d.Orden)
                      .HasForeignKey(d => d.IdOrden)
                      .OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<DetalleOrden>(entity =>
            {
                entity.ToTable("detalle_orden");
                entity.HasKey(e => e.IdDetalleOrden);

                entity.Property(e => e.Cantidad)
                      .IsRequired();

                entity.Property(e => e.Observaciones)
                      .HasMaxLength(500);

                entity.HasOne(e => e.Orden)
                      .WithMany(r => r.Detalles)
                      .HasForeignKey(e => e.IdOrden)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Productos)
                      .WithMany()
                      .HasForeignKey(e => e.IdProducto)
                      .OnDelete(DeleteBehavior.Restrict);
            });


            // Configuraciones adicionales para otras entidades...


            base.OnModelCreating(modelBuilder);
        }
    }
}
