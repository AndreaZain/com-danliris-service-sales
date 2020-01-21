﻿using Com.Danliris.Service.Sales.Lib.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Com.Danliris.Service.Sales.Lib.ViewModels.SalesReceipt
{
    public class SalesReceiptViewModel : BaseViewModel, IValidatableObject
    {
        [MaxLength(255)]
        public string Code { get; set; }
        public long? AutoIncreament { get; set; }
        [MaxLength(255)]
        public string SalesReceiptNo { get; set; }
        [MaxLength(255)]
        public string SalesReceiptType { get; set; }
        public DateTimeOffset? SalesReceiptDate { get; set; }

        /*Bank*/
        public int? BankId { get; set; }
        [MaxLength(255)]
        public string AccountCOA { get; set; }
        [MaxLength(255)]
        public string AccountName { get; set; }
        [MaxLength(255)]
        public string AccountNumber { get; set; }
        [MaxLength(255)]
        public string BankName { get; set; }
        [MaxLength(255)]
        public string BankCode { get; set; }

        /*Buyer*/
        public int? BuyerId { get; set; }
        [MaxLength(255)]
        public string BuyerName { get; set; }
        [MaxLength(1000)]
        public string BuyerAddress { get; set; }

        public double TotalPaid { get; set; }

        public ICollection<SalesReceiptDetailViewModel> SalesReceiptDetails { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(SalesReceiptType))
                yield return new ValidationResult("Tipe Kwitansi harus diisi", new List<string> { "SalesReceiptType" });

            if (!SalesReceiptDate.HasValue || SalesReceiptDate.Value < DateTimeOffset.Now.AddDays(-1))
                yield return new ValidationResult("Tgl kwitansi harus diisi dan lebih besar atau sama dengan hari ini", new List<string> { "SalesReceiptDate" });

            if (string.IsNullOrWhiteSpace(AccountName))
            {
                yield return new ValidationResult("Nama bank harus diisi", new List<string> { "Bank" });
            }
            if (string.IsNullOrWhiteSpace(BuyerName))
            {
                yield return new ValidationResult("Nama buyer harus di isi", new List<string> { "Buyer" });
            }

            if (TotalPaid <= 0)
                yield return new ValidationResult("Total Paid kosong", new List<string> { "TotalPaid" });

            int Count = 0;
            string DetailErrors = "[";

            if (SalesReceiptDetails != null && SalesReceiptDetails.Count > 0)
            {
                foreach (SalesReceiptDetailViewModel detail in SalesReceiptDetails)
                {
                    DetailErrors += "{";

                    var rowErrorCount = 0;

                    if (string.IsNullOrWhiteSpace(detail.SalesInvoiceNo))
                    {
                        Count++;
                        rowErrorCount++;
                        DetailErrors += "SalesInvoice : 'Kode Faktur harus diisi',";
                    }
                    if (detail.TotalPayment <= 0)
                    {
                        Count++;
                        rowErrorCount++;
                        DetailErrors += "TotalPayment : 'Kode Faktur harus diisi untuk memperoleh jumlah pembayaran',";
                    }
                    if (detail.Paid < 0)
                    {
                        Count++;
                        rowErrorCount++;
                        DetailErrors += "Paid : 'Kode Faktur harus diisi untuk memperoleh jumlah yang sudah dibayar',";
                    }
                    if (detail.Nominal <= 0)
                    {
                        Count++;
                        rowErrorCount++;
                        DetailErrors += "Nominal : 'Nominal tidak boleh kosong & harus lebih besar dari 0',";
                    }
                    if (detail.Unpaid < 0)
                    {
                        Count++;
                        rowErrorCount++;
                        DetailErrors += "Unpaid : 'Kode Faktur & Nominal harus diisi untuk memperoleh sisa pembayaran',";
                    }

                    if (rowErrorCount == 0)
                    {
                        var duplicateDetails = SalesReceiptDetails.Where(f =>
                                f.SalesInvoiceNo.Equals(detail.SalesInvoiceNo) &&
                                f.SalesInvoiceId.GetValueOrDefault().Equals(detail.SalesInvoiceId.GetValueOrDefault())
                            ).ToList();

                        if (duplicateDetails.Count > 1)
                        {
                            Count++;
                            DetailErrors += "SalesInvoiceNo : 'Nomor Faktur penjualan tidak boleh duplikat',";
                        }
                    }
                    DetailErrors += "}, ";
                }
            }
            else
            {
                yield return new ValidationResult("Detail harus diisi", new List<string> { "SalesReceiptDetail" });
            }

            DetailErrors += "]";

            if (Count > 0)
                yield return new ValidationResult(DetailErrors, new List<string> { "SalesReceiptDetails" });

        }


    }
}
