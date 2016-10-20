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
    //smart device repait Type
    public class SmartDeviceRepair : BusinessBase<SmartDeviceRepair>
    {
        #region Instance Properties

        public int id { get; set; }

        public String fault { get; set; }

        public int DeviceTypeId { get; set; }

        public String price { get; set; }

        public String repair_time { get; set; }

        #endregion Instance Properties

        #region Data access logic

        #region Get()
        protected override DataRow Get(int id)
        {
            SqlParameter[] parameters = 		{			new SqlParameter("@id", System.Data.SqlDbType.Int)		};
            parameters[0].Value = id;
            try
            {
                OpenDatabase();
                DataTable dataTable = DataBase.QueryDataTable("sp_GetSmartDeviceRepair", parameters);
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
                DataTable dataTable = DataBase.QueryDataTable("sp_GetSmartDeviceRepairList", null);
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
            SqlParameter[] parameters =             {             new SqlParameter("id",System.Data.SqlDbType.Int),             new SqlParameter("fault",System.Data.SqlDbType.VarChar,300),             new SqlParameter("DeviceTypeId",System.Data.SqlDbType.Int),             new SqlParameter("price",System.Data.SqlDbType.VarChar,10),             new SqlParameter("repair_time",System.Data.SqlDbType.VarChar,2),            };

            parameters[0].Value = (object)id ?? DBNull.Value;
            parameters[1].Value = (object)fault ?? DBNull.Value;
            parameters[2].Value = (object)DeviceTypeId ?? DBNull.Value;
            parameters[3].Value = (object)price ?? DBNull.Value;
            parameters[4].Value = (object)repair_time ?? DBNull.Value;
            DataBase.RunNonQuery("sp_UpdateSmartDeviceRepair", parameters);

        }

        #endregion Update()

        #region Delete()
        protected override void Delete()
        {
            Delete(id);
        }
        protected override void Delete(int id)
        {
            SqlParameter[] parameters = 	        {		        new SqlParameter("@id", System.Data.SqlDbType.Int)	        };
            parameters[0].Value = (object)id ?? DBNull.Value;
            try
            {
                OpenDatabase();
                DataBase.RunNonQuery("sp_DeleteSmartDeviceRepair", parameters);
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
            SqlParameter[] parameters =             {             new SqlParameter("fault",System.Data.SqlDbType.VarChar,300),             new SqlParameter("DeviceTypeId",System.Data.SqlDbType.Int),             new SqlParameter("price",System.Data.SqlDbType.VarChar,10),             new SqlParameter("repair_time",System.Data.SqlDbType.VarChar,2),            };

                        parameters[1].Value = (object)fault ?? DBNull.Value;
            parameters[2].Value = (object)DeviceTypeId ?? DBNull.Value;
            parameters[3].Value = (object)price ?? DBNull.Value;
            parameters[4].Value = (object)repair_time ?? DBNull.Value;

            try
            {
                OpenDatabase();
                object returnObj = DataBase.QuerySingleValue("sp_AddSmartDeviceRepair", parameters);
                int newid;
                if (Int32.TryParse(returnObj.ToString(), out newid))
                {

                    return newid;

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

            ValidationRuleList.AddRule((new RuleMethods()).IsRequired, new RuleCriteria("fault", "fault is required."));
            ValidationRuleList.AddRule((new RuleMethods()).IsRequired, new RuleCriteria("DeviceTypeId", "DeviceTypeId is required."));
            ValidationRuleList.AddRule((new RuleMethods()).IsRequired, new RuleCriteria("price", "price is required."));
        }

        public override void Map(DataRow dataRow)
        {
            if (dataRow != null)
            {

                if (!dataRow.IsNull("id")) id = Convert.ToInt32(dataRow["id"]);
                if (!dataRow.IsNull("fault")) fault = Convert.ToString(dataRow["fault"]);
                if (!dataRow.IsNull("DeviceTypeId")) DeviceTypeId = Convert.ToInt32(dataRow["DeviceTypeId"]);
                if (!dataRow.IsNull("price")) price = Convert.ToString(dataRow["price"]);
            }

        }

        public void SetPropertySet()
        {

            if (id > 0) PropertySet("id");

            if (!string.IsNullOrEmpty(fault)) PropertySet("fault");

            if (DeviceTypeId > 0) PropertySet("DeviceTypeId");

            if (!string.IsNullOrEmpty(price)) PropertySet("price");
        }

        #endregion Methods
    }

}


