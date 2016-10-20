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
    public class Network : BusinessBase<Network>
    {
        #region Instance Properties

        public int NetworkId { get; set; }

        public String name { get; set; }

        #endregion Instance Properties

        #region Data access logic

        #region Get()
        protected override DataRow Get(int NetworkId)
        {
            SqlParameter[] parameters = 		{			new SqlParameter("@NetworkId", System.Data.SqlDbType.Int)		};
            parameters[0].Value = NetworkId;
            try
            {
                OpenDatabase();
                DataTable dataTable = DataBase.QueryDataTable("sp_GetNetwork", parameters);
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
                DataTable dataTable = DataBase.QueryDataTable("sp_GetNetworkList", null);
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
            SqlParameter[] parameters =             {             new SqlParameter("NetworkId",System.Data.SqlDbType.Int),             new SqlParameter("name",System.Data.SqlDbType.VarChar,100),            };

            parameters[0].Value = (object)NetworkId ?? DBNull.Value;
            parameters[1].Value = (object)name ?? DBNull.Value;
            DataBase.RunNonQuery("sp_UpdateNetwork", parameters);

        }

        #endregion Update()

        #region Delete()
        protected override void Delete()
        {
            Delete(NetworkId);
        }
        protected override void Delete(int NetworkId)
        {
            SqlParameter[] parameters = 	            {		            new SqlParameter("@NetworkId", System.Data.SqlDbType.Int)	            };
                        parameters[0].Value = (object)NetworkId ?? DBNull.Value;
            try
            {
                OpenDatabase();
                DataBase.RunNonQuery("sp_DeleteNetwork", parameters);
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
            SqlParameter[] parameters =             {             new SqlParameter("name",System.Data.SqlDbType.VarChar,100),            };

            parameters[0].Value = (object)name ?? DBNull.Value;

            try
            {
                OpenDatabase();
                object returnObj = DataBase.QuerySingleValue("sp_AddNetwork", parameters);
                int newNetworkId;
                if (Int32.TryParse(returnObj.ToString(), out newNetworkId))
                {

                    return newNetworkId;

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

            ValidationRuleList.AddRule((new RuleMethods()).IsRequired, new RuleCriteria("name", "name is required."));
        }

        public override void Map(DataRow dataRow)
        {
            if (dataRow != null)
            {

                if (!dataRow.IsNull("NetworkId")) NetworkId = Convert.ToInt32(dataRow["NetworkId"]);
                if (!dataRow.IsNull("name")) name = Convert.ToString(dataRow["name"]);
            }

        }

        public void SetPropertySet()
        {

            if (NetworkId > 0) PropertySet("NetworkId");

            if (!string.IsNullOrEmpty(name)) PropertySet("name");
        }

        #endregion Methods
    }
}
