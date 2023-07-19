using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Model.CustomerService;
using Test.Model.Models;

namespace Test.Model
{
    public class ServiceAdapter
    {
        public bool InsertCustomerService(CustomerLocal customer)
        {
            CustomerService.Service1Client serv = new CustomerService.Service1Client();
            CustomerService.Customer c = new CustomerService.Customer();
            c.Id = customer.Id;
            c.Nombre = customer.Nombre;
            c.Edad = customer.Edad;
            c.Email = customer.Email;
            return serv.InsertCustomer(c);
        }

        public bool UpdateCustomerService(CustomerLocal customer)
        {
            CustomerService.Service1Client serv = new CustomerService.Service1Client();
            CustomerService.Customer c = new CustomerService.Customer();
            c.Id = customer.Id;
            c.Nombre = customer.Nombre;
            c.Edad = customer.Edad;
            c.Email = customer.Email;
            return serv.UpdateCustomer(c);
        }
        public bool DeleteCustomerService(int IdCustomer)
        {
            CustomerService.Service1Client serv = new CustomerService.Service1Client();
            return serv.DeleteCustomer(IdCustomer);
        }
        public CustomerLocal[] GetAllCustomerService()
        {
            CustomerService.Service1Client serv = new CustomerService.Service1Client();
            CustomerService.Customer [] customer = serv.GetAllCustomer();
            return BindList(customer);
        }

        public CustomerLocal[] BindList(CustomerService.Customer [] customerList)
        {
            CustomerLocal [] result = new CustomerLocal[customerList.Length];
            for(int i = 0; i < customerList.Length; i++)
            {
                CustomerLocal customer = new CustomerLocal();
                customer.Id = customerList[i].Id;
                customer.Nombre = customerList[i].Nombre;
                customer.Edad = customerList[i].Edad;
                customer.Email = customerList[i].Email;
                result[i] = customer;
            }

            return result;
        }

    }
}
