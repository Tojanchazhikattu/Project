using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using htDAL;
using htBLL.Utility;

namespace htBLL
{
    public class ProductTypeServices : ServiceBase<ProductType>
    {

        public void create(string productTypeName)
        {
            try
            {
                ProductType productType = new ProductType();
                productType.Name = productTypeName;
                productType.Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IList<ProductType> GetAllProdutType()
        {
            IList<ProductType> lstProdutTypes = ProductType.FetchAll();
            return lstProdutTypes;
        }

        public ProductType GetProdutType(int productTypeId)
        {
            ProductType productType = ProductType.Fetch(productTypeId);
            return productType;
        }


        public ProductType AddProductType(ProductType newProductType)
        {
            newProductType.SetPropertySet();
            newProductType.ValidationRuleList.CheckAll();

            int id = newProductType.Save();
            ProductType productType = ProductType.Fetch(id);
            return productType;
        }

        public void UpdateProductType(ProductType newProductType)
        {

            newProductType.SetPropertySet();
            newProductType.ValidationRuleList.CheckAll();
            newProductType.MarkforUpdate();

            newProductType.Save();

        }

        public IList<ProductType> SortProdutTypeList(IList<ProductType> lstProdutTypes, string propName, string sortOrder)
        {
            return base.SortObjectList(lstProdutTypes, propName, sortOrder);


        }


        public void DeleteProductType( int ProductTypeId )
        {
            ProductType productType = ProductType.Fetch(ProductTypeId);
            productType.Remove();
            productType.SetPropertySet();
            productType.ValidationRuleList.CheckAll();
            
            productType.Save();


        }
    }
}

