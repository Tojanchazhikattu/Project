using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace htBLL
{
    [Serializable()]
    public class Rule
    {
        #region Contructors

        public Rule(object target, RuleMethod method, RuleCriteria criteria)
        {
            _criteria = criteria;
            _method = method;
            _target = target;
        }

        #endregion

        #region Fields

        private RuleMethod _method;
        private RuleCriteria _criteria;
        private object _target;

        #endregion

        #region Properties

        public RuleCriteria Criteria
        {
            get { return _criteria; }
        }

        #endregion

        #region Methods

        public bool Check()
        {
            return _method.Invoke(_target, _criteria);
        }

        #endregion
    }
}
