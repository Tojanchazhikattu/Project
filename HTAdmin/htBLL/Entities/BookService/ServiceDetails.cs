using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using htDAL;
using System.ComponentModel.DataAnnotations;

namespace htBLL
{

    public class ServiceDetails : BusinessBase<ServiceDetails>
    {
        #region Instance Properties

        public int ServiceDetailsId { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Please select CollectionDateTime")]
        [DisplayFormat(DataFormatString = "{0:s}")]
        public DateTime? CollectionDateTime { get; set; }


        public int? EngineerUserId { get; set; }

        public String MessageToEngineer { get; set; }

        public String OtherRepair { get; set; }

        public Decimal? OtherRepairCost { get; set; }

        public Decimal? UnlockingCost { get; set; }

        public Boolean? AdancePayment { get; set; }

        public Decimal? AdancePaymentAmount { get; set; }

        #endregion Instance Properties

        #region Data access logic

        #region Get()
        protected override DataRow Get(int ServiceDetailsId)
        {
            SqlParameter[] parameters = 		{			new SqlParameter("@ServiceDetailsId", System.Data.SqlDbType.Int)		};
            parameters[0].Value = ServiceDetailsId;
            try
            {
                OpenDatabase();
                DataTable dataTable = DataBase.QueryDataTable("sp_GetServiceDetails", parameters);
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
                DataTable dataTable = DataBase.QueryDataTable("sp_GetServiceDetailsList", null);
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
                SqlParameter[] parameters =             {             new SqlParameter("ServiceDetailsId",System.Data.SqlDbType.Int),             new SqlParameter("CollectionDateTime",System.Data.SqlDbType.DateTime),             new SqlParameter("EngineerUserId",System.Data.SqlDbType.Int),             new SqlParameter("MessageToEngineer",System.Data.SqlDbType.VarChar,500),             new SqlParameter("OtherRepair",System.Data.SqlDbType.VarChar,500),             new SqlParameter("OtherRepairCost",System.Data.SqlDbType.Decimal),             new SqlParameter("UnlockingCost",System.Data.SqlDbType.Decimal),             new SqlParameter("AdancePayment",System.Data.SqlDbType.Bit),             new SqlParameter("AdancePaymentAmount",System.Data.SqlDbType.Decimal),            };

                parameters[0].Value = (object)ServiceDetailsId ?? DBNull.Value;
                parameters[1].Value = (object)CollectionDateTime ?? DBNull.Value;
                parameters[2].Value = (object)EngineerUserId ?? DBNull.Value;
                parameters[3].Value = (object)MessageToEngineer ?? DBNull.Value;
                parameters[4].Value = (object)OtherRepair ?? DBNull.Value;
                parameters[5].Value = (object)OtherRepairCost ?? DBNull.Value;
                parameters[6].Value = (object)UnlockingCost ?? DBNull.Value;
                parameters[7].Value = (object)AdancePayment ?? DBNull.Value;
                parameters[8].Value = (object)AdancePaymentAmount ?? DBNull.Value;
                OpenDatabase();
                DataBase.RunNonQuery("sp_UpdateServiceDetails", parameters);
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
            Delete(ServiceDetailsId);
        }
        protected override void Delete(int ServiceDetailsId)
        {
            SqlParameter[] parameters = 	        {		        new SqlParameter("@ServiceDetailsId", System.Data.SqlDbType.Int)	        };
            parameters[0].Value = (object)ServiceDetailsId ?? DBNull.Value;
            try
            {
                OpenDatabase();
                DataBase.RunNonQuery("sp_DeleteServiceDetails", parameters);
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
            SqlParameter[] parameters =             {             new SqlParameter("CollectionDateTime",System.Data.SqlDbType.DateTime),             new SqlParameter("EngineerUserId",System.Data.SqlDbType.Int),             new SqlParameter("MessageToEngineer",System.Data.SqlDbType.VarChar,500),             new SqlParameter("OtherRepair",System.Data.SqlDbType.VarChar,500),             new SqlParameter("OtherRepairCost",System.Data.SqlDbType.Decimal),             new SqlParameter("UnlockingCost",System.Data.SqlDbType.Decimal),             new SqlParameter("AdancePayment",System.Data.SqlDbType.Bit),             new SqlParameter("AdancePaymentAmount",System.Data.SqlDbType.Decimal),            };

            parameters[0].Value = (object)CollectionDateTime ?? DBNull.Value;
            parameters[1].Value = (object)EngineerUserId ?? DBNull.Value;
            parameters[2].Value = (object)MessageToEngineer ?? DBNull.Value;
            parameters[3].Value = (object)OtherRepair ?? DBNull.Value;
            parameters[4].Value = (object)OtherRepairCost ?? DBNull.Value;
            parameters[5].Value = (object)UnlockingCost ?? DBNull.Value;
            parameters[6].Value = (object)AdancePayment ?? DBNull.Value;
            parameters[7].Value = (object)AdancePaymentAmount ?? DBNull.Value;

            try
            {
                OpenDatabase();
                object returnObj = DataBase.QuerySingleValue("sp_AddServiceDetails", parameters);
                int newServiceDetailsId;
                if (Int32.TryParse(returnObj.ToString(), out newServiceDetailsId))
                {

                    return newServiceDetailsId;

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

                if (!dataRow.IsNull("ServiceDetailsId")) ServiceDetailsId = Convert.ToInt32(dataRow["ServiceDetailsId"]);
                if (!dataRow.IsNull("CollectionDateTime")) CollectionDateTime = Convert.ToDateTime(dataRow["CollectionDateTime"]);
                if (!dataRow.IsNull("EngineerUserId")) EngineerUserId = Convert.ToInt32(dataRow["EngineerUserId"]);
                if (!dataRow.IsNull("MessageToEngineer")) MessageToEngineer = Convert.ToString(dataRow["MessageToEngineer"]);
                if (!dataRow.IsNull("OtherRepair")) OtherRepair = Convert.ToString(dataRow["OtherRepair"]);
                if (!dataRow.IsNull("OtherRepairCost")) OtherRepairCost = Convert.ToDecimal(dataRow["OtherRepairCost"]);
                if (!dataRow.IsNull("UnlockingCost")) UnlockingCost = Convert.ToDecimal(dataRow["UnlockingCost"]);
                if (!dataRow.IsNull("AdancePayment")) AdancePayment = Convert.ToBoolean(dataRow["AdancePayment"]);
                if (!dataRow.IsNull("AdancePaymentAmount")) AdancePaymentAmount = Convert.ToDecimal(dataRow["AdancePaymentAmount"]);
            }

        }

        public void SetPropertySet()
        {

            if (ServiceDetailsId > 0) PropertySet("ServiceDetailsId");
        }

        #endregion Methods
    }

}
