using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using GTE_BL;
using GTE_BL.Entities;
using GTE_BL.QPBEntities;
using RestSharp;
using Newtonsoft.Json;
using com.Xgensoftware.Core;

namespace BikePartScraper
{
    class Program
    {
        static ConcurrentBag<QBPOutputFile> _outputFile = null;

        static List<string> GetWarehouseCodes()
        {
            List<string> codes = new List<string>();
            codes.Add("1000");
            codes.Add("1100");
            codes.Add("1200");
            codes.Add("1300");
            return codes;
        }

        static RestRequest FormRequest(Method method, string url)
        {
            RestRequest request = new RestRequest(url, method);
            request.AddHeader("X-QBPAPI-KEY", ApplicationConfiguration.QBPAPIKey);
            request.AddHeader("Accept", "application/json");
            return request;
        }

        static T GetAPIData<T>(Method method, string url, QBPAPIRequest body = null)
        {
            var restClient = new RestClient(ApplicationConfiguration.QBPAPIUrl);
            var request = FormRequest(method, url);

            if(body != null)
            {
                request.AddJsonBody(body);
            }

            var response = restClient.Execute(request);
            if(response.StatusCode == HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<T>(response.Content);
            }

            throw new Exception(string.Format("Failed to get data from API. RESPONSE: {0}", response.Content));
                
        }
        
        static int GetProductInventoryCount()
        {
            int inventoryCount = 0;

            return inventoryCount;
        }

        static QBPOutputFile CreateProductOutputRecord(Product p)
        {
            QBPOutputFile output = new QBPOutputFile();

            try
            {
                output.ProdID = p.ProdID;

                if (p.model != null)
                {
                    output.Model = p.model.name;
                    output.ModelDescription = p.model.description;
                }

                if (p.brand != null)
                {
                    output.Brand = p.brand.name;
                }

                if (p.barcodes.Count > 0)
                {
                    var sUPC = p.barcodes[0];
                    var isVald = long.TryParse(sUPC, out long upc);
                    output.UPC = upc;
                }

                output.MSRP = p.msrp;
                output.MAP = p.mapPrice;
                output.EachCost = p.basePrice;
                output.ManufacturerProd = p.manufacturerPartNumber;
                output.Discontinued = p.discontinued ? "Yes" : "No";
                output.ThirdPartyAllowed = p.thirdPartyAllowed ? "Yes" : "No";
                output.UOM = p.unit;
                output.Weight = p.weightsAndMeasures.weight.value;
                output.Length = p.weightsAndMeasures.length.value;
                output.Width = p.weightsAndMeasures.width.value;
                output.Height = p.weightsAndMeasures.height.value;
                output.ProductDescription = p.name;
                output.ORMD = p.ormd ? "Yes" : "No";

                var size = p.productAttributes.FirstOrDefault(s => s.name.ToLower() == "size");
                if (size != null)
                    output.Size = size.value;

                var color = p.productAttributes.FirstOrDefault(s => s.name.ToLower() == "color");
                if (color != null)
                    output.Color = color.value;

                if (p.substitutes.Count > 0)
                {
                    output.Substitute = p.substitutes[0];
                }
            }
            catch { }
            return output;
        }

