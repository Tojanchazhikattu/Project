using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace htBLL
{
    public class BookSmartDeviceService : ServiceBase<DeviceType>
    {
        public IList<DeviceType> GetAllDeviceType()
        {
            IList<DeviceType> lstDeviceTypes = DeviceType.FetchAll();
            return lstDeviceTypes;
        }

        public IList<HandsetModel> GetHandSetModelForDeviceType(int? deviceTypeId)
        {
            IList<HandsetModel> lstHandSetModels = HandsetModel.FetchAll();
            var lstHandSetModelsForDeviceType = lstHandSetModels.Where(s => s.DeviceTypeId == deviceTypeId).ToList();
            return lstHandSetModelsForDeviceType;
        }

        public IList<Colour> GetColours()
        {
            IList<Colour> lstColours = Colour.FetchAll();
            return lstColours;
        }

        public IList<Network> GetNetworks()
        {
            IList<Network> lstNetworks = Network.FetchAll();
            return lstNetworks;
        }

        public void SaveSmartDeviceRequest(ServiceDetails serviceDetails,
                    CustomerInformation customerInformation,
                    DeviceDetails deviceDetails,
                    EquipmentInformation equipmentInformation, 
                    int repairTypeId,
                    List<int> lstSelectedRepairs
            )
        {
            try
            {

                int customerInformationId = customerInformation.SaveAndReturnID();
                int serviceDetailID = serviceDetails.SaveAndReturnID();
                ServiceRepairMapping mapping;
                foreach (int SelectedRepair in lstSelectedRepairs)
                {
                    mapping = new ServiceRepairMapping();
                    mapping.ServiceDetailsId = serviceDetailID;
                    mapping.RepairTypeId = SelectedRepair;
                    mapping.MarkAsDirty();
                    mapping.Save();
                }
                ServiceRequest serviceRequest = new ServiceRequest();
                serviceRequest.DeviceTypeID = deviceDetails.DeviceTypeID;
                serviceRequest.HandsetModelID = deviceDetails.HandsetModelID;
                serviceRequest.DeviceColourID = deviceDetails.DeviceColourID;
                serviceRequest.IMEINo = deviceDetails.IMEINo;
                serviceRequest.PassCode = deviceDetails.PassCode;
                serviceRequest.NetworkID = deviceDetails.NetworkID;
                serviceRequest.Handset = equipmentInformation.Handset;
                serviceRequest.Simcard = equipmentInformation.Simcard;
                serviceRequest.MemoryCard = equipmentInformation.MemoryCard;
                serviceRequest.Battery = equipmentInformation.Battery;
                serviceRequest.BackCover = equipmentInformation.BackCover;
                serviceRequest.OtherAccessories = equipmentInformation.OtherAccessories;
                serviceRequest.SearchChannelId = customerInformation.SearchChannelId;
                serviceRequest.RepairTypeId = repairTypeId;
                serviceRequest.CustomerInformationId = customerInformationId;
                serviceRequest.SetPropertySet("CustomerInformationId");
                serviceRequest.ServiceDetailsId = serviceDetailID;
                serviceRequest.SetPropertySet("ServiceDetailsId");
                serviceRequest.ServiceRequestTypeId = 1; // smart device
                serviceRequest.CreatedDate = serviceRequest.LastUpdatedDate= DateTime.Now;
                serviceRequest.RequestStatusID = (int)RequestStatus.New;


                //serviceRequest.MarkAsDirty();
                serviceRequest.Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}
