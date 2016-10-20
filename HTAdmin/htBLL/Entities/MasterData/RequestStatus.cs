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
    public class RequestStatusentity : BusinessBase<RequestStatusentity>
    {
        #region Instance Properties

        public int RequestStatusID { get; set; }

        public String Description { get; set; }

        #endregion Instance Properties

        #region Data access logic

        #region Get()
        protected override DataRow Get(int RequestStatusID)
        {
            SqlParameter[] parameters = 		{			new SqlParameter("@RequestStatusID", System.Data.SqlDbType.Int)		};
            parameters[0].Value = RequestStatusID;
            try
            {
                OpenDatabase();
                DataTable dataTable = DataBase.QueryDataTable("sp_GetRequestStatus", parameters);
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
                DataTable dataTable = DataBase.QueryDataTable("sp_GetRequestStatusList", null);
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
            SqlParameter[] parameters =             {             new SqlParameter("RequestStatusID",System.Data.SqlDbType.Int),             new SqlParameter("Description",System.Data.SqlDbType.VarChar,25),            };

            parameters[0].Value = (object)RequestStatusID ?? DBNull.Value;
            parameters[1].Value = (object)Description ?? DBNull.Value;
            DataBase.RunNonQuery("sp_UpdateRequestStatus", parameters);

        }

        #endregion Update()

        #region Delete()
        protected override void Delete()
        {
            Delete(RequestStatusID);
        }
        protected override void Delete(int RequestStatusID)
        {
            SqlParameter[] parameters = 	{		new SqlParameter("@RequestStatusID", System.Data.SqlDbType.Int)	};
            parameters[0].Value = (object)RequestStatusID ?? DBNull.Value;
            try
            {
                OpenDatabase();
                DataBase.RunNonQuery("sp_DeleteRequestStatus", parameters);
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
            SqlParameter[] parameters = { new SqlParameter("Description",System.Data.SqlDbType.VarChar,25),};

            parameters[0].Value = (object)Description ?? DBNull.Value;

            try
            {
                OpenDatabase();
                object returnObj = DataBase.QuerySingleValue("sp_AddRequestStatus", parameters);
                int newRequestStatusID;
                if (Int32.TryParse(returnObj.ToString(), out newRequestStatusID))
                {

                    return newRequestStatusID;

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

                if (!dataRow.IsNull("RequestStatusID")) RequestStatusID = Convert.ToInt32(dataRow["RequestStatusID"]);
                if (!dataRow.IsNull("Description")) Description = Convert.ToString(dataRow["Description"]);
            }

        }

        public void SetPropertySet()
        {

            if (RequestStatusID > 0) PropertySet("RequestStatusID");
        }

        #endregion Methods
    }

}
