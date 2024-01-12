using Microsoft.AspNetCore.Mvc;
using CustomerCrud.Core;
using CustomerCrud.Requests;
using CustomerCrud.Repositories;

namespace CustomerCrud.Controllers;

[ApiController]
[Route("/controller")]
public class CustomerController: ControllerBase
{
  private readonly ICustomerRepository _repository;

  public CustomerController(ICustomerRepository CustomerRepository)
  {
    _repository = CustomerRepository;
  }
  [HttpGet] 
  public ActionResult GetALl() {
    var customer = _repository.GetAll();
    return Ok(customer);
  }

  [HttpGet("{id}")] 
  public ActionResult GetById(int id) {
    var customer = _repository.GetById(id);
    if(customer == null) {
     return NotFound("Customer not found!");
    } else {

    return Ok(customer);
    }
  }
}
