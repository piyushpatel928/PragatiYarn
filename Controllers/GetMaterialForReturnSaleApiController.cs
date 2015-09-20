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
    public class GetMaterialForReturnSaleApiController : ApiController
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
        public string Get(int id)
        {
            return "value";
        }
        public int Post(AddSalesViewModel asvm)
        {
            int num = -2;
            int result;
            if (asvm.lstsaleitem != null)
            {
                float num2 = 0f;
                Invoice invoice = new Invoice();
                invoice.CustomerId = new int?(asvm.CustomerId);
                invoice.UserId = new int?(asvm.UserId);
                invoice.DateTime = DateTime.Now.ToShortDateString();
                using (List<SalesItemViewModel>.Enumerator enumerator = asvm.lstsaleitem.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        SalesItemViewModel item = enumerator.Current;
                        if (item.MaterialTypeId == 1 || item.MaterialTypeId == 4 || item.MaterialTypeId == 2)
                        {
                            Stock stock = (
                                from a in this.db.Stocks
                                where a.MaterialId == (int?)item.MaterialId && a.UserId == (int?)asvm.UserId && a.SubMaterialTypeId == (int?)item.SubMaterialTypeId
                                select a).FirstOrDefault<Stock>();
                            SubMaterialType subMaterialType = (
                                from a in this.db.SubMaterialTypes
                                where a.MaterialTypeId == (int?)item.MaterialTypeId && a.SubMaterialTypeId == item.SubMaterialTypeId
                                select a).FirstOrDefault<SubMaterialType>();
                            num2 += (float)item.Piece * subMaterialType.Price.Value;
                        }
                        else
                        {
                            if (item.MaterialTypeId == 3)
                            {
                                Stock stock = (
                                    from a in this.db.Stocks
                                    where a.MaterialId == (int?)item.MaterialId && a.UserId == (int?)asvm.UserId && a.Sub_KasabId == (int?)item.SubMaterialTypeId
                                    select a).FirstOrDefault<Stock>();
                                Sub_Kasab sub_Kasab = (
                                    from a in this.db.Sub_Kasab
                                    where a.MaterialTypeId == (int?)item.MaterialTypeId && a.Sub_KasabId == item.SubMaterialTypeId
                                    select a).FirstOrDefault<Sub_Kasab>();
                                num2 += (float)item.Piece * sub_Kasab.Price.Value;
                            }
                            else
                            {
                                if (item.MaterialTypeId == 5 || item.MaterialTypeId == 6 || item.MaterialTypeId == 7)
                                {
                                    Stock stock = (
                                        from a in this.db.Stocks
                                        where a.MaterialId == (int?)item.MaterialId && a.UserId == (int?)asvm.UserId
                                        select a).FirstOrDefault<Stock>();
                                    AllMaterial allMaterial = (
                                        from a in this.db.AllMaterials
                                        where a.MaterialTypeId == (int?)item.MaterialTypeId && a.MaterialId == item.MaterialId
                                        select a).FirstOrDefault<AllMaterial>();
                                    float num3 = item.Weight * 1000f * (float)allMaterial.Price.Value / 100f;
                                    num2 += num3;
                                }
                            }
                        }
                    }
                }
                invoice.TotalAmount = new float?(-num2);
                CustomerPayableAmount customerPayableAmount = (
                    from a in this.db.CustomerPayableAmounts
                    where a.CustomerId == (int?)asvm.CustomerId && a.UserId == (int?)asvm.UserId
                    select a).FirstOrDefault<CustomerPayableAmount>();
                if (customerPayableAmount != null)
                {
                    CustomerPayableAmount arg_93B_0 = customerPayableAmount;
                    float? num4 = customerPayableAmount.Amount;
                    float num5 = num2;
                    arg_93B_0.Amount = (num4.HasValue ? new float?(num4.GetValueOrDefault() - num5) : null);
                    try
                    {
                        this.db.SaveChanges();
                    }
                    catch
                    {
                        result = 0;
                        return result;
                    }
                }
                this.db.Invoices.Add(invoice);
                try
                {
                    this.db.SaveChanges();
                }
                catch
                {
                    result = 0;
                    return result;
                }
                num = invoice.InvoiceId;
                using (List<SalesItemViewModel>.Enumerator enumerator = asvm.lstsaleitem.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        SalesItemViewModel item = enumerator.Current;
                        if (item.MaterialTypeId == 1 || item.MaterialTypeId == 4 || item.MaterialTypeId == 2)
                        {
                            Stock stock = (
                                from a in this.db.Stocks
                                where a.MaterialId == (int?)item.MaterialId && a.UserId == (int?)asvm.UserId && a.SubMaterialTypeId == (int?)item.SubMaterialTypeId
                                select a).FirstOrDefault<Stock>();
                            Sale sale = new Sale();
                            sale.CustomerId = new int?(asvm.CustomerId);
                            sale.StockId = new int?(stock.StockId);
                            sale.Nos_Of_Piece = new int?(item.Piece);
                            sale.MaterialId = new int?(item.MaterialId);
                            sale.CreatedDateTime = DateTime.Now.ToShortDateString();
                            sale.UserId = new int?(asvm.UserId);
                            sale.InvoiceId = new int?(num);
                            SubMaterialType subMaterialType = (
                                from a in this.db.SubMaterialTypes
                                where a.MaterialTypeId == (int?)item.MaterialTypeId && a.SubMaterialTypeId == item.SubMaterialTypeId
                                select a).FirstOrDefault<SubMaterialType>();
                            Sale arg_D36_0 = sale;
                            float num5 = (float)item.Piece;
                            float? num4 = subMaterialType.Price;
                            num4 = (num4.HasValue ? new float?(num5 * num4.GetValueOrDefault()) : null);
                            arg_D36_0.TotalAmount = (num4.HasValue ? new float?(-num4.GetValueOrDefault()) : null);
                            this.db.Sales.Add(sale);
                            stock.Nos_Of_Piece += item.Piece;
                            try
                            {
                                this.db.SaveChanges();
                                continue;
                            }
                            catch
                            {
                                result = 0;
                                return result;
                            }
                        }
                        if (item.MaterialTypeId == 3)
                        {
                            Stock stock = (
                                from a in this.db.Stocks
                                where a.MaterialId == (int?)item.MaterialId && a.UserId == (int?)asvm.UserId && a.Sub_KasabId == (int?)item.SubMaterialTypeId
                                select a).FirstOrDefault<Stock>();
                            Sale sale = new Sale();
                            sale.CustomerId = new int?(asvm.CustomerId);
                            sale.StockId = new int?(stock.StockId);
                            sale.Nos_Of_Piece = new int?(item.Piece);
                            sale.MaterialId = new int?(item.MaterialId);
                            sale.CreatedDateTime = DateTime.Now.ToShortDateString();
                            sale.UserId = new int?(asvm.UserId);
                            sale.InvoiceId = new int?(num);
                            Sub_Kasab sub_Kasab = (
                                from a in this.db.Sub_Kasab
                                where a.MaterialTypeId == (int?)item.MaterialTypeId && a.Sub_KasabId == item.SubMaterialTypeId
                                select a).FirstOrDefault<Sub_Kasab>();
                            sale.TotalAmount = new float?(-((float)item.Piece * sub_Kasab.Price.Value));
                            this.db.Sales.Add(sale);
                            stock.Nos_Of_Piece += item.Piece;
                            try
                            {
                                this.db.SaveChanges();
                                continue;
                            }
                            catch
                            {
                                result = 0;
                                return result;
                            }
                        }
                        if (item.MaterialTypeId == 5 || item.MaterialTypeId == 6 || item.MaterialTypeId == 7)
                        {
                            Stock stock = (
                                from a in this.db.Stocks
                                where a.MaterialId == (int?)item.MaterialId && a.UserId == (int?)asvm.UserId
                                select a).FirstOrDefault<Stock>();
                            Sale sale = new Sale();
                            sale.CustomerId = new int?(asvm.CustomerId);
                            sale.StockId = new int?(stock.StockId);
                            sale.Tot_Weight_in_Gram_ = new float?(item.Weight * 1000f);
                            sale.MaterialId = new int?(item.MaterialId);
                            sale.CreatedDateTime = DateTime.Now.ToShortDateString();
                            sale.UserId = new int?(asvm.UserId);
                            sale.InvoiceId = new int?(num);
                            AllMaterial allMaterial = (
                                from a in this.db.AllMaterials
                                where a.MaterialTypeId == (int?)item.MaterialTypeId && a.MaterialId == item.MaterialId
                                select a).FirstOrDefault<AllMaterial>();
                            sale.TotalAmount = new float?(-(item.Weight * 1000f * (float)allMaterial.Price.Value) / 100f);
                            this.db.Sales.Add(sale);
                            Stock arg_1463_0 = stock;
                            float? num4 = stock.Tot_Weight_in_Gram_;
                            float num5 = item.Weight * 1000f;
                            arg_1463_0.Tot_Weight_in_Gram_ = (num4.HasValue ? new float?(num4.GetValueOrDefault() + num5) : null);
                            try
                            {
                                this.db.SaveChanges();
                                continue;
                            }
                            catch
                            {
                                result = 0;
                                return result;
                            }
                        }
                        try
                        {
                            this.db.SaveChanges();
                        }
                        catch
                        {
                            result = 0;
                            return result;
                        }
                    }
                }
            }
            result = num;
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
