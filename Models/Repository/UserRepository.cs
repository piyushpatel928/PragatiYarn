using PragatiYarn.Models.Abstract;
using PragatiYarn.Models.DB;
using PragatiYarn.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PragatiYarn.Models.Repository
{
    public class UserRepository : IUserRepository
    {
        private PragatiEntities db = new PragatiEntities();
        public int CheckUserLogin(User usr)
        {
            User user = this.db.Users.FirstOrDefault((User a) => a.Username == usr.Username && a.Password == usr.Password);
            int result;
            if (user != null)
            {
                result = user.UserId;
            }
            else
            {
                result = 0;
            }
            return result;
        }
        public int InsertNewCustomer(Customer cust)
        {
            this.db.Customers.Add(cust);
            int result;
            try
            {
                this.db.SaveChanges();
                result = 1;
            }
            catch
            {
                result = 0;
            }
            return result;
        }
        public int DeleteCustomer(int id)
        {
            List<MonthlyInvoice> list = (
                from a in this.db.MonthlyInvoices
                where a.CustomerId == (int?)id
                select a).ToList<MonthlyInvoice>();
            if (list != null)
            {
                foreach (MonthlyInvoice current in list)
                {
                    this.db.MonthlyInvoices.Remove(current);
                }
            }
            List<Sale> list2 = (
                from a in this.db.Sales
                where a.CustomerId == (int?)id
                select a).ToList<Sale>();
            if (list2 != null)
            {
                foreach (Sale current2 in list2)
                {
                    this.db.Sales.Remove(current2);
                }
            }
            List<Invoice> list3 = (
                from a in this.db.Invoices
                where a.CustomerId == (int?)id
                select a).ToList<Invoice>();
            if (list3 != null)
            {
                foreach (Invoice current3 in list3)
                {
                    this.db.Invoices.Remove(current3);
                }
            }
            Customer customer = (
                from a in this.db.Customers
                where a.CustomerId == id
                select a).FirstOrDefault<Customer>();
            this.db.Customers.Remove(customer);
            int result;
            try
            {
                this.db.SaveChanges();
                result = 1;
            }
            catch
            {
                result = 0;
            }
            return result;
        }
        public int UpdateCustomer(AddCustomerViewModel acvm, int CustId)
        {
            Customer customer = (
                from a in this.db.Customers
                where a.CustomerId == CustId
                select a).FirstOrDefault<Customer>();
            int result;
            try
            {
                customer.CustomerFirmname = acvm.FirmName;
                customer.CustomerName = acvm.CustomerName;
                customer.CustomerSurname = acvm.CustomerSurName;
                customer.MobileNo = new decimal?(decimal.Parse(acvm.MobileNumber));
                customer.Address = acvm.Address;
                if (acvm.CustomerImagePath != null && acvm.CustomerImagePath != "")
                {
                    customer.CustomerImageName = acvm.CustomerImageName;
                    customer.CustomerImagePath = acvm.CustomerImagePath;
                }
                this.db.SaveChanges();
                result = 1;
            }
            catch
            {
                result = 0;
            }
            return result;
        }
        public List<Customer> GetAllCustomer()
        {
            return this.db.Customers.ToList<Customer>();
        }
        public Customer GetCustomerById(int custid)
        {
            return (
                from a in this.db.Customers
                where a.CustomerId == custid
                select a).FirstOrDefault<Customer>();
        }
        public int insertNewMaterial(AllMaterial allmat)
        {
            this.db.AllMaterials.Add(allmat);
            int result;
            try
            {
                this.db.SaveChanges();
                result = 1;
            }
            catch
            {
                result = 0;
            }
            return result;
        }
        public int InsertNewUser(User usr)
        {
            this.db.Users.Add(usr);
            int result;
            try
            {
                this.db.SaveChanges();
                result = 1;
            }
            catch
            {
                result = 0;
            }
            return result;
        }
        public int IsUsernameAvailable(string username)
        {
            User user = this.db.Users.FirstOrDefault((User a) => a.Username == username);
            int result;
            if (user != null)
            {
                result = 1;
            }
            else
            {
                result = 0;
            }
            return result;
        }
        public CustomerCreditViewModel GetAllCustomersCreditAmtByUserId(int userid)
        {
            var data = db.Customers.Where(a => a.UserId == userid).ToList();
            List<AllCustomerCreditViewModel> lstaccvm = new List<AllCustomerCreditViewModel>();
            CustomerCreditViewModel ccvm = new CustomerCreditViewModel();
            if (data != null)
            {
                foreach (var item in data)
                {
                    AllCustomerCreditViewModel accvm = new AllCustomerCreditViewModel();
                    accvm.customerId = item.CustomerId;
                    accvm.CustomerName = item.CustomerName + " " + item.CustomerSurname;
                    accvm.FirmName = item.CustomerFirmname;
                    accvm.MobileNo = item.MobileNo.ToString();
                    var amt = db.CustomerPayableAmounts.Where(a => a.CustomerId == item.CustomerId && a.UserId == userid).FirstOrDefault();
                    if (amt != null)
                    {
                        accvm.Amount = (float)amt.Amount;
                        ccvm.TotalAmount += (float)amt.Amount;
                        lstaccvm.Add(accvm);
                    }

                }
                ccvm.lstaccvm = lstaccvm;
                var date = DateTime.Parse(DateTime.Now.ToShortDateString()).ToString("dd/MM/yyyy");
                ccvm.DateTime = date;
                //var d = date.Date;
                return ccvm;
            }
            else
            {
                return null;
            }
        }
    }
}