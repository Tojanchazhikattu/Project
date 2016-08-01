using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace htDAL.DBUtility
{
    [Serializable()]
    public class ValidationException : Exception, ISerializable
    {
        public ValidationException()
        {
            // Add implementation.
        }
        public ValidationException(string message)
            : base(message)
        {

        }
        public ValidationException(string message, Exception inner)
            : base(message, inner)
        {
            // Add implementation.
        }

        // This constructor is needed for serialization.
        protected ValidationException(SerializationInfo info, StreamingContext context)
        {
            // Add implementation.
        }
    }
}
