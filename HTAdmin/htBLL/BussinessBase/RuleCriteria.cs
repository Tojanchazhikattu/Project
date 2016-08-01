using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace htBLL
{
    [Serializable()]
    public class RuleCriteria
    {
        public RuleCriteria(string propertyName, string message)
        {
            _propertyName = propertyName;
            _message = message;
        }

        protected string _propertyName;
        protected string _description;
        protected string _message;

        public string PropertyName
        {
            get { return _propertyName; }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public string Message
        {
            get { return _message; }
        }
    }
}
