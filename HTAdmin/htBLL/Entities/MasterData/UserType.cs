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
    public class UserType : BusinessBase<UserType>
    {
        #region Instance Properties

        public int UserTypeId { get; set; }

        public String Description { get; set; }

        public String Rights { get; set; }

        #endregion Instance Properties

        #region Data access logic

        #region Get()
        protected override DataRow Get(int UserTypeId)
        {
            SqlParameter[] parameters = 		{			new SqlParameter("@UserTypeId", System.Data.SqlDbType.Int)		};
            parameters[0].Value = UserTypeId;
            try
            {
                OpenDatabase();
                DataTable dataTable = DataBase.QueryDataTable("sp_GetUserType", parameters);
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
                DataTable dataTable = DataBase.QueryDataTable("sp_GetUserTypeList", null);
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
            SqlParameter[] parameters = { new SqlParameter("UserTypeId",System.Data.SqlDbType.Int), new SqlParameter("Description",System.Data.SqlDbType.VarChar,45), new SqlParameter("Rights",System.Data.SqlDbType.VarChar,255),};

            parameters[0].Value = (object)UserTypeId ?? DBNull.Value;
            parameters[1].Value = (object)Description ?? DBNull.Value;
            parameters[2].Value = (object)Rights ?? DBNull.Value;
            DataBase.RunNonQuery("sp_UpdateUserType", parameters);

        }

        #endregion Update()

        #region Delete()
        protected override void Delete()
        {
            Delete(UserTypeId);
        }
        protected override void Delete(int UserTypeId)
        {
            SqlParameter[] parameters = 	{		new SqlParameter("@UserTypeId", System.Data.SqlDbType.Int)	};
            parameters[0].Value = (object)UserTypeId ?? DBNull.Value;
            try
            {
                OpenDatabase();
                DataBase.RunNonQuery("sp_DeleteUserType", parameters);
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
            SqlParameter[] parameters = { new SqlParameter("Description",System.Data.SqlDbType.VarChar,45), new SqlParameter("Rights",System.Data.SqlDbType.VarChar,255),};

            parameters[1].Value = (object)Description ?? DBNull.Value;
            parameters[2].Value = (object)Rights ?? DBNull.Value;

            try
            {
                OpenDatabase();
                object returnObj = DataBase.QuerySingleValue("sp_AddUserType", parameters);
                int newUserTypeId;
                if (Int32.TryParse(returnObj.ToString(), out newUserTypeId))
                {

                    return newUserTypeId;

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

            ValidationRuleList.AddRule((new RuleMethods()).IsRequired, new RuleCriteria("Description", "Description is required."));
        }

        public override void Map(DataRow dataRow)
        {
            if (dataRow != null)
            {

                if (!dataRow.IsNull("UserTypeId")) UserTypeId = Convert.ToInt32(dataRow["UserTypeId"]);
                if (!dataRow.IsNull("Description")) Description = Convert.ToString(dataRow["Description"]);
            }

        }

        public void SetPropertySet()
        {

            if (UserTypeId > 0) PropertySet("UserTypeId");

            if (!string.IsNullOrEmpty(Description)) PropertySet("Description");
        }

        #endregion Methods
    }
}
