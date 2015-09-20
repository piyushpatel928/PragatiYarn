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
    public class GetRatesApiController : ApiController
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
        public string Get(int TypeId, int SubMaterialId, string spd)
        {
            string result;
            if (TypeId == 1 || TypeId == 4)
            {
                SubMaterialType subMaterialType = (
                    from a in this.db.SubMaterialTypes
                    where a.MaterialTypeId == (int?)TypeId && a.SubMaterialTypeId == SubMaterialId
                    select a).FirstOrDefault<SubMaterialType>();
                result = subMaterialType.Price.Value + " Rs";
            }
            else
            {
                if (TypeId == 3)
                {
                    Sub_Kasab sub_Kasab = (
                        from a in this.db.Sub_Kasab
                        where a.MaterialTypeId == (int?)TypeId && a.Sub_KasabId == SubMaterialId
                        select a).FirstOrDefault<Sub_Kasab>();
                    result = sub_Kasab.Price.Value + " Rs";
                }
                else
                {
                    SubMaterialType subMaterialType = (
                        from a in this.db.SubMaterialTypes
                        where a.MaterialTypeId == (int?)TypeId
                        select a).FirstOrDefault<SubMaterialType>();
                    result = subMaterialType.Price.Value + " Rs";
                }
            }
            return result;
        }
        public int Post(RateViewModel rvm)
        {
            if (rvm.TypeId == 1 || rvm.TypeId == 4)
            {
                SubMaterialType subMaterialType = (
                    from a in this.db.SubMaterialTypes
                    where a.MaterialTypeId == (int?)rvm.TypeId && a.SubMaterialTypeId == rvm.SubMaterialId
                    select a).FirstOrDefault<SubMaterialType>();
                subMaterialType.Price = new float?(rvm.NewRate);
            }
            else
            {
                if (rvm.TypeId == 3)
                {
                    Sub_Kasab sub_Kasab = (
                        from a in this.db.Sub_Kasab
                        where a.MaterialTypeId == (int?)rvm.TypeId && a.Sub_KasabId == rvm.SubMaterialId
                        select a).FirstOrDefault<Sub_Kasab>();
                    sub_Kasab.Price = new float?(rvm.NewRate);
                }
                else
                {
                    SubMaterialType subMaterialType = (
                        from a in this.db.SubMaterialTypes
                        where a.MaterialTypeId == (int?)rvm.TypeId && a.SubMaterialTypeId == rvm.SubMaterialId
                        select a).FirstOrDefault<SubMaterialType>();
                    subMaterialType.Price = new float?(rvm.NewRate);
                }
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
        public void Put(int id, [FromBody] string value)
        {
        }
        public void Delete(int id)
        {
        }
    }
}
