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
    public class SearchChannel : BusinessBase<SearchChannel>
    {
        #region Instance Properties

        public int SearchChannelId { get; set; }

        public String name { get; set; }

        #endregion Instance Properties

        #region Data access logic

        #region Get()
        protected override DataRow Get(int SearchChannelId)
        {
            SqlParameter[] parameters = 		{			new SqlParameter("@SearchChannelId", System.Data.SqlDbType.Int)		};
            parameters[0].Value = SearchChannelId;
            try
            {
                OpenDatabase();
                DataTable dataTable = DataBase.QueryDataTable("sp_GetSearchChannel", parameters);
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
                DataTable dataTable = DataBase.QueryDataTable("sp_GetSearchChannelList", null);
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
            SqlParameter[] parameters =             {             new SqlParameter("SearchChannelId",System.Data.SqlDbType.Int),             new SqlParameter("name",System.Data.SqlDbType.VarChar,100),            };

            parameters[0].Value = (object)SearchChannelId ?? DBNull.Value;
            parameters[1].Value = (object)name ?? DBNull.Value;
            DataBase.RunNonQuery("sp_UpdateSearchChannel", parameters);

        }

        #endregion Update()

        #region Delete()
        protected override void Delete()
        {
            Delete(SearchChannelId);
        }
        protected override void Delete(int SearchChannelId)
        {
            SqlParameter[] parameters = 	        {		        new SqlParameter("@SearchChannelId", System.Data.SqlDbType.Int)	        };
            parameters[0].Value = (object)SearchChannelId ?? DBNull.Value;
            try
            {
                OpenDatabase();
                DataBase.RunNonQuery("sp_DeleteSearchChannel", parameters);
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

            parameters[1].Value = (object)name ?? DBNull.Value;

            try
            {
                OpenDatabase();
                object returnObj = DataBase.QuerySingleValue("sp_AddSearchChannel", parameters);
                int newSearchChannelId;
                if (Int32.TryParse(returnObj.ToString(), out newSearchChannelId))
                {

                    return newSearchChannelId;

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

                if (!dataRow.IsNull("SearchChannelId")) SearchChannelId = Convert.ToInt32(dataRow["SearchChannelId"]);
                if (!dataRow.IsNull("name")) name = Convert.ToString(dataRow["name"]);
            }

        }

        public void SetPropertySet()
        {

            if (SearchChannelId > 0) PropertySet("SearchChannelId");

            if (!string.IsNullOrEmpty(name)) PropertySet("name");
        }

        #endregion Methods
    }
}
