using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using htDAL.DBUtility;
using System.IO;

namespace htBLL
{
     [Serializable()]
     public abstract class BusinessBase<T> where T : BusinessBase<T>, new()
    {
        #region Contructors

        public BusinessBase()
        {
            AddBusinessRules();
            MarkAsNew();
        }

        #endregion

        #region Fields

        protected bool _isNew = false;
        protected bool _isDirty = false;
        protected bool _isDeleted = false;
        protected int _id;
        protected ValidationRules _validationRules;
        protected bool _isDatabaseOwner = true;
        [NonSerialized()]
        protected Db _dataBase = null;

        #endregion

        #region Properties

        /// <summary>
        /// Indicates if the current instance established the database connection.
        /// </summary>
        /// 
        [XmlIgnore]
        public bool IsDatabaseOwner
        {
            get
            {
                return _isDatabaseOwner;
            }
            set
            {
                _isDatabaseOwner = value;
            }
        }

        /// <summary>
        /// Callers of this instances' methods may set this property if they 
        /// want this instance to take part in a transaction.
        /// </summary>
        /// 
        [XmlIgnore]
        public Db DataBase
        {
            get
            {
                return _dataBase;
            }
            set
            {
                _dataBase = value;
                IsDatabaseOwner = false;
            }
        }

        [XmlIgnore]
        public ValidationRules ValidationRuleList
        {
            get
            {
                if (_validationRules == null)
                {
                    _validationRules = new ValidationRules(this);
                }
                return _validationRules;
            }

        }

        [XmlIgnore]
        public List<Rule> BrokenRuleList
        {
            get
            {
                return ValidationRuleList.BrokenRules;
            }
        }

        [XmlIgnore]
        public List<string> MandatoryList
        {
            get
            {
                return ValidationRuleList.Mandatory;
            }
        }


        /// <summary>
        /// Indicates that the object is new and does not exist in the database.
        /// </summary>
        /// 
        [XmlIgnore]
        public bool IsNew
        {
            get { return _isNew; }
        }

        /// <summary>
        /// Indicates that this object instance has been marked for deletion.
        /// </summary>
        /// 
        [XmlIgnore]
        public bool IsDeleted
        {
            get { return _isDeleted; }
        }

        /// <summary>
        /// Indicates if the objects data has been modified and is different from the data in the
        /// database.
        /// </summary>  
        /// 
        [XmlIgnore]
        public bool IsDirty
        {
            get { return _isDirty; }
        }

        [XmlIgnore]
        public bool IsValid
        {
            get
            {
                if (BrokenRuleList.Count == 0)
                    return true;
                else
                    return false;
            }
        }

        [XmlIgnore]
        public bool IsSavable
        {
            get
            {
                if (IsDirty && IsValid)
                    return true;
                else
                    return false;
            }
        }


        public int ID
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }


        #endregion

        #region Methods
        public virtual string ToXml()
        {
            MemoryStream ms = new MemoryStream();

            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add(string.Empty, string.Empty);

            XmlSerializer xs = new XmlSerializer(typeof(T));
            xs.Serialize(ms, this, ns);

            return Encoding.ASCII.GetString(ms.ToArray()).Replace("&#", "&amp;#");

        }

        /// <summary>
        /// Deserializes an XML string to an object. 
        /// </summary>
        /// <param name="xml">An XML payload representing a previously serialized object. 
        /// The XML must represent the type on which this method is called.</param>
        /// <returns>An instance representing the deserialized XML payload</returns>
        public static T FromXml(string xml)
        {
            MemoryStream ms = new MemoryStream(Encoding.ASCII.GetBytes(xml));
            XmlSerializer xs = new XmlSerializer(typeof(T));
            T obj = (T)xs.Deserialize(ms);

            return obj;

        }

        /// <summary>
        /// Removes this instances' reference to the underlying Database object
        /// if this instance is the owner of the Database instance.
        /// </summary>
        protected void CloseDatabase()
        {
            if (_dataBase != null)
            {
                if (this.IsDatabaseOwner)
                {
                    _dataBase.Dispose();
                    _dataBase = null;
                }


            }

        }

        /// <summary>
        /// Can be called by objects who own a transaction. It ensures
        /// that this instances' reference to the underlying database object is
        /// removed. If this instance is not the owner then the underlying
        /// instance is not disposed.
        /// </summary>
        /// <param name="caller">The object calling this method.</param>
        public void CloseDatabase(object caller)
        {
            CloseDatabase();
            if (!this.Equals(caller))
            {
                _dataBase = null;
            }
        }

