using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;

namespace SA47_Team12_StationeryStore.BizLogic
{
    public class MailBizLogic
    {
        public static void sendMail(String from, String to, String subject, String body)
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.To.Add(to);
            mailMessage.From = new MailAddress(from);
            mailMessage.Subject = subject;
            mailMessage.Body = body;
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.Credentials = new System.Net.NetworkCredential()
            {
                UserName = "teststationery47@gmail.com",
                Password = "rFEq8D6UnwV9"
            };
            smtpClient.EnableSsl = true;
            smtpClient.Send(mailMessage);
        }

        public static String DeptHeadEmail(int DeptId)
        {
            using (StationeryStoreEntities1 context1 = new StationeryStoreEntities1())
            {
                StationeryStoreEntities context = new StationeryStoreEntities();
                AspNetRole role = context.AspNetRoles.Where(c => c.Name == "Dept Head").FirstOrDefault();
                string roleid = role.Id;
                List<String> DHlists = context1.AspNetUserRoles.Where(c => c.RoleId == roleid).Select<AspNetUserRole, String>(y => y.UserId).ToList();
                Employee emp = context.Employee.Where(x => x.DepartmentID == DeptId && DHlists.Contains(x.Id)).FirstOrDefault();
                string email = emp.Email;

                return email;
            }
        }

        public static List<String> ClerkEmail()
        {
            using (StationeryStoreEntities1 context1 = new StationeryStoreEntities1())
            {
                StationeryStoreEntities context = new StationeryStoreEntities();
                AspNetRole role = context.AspNetRoles.Where(c => c.Name == "Store Clerk").FirstOrDefault();
                string roleid = role.Id;
                List<String> list = context1.AspNetUserRoles.Where(c => c.RoleId == roleid).Select<AspNetUserRole, String>(y => y.UserId).ToList();
                List<Employee> emp = context.Employee.Where(x => list.Contains(x.Id)).ToList();

                List<String> email = new List<string>();
                foreach (Employee e in emp)
                {
                    email.Add(e.Email);
                    
                }
                return email;
            }
        }

        public static List<String> SupervisorEmail()
        {
            using (StationeryStoreEntities1 context1 = new StationeryStoreEntities1())
            {
                StationeryStoreEntities context = new StationeryStoreEntities();
                AspNetRole role = context.AspNetRoles.Where(c => c.Name == "Store Supervisor").FirstOrDefault();
                string roleid = role.Id;
                List<String> list = context1.AspNetUserRoles.Where(c => c.RoleId == roleid).Select<AspNetUserRole, String>(y => y.UserId).ToList();
                List<Employee> emp = context.Employee.Where(x => list.Contains(x.Id)).ToList();

                List<String> email = new List<string>();
                foreach (Employee e in emp)
                {
                    email.Add(e.Email);
                }
                return email;
            }
        }

        public static List<String> ManagerEmail()
        {
            using (StationeryStoreEntities1 context1 = new StationeryStoreEntities1())
            {
                StationeryStoreEntities context = new StationeryStoreEntities();
                AspNetRole role = context.AspNetRoles.Where(c => c.Name == "Store Manager").FirstOrDefault();
                string roleid = role.Id;
                List<String> list = context1.AspNetUserRoles.Where(c => c.RoleId == roleid).Select<AspNetUserRole, String>(y => y.UserId).ToList();
                List<Employee> emp = context.Employee.Where(x => list.Contains(x.Id)).ToList();

                List<String> email = new List<string>();
                foreach (Employee e in emp)
                {
                    email.Add(e.Email);
                }
                return email;
            }
        }
    }
}