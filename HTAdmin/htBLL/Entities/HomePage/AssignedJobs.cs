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
    public class AssignedJobs : BusinessBase<AssignedJobs>
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int New { get; set; }
        public int AssignedToEngineer { get; set; }
        public int InProcess { get; set; }
        public int Completed { get; set; }

        public AssignedJobs()
        {

        }
        protected override DataTable GetAll()
        {
            try
            {
                OpenDatabase();
                DataTable dataTable = DataBase.QueryDataTable("sp_GetAssignedJobs", null);
                return dataTable;
            }
            finally
            {
                CloseDatabase();
            }
        }
        public override void Map(DataRow dataRow)
        {
            if (dataRow != null)
            {

                if (!dataRow.IsNull("UserId")) UserId = Convert.ToInt32(dataRow["UserId"]);
                if (!dataRow.IsNull("UserName")) UserName = Convert.ToString(dataRow["UserName"]);
                if (!dataRow.IsNull("New")) New = Convert.ToInt32(dataRow["New"]);
                if (!dataRow.IsNull("AssignedToEngineer")) AssignedToEngineer = Convert.ToInt32(dataRow["AssignedToEngineer"]);
                if (!dataRow.IsNull("InProcess")) InProcess = Convert.ToInt32(dataRow["InProcess"]);
                if (!dataRow.IsNull("Completed")) Completed = Convert.ToInt32(dataRow["Completed"]);

            }

        }
    }
}