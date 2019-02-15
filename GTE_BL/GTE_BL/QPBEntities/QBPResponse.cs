using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GTE_BL.QPBEntities
{

    public class ResponseStatus
    {
        public string type { get; set; }
    }
    #region Inventory
    public class QBPInventory
    {
        public int quantity { get; set; }
        public string product { get; set; }
        public string warehouse { get; set; }
    }
    public class QBPAPIRequest
    {
        public List<string> warehouseCodes { get; set; }
        public List<string> productCodes { get; set; }

        public List<string> codes { get; set; }
    }

    public class QBPInventoryResponse
    {
        public ResponseStatus responseStatus { get; set; }
        public List<object> errors { get; set; }
        public List<QBPInventory> inventories { get; set; }
    }
    
    #endregion

    #region Product
    public class QBPProductDetailResponse
    {
        public List<Error> errors { get; set; }
        public List<Product> products { get; set; }
        public ResponseStatus responseStatus { get; set; }
    }
    public class QBPProductResponse
    {
        public ResponseStatus responseStatus { get; set; }
        public List<object> errors { get; set; }
        public List<string> codes { get; set; }
    }
    

    public class ProductAttribute
    {
        public string name { get; set; }
        public string unit { get; set; }
        public string value { get; set; }
    }
    #endregion



    public class Error
    {
        public string details { get; set; }
        public string errorCode { get; set; }
        public string errorMessage { get; set; }
        public string logKey { get; set; }
    }

    public class Brand
    {
        public string code { get; set; }
        public string name { get; set; }
    }

    public class Model
    {
        public List<string> bulletPoints { get; set; }
        public string code { get; set; }
        public string description { get; set; }
        public string name { get; set; }
    }



    public class Height
    {
        public string unit { get; set; }
        public double value { get; set; }
    }

    public class Length
    {
        public string unit { get; set; }
        public double value { get; set; }
    }

    public class Weight
    {
        public string unit { get; set; }
        public double value { get; set; }
    }

    public class Width
    {
        public string unit { get; set; }
        public double value { get; set; }
    }

    public class WeightsAndMeasures
    {
        public Height height { get; set; }
        public Length length { get; set; }
        public Weight weight { get; set; }
        public Width width { get; set; }
    }

    public class Product
    {
        public List<string> barcodes { get; set; }
        public double basePrice { get; set; }


        public Brand brand { get; set; }
        public List<string> bulletPoints { get; set; }
        public List<string> categoryCodes { get; set; }
        public string chokingHazardWarningText { get; set; }
        public string chokingHazardWarningType { get; set; }

        [JsonProperty("code")]
        public string ProdID { get; set; }

        public bool discontinued { get; set; }
        public bool hazmat { get; set; }
        public List<string> images { get; set; }
        public string intendedAgeWarningText { get; set; }
        public string intendedAgeWarningType { get; set; }
        public string manufacturerPartNumber { get; set; }
        public double mapPrice { get; set; }
        public Model model { get; set; }
        public double msrp { get; set; }
        public string name { get; set; }
        public string orderProcess { get; set; }
        public bool ormd { get; set; }
        public List<ProductAttribute> productAttributes { get; set; }
        public string prop65Text { get; set; }
        public List<string> recommendations { get; set; }
        public List<string> seeAlsos { get; set; }
        public List<string> smallParts { get; set; }
        public List<string> substitutes { get; set; }
        public List<string> supersedes { get; set; }
        public bool thirdPartyAllowed { get; set; }
        public string unit { get; set; }
        public WeightsAndMeasures weightsAndMeasures { get; set; }
    }

}
