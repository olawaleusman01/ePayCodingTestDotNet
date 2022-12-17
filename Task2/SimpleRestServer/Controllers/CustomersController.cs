using Microsoft.AspNetCore.Mvc;
using SimpleRestServer.Data;
using SimpleRestServer.Models;

namespace SimpleRestServer.Controllers;

[ApiController]
[Route("[controller]")]
public class CustomersController : ControllerBase
{
    private readonly ICustomerRepository _customerRepository;

    public CustomersController(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Customer>> Get()
    {
        return Ok(_customerRepository.GetCustomers());
    }

    [HttpPost]
    public ActionResult<IEnumerable<Customer>> Post([FromBody] List<Customer> customers)
    {
        try
        {
            if (customers == null || customers.Count == 0)
            {
                return BadRequest("Customer record is required");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Select(d => d.Value.Errors).Where(e => e.Count > 0));
            }

            _customerRepository.InsertCustomers(customers);
            return Ok();
        }
        catch
        {
            return BadRequest("Could not create item");
        }
    }
}
