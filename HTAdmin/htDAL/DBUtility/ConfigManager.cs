using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Xml;

namespace htDAL.DBUtility
{
    public class ConfigManager
    {
        //private DatabaseInfo _database;

        public static string htConnectionstring
        {
            get
            {
                //htSolutionConnection
                string cs = System.Configuration.ConfigurationManager.ConnectionStrings["htSolutionConnection"].ToString();
                return cs;
                //ConfigManager cm = (ConfigManager)ConfigurationManager.GetSection("faasSettings");
                //return cm._database;
            }
        }
        //internal ConfigManager(XmlNode data)
        //{
        //    try
        //    {
        //        _database = new DatabaseInfo(data);
        //    }
        //    catch (Exception ex)
        //    {
        //       // Log.ErrorFormat("Exception in ConfigManager {0}", ex.Message);
        //        throw ex;
        //    }
        //}
    }
}
