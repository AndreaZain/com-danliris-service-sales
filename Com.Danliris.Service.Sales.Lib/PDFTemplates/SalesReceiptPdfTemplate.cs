﻿using Com.Danliris.Service.Sales.Lib.Utilities;
using Com.Danliris.Service.Sales.Lib.ViewModels.SalesReceipt;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace Com.Danliris.Service.Sales.Lib.PDFTemplates
{
    public class SalesReceiptPdfTemplate
    {
        public MemoryStream GeneratePdfTemplate(SalesReceiptViewModel viewModel, int clientTimeZoneOffset)
        {
            const int MARGIN = 30;

            Font header_font = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 18);
            Font normal_font = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 8);
            Font bold_font = FontFactory.GetFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 8);

            Document document = new Document(PageSize.A4, MARGIN, MARGIN, MARGIN, MARGIN);
            MemoryStream stream = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(document, stream);
            document.Open();

            #region Header

            PdfPTable headerTable = new PdfPTable(2);
            PdfPTable headerTable1 = new PdfPTable(1);
            PdfPTable headerTable2 = new PdfPTable(1);
            PdfPTable headerTable3 = new PdfPTable(3);
            PdfPTable headerTable4 = new PdfPTable(2);
            headerTable.SetWidths(new float[] { 10f, 10f });
            headerTable.WidthPercentage = 100;
            headerTable3.SetWidths(new float[] { 25f, 4f, 60f });
            headerTable3.WidthPercentage = 100;
            headerTable4.SetWidths(new float[] { 10f, 40f });
            headerTable4.WidthPercentage = 100;

            PdfPCell cellHeader1 = new PdfPCell() { Border = Rectangle.NO_BORDER };
            PdfPCell cellHeader2 = new PdfPCell() { Border = Rectangle.NO_BORDER };
            PdfPCell cellHeader3 = new PdfPCell() { Border = Rectangle.NO_BORDER };
            PdfPCell cellHeader4 = new PdfPCell() { Border = Rectangle.NO_BORDER };
            PdfPCell cellHeaderBody = new PdfPCell() { Border = Rectangle.NO_BORDER };

            cellHeaderBody.Phrase = new Phrase("PT. DANLIRIS", bold_font);
            headerTable1.AddCell(cellHeaderBody);
            cellHeaderBody.Phrase = new Phrase("INDUSTRIAL & TRADING CO.LTD.", bold_font);
            headerTable1.AddCell(cellHeaderBody);
            cellHeaderBody.Phrase = new Phrase("Kel. Banaran (Selatan Lawehan)", normal_font);
            headerTable1.AddCell(cellHeaderBody);
            cellHeaderBody.Phrase = new Phrase("Telp. 714400, 719113", normal_font);
            headerTable1.AddCell(cellHeaderBody);
            cellHeaderBody.Phrase = new Phrase("SOLO - INDONESIA 57100", normal_font);
            headerTable1.AddCell(cellHeaderBody);

            cellHeaderBody.Phrase = new Phrase("", normal_font);
            headerTable1.AddCell(cellHeaderBody);
            cellHeaderBody.Phrase = new Phrase("", normal_font);
            headerTable1.AddCell(cellHeaderBody);
            cellHeaderBody.Phrase = new Phrase("", normal_font);
            headerTable1.AddCell(cellHeaderBody);

            cellHeader1.AddElement(headerTable1);
            headerTable.AddCell(cellHeader1);

            cellHeaderBody.HorizontalAlignment = Element.ALIGN_CENTER;

            cellHeaderBody.Phrase = new Phrase("", header_font);
            headerTable2.AddCell(cellHeaderBody);
            cellHeaderBody.Phrase = new Phrase("", header_font);
            headerTable2.AddCell(cellHeaderBody);
            cellHeaderBody.Phrase = new Phrase("KUITANSI", header_font);
            headerTable2.AddCell(cellHeaderBody);
            cellHeaderBody.Phrase = new Phrase("", header_font);
            headerTable2.AddCell(cellHeaderBody);
            cellHeaderBody.Phrase = new Phrase("No. " + viewModel.SalesReceiptNo, bold_font);
            headerTable2.AddCell(cellHeaderBody);

            cellHeader2.AddElement(headerTable2);
            headerTable.AddCell(cellHeader2);

            cellHeaderBody.HorizontalAlignment = Element.ALIGN_LEFT;

            cellHeaderBody.Phrase = new Phrase("Bank ", normal_font);
            headerTable3.AddCell(cellHeaderBody);
            cellHeaderBody.Phrase = new Phrase(":", normal_font);
            headerTable3.AddCell(cellHeaderBody);
            cellHeaderBody.Phrase = new Phrase(viewModel.AccountName, normal_font);
            headerTable3.AddCell(cellHeaderBody);

            cellHeaderBody.Phrase = new Phrase("Telah terima dari ", normal_font);
            headerTable3.AddCell(cellHeaderBody);
            cellHeaderBody.Phrase = new Phrase(":", normal_font);
            headerTable3.AddCell(cellHeaderBody);
            cellHeaderBody.Phrase = new Phrase(viewModel.BuyerName, normal_font);
            headerTable3.AddCell(cellHeaderBody);

            cellHeaderBody.Phrase = new Phrase("Jumlah ", normal_font);
            headerTable3.AddCell(cellHeaderBody);
            cellHeaderBody.Phrase = new Phrase(":", normal_font);
            headerTable3.AddCell(cellHeaderBody);
            cellHeaderBody.Phrase = new Phrase("" + viewModel.TotalPaid, normal_font);
            headerTable3.AddCell(cellHeaderBody);

            cellHeaderBody.Phrase = new Phrase("Untuk pembayaran ", normal_font);
            headerTable3.AddCell(cellHeaderBody);
            cellHeaderBody.Phrase = new Phrase(":", normal_font);
            headerTable3.AddCell(cellHeaderBody);
            foreach (SalesReceiptDetailViewModel item in viewModel.SalesReceiptDetails)
            {
                cellHeaderBody.Phrase = new Phrase(item.SalesInvoiceNo + "  ", normal_font);
                headerTable3.AddCell(cellHeaderBody);
                cellHeaderBody.Phrase = new Phrase(" ", normal_font);
                headerTable3.AddCell(cellHeaderBody);
                cellHeaderBody.Phrase = new Phrase(" ", normal_font);
                headerTable3.AddCell(cellHeaderBody);
            }
            cellHeaderBody.Phrase = new Phrase(" ", normal_font);
            headerTable3.AddCell(cellHeaderBody);

            cellHeader3.AddElement(headerTable3);
            headerTable.AddCell(cellHeader3);

            cellHeader4.AddElement(headerTable4);
            headerTable.AddCell(cellHeader4);

            document.Add(headerTable);

            #endregion Header

            #region Footer
            PdfPTable footerTable = new PdfPTable(2);
            PdfPTable footerTable1 = new PdfPTable(1);
            PdfPTable footerTable2 = new PdfPTable(2);

            footerTable.SetWidths(new float[] { 10f, 10f });
            footerTable.WidthPercentage = 100;
            footerTable1.WidthPercentage = 80;
            footerTable2.SetWidths(new float[] { 30f, 50f });
            footerTable2.WidthPercentage = 100;

            PdfPCell cellFooterLeft1 = new PdfPCell() { Border = Rectangle.NO_BORDER };
            PdfPCell cellFooterLeft2 = new PdfPCell() { Border = Rectangle.NO_BORDER };
            PdfPCell cellHeaderFooter = new PdfPCell() { Border = Rectangle.NO_BORDER };


            cellHeaderFooter.HorizontalAlignment = Element.ALIGN_CENTER;

            cellHeaderFooter.Phrase = new Phrase("", normal_font);
            footerTable1.AddCell(cellHeaderFooter);
            cellHeaderFooter.Phrase = new Phrase("", normal_font);
            footerTable1.AddCell(cellHeaderFooter);

            cellHeaderFooter.Phrase = new Phrase("Solo, " + viewModel.SalesReceiptDate?.AddHours(clientTimeZoneOffset).ToString("dd MMMM yyyy", new CultureInfo("id-ID")), normal_font);
            footerTable1.AddCell(cellHeaderFooter);

            cellHeaderFooter.Phrase = new Phrase("", normal_font);
            footerTable1.AddCell(cellHeaderFooter);
            cellHeaderFooter.Phrase = new Phrase("", normal_font);
            footerTable1.AddCell(cellHeaderFooter);
            cellHeaderFooter.Phrase = new Phrase("", normal_font);
            footerTable1.AddCell(cellHeaderFooter);
            cellHeaderFooter.Phrase = new Phrase("", normal_font);
            footerTable1.AddCell(cellHeaderFooter);
            cellHeaderFooter.Phrase = new Phrase("", normal_font);
            footerTable1.AddCell(cellHeaderFooter);
            cellHeaderFooter.Phrase = new Phrase("", normal_font);
            footerTable1.AddCell(cellHeaderFooter);
            cellHeaderFooter.Phrase = new Phrase("", normal_font);
            footerTable1.AddCell(cellHeaderFooter);
            cellHeaderFooter.Phrase = new Phrase("", normal_font);
            footerTable1.AddCell(cellHeaderFooter);
            cellHeaderFooter.Phrase = new Phrase("", normal_font);
            footerTable1.AddCell(cellHeaderFooter);
            cellHeaderFooter.Phrase = new Phrase("", normal_font);
            footerTable1.AddCell(cellHeaderFooter);
            cellHeaderFooter.Phrase = new Phrase("", normal_font);
            footerTable1.AddCell(cellHeaderFooter);
            cellHeaderFooter.Phrase = new Phrase("", normal_font);
            footerTable1.AddCell(cellHeaderFooter);

            cellHeaderFooter.Phrase = new Phrase("(                                                                )", normal_font);
            footerTable1.AddCell(cellHeaderFooter);

            cellFooterLeft1.AddElement(footerTable1);
            footerTable.AddCell(cellFooterLeft1);

            cellHeaderFooter.Phrase = new Phrase("", normal_font);
            footerTable2.AddCell(cellHeaderFooter);
            cellHeaderFooter.Phrase = new Phrase("", normal_font);
            footerTable2.AddCell(cellHeaderFooter);
            cellHeaderFooter.Phrase = new Phrase("", normal_font);
            footerTable2.AddCell(cellHeaderFooter);
            cellHeaderFooter.Phrase = new Phrase("", normal_font);
            footerTable2.AddCell(cellHeaderFooter);

            cellFooterLeft2.AddElement(footerTable2);
            footerTable.AddCell(cellFooterLeft2);

            document.Add(footerTable);

            #endregion Footer

            document.Close();
            byte[] byteInfo = stream.ToArray();
            stream.Write(byteInfo, 0, byteInfo.Length);
            stream.Position = 0;

            return stream;
        }
    }
}
