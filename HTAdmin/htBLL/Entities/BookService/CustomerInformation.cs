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
    public class CustomerInformation : BusinessBase<CustomerInformation>
    {
        //SearchChannelId is part of ServiceRequest
        [Required(ErrorMessage = "Please select SearchChannelId")]
        public int SearchChannelId { get; set; }
        public string SearchChannelstrId { get; set; }
        //public string FirstName { get; set; }
        //public string LastName { get; set; }
        //public string ContactNo { get; set; }
        //public string Email { get; set; }
        //public string Address { get; set; }
        //public string PostCode { get; set; }

        public int CustomerInformationId { get; set; }
        [Required(ErrorMessage = "Please enter customer First Name")]
        public String FirstName { get; set; }
        [Required(ErrorMessage = "Please enter customer SurName")]
        public String LastName { get; set; }
        [Required(ErrorMessage = "Please enter customer contact no")]
        public String ContactNo { get; set; }
        [Required(ErrorMessage = "Please enter customer email")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public String Email { get; set; }

        public String Address { get; set; }

        public String PostCode { get; set; } 

        public IList<SearchChannel> lstSearchChannel;

        public CustomerInformation()
        {
            lstSearchChannel = SearchChannel.FetchAll();
        }

        #region Data access logic

        #region Get()
        protected override DataRow Get(int CustomerInformationId)
        {
            SqlParameter[] parameters = 
		{
			new SqlParameter("@CustomerInformationId", System.Data.SqlDbType.Int)
		};
            parameters[0].Value = CustomerInformationId;
            try
            {
                OpenDatabase();
                DataTable dataTable = DataBase.QueryDataTable("sp_GetCustomerInformation", parameters);
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
                DataTable dataTable = DataBase.QueryDataTable("sp_GetCustomerInformationList", null);
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
            SqlParameter[] parameters = 
            {

             new SqlParameter("CustomerInformationId",System.Data.SqlDbType.Int),

             new SqlParameter("FirstName",System.Data.SqlDbType.VarChar,100),

             new SqlParameter("LastName",System.Data.SqlDbType.VarChar,100),

             new SqlParameter("ContactNo",System.Data.SqlDbType.VarChar,25),

             new SqlParameter("Email",System.Data.SqlDbType.VarChar,100),

             new SqlParameter("Address",System.Data.SqlDbType.VarChar,255),

             new SqlParameter("PostCode",System.Data.SqlDbType.VarChar,10),
            };

            parameters[0].Value = (object)CustomerInformationId ?? DBNull.Value;
            parameters[1].Value = (object)FirstName ?? DBNull.Value;
            parameters[2].Value = (object)LastName ?? DBNull.Value;
            parameters[3].Value = (object)ContactNo ?? DBNull.Value;
            parameters[4].Value = (object)Email ?? DBNull.Value;
            parameters[5].Value = (object)Address ?? DBNull.Value;
            parameters[6].Value = (object)PostCode ?? DBNull.Value;
            DataBase.RunNonQuery("sp_UpdateCustomerInformation", parameters);

        }

        #endregion Update()

        #region Delete()
        protected override void Delete()
        {
            Delete(CustomerInformationId);
        }
        protected override void Delete(int CustomerInformationId)
        {
            SqlParameter[] parameters = 
	        {
		        new SqlParameter("@CustomerInformationId", System.Data.SqlDbType.Int)
	        };
            parameters[0].Value = (object)CustomerInformationId ?? DBNull.Value;
            try
            {
                OpenDatabase();
                DataBase.RunNonQuery("sp_DeleteCustomerInformation", parameters);
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
            SqlParameter[] parameters = 
            {

             new SqlParameter("FirstName",System.Data.SqlDbType.VarChar,100),

             new SqlParameter("LastName",System.Data.SqlDbType.VarChar,100),

             new SqlParameter("ContactNo",System.Data.SqlDbType.VarChar,25),

             new SqlParameter("Email",System.Data.SqlDbType.VarChar,100),

             new SqlParameter("Address",System.Data.SqlDbType.VarChar,255),

             new SqlParameter("PostCode",System.Data.SqlDbType.VarChar,10),
            };

            parameters[0].Value = (object)FirstName ?? DBNull.Value;
            parameters[1].Value = (object)LastName ?? DBNull.Value;
            parameters[2].Value = (object)ContactNo ?? DBNull.Value;
            parameters[3].Value = (object)Email ?? DBNull.Value;
            parameters[4].Value = (object)Address ?? DBNull.Value;
            parameters[5].Value = (object)PostCode ?? DBNull.Value;

            try
            {
                OpenDatabase();
                object returnObj = DataBase.QuerySingleValue("sp_AddCustomerInformation", parameters);
                int newCustomerInformationId;
                if (Int32.TryParse(returnObj.ToString(), out newCustomerInformationId))
                {

                    return newCustomerInformationId;

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

                if (!dataRow.IsNull("CustomerInformationId")) CustomerInformationId = Convert.ToInt32(dataRow["CustomerInformationId"]);
            }

        }

        public void SetPropertySet()
        {

            if (CustomerInformationId > 0) PropertySet("CustomerInformationId");
        }

        #endregion Methods

    }
}