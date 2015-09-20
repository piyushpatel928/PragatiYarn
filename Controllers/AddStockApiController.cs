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
    public class AddStockApiController : ApiController
    {
        public PragatiEntities db = new PragatiEntities();
        public IEnumerable<string> Get()
        {
            return new string[]
			{
				"value1",
				"value2"
			};
        }
        public List<PaymentHistoryViewModel> Get(int CustomerId, int UserId)
        {
            List<PaymentHistoryViewModel> list = new List<PaymentHistoryViewModel>();
            IOrderedQueryable<CustomerPaidHistory> orderedQueryable =
                from a in this.db.CustomerPaidHistories
                where a.CustomerId == (int?)CustomerId && a.UserId == (int?)UserId
                select a into p
                orderby p.PaidId descending
                select p;
            List<PaymentHistoryViewModel> result;
            if (orderedQueryable != null)
            {
                foreach (CustomerPaidHistory current in orderedQueryable)
                {
                    list.Add(new PaymentHistoryViewModel
                    {
                        PaidId = current.PaidId,
                        Amount = current.Amount.Value,
                        DateTime = current.DateTime.Value.ToLongDateString()
                    });
                }
                result = list;
            }
            else
            {
                result = null;
            }
            return result;
        }
        public int Post(AddStockViewModel asvm)
        {
            int result;
            if (asvm.MaterialTypeId == 1 || asvm.MaterialTypeId == 4 || asvm.MaterialTypeId == 2)
            {
                Stock stock = (
                    from a in this.db.Stocks
                    where a.MaterialId == (int?)asvm.MaterialId && a.SubMaterialTypeId == (int?)asvm.SubMaterialTypeId && a.UserId == (int?)asvm.UserId
                    select a).FirstOrDefault<Stock>();
                Stock stock2;
                if (stock != null)
                {
                    stock2 = (
                        from a in this.db.Stocks
                        where a.MaterialId == (int?)asvm.MaterialId && a.SubMaterialTypeId == (int?)asvm.SubMaterialTypeId && a.UserId == (int?)asvm.UserId
                        select a).FirstOrDefault<Stock>();
                    stock2.Nos_Of_Piece = stock.Nos_Of_Piece + asvm.Piece;
                    stock2.LastAddedStock = asvm.Piece.ToString();
                    stock2.UpdatedDatetime = new DateTime?(DateTime.Now);
                    stock2.UserId =new int?(asvm.UserId);
                    try
                    {
                        this.db.SaveChanges();
                        result = 1;
                        return result;
                    }
                    catch
                    {
                        result = 0;
                        return result;
                    }
                }
                stock2 = new Stock();
                stock2.MaterialId = new int?(asvm.MaterialId);
                stock2.MaterialTypeId = new int?(asvm.MaterialTypeId);
                stock2.SubMaterialTypeId = new int?(asvm.SubMaterialTypeId);
                stock2.Nos_Of_Piece = new int?(asvm.Piece);
                stock2.LastAddedStock = asvm.Piece.ToString();
                stock2.UpdatedDatetime = new DateTime?(DateTime.Now);
                stock2.UserId = new int?(asvm.UserId);
                this.db.Stocks.Add(stock2);
                try
                {
                    this.db.SaveChanges();
                    result = 1;
                    return result;
                }
                catch
                {
                    result = 0;
                    return result;
                }
            }
            if (asvm.MaterialTypeId == 5 || asvm.MaterialTypeId == 6 || asvm.MaterialTypeId == 7)
            {
                Stock stock = (
                    from a in this.db.Stocks
                    where a.MaterialId == (int?)asvm.MaterialId &&  a.UserId == (int?)asvm.UserId
                    select a).FirstOrDefault<Stock>();
                Stock stock2;
                if (stock != null)
                {
                    stock2 = (
                        from a in this.db.Stocks
                        where a.MaterialId == (int?)asvm.MaterialId && a.UserId == (int?)asvm.UserId
                        select a).FirstOrDefault<Stock>();
                    Stock arg_533_0 = stock2;
                    float? tot_Weight_in_Gram_ = stock.Tot_Weight_in_Gram_;
                    float num = asvm.Weight;
                    arg_533_0.Tot_Weight_in_Gram_ = (tot_Weight_in_Gram_.HasValue ? new float?(tot_Weight_in_Gram_.GetValueOrDefault() + num) : null);
                    stock2.LastAddedStock = (asvm.Weight).ToString();
                    stock2.UpdatedDatetime = new DateTime?(DateTime.Now);
                    stock2.UserId=new int?(asvm.UserId);
                    try
                    {
                        this.db.SaveChanges();
                        result = 1;
                        return result;
                    }
                    catch
                    {
                        result = 0;
                        return result;
                    }
                }
                stock2 = new Stock();
                stock2.MaterialId = new int?(asvm.MaterialId);
                stock2.MaterialTypeId = new int?(asvm.MaterialTypeId);
                stock2.Tot_Weight_in_Gram_ = new float?(asvm.Weight );
                stock2.LastAddedStock = (asvm.Weight).ToString();
                stock2.UpdatedDatetime = new DateTime?(DateTime.Now);
                stock2.UserId = new int?(asvm.UserId);

                this.db.Stocks.Add(stock2);
                try
                {
                    this.db.SaveChanges();
                    result = 1;
                    return result;
                }
                catch
                {
                    result = 0;
                    return result;
                }
            }
            if (asvm.MaterialTypeId == 3)
            {
                Stock stock = (
                    from a in this.db.Stocks
                    where a.MaterialId == (int?)asvm.MaterialId && a.UserId == (int?)asvm.UserId
                    select a).FirstOrDefault<Stock>();
                Stock stock2;
                if (stock != null)
                {
                    stock2 = (
                        from a in this.db.Stocks
                        where a.MaterialId == (int?)asvm.MaterialId && a.UserId == (int?)asvm.UserId
                        select a).FirstOrDefault<Stock>();
                    stock2.Nos_Of_Piece = stock.Nos_Of_Piece + asvm.Piece;
                    stock2.LastAddedStock = asvm.Piece.ToString();
                    stock2.Sub_KasabId = new int?(asvm.SubMaterialTypeId);
                    stock2.UpdatedDatetime = new DateTime?(DateTime.Now);
                    stock2.UserId=new int?(asvm.UserId);
                    try
                    {
                        this.db.SaveChanges();
                        result = 1;
                        return result;
                    }
                    catch
                    {
                        result = 0;
                        return result;
                    }
                }
                stock2 = new Stock();
                stock2.MaterialId = new int?(asvm.MaterialId);
                stock2.MaterialTypeId = new int?(asvm.MaterialTypeId);
                stock2.Nos_Of_Piece = new int?(asvm.Piece);
                stock2.LastAddedStock = asvm.Piece.ToString();
                stock2.UpdatedDatetime = new DateTime?(DateTime.Now);
                stock2.UserId = new int?(asvm.UserId);
                stock2.Sub_KasabId = new int?(asvm.SubMaterialTypeId);
                this.db.Stocks.Add(stock2);
                try
                {
                    this.db.SaveChanges();
                    result = 1;
                    return result;
                }
                catch
                {
                    result = 0;
                    return result;
                }
            }
            result = 2;
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
