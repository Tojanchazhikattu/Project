using htBLL.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace htBLL
{
     [Serializable()]
    public abstract class ServiceBase<T> where T : BusinessBase<T>, new()
    {
         public ServiceBase()
         {
         }
         public IList<T> SortObjectList(IList<T> ObjectList, string propName, string sortOrder)
         {
             IList<T> lstSortedObjects = new List<T>();

             if (sortOrder.ToLower().Equals("asc"))
                 lstSortedObjects = ObjectList.OrderBy(x => TypeHelper.GetPropertyValue(x, propName)).ToList();
             else
                 lstSortedObjects = ObjectList.OrderByDescending(x => TypeHelper.GetPropertyValue(x, propName)).ToList();

             return lstSortedObjects;
         }
    }
}
