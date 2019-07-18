using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplicationStationaryApi.Models.ApiModel;

namespace WebApplicationStationaryApi.Repo
{
    public class EmployeeRepo
    {
        public List<EmployeeModel> ListEmployeeByDepartmentID(int DepID)
        {
            
            StationeryStoreEntities entities = new StationeryStoreEntities();
            List<EmployeeModel> employees = new List<EmployeeModel>();
            try
            {
                employees = entities.Employees.Where(a => a.DepartmentID == DepID)
                    .Select<Employee, EmployeeModel>
                (c => new EmployeeModel()
                {
                    EmployeeID = c.EmployeeID,
                    Name = c.Name,
                    Address = c.Address,
                    Email = c.Email,
                    Phone = c.Phone,
                    DepartmentID = c.DepartmentID
                }).ToList<EmployeeModel>();

            }
            catch (NullReferenceException)
            {
                return null;
            }
            catch (Exception e)
            {
                return null;
            }

            
            return employees;
            

        }


        public List<EmployeeModel> ListEmployeeByDepartmentID2(int DepID)
        {

            StationeryStoreEntities entities = new StationeryStoreEntities();
            List<EmployeeModel> employees = new List<EmployeeModel>();
            try
            {
                employees = entities.Employees.Where(a => a.DepartmentID == DepID )
                    .Select<Employee, EmployeeModel>
                (c => new EmployeeModel()
                {
                    EmployeeID = c.EmployeeID,
                    Name = c.Name,
                    Address = c.Address,
                    Email = c.Email,
                    Phone = c.Phone,
                    DepartmentID = c.DepartmentID
                }).ToList<EmployeeModel>();

            }
            catch (NullReferenceException)
            {
                return null;
            }
            catch (Exception e)
            {
                return null;
            }


            return employees;


        }

    }
}