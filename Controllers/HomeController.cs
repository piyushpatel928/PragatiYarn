using PragatiYarn.Models.Abstract;
using PragatiYarn.Models.DB;
using PragatiYarn.Models.Maps;
using PragatiYarn.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PragatiYarn.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        public UserMaps usrMaps = new UserMaps();
        public IUserRepository _db
        {
            get;
            set;
        }
        public HomeController(IUserRepository db)
        {
            _db = db;
        }
        public ActionResult Index()
        {
            return RedirectToAction("LogIn", "Home");
        }
        public ActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LogIn(LoginViewModel lvm)
        {
            //ActionResult result;
            if (ModelState.IsValid == true)
            {
                try
                {
                    int num = this._db.CheckUserLogin(usrMaps.MapLogInVMToUser(lvm));
                    if (num != 0)
                    {
                        Session["UserId"] = num.ToString();
                        //Session["UserId"] = num.ToString();
                        return RedirectToAction("Sales", "Home");
                    }
                    else
                    {
                        //if (HomeController.<LogIn>o__SiteContainer0.<>p__Site1 == null)
                        //{
                        //    HomeController.<LogIn>o__SiteContainer0.<>p__Site1 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Message", typeof(HomeController), new CSharpArgumentInfo[]
                        //    {
                        //        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
                        //        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
                        //    }));
                        //}
                        //HomeController.<LogIn>o__SiteContainer0.<>p__Site1.Target(HomeController.<LogIn>o__SiteContainer0.<>p__Site1, base.get_ViewBag(), "Invalid UserName / Password.");
                        @ViewBag.Message = "Invalid UserName / Password.";
                        return View();
                    }
                }
                catch
                {
                    @ViewBag.Message = "Error occured While loged in your account.";
                    return View();
                }
            }
            else
            {
                return View();
            }
            //return result;
        }

        public ActionResult AddUser()
        {

            if (Session["UserId"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("LogIn");
            }

        }
        [HttpPost]
        public ActionResult AddUser(AddUserViewModel auvm)
        {

            if (Session["UserId"] != null)
            {
                if (ModelState.IsValid == true)
                {
                    int num = _db.IsUsernameAvailable(auvm.Username);
                    if (num == 0)
                    {
                        int num2 = this._db.InsertNewUser(this.usrMaps.MapNewUserVMToUser(auvm));
                        if (num2 == 1)
                        {
                            //if (HomeController.<AddUser>o__SiteContainer2.<>p__Site3 == null)
                            //{
                            //    HomeController.<AddUser>o__SiteContainer2.<>p__Site3 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Message", typeof(HomeController), new CSharpArgumentInfo[]
                            //    {
                            //        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
                            //        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
                            //    }));
                            //}
                            //HomeController.<AddUser>o__SiteContainer2.<>p__Site3.Target(HomeController.<AddUser>o__SiteContainer2.<>p__Site3, base.get_ViewBag(), "User Added Successfully");
                            ViewBag.Message = "User Added Successfully";
                            return View();
                        }
                        else
                        {
                            //if (HomeController.<AddUser>o__SiteContainer2.<>p__Site4 == null)
                            //{
                            //    HomeController.<AddUser>o__SiteContainer2.<>p__Site4 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "error", typeof(HomeController), new CSharpArgumentInfo[]
                            //    {
                            //        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
                            //        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
                            //    }));
                            //}
                            //HomeController.<AddUser>o__SiteContainer2.<>p__Site4.Target(HomeController.<AddUser>o__SiteContainer2.<>p__Site4, base.get_ViewBag(), "Error Occured while Inserting New User");
                            ViewBag.error = "Error Occured while Inserting New User";
                            return View();
                        }
                    }
                    else
                    {
                        //if (HomeController.<AddUser>o__SiteContainer2.<>p__Site5 == null)
                        //{
                        //    HomeController.<AddUser>o__SiteContainer2.<>p__Site5 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "error", typeof(HomeController), new CSharpArgumentInfo[]
                        //    {
                        //        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
                        //        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
                        //    }));
                        //}
                        //HomeController.<AddUser>o__SiteContainer2.<>p__Site5.Target(HomeController.<AddUser>o__SiteContainer2.<>p__Site5, base.get_ViewBag(), "Username Already avilable");
                        ViewBag.error = "Username Already avilable";
                        return View();
                    }
                }
                else
                {
                    return View();
                }
            }
            else
            {
                return RedirectToAction("LogIn");
            }

        }

        public ActionResult AddCustomer()
        {

            if (Session["UserId"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("LogIn");
            }

        }
        [HttpPost]
        public ActionResult AddCustomer(AddCustomerViewModel acvm, HttpPostedFileBase ProfImage)
        {

            if (Session["UserId"] != null)
            {
                acvm.UserId = int.Parse(Session["UserId"].ToString());
                if (ModelState.IsValid == true)
                {
                    if (ProfImage != null && ProfImage.ContentLength > 0)
                    {
                        string text = ProfImage.FileName;
                        text = text.Substring(text.LastIndexOf("."));
                        if (!(text == ".jpg") && !(text == ".jpeg") && !(text == ".png"))
                        {

                            //HomeController.<AddCustomer>o__SiteContainer6.<>p__Site7 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Message", typeof(HomeController), new CSharpArgumentInfo[]
                            //{
                            //    CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
                            //    CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
                            //}));
                            ViewBag.Message = "Invalid Image.";

                            //HomeController.<AddCustomer>o__SiteContainer6.<>p__Site7.Target(HomeController.<AddCustomer>o__SiteContainer6.<>p__Site7, base.get_ViewBag(), "Invalid Image.");
                            return View();

                        }
                        string text2 = Path.GetFileName(ProfImage.FileName);
                        string fileName = ProfImage.FileName;
                        text2 = text2.Substring(0, text2.LastIndexOf(".") - 1);
                        text2 = text2 + DateTime.Now.Ticks.ToString() + text;
                        string filename = Path.Combine(Server.MapPath("~/CustomerImage/"), text2);
                        ProfImage.SaveAs(filename);
                        string customerImagePath = "~/CustomerImage/" + text2;
                        acvm.CustomerImageName = acvm.CustomerSurName + " " + acvm.CustomerName;
                        acvm.CustomerImagePath = customerImagePath;


                    }
                    else
                    {
                        acvm.CustomerImageName = "No Image";
                        acvm.CustomerImagePath = "~/Content/images/no-Image.png";
                    }
                    int num = this._db.InsertNewCustomer(usrMaps.MapNewCustomerVMToCustomer(acvm));
                    if (num == 1)
                    {
                        //if (HomeController.<AddCustomer>o__SiteContainer6.<>p__Site8 == null)
                        //{
                        //    HomeController.<AddCustomer>o__SiteContainer6.<>p__Site8 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Message", typeof(HomeController), new CSharpArgumentInfo[]
                        //    {
                        //        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
                        //        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
                        //    }));
                        //}
                        //HomeController.<AddCustomer>o__SiteContainer6.<>p__Site8.Target(HomeController.<AddCustomer>o__SiteContainer6.<>p__Site8, base.get_ViewBag(), "Customer Added Successfully");
                        @ViewBag.Message = "Customer Added Successfully";
                        return RedirectToAction("ViewCustomers", "Home");
                        //result = base.RedirectToAction("ViewCustomers");
                    }
                    else
                    {
                        //if (HomeController.<AddCustomer>o__SiteContainer6.<>p__Site9 == null)
                        //{
                        //    HomeController.<AddCustomer>o__SiteContainer6.<>p__Site9 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "error", typeof(HomeController), new CSharpArgumentInfo[]
                        //    {
                        //        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
                        //        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
                        //    }));
                        //}
                        //HomeController.<AddCustomer>o__SiteContainer6.<>p__Site9.Target(HomeController.<AddCustomer>o__SiteContainer6.<>p__Site9, base.get_ViewBag(), "Error Occured while Inserting New Customer");
                        //result = base.View();
                        @ViewBag.error = "Error Occured while Inserting New Customer";
                        return View();
                    }

                }
                else
                {
                    return View();
                }
            }
            else
            {
                return RedirectToAction("Home", "LogIn");
            }

        }

        public ActionResult EditCustomer(int id)
        {

            if (Session["UserId"] != null)
            {
                return View(usrMaps.MapCustomerToCustomerVM(_db.GetCustomerById(id)));
            }
            else
            {
                return RedirectToAction("LogIn");
            }

        }
        [HttpPost]
        public ActionResult EditCustomer(int id, AddCustomerViewModel acvm, HttpPostedFileBase ProfImage)
        {

            if (Session["UserId"] != null)
            {
                if (ModelState.IsValid == true)
                {
                    if (ProfImage != null && ProfImage.ContentLength > 0)
                    {
                        string text = ProfImage.FileName;
                        text = text.Substring(text.LastIndexOf("."));
                        if (!(text == ".jpg") && !(text == ".jpeg") && !(text == ".png"))
                        {
                            //if (HomeController.<EditCustomer>o__SiteContainera.<>p__Siteb == null)
                            //{
                            //    HomeController.<EditCustomer>o__SiteContainera.<>p__Siteb = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Message", typeof(HomeController), new CSharpArgumentInfo[]
                            //    {
                            //        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
                            //        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
                            //    }));
                            //}
                            //HomeController.<EditCustomer>o__SiteContainera.<>p__Siteb.Target(HomeController.<EditCustomer>o__SiteContainera.<>p__Siteb, base.get_ViewBag(), "Invalid Image.");
                            ViewBag.Message = "Invalid Image.";
                            return View(acvm);

                        }
                        string text2 = Path.GetFileName(ProfImage.FileName);
                        string fileName = ProfImage.FileName;
                        text2 = text2.Substring(0, text2.LastIndexOf(".") - 1);
                        text2 = text2 + DateTime.Now.Ticks.ToString() + text;
                        string filename = Path.Combine(Server.MapPath("~/CustomerImage/"), text2);
                        ProfImage.SaveAs(filename);
                        string customerImagePath = "~/CustomerImage/" + text2;
                        acvm.CustomerImageName = acvm.CustomerSurName + " " + acvm.CustomerName;
                        acvm.CustomerImagePath = customerImagePath;
                    }
                    int num = this._db.UpdateCustomer(acvm, id);
                    if (num == 1)
                    {
                        //if (HomeController.<EditCustomer>o__SiteContainera.<>p__Sitec == null)
                        //{
                        //    HomeController.<EditCustomer>o__SiteContainera.<>p__Sitec = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Message", typeof(HomeController), new CSharpArgumentInfo[]
                        //    {
                        //        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
                        //        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
                        //    }));
                        //}
                        //HomeController.<EditCustomer>o__SiteContainera.<>p__Sitec.Target(HomeController.<EditCustomer>o__SiteContainera.<>p__Sitec, base.get_ViewBag(), "Customer Updated Successfully");
                        //result = base.View(acvm);
                        ViewBag.Message = "Customer Updated Successfully";
                        return View(acvm);
                    }
                    else
                    {
                        //if (HomeController.<EditCustomer>o__SiteContainera.<>p__Sited == null)
                        //{
                        //    HomeController.<EditCustomer>o__SiteContainera.<>p__Sited = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "error", typeof(HomeController), new CSharpArgumentInfo[]
                        //    {
                        //        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
                        //        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
                        //    }));
                        //}
                        //HomeController.<EditCustomer>o__SiteContainera.<>p__Sited.Target(HomeController.<EditCustomer>o__SiteContainera.<>p__Sited, base.get_ViewBag(), "Error Occured while Updating Customer");
                        ViewBag.Message = "Error Occured while Updating Customer";
                        return View(acvm);
                    }
                }
                else
                {
                    return View(acvm);
                }
            }
            else
            {
                return RedirectToAction("LogIn");
            }

        }
        public ActionResult DeleteCustomer(int id)
        {

            if (Session["UserId"] != null)
            {
                int num = _db.DeleteCustomer(id);
                return RedirectToAction("ViewCustomers");
            }
            else
            {
                return RedirectToAction("LogIn");
            }

        }

        public ActionResult ViewCustomers()
        {

            if (Session["UserId"] != null)
            {
                return View(usrMaps.MaplstCustomerTolstCustomerVM(_db.GetAllCustomer()));
            }
            else
            {
                return RedirectToAction("LogIn");
            }

        }
        public ActionResult CustomerPayment()
        {

            if (Session["UserId"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("LogIn");
            }

        }

        public ActionResult Payment()
        {
            if (Session["UserId"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("LogIn");
            }
        }

        public ActionResult Pay(int CustomerId, int UserId)
        {
            if (Session["UserId"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("LogIn");
            }
        }
        public ActionResult PaymentHistory()
        {
            if (Session["UserId"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("LogIn");
            }
        }

        public ActionResult AddCustomerCredit()
        {
            if (Session["UserId"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("LogIn");
            }
        }
        [HttpPost]
        public ActionResult AddCustomerCredit(AddCreditAmountViewModel acavm)
        {
            if (Session["UserId"] != null)
            {
                int UserId = int.Parse(Session["UserId"].ToString());
                acavm.UserId = UserId;
                if (acavm.Amount != 0.0)
                {
                    if (acavm.customerId != 0)
                    {
                        PragatiEntities db = new PragatiEntities();
                        CustomerPayableAmount cpa = db.CustomerPayableAmounts.Where(a => a.CustomerId == acavm.customerId && a.UserId == acavm.UserId).FirstOrDefault();
                        if (cpa == null)
                        {
                            CustomerPayableAmount cpab = new CustomerPayableAmount();
                            cpab.CustomerId = acavm.customerId;
                            cpab.UserId = acavm.UserId;
                            cpab.Amount = (float)acavm.Amount;
                            db.CustomerPayableAmounts.Add(cpab);
                            try
                            {
                                db.SaveChanges();
                            }
                            catch
                            {
                                @ViewBag.Message = "Error Occured While adding credit into Customer Account...Please try again";
                                return View();
                            }
                            return RedirectToAction("CustomerPayment");

                        }
                        else
                        {
                            cpa.Amount = cpa.Amount + (float)acavm.Amount;
                            cpa.CustomerId = acavm.customerId;
                            cpa.UserId = acavm.UserId;
                            try
                            {
                                db.SaveChanges();
                            }
                            catch
                            {
                                @ViewBag.Message = "Error Occured While adding credit into Customer Account...Please try again";
                                return View();
                            }
                            return RedirectToAction("CustomerPayment");
                        }
                        //if (num == 1)
                        //{

                        //    return RedirectToAction("CustomerPayment");
                        //}
                        //else
                        //{
                        //    @ViewBag.Message = "Error Occured While adding credit into Customer Account...Please try again";
                        //    return View();
                        //}
                    }
                    else
                    {
                        @ViewBag.Message = "Please Select Customer";
                        return View();
                    }
                }
                else
                {
                    @ViewBag.Message = "Please Enter Valid Amount";
                    return View();
                }

            }
            else
            {
                return RedirectToAction("LogIn");
            }
        }

        public ActionResult CustomersCreditReport()
        {
            if (Session["UserId"] != null)
            {
                int UserId = int.Parse(Session["UserId"].ToString());
                var data = _db.GetAllCustomersCreditAmtByUserId(UserId);
                if (data != null)
                {

                    return View(data);
                }
                else
                {
                    ViewBag.message = "No data available";
                    return View();

                }
            }
            else
            {
                return RedirectToAction("LogIn");
            }
        }

        public ActionResult Sales()
        {
            if (Session["UserId"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("LogIn");
            }
        }

        public ActionResult ReturnSales()
        {

            if (Session["UserId"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("LogIn");
            }

        }

        public ActionResult AddNewMaterial()
        {

            if (Session["UserId"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("LogIn");
            }

        }
        [HttpPost]
        public ActionResult AddNewMaterial(AddMaterialViewModel addmvm, HttpPostedFileBase MaterialImage)
        {

            if (Session["UserId"] != null)
            {

                if (MaterialImage != null)
                {
                    string text = MaterialImage.FileName;
                    text = text.Substring(text.LastIndexOf("."));
                    if (text == ".jpg" || text == ".jpeg" || text == ".png" || text == ".JPG" || text == ".JPEG" || text == ".PNG")
                    {
                        string text2 = Path.GetFileName(MaterialImage.FileName);
                        string fileName = MaterialImage.FileName;
                        text2 = text2.Substring(0, text2.LastIndexOf(".") - 1);
                        text2 = text2 + DateTime.Now.Ticks.ToString() + text;
                        string filename = Path.Combine(Server.MapPath("~/MaterialImage/"), text2);
                        MaterialImage.SaveAs(filename);
                        string materialImagePath = "~/MaterialImage/" + text2;
                        addmvm.MaterialImagePath = materialImagePath;
                        int num = _db.insertNewMaterial(usrMaps.MapAllMaterialVmToAllMaterial(addmvm));
                        if (num == 1)
                        {
                            //if (HomeController.<AddNewMaterial>o__SiteContainere.<>p__Site10 == null)
                            //{
                            //    HomeController.<AddNewMaterial>o__SiteContainere.<>p__Site10 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "success", typeof(HomeController), new CSharpArgumentInfo[]
                            //    {
                            //        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
                            //        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
                            //    }));
                            //}
                            //HomeController.<AddNewMaterial>o__SiteContainere.<>p__Site10.Target(HomeController.<AddNewMaterial>o__SiteContainere.<>p__Site10, base.get_ViewBag(), "Material Added Successfully");
                            ViewBag.success = "Material Added Successfully";
                            return View();
                        }
                        else
                        {
                            //if (HomeController.<AddNewMaterial>o__SiteContainere.<>p__Site11 == null)
                            //{
                            //    HomeController.<AddNewMaterial>o__SiteContainere.<>p__Site11 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Message", typeof(HomeController), new CSharpArgumentInfo[]
                            //    {
                            //        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
                            //        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
                            //    }));
                            //}
                            //HomeController.<AddNewMaterial>o__SiteContainere.<>p__Site11.Target(HomeController.<AddNewMaterial>o__SiteContainere.<>p__Site11, base.get_ViewBag(), "Error occured Please try Again");
                            ViewBag.Message = "Error occured Please try Again";
                            return View();
                        }
                    }
                    else
                    {
                        //if (HomeController.<AddNewMaterial>o__SiteContainere.<>p__Sitef == null)
                        //{
                        //    HomeController.<AddNewMaterial>o__SiteContainere.<>p__Sitef = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Message", typeof(HomeController), new CSharpArgumentInfo[]
                        //    {
                        //        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
                        //        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
                        //    }));
                        //}
                        //HomeController.<AddNewMaterial>o__SiteContainere.<>p__Sitef.Target(HomeController.<AddNewMaterial>o__SiteContainere.<>p__Sitef, base.get_ViewBag(), "Invalid Image.");
                        ViewBag.Message = "Invalid Image.";
                        return View();
                    }
                }
                else
                {
                    addmvm.MaterialImage = "No Image";
                    addmvm.MaterialImagePath = "~/Content/images/no-Image.png";
                    int num = _db.insertNewMaterial(usrMaps.MapAllMaterialVmToAllMaterial(addmvm));
                    if (num == 1)
                    {
                        ViewBag.success = "Material Added Successfully";
                        return View();
                    }
                    else
                    {
                        ViewBag.Message = "Error occured Please try Again";
                        return View();
                    }
                }
            }
            else
            {
                return RedirectToAction("LogIn");
            }

        }


        public ActionResult ViewMaterials(int? id)
        {

            if (Session["UserId"] != null)
            {
                if (id == 1)
                {

                    ViewBag.success = "Material Removed Successfully";

                }
                if (id == -1)
                {

                    ViewBag.error = "Error Occured While Deleting This Material";
                }
                if (id == -2)
                {

                    ViewBag.error = "Stock Already Added for this material...So You Can not Delete this Material";
                }
                return View();
            }
            else
            {
                return RedirectToAction("LogIn");
            }

        }
        public ActionResult DeleteMaterial(int id)
        {
            if (Session["UserId"] != null)
            {
                PragatiEntities pragatiEntities = new PragatiEntities();
                Stock stock = (
                    from a in pragatiEntities.Stocks
                    where a.MaterialId == (int?)id
                    select a).FirstOrDefault<Stock>();
                if (stock == null)
                {
                    AllMaterial allMaterial = (
                        from a in pragatiEntities.AllMaterials
                        where a.MaterialId == id
                        select a).FirstOrDefault<AllMaterial>();
                    string path = Server.MapPath(allMaterial.MaterialImagePath);
                    // File.Delete(path);
                    //try
                    //{
                    //    FileInfo fifo = new FileInfo(path);
                    //    fifo.Delete();
                    //}
                    pragatiEntities.AllMaterials.Remove(allMaterial);
                    try
                    {
                        pragatiEntities.SaveChanges();
                        return RedirectToAction("ViewMaterials", new
                        {
                            id = 1
                        });

                    }
                    catch
                    {
                        return RedirectToAction("ViewMaterials", new
                        {
                            id = -1
                        });

                    }
                }
                return RedirectToAction("ViewMaterials", new
                {
                    id = -2
                });
            }
            else
            {
                return RedirectToAction("LogIn");
            }

        }
        public ActionResult EditMaterial(int id)
        {

            if (Session["UserId"] != null)
            {
                PragatiEntities db = new PragatiEntities();
                var data = db.AllMaterials.Where(a => a.MaterialId == id).FirstOrDefault();
                EditMaterialViewModel edtmvm = new EditMaterialViewModel();
                edtmvm.MaterialId = id;
                edtmvm.MaterialCode = data.MaterialCode;
                edtmvm.MaterialImagePath = data.MaterialImagePath;
                var materialtype = db.MaterialTypes.Where(a => a.MaterialTypeId == data.MaterialTypeId).FirstOrDefault();
                edtmvm.MaterialName = materialtype.MaterialName;
                if (materialtype.MaterialTypeId == 3)
                {
                    var submaterialtype = db.Sub_Kasab.Where(a => a.MaterialTypeId == data.MaterialTypeId).FirstOrDefault();
                    edtmvm.SubMaterialName = submaterialtype.Name;
                    edtmvm.SubMaterialId = submaterialtype.Sub_KasabId;
                }
                return View(edtmvm);

            }
            else
            {
                return RedirectToAction("LogIn");
            }
        }
        [HttpPost]
        public ActionResult EditMaterial(EditMaterialViewModel emvm, HttpPostedFileBase MaterialImage)
        {
            if (Session["UserId"] != null)
            {
                PragatiEntities db = new PragatiEntities();

                string text = MaterialImage.FileName;
                text = text.Substring(text.LastIndexOf("."));
                if (text == ".jpg" || text == ".jpeg" || text == ".png" || text == ".JPG" || text == ".JPEG" || text == ".PNG")
                {
                    string text2 = Path.GetFileName(MaterialImage.FileName);
                    string fileName = MaterialImage.FileName;
                    text2 = text2.Substring(0, text2.LastIndexOf(".") - 1);
                    text2 = text2 + DateTime.Now.Ticks.ToString() + text;
                    string filename = Path.Combine(Server.MapPath("~/MaterialImage/"), text2);
                    MaterialImage.SaveAs(filename);
                    string materialImagePath = "~/MaterialImage/" + text2;
                    emvm.MaterialImagePath = materialImagePath;

                    AllMaterial allMaterial = db.AllMaterials.Where(a => a.MaterialId == emvm.MaterialId).FirstOrDefault();
                    allMaterial.MaterialCode = emvm.MaterialCode;
                    allMaterial.Datetime = new DateTime?(DateTime.Now);
                    allMaterial.MaterialId = emvm.MaterialId;
                    if (emvm.MaterialImage != null)
                    {

                        allMaterial.MaterialImagePath = emvm.MaterialImagePath;
                    }
                    try
                    {
                        db.SaveChanges();
                        ViewBag.success = "Material Updated Successfully";
                        return View(emvm);
                    }
                    catch
                    {
                        ViewBag.Message = "Error occured Please try Again";
                        return View(emvm);
                    }

                }
                else
                {

                    ViewBag.Message = "Invalid Image.";
                    return View(emvm);
                }

            }
            else
            {
                return RedirectToAction("LogIn");
            }
        }
        public ActionResult AddStock()
        {
            if (Session["UserId"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("LogIn");
            }
        }
        public ActionResult ViewStocks()
        {
            if (Session["UserId"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("LogIn");
            }
        }
        public ActionResult LogOut()
        {
            Session["UserId"] = null;
            return RedirectToAction("LogIn");
        }

        public ActionResult Chalan(int CustomerId, int UserId, int InvoiceId)
        {

            //HomeController.<>c__DisplayClass18 <>c__DisplayClass = new HomeController.<>c__DisplayClass18();
            //<>c__DisplayClass.CustomerId = CustomerId;
            //<>c__DisplayClass.UserId = UserId;
            //<>c__DisplayClass.InvoiceId = InvoiceId;

            if (Session["UserId"] != null)
            {
                int num = 0;
                PragatiEntities pragatiEntities = new PragatiEntities();
                ChalanViewModel chalanViewModel = new ChalanViewModel();
                string text = DateTime.Now.ToShortDateString();
                Invoice invoice = (
                    from a in pragatiEntities.Invoices
                    where a.CustomerId == (int?)CustomerId && a.UserId == (int?)UserId && a.InvoiceId == InvoiceId
                    select a).FirstOrDefault<Invoice>();
                Customer customer = (
                    from a in pragatiEntities.Customers
                    where a.CustomerId == CustomerId
                    select a).FirstOrDefault<Customer>();
                chalanViewModel.ChalanId = InvoiceId;
                chalanViewModel.CustomerName = customer.CustomerName + " " + customer.CustomerSurname;
                chalanViewModel.CustomerFirmName = customer.CustomerFirmname;
                chalanViewModel.Date = DateTime.Parse(invoice.DateTime).ToString("dd/MM/yyyy");
                chalanViewModel.TotalAmount = invoice.TotalAmount.Value;
                List<Sale> list = (
                    from a in pragatiEntities.Sales
                    where a.CustomerId == (int?)CustomerId && a.UserId == (int?)UserId && a.InvoiceId == (int?)InvoiceId
                    select a).ToList<Sale>();
                List<ItemsViewModel> list2 = new List<ItemsViewModel>();
                int num2 = 0;
                bool flag = false;
                bool flag2 = false;
                bool flag3 = false;
                bool flag4 = false;
                string text2 = "";
                string text3 = "";
                string text4 = "";
                float num3 = 0f;
                float num4 = 0f;
                //using (List<Sale>.Enumerator enumerator = list.GetEnumerator())
                //{
                //while (enumerator.MoveNext())
                //{
                foreach (var item in list)
                {
                    //HomeController.<>c__DisplayClass1a <>c__DisplayClass1a = new HomeController.<>c__DisplayClass1a();
                    //<>c__DisplayClass1a.CS$<>8__locals19 = <>c__DisplayClass;
                    //<>c__DisplayClass1a.item = enumerator.Current;
                    if (item.ReturnPiece == "No" || item.ReturnPiece == null)
                    {
                        flag4 = false;
                        num++;
                        ItemsViewModel itemsViewModel = new ItemsViewModel();
                        List<SubItemsViewModel> list3 = new List<SubItemsViewModel>();
                        Stock stockdata = (
                            from a in pragatiEntities.Stocks
                            where (int?)a.StockId == item.StockId && a.MaterialId == item.MaterialId && a.UserId == item.UserId
                            select a).FirstOrDefault<Stock>();
                        AllMaterial allMaterial = (
                            from a in pragatiEntities.AllMaterials
                            where (int?)a.MaterialId == item.MaterialId
                            select a).FirstOrDefault<AllMaterial>();
                        MaterialType materialType = (
                            from a in pragatiEntities.MaterialTypes
                            where (int?)a.MaterialTypeId == stockdata.MaterialTypeId
                            select a).FirstOrDefault<MaterialType>();
                        string text5 = materialType.MaterialName.Substring(1);
                        if (stockdata.MaterialTypeId == 1 || stockdata.MaterialTypeId == 4 || stockdata.MaterialTypeId == 2)
                        {
                            SubMaterialType subMaterialType = (
                                from a in pragatiEntities.SubMaterialTypes
                                where (int?)a.SubMaterialTypeId == stockdata.SubMaterialTypeId && a.MaterialTypeId == stockdata.MaterialTypeId
                                select a).FirstOrDefault<SubMaterialType>();
                            foreach (ItemsViewModel current in list2)
                            {
                                if (current.ItemName == materialType.MaterialName.Substring(0, 1) + "-" + subMaterialType.Name.Substring(0, 3))
                                {
                                    itemsViewModel = current;
                                    flag = true;
                                    break;
                                }
                                flag = false;
                                text2 = "";
                            }
                            if (!flag)
                            {
                                flag4 = true;
                                itemsViewModel.ItemName = materialType.MaterialName.Substring(0, 1) + "-" + subMaterialType.Name.Substring(0, 3);
                                itemsViewModel.Rate = subMaterialType.Price.Value;
                                itemsViewModel.Amount = item.TotalAmount.Value;
                                itemsViewModel.SerailNo = num;
                            }
                            // float num5 = subMaterialType.Price.Value * (float)item.Nos_Of_Piece.Value;
                            float num5 = item.TotalAmount.Value;
                            chalanViewModel.TotalPiece += item.Nos_Of_Piece.Value;
                            itemsViewModel.totAmount += num5;
                            num2 += item.Nos_Of_Piece.Value;
                            string text6 = text2;
                            text2 = string.Concat(new string[]
							{
								text6,
								allMaterial.MaterialCode,
								"(",
								item.Nos_Of_Piece.ToString(),
								"),"
							});
                            float num6 = (float)item.Nos_Of_Piece.Value;
                            itemsViewModel.totpiece += num6;
                            ItemsViewModel expr_982 = itemsViewModel;
                            expr_982.lstsubitems += text2;
                            flag = false;
                            text2 = "";
                        }
                        else
                        {
                            if (stockdata.MaterialTypeId == 3)
                            {
                                Sub_Kasab sub_Kasab = (
                                    from a in pragatiEntities.Sub_Kasab
                                    where (int?)a.Sub_KasabId == stockdata.Sub_KasabId && a.MaterialTypeId == stockdata.MaterialTypeId
                                    select a).FirstOrDefault<Sub_Kasab>();
                                foreach (ItemsViewModel current in list2)
                                {
                                    if (current.ItemName == materialType.MaterialName.Substring(0, 1) + "-" + sub_Kasab.Name.Substring(0, 3))
                                    {
                                        itemsViewModel = current;
                                        flag2 = true;
                                        break;
                                    }
                                    flag2 = false;
                                    text2 = "";
                                }
                                if (!flag2)
                                {
                                    flag4 = true;
                                    itemsViewModel.ItemName = materialType.MaterialName.Substring(0, 1) + "-" + sub_Kasab.Name.Substring(0, 3);
                                    itemsViewModel.Rate = sub_Kasab.Price.Value;
                                    itemsViewModel.Amount = item.TotalAmount.Value;
                                    itemsViewModel.SerailNo = num;
                                }
                                //float num7 = sub_Kasab.Price.Value * (float)item.Nos_Of_Piece.Value;
                                float num7 = item.TotalAmount.Value;
                                chalanViewModel.TotalPiece += item.Nos_Of_Piece.Value;

                                itemsViewModel.totAmount += num7;
                                string text6 = text3;
                                text3 = string.Concat(new string[]
								{
									text6,
									allMaterial.MaterialCode,
									"(",
									item.Nos_Of_Piece.ToString(),
									"),"
								});
                                float num8 = (float)item.Nos_Of_Piece.Value;
                                itemsViewModel.totpiece += num8;
                                ItemsViewModel expr_C7F = itemsViewModel;
                                expr_C7F.lstsubitems += text3;
                                flag2 = false;
                                text3 = "";
                            }
                            else
                            {
                                if (stockdata.MaterialTypeId == 5 || stockdata.MaterialTypeId == 6 || stockdata.MaterialTypeId == 7)
                                {
                                    foreach (ItemsViewModel current in list2)
                                    {
                                        if (current.ItemName == materialType.MaterialName.Substring(0, 1))
                                        {
                                            itemsViewModel = current;
                                            flag3 = true;
                                            break;
                                        }
                                        flag3 = false;
                                        text4 = "";
                                    }
                                    float? tot_Weight_in_Gram_;
                                    if (!flag3)
                                    {
                                        flag4 = true;
                                        itemsViewModel.ItemName = materialType.MaterialName.Substring(0, 1);
                                        itemsViewModel.Rate = (float)allMaterial.Price.Value;
                                        itemsViewModel.Amount = item.TotalAmount.Value;
                                        ItemsViewModel arg_E1A_0 = itemsViewModel;
                                        tot_Weight_in_Gram_ = item.Tot_Weight_in_Gram_;
                                        arg_E1A_0.Piece = ((tot_Weight_in_Gram_.HasValue ? new float?(tot_Weight_in_Gram_.GetValueOrDefault()) : null) + " Kg ").ToString();
                                        itemsViewModel.SerailNo = num;
                                    }
                                    object obj = text4;
                                    string text7 = text4;
                                    text4 = string.Concat(new string[]
								{
									text7,
									allMaterial.MaterialCode,
									"(",
									item.Tot_Weight_in_Gram_.ToString(),
									"),"
								});
                                    object[] array = new object[5];
                                    array[0] = obj;
                                    array[1] = allMaterial.MaterialCode;
                                    array[2] = "(";
                                    object[] arg_E8F_0 = array;
                                    int arg_E8F_1 = 3;
                                    tot_Weight_in_Gram_ = item.Tot_Weight_in_Gram_;
                                    arg_E8F_0[arg_E8F_1] = (tot_Weight_in_Gram_.HasValue ? new float?(tot_Weight_in_Gram_.GetValueOrDefault()) : null);
                                    array[4] = "),";
                                    // text4 = string.Concat(array);
                                    itemsViewModel.lstsubitems += text4;


                                    //num3 += item.Tot_Weight_in_Gram_.Value;
                                    itemsViewModel.totpiece += item.Tot_Weight_in_Gram_.Value;
                                    // num4 += item.TotalAmount.Value;
                                    itemsViewModel.totAmount += item.TotalAmount.Value;


                                    flag3 = false;
                                    text4 = "";
                                    num3 = 0f;
                                    num4 = 0f;
                                }
                            }
                        }
                        if (flag4)
                        {
                            list2.Add(itemsViewModel);
                            chalanViewModel.Items = list2;
                        }
                    }
                    if (item.ReturnPiece == "Yes")
                    {
                        flag4 = false;
                        num++;
                        ItemsViewModel itemsViewModel = new ItemsViewModel();
                        List<SubItemsViewModel> list3 = new List<SubItemsViewModel>();
                        Stock stockdata = (
                            from a in pragatiEntities.Stocks
                            where (int?)a.StockId == item.StockId && a.MaterialId == item.MaterialId && a.UserId == item.UserId
                            select a).FirstOrDefault<Stock>();
                        AllMaterial allMaterial = (
                            from a in pragatiEntities.AllMaterials
                            where (int?)a.MaterialId == item.MaterialId
                            select a).FirstOrDefault<AllMaterial>();
                        MaterialType materialType = (
                            from a in pragatiEntities.MaterialTypes
                            where (int?)a.MaterialTypeId == stockdata.MaterialTypeId
                            select a).FirstOrDefault<MaterialType>();
                        string text5 = materialType.MaterialName.Substring(1);
                        if (stockdata.MaterialTypeId == 1 || stockdata.MaterialTypeId == 4 || stockdata.MaterialTypeId == 2)
                        {
                            SubMaterialType subMaterialType = (
                                from a in pragatiEntities.SubMaterialTypes
                                where (int?)a.SubMaterialTypeId == stockdata.SubMaterialTypeId && a.MaterialTypeId == stockdata.MaterialTypeId
                                select a).FirstOrDefault<SubMaterialType>();
                            foreach (ItemsViewModel current in list2)
                            {
                                if (current.ItemName == materialType.MaterialName.Substring(0, 1) + "-" + subMaterialType.Name.Substring(0, 3) + "-R")
                                {
                                    itemsViewModel = current;
                                    flag = true;
                                    break;
                                }
                                flag = false;
                                text2 = "";
                            }
                            if (!flag)
                            {
                                flag4 = true;
                                itemsViewModel.ItemName = materialType.MaterialName.Substring(0, 1) + "-" + subMaterialType.Name.Substring(0, 3) + "-R";
                                itemsViewModel.Rate = subMaterialType.Price.Value;
                                itemsViewModel.Amount = item.TotalAmount.Value;
                                itemsViewModel.SerailNo = num;
                            }
                            //float num5 = subMaterialType.Price.Value * (float)item.Nos_Of_Piece.Value;
                            float num5 = item.TotalAmount.Value;
                            itemsViewModel.totAmount += num5;
                            num2 += item.Nos_Of_Piece.Value;
                            chalanViewModel.TotalPiece -= item.Nos_Of_Piece.Value;
                            string text6 = text2;
                            text2 = string.Concat(new string[]
							{
								text6,
								allMaterial.MaterialCode,
								"(",
								item.Nos_Of_Piece.ToString(),
								"),"
							});
                            float num6 = (float)item.Nos_Of_Piece.Value;
                            itemsViewModel.totpiece += num6;
                            ItemsViewModel expr_982 = itemsViewModel;
                            expr_982.lstsubitems += text2;
                            flag = false;
                            text2 = "";
                        }
                        else
                        {
                            if (stockdata.MaterialTypeId == 3)
                            {
                                Sub_Kasab sub_Kasab = (
                                    from a in pragatiEntities.Sub_Kasab
                                    where (int?)a.Sub_KasabId == stockdata.Sub_KasabId && a.MaterialTypeId == stockdata.MaterialTypeId
                                    select a).FirstOrDefault<Sub_Kasab>();
                                foreach (ItemsViewModel current in list2)
                                {
                                    if (current.ItemName == materialType.MaterialName.Substring(0, 1) + "-" + sub_Kasab.Name.Substring(0, 3) + "-R")
                                    {
                                        itemsViewModel = current;
                                        flag2 = true;
                                        break;
                                    }
                                    flag2 = false;
                                    text2 = "";
                                }
                                if (!flag2)
                                {
                                    flag4 = true;
                                    itemsViewModel.ItemName = materialType.MaterialName.Substring(0, 1) + "-" + sub_Kasab.Name.Substring(0, 3) + "-R";
                                    itemsViewModel.Rate = sub_Kasab.Price.Value;
                                    itemsViewModel.Amount = item.TotalAmount.Value;
                                    itemsViewModel.SerailNo = num;
                                }
                                //float num7 = sub_Kasab.Price.Value * (float)item.Nos_Of_Piece.Value;
                                float num7 = item.TotalAmount.Value;
                                itemsViewModel.totAmount += num7;
                                string text6 = text3;
                                text3 = string.Concat(new string[]
								{
									text6,
									allMaterial.MaterialCode,
									"(",
									item.Nos_Of_Piece.ToString(),
									"),"
								});
                                float num8 = (float)item.Nos_Of_Piece.Value;
                                itemsViewModel.totpiece += num8;
                                chalanViewModel.TotalPiece -= item.Nos_Of_Piece.Value;
                                ItemsViewModel expr_C7F = itemsViewModel;
                                expr_C7F.lstsubitems += text3;
                                flag2 = false;
                                text3 = "";
                            }
                            else
                            {
                                if (stockdata.MaterialTypeId == 5 || stockdata.MaterialTypeId == 6 || stockdata.MaterialTypeId == 7)
                                {
                                    foreach (ItemsViewModel current in list2)
                                    {
                                        if (current.ItemName == materialType.MaterialName.Substring(0, 1) + "-R")
                                        {
                                            itemsViewModel = current;
                                            flag3 = true;
                                            break;
                                        }
                                        flag3 = false;
                                    }
                                    float? tot_Weight_in_Gram_;
                                    if (!flag3)
                                    {
                                        flag4 = true;
                                        itemsViewModel.ItemName = materialType.MaterialName.Substring(0, 1) + "-R";
                                        itemsViewModel.Rate = (float)allMaterial.Price.Value;
                                        itemsViewModel.Amount = item.TotalAmount.Value;
                                        ItemsViewModel arg_E1A_0 = itemsViewModel;
                                        tot_Weight_in_Gram_ = item.Tot_Weight_in_Gram_;
                                        arg_E1A_0.Piece = ((tot_Weight_in_Gram_.HasValue ? new float?(tot_Weight_in_Gram_.GetValueOrDefault()) : null) + " Kg ").ToString();
                                        itemsViewModel.SerailNo = num;
                                    }
                                    object obj = text4;
                                    object[] array = new object[5];
                                    array[0] = obj;
                                    array[1] = allMaterial.MaterialCode;
                                    array[2] = "(";
                                    object[] arg_E8F_0 = array;
                                    int arg_E8F_1 = 3;
                                    tot_Weight_in_Gram_ = item.Tot_Weight_in_Gram_;
                                    arg_E8F_0[arg_E8F_1] = (tot_Weight_in_Gram_.HasValue ? new float?(tot_Weight_in_Gram_.GetValueOrDefault()) : null);
                                    array[4] = "),";
                                    text4 = string.Concat(array);
                                    itemsViewModel.lstsubitems += text4;
                                    //num3 += item.Tot_Weight_in_Gram_.Value;
                                    itemsViewModel.totpiece += item.Tot_Weight_in_Gram_.Value;
                                    //num4 += item.TotalAmount.Value;
                                    itemsViewModel.totAmount += item.TotalAmount.Value;
                                    flag3 = false;
                                    text4 = "";
                                    num3 = 0f;
                                    num4 = 0f;
                                }
                            }
                        }
                        if (flag4)
                        {
                            list2.Add(itemsViewModel);
                            chalanViewModel.Items = list2;
                        }
                    }
                }

                //}
                //}
                return View(chalanViewModel);
            }
            else
            {
                return RedirectToAction("LogIn");
            }

        }

        public ActionResult RetunSaleChalan(int CustomerId, int UserId, int InvoiceId)
        {
            //HomeController.<>c__DisplayClass1e <>c__DisplayClass1e = new HomeController.<>c__DisplayClass1e();
            //<>c__DisplayClass1e.CustomerId = CustomerId;
            //<>c__DisplayClass1e.UserId = UserId;
            //<>c__DisplayClass1e.InvoiceId = InvoiceId;

            if (Session["UserId"] != null)
            {
                int num = 0;
                PragatiEntities pragatiEntities = new PragatiEntities();
                ChalanViewModel chalanViewModel = new ChalanViewModel();
                string text = DateTime.Now.ToShortDateString();
                Invoice invoice = (
                    from a in pragatiEntities.Invoices
                    where a.CustomerId == (int?)CustomerId && a.UserId == (int?)UserId && a.InvoiceId == InvoiceId
                    select a).FirstOrDefault<Invoice>();
                Customer customer = (
                    from a in pragatiEntities.Customers
                    where a.CustomerId == CustomerId
                    select a).FirstOrDefault<Customer>();
                chalanViewModel.ChalanId = InvoiceId;
                chalanViewModel.CustomerName = customer.CustomerName + " " + customer.CustomerSurname;
                chalanViewModel.CustomerFirmName = customer.CustomerFirmname;
                chalanViewModel.Date = DateTime.Parse(invoice.DateTime).ToString("dd/MM/yyyy");
                chalanViewModel.TotalAmount = invoice.TotalAmount.Value;
                float num2 = invoice.TotalAmount.Value;
                List<Sale> list = (
                    from a in pragatiEntities.Sales
                    where a.CustomerId == (int?)CustomerId && a.UserId == (int?)UserId && a.InvoiceId == (int?)InvoiceId
                    select a).ToList<Sale>();
                List<ItemsViewModel> list2 = new List<ItemsViewModel>();
                int num3 = 0;
                bool flag = false;
                bool flag2 = false;
                bool flag3 = false;
                bool flag4 = false;
                string text2 = "";
                string text3 = "";
                string text4 = "";
                float num4 = 0f;
                float num5 = 0f;
                //using (List<Sale>.Enumerator enumerator = list.GetEnumerator())
                //{
                //while (enumerator.MoveNext())
                //{
                foreach (var item in list)
                {
                    //HomeController.<>c__DisplayClass20 <>c__DisplayClass = new HomeController.<>c__DisplayClass20();
                    //<>c__DisplayClass.CS$<>8__locals1f = <>c__DisplayClass1e;
                    //<>c__DisplayClass.item = enumerator.Current;
                    flag4 = false;
                    num++;
                    ItemsViewModel itemsViewModel = new ItemsViewModel();
                    List<SubItemsViewModel> list3 = new List<SubItemsViewModel>();
                    Stock stockdata = (
                        from a in pragatiEntities.Stocks
                        where (int?)a.StockId == item.StockId && a.MaterialId == item.MaterialId && a.UserId == item.UserId
                        select a).FirstOrDefault<Stock>();
                    AllMaterial allMaterial = (
                        from a in pragatiEntities.AllMaterials
                        where (int?)a.MaterialId == item.MaterialId
                        select a).FirstOrDefault<AllMaterial>();
                    MaterialType materialType = (
                        from a in pragatiEntities.MaterialTypes
                        where (int?)a.MaterialTypeId == stockdata.MaterialTypeId
                        select a).FirstOrDefault<MaterialType>();
                    string text5 = materialType.MaterialName.Substring(1);
                    if (stockdata.MaterialTypeId == 1 || stockdata.MaterialTypeId == 4 || stockdata.MaterialTypeId == 2)
                    {
                        SubMaterialType subMaterialType = (
                            from a in pragatiEntities.SubMaterialTypes
                            where (int?)a.SubMaterialTypeId == stockdata.SubMaterialTypeId && a.MaterialTypeId == stockdata.MaterialTypeId
                            select a).FirstOrDefault<SubMaterialType>();
                        foreach (ItemsViewModel current in list2)
                        {
                            if (current.ItemName == materialType.MaterialName.Substring(0, 1) + "-" + subMaterialType.Name.Substring(0, 3))
                            {
                                itemsViewModel = current;
                                flag = true;
                                break;
                            }
                            flag = false;
                            text2 = "";
                        }
                        if (!flag)
                        {
                            flag4 = true;
                            itemsViewModel.ItemName = materialType.MaterialName.Substring(0, 1) + "-" + subMaterialType.Name.Substring(0, 3);
                            itemsViewModel.Rate = subMaterialType.Price.Value;
                            itemsViewModel.Amount = item.TotalAmount.Value;
                            itemsViewModel.SerailNo = num;
                        }
                        float num6 = -(subMaterialType.Price.Value * (float)item.Nos_Of_Piece.Value);
                        itemsViewModel.totAmount += num6;
                        num3 += item.Nos_Of_Piece.Value;
                        string text6 = text2;
                        text2 = string.Concat(new string[]
							{
								text6,
								allMaterial.MaterialCode,
								"(",
								item.Nos_Of_Piece.ToString(),
								"),"
							});
                        float num7 = (float)item.Nos_Of_Piece.Value;
                        itemsViewModel.totpiece += num7;
                        ItemsViewModel expr_99C = itemsViewModel;
                        expr_99C.lstsubitems += text2;
                        flag = false;
                        text2 = "";
                    }
                    else
                    {
                        if (stockdata.MaterialTypeId == 3)
                        {
                            Sub_Kasab sub_Kasab = (
                                from a in pragatiEntities.Sub_Kasab
                                where (int?)a.Sub_KasabId == stockdata.Sub_KasabId && a.MaterialTypeId == stockdata.MaterialTypeId
                                select a).FirstOrDefault<Sub_Kasab>();
                            foreach (ItemsViewModel current in list2)
                            {
                                if (current.ItemName == materialType.MaterialName.Substring(0, 1) + "-" + sub_Kasab.Name.Substring(0, 3))
                                {
                                    itemsViewModel = current;
                                    flag2 = true;
                                    break;
                                }
                                flag2 = false;
                                text2 = "";
                            }
                            if (!flag2)
                            {
                                flag4 = true;
                                itemsViewModel.ItemName = materialType.MaterialName.Substring(0, 1) + "-" + sub_Kasab.Name.Substring(0, 3);
                                itemsViewModel.Rate = sub_Kasab.Price.Value;
                                itemsViewModel.Amount = item.TotalAmount.Value;
                                itemsViewModel.SerailNo = num;
                            }
                            float num8 = -(sub_Kasab.Price.Value * (float)item.Nos_Of_Piece.Value);
                            itemsViewModel.totAmount += num8;
                            string text6 = text3;
                            text3 = string.Concat(new string[]
								{
									text6,
									allMaterial.MaterialCode,
									"(",
									item.Nos_Of_Piece.ToString(),
									"),"
								});
                            float num9 = (float)item.Nos_Of_Piece.Value;
                            itemsViewModel.totpiece += num9;
                            ItemsViewModel expr_C9A = itemsViewModel;
                            expr_C9A.lstsubitems += text3;
                            flag2 = false;
                            text3 = "";
                        }
                        else
                        {
                            if (stockdata.MaterialTypeId == 5 || stockdata.MaterialTypeId == 6 || stockdata.MaterialTypeId == 7)
                            {
                                foreach (ItemsViewModel current in list2)
                                {
                                    if (current.ItemName == materialType.MaterialName.Substring(0, 1))
                                    {
                                        itemsViewModel = current;
                                        flag3 = true;
                                        break;
                                    }
                                    flag3 = false;
                                }
                                float? tot_Weight_in_Gram_;
                                if (!flag3)
                                {
                                    flag4 = true;
                                    itemsViewModel.ItemName = materialType.MaterialName.Substring(0, 1);
                                    itemsViewModel.Rate = (float)allMaterial.Price.Value;
                                    itemsViewModel.Amount = item.TotalAmount.Value;
                                    ItemsViewModel arg_E35_0 = itemsViewModel;
                                    tot_Weight_in_Gram_ = item.Tot_Weight_in_Gram_;
                                    arg_E35_0.Piece = ((tot_Weight_in_Gram_.HasValue ? new float?(tot_Weight_in_Gram_.GetValueOrDefault() / 1000f) : null) + " Kg ").ToString();
                                    itemsViewModel.SerailNo = num;
                                }
                                object obj = text4;
                                object[] array = new object[5];
                                array[0] = obj;
                                array[1] = allMaterial.MaterialCode;
                                array[2] = "(";
                                object[] arg_EAA_0 = array;
                                int arg_EAA_1 = 3;
                                tot_Weight_in_Gram_ = item.Tot_Weight_in_Gram_;
                                arg_EAA_0[arg_EAA_1] = (tot_Weight_in_Gram_.HasValue ? new float?(tot_Weight_in_Gram_.GetValueOrDefault() / 1000f) : null);
                                array[4] = "),";
                                text4 = string.Concat(array);
                                itemsViewModel.lstsubitems = text4;
                                num4 += item.Tot_Weight_in_Gram_.Value / 1000f;
                                itemsViewModel.totpiece = num4;
                                num5 += item.TotalAmount.Value;
                                itemsViewModel.totAmount = num5;
                                flag3 = false;
                                text4 = "";
                                num4 = 0f;
                                num5 = 0f;
                            }
                        }
                    }
                    if (flag4)
                    {
                        list2.Add(itemsViewModel);
                        chalanViewModel.Items = list2;
                    }
                }
                //}
                //}
                return View(chalanViewModel);
            }
            else
            {
                return RedirectToAction("LogIn");
            }

        }

        public ActionResult DailyChalan(int CustomerId, int UserId, int ChalanId)
        {
            //HomeController.<>c__DisplayClass24 <>c__DisplayClass = new HomeController.<>c__DisplayClass24();
            //<>c__DisplayClass.CustomerId = CustomerId;
            //<>c__DisplayClass.UserId = UserId;
            //<>c__DisplayClass.ChalanId = ChalanId;
            if (Session["UserId"] != null)
            {
                //HomeController.<>c__DisplayClass26 <>c__DisplayClass2 = new HomeController.<>c__DisplayClass26();
                //<>c__DisplayClass2.CS$<>8__locals25 = <>c__DisplayClass;
                int num = 0;
                PragatiEntities pragatiEntities = new PragatiEntities();
                ChalanViewModel chalanViewModel = new ChalanViewModel();
                var invcdata = (
                    from a in pragatiEntities.Invoices
                    where a.CustomerId == (int?)CustomerId && a.UserId == (int?)UserId && a.InvoiceId == ChalanId
                    select a).FirstOrDefault<Invoice>();

                Customer customer = (
                    from a in pragatiEntities.Customers
                    where a.CustomerId == CustomerId
                    select a).FirstOrDefault<Customer>();
                chalanViewModel.ChalanId = invcdata.InvoiceId;
                chalanViewModel.CustomerName = customer.CustomerName + " " + customer.CustomerSurname;
                chalanViewModel.CustomerFirmName = customer.CustomerFirmname;
                chalanViewModel.Date = DateTime.Parse(invcdata.DateTime).ToString("dd/MM/yyyy");
                chalanViewModel.TotalAmount = invcdata.TotalAmount.Value;
                List<Sale> list = (
                    from a in pragatiEntities.Sales
                    where a.CustomerId == (int?)CustomerId && a.UserId == (int?)UserId && a.CreatedDateTime == invcdata.DateTime
                    select a).ToList<Sale>();
                List<ItemsViewModel> list2 = new List<ItemsViewModel>();
                bool flag = false;
                bool flag2 = false;
                bool flag3 = false;
                bool flag4 = false;
                string text = "";
                string text2 = "";
                string text3 = "";
                float num2 = 0f;
                float num3 = 0f;
                //using (List<Sale>.Enumerator enumerator = list.GetEnumerator())
                //{
                //while (enumerator.MoveNext())
                //{
                foreach (var item in list)
                {
                    //HomeController.<>c__DisplayClass28 <>c__DisplayClass3 = new HomeController.<>c__DisplayClass28();
                    //<>c__DisplayClass3.CS$<>8__locals27 = <>c__DisplayClass2;
                    //<>c__DisplayClass3.CS$<>8__locals25 = <>c__DisplayClass;
                    //<>c__DisplayClass3.item = enumerator.Current;

                    num++;
                    ItemsViewModel itemsViewModel = new ItemsViewModel();
                    flag4 = false;
                    Stock stockdata = (
                        from a in pragatiEntities.Stocks
                        where (int?)a.StockId == item.StockId && a.MaterialId == item.MaterialId && a.UserId == item.UserId
                        select a).FirstOrDefault<Stock>();
                    AllMaterial allMaterial = (
                        from a in pragatiEntities.AllMaterials
                        where (int?)a.MaterialId == item.MaterialId
                        select a).FirstOrDefault<AllMaterial>();
                    MaterialType materialType = (
                        from a in pragatiEntities.MaterialTypes
                        where (int?)a.MaterialTypeId == stockdata.MaterialTypeId
                        select a).FirstOrDefault<MaterialType>();
                    if (stockdata.MaterialTypeId == 1 || stockdata.MaterialTypeId == 4 || stockdata.MaterialTypeId == 2)
                    {
                        SubMaterialType subMaterialType = (
                            from a in pragatiEntities.SubMaterialTypes
                            where (int?)a.SubMaterialTypeId == stockdata.SubMaterialTypeId && a.MaterialTypeId == stockdata.MaterialTypeId
                            select a).FirstOrDefault<SubMaterialType>();
                        foreach (ItemsViewModel current in list2)
                        {
                            if (current.ItemName == materialType.MaterialName.Substring(0, 1) + "-" + subMaterialType.Name.Substring(0, 3))
                            {
                                itemsViewModel = current;
                                flag = true;
                                break;
                            }
                            flag = false;
                            text = "";
                        }
                        if (!flag)
                        {
                            flag4 = true;
                            itemsViewModel.ItemName = materialType.MaterialName.Substring(0, 1) + "-" + subMaterialType.Name.Substring(0, 3);
                            itemsViewModel.Rate = subMaterialType.Price.Value;
                            itemsViewModel.Amount = item.TotalAmount.Value;
                            itemsViewModel.SerailNo = num;
                        }
                        float num4 = subMaterialType.Price.Value * (float)item.Nos_Of_Piece.Value;
                        itemsViewModel.totAmount += num4;
                        string text4 = text;
                        text = string.Concat(new string[]
							{
								text4,
								allMaterial.MaterialCode,
								"(",
								item.Nos_Of_Piece.ToString(),
								"),"
							});
                        float num5 = (float)item.Nos_Of_Piece.Value;
                        itemsViewModel.totpiece += num5;
                        ItemsViewModel expr_980 = itemsViewModel;
                        expr_980.lstsubitems += text;
                        flag = false;
                        text = "";
                    }
                    else
                    {
                        if (stockdata.MaterialTypeId == 3)
                        {
                            Sub_Kasab sub_Kasab = (
                                from a in pragatiEntities.Sub_Kasab
                                where (int?)a.Sub_KasabId == stockdata.Sub_KasabId && a.MaterialTypeId == stockdata.MaterialTypeId
                                select a).FirstOrDefault<Sub_Kasab>();
                            foreach (ItemsViewModel current in list2)
                            {
                                if (current.ItemName == materialType.MaterialName.Substring(0, 1) + "-" + sub_Kasab.Name.Substring(0, 3))
                                {
                                    itemsViewModel = current;
                                    flag2 = true;
                                    break;
                                }
                                flag2 = false;
                                text = "";
                            }
                            if (!flag2)
                            {
                                flag4 = true;
                                itemsViewModel.ItemName = materialType.MaterialName.Substring(0, 1) + "-" + sub_Kasab.Name.Substring(0, 3);
                                itemsViewModel.Rate = sub_Kasab.Price.Value;
                                itemsViewModel.Amount = item.TotalAmount.Value;
                                itemsViewModel.SerailNo = num;
                            }
                            float num6 = sub_Kasab.Price.Value * (float)item.Nos_Of_Piece.Value;
                            itemsViewModel.totAmount += num6;
                            string text4 = text2;
                            text2 = string.Concat(new string[]
								{
									text4,
									allMaterial.MaterialCode,
									"(",
									item.Nos_Of_Piece.ToString(),
									"),"
								});
                            float num7 = (float)item.Nos_Of_Piece.Value;
                            itemsViewModel.totpiece += num7;
                            ItemsViewModel expr_C7D = itemsViewModel;
                            expr_C7D.lstsubitems += text2;
                            flag2 = false;
                            text2 = "";
                        }
                        else
                        {
                            if (stockdata.MaterialTypeId == 5 || stockdata.MaterialTypeId == 6 || stockdata.MaterialTypeId == 7)
                            {
                                foreach (ItemsViewModel current in list2)
                                {
                                    if (current.ItemName == materialType.MaterialName.Substring(0, 1))
                                    {
                                        itemsViewModel = current;
                                        flag3 = true;
                                        break;
                                    }
                                    flag3 = false;
                                }
                                float? tot_Weight_in_Gram_;
                                if (!flag3)
                                {
                                    flag4 = true;
                                    itemsViewModel.ItemName = materialType.MaterialName.Substring(0, 1);
                                    itemsViewModel.Rate = (float)allMaterial.Price.Value;
                                    itemsViewModel.Amount = item.TotalAmount.Value;
                                    ItemsViewModel arg_E18_0 = itemsViewModel;
                                    tot_Weight_in_Gram_ = item.Tot_Weight_in_Gram_;
                                    arg_E18_0.Piece = ((tot_Weight_in_Gram_.HasValue ? new float?(tot_Weight_in_Gram_.GetValueOrDefault() / 1000f) : null) + " Kg ").ToString();
                                    itemsViewModel.SerailNo = num;
                                }
                                object obj = text3;
                                object[] array = new object[5];
                                array[0] = obj;
                                array[1] = allMaterial.MaterialCode;
                                array[2] = "(";
                                object[] arg_E8D_0 = array;
                                int arg_E8D_1 = 3;
                                tot_Weight_in_Gram_ = item.Tot_Weight_in_Gram_;
                                arg_E8D_0[arg_E8D_1] = (tot_Weight_in_Gram_.HasValue ? new float?(tot_Weight_in_Gram_.GetValueOrDefault() / 1000f) : null);
                                array[4] = "),";
                                text3 = string.Concat(array);
                                itemsViewModel.lstsubitems = text3;
                                num2 += item.Tot_Weight_in_Gram_.Value / 1000f;
                                itemsViewModel.totpiece = num2;
                                num3 += item.TotalAmount.Value;
                                itemsViewModel.totAmount = num3;
                                flag3 = false;
                                text3 = "";
                                num2 = 0f;
                                num3 = 0f;
                            }
                        }
                    }
                    if (flag4)
                    {
                        list2.Add(itemsViewModel);
                        chalanViewModel.Items = list2;
                    }
                }
                //}
                //}
                return View(chalanViewModel);
            }
            else
            {
                return RedirectToAction("LogIn");
            }
        }

        public ActionResult MonthlyChalan(int CustomerId, int UserId, string Fromdate, string ToDate)
        {
            if (Session["UserId"] != null)
            {
                PragatiEntities pragatiEntities = new PragatiEntities();
                MonthlyInvoiceViewModel monthlyInvoiceViewModel = new MonthlyInvoiceViewModel();
                List<ChalansViewModel> list = new List<ChalansViewModel>();
                List<MonthlyInvoice_Down_ViewModel> lst_sub_down = new List<MonthlyInvoice_Down_ViewModel>();
                List<MonthlyInvoice_Down_ReturnViewModel> lst_sub_return_down = new List<MonthlyInvoice_Down_ReturnViewModel>();



                Customer customer = (
                    from a in pragatiEntities.Customers
                    where a.CustomerId == CustomerId
                    select a).FirstOrDefault<Customer>();
                DateTime t = DateTime.Parse(Fromdate);
                DateTime t2 = DateTime.Parse(ToDate);
                monthlyInvoiceViewModel.CustomerName = customer.CustomerName + " " + customer.CustomerSurname;
                monthlyInvoiceViewModel.CustomerFirmName = customer.CustomerFirmname;
                monthlyInvoiceViewModel.FromDate = t.ToString("dd/MM/yyyy");
                monthlyInvoiceViewModel.ToDate = t2.ToString("dd/MM/yyyy");


                List<Invoice> list2 = (
                    from a in pragatiEntities.Invoices
                    where a.CustomerId == (int?)CustomerId && a.UserId == (int?)UserId
                    select a).ToList<Invoice>();
                
                
                
                float num = 0f;
                foreach (Invoice current in list2)
                {
                    DateTime dateTime = DateTime.Parse(current.DateTime);
                    if (t <= dateTime && dateTime <= t2)
                    {
                        ChalansViewModel chalansViewModel = new ChalansViewModel();
                        chalansViewModel.ChalanId = current.InvoiceId;
                        chalansViewModel.Date = DateTime.Parse(current.DateTime).ToString("dd/MM/yyyy");
                        chalansViewModel.Amount = current.TotalAmount.Value;
                        num += current.TotalAmount.Value;


                        foreach (var current_sub in current.Sales)
                        {
                            var submatid = (from m in pragatiEntities.Stocks where m.StockId == (int)current_sub.StockId select m).FirstOrDefault();

                            if (current_sub.ReturnPiece == "No")
                            {
                                // foreach (var item in lst_sub_down)
                                // {
                                MonthlyInvoice_Down_ViewModel sub_down = new MonthlyInvoice_Down_ViewModel();

                                var subname = (from m in pragatiEntities.SubMaterialTypes where m.SubMaterialTypeId == submatid.SubMaterialTypeId select m).FirstOrDefault();
                                var mainname = (from m in pragatiEntities.MaterialTypes where m.MaterialTypeId == submatid.MaterialTypeId select m).FirstOrDefault();
                                if (subname != null)
                                {
                                    // subname.Name = "";
                                    //total number of thread based on item
                                    if (lst_sub_down.Count == 0)
                                    {
                                        sub_down.Matname = mainname.MaterialName + "-" + subname.Name;
                                        if (current_sub.Nos_Of_Piece != 0)
                                        {
                                            sub_down.Piece = (int)current_sub.Nos_Of_Piece;

                                            lst_sub_down.Add(sub_down);
                                        }
                                    }
                                    else
                                    {
                                        foreach (var itemss in lst_sub_down)
                                        {
                                            if (itemss.Matname == mainname.MaterialName + "-" + subname.Name)
                                            {
                                                if (current_sub.Nos_Of_Piece != 0)
                                                {

                                                    itemss.Piece = itemss.Piece + (int)current_sub.Nos_Of_Piece;

                                                    //sub_down.Piece += (int)current_sub.Nos_Of_Piece;
                                                }
                                            }
                                            else
                                            {
                                                sub_down.Matname = mainname.MaterialName + "-" + subname.Name;
                                                if (current_sub.Nos_Of_Piece != 0)
                                                {
                                                    sub_down.Piece = (int)current_sub.Nos_Of_Piece;

                                                    lst_sub_down.Add(sub_down);
                                                }
                                            }
                                        }
                                    }

                                }
                                // }
                            }
                            else if (current_sub.ReturnPiece == "Yes")
                            {
                                MonthlyInvoice_Down_ReturnViewModel sub_returndown = new MonthlyInvoice_Down_ReturnViewModel();

                                var subname = (from m in pragatiEntities.SubMaterialTypes where m.SubMaterialTypeId == submatid.SubMaterialTypeId select m).FirstOrDefault();
                                var mainname = (from m in pragatiEntities.MaterialTypes where m.MaterialTypeId == submatid.MaterialTypeId select m).FirstOrDefault();
                                if (subname != null)
                                {
                                    // subname.Name = "";
                                    //total number of thread based on item
                                    if (lst_sub_return_down.Count == 0)
                                    {
                                        sub_returndown.ReturnMatname = mainname.MaterialName + "-" + subname.Name;
                                        if (current_sub.Nos_Of_Piece != 0)
                                        {
                                            sub_returndown.ReturnPiece = (int)current_sub.Nos_Of_Piece;

                                            lst_sub_return_down.Add(sub_returndown);
                                        }
                                    }
                                    else
                                    {
                                        foreach (var itemss in lst_sub_return_down)
                                        {
                                            if (itemss.ReturnMatname == mainname.MaterialName + "-" + subname.Name)
                                            {
                                                if (current_sub.Nos_Of_Piece != 0)
                                                {

                                                    itemss.ReturnPiece = itemss.ReturnPiece + (int)current_sub.Nos_Of_Piece;

                                                    //sub_down.Piece += (int)current_sub.Nos_Of_Piece;
                                                }
                                            }
                                            else
                                            {
                                                sub_returndown.ReturnMatname = mainname.MaterialName + "-" + subname.Name;
                                                if (current_sub.Nos_Of_Piece != 0)
                                                {
                                                    sub_returndown.ReturnPiece = (int)current_sub.Nos_Of_Piece;

                                                    lst_sub_return_down.Add(sub_returndown);
                                                }
                                            }
                                        }
                                    }

                                }
                            }
                            
                        }
                                               
                        list.Add(chalansViewModel);
                    }
                }
                monthlyInvoiceViewModel.TotalAmount = num;
                monthlyInvoiceViewModel.lstChalans = list;
                monthlyInvoiceViewModel.lst_totaldownItems = lst_sub_down;
                monthlyInvoiceViewModel.lst_totalReturndownItems = lst_sub_return_down;
                MonthlyInvoice monthlyInvoice = (
                    from a in pragatiEntities.MonthlyInvoices
                    where a.CustomerId == (int?)CustomerId && a.UserId == (int?)UserId && a.FromDateTime == Fromdate && a.ToDateTime == ToDate
                    select a).FirstOrDefault<MonthlyInvoice>();
                if (monthlyInvoice == null)
                {
                    MonthlyInvoice monthlyInvoice2 = new MonthlyInvoice();
                    monthlyInvoice2.CustomerId = new int?(CustomerId);
                    monthlyInvoice2.FromDateTime = Fromdate;
                    monthlyInvoice2.ToDateTime = ToDate;
                    monthlyInvoice2.TotalAmount = new float?(num);
                    monthlyInvoice2.UserId = new int?(UserId);
                    pragatiEntities.MonthlyInvoices.Add(monthlyInvoice2);
                    pragatiEntities.SaveChanges(); 
                }
                MonthlyInvoice monthlyInvoice3 = (
                    from a in pragatiEntities.MonthlyInvoices
                    where a.CustomerId == (int?)CustomerId && a.UserId == (int?)UserId && a.FromDateTime == Fromdate && a.ToDateTime == ToDate
                    select a).FirstOrDefault<MonthlyInvoice>();
                monthlyInvoiceViewModel.BillId = monthlyInvoice3.MonthlyInvoiceId;
                return View(monthlyInvoiceViewModel);
            }
            else
            {
                return RedirectToAction("LogIn");
            }

        }

        public ActionResult DailyInvoice()
        {
            if (Session["UserId"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("LogIn");
            }
        }

        public ActionResult MonthlyInvoice()
        {
            if (Session["UserId"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("LogIn");
            }
        }

        public ActionResult ChangeRate()
        {
            if (Session["UserId"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("LogIn");
            }
        }

        public ActionResult AddStockSample()
        {
            if (Session["UserId"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("LogIn");
            }
        }
    }
}
