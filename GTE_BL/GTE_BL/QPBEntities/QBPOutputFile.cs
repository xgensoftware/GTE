using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTE_BL.QPBEntities
{
    public class QBPOutputFile
    {
        public string ProdID { get; set;}

        public long UPC { get; set; }

        public string Category { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }

        public string ModelDescription { get; set; }

        public int Inventory { get; set; }

        public string Size { get; set; }

        public string Color { get; set; }

        public double MSRP { get; set; }

        public double MAP { get; set; }

        public double EachCost { get; set; }

        public string ManufacturerProd { get; set; }

        public string COO { get; set; }

        public string Discontinued { get; set; }

        public string UOM { get; set; }

        public double Weight { get; set; }

        public double Length { get; set; }

        public double Width { get; set; }

        public double Height { get; set; }

        public string ORMD { get; set; }

        public string ProductDescription { get; set; }

        public string Replacement { get; set; }

        public string Substitute { get; set; }

        public string RecentlyUpdated { get; set; }

        public string ImageUrl { get; set; }

        public string ThirdPartyAllowed { get; set; }

        public override string ToString()
        {
            return string.Format("{0} {1}", ProdID, ProductDescription);
        }
    }
}
