using System;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Taipa.API.Entidades;

namespace Taipa.API.Utilidades.PDF
{
    public static class OrdenPdfHelper
    {
        public static byte[] GenerarOrdenPdf(Orden orden)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                Document doc = new Document(PageSize.A4, 40, 40, 40, 40);
                PdfWriter writer = PdfWriter.GetInstance(doc, ms);
                doc.Open();

                // -----  ENCABEZADO -----
                PdfPTable tablaEncabezado = new PdfPTable(3);
                tablaEncabezado.WidthPercentage = 100;
                tablaEncabezado.SetWidths(new float[] { 1.5f, 1f, 1.2f });

                // LOGO (opcional)
                var logoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "logo.png");
                if (File.Exists(logoPath))
                {
                    Image logo = Image.GetInstance(logoPath);
                    logo.ScaleToFit(120f, 60f);
                    PdfPCell celdaLogo = new PdfPCell(logo);
                    celdaLogo.Border = Rectangle.NO_BORDER;
                    celdaLogo.HorizontalAlignment = Element.ALIGN_LEFT;
                    tablaEncabezado.AddCell(celdaLogo);
                }
                else
                {
                    PdfPCell celdaTexto = new PdfPCell(new Phrase("TAIPA EXPRESS", 
                        FontFactory.GetFont("Helvetica", 20, Font.BOLD)));
                    celdaTexto.Border = Rectangle.NO_BORDER;
                    celdaTexto.VerticalAlignment = Element.ALIGN_MIDDLE;
                    tablaEncabezado.AddCell(celdaTexto);
                }

                // Centro: Datos empresa
                PdfPCell celdaCentro = new PdfPCell(new Phrase(
                    "Av. Tecnológica 1234\nCórdoba, Argentina\nTel: (351) 555-5555",
                    FontFactory.GetFont("Helvetica", 10, Font.NORMAL)));
                celdaCentro.Border = Rectangle.NO_BORDER;
                celdaCentro.HorizontalAlignment = Element.ALIGN_CENTER;
                tablaEncabezado.AddCell(celdaCentro);

                // Derecha: Tipo y número
                PdfPCell celdaDerecha = new PdfPCell();
                celdaDerecha.Border = Rectangle.NO_BORDER;
                celdaDerecha.AddElement(new Paragraph("orden", FontFactory.GetFont("Helvetica", 14, Font.BOLD)));
                celdaDerecha.AddElement(new Paragraph($"N°: {orden.NumeroOrden}", FontFactory.GetFont("Helvetica", 12, Font.NORMAL)));
                celdaDerecha.AddElement(new Paragraph($"Fecha: {orden.Fecha:dd/MM/yyyy}", FontFactory.GetFont("Helvetica", 10)));
                tablaEncabezado.AddCell(celdaDerecha);

                doc.Add(tablaEncabezado);
                doc.Add(new Paragraph("\n"));

                // ----- 2️ DATOS REMITENTE Y DESTINATARIO -----
                PdfPTable tablaDatos = new PdfPTable(2);
                tablaDatos.WidthPercentage = 100;
                tablaDatos.SetWidths(new float[] { 1, 1 });

                tablaDatos.AddCell(CeldaTitulo("Remitente"));
                tablaDatos.AddCell(CeldaTitulo("Destinatario"));

                tablaDatos.AddCell(CeldaNormal($"Nombre: {orden.Cliente?.NombreApellido ?? "----"}\nDomicilio: {orden.Cliente?.Direccion ?? "----"}\nTel: {orden.Cliente?.Telefono ?? "-"}\nForma de pago: CONTADO"));
                tablaDatos.AddCell(CeldaNormal($"Nombre: {orden.DestinatarioNombre ?? "----"}\nLocalidad: {orden.DestinatarioLocalidad ?? "----"}\nTel: {orden.DestinatarioTelefono ?? "-"}\nAgencia: {orden.DestinatarioAgencia ?? "-"}"));