        /// <summary>
        /// Instantiates the private member _database. If the instance
        /// already exists but the connection is closed, then the connection
        /// is reopened.
        /// </summary>
        protected void OpenDatabase()
        {
            if (_dataBase == null)
            {
                _dataBase = new Db(htDAL.DBUtility.ConfigManager.htConnectionstring);
            }
            else
            {
                if (_dataBase.State == ConnectionState.Closed)
                    _dataBase.Connect(htDAL.DBUtility.ConfigManager.htConnectionstring);
            }

        }


        /// <summary>
        /// Retrieves a record from the database matching the ID and
        /// maps the record to the object instance.
        /// </summary>
        /// <param name="ID">The key of the business entity.</param>
        /// <returns></returns>
        public static T Fetch(int ID)
        {
            T obj = new T();
            if (ID > 0)
            {

                DataRow dr = obj.Get(ID);
                if (dr != null)
                {
                    obj.Map(dr);
                    obj.MarkAsOld();
                    return obj;
                }
                else
                {
                    return null;
                }

            }

            return obj;
        }

        public static IList<T> FetchAll()
        {
            T obj = new T();

            IList<T> objLst = new List<T>();
            DataTable dt = obj.GetAll();
            if (dt!=null && dt.Rows.Count>0)
            {
                foreach(DataRow dr in dt.Rows)
                {
                    obj = new T();
                    obj.Map(dr);
                    obj.MarkAsOld();
                    objLst.Add(obj);
                }

            }
            else
            {
                   return null;
            }

            return objLst;
        }


        public static T Fetch(int ID, Db database)
        {
            T obj = new T();
            obj.DataBase = database;
            if (ID > 0)
            {

                DataRow dr = obj.Get(ID);
                if (dr != null)
                {
                    obj.Map(dr);
                    obj.MarkAsOld();
                    return obj;
                }
                else
                {
                    return null;
                }

            }

            return obj;
        }

        /// <summary>
        /// Deferred deletion. Will occur when save is called.
        /// </summary>
        public void Remove()
        {
            MarkAsDeleted();
        }

        /// <summary>
        /// Immediate deletion.
        /// </summary>
        public static void Remove(int ID)
        {
            T obj = new T();
            obj.Delete(ID);
        }

        public virtual int SaveAndReturnID()
        {
            //if (IsValid)
            //{
                int returnValue = 0;
                try
                {

                    if (_isNew)
                    {
                       returnValue= Add();
                        MarkAsOld();
                        _isDirty = false;
                    }
                    else if (_isDeleted)
                    {
                        Delete();
                    }
                    else if (IsDirty)
                    {
                        Update();
                        _isDirty = false;
                        returnValue = 1;
                    }
                    return returnValue;
                }
                catch
                {
                    // Destroy the instance of the Database clase if required.
                    CloseDatabase();
                    throw;
                }
            //}
            //else
            //{
            //    throw new htDAL.DBUtility.ValidationException(BrokenRuleList[0].Criteria.Message);
            //}

        }

        /// <summary>
        /// Saves this instance of the object to the database.
        /// </summary>
        public virtual int Save()
        {
            if (IsValid)
            {
                try
                {
                    int returnValue = 0;

                    if (_isNew && IsSavable)
                    {
                        returnValue=Add();
                        MarkAsOld();
                        _isDirty = false;
                    }
                    else if (_isDeleted)
                    {
                        Delete();
                        returnValue = 1;
                    }
                    else if (IsSavable)
                    {
                        Update();
                        _isDirty = false;
                        returnValue = 1;
                    }
                    return returnValue;

                }
                catch
                {
                    // Destroy the instance of the Database clase if required.
                    CloseDatabase();
                    throw;
                }
            }
            else
            {
                throw new htDAL.DBUtility.ValidationException(BrokenRuleList[0].Criteria.Message);
            }

        }

        /// <summary>
        /// Adds obj to the database. This method should be used by the InsertMethod
        /// property of a ObjectDatasource control only.
        /// </summary>
        /// <param name="obj">Instance of class to be written to the database.</param>
        public void AddObject(T obj)
        {
            obj.Save();
        }

