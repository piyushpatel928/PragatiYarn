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
    public class getAllMaterialApiController : ApiController
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
            return list;
        }
        public List<MaterialsViewModel> Get(int TypeId, int SubTypeId)
        {
            List<MaterialsViewModel> list = new List<MaterialsViewModel>();
            if (TypeId == 3)
            {
                List<AllMaterial> list2 = (
                    from a in this.db.AllMaterials
                    where a.MaterialTypeId == (int?)TypeId && a.Sub_KasabId == (int?)SubTypeId
                    select a).ToList<AllMaterial>();
                foreach (AllMaterial current in list2)
                {
                    list.Add(new MaterialsViewModel
                    {
                        MaterialId = current.MaterialId,
                        MaterialCode = current.MaterialCode,
                        MaterialImagePath = current.MaterialImagePath,
                        Datetime = current.Datetime.Value.ToLongDateString()
                    });
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
                    list.Add(new MaterialsViewModel
                    {
                        MaterialId = current.MaterialId,
                        MaterialCode = current.MaterialCode,
                        MaterialImagePath = current.MaterialImagePath,
                        Datetime = current.Datetime.Value.ToLongDateString()
                    });
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
