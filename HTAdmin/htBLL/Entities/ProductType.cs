﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using htDAL;

namespace htBLL
{
    public class ProductType : BusinessBase<ProductType>

         public void SetPropertySet()
         {
             if (ProductTypeId > 0) PropertySet("ProductTypeId");
             if (!string.IsNullOrEmpty(Name)) PropertySet("Name");

         }

        #region GetAll()
        protected override DataTable GetAll()
        {
            try
            {
                OpenDatabase();
                DataTable dataTable = DataBase.QueryDataTable("sp_GetProductTypeList", null);
                return dataTable;
            }
            finally
            {
                CloseDatabase();
            }
        }

        #endregion GetAll()
            try
            {

                SqlParameter[] parameters = 

                parameters[0].Value = (object)ProductTypeId ?? DBNull.Value;
                parameters[1].Value = (object)Name ?? DBNull.Value;

                OpenDatabase();

                DataBase.RunNonQuery("sp_UpdateProductType", parameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseDatabase();
            }
            Delete(ProductTypeId);
                object returnObj=DataBase.QuerySingleValue("sp_AddProductType", parameters);
                if (Int32.TryParse(returnObj.ToString(), out newProdutTypeId))
                {
                    return newProdutTypeId;
                }
                return 0;
        ValidationRuleList.AddRule((new RuleMethods()).IsRequired, new RuleCriteria("Name", "Name is required."));
}