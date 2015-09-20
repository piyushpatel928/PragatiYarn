using PragatiYarn.Models.DB;
using PragatiYarn.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PragatiYarn.Models.Abstract
{
   public interface IUserRepository
    {
        int CheckUserLogin(User usr);
        int InsertNewCustomer(Customer cust);
        List<Customer> GetAllCustomer();
        int insertNewMaterial(AllMaterial allmat);
        int InsertNewUser(User usr);
        int IsUsernameAvailable(string username);
        Customer GetCustomerById(int custid);
        int UpdateCustomer(AddCustomerViewModel acvm, int CustId);
        int DeleteCustomer(int id);
        CustomerCreditViewModel GetAllCustomersCreditAmtByUserId(int userid);
        
    }
}
