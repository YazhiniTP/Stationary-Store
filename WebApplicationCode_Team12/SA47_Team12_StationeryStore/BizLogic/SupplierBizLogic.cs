using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SA47_Team12_StationeryStore.BizLogic
{
    public class SupplierBizLogic
    {
        public static void AddSupplier(string Name, string Address, string Phone)
        {
            using (StationeryStoreEntities context = new StationeryStoreEntities())
            {
                Supplier s = new Supplier
                {
                   
                    Name = Name,
                    Address = Address,
                    Phone = Phone,
                    P1name = Name,
                    P2name = Name,
                    P3name = Name
                };
                context.Supplier.Add(s);
                context.SaveChanges();
            }
        }
        public static void UpdateSupplier(int SupplierID, string Address, string Phone)
        {
            using (StationeryStoreEntities context = new StationeryStoreEntities())
            {
                Supplier s = context.Supplier.Where(p => p.SupplierID == SupplierID).First<Supplier>();
                //s.Name = Name;
                s.Address = Address;
                s.Phone = Phone;
                //s.P1name = Name;
                //s.P2name = Name;
                //s.P3name = Name;
                context.SaveChanges();
            }
        }
        public static void DeleteSupplier(int SupplierID)
        {
            using (StationeryStoreEntities context = new StationeryStoreEntities())
            {
                Supplier s = context.Supplier.Where(p => p.SupplierID == SupplierID).First<Supplier>();
                context.Supplier.Remove(s);
                context.SaveChanges();
            }
        }

        public static List<Supplier> ListSupplier()
        {
            using (StationeryStoreEntities context = new StationeryStoreEntities())
            {
                return context.Supplier.ToList<Supplier>();
            }
        }

        public static BriefItem FindItemSupplierByID(string ItemID)
        {
            using (StationeryStoreEntities context = new StationeryStoreEntities())
            {
                BriefItem i = context.CatalogueInventory.Where(p => p.ItemID == ItemID).Select(y => new BriefItem()
                {
                    ItemID = y.ItemID,
                    Item_Description = y.Item_Description,
                    P1Supplier = y.Supplier.P1name,
                    P2Supplier = y.Supplier1.P2name,
                    P3Supplier = y.Supplier2.P3name,

                }
                ).ToList<BriefItem>().First();

                return i;
            }
        }

        public static void UpdateItemSupplier(string ItemID, int p1, int p2, int p3)
        {
            using (StationeryStoreEntities context = new StationeryStoreEntities())
            {
                CatalogueInventory s = context.CatalogueInventory.
                    Where(p => p.ItemID == ItemID).ToList<CatalogueInventory>().First();
                s.Priority1 = p1;
                s.Priority2 = p2;
                s.Priority3 = p3;
                context.SaveChanges();
            }
        }

        public static List<String> ListAllItem()
        {
            using (StationeryStoreEntities context = new StationeryStoreEntities())
            {
                return context.CatalogueInventory.Select<CatalogueInventory, String>
                                       (c => c.ItemID).ToList<String>();
            }
        }

        public static void AddItem(string ItemID, int CatID, string Item_Description,
            string UnitOfMeasure, double UnitCost, int Priority1, int Priority2, int Priority3)
        {
            using (StationeryStoreEntities context = new StationeryStoreEntities())
            {
                CatalogueInventory i = new CatalogueInventory()
                {
                    ItemID = ItemID,
                    CatID = CatID,
                    Item_Description = Item_Description,
                    UnitOfMeasure = UnitOfMeasure,
                    UnitCost = Convert.ToDecimal(UnitCost),
                    Priority1 = Priority1,
                    Priority2 = Priority2,
                    Priority3 = Priority3,
                    ActualQty = 0
                };
                context.CatalogueInventory.Add(i);
                context.SaveChanges();
            }
        }

        public static BriefCollection FindCollectionByDepID(int DepID)
        {
            using (StationeryStoreEntities context = new StationeryStoreEntities())
            {
                BriefCollection c = context.UserRepCollection.Where(p => p.DepartmentID == DepID).Select(y => new BriefCollection()
                {
                    CollectionPoint = y.Collection.Location,
                    CollectionPointID = y.CollectionID,
                    EmployeeID = y.EmployeeID,
                    EmployeeName = y.Employee.Name,
                }
                ).ToList<BriefCollection>().First();
                return c;
            }
        }

        public static List<BriefDepEmp> FindEmpByDepID(int DepID)
        {
            using (StationeryStoreEntities context = new StationeryStoreEntities())
            {
                List<BriefDepEmp> c = context.Employee.Where(p => p.DepartmentID == DepID).Select(y => new BriefDepEmp()
                {
                    EmployeeID = y.EmployeeID,
                    Name = y.Name,
                }
                ).ToList<BriefDepEmp>();
                return c;
            }
        }
        public static List<BriefDepEmp> FindEmpByDepID2(int DepID, int EmpID)
        {
            using (StationeryStoreEntities context = new StationeryStoreEntities())
            {
                List<BriefDepEmp> c = context.Employee.Where(p => p.DepartmentID == DepID & p.EmployeeID != EmpID).Select(y => new BriefDepEmp()
                {
                    EmployeeID = y.EmployeeID,
                    Name = y.Name,
                }
                ).ToList<BriefDepEmp>();
                return c;
            }
        }

        public static void UpdateNewURinEmptable(int EmpID)
        {
            using (StationeryStoreEntities context = new StationeryStoreEntities())
            {
                Employee s = context.Employee.Where(p => p.EmployeeID == EmpID).First<Employee>();
                s.isUserRep = 1;
                context.SaveChanges();
            }
        }
        public static void UpdateOldURinEmptable(int EmpID)
        {
            using (StationeryStoreEntities context = new StationeryStoreEntities())
            {
                Employee s = context.Employee.Where(p => p.EmployeeID == EmpID).First<Employee>();
                s.isUserRep = 0;
                context.SaveChanges();
            }
        }
       
        static StationeryStoreEntities _entities = new StationeryStoreEntities();

        public static void CreateDelegation(int EmpID, DateTime start, DateTime end, int DepID)
        {
            using (StationeryStoreEntities context = new StationeryStoreEntities())
            {
                Delegation s = new Delegation
                {
                    EmployeeID = EmpID,
                    StartDate = start,
                    EndDate = end,
                    DepartmentID = DepID,                    
                };
                context.Delegation.Add(s);
                context.SaveChanges();

                Employee e = context.Employee.Where(x => x.EmployeeID == EmpID).FirstOrDefault();
                String from = "teststationery47@gmail.com";
                String to = e.Email;
                String subject = "[Auto Notification] Delegation Status";
                String body = String.Format("You are Delegated from " + start + " to " + end +
                        "\n\nNote: This is an auto-generated email.  Please do not reply to this email." +
                        "\n\nThis email is confidential and may be privileged.If you are not the intended recipient, " +
                        "please delete it and notify us immediately; you should not copy or use it for any purpose, " +
                        "nor disclose its contents to any other person.\n\nThank you.");
                MailBizLogic.sendMail(from, to, subject, body);
            }
        }

        public static bool IsDuplicate(int DepID, DateTime start, DateTime end)
        {
            DateTime today = DateTime.Now.Date;
            using (StationeryStoreEntities context = new StationeryStoreEntities())
            {
                Delegation s = context.Delegation.Where(p => p.DepartmentID == DepID && ((p.StartDate <= end && p.EndDate >= end) || (p.StartDate <= start && p.EndDate >= start))).FirstOrDefault<Delegation>();
                if (s != null) return true;
                else return false;
            }
        }

        public static void DeleteDelegation(int DelegationID)
        {
            Delegation s;
            using (StationeryStoreEntities context = new StationeryStoreEntities())
            {
                s = context.Delegation.Where(p => p.DelegationID == DelegationID).First<Delegation>();
                Employee e = context.Employee.Where(x => x.EmployeeID == s.EmployeeID).FirstOrDefault<Employee>();
                context.Delegation.Remove(s);
                e.isDelegated = 0;
                context.SaveChanges();


                String from = "teststationery47@gmail.com";
                String to = e.Email;
                String subject = "[Auto Notification] Delegation Status";
                String body = String.Format("Your delegation {0} has been cancelled." +
                        "\n\nNote: This is an auto-generated email.  Please do not reply to this email." +
                        "\n\nThis email is confidential and may be privileged.If you are not the intended recipient, " +
                        "please delete it and notify us immediately; you should not copy or use it for any purpose, " +
                        "nor disclose its contents to any other person.\n\nThank you.", DelegationID);
                MailBizLogic.sendMail(from, to, subject, body);
            }
        }

        public static void UpdateDelegation(int DelegationID, DateTime? end)
        {
            using (StationeryStoreEntities context = new StationeryStoreEntities())
            {
                Delegation s = context.Delegation.Where(p => p.DelegationID == DelegationID).First<Delegation>();
                s.EndDate = end;

                context.SaveChanges();
            }
        }

        public static DateTime? findStartDate(int DelegationID)
        {
            DateTime? start;
            using (StationeryStoreEntities context = new StationeryStoreEntities())
            {
                Delegation s = context.Delegation.Where(p => p.DelegationID == DelegationID).First<Delegation>();
                start = s.StartDate;
                return start;
            }
        }

        public static List<BriefDelegation> FindDelegation(int DepID)
        {
            using (StationeryStoreEntities context = new StationeryStoreEntities())
            {
                List<BriefDelegation> i = context.Delegation.Where(p => p.DepartmentID == DepID & p.EndDate > DateTime.Now).AsEnumerable()
                    .Select(y => new BriefDelegation()
                    {
                        DelegationID = y.DelegationID,
                        EmpID = y.EmployeeID,
                        StartDate = String.Format("{0:ddd, MMM d, yyyy}", y.StartDate),
                        EndDate = String.Format("{0:ddd, MMM d, yyyy}", y.EndDate),
                        Name = y.Employee.Name,

                    }).ToList<BriefDelegation>();

                return i;
            }
        }
        public static List<BriefDelegation> FindDelegation2(int DepID)
        {
            using (StationeryStoreEntities context = new StationeryStoreEntities())
            {
                List<BriefDelegation> i = context.Delegation.Where(p => p.DepartmentID == DepID).AsEnumerable()
                    .Select(y => new BriefDelegation()
                    {
                        DelegationID = y.DelegationID,
                        EmpID = y.EmployeeID,
                        StartDate = String.Format("{0:ddd, MMM d, yyyy}", y.StartDate),
                        EndDate = String.Format("{0:ddd, MMM d, yyyy}", y.EndDate),
                        Name = y.Employee.Name,

                    }).ToList<BriefDelegation>();

                return i;
            }
        }

        public static UserRepCollection UpdateURcollectiontableCollection(int DepID, int CollectionID)
        // changed void return type to UserRepCollection return type
        {
            UserRepCollection s = _entities.UserRepCollection
                                    .Where(p => p.DepartmentID == DepID)
                                    .First<UserRepCollection>();

            s.DepartmentID = DepID;
            s.CollectionID = CollectionID;
            // s.EmployeeID = EmpID;
            _entities.SaveChanges();
            return s;
        }

        public static UserRepCollection UpdateURcollectiontableUR(int DepID, int EmpID)
        // changed void return type to UserRepCollection return type
        {
            UserRepCollection s = _entities.UserRepCollection
                                    .Where(p => p.DepartmentID == DepID)
                                    .First<UserRepCollection>();
            s.DepartmentID = DepID;
            s.EmployeeID = EmpID;
            _entities.SaveChanges();
            
            return s;
        }

    }
}