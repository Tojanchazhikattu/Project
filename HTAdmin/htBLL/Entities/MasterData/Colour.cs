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
    public class Colour : BusinessBase<Colour>
    {
        #region Instance Properties

        public int ColourId { get; set; }

        public String colour { get; set; }

        #endregion Instance Properties

        #region Data access logic

        #region Get()
        protected override DataRow Get(int ColourId)
        {
            SqlParameter[] parameters = 		{			new SqlParameter("@ColourId", System.Data.SqlDbType.Int)		};
            parameters[0].Value = ColourId;
            try
            {
                OpenDatabase();
                DataTable dataTable = DataBase.QueryDataTable("sp_GetColour", parameters);
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
                DataTable dataTable = DataBase.QueryDataTable("sp_GetColourList", null);
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
            SqlParameter[] parameters =             {             new SqlParameter("ColourId",System.Data.SqlDbType.Int),             new SqlParameter("colour",System.Data.SqlDbType.VarChar,100),            };

            parameters[0].Value = (object)ColourId ?? DBNull.Value;
            parameters[1].Value = (object)colour ?? DBNull.Value;
            DataBase.RunNonQuery("sp_UpdateColour", parameters);

        }

        #endregion Update()

        #region Delete()
        protected override void Delete()
        {
            Delete(ColourId);
        }
        protected override void Delete(int ColourId)
        {
            SqlParameter[] parameters = 	        {		        new SqlParameter("@ColourId", System.Data.SqlDbType.Int)	        };
            parameters[0].Value = (object)ColourId ?? DBNull.Value;
            try
            {
                OpenDatabase();
                DataBase.RunNonQuery("sp_DeleteColour", parameters);
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
            SqlParameter[] parameters =             {             new SqlParameter("colour",System.Data.SqlDbType.VarChar,100),            };

            parameters[0].Value = (object)colour ?? DBNull.Value;

            try
            {
                OpenDatabase();
                object returnObj = DataBase.QuerySingleValue("sp_AddColour", parameters);
                int newColourId;
                if (Int32.TryParse(returnObj.ToString(), out newColourId))
                {

                    return newColourId;

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
            ValidationRuleList.AddRule((new RuleMethods()).IsRequired, new RuleCriteria("colour", "Colour is required."));
        }

        public override void Map(DataRow dataRow)
        {
            if (dataRow != null)
            {
                if (!dataRow.IsNull("ColourId")) ColourId = Convert.ToInt32(dataRow["ColourId"]);
                if (!dataRow.IsNull("Colour")) colour = Convert.ToString(dataRow["Colour"]);
            }

        }

        public void SetPropertySet()
        {

            if (ColourId > 0) PropertySet("ColourId");
        }

        #endregion Methods
    }
}
