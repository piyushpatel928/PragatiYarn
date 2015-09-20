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
    public class ViewAllStocksApiController : ApiController
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
        public List<ViewAllStocksViewModel> Get(int MaterialTypeId, int UserId)
        {
            List<ViewAllStocksViewModel> result;
            if (MaterialTypeId == 2)
            {
                List<ViewAllStocksViewModel> list = new List<ViewAllStocksViewModel>();
                List<Stock> list2 = (
                    from a in this.db.Stocks
                    where a.MaterialTypeId == (int?)MaterialTypeId && a.UserId == (int?)UserId
                    select a).ToList<Stock>();
                if (list2 != null)
                {
                    using (List<Stock>.Enumerator enumerator = list2.GetEnumerator())
                    {
                        while (enumerator.MoveNext())
                        {
                            Stock item = enumerator.Current;
                            ViewAllStocksViewModel viewAllStocksViewModel = new ViewAllStocksViewModel();
                            viewAllStocksViewModel.Stock = item.Nos_Of_Piece + " piece";
                            viewAllStocksViewModel.LastAddedStock = item.LastAddedStock + " piece";
                            viewAllStocksViewModel.UpdatedDateTime = item.UpdatedDatetime.Value.ToLongDateString();
                            AllMaterial allMaterial = (
                                from a in this.db.AllMaterials
                                where (int?)a.MaterialId == item.MaterialId
                                select a).FirstOrDefault<AllMaterial>();
                            viewAllStocksViewModel.MaterialCode = allMaterial.MaterialCode;
                            viewAllStocksViewModel.MaterialImage = allMaterial.MaterialImagePath;
                            list.Add(viewAllStocksViewModel);
                        }
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
                List<ViewAllStocksViewModel> list = new List<ViewAllStocksViewModel>();
                List<Stock> list2 = (
                    from a in this.db.Stocks
                    where a.MaterialTypeId == (int?)MaterialTypeId && a.UserId == (int?)UserId
                    select a).ToList<Stock>();
                if (list2 != null)
                {
                    using (List<Stock>.Enumerator enumerator = list2.GetEnumerator())
                    {
                        while (enumerator.MoveNext())
                        {
                            Stock item = enumerator.Current;
                            ViewAllStocksViewModel viewAllStocksViewModel = new ViewAllStocksViewModel();
                            ViewAllStocksViewModel arg_3E2_0 = viewAllStocksViewModel;
                            float? tot_Weight_in_Gram_ = item.Tot_Weight_in_Gram_;
                            arg_3E2_0.Stock = (tot_Weight_in_Gram_.HasValue ? new float?(tot_Weight_in_Gram_.GetValueOrDefault()) : null) + " Kilogram";
                            viewAllStocksViewModel.LastAddedStock = float.Parse(item.LastAddedStock) + " Kilogram";
                            viewAllStocksViewModel.UpdatedDateTime = item.UpdatedDatetime.Value.ToLongDateString();
                            AllMaterial allMaterial = (
                                from a in this.db.AllMaterials
                                where (int?)a.MaterialId == item.MaterialId
                                select a).FirstOrDefault<AllMaterial>();
                            viewAllStocksViewModel.MaterialCode = allMaterial.MaterialCode;
                            viewAllStocksViewModel.MaterialImage = allMaterial.MaterialImagePath;
                            list.Add(viewAllStocksViewModel);
                        }
                    }
                    result = list;
                }
                else
                {
                    result = null;
                }
            }
            return result;
        }
        public List<ViewAllStocksViewModel> Get(int MaterialTypeId, int SubMaterialId, string spd, int UserId)
        {
            List<ViewAllStocksViewModel> list = new List<ViewAllStocksViewModel>();
            List<ViewAllStocksViewModel> result;
            if (MaterialTypeId == 3)
            {
                List<Stock> list2 = (
                    from a in this.db.Stocks
                    where a.MaterialTypeId == (int?)MaterialTypeId && a.UserId == (int?)UserId && a.Sub_KasabId == SubMaterialId 
                    select a).ToList<Stock>();
                if (list2 != null)
                {
                    using (List<Stock>.Enumerator enumerator = list2.GetEnumerator())
                    {
                        while (enumerator.MoveNext())
                        {
                            Stock item = enumerator.Current;
                            ViewAllStocksViewModel viewAllStocksViewModel = new ViewAllStocksViewModel();
                            viewAllStocksViewModel.Stock = item.Nos_Of_Piece + " piece";
                            viewAllStocksViewModel.LastAddedStock = item.LastAddedStock + " piece";
                            viewAllStocksViewModel.UpdatedDateTime = item.UpdatedDatetime.Value.ToLongDateString();
                            AllMaterial allMaterial = (
                                from a in this.db.AllMaterials
                                where (int?)a.MaterialId == item.MaterialId
                                select a).FirstOrDefault<AllMaterial>();
                            viewAllStocksViewModel.MaterialCode = allMaterial.MaterialCode;
                            viewAllStocksViewModel.MaterialImage = allMaterial.MaterialImagePath;
                            list.Add(viewAllStocksViewModel);
                        }
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
                List<Stock> list2 = (
                    from a in this.db.Stocks
                    where a.MaterialTypeId == (int?)MaterialTypeId && a.SubMaterialTypeId == (int?)SubMaterialId && a.UserId == (int?)UserId
                    select a).ToList<Stock>();
                if (list2 != null)
                {
                    using (List<Stock>.Enumerator enumerator = list2.GetEnumerator())
                    {
                        while (enumerator.MoveNext())
                        {
                            Stock item = enumerator.Current;
                            ViewAllStocksViewModel viewAllStocksViewModel = new ViewAllStocksViewModel();
                            viewAllStocksViewModel.Stock = item.Nos_Of_Piece + " piece";
                            viewAllStocksViewModel.LastAddedStock = item.LastAddedStock + " piece";
                            viewAllStocksViewModel.UpdatedDateTime = item.UpdatedDatetime.Value.ToLongDateString();
                            AllMaterial allMaterial = (
                                from a in this.db.AllMaterials
                                where (int?)a.MaterialId == item.MaterialId
                                select a).FirstOrDefault<AllMaterial>();
                            viewAllStocksViewModel.MaterialCode = allMaterial.MaterialCode;
                            viewAllStocksViewModel.MaterialImage = allMaterial.MaterialImagePath;
                            list.Add(viewAllStocksViewModel);
                        }
                    }
                    result = list;
                }
                else
                {
                    result = null;
                }
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
