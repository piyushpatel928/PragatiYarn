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
    public class GetCustomerPaymentController : ApiController
    {
        private PragatiEntities db = new PragatiEntities();
        public IEnumerable<string> Get()
        {
            return new string[]
			{
				"value1",
				"value2"
			};
        }
        public CustomerCreditPaymentViewModel Get(int Userid, int CustId)
        {
            CustomerCreditPaymentViewModel customerCreditPaymentViewModel = new CustomerCreditPaymentViewModel();
            Customer customer = (
                from a in this.db.Customers
                where a.CustomerId == CustId
                select a).FirstOrDefault<Customer>();
            CustomerPayableAmount customerPayableAmount = (
                from a in this.db.CustomerPayableAmounts
                where a.CustomerId == (int?)CustId && a.UserId == (int?)Userid
                select a).FirstOrDefault<CustomerPayableAmount>();
            if (customerPayableAmount != null)
            {
                customerCreditPaymentViewModel.customerId = customer.CustomerId;
                customerCreditPaymentViewModel.CustomerName = customer.CustomerSurname + " " + customer.CustomerName;
                customerCreditPaymentViewModel.FirmName = customer.CustomerFirmname;
                customerCreditPaymentViewModel.Amount = customerPayableAmount.Amount.Value;
                return customerCreditPaymentViewModel;
            }
            else
            {
                return null;
            }
        }
        public int Post(CustomerCreditPaymentViewModel ccpvm)
        {
            CustomerPayableAmount customerPayableAmount = (
                from a in this.db.CustomerPayableAmounts
                where a.CustomerId == (int?)ccpvm.customerId && a.UserId == (int?)ccpvm.UserId
                select a).FirstOrDefault<CustomerPayableAmount>();
            int result;
            if (customerPayableAmount != null)
            {
                CustomerPayableAmount arg_144_0 = customerPayableAmount;
                float? amount = customerPayableAmount.Amount;
                float amount2 = ccpvm.Amount;
                arg_144_0.Amount = (amount.HasValue ? new float?(amount.GetValueOrDefault() - amount2) : null);
                try
                {
                    this.db.SaveChanges();
                }
                catch
                {
                    result = 0;
                    return result;
                }
                CustomerPaidHistory customerPaidHistory = new CustomerPaidHistory();
                customerPaidHistory.Amount = new float?(ccpvm.Amount);
                customerPaidHistory.CustomerId = new int?(ccpvm.customerId);
                customerPaidHistory.UserId = new int?(ccpvm.UserId);
                customerPaidHistory.DateTime = new DateTime?(DateTime.Now);
                this.db.CustomerPaidHistories.Add(customerPaidHistory);
                try
                {
                    this.db.SaveChanges();
                }
                catch
                {
                    result = 0;
                    return result;
                }
                result = 1;
            }
            else
            {
                result = 0;
            }
            return result;
        }
        public void Put(int id, [FromBody] string value)
        {
        }
        public void Delete(int id)
        {
        }
    }
}
