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
    public class GetAllMaterialforStockApiController : ApiController
    {
        private PragatiEntities db = new PragatiEntities();
        public List<GetMaterialViewModel> Get()
        {
            List<MaterialType> list = this.db.MaterialTypes.ToList<MaterialType>();
            List<GetMaterialViewModel> list2 = new List<GetMaterialViewModel>();
            foreach (MaterialType current in list)
            {
                list2.Add(new GetMaterialViewModel
                {
                    MaterialTypeId = current.MaterialTypeId,
                    MaterialTypeName = current.MaterialName
                });
            }
            return list2;
        }
        public List<GetSubmaterialViewModel> Get(int TypeId)
        {
            List<GetSubmaterialViewModel> list = new List<GetSubmaterialViewModel>();
            if (TypeId == 3)
            {
                List<Sub_Kasab> list2 = (
                    from a in this.db.Sub_Kasab
                    where a.MaterialTypeId == (int?)TypeId
                    select a).ToList<Sub_Kasab>();
                foreach (Sub_Kasab current in list2)
                {
                    list.Add(new GetSubmaterialViewModel
                    {
                        SubMaterialTypeId = current.Sub_KasabId,
                        SubMaterialTypeName = current.Name
                    });
                }
            }
            if (TypeId == 1 || TypeId == 4 || TypeId == 2)
            {
                List<SubMaterialType> list3 = (
                    from a in this.db.SubMaterialTypes
                    where a.MaterialTypeId == (int?)TypeId
                    select a).ToList<SubMaterialType>();
                foreach (SubMaterialType current2 in list3)
                {
                    GetSubmaterialViewModel getSubmaterialViewModel = new GetSubmaterialViewModel();
                    getSubmaterialViewModel.SubMaterialTypeId = current2.SubMaterialTypeId;
                    string subMaterialTypeName = current2.Name + "(" + current2.Length + ")";
                    getSubmaterialViewModel.SubMaterialTypeName = subMaterialTypeName;
                    list.Add(getSubmaterialViewModel);
                }
            }
            return list;
        }
        public List<GetAllMaterialForStockViewModel> Get(int TypeId, string sp)
        {
            List<GetAllMaterialForStockViewModel> list = new List<GetAllMaterialForStockViewModel>();
            List<AllMaterial> list2 = (
                from a in this.db.AllMaterials
                where a.MaterialTypeId == (int?)TypeId
                select a).ToList<AllMaterial>();
            foreach (AllMaterial current in list2)
            {
                GetAllMaterialForStockViewModel getAllMaterialForStockViewModel = new GetAllMaterialForStockViewModel();
                getAllMaterialForStockViewModel.MaterialId = current.MaterialId;
                getAllMaterialForStockViewModel.MaterialCode = current.MaterialCode;
                if (current.MaterialImagePath != null)
                {
                    getAllMaterialForStockViewModel.MaterialImage = current.MaterialImagePath;
                }
                list.Add(getAllMaterialForStockViewModel);
            }
            return list;
        }
        public List<GetAllMaterialForStockViewModel> Get(int TypeId, int SubMaterialId, string spd)
        {
            List<GetAllMaterialForStockViewModel> list = new List<GetAllMaterialForStockViewModel>();
            if (TypeId == 3)
            {
                List<AllMaterial> list2 = (
                    from a in this.db.AllMaterials
                    where a.MaterialTypeId == (int?)TypeId && a.Sub_KasabId == (int?)SubMaterialId
                    select a).ToList<AllMaterial>();
                foreach (AllMaterial current in list2)
                {
                    GetAllMaterialForStockViewModel getAllMaterialForStockViewModel = new GetAllMaterialForStockViewModel();
                    getAllMaterialForStockViewModel.MaterialId = current.MaterialId;
                    getAllMaterialForStockViewModel.MaterialCode = current.MaterialCode;
                    if (current.MaterialImagePath != null)
                    {
                        getAllMaterialForStockViewModel.MaterialImage = current.MaterialImagePath;
                    }
                    list.Add(getAllMaterialForStockViewModel);
                }
            }
            else
            {
                List<AllMaterial> list2 = (
                    from a in this.db.AllMaterials
                    where a.MaterialTypeId == (int?)TypeId
                    select a).ToList<AllMaterial>();
                foreach (AllMaterial current in list2)
                {
                    GetAllMaterialForStockViewModel getAllMaterialForStockViewModel = new GetAllMaterialForStockViewModel();
                    getAllMaterialForStockViewModel.MaterialId = current.MaterialId;
                    getAllMaterialForStockViewModel.MaterialCode = current.MaterialCode;
                    if (current.MaterialImagePath != null)
                    {
                        getAllMaterialForStockViewModel.MaterialImage = current.MaterialImagePath;
                    }
                    list.Add(getAllMaterialForStockViewModel);
                }
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