        static void ProcessProductDetails(QBPAPIRequest productDetailRequest, List<string> selectedProductCode)
        {
            var inventoryRequest = new QBPAPIRequest();
            inventoryRequest.productCodes = selectedProductCode;
            inventoryRequest.warehouseCodes = GetWarehouseCodes();

            try
            {
                var productDetails = GetAPIData<QBPProductDetailResponse>(Method.POST, string.Format("{0}/1/product", ApplicationConfiguration.QBPAPIUrl), productDetailRequest);
                var productinventories = GetAPIData<QBPInventoryResponse>(Method.POST, string.Format("{0}/1/inventory", ApplicationConfiguration.QBPAPIUrl), inventoryRequest);
                Parallel.ForEach(productDetails.products, product =>
                {
                    var productOutput = CreateProductOutputRecord(product);

                    var image = product.images.FirstOrDefault();
                    if (image != null)
                        productOutput.ImageUrl = string.Format("{0}{1}", ApplicationConfiguration.ProductImageURL, image);

                    var inventoryList = productinventories.inventories.Where(i => i.product == product.ProdID).ToList();
                    productOutput.Inventory = inventoryList.Sum(i => i.quantity);

                    WriteToConsole(string.Format("Adding {0} to file.", productOutput.ToString()));
                    _outputFile.Add(productOutput);
                });


            }
            catch (Exception ex)
            {
                WriteToConsole(string.Format("Error processing product details. ERROR: {0}", ex.Message));
            }
        }

        static void WriteToConsole(string msg)
        {
            Logging log = new Logging(ApplicationConfiguration.LogFile);
            string message = string.Format("{0}     {1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),msg);
            Console.WriteLine(message);
            log.LogMessage(Logging.LogMessageType.INFO, message);
        }

        static void Main(string[] args)
        {
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            WriteToConsole(string.Format("******** Starting export process {0}  ********", DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss")));
            List<Task> taskList = new List<Task>();

            _outputFile = new ConcurrentBag<QBPOutputFile>();
            string url = string.Empty;

            var productCodes = GetAPIData<QBPProductResponse>(Method.GET, string.Format("{0}/1/productcode/list", ApplicationConfiguration.QBPAPIUrl));
            
            while (productCodes.codes.Count > 0)
            {
                var selectedProductCode = productCodes.codes.Take(100).ToList();
                var productDetailRequest = new QBPAPIRequest();
                productDetailRequest.codes = selectedProductCode;

                var newTask = Task.Factory.StartNew(() => {
                    ProcessProductDetails(productDetailRequest, selectedProductCode);
                });
                taskList.Add(newTask);
                
                #region Old
                //var inventoryRequest = new QBPAPIRequest();
                //var productDetailRequest = new QBPAPIRequest();

                //productDetailRequest.codes = selectedProductCode;
                //inventoryRequest.productCodes = selectedProductCode;
                //inventoryRequest.warehouseCodes = GetWarehouseCodes();
                //try
                //{
                //    var productDetails = GetAPIData<QBPProductDetailResponse>(Method.POST, string.Format("{0}/1/product", ApplicationConfiguration.QBPAPIUrl), productDetailRequest);
                //    var productinventories = GetAPIData<QBPInventoryResponse>(Method.POST, string.Format("{0}/1/inventory", ApplicationConfiguration.QBPAPIUrl), inventoryRequest);
                //    Parallel.ForEach(productDetails.products, product =>
                //    {
                //        var productOutput = CreateProductOutputRecord(product);

                //        var image = product.images.FirstOrDefault();
                //        if (image != null)
                //            productOutput.ImageUrl = string.Format("{0}{1}", ApplicationConfiguration.ProductImageURL, image);

                //        var inventoryList = productinventories.inventories.Where(i => i.product == product.ProdID).ToList();
                //        productOutput.Inventory = inventoryList.Sum(i => i.quantity);

                //        outputFile.Add(productOutput);
                //    });


                //}
                //catch (Exception ex)
                //{
                //    WriteToConsole(string.Format("Error processing product details. ERROR: {0}", ex.Message));
                //}
                #endregion

                productCodes.codes.RemoveAll(p => productDetailRequest.codes.Contains(p));
            }

            Task.WaitAll(taskList.ToArray());
            if (_outputFile.Any())
            {
                _outputFile.ToCSV(",", ApplicationConfiguration.QBPOutputFile);
                WriteToConsole(string.Format("Writing output file {0}", ApplicationConfiguration.QBPOutputFile));
            }

            WriteToConsole(string.Format("******** Export process complete {0}  ********", DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss")));
        }
    }
}
