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
    public class DeviceType: BusinessBase<DeviceType>
    {
        #region Instance Properties

        public int DeviceTypeId { get; set; }

        public String Name { get; set; }

        public String Description { get; set; }

        #endregion Instance Properties

        #region Data access logic

        #region Get()
        protected override DataRow Get(int DeviceTypeId)
        {
            SqlParameter[] parameters = 		    {			    new SqlParameter("@DeviceTypeId", System.Data.SqlDbType.Int)		    };
            parameters[0].Value = DeviceTypeId;
            try
            {
                OpenDatabase();
                DataTable dataTable = DataBase.QueryDataTable("sp_GetDeviceType", parameters);
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
                DataTable dataTable = DataBase.QueryDataTable("sp_GetDeviceTypeList", null);
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
            SqlParameter[] parameters =             {             new SqlParameter("DeviceTypeId",System.Data.SqlDbType.Int),             new SqlParameter("Name",System.Data.SqlDbType.VarChar,100),             new SqlParameter("Description",System.Data.SqlDbType.VarChar,100),            };

            parameters[0].Value = (object)DeviceTypeId ?? DBNull.Value;
            parameters[1].Value = (object)Name ?? DBNull.Value;
            parameters[2].Value = (object)Description ?? DBNull.Value;
            DataBase.RunNonQuery("sp_UpdateDeviceType", parameters);

        }

        #endregion Update()

        #region Delete()
        protected override void Delete()
        {
            Delete(DeviceTypeId);
        }
        protected override void Delete(int DeviceTypeId)
        {
            SqlParameter[] parameters = 	        {		        new SqlParameter("@DeviceTypeId", System.Data.SqlDbType.Int)	        };
            parameters[0].Value = (object)DeviceTypeId ?? DBNull.Value;
            try
            {
                OpenDatabase();
                DataBase.RunNonQuery("sp_DeleteDeviceType", parameters);
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
            SqlParameter[] parameters =             {             new SqlParameter("Name",System.Data.SqlDbType.VarChar,100),             new SqlParameter("Description",System.Data.SqlDbType.VarChar,100),            };

            parameters[0].Value = (object)Name ?? DBNull.Value;
            parameters[1].Value = (object)Description ?? DBNull.Value;

            try
            {
                OpenDatabase();
                object returnObj = DataBase.QuerySingleValue("sp_AddDeviceType", parameters);
                int newDeviceTypeId;
                if (Int32.TryParse(returnObj.ToString(), out newDeviceTypeId))
                {

                    return newDeviceTypeId;

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

            ValidationRuleList.AddRule((new RuleMethods()).IsRequired, new RuleCriteria("Name", "Name is required."));
            ValidationRuleList.AddRule((new RuleMethods()).IsRequired, new RuleCriteria("Description", "Description is required."));
        }

        public override void Map(DataRow dataRow)
        {
            if (dataRow != null)
            {

                if (!dataRow.IsNull("DeviceTypeId")) DeviceTypeId = Convert.ToInt32(dataRow["DeviceTypeId"]);
                if (!dataRow.IsNull("Name")) Name = Convert.ToString(dataRow["Name"]);
                if (!dataRow.IsNull("Description")) Description = Convert.ToString(dataRow["Description"]);
            }

        }

        public void SetPropertySet()
        {

            if (DeviceTypeId > 0) PropertySet("DeviceTypeId");

            if (!string.IsNullOrEmpty(Name)) PropertySet("Name");

            if (!string.IsNullOrEmpty(Description)) PropertySet("Description");
        }

        #endregion Methods
    }
}
