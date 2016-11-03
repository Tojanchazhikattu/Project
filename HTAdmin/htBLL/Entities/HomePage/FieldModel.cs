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
    public class FieldModel
    {

        public string PropertyName { get; set; }

        public string PropertyValue { get; set; }
        public FieldModel(string propName,string propValue)
        {
            PropertyName = propName;
            PropertyValue = propValue;
        }
    }
}
