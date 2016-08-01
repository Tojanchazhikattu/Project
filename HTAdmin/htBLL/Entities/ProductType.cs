using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using htDAL;

namespace htBLL
{
    public class ProductType : BusinessBase<ProductType>{    #region Instance Properties         public int ProductTypeId { get; set; }          public String Name { get; set; }     #endregion Instance Properties

         public void SetPropertySet()
         {
             if (ProductTypeId > 0) PropertySet("ProductTypeId");
             if (!string.IsNullOrEmpty(Name)) PropertySet("Name");

         }    #region Instance Methods    #region Get()        protected override DataRow Get(int ProductTypeId)	    {			SqlParameter[] parameters = 		    {			    new SqlParameter("@ProductTypeId", System.Data.SqlDbType.Int)		    };	        parameters[0].Value = ProductTypeId;	        try             {		        OpenDatabase();		        DataTable dataTable = DataBase.QueryDataTable("sp_GetProductType", parameters);		        return (dataTable.Rows.Count > 0) ? dataTable.Rows[0] : null;	        }	        finally {		        CloseDatabase();	        }        }    #endregion Get()

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

        #endregion GetAll()    #region Update()        protected override void Update()        {
            try
            {

                SqlParameter[] parameters =             {             new SqlParameter("ProductTypeId",System.Data.SqlDbType.Int),             new SqlParameter("Name",System.Data.SqlDbType.VarChar,100),            };

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
            }        }    #endregion Update()    #region Delete()        protected override void Delete()        {
            Delete(ProductTypeId);        }        protected override void Delete(int ProductTypeId)        {	        SqlParameter[] parameters = 	        {		        new SqlParameter("@ProductTypeId", System.Data.SqlDbType.Int)	        };	        parameters[0].Value = (object)ProductTypeId?? DBNull.Value;	        try {		        OpenDatabase();		        DataBase.RunNonQuery("sp_DeleteProductType", parameters);	        }	        finally {		        CloseDatabase();	        }        }    #endregion Delete()    #region Add()        protected override int Add()        {            SqlParameter[] parameters =             {             new SqlParameter("Name",System.Data.SqlDbType.VarChar,100),            };            //parameters[0].Value = (object)ProductTypeId?? DBNull.Value;            parameters[0].Value = (object)Name?? DBNull.Value;            try            {		        OpenDatabase();
                object returnObj=DataBase.QuerySingleValue("sp_AddProductType", parameters);                int newProdutTypeId;
                if (Int32.TryParse(returnObj.ToString(), out newProdutTypeId))
                {
                    return newProdutTypeId;
                }
                return 0;            }            finally            {                CloseDatabase();            }        }    #endregion Add()#endregion     #region Methods    protected override void AddBusinessRules()    {       // ValidationRuleList.AddRule((new RuleMethods()).IsRequired, new RuleCriteria("ProductTypeId","ProductTypeId is required."));
        ValidationRuleList.AddRule((new RuleMethods()).IsRequired, new RuleCriteria("Name", "Name is required."));    }    public override void Map(DataRow dataRow)    {        if (dataRow != null){            if (!dataRow.IsNull("ProductTypeId")) ProductTypeId=Convert.ToInt32(dataRow["ProductTypeId"]);            if (!dataRow.IsNull("Name")) Name=Convert.ToString(dataRow["Name"]);        }    }#endregion Methods}
}
