using PragatiYarn.Models.DB;
using PragatiYarn.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PragatiYarn.Controllers
{
    public class getAllCustomerApiController : ApiController
    {
        public PragatiEntities db = new PragatiEntities();
        public List<AddCustomerViewModel> Get()
        {
            List<AddCustomerViewModel> list = new List<AddCustomerViewModel>();
            List<Customer> list2 = this.db.Customers.ToList<Customer>();
            foreach (Customer current in list2)
            {
                list.Add(new AddCustomerViewModel
                {
                    customerId = current.CustomerId,
                    CustomerName = current.CustomerName + " " + current.CustomerSurname,
                    FirmName = current.CustomerFirmname
                });
            }
            return list;
        }
        public List<AddCustomerViewModel> Get(int UserId)
        {
            List<AddCustomerViewModel> list = new List<AddCustomerViewModel>();
            List<Customer> list2 = db.Customers.Where(a => a.UserId == UserId).ToList();
            foreach (Customer current in list2)
            {
                list.Add(new AddCustomerViewModel
                {
                    customerId = current.CustomerId,
                    CustomerName = current.CustomerName + " " + current.CustomerSurname,
                    FirmName = current.CustomerFirmname
                });
            }
            return list;
        }
        public void Post([FromBody] string value)
        {
        }
        public void Put(int id, [FromBody] string value)
        {
        }
        public void Delete(int id)
        {
        }
    }
}
