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

  [HttpPost] 
  public ActionResult Create(CustomerRequest request) {
    var id = _repository.GetNextIdValue();
    var customer = new Customer(id, request);
    _repository.Create(customer);
    return CreatedAtAction("GetById", new { id = customer.Id }, customer);
  }

  [HttpPut("{id}")]
  public ActionResult Create(CustomerRequest request, int id) {
    var update = _repository.Update(id, new {
      Name = request.Name,
      CPF = request.CPF,
      Transactions = request.Transactions,
      UpdatedAt = DateTime.Now
    });

    if(!update) return NotFound("Customer not found");
    return Ok($"Customer {id} updated");
  }

  [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        var diDelete = _repository.Delete(id);

        if (!diDelete) return NotFound("Customer not found");

        return NoContent();
    }

}
