using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplicationStationaryApi.Models.ApiModel;

namespace WebApplicationStationaryApi.Repo
{
    public class AspNetUserRepo
    {
        public List<AspNetUser> ListUsers()
        {

            StationeryStoreEntities entities = new StationeryStoreEntities();
            List<AspNetUser> adjds = new List<AspNetUser>();
            try
            {
                adjds = entities.AspNetUsers.ToList();

            }
            catch (NullReferenceException)
            {
                return null;
            }
            catch (Exception e)
            {
                return null;
            }

            return adjds;

        }


        public AspNetUser FindUserByEmail(String Email)
        {
            StationeryStoreEntities entities = new StationeryStoreEntities();
            List<AspNetUser> users = new List<AspNetUser>();
            try
            {
                users = entities.AspNetUsers.Where(a => a.Email == Email).ToList();

            }
            catch (NullReferenceException)
            {
                return null;
            }
            catch (Exception e)
            {
                return null;
            }

            if (users.Count > 0)
                return users[0];
            else return null;


        }



        public Employee ListEmployeeByUserID(string UserID)
        {
            StationeryStoreEntities entities = new StationeryStoreEntities();
            List<Employee> employees = new List<Employee>();
            try
            {
                employees = entities.Employees.Where(a => a.Id == UserID).ToList();

            }
            catch (NullReferenceException)
            {
                return null;
            }
            catch (Exception e)
            {
                return null;
            }

            if (employees.Count > 0)
                return employees[0];
            else return null;

        }

        public Employee ListEmployeeByUserID1(String UserID)
        {
            StationeryStoreEntities entities = new StationeryStoreEntities();
            List<Employee> employees = new List<Employee>();
            try
            {
                int id = Convert.ToInt32(UserID);
                employees = entities.Employees.Where(a => a.EmployeeID == id).ToList();

            }
            catch (NullReferenceException)
            {
                return null;
            }
            catch (Exception e)
            {
                return null;
            }

            if (employees.Count > 0)
                return employees[0];
            else return null;

        }

        public EmployeeModel ListEmployeeByUserID2(string UserID)
        {
            StationeryStoreEntities entities = new StationeryStoreEntities();
            List<EmployeeModel> employees = new List<EmployeeModel>();
            try
            {
                employees = entities.Employees.Where(a => a.Id == UserID)
                    .Select<Employee, EmployeeModel>
                (c => new EmployeeModel()
                {
                    EmployeeID = c.EmployeeID,
                    Name = c.Name,
                    Address = c.Address,
                    Email = c.Email,
                    Phone = c.Phone,
                    DepartmentID = c.DepartmentID,
                    RoleID= 3001

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

            if (employees.Count > 0)
                return employees[0];
            else return null;

        }


        public EmployeeModel ListEmployeeByUserID3(string UserID)
        {
            StationeryStoreEntities entities = new StationeryStoreEntities();
            List<EmployeeModel> employees = new List<EmployeeModel>();
            try
            {
                int id = Convert.ToInt32(UserID);
                employees = entities.Employees.Where(a => a.EmployeeID == id)
                    .Select<Employee, EmployeeModel>
                (c => new EmployeeModel()
                {
                    EmployeeID = c.EmployeeID,
                    Name = c.Name,
                    Address = c.Address,
                    Email = c.Email,
                    Phone = c.Phone,
                    DepartmentID = c.DepartmentID,
                    RoleID = 3001
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

            if (employees.Count > 0)
                return employees[0];
            else return null;

        }


    }
}