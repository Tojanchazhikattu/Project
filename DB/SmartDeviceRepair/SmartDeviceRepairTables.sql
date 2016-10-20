table4
ServiceRequest
ServiceRequestId int identity primary key
DeviceTypeID int FOREIGN KEY REFERENCES DeviceType(DeviceTypeId) ,
HandsetModelID int FOREIGN KEY REFERENCES HandsetModel(HandsetModelId) ,
DeviceColourID int FOREIGN KEY REFERENCES Colour(ColourId) ,
IMEINo int,
PassCode varchar(25),
NetworkID int FOREIGN KEY REFERENCES Network(NetworkId) ,
Handset bool,
Simcard bool,
MemoryCard bool,
Battery bool,
BackCover bool,
OtherAccessories varchar(100),
SearchChannelId int FOREIGN KEY REFERENCES SearchChannel(SearchChannelId) , 
RepairTypeId int FOREIGN KEY REFERENCES RepairType(RepairTypeId) , 
CustomerInformationId int FOREIGN KEY REFERENCES CustomerInformation(CustomerInformationId) , 
ServiceDetailsId int FOREIGN KEY REFERENCES ServiceDetails(ServiceDetailsId) , 

table 1
CustomerInformation
CustomerInformationId int identity primary key
FirstName varchar (100)
LastName varchar (100)
ContactNo varchar (25)
Email varchar(100)
Address varchar (255)
PostCode string (10)

table 2
ServiceDetails
ServiceDetailsId int identity primary key,
CollectionDateTime datetime
EngineerUserId int  FOREIGN KEY REFERENCES User(UserId) ,  
MessageToEngineer varchar(500)
OtherRepair varchar(500)
OtherRepairCost double

table3
ServiceRepairMapping
ServiceRepairMappingID int identity primary key,
ServiceDetailsId int FOREIGN KEY REFERENCES ServiceDetails(ServiceDetailsId) ,
RepairTypeId int FOREIGN KEY REFERENCES smartdevicerepair(id),  


