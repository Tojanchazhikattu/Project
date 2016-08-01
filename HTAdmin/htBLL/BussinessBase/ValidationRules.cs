using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace htBLL
{
    [Serializable()]
    public class ValidationRules
    {

        public ValidationRules(object businessObject)
        {
            _target = businessObject;
        }

        private object _target;
        private Dictionary<string, List<Rule>> _rules = new Dictionary<string, List<Rule>>();
        private List<Rule> _brokenRules = new List<Rule>();
        private List<string> _mandatory = new List<string>();

        public List<Rule> BrokenRules
        {
            get { return _brokenRules; }
        }

        public List<string> Mandatory
        {
            get { return _mandatory; }
        }


        public void AddRule(RuleMethod method, RuleCriteria criteria)
        {
            List<Rule> propertyRules = GetRulesByProperty(criteria.PropertyName);

            Rule rule = new Rule(_target, method, criteria);
            propertyRules.Add(rule);
            _brokenRules.Add(rule);

            // Maintain a list mandatory properties.
            if (method.Method.Name == "IsRequired" || method.Method.Name == "IsNumericAndRequired")
            {
                if (!_mandatory.Contains(criteria.PropertyName))
                {
                    _mandatory.Add(criteria.PropertyName);
                }
            }

        }

        public void Check(string propertyName)
        {

            List<Rule> propertyRules = GetRulesByProperty(propertyName);

            foreach (Rule rule in propertyRules)
            {
                if (rule.Check())
                {
                    _brokenRules.Remove(rule);
                }
                else
                {
                    // Do not add the rule if it already exists.
                    if (!_brokenRules.Contains(rule))
                    {
                        _brokenRules.Add(rule);
                    }

                }
            }
        }


        public void CheckAll()
        {

            foreach (KeyValuePair<string, List<Rule>> propertyRules in _rules)
            {
                List<Rule> rules = propertyRules.Value;

                foreach (Rule rule in rules)
                {
                    if (rule.Check())
                    {
                        _brokenRules.Remove(rule);
                    }
                    else
                    {
                        // Do not add the rule if it already exists.
                        if (!_brokenRules.Contains(rule))
                        {
                            _brokenRules.Add(rule);
                        }
                    }
                }

            }
        }

        public List<Rule> GetRulesByProperty(string propertyName)
        {
            if (_rules.ContainsKey(propertyName))
            {
                return _rules[propertyName];
            }
            else
            {
                List<Rule> list = new List<Rule>();
                _rules.Add(propertyName, list);
                return list;
            }
        }

    }
}