        /// <summary>
        /// Updates existing record in the database. This method should be used by the UpdateMethod
        /// property of a ObjectDatasource control only.
        /// </summary>
        /// <param name="obj">Instance of class to be written to the database.</param>
        public void UpdateObject(T oldObj, T newObj)
        {
            T instanceToUpdate = Fetch(oldObj.ID);
            instanceToUpdate.BrokenRuleList.Clear();

            PropertyInfo[] properties = typeof(T).GetProperties();

            for (int i = 0; i < properties.Length; i++)
            {
                object oldProperty = properties[i].GetValue(instanceToUpdate, null);
                object newProperty = properties[i].GetValue(newObj, null);

                if (oldProperty != null || newProperty != null)
                {
                    if (properties[i].Name != "IsNew" &&
                        properties[i].Name != "IsValid" &&
                        properties[i].Name != "IsSavable" &&
                        properties[i].Name != "IsDeleted" &&
                        properties[i].Name != "IsDirty")
                    {
                        if (oldProperty == null)
                            oldProperty = -1;
                        if (!oldProperty.Equals(newProperty) &&
                            !(oldProperty is System.Collections.ICollection))
                        {
                            if (properties[i].CanWrite)
                            {
                                instanceToUpdate.MarkAsDirty();
                                properties[i].SetValue(instanceToUpdate, newProperty, null);
                            }
                        }
                    }
                }
            }

            instanceToUpdate.Save();
        }


        /// <summary>
        /// Deletes existing record from the database. This method should be used by the DeleteMethod
        /// property of an ObjectDatasource control only.
        /// </summary>
        /// <param name="obj">Instance of class to be written to the database.</param>
        public void RemoveObject(T obj)
        {
            obj.MarkAsOld();
            obj.MarkAsDeleted();
            obj.BrokenRuleList.Clear();
            obj.Save();
        }

        /// <summary>
        /// Marks this instance as new.
        /// </summary>
        protected void MarkAsNew()
        {
            _isNew = true;
        }

        /// <summary>
        /// Marks this instance as not new.
        /// </summary>
        protected internal void MarkAsOld()
        {
            _isNew = false;
            MarkAsClean();
        }

        public void  MarkforUpdate()
        {
            _isNew = false;
            _isDirty = true;
        }
        /// <summary>
        /// Marks this instance for deletion.
        /// </summary>
        protected void MarkAsDeleted()
        {
            _isDeleted = true;
        }

        /// <summary>
        /// Cancels this instances deferred deletion.
        /// </summary>
        public void CancelDelete()
        {
            _isDeleted = false;
        }

        /// <summary>
        /// Marks this instance as dirty.
        /// </summary>
        public void MarkAsDirty()
        {
            _isDirty = true;
        }

        /// <summary>
        /// Marks this instance as not dirty. Should not be called directly.
        /// </summary>
        protected void MarkAsClean()
        {
            _isDirty = false;
        }


        protected void PropertySet(string propertyName)
        {
            if (_validationRules != null)
            {
                _validationRules.Check(propertyName);
            }

            MarkAsDirty();
        }

        protected virtual void AddBusinessRules()
        {

        }

        #endregion

        #region Data Access

        /// <summary>
        /// Developers deriving from BusinessBase must override
        /// this method.
        /// </summary>
        /// <param name="dataRow"></param>
        public virtual void Map(DataRow dataRow) { }

        /// <summary>
        /// Inserts a record in the database for this instance.
        /// Developers deriving from BusinessBase must override this method.
        /// </summary>
        protected virtual int Add() { return 0; }



        /// <summary>
        /// Updates the record in the database that corresponds to this instance.
        /// Developers deriving from BusinessBase must override this method.
        /// </summary>
        protected virtual void Update() { }

        /// <summary>
        /// Deletes the record in the database that corresponds to this instance.
        /// Developers deriving from BusinessBase must override this method.
        /// </summary>
        protected virtual void Delete() { }

        protected virtual void Delete(int ID) { }

        /// <summary>
        /// Retrieves the record in the database that corresponds to this instance.
        /// Developers deriving from BusinessBase must override this method.
        /// </summary>
        protected virtual DataRow Get(int ID)
        {
            return new DataTable().NewRow();
        }

        protected virtual DataTable GetAll()
        {
            return new DataTable();
        }
        #endregion 
    }
}
