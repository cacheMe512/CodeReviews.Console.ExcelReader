using OfficeOpenXml;

namespace ExcelReader.cacheMe512
{
    public class SalesOrderUploader
    {
        public static void UploadExcel(string filePath)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            if (!File.Exists(filePath))
            {
                Console.WriteLine("Error: File not found.");
                return;
            }

            List<SalesOrder> salesOrders = new List<SalesOrder>();

            try
            {
                using (var package = new ExcelPackage(new FileInfo(filePath)))
                {
                    var worksheet = package.Workbook.Worksheets[0];
                    int rowCount = worksheet.Dimension.Rows;

                    using (var context = new SalesOrderContext())
                    {

                        for (int row = 2; row <= rowCount; row++)
                        {
                            SalesOrder order = new SalesOrder
                            {
                                RevisionNumber = Convert.ToByte(worksheet.Cells[row, 1].Value),
                                OrderDate = Convert.ToDateTime(worksheet.Cells[row, 2].Value),
                                DueDate = Convert.ToDateTime(worksheet.Cells[row, 3].Value),
                                ShipDate = worksheet.Cells[row, 4].Value == null ? null : (DateTime?)Convert.ToDateTime(worksheet.Cells[row, 4].Value),
                                Status = Convert.ToByte(worksheet.Cells[row, 5].Value),
                                OnlineOrderFlag = worksheet.Cells[row, 6].Value?.ToString(),
                                SalesOrderNumber = worksheet.Cells[row, 7].Value?.ToString(),
                                PurchaseOrderNumber = worksheet.Cells[row, 8].Value?.ToString(),
                                AccountNumber = worksheet.Cells[row, 9].Value?.ToString(),
                                CustomerID = Convert.ToInt32(worksheet.Cells[row, 10].Value),
                                SalesPersonID = worksheet.Cells[row, 11].Value == null ? null : (int?)Convert.ToInt32(worksheet.Cells[row, 11].Value),
                                TerritoryID = worksheet.Cells[row, 12].Value == null ? null : (int?)Convert.ToInt32(worksheet.Cells[row, 12].Value),
                                BillToAddressID = Convert.ToInt32(worksheet.Cells[row, 13].Value),
                                ShipToAddressID = Convert.ToInt32(worksheet.Cells[row, 14].Value),
                                ShipMethodID = Convert.ToInt32(worksheet.Cells[row, 15].Value),
                                CreditCardID = worksheet.Cells[row, 16].Value == null ? null : (int?)Convert.ToInt32(worksheet.Cells[row, 16].Value),
                                CreditCardApprovalCode = worksheet.Cells[row, 17].Value?.ToString(),
                                CurrencyRateID = worksheet.Cells[row, 18].Value == null ? null : (int?)Convert.ToInt32(worksheet.Cells[row, 18].Value),
                                SubTotal = Convert.ToDecimal(worksheet.Cells[row, 19].Value),
                                TaxAmt = Convert.ToDecimal(worksheet.Cells[row, 20].Value),
                                Freight = Convert.ToDecimal(worksheet.Cells[row, 21].Value),
                                TotalDue = Convert.ToDecimal(worksheet.Cells[row, 22].Value),
                                Comment = worksheet.Cells[row, 23].Value?.ToString(),
                                Rowguid = Guid.Parse(worksheet.Cells[row, 24].Value?.ToString() ?? Guid.NewGuid().ToString()),
                                ModifiedDate = Convert.ToDateTime(worksheet.Cells[row, 25].Value)
                            };

                            salesOrders.Add(order);
                        }

                        context.SalesOrders.AddRange(salesOrders);
                        context.SaveChanges();
                    }
                }

                Console.WriteLine("Upload successful. Records inserted: " + salesOrders.Count);

                var tableData = salesOrders.Take(10)
                                           .Select(order => new
                                           {
                                               order.SalesOrderNumber,
                                               order.OrderDate,
                                               TotalDue = order.TotalDue.HasValue ? order.TotalDue.Value.ToString("F2") : "N/A",
                                               order.CustomerID,
                                               order.Status
                                           })
                                           .Cast<object>()
                                           .ToList();

                TableVisualizationEngine.ShowTable(tableData, "Recent Sales Orders");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error during upload: " + ex.Message);
            }
        }
    }
}
