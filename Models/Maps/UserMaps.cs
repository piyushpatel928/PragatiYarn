using PragatiYarn.Models.DB;
using PragatiYarn.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PragatiYarn.Models.Maps
{
    public class UserMaps
    {
        public User MapLogInVMToUser(LoginViewModel lvm)
        {
            return new User
            {
                Username = lvm.Username,
                Password = lvm.Password
            };
        }
        public Customer MapNewCustomerVMToCustomer(AddCustomerViewModel addcustvm)
        {
            return new Customer
            {
                CustomerSurname = addcustvm.CustomerSurName,
                CustomerName = addcustvm.CustomerName,
                MobileNo = new decimal?(decimal.Parse(addcustvm.MobileNumber)),
                Address = addcustvm.Address,
                CustomerFirmname = addcustvm.FirmName,
                CustomerImageName = addcustvm.CustomerImageName,
                CustomerImagePath = addcustvm.CustomerImagePath,
                UserId = addcustvm.UserId
            };
        }
        public List<AddCustomerViewModel> MaplstCustomerTolstCustomerVM(List<Customer> lstcust)
        {
            List<AddCustomerViewModel> listacvm = new List<AddCustomerViewModel>();
            if (lstcust.Count > 0)
            {
                foreach (Customer current in lstcust)
                {
                    AddCustomerViewModel acvm = new AddCustomerViewModel();
                    acvm.customerId = current.CustomerId;

                    acvm.CustomerName = current.CustomerName;

                    if (current.CustomerSurname == null)
                    {
                        acvm.CustomerSurName = "";
                    }
                    else
                    {
                        acvm.CustomerSurName = current.CustomerSurname;
                    }
                    if (current.CustomerFirmname == null)
                    {
                        acvm.FirmName = "";
                    }
                    else
                    {
                        acvm.FirmName = current.CustomerFirmname;
                    }


                    acvm.MobileNumber = current.MobileNo.ToString();
                    acvm.Address = current.Address;
                    acvm.CustomerImageName = current.CustomerImageName;
                    acvm.CustomerImagePath = current.CustomerImagePath;
                    listacvm.Add(acvm);
                }
            }
            return listacvm;
        }
        public AddCustomerViewModel MapCustomerToCustomerVM(Customer lstcust)
        {
            AddCustomerViewModel addCustomerViewModel = new AddCustomerViewModel();
            AddCustomerViewModel result;
            if (lstcust != null)
            {
                addCustomerViewModel.customerId = lstcust.CustomerId;
                addCustomerViewModel.CustomerSurName = lstcust.CustomerSurname;
                addCustomerViewModel.CustomerName = lstcust.CustomerName;
                addCustomerViewModel.FirmName = lstcust.CustomerFirmname;
                if (lstcust.CustomerFirmname == null)
                {
                    addCustomerViewModel.FirmName = "";
                }
                else
                {
                    addCustomerViewModel.FirmName = lstcust.CustomerFirmname;
                }
                addCustomerViewModel.MobileNumber = lstcust.MobileNo.ToString();
                addCustomerViewModel.Address = lstcust.Address;
                addCustomerViewModel.CustomerImageName = lstcust.CustomerImageName;
                addCustomerViewModel.CustomerImagePath = lstcust.CustomerImagePath;
                result = addCustomerViewModel;
            }
            else
            {
                result = null;
            }
            return result;
        }
        public AllMaterial MapAllMaterialVmToAllMaterial(AddMaterialViewModel addvm)
        {
            AllMaterial allMaterial = new AllMaterial();
            allMaterial.MaterialCode = addvm.MaterialCode;
            allMaterial.Datetime = new DateTime?(DateTime.Now);
            if (addvm.MaterialImage != null)
            {
                allMaterial.MaterialImagePath = addvm.MaterialImagePath;
            }
            else
            {
                allMaterial.MaterialImagePath = "No Image";
            }
            allMaterial.MaterialTypeId = new int?(addvm.MaterialId);
            if (addvm.MaterialId == 3)
            {
                allMaterial.Sub_KasabId = new int?(addvm.SubMaterialId);
            }
            if (addvm.MaterialId == 5)
            {
                allMaterial.Price = new double?((double)addvm.Price);
            }
            if (addvm.MaterialId == 6)
            {
                allMaterial.Price = new double?((double)addvm.Price);
            }
            if (addvm.MaterialId == 7)
            {
                allMaterial.Price = new double?((double)addvm.Price);
            }
            return allMaterial;
        }
        public AllMaterial MapEditMaterialVmToEditMaterial(EditMaterialViewModel addvm)
        {
            AllMaterial allMaterial = new AllMaterial();
            allMaterial.MaterialCode = addvm.MaterialCode;
            allMaterial.Datetime = new DateTime?(DateTime.Now);
            allMaterial.MaterialId = addvm.MaterialId;
            if (addvm.MaterialImage != null)
            {
                allMaterial.MaterialImagePath = addvm.MaterialImagePath;
            }

            return allMaterial;
        }
        public User MapNewUserVMToUser(AddUserViewModel addusr)
        {
            return new User
            {
                Username = addusr.Username,
                Password = addusr.Password
            };
        }

    }
}