                doc.Add(tablaDatos);
                doc.Add(new Paragraph("\n"));

                // ----- 3️ DETALLE -----
                PdfPTable tablaDetalles = new PdfPTable(4);
                tablaDetalles.WidthPercentage = 100;
                tablaDetalles.SetWidths(new float[] { 1f, 3f, 1f, 1f });

                tablaDetalles.AddCell(CeldaCabecera("N° Pieza"));
                tablaDetalles.AddCell(CeldaCabecera("Descripción"));
                tablaDetalles.AddCell(CeldaCabecera("Peso"));
                tablaDetalles.AddCell(CeldaCabecera("Importe"));

                foreach (var item in orden.Detalles)
                {
                    //tablaDetalles.AddCell(CeldaNormal(item.Productos?.Codigo ?? "-"));
                    tablaDetalles.AddCell(CeldaNormal($"{item.Productos?.Marca} - {item.Productos?.Descripcion}"));
                    //tablaDetalles.AddCell(CeldaNormal($"{item.Peso} Kg"));
                    //tablaDetalles.AddCell(CeldaNormal($"${item.Importe:N2}"));
                }

                doc.Add(tablaDetalles);
                doc.Add(new Paragraph("\n"));

                // ----- 4️ TOTALES -----
                PdfPTable tablaTotales = new PdfPTable(2);
                tablaTotales.WidthPercentage = 40;
                tablaTotales.HorizontalAlignment = Element.ALIGN_RIGHT;

                tablaTotales.AddCell(CeldaCabecera("Subtotal"));
                tablaTotales.AddCell(CeldaNormal($"${orden.Subtotal:N2}"));

                tablaTotales.AddCell(CeldaCabecera("Total"));
                tablaTotales.AddCell(CeldaNormal($"${orden.Total:N2}"));

                tablaTotales.AddCell(CeldaCabecera("Valor asegurado"));
                tablaTotales.AddCell(CeldaNormal($"${orden.ValorAsegurado:N2}"));

                doc.Add(tablaTotales);
                doc.Add(new Paragraph("\n"));

                // ----- 5️ OBSERVACIONES -----
                doc.Add(new Paragraph($"Observaciones: {orden.Observaciones ?? "Ninguna"}",
                    FontFactory.GetFont("Helvetica", 11, Font.NORMAL)));

                doc.Add(new Paragraph("\n"));
                doc.Add(new Paragraph("*** DOCUMENTO NO VÁLIDO COMO FACTURA ***",
                    FontFactory.GetFont("Helvetica", 10, Font.ITALIC)));

                doc.Add(new Paragraph("\n"));
                doc.Add(new Paragraph("Firma: ___________________________",
                    FontFactory.GetFont("Helvetica", 12, Font.NORMAL)));

                doc.Close();
                return ms.ToArray();
            }
        }

        // ----- Helpers -----
        private static PdfPCell CeldaTitulo(string texto)
        {
            var celda = new PdfPCell(new Phrase(texto, FontFactory.GetFont("Helvetica", 13, Font.BOLD)));
            celda.HorizontalAlignment = Element.ALIGN_CENTER;
            celda.BackgroundColor = new BaseColor(200, 200, 200);
            return celda;
        }

        private static PdfPCell CeldaCabecera(string texto)
        {
            var celda = new PdfPCell(new Phrase(texto, FontFactory.GetFont("Helvetica", 12, Font.BOLD)));
            celda.HorizontalAlignment = Element.ALIGN_CENTER;
            celda.BackgroundColor = new BaseColor(220, 220, 220);
            return celda;
        }

        private static PdfPCell CeldaNormal(string texto)
        {
            var celda = new PdfPCell(new Phrase(texto, FontFactory.GetFont("Helvetica", 10, Font.NORMAL)));
            celda.HorizontalAlignment = Element.ALIGN_LEFT;
            return celda;
        }
    }
}
