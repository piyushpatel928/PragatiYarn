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
    public class GetAllMaterialForRateChangeApiController : ApiController
    {
        private PragatiEntities db = new PragatiEntities();
        // GET api/getallmaterialforratechangeapi
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/getallmaterialforratechangeapi/5
        public List<GetSubmaterialViewModel> Get(int TypeId)
        {
            List<GetSubmaterialViewModel> list = new List<GetSubmaterialViewModel>();
            if (TypeId == 5 || TypeId == 6 || TypeId == 7)
            {
                List<AllMaterial> list2 = (
                    from a in this.db.AllMaterials
                    where a.MaterialTypeId == (int?)TypeId
                    select a).ToList<AllMaterial>();
                foreach (AllMaterial current in list2)
                {
                    list.Add(new GetSubmaterialViewModel
                    {
                        SubMaterialTypeId = current.MaterialId,
                        SubMaterialTypeName = current.MaterialCode
                    });
                }
            }
            
            return list;
        }

        public string Get(int TypeId, int SubMaterialId, string spd)
        {
            string result;
            if (TypeId == 5 || TypeId == 6 || TypeId == 7)
            {
                AllMaterial allmat = (
                    from a in this.db.AllMaterials
                    where a.MaterialTypeId == (int?)TypeId && a.MaterialId == SubMaterialId
                    select a).FirstOrDefault<AllMaterial>();
                result = allmat.Price.Value + " Rs";
                return result;
            }
            else
            {
                return null;
            }
            
        }
        // POST api/getallmaterialforratechangeapi
        public int Post(RateViewModel rvm)
        {
            if (rvm.TypeId == 5 || rvm.TypeId == 6 || rvm.TypeId == 7)
            {
                AllMaterial allmat = (
                    from a in this.db.AllMaterials
                    where a.MaterialTypeId == (int?)rvm.TypeId && a.MaterialId == rvm.SubMaterialId
                    select a).FirstOrDefault<AllMaterial>();
                allmat.Price = new float?(rvm.NewRate);
            }
            
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

        // PUT api/getallmaterialforratechangeapi/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/getallmaterialforratechangeapi/5
        public void Delete(int id)
        {
        }
    }
}
