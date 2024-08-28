using Example.Model;
using Example.Services;
using Microsoft.AspNetCore.Mvc;


namespace Example.WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CustomerController : ControllerBase
	{
		private readonly ICustomerService _customerService;
		public CustomerController(ICustomerService customerService)
		{
			_customerService = customerService;
		}
		// GET: api/<CustomerController>
		[HttpGet]
		public IEnumerable<Customer> GetAll()
		{
			return _customerService.GetAllCustomers();
		}

		// GET api/<CustomerController>/5
		[HttpGet("{id}")]
		public Customer Get(Guid id)
		{
			return _customerService.GetCustomerById(id);
		}

		[HttpGet("{filter}")]
		public Customer Get(string filter)
		{
			return _customerService.GetCustomerByFilter(filter);
		}

		// POST api/<CustomerController>
		[HttpPost]
		public void Post([FromBody] Customer model)
		{
			_customerService.UpdateCustomer(model);
		}


		// DELETE api/<CustomerController>/5
		[HttpDelete("{id}")]
		public void Delete(Guid id)
		{
			_customerService.DeleteCustomer(id);
		}
	}
}
