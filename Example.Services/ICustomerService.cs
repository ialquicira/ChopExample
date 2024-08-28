using Example.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Services
{
	public interface ICustomerService
	{
		public void CreateCustomer(Customer domain);
		public void DeleteCustomer(Guid CustomerId);
		public IList<Customer> GetAllCustomers();
		public Customer GetCustomerById(Guid CustomerId);
		public Customer GetCustomerByFilter(string filter);
		public void UpdateCustomer(Customer domain);
	}
}
