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
    public class User : BusinessBase<User>
    {
        #region Instance Properties

        public int UserId { get; set; }

        public String UserName { get; set; }

        public String Email { get; set; }

        public int UserTypeId { get; set; }

        public int Status { get; set; }

        #endregion Instance Properties

        #region Data access logic

        #region Get()
        protected override DataRow Get(int UserId)
        {
            SqlParameter[] parameters = 		{			new SqlParameter("@UserId", System.Data.SqlDbType.Int)		};
            parameters[0].Value = UserId;
            try
            {
                OpenDatabase();
                DataTable dataTable = DataBase.QueryDataTable("sp_GetUser", parameters);
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
                DataTable dataTable = DataBase.QueryDataTable("sp_GetUserList", null);
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
            SqlParameter[] parameters = { new SqlParameter("UserId",System.Data.SqlDbType.Int), new SqlParameter("UserName",System.Data.SqlDbType.VarChar,45), new SqlParameter("Email",System.Data.SqlDbType.VarChar,45), new SqlParameter("UserTypeId",System.Data.SqlDbType.Int), new SqlParameter("Status",System.Data.SqlDbType.Int),};

            parameters[0].Value = (object)UserId ?? DBNull.Value;
            parameters[1].Value = (object)UserName ?? DBNull.Value;
            parameters[2].Value = (object)Email ?? DBNull.Value;
            parameters[3].Value = (object)UserTypeId ?? DBNull.Value;
            parameters[4].Value = (object)Status ?? DBNull.Value;
            DataBase.RunNonQuery("sp_UpdateUser", parameters);

        }

        #endregion Update()

        #region Delete()
        protected override void Delete()
        {
            Delete(UserId);
        }
        protected override void Delete(int UserId)
        {
            SqlParameter[] parameters = 	{		new SqlParameter("@UserId", System.Data.SqlDbType.Int)	};
            parameters[0].Value = (object)UserId ?? DBNull.Value;
            try
            {
                OpenDatabase();
                DataBase.RunNonQuery("sp_DeleteUser", parameters);
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
            SqlParameter[] parameters = { new SqlParameter("UserName",System.Data.SqlDbType.VarChar,45), new SqlParameter("Email",System.Data.SqlDbType.VarChar,45), new SqlParameter("UserTypeId",System.Data.SqlDbType.Int), new SqlParameter("Status",System.Data.SqlDbType.Int),};

            parameters[1].Value = (object)UserName ?? DBNull.Value;
            parameters[2].Value = (object)Email ?? DBNull.Value;
            parameters[3].Value = (object)UserTypeId ?? DBNull.Value;
            parameters[4].Value = (object)Status ?? DBNull.Value;

            try
            {
                OpenDatabase();
                object returnObj = DataBase.QuerySingleValue("sp_AddUser", parameters);
                int newUserId;
                if (Int32.TryParse(returnObj.ToString(), out newUserId))
                {

                    return newUserId;

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

            ValidationRuleList.AddRule((new RuleMethods()).IsRequired, new RuleCriteria("UserName", "UserName is required."));
            ValidationRuleList.AddRule((new RuleMethods()).IsRequired, new RuleCriteria("Email", "Email is required."));
            ValidationRuleList.AddRule((new RuleMethods()).IsRequired, new RuleCriteria("UserTypeId", "UserTypeId is required."));
            ValidationRuleList.AddRule((new RuleMethods()).IsRequired, new RuleCriteria("Status", "Status is required."));
        }

        public override void Map(DataRow dataRow)
        {
            if (dataRow != null)
            {

                if (!dataRow.IsNull("UserId")) UserId = Convert.ToInt32(dataRow["UserId"]);
                if (!dataRow.IsNull("UserName")) UserName = Convert.ToString(dataRow["UserName"]);
                if (!dataRow.IsNull("Email")) Email = Convert.ToString(dataRow["Email"]);
                if (!dataRow.IsNull("UserTypeId")) UserTypeId = Convert.ToInt32(dataRow["UserTypeId"]);
                if (!dataRow.IsNull("Status")) Status = Convert.ToInt32(dataRow["Status"]);
            }

        }

        public void SetPropertySet()
        {

            if (UserId > 0) PropertySet("UserId");

            if (!string.IsNullOrEmpty(UserName)) PropertySet("UserName");

            if (!string.IsNullOrEmpty(Email)) PropertySet("Email");

            if (UserTypeId > 0) PropertySet("UserTypeId");

            if (Status > 0) PropertySet("Status");
        }

        #endregion Methods
    }
}
