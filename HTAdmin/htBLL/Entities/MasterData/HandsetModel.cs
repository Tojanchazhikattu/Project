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
    public class HandsetModel : BusinessBase<HandsetModel>
    {
        #region Instance Properties

        public int HandsetModelId { get; set; }

        public String Name { get; set; }

        public int ProductTypeId { get; set; }

        public int DeviceTypeId { get; set; }

        #endregion Instance Properties

        #region Data access logic

        #region Get()
        protected override DataRow Get(int HandsetModelId)
        {
            SqlParameter[] parameters = 		{			new SqlParameter("@HandsetModelId", System.Data.SqlDbType.Int)		};
            parameters[0].Value = HandsetModelId;
            try
            {
                OpenDatabase();
                DataTable dataTable = DataBase.QueryDataTable("sp_GetHandsetModel", parameters);
                return (dataTable.Rows.Count > 0) ? dataTable.Rows[0] : null;
            }
            finally
            {
                CloseDatabase();
            }
        }

        #endregion Get()

        #region GetAll()
        protected override DataTable GetAll()
        {
            try
            {
                OpenDatabase();
                DataTable dataTable = DataBase.QueryDataTable("sp_GetHandsetModelList", null);
                return dataTable;
            }
            finally
            {
                CloseDatabase();
            }
        }

        

        #endregion GetAll()

        #region Update()
        protected override void Update()
        {
            SqlParameter[] parameters =             {             new SqlParameter("HandsetModelId",System.Data.SqlDbType.Int),             new SqlParameter("name",System.Data.SqlDbType.VarChar,100),             new SqlParameter("ProductTypeId",System.Data.SqlDbType.Int),             new SqlParameter("DeviceTypeId",System.Data.SqlDbType.Int),            };

            parameters[0].Value = (object)HandsetModelId ?? DBNull.Value;
            parameters[1].Value = (object)Name ?? DBNull.Value;
            parameters[2].Value = (object)ProductTypeId ?? DBNull.Value;
            parameters[3].Value = (object)DeviceTypeId ?? DBNull.Value;
            DataBase.RunNonQuery("sp_UpdateHandsetModel", parameters);

        }

        #endregion Update()

        #region Delete()
        protected override void Delete()
        {
            Delete(HandsetModelId);
        }
        protected override void Delete(int HandsetModelId)
        {
            SqlParameter[] parameters = 	        {		        new SqlParameter("@HandsetModelId", System.Data.SqlDbType.Int)	        };
                    parameters[0].Value = (object)HandsetModelId ?? DBNull.Value;
            try
            {
                OpenDatabase();
                DataBase.RunNonQuery("sp_DeleteHandsetModel", parameters);
            }
            finally
            {
                CloseDatabase();
            }
        }

        #endregion Delete()

        #region Add()
        protected override int Add()
        {
            SqlParameter[] parameters =             {             new SqlParameter("name",System.Data.SqlDbType.VarChar,100),             new SqlParameter("ProductTypeId",System.Data.SqlDbType.Int),             new SqlParameter("DeviceTypeId",System.Data.SqlDbType.Int),            };

            parameters[0].Value = (object)Name ?? DBNull.Value;
            parameters[1].Value = (object)ProductTypeId ?? DBNull.Value;
            parameters[2].Value = (object)DeviceTypeId ?? DBNull.Value;

            try
            {
                OpenDatabase();
                object returnObj = DataBase.QuerySingleValue("sp_AddHandsetModel", parameters);
                int newHandsetModelId;
                if (Int32.TryParse(returnObj.ToString(), out newHandsetModelId))
                {

                    return newHandsetModelId;

                }

                return 0;

            }

            finally
            {
                CloseDatabase();

            }

        }

        #endregion Add()

        #endregion Data access logic

        #region Methods

        protected override void AddBusinessRules()
        {

            ValidationRuleList.AddRule((new RuleMethods()).IsRequired, new RuleCriteria("name", "name is required."));
            ValidationRuleList.AddRule((new RuleMethods()).IsRequired, new RuleCriteria("ProductTypeId", "ProductTypeId is required."));
            ValidationRuleList.AddRule((new RuleMethods()).IsRequired, new RuleCriteria("DeviceTypeId", "DeviceTypeId is required."));
        }

        public override void Map(DataRow dataRow)
        {
            if (dataRow != null)
            {

                if (!dataRow.IsNull("HandsetModelId")) HandsetModelId = Convert.ToInt32(dataRow["HandsetModelId"]);
                if (!dataRow.IsNull("name")) Name = Convert.ToString(dataRow["name"]);
                if (!dataRow.IsNull("ProductTypeId")) ProductTypeId = Convert.ToInt32(dataRow["ProductTypeId"]);
                if (!dataRow.IsNull("DeviceTypeId")) DeviceTypeId = Convert.ToInt32(dataRow["DeviceTypeId"]);
            }

        }

        public void SetPropertySet()
        {

            if (HandsetModelId > 0) PropertySet("HandsetModelId");

            if (!string.IsNullOrEmpty(Name)) PropertySet("name");

            if (ProductTypeId > 0) PropertySet("ProductTypeId");

            if (DeviceTypeId > 0) PropertySet("DeviceTypeId");
        }

        #endregion Methods
    }
}
