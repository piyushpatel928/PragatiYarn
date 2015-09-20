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
    public class GetDailyInvoiceApiController : ApiController
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
        public List<ChalanViewModel> Get(int CustomerId, int UserId, string Invoicedate)
        {
            List<ChalanViewModel> result;
            if (CustomerId != 0)
            {
                List<ChalanViewModel> list = new List<ChalanViewModel>();
                if (Invoicedate != null)
                {
                    List<Invoice> list2 = (
                        from a in this.db.Invoices
                        where a.CustomerId == (int?)CustomerId && a.UserId == (int?)UserId && a.DateTime == Invoicedate
                        select a).ToList<Invoice>();
                    if (list2 != null)
                    {
                        foreach (Invoice current in list2)
                        {
                            list.Add(new ChalanViewModel
                            {
                                ChalanId = current.InvoiceId,
                                TotalAmount = current.TotalAmount.Value,
                                Date = DateTime.Parse(current.DateTime).ToLongDateString()
                            });
                        }
                        result = list;
                    }
                    else
                    {
                        result = null;
                    }
                }
                else
                {
                    List<Invoice> list2 = (
                        from a in this.db.Invoices
                        where a.CustomerId == (int?)CustomerId && a.UserId == (int?)UserId
                        select a).ToList<Invoice>();
                    if (list2 != null)
                    {
                        foreach (Invoice current in list2)
                        {
                            list.Add(new ChalanViewModel
                            {
                                ChalanId = current.InvoiceId,
                                TotalAmount = current.TotalAmount.Value,
                                Date = DateTime.Parse(current.DateTime).ToLongDateString()
                            });
                        }
                        result = list;
                    }
                    else
                    {
                        result = null;
                    }
                }
            }
            else
            {
                result = null;
            }
            return result;
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
