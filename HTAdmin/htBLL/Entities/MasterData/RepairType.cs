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

    public class RepairType : BusinessBase<RepairType>
    {
        #region Instance Properties

        public int RepairTypeId { get; set; }

        public String Name { get; set; }

        #endregion Instance Properties

        #region Data access logic

        #region Get()
        protected override DataRow Get(int RepairTypeId)
        {
            SqlParameter[] parameters = 		{			new SqlParameter("@RepairTypeId", System.Data.SqlDbType.Int)		};
            parameters[0].Value = RepairTypeId;
            try
            {
                OpenDatabase();
                DataTable dataTable = DataBase.QueryDataTable("sp_GetRepairType", parameters);
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
                DataTable dataTable = DataBase.QueryDataTable("sp_GetRepairTypeList", null);
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
            SqlParameter[] parameters =                 {                 new SqlParameter("RepairTypeId",System.Data.SqlDbType.Int),                 new SqlParameter("Name",System.Data.SqlDbType.VarChar,100),                };

            parameters[0].Value = (object)RepairTypeId ?? DBNull.Value;
            parameters[1].Value = (object)Name ?? DBNull.Value;
            DataBase.RunNonQuery("sp_UpdateRepairType", parameters);

        }

        #endregion Update()

        #region Delete()
        protected override void Delete()
        {
            Delete(RepairTypeId);
        }
        protected override void Delete(int RepairTypeId)
        {
            SqlParameter[] parameters = 	        {		        new SqlParameter("@RepairTypeId", System.Data.SqlDbType.Int)	        };
            parameters[0].Value = (object)RepairTypeId ?? DBNull.Value;
            try
            {
                OpenDatabase();
                DataBase.RunNonQuery("sp_DeleteRepairType", parameters);
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
            SqlParameter[] parameters =             {             new SqlParameter("Name",System.Data.SqlDbType.VarChar,100),            };

            parameters[1].Value = (object)Name ?? DBNull.Value;

            try
            {
                OpenDatabase();
                object returnObj = DataBase.QuerySingleValue("sp_AddRepairType", parameters);
                int newRepairTypeId;
                if (Int32.TryParse(returnObj.ToString(), out newRepairTypeId))
                {

                    return newRepairTypeId;

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
        }

        public override void Map(DataRow dataRow)
        {
            if (dataRow != null)
            {

                if (!dataRow.IsNull("RepairTypeId")) RepairTypeId = Convert.ToInt32(dataRow["RepairTypeId"]);
                if (!dataRow.IsNull("Name")) Name = Convert.ToString(dataRow["Name"]);
            }

        }

        public void SetPropertySet()
        {

            if (RepairTypeId > 0) PropertySet("RepairTypeId");

            if (!string.IsNullOrEmpty(Name)) PropertySet("Name");
        }

        #endregion Methods
    }

}
