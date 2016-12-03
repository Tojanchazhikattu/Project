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
    public class ServiceEquipmentMapping : BusinessBase<ServiceEquipmentMapping>
    {

        #region Instance Properties

        public int ServiceEquipmentMappingId { get; set; }

        public int? ServiceDetailsId { get; set; }

        public int? EquipmentsId { get; set; }

        #endregion Instance Properties

        #region Data access logic

        #region Get()
        protected override DataRow Get(int ServiceEquipmentMappingId)
        {
            SqlParameter[] parameters = 		{			new SqlParameter("@ServiceEquipmentMappingId", System.Data.SqlDbType.Int)		};
            parameters[0].Value = ServiceEquipmentMappingId;
            try
            {
                OpenDatabase();
                DataTable dataTable = DataBase.QueryDataTable("sp_GetServiceEquipmentMapping", parameters);
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
                DataTable dataTable = DataBase.QueryDataTable("sp_GetServiceEquipmentMappingList", null);
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
            SqlParameter[] parameters =             {             new SqlParameter("ServiceEquipmentMappingId",System.Data.SqlDbType.Int),             new SqlParameter("ServiceDetailsId",System.Data.SqlDbType.Int),             new SqlParameter("EquipmentsId",System.Data.SqlDbType.Int),            };

            parameters[0].Value = (object)ServiceEquipmentMappingId ?? DBNull.Value;
            parameters[1].Value = (object)ServiceDetailsId ?? DBNull.Value;
            parameters[2].Value = (object)EquipmentsId ?? DBNull.Value;
            DataBase.RunNonQuery("sp_UpdateServiceEquipmentMapping", parameters);

        }

        #endregion Update()

        #region Delete()
        protected override void Delete()
        {
            Delete(ServiceEquipmentMappingId);
        }
        protected override void Delete(int ServiceEquipmentMappingId)
        {
            SqlParameter[] parameters = 	        {		        new SqlParameter("@ServiceEquipmentMappingId", System.Data.SqlDbType.Int)	        };
            parameters[0].Value = (object)ServiceEquipmentMappingId ?? DBNull.Value;
            try
            {
                OpenDatabase();
                DataBase.RunNonQuery("sp_DeleteServiceEquipmentMapping", parameters);
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
            SqlParameter[] parameters =                 {                 new SqlParameter("ServiceDetailsId",System.Data.SqlDbType.Int),                 new SqlParameter("EquipmentsId",System.Data.SqlDbType.Int),                };

            parameters[0].Value = (object)ServiceDetailsId ?? DBNull.Value;
            parameters[1].Value = (object)EquipmentsId ?? DBNull.Value;

            try
            {
                OpenDatabase();
                object returnObj = DataBase.QuerySingleValue("sp_AddServiceEquipmentMapping", parameters);
                int newServiceEquipmentMappingId;
                if (Int32.TryParse(returnObj.ToString(), out newServiceEquipmentMappingId))
                {

                    return newServiceEquipmentMappingId;

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

                if (!dataRow.IsNull("ServiceEquipmentMappingId")) ServiceEquipmentMappingId = Convert.ToInt32(dataRow["ServiceEquipmentMappingId"]);
            }

        }

        public void SetPropertySet()
        {

            if (ServiceEquipmentMappingId > 0) PropertySet("ServiceEquipmentMappingId");
        }

        #endregion Methods


    }
}
