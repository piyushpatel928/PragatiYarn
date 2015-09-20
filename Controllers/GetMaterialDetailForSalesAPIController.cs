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
    public class GetMaterialDetailForSalesAPIController : ApiController
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
        public List<GetAllMaterialForSalesViewModel> Get(int MaterialTypeid, string sp, int Userid)
        {
            List<Stock> list = (
                from a in this.db.Stocks
                where a.MaterialTypeId == (int?)MaterialTypeid
                select a).ToList<Stock>();
            List<GetAllMaterialForSalesViewModel> list2 = new List<GetAllMaterialForSalesViewModel>();
            if (list != null)
            {
                using (List<Stock>.Enumerator enumerator = list.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        Stock item = enumerator.Current;
                        AllMaterial allMaterial = (
                            from a in this.db.AllMaterials
                            where (int?)a.MaterialId == item.MaterialId
                            select a).FirstOrDefault<AllMaterial>();
                        GetAllMaterialForSalesViewModel getAllMaterialForSalesViewModel = new GetAllMaterialForSalesViewModel();
                        getAllMaterialForSalesViewModel.MaterialId = allMaterial.MaterialId;
                        getAllMaterialForSalesViewModel.MaterialCode = allMaterial.MaterialCode;
                        if (item.MaterialTypeId == 1 || item.MaterialTypeId == 2 || item.MaterialTypeId == 3 || item.MaterialTypeId == 4)
                        {

                            getAllMaterialForSalesViewModel.stock = (float)item.Nos_Of_Piece;
                        }
                        else if (item.MaterialTypeId == 5 || item.MaterialTypeId == 6 || item.MaterialTypeId == 7)
                        {
                            getAllMaterialForSalesViewModel.stock = (float)item.Tot_Weight_in_Gram_;
                        }
                        if (allMaterial.MaterialImagePath != null)
                        {
                            getAllMaterialForSalesViewModel.MaterialImage = allMaterial.MaterialImagePath;
                        }
                        list2.Add(getAllMaterialForSalesViewModel);
                    }
                }
            }
            return list2;
        }
        public List<GetAllMaterialForSalesViewModel> Get(int MaterialTypeid, int SubMaterialId, string spd, int Userid)
        {
            List<GetAllMaterialForSalesViewModel> result;
            if (MaterialTypeid == 3)
            {
                List<Stock> list = (
                    from a in this.db.Stocks
                    where a.MaterialTypeId == (int?)MaterialTypeid && a.Sub_KasabId == (int?)SubMaterialId && a.UserId == (int?)Userid
                    select a).ToList<Stock>();
                List<GetAllMaterialForSalesViewModel> list2 = new List<GetAllMaterialForSalesViewModel>();
                if (list != null)
                {
                    using (List<Stock>.Enumerator enumerator = list.GetEnumerator())
                    {
                        while (enumerator.MoveNext())
                        {
                            Stock item = enumerator.Current;
                            AllMaterial allMaterial = (
                                from a in this.db.AllMaterials
                                where (int?)a.MaterialId == item.MaterialId
                                select a).FirstOrDefault<AllMaterial>();
                            GetAllMaterialForSalesViewModel getAllMaterialForSalesViewModel = new GetAllMaterialForSalesViewModel();
                            getAllMaterialForSalesViewModel.MaterialId = allMaterial.MaterialId;
                            getAllMaterialForSalesViewModel.MaterialCode = allMaterial.MaterialCode;
                            if (item.MaterialTypeId == 1 || item.MaterialTypeId == 2 || item.MaterialTypeId == 3 || item.MaterialTypeId == 4)
                            {

                                getAllMaterialForSalesViewModel.stock = (float)item.Nos_Of_Piece;
                            }
                            else if (item.MaterialTypeId == 5 || item.MaterialTypeId == 6 || item.MaterialTypeId == 7)
                            {
                                getAllMaterialForSalesViewModel.stock = (float)item.Tot_Weight_in_Gram_;
                            }
                            if (allMaterial.MaterialImagePath != null)
                            {
                                getAllMaterialForSalesViewModel.MaterialImage = allMaterial.MaterialImagePath;
                            }
                            list2.Add(getAllMaterialForSalesViewModel);
                        }
                    }
                }
                result = list2;
            }
            else
            {


                List<Stock> list = (
                    from a in this.db.Stocks
                    where a.MaterialTypeId == (int?)MaterialTypeid && a.SubMaterialTypeId == (int?)SubMaterialId && a.UserId == (int?)Userid
                    select a).ToList<Stock>();
                List<GetAllMaterialForSalesViewModel> list2 = new List<GetAllMaterialForSalesViewModel>();
                if (list != null)
                {
                    using (List<Stock>.Enumerator enumerator = list.GetEnumerator())
                    {
                        while (enumerator.MoveNext())
                        {
                            Stock item = enumerator.Current;
                            AllMaterial allMaterial = (
                                from a in this.db.AllMaterials
                                where (int?)a.MaterialId == item.MaterialId
                                select a).FirstOrDefault<AllMaterial>();
                            GetAllMaterialForSalesViewModel getAllMaterialForSalesViewModel = new GetAllMaterialForSalesViewModel();
                            getAllMaterialForSalesViewModel.MaterialId = allMaterial.MaterialId;
                            getAllMaterialForSalesViewModel.MaterialCode = allMaterial.MaterialCode;
                            if (item.MaterialTypeId == 1 || item.MaterialTypeId == 2 || item.MaterialTypeId == 3 || item.MaterialTypeId == 4)
                            {

                                getAllMaterialForSalesViewModel.stock = (float)item.Nos_Of_Piece;
                            }
                            else if (item.MaterialTypeId == 5 || item.MaterialTypeId == 6 || item.MaterialTypeId == 7)
                            {
                                getAllMaterialForSalesViewModel.stock = (float)item.Tot_Weight_in_Gram_;
                            }
                            if (allMaterial.MaterialImagePath != null)
                            {
                                getAllMaterialForSalesViewModel.MaterialImage = allMaterial.MaterialImagePath;
                            }
                            list2.Add(getAllMaterialForSalesViewModel);
                        }
                    }
                }
                result = list2;

            }
            return result;
        }
        public int Post(AddSalesViewModel asvm)
        {
            int num = -2;
            int result;

            float num2 = 0f;
            Invoice invoice = new Invoice();
            invoice.CustomerId = new int?(asvm.CustomerId);
            invoice.UserId = new int?(asvm.UserId);
            if (asvm.Datetime != null)
            {
                invoice.DateTime = asvm.Datetime;
            }
            else
            {
                invoice.DateTime = DateTime.Now.ToShortDateString();
            }
            float returnsaleamt = 0f;
            float saleamt = 0f;


            if (asvm.lstreturnsaleitem != null)
            {
                foreach (var item in asvm.lstreturnsaleitem)
                {
                    //SalesItemViewModel item = enumerator.Current;
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
                                float num3 = item.Weight * (float)allMaterial.Price.Value;
                                num2 += num3;
                            }
                        }
                    }
                }
                returnsaleamt = num2;
                if (asvm.lstsaleitem == null)
                {
                    invoice.TotalAmount = -returnsaleamt;
                }
            }
            if (asvm.lstsaleitem != null)
            {
                foreach (var item in asvm.lstsaleitem)
                {
                    // SalesItemViewModel item = enumerator.Current;
                    if (item.MaterialTypeId == 1 || item.MaterialTypeId == 4 || item.MaterialTypeId == 2)
                    {
                        if (!((
                            from a in this.db.Stocks
                            where a.MaterialId == (int?)item.MaterialId && a.UserId == (int?)asvm.UserId && a.SubMaterialTypeId == (int?)item.SubMaterialTypeId
                            select a).FirstOrDefault<Stock>().Nos_Of_Piece >= item.Piece))
                        {
                            result = -1;
                            return result;
                        }
                        SubMaterialType subMaterialType = (
                            from a in this.db.SubMaterialTypes
                            where a.MaterialTypeId == (int?)item.MaterialTypeId && a.SubMaterialTypeId == item.SubMaterialTypeId
                            select a).FirstOrDefault<SubMaterialType>();
                        saleamt += (float)item.Piece * subMaterialType.Price.Value;
                    }
                    else
                    {
                        if (item.MaterialTypeId == 3)
                        {
                            if (!((
                                from a in this.db.Stocks
                                where a.MaterialId == (int?)item.MaterialId && a.UserId == (int?)asvm.UserId && a.Sub_KasabId == (int?)item.SubMaterialTypeId
                                select a).FirstOrDefault<Stock>().Nos_Of_Piece >= item.Piece))
                            {
                                result = -1;
                                return result;
                            }
                            Sub_Kasab sub_Kasab = (
                                from a in this.db.Sub_Kasab
                                where a.MaterialTypeId == (int?)item.MaterialTypeId && a.Sub_KasabId == item.SubMaterialTypeId
                                select a).FirstOrDefault<Sub_Kasab>();
                            saleamt += (float)item.Piece * sub_Kasab.Price.Value;
                        }
                        else
                        {
                            if (item.MaterialTypeId == 5 || item.MaterialTypeId == 6 || item.MaterialTypeId == 7)
                            {
                                Stock stock = (
                                    from a in this.db.Stocks
                                    where a.MaterialId == (int?)item.MaterialId && a.UserId == (int?)asvm.UserId
                                    select a).FirstOrDefault<Stock>();
                                float? num3 = stock.Tot_Weight_in_Gram_;
                                float num4 = item.Weight;
                                if (num3.GetValueOrDefault() <= num4 || !num3.HasValue)
                                {
                                    result = -1;
                                    return result;
                                }
                                AllMaterial allMaterial = (
                                    from a in this.db.AllMaterials
                                    where a.MaterialTypeId == (int?)item.MaterialTypeId && a.MaterialId == item.MaterialId
                                    select a).FirstOrDefault<AllMaterial>();
                                float num5 = item.Weight * (float)allMaterial.Price.Value;
                                saleamt += num5;
                            }
                        }
                    }
                }

                invoice.TotalAmount = new float?(saleamt) - returnsaleamt;
                CustomerPayableAmount customerPayableAmount = (
                    from a in this.db.CustomerPayableAmounts
                    where a.CustomerId == (int?)asvm.CustomerId && a.UserId == (int?)asvm.UserId
                    select a).FirstOrDefault<CustomerPayableAmount>();
                if (customerPayableAmount != null)
                {
                    CustomerPayableAmount arg_A07_0 = customerPayableAmount;
                    float? num3 = customerPayableAmount.Amount;
                    float num4 = num2;
                    arg_A07_0.Amount = (num3.HasValue ? new float?(num3.GetValueOrDefault() + saleamt) : null);
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
                else
                {
                    CustomerPayableAmount customerPayableAmount2 = new CustomerPayableAmount();
                    customerPayableAmount2.CustomerId = new int?(asvm.CustomerId);
                    customerPayableAmount2.Amount = new float?(saleamt) + saleamt;
                    customerPayableAmount2.UserId = new int?(asvm.UserId);
                    this.db.CustomerPayableAmounts.Add(customerPayableAmount2);
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
                foreach (var item in asvm.lstsaleitem)
                {
                    //SalesItemViewModel item = enumerator.Current;
                    if (item.MaterialTypeId == 1 || item.MaterialTypeId == 4 || item.MaterialTypeId == 2)
                    {
                        Stock stock = (
                            from a in this.db.Stocks
                            where a.MaterialId == (int?)item.MaterialId && a.UserId == (int?)asvm.UserId && a.SubMaterialTypeId == (int?)item.SubMaterialTypeId
                            select a).FirstOrDefault<Stock>();
                        if (stock.Nos_Of_Piece >= item.Piece)
                        {
                            Sale sale = new Sale();
                            sale.CustomerId = new int?(asvm.CustomerId);
                            sale.StockId = new int?(stock.StockId);
                            sale.Nos_Of_Piece = new int?(item.Piece);
                            sale.MaterialId = new int?(item.MaterialId);

                            if (asvm.Datetime != null)
                            {
                                sale.CreatedDateTime = asvm.Datetime;
                            }
                            else
                            {
                                sale.CreatedDateTime = DateTime.Now.ToShortDateString();
                            }
                            sale.UserId = new int?(asvm.UserId);
                            sale.InvoiceId = new int?(num);
                            sale.ReturnPiece = "No";
                            SubMaterialType subMaterialType = (
                                from a in this.db.SubMaterialTypes
                                where a.MaterialTypeId == (int?)item.MaterialTypeId && a.SubMaterialTypeId == item.SubMaterialTypeId
                                select a).FirstOrDefault<SubMaterialType>();
                            Sale arg_E8E_0 = sale;
                            float num4 = (float)item.Piece;
                            float? num3 = subMaterialType.Price;
                            arg_E8E_0.TotalAmount = (num3.HasValue ? new float?(num4 * num3.GetValueOrDefault()) : null);
                            this.db.Sales.Add(sale);
                            stock.Nos_Of_Piece -= item.Piece;
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
                        result = -1;
                        return result;
                    }
                    if (item.MaterialTypeId == 3)
                    {
                        Stock stock = (
                            from a in this.db.Stocks
                            where a.MaterialId == (int?)item.MaterialId && a.UserId == (int?)asvm.UserId && a.Sub_KasabId == (int?)item.SubMaterialTypeId
                            select a).FirstOrDefault<Stock>();
                        if (stock.Nos_Of_Piece >= item.Piece)
                        {
                            Sale sale = new Sale();
                            sale.CustomerId = new int?(asvm.CustomerId);
                            sale.StockId = new int?(stock.StockId);
                            sale.Nos_Of_Piece = new int?(item.Piece);
                            sale.MaterialId = new int?(item.MaterialId);
                            if (asvm.Datetime != null)
                            {
                                sale.CreatedDateTime = asvm.Datetime;
                            }
                            else
                            {
                                sale.CreatedDateTime = DateTime.Now.ToShortDateString();
                            }
                            sale.UserId = new int?(asvm.UserId);
                            sale.InvoiceId = new int?(num);
                            sale.ReturnPiece = "No";
                            Sub_Kasab sub_Kasab = (
                                from a in this.db.Sub_Kasab
                                where a.MaterialTypeId == (int?)item.MaterialTypeId && a.Sub_KasabId == item.SubMaterialTypeId
                                select a).FirstOrDefault<Sub_Kasab>();
                            sale.TotalAmount = new float?((float)item.Piece * sub_Kasab.Price.Value);
                            this.db.Sales.Add(sale);
                            stock.Nos_Of_Piece -= item.Piece;
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
                        result = -1;
                        return result;
                    }
                    if (item.MaterialTypeId == 5 || item.MaterialTypeId == 6 || item.MaterialTypeId == 7)
                    {
                        Stock stock = (
                            from a in this.db.Stocks
                            where a.MaterialId == (int?)item.MaterialId && a.UserId == (int?)asvm.UserId
                            select a).FirstOrDefault<Stock>();
                        float? num3 = stock.Tot_Weight_in_Gram_;
                        float num4 = item.Weight;
                        if (num3.GetValueOrDefault() >= num4 && num3.HasValue)
                        {
                            Sale sale = new Sale();
                            sale.CustomerId = new int?(asvm.CustomerId);
                            sale.StockId = new int?(stock.StockId);
                            sale.Tot_Weight_in_Gram_ = new float?(item.Weight);
                            sale.MaterialId = new int?(item.MaterialId);
                            if (asvm.Datetime != null)
                            {
                                sale.CreatedDateTime = asvm.Datetime;
                            }
                            else
                            {
                                sale.CreatedDateTime = DateTime.Now.ToShortDateString();
                            }
                            sale.UserId = new int?(asvm.UserId);
                            sale.InvoiceId = new int?(num);
                            sale.ReturnPiece = "No";
                            AllMaterial allMaterial = (
                                from a in this.db.AllMaterials
                                where a.MaterialTypeId == (int?)item.MaterialTypeId && a.MaterialId == item.MaterialId
                                select a).FirstOrDefault<AllMaterial>();
                            sale.TotalAmount = new float?(item.Weight * (float)allMaterial.Price.Value);
                            this.db.Sales.Add(sale);
                            Stock arg_1644_0 = stock;
                            num3 = stock.Tot_Weight_in_Gram_;
                            num4 = item.Weight;
                            arg_1644_0.Tot_Weight_in_Gram_ = (num3.HasValue ? new float?(num3.GetValueOrDefault() - num4) : null);
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
                        result = -1;
                        return result;
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



            if (asvm.lstreturnsaleitem != null)
            {
                CustomerPayableAmount customerPayableAmount = (
                   from a in this.db.CustomerPayableAmounts
                   where a.CustomerId == (int?)asvm.CustomerId && a.UserId == (int?)asvm.UserId
                   select a).FirstOrDefault<CustomerPayableAmount>();
                if (customerPayableAmount != null)
                {
                    CustomerPayableAmount arg_93B_0 = customerPayableAmount;
                    float? num4 = customerPayableAmount.Amount;
                    float num5 = num2;
                    arg_93B_0.Amount = (num4.HasValue ? new float?(num4.GetValueOrDefault() - returnsaleamt) : null);
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
                else
                {
                    CustomerPayableAmount customerPayableAmount2 = new CustomerPayableAmount();
                    customerPayableAmount2.CustomerId = new int?(asvm.CustomerId);
                    customerPayableAmount2.Amount = new float?(returnsaleamt);
                    customerPayableAmount2.UserId = new int?(asvm.UserId);
                    this.db.CustomerPayableAmounts.Add(customerPayableAmount2);
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
                if (asvm.lstsaleitem == null)
                {
                    this.db.Invoices.Add(invoice);
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
                num = invoice.InvoiceId;
                foreach (var item in asvm.lstreturnsaleitem)
                {
                    //SalesItemViewModel item = enumerator.Current;
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
                        if (asvm.Datetime != null)
                        {
                            sale.CreatedDateTime = asvm.Datetime;
                        }
                        else
                        {
                            sale.CreatedDateTime = DateTime.Now.ToShortDateString();
                        }
                        sale.UserId = new int?(asvm.UserId);
                        sale.InvoiceId = new int?(num);
                        sale.ReturnPiece = "Yes";
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
                        if (asvm.Datetime != null)
                        {
                            sale.CreatedDateTime = asvm.Datetime;
                        }
                        else
                        {
                            sale.CreatedDateTime = DateTime.Now.ToShortDateString();
                        }
                        sale.UserId = new int?(asvm.UserId);
                        sale.InvoiceId = new int?(num);
                        sale.ReturnPiece = "Yes";
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
                        sale.Tot_Weight_in_Gram_ = new float?(item.Weight);
                        sale.MaterialId = new int?(item.MaterialId);
                        if (asvm.Datetime != null)
                        {
                            sale.CreatedDateTime = asvm.Datetime;
                        }
                        else
                        {
                            sale.CreatedDateTime = DateTime.Now.ToShortDateString();
                        }
                        sale.UserId = new int?(asvm.UserId);
                        sale.InvoiceId = new int?(num);
                        sale.ReturnPiece = "Yes";
                        AllMaterial allMaterial = (
                            from a in this.db.AllMaterials
                            where a.MaterialTypeId == (int?)item.MaterialTypeId && a.MaterialId == item.MaterialId
                            select a).FirstOrDefault<AllMaterial>();
                        sale.TotalAmount = new float?(-(item.Weight * (float)allMaterial.Price.Value));
                        this.db.Sales.Add(sale);
                        Stock arg_1463_0 = stock;
                        float? num4 = stock.Tot_Weight_in_Gram_;
                        float num5 = item.Weight;
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
            //try
            //{
            //    this.db.Invoices.Add(invoice);

            //    this.db.SaveChanges();
            //}
            //catch
            //{
            //    result = 0;
            //    return result;
            //}
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
