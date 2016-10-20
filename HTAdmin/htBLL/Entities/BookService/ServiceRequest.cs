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
    public enum RequestStatus : int
    {
        New = 1,
        AssignedToEngineer=2

    }
    public class ServiceRequest : BusinessBase<ServiceRequest>
    {
        #region Instance Properties

        public int ServiceRequestId { get; set; }

        public int? DeviceTypeID { get; set; }

        public int? HandsetModelID { get; set; }

        public int? DeviceColourID { get; set; }

        public String IMEINo { get; set; }

        public String PassCode { get; set; }

        public int? NetworkID { get; set; }

        public Boolean? Handset { get; set; }

        public Boolean? Simcard { get; set; }

        public Boolean? MemoryCard { get; set; }

        public Boolean? Battery { get; set; }

        public Boolean? BackCover { get; set; }

        public String OtherAccessories { get; set; }

        public int? SearchChannelId { get; set; }

        public int? RepairTypeId { get; set; }

        public int? CustomerInformationId { get; set; }

        public int? ServiceDetailsId { get; set; }

        public int? ServiceRequestTypeId { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? LastUpdatedDate { get; set; }

        public int? RequestStatusID { get; set; } 

        #endregion Instance Properties

        #region Data access logic

        #region Get()
        protected override DataRow Get(int ServiceRequestId)
        {
            SqlParameter[] parameters = 		{			new SqlParameter("@ServiceRequestId", System.Data.SqlDbType.Int)		};
            parameters[0].Value = ServiceRequestId;
            try
            {
                OpenDatabase();
                DataTable dataTable = DataBase.QueryDataTable("sp_GetServiceRequest", parameters);
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
                DataTable dataTable = DataBase.QueryDataTable("sp_GetServiceRequestList", null);
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
            try
            {
                SqlParameter[] parameters =             {             new SqlParameter("ServiceRequestId",System.Data.SqlDbType.Int),             new SqlParameter("DeviceTypeID",System.Data.SqlDbType.Int),             new SqlParameter("HandsetModelID",System.Data.SqlDbType.Int),             new SqlParameter("DeviceColourID",System.Data.SqlDbType.Int),             new SqlParameter("IMEINo",System.Data.SqlDbType.VarChar,25),             new SqlParameter("PassCode",System.Data.SqlDbType.VarChar,25),             new SqlParameter("NetworkID",System.Data.SqlDbType.Int),             new SqlParameter("Handset",System.Data.SqlDbType.Bit),             new SqlParameter("Simcard",System.Data.SqlDbType.Bit),             new SqlParameter("MemoryCard",System.Data.SqlDbType.Bit),             new SqlParameter("Battery",System.Data.SqlDbType.Bit),             new SqlParameter("BackCover",System.Data.SqlDbType.Bit),             new SqlParameter("OtherAccessories",System.Data.SqlDbType.VarChar,100),             new SqlParameter("SearchChannelId",System.Data.SqlDbType.Int),             new SqlParameter("RepairTypeId",System.Data.SqlDbType.Int),             new SqlParameter("CustomerInformationId",System.Data.SqlDbType.Int),             new SqlParameter("ServiceDetailsId",System.Data.SqlDbType.Int),             new SqlParameter("ServiceRequestTypeId",System.Data.SqlDbType.Int),             new SqlParameter("CreatedDate",System.Data.SqlDbType.DateTime),             new SqlParameter("LastUpdatedDate",System.Data.SqlDbType.DateTime),             new SqlParameter("RequestStatusID",System.Data.SqlDbType.Int)                        };

                parameters[0].Value = (object)ServiceRequestId ?? DBNull.Value;
                parameters[1].Value = (object)DeviceTypeID ?? DBNull.Value;
                parameters[2].Value = (object)HandsetModelID ?? DBNull.Value;
                parameters[3].Value = (object)DeviceColourID ?? DBNull.Value;
                parameters[4].Value = (object)IMEINo ?? DBNull.Value;
                parameters[5].Value = (object)PassCode ?? DBNull.Value;
                parameters[6].Value = (object)NetworkID ?? DBNull.Value;
                parameters[7].Value = (object)Handset ?? DBNull.Value;
                parameters[8].Value = (object)Simcard ?? DBNull.Value;
                parameters[9].Value = (object)MemoryCard ?? DBNull.Value;
                parameters[10].Value = (object)Battery ?? DBNull.Value;
                parameters[11].Value = (object)BackCover ?? DBNull.Value;
                parameters[12].Value = (object)OtherAccessories ?? DBNull.Value;
                parameters[13].Value = (object)SearchChannelId ?? DBNull.Value;
                parameters[14].Value = (object)RepairTypeId ?? DBNull.Value;
                parameters[15].Value = (object)CustomerInformationId ?? DBNull.Value;
                parameters[16].Value = (object)ServiceDetailsId ?? DBNull.Value;
                parameters[17].Value = (object)ServiceRequestTypeId ?? DBNull.Value;
                parameters[18].Value = (object)CreatedDate ?? DBNull.Value;
                parameters[19].Value = (object)LastUpdatedDate ?? DBNull.Value;
                parameters[20].Value = (object)RequestStatusID ?? DBNull.Value;
                OpenDatabase();
                DataBase.RunNonQuery("sp_UpdateServiceRequest", parameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseDatabase();
            }

        }

        #endregion Update()

        #region Delete()
        protected override void Delete()
        {
            Delete(ServiceRequestId);
        }
        protected override void Delete(int ServiceRequestId)
        {
            SqlParameter[] parameters = 	        {		        new SqlParameter("@ServiceRequestId", System.Data.SqlDbType.Int)	        };
            parameters[0].Value = (object)ServiceRequestId ?? DBNull.Value;
            try
            {
                OpenDatabase();
                DataBase.RunNonQuery("sp_DeleteServiceRequest", parameters);
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
            SqlParameter[] parameters =              {             new SqlParameter("DeviceTypeID",System.Data.SqlDbType.Int),             new SqlParameter("HandsetModelID",System.Data.SqlDbType.Int),             new SqlParameter("DeviceColourID",System.Data.SqlDbType.Int),             new SqlParameter("IMEINo",System.Data.SqlDbType.VarChar,25),             new SqlParameter("PassCode",System.Data.SqlDbType.VarChar,25),             new SqlParameter("NetworkID",System.Data.SqlDbType.Int),             new SqlParameter("Handset",System.Data.SqlDbType.Bit),             new SqlParameter("Simcard",System.Data.SqlDbType.Bit),             new SqlParameter("MemoryCard",System.Data.SqlDbType.Bit),             new SqlParameter("Battery",System.Data.SqlDbType.Bit),             new SqlParameter("BackCover",System.Data.SqlDbType.Bit),             new SqlParameter("OtherAccessories",System.Data.SqlDbType.VarChar,100),             new SqlParameter("SearchChannelId",System.Data.SqlDbType.Int),             new SqlParameter("RepairTypeId",System.Data.SqlDbType.Int),             new SqlParameter("CustomerInformationId",System.Data.SqlDbType.Int),             new SqlParameter("ServiceDetailsId",System.Data.SqlDbType.Int),            new SqlParameter("ServiceRequestTypeId",System.Data.SqlDbType.Int),            new SqlParameter("CreatedDate",System.Data.SqlDbType.DateTime),            new SqlParameter("LastUpdatedDate",System.Data.SqlDbType.DateTime),            new SqlParameter("RequestStatusID",System.Data.SqlDbType.Int),            };

            parameters[0].Value = (object)DeviceTypeID ?? DBNull.Value;
            parameters[1].Value = (object)HandsetModelID ?? DBNull.Value;
            parameters[2].Value = (object)DeviceColourID ?? DBNull.Value;
            parameters[3].Value = (object)IMEINo ?? DBNull.Value;
            parameters[4].Value = (object)PassCode ?? DBNull.Value;
            parameters[5].Value = (object)NetworkID ?? DBNull.Value;
            parameters[6].Value = (object)Handset ?? DBNull.Value;
            parameters[7].Value = (object)Simcard ?? DBNull.Value;
            parameters[8].Value = (object)MemoryCard ?? DBNull.Value;
            parameters[9].Value = (object)Battery ?? DBNull.Value;
            parameters[10].Value = (object)BackCover ?? DBNull.Value;
            parameters[11].Value = (object)OtherAccessories ?? DBNull.Value;
            parameters[12].Value = (object)SearchChannelId ?? DBNull.Value;
            parameters[13].Value = (object)RepairTypeId ?? DBNull.Value;
            parameters[14].Value = (object)CustomerInformationId ?? DBNull.Value;
            
            parameters[15].Value = (object)ServiceDetailsId ?? DBNull.Value;

            parameters[16].Value = (object)ServiceRequestTypeId ?? DBNull.Value;
            parameters[17].Value = (object)CreatedDate ?? DBNull.Value;
            parameters[18].Value = (object)LastUpdatedDate ?? DBNull.Value;
            parameters[19].Value = (object)RequestStatusID ?? DBNull.Value;

            try
            {
                OpenDatabase();
                object returnObj = DataBase.QuerySingleValue("sp_AddServiceRequest", parameters);
                int newServiceRequestId;
                if (Int32.TryParse(returnObj.ToString(), out newServiceRequestId))
                {

                    return newServiceRequestId;

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
            //CustomerInformationId 
            //    ServiceDetailsId
            ValidationRuleList.AddRule((new RuleMethods()).IsRequired, new RuleCriteria("CustomerInformationId", "CustomerInformationId is required."));
            ValidationRuleList.AddRule((new RuleMethods()).IsRequired, new RuleCriteria("ServiceDetailsId", "ServiceDetailsId is required."));

        }

        public override void Map(DataRow dataRow)
        {
            if (dataRow != null)
            {

                if (!dataRow.IsNull("ServiceRequestId")) ServiceRequestId = Convert.ToInt32(dataRow["ServiceRequestId"]);
                if (!dataRow.IsNull("DeviceTypeID")) DeviceTypeID = Convert.ToInt32(dataRow["DeviceTypeID"]);
                if (!dataRow.IsNull("HandsetModelID")) HandsetModelID = Convert.ToInt32(dataRow["HandsetModelID"]);
                if (!dataRow.IsNull("DeviceColourID")) DeviceColourID = Convert.ToInt32(dataRow["DeviceColourID"]);
                if (!dataRow.IsNull("IMEINo")) IMEINo = Convert.ToString(dataRow["IMEINo"]);
                if (!dataRow.IsNull("PassCode")) PassCode = Convert.ToString(dataRow["PassCode"]);
                if (!dataRow.IsNull("NetworkID")) NetworkID = Convert.ToInt32(dataRow["NetworkID"]);
                if (!dataRow.IsNull("Handset")) Handset = Convert.ToBoolean(dataRow["Handset"]);
                if (!dataRow.IsNull("Simcard")) Simcard = Convert.ToBoolean(dataRow["Simcard"]);
                if (!dataRow.IsNull("MemoryCard")) MemoryCard = Convert.ToBoolean(dataRow["MemoryCard"]);
                if (!dataRow.IsNull("Battery")) Battery = Convert.ToBoolean(dataRow["Battery"]);
                if (!dataRow.IsNull("BackCover")) BackCover = Convert.ToBoolean(dataRow["BackCover"]);
                if (!dataRow.IsNull("OtherAccessories")) OtherAccessories = Convert.ToString(dataRow["OtherAccessories"]);
                if (!dataRow.IsNull("RepairTypeId")) RepairTypeId = Convert.ToInt32(dataRow["RepairTypeId"]);
                if (!dataRow.IsNull("CustomerInformationId")) CustomerInformationId = Convert.ToInt32(dataRow["CustomerInformationId"]);
                if (!dataRow.IsNull("ServiceDetailsId")) ServiceDetailsId = Convert.ToInt32(dataRow["ServiceDetailsId"]);
                if (!dataRow.IsNull("ServiceRequestTypeId")) ServiceRequestTypeId = Convert.ToInt32(dataRow["ServiceRequestTypeId"]);
                if (!dataRow.IsNull("CreatedDate")) CreatedDate = Convert.ToDateTime(dataRow["CreatedDate"]);
                if (!dataRow.IsNull("LastUpdatedDate")) LastUpdatedDate = Convert.ToDateTime(dataRow["LastUpdatedDate"]);
                if (!dataRow.IsNull("RequestStatusID")) RequestStatusID = Convert.ToInt32(dataRow["RequestStatusID"]);
                if (!dataRow.IsNull("SearchChannelId")) SearchChannelId = Convert.ToInt32(dataRow["SearchChannelId"]);

            }

        }

        public void SetPropertySet(string prop)
        {
            PropertySet(prop);

        }

        #endregion Methods
    }

}
