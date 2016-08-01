using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace htBLL
{
    public delegate bool RuleMethod(object target, RuleCriteria ruleCriteria);

    [Serializable()]
    public class RuleMethods
    {

        /// <summary>
        /// Requires a value. This is only use on data types that can have nulls or be empty.
        /// An example of such a data type is a string.
        /// </summary>
        /// <param name="target">The business object to evaluate</param>
        /// <param name="ruleCriteria">The information required to evaluate this rule.</param>
        /// <returns>Indicates if the rule passed</returns>
        public bool IsRequired(object target, RuleCriteria ruleCriteria)
        {
            object value = GetPropertyValue(target, ruleCriteria.PropertyName);
            string stringToEvaluate = string.Empty;

            if (value != null)
                stringToEvaluate = value.ToString();

            if (string.IsNullOrEmpty(stringToEvaluate))
            {
                ruleCriteria.Description = ruleCriteria.PropertyName + " is required.";
                return false;
            }
            else
            {
                return true;
            }
        }


        /// <summary>
        /// Requires that a value is numeric when it is specified. If the value is null or empty
        /// the rule will pass.
        /// </summary>
        /// <param name="target">The business object to evaluate</param>
        /// <param name="ruleCriteria">The information required to evaluate this rule.</param>
        /// <returns>Indicates if the rule passed</returns>
        public bool IsNumeric(object target, RuleCriteria ruleCriteria)
        {
            object value = GetPropertyValue(target, ruleCriteria.PropertyName);
            string stringToEvaluate = string.Empty;

            if (value != null)
                stringToEvaluate = value.ToString();


            if (!string.IsNullOrEmpty(stringToEvaluate))
            {
                try
                {
                    int i = Convert.ToInt32(stringToEvaluate);
                    return true;
                }
                catch
                {
                    ruleCriteria.Description = ruleCriteria.PropertyName + " must have a numeric value when provided.";
                    return false;
                }
            }
            else
            {
                return true;
            }
        }


        /// <summary>
        /// Requires that a value is numeric.
        /// </summary>
        /// <param name="target">The business object to evaluate</param>
        /// <param name="ruleCriteria">The information required to evaluate this rule.</param>
        /// <returns>Indicates if the rule passed</returns>
        public bool IsNumericAndRequired(object target, RuleCriteria ruleCriteria)
        {
            try
            {

                int i = Convert.ToInt32(GetPropertyValue(target, ruleCriteria.PropertyName));
                return true;
            }
            catch
            {
                ruleCriteria.Description = ruleCriteria.PropertyName + " must have a numeric value.";
                return false;
            }

        }





      
        private object GetPropertyValue(object target, string propertyName)
        {
            PropertyInfo property = target.GetType().GetProperty(propertyName);
            return property.GetValue(target, null);
        }

    }
}
