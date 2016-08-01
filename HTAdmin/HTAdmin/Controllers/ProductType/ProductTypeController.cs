using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using htBLL;

namespace HTAdmin.Controllers
{
    public class ProductTypeController : Controller
    {

        public ActionResult ProductType()
        {
            return PartialView();
        }

        //
        // GET: /ProductType/
        [HttpPost]
        public JsonResult ProductTypeList(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                ProductTypeServices productTypeServices = new ProductTypeServices();
                var ProdutTypes = productTypeServices.GetAllProdutType();
                var produtTypesCount = ProdutTypes.Count;
                string[] sortParms = jtSorting.Split(' ');
                var produtTypesSorted = productTypeServices.SortObjectList(ProdutTypes, sortParms[0], sortParms[1]);


                return Json(new { Result = "OK", Records = produtTypesSorted, TotalRecordCount = produtTypesCount });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }

        }


        [HttpPost]
        public JsonResult CreateProductType(ProductType productType)
        {
            try
            {
                //productType.AddBusinessRules();
                if (!ModelState.IsValid)
                {
                    return Json(new { Result = "ERROR", Message = "Form is not valid! Please correct it and try again." });
                }
                ProductTypeServices productTypeServices = new ProductTypeServices();
                var addedProductType = productTypeServices.AddProductType(productType);
                return Json(new { Result = "OK", Record = addedProductType });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }


        [HttpPost]
        public JsonResult UpdateProductType(ProductType productType)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(new { Result = "ERROR", Message = "Form is not valid! Please correct it and try again." });
                }

                ProductTypeServices productTypeServices = new ProductTypeServices();
                productTypeServices.UpdateProductType(productType);

                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        
        [HttpPost]
        public JsonResult DeleteProductType(int ProductTypeId)
    {
        try
        {

            ProductTypeServices productTypeServices = new ProductTypeServices();
            productTypeServices.DeleteProductType(ProductTypeId);

            
            return Json(new { Result = "OK" });
        }
        catch (Exception ex)
        {
            return Json(new { Result = "ERROR", Message = ex.Message });
        }
    }
 


    }
}
