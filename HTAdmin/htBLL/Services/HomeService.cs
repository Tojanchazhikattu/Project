using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace htBLL
{
    public class HomeService
    {
        public IList<AssignedJobs> GetAssignedJobs()
        {
            IList<AssignedJobs> lstAssignedJobs = AssignedJobs.FetchAll();
            return lstAssignedJobs;
        }

        public IList<ServiceRequestsComposite> GetRequestsByStatus(RequestStatus status)
        {
            var NewRequests = ServiceRequestsComposite.FetchAll().Where(s => s.RequestStatusID == (int)status).ToList();
            return NewRequests;
            //return ServiceRequestsComposite.FetchAll();
        }

        public IList<RequestStatusentity> GetRequestStatus()
        {
            var RequestStatus = RequestStatusentity.FetchAll();
            return RequestStatus;
        }
        public IList<User> GetEngineers()
        {
            var lstEngineers = User.FetchAll().Where(s => s.UserTypeId == 2 && s.Status == 1).ToList();
            return lstEngineers;
        }

        public void UpdateServiceRequest(ServiceRequestsComposite serviceRequestsComposite)
        {
            serviceRequestsComposite.MarkforUpdate();
            serviceRequestsComposite.Save();
        }

    }
}
