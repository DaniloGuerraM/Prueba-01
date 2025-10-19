CREATE EXTENSION IF NOT EXISTS "pgcrypto";

-- ===========================
-- Clientes (SIN CAMBIOS)
-- ===========================
CREATE TABLE clientes (
  id_cliente UUID PRIMARY KEY DEFAULT gen_random_uuid() NOT NULL,
  nombre_apellido VARCHAR(100) NOT NULL,
  correo_electronico VARCHAR(100),
  telefono VARCHAR(100),
  direccion VARCHAR(100),
  fecha_nacimiento TIMESTAMP WITH TIME ZONE,
  dni INTEGER,
  nombre_comercial VARCHAR(100),
  codigo_postal INTEGER,
  codigo_provincia VARCHAR(100),
  categoria_arca VARCHAR(100),
  cuit BIGINT,
  saldo DOUBLE PRECISION DEFAULT 0.00,
  limite_compra DOUBLE PRECISION,
  observaciones VARCHAR(1000),
  zona VARCHAR(100),
  fecha_ultima_operacion TIMESTAMP WITH TIME ZONE
);

-- ===========================
-- Proveedores (SIN CAMBIOS)
-- ===========================
CREATE TABLE proveedores (
  id_proveedor UUID PRIMARY KEY DEFAULT gen_random_uuid() NOT NULL,
  nombre TEXT NOT NULL,
  nombre_contacto TEXT,
  correo_electronico TEXT UNIQUE,
  telefono TEXT,
  direccion TEXT
);

-- ===========================
-- Productos (SIN CAMBIOS)
-- ===========================
CREATE TABLE productos (

  id_producto UUID PRIMARY KEY DEFAULT gen_random_uuid() NOT NULL,
  marca TEXT NOT NULL,
  descripcion TEXT,
  precio NUMERIC(10, 2) NOT NULL,
  proveedor_id UUID REFERENCES proveedores (id_proveedor),

  tipo TEXT
);

-- ===========================
-- Inventario (SIN CAMBIOS)
-- ===========================
CREATE TABLE inventario (

  id_inventario UUID PRIMARY KEY DEFAULT gen_random_uuid() NOT NULL,
  producto_id UUID NOT NULL REFERENCES productos (id_producto),

  cantidad INT NOT NULL
);

-- ===========================
-- Ventas (MODIFICADO)
-- ===========================
CREATE TABLE ventas (

  id_venta UUID PRIMARY KEY DEFAULT gen_random_uuid() NOT NULL,
  id_cliente UUID NOT NULL REFERENCES clientes(id_cliente),
  fecha_venta TIMESTAMP WITH TIME ZONE DEFAULT now(),
  monto_total NUMERIC(10, 2) NOT NULL,
  estado BOOLEAN NOT NULL DEFAULT true,
  -- CAMBIO: Se agregó este campo para diferenciar forma de pago de condición de venta
  condicion_venta VARCHAR(50) NOT NULL DEFAULT 'Contado' 
  -- Valores posibles: 'Contado', 'Cuenta Corriente'
);

-- ===========================
-- Detalle de venta (SIN CAMBIOS)
-- ===========================
CREATE TABLE detalles_venta (
  id_detalle_venta UUID PRIMARY KEY DEFAULT gen_random_uuid() NOT NULL,
  venta_id UUID NOT NULL REFERENCES ventas (id_venta),
  producto_id UUID NOT NULL REFERENCES productos (id_producto),
  cantidad INT NOT NULL,

  precio_unitario NUMERIC(10, 2) NOT NULL,
  subtotal NUMERIC(10, 2) GENERATED ALWAYS AS (cantidad * precio_unitario) STORED,
  observaciones TEXT
  
);

-- ===========================
-- NUEVO: Métodos de pago
-- ===========================
CREATE TABLE metodos_pago (
  id_metodo UUID PRIMARY KEY DEFAULT gen_random_uuid() NOT NULL,
  nombre VARCHAR(50) NOT NULL -- Ej: Efectivo, Transferencia, Débito, Crédito, MP

  observaciones text

);

-- ===========================
-- NUEVO: Pagos (pueden ser múltiples por venta)
-- ===========================
CREATE TABLE pagos (
  id_pago UUID PRIMARY KEY DEFAULT gen_random_uuid() NOT NULL,
  id_venta UUID NOT NULL REFERENCES ventas(id_venta),
  id_metodo UUID NOT NULL REFERENCES metodos_pago(id_metodo),
  monto NUMERIC(10, 2) NOT NULL,
  detalle TEXT, -- Ej: Nro comprobante, alias transferencia, etc.
  fecha_pago TIMESTAMP DEFAULT now()
);

-- ===========================
-- Cuentas corrientes (SIN CAMBIOS ESTRUCTURALES)
-- Uso recomendado: historial de movimientos
-- ===========================
CREATE TABLE cuentas_corrientes (

  id UUID PRIMARY KEY DEFAULT gen_random_uuid() NOT NULL,
  id_cliente UUID NOT NULL REFERENCES clientes(id_cliente),
  saldo NUMERIC(10, 2) NOT NULL DEFAULT 0.00,
  descripcion TEXT,
  fecha_consulta DATE,
  fecha_operacion DATE,
  codigo_operacion TEXT,
  num_comprobante TEXT,
  importe NUMERIC(10, 2)
);

-- ===========================
-- Para las ordenes de entrega
-- ===========================
create table orden(
	id_orden UUID PRIMARY KEY DEFAULT gen_random_uuid() not null,
	numero_orden serial UNIQUE not null,
	fecha TIMESTAMP WITH TIME ZONE DEFAULT now(),
	id_cliente UUID not null,
	id_venta UUID not null,
	observaciones text,
	estado VARCHAR(20) not null DEFAULT 'Pendiente'
		CHECK (estado IN('Pendiente','Entregado','Anulado')),
	CONSTRAINT fk_remito_cliente FOREIGN KEY (id_cliente) REFERENCES clientes(id_cliente),
	CONSTRAINT fk_remito_venta FOREIGN KEY (id_venta) REFERENCES ventas(id_venta)
);

-- ===========================
-- para guardar los detalles de las ordenes
-- ===========================
create table detalle_orden(
	id_detalle_orden UUID PRIMARY KEY DEFAULT gen_random_uuid() not null,
	id_orden UUID not null,
	id_producto UUID not null,
	cantidad int not null,
	observaciones text,
	CONSTRAINT fk_detalle_orden FOREIGN KEY (id_orden) REFERENCES orden(id_orden) ON DELETE CASCADE,
	CONSTRAINT fk_detalle_producto FOREIGN KEY (id_producto) REFERENCES productos(id_producto)
);
-- para ventas sin clientes guardados
INSERT INTO clientes (
    id_cliente,
    nombre_apellido,
    nombre_comercial,
    saldo,
    fecha_ultima_operacion
) VALUES (
    '123e4567-e89b-12d3-a456-426614174000', -- UUID que vamos a utilizar
    'Cliente Genérico',
    'CF',
    0.00,
    now()
);
