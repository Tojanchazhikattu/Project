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
    //service requests for various tabs in homepage
   public class ServiceRequestsComposite : BusinessBase<ServiceRequestsComposite>
    {
        public int ServiceRequestId { get; set; }
        public int DeviceTypeID { get; set; }
        public int HandsetModelID { get; set; }
        public int DeviceColourID { get; set; }
        public string IMEINo { get; set; }
        public string PassCode { get; set; }
        public int NetworkID { get; set; }
        public bool Handset { get; set; }
        public bool Simcard { get; set; }
        public bool MemoryCard { get; set; }
        public bool Battery { get; set; }
        public bool BackCover { get; set; }
        public string OtherAccessories { get; set; }
        public int SearchChannelId { get; set; }
        public int RepairTypeId { get; set; }
        public int CustomerInformationId { get; set; }
        public int ServiceDetailsId { get; set; }
        public int ServiceRequestTypeId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public int RequestStatusID { get; set; }
        //ServiceDetails
        public DateTime CollectionDateTime { get; set; }
        public string CollectionDateTimeDisplay { get; set; }
        public int EngineerUserId { get; set; }
        public string MessageToEngineer { get; set; }
        public string OtherRepair { get; set; }
        public decimal OtherRepairCost { get; set; }
        public decimal UnlockingCost { get; set; }
        public bool AdancePayment { get; set; }
        public decimal AdancePaymentAmount { get; set; }
        //CustomerInformation
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ContactNo { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string PostCode { get; set; }
        //Engineer Name
        public string UserName { get; set; }
        

        protected override DataTable GetAll()
        {
            try
            {
                OpenDatabase();
                DataTable dataTable = DataBase.QueryDataTable("sp_GetServiceRequestsComposite", null);
                return dataTable;
            }
            finally
            {
                CloseDatabase();
            }
        }

        protected override void Update()
        {
            ServiceRequest serviceRequest = ServiceRequest.Fetch(ServiceRequestId);
            

            serviceRequest.LastUpdatedDate = DateTime.Now;
            serviceRequest.RequestStatusID = RequestStatusID;
            serviceRequest.MarkforUpdate();
            //serviceRequest.ValidationRuleList.CheckAll();
            serviceRequest.Save();

            if (serviceRequest.ServiceDetailsId.HasValue && serviceRequest.ServiceDetailsId!= null)
            {
                ServiceDetails serviceDetails = ServiceDetails.Fetch((int)serviceRequest.ServiceDetailsId);
                serviceDetails.CollectionDateTime = CollectionDateTime;
                serviceDetails.EngineerUserId = EngineerUserId;
                serviceDetails.MarkforUpdate();
                serviceDetails.Save();
            }

        }

        public override void Map(DataRow dataRow)
        {
            if (dataRow != null)
            {

                if (!dataRow.IsNull("ServiceRequestId")) ServiceRequestId = Convert.ToInt32(dataRow["ServiceRequestId"]);
                if (!dataRow.IsNull("DeviceTypeID")) DeviceTypeID = Convert.ToInt32(dataRow["DeviceTypeID"]);
                if (!dataRow.IsNull("HandsetModelID")) HandsetModelID = Convert.ToInt32(dataRow["HandsetModelID"]);
                if (!dataRow.IsNull("DeviceColourID")) DeviceColourID = Convert.ToInt32(dataRow["DeviceColourID"]);

                if (!dataRow.IsNull("IMEINo")) IMEINo = Convert.ToString(dataRow["IMEINo"]);
                if (!dataRow.IsNull("PassCode")) PassCode = Convert.ToString(dataRow["PassCode"]);
                if (!dataRow.IsNull("NetworkID")) NetworkID = Convert.ToInt32(dataRow["NetworkID"]);
                if (!dataRow.IsNull("Handset")) Handset = Convert.ToBoolean(dataRow["Handset"]);

                if (!dataRow.IsNull("Simcard")) Simcard = Convert.ToBoolean(dataRow["Simcard"]);
                if (!dataRow.IsNull("MemoryCard")) MemoryCard = Convert.ToBoolean(dataRow["MemoryCard"]);
                if (!dataRow.IsNull("Battery")) Battery = Convert.ToBoolean(dataRow["Battery"]);
                if (!dataRow.IsNull("BackCover")) BackCover = Convert.ToBoolean(dataRow["BackCover"]);

                if (!dataRow.IsNull("OtherAccessories")) OtherAccessories = Convert.ToString(dataRow["OtherAccessories"]);
                if (!dataRow.IsNull("SearchChannelId")) SearchChannelId = Convert.ToInt32(dataRow["SearchChannelId"]);
                if (!dataRow.IsNull("RepairTypeId")) RepairTypeId = Convert.ToInt32(dataRow["RepairTypeId"]);
                if (!dataRow.IsNull("CustomerInformationId")) CustomerInformationId = Convert.ToInt32(dataRow["CustomerInformationId"]);

                if (!dataRow.IsNull("ServiceDetailsId")) ServiceDetailsId = Convert.ToInt32(dataRow["ServiceDetailsId"]);
                if (!dataRow.IsNull("ServiceRequestTypeId")) ServiceRequestTypeId = Convert.ToInt32(dataRow["ServiceRequestTypeId"]);
                if (!dataRow.IsNull("CreatedDate")) CreatedDate = Convert.ToDateTime(dataRow["CreatedDate"]);
                if (!dataRow.IsNull("LastUpdatedDate")) LastUpdatedDate = Convert.ToDateTime(dataRow["LastUpdatedDate"]);

                if (!dataRow.IsNull("RequestStatusID")) RequestStatusID = Convert.ToInt32(dataRow["RequestStatusID"]);
                if (!dataRow.IsNull("CollectionDateTime")) 
                {
                    CollectionDateTime = Convert.ToDateTime(dataRow["CollectionDateTime"]);
                    CollectionDateTimeDisplay = String.Format("{0:u}", CollectionDateTime);
                }
                if (!dataRow.IsNull("EngineerUserId")) EngineerUserId = Convert.ToInt32(dataRow["EngineerUserId"]);
                if (!dataRow.IsNull("MessageToEngineer")) MessageToEngineer = Convert.ToString(dataRow["MessageToEngineer"]);

                if (!dataRow.IsNull("OtherRepair")) OtherRepair = Convert.ToString(dataRow["OtherRepair"]);
                if (!dataRow.IsNull("OtherRepairCost")) OtherRepairCost = Convert.ToDecimal(dataRow["OtherRepairCost"]);
                if (!dataRow.IsNull("UnlockingCost")) UnlockingCost = Convert.ToDecimal(dataRow["UnlockingCost"]);
                if (!dataRow.IsNull("AdancePayment")) AdancePayment = Convert.ToBoolean(dataRow["AdancePayment"]);

                if (!dataRow.IsNull("AdancePaymentAmount")) AdancePaymentAmount = Convert.ToDecimal(dataRow["AdancePaymentAmount"]);
                if (!dataRow.IsNull("FirstName")) FirstName = Convert.ToString(dataRow["FirstName"]);
                if (!dataRow.IsNull("LastName")) LastName = Convert.ToString(dataRow["LastName"]);
                if (!dataRow.IsNull("ContactNo")) ContactNo = Convert.ToString(dataRow["ContactNo"]);

                if (!dataRow.IsNull("Email")) Email = Convert.ToString(dataRow["Email"]);
                if (!dataRow.IsNull("Address")) Address = Convert.ToString(dataRow["Address"]);
                if (!dataRow.IsNull("PostCode")) PostCode = Convert.ToString(dataRow["PostCode"]);

                if (!dataRow.IsNull("UserName")) UserName = Convert.ToString(dataRow["UserName"]);
                

            }

        }

    }
}
