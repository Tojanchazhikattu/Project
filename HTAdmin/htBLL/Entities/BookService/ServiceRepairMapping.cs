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
    public class ServiceRepairMapping : BusinessBase<ServiceRepairMapping>
    {
        #region Instance Properties

        public int ServiceRepairMappingID { get; set; }

        public int? ServiceDetailsId { get; set; }

        public int? RepairTypeId { get; set; }

        #endregion Instance Properties

        #region Data access logic

        #region Get()
        protected override DataRow Get(int ServiceRepairMappingID)
        {
            SqlParameter[] parameters = 		{			new SqlParameter("@ServiceRepairMappingID", System.Data.SqlDbType.Int)		};
            parameters[0].Value = ServiceRepairMappingID;
            try
            {
                OpenDatabase();
                DataTable dataTable = DataBase.QueryDataTable("sp_GetServiceRepairMapping", parameters);
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
                DataTable dataTable = DataBase.QueryDataTable("sp_GetServiceRepairMappingList", null);
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
            SqlParameter[] parameters =             {             new SqlParameter("ServiceRepairMappingID",System.Data.SqlDbType.Int),             new SqlParameter("ServiceDetailsId",System.Data.SqlDbType.Int),             new SqlParameter("RepairTypeId",System.Data.SqlDbType.Int),            };

            parameters[0].Value = (object)ServiceRepairMappingID ?? DBNull.Value;
            parameters[1].Value = (object)ServiceDetailsId ?? DBNull.Value;
            parameters[2].Value = (object)RepairTypeId ?? DBNull.Value;
            DataBase.RunNonQuery("sp_UpdateServiceRepairMapping", parameters);

        }

        #endregion Update()

        #region Delete()
        protected override void Delete()
        {
            Delete(ServiceRepairMappingID);
        }
        protected override void Delete(int ServiceRepairMappingID)
        {
            SqlParameter[] parameters = 	        {		        new SqlParameter("@ServiceRepairMappingID", System.Data.SqlDbType.Int)	        };
            parameters[0].Value = (object)ServiceRepairMappingID ?? DBNull.Value;
            try
            {
                OpenDatabase();
                DataBase.RunNonQuery("sp_DeleteServiceRepairMapping", parameters);
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
            SqlParameter[] parameters =             {             new SqlParameter("ServiceDetailsId",System.Data.SqlDbType.Int),             new SqlParameter("RepairTypeId",System.Data.SqlDbType.Int),            };

            parameters[0].Value = (object)ServiceDetailsId ?? DBNull.Value;
            parameters[1].Value = (object)RepairTypeId ?? DBNull.Value;

            try
            {
                OpenDatabase();
                object returnObj = DataBase.QuerySingleValue("sp_AddServiceRepairMapping", parameters);
                int newServiceRepairMappingID;
                if (Int32.TryParse(returnObj.ToString(), out newServiceRepairMappingID))
                {

                    return newServiceRepairMappingID;

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

        }

        public override void Map(DataRow dataRow)
        {
            if (dataRow != null)
            {

                if (!dataRow.IsNull("ServiceRepairMappingID")) ServiceRepairMappingID = Convert.ToInt32(dataRow["ServiceRepairMappingID"]);
            }

        }

        public void SetPropertySet()
        {

            if (ServiceRepairMappingID > 0) PropertySet("ServiceRepairMappingID");
        }

        #endregion Methods
    }

}
