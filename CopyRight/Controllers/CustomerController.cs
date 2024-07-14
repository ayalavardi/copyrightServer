using Bl.Interfaces;using Dto.models;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        public ICustomer _customerService { get; set; }
        public CustomerController(ICustomer customerService)
        {
            this._customerService = customerService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Customers>>> ReadAllAsync()
        {
            try
            {
                List<Customers> AllCustomers = await _customerService.ReadAllAsync();
                return Ok(AllCustomers);
            }
            catch (Exception ex)
            {
                throw new Exception
                    (ex.Message, ex);
            }

        }
        [HttpGet("GetById")]
        public async Task<ActionResult<Customers>> GetByIdAsync([FromQuery(Name = "custometId")] int customerId)
        {
            try
            {
                Customers customer = await _customerService.GetByIdAsync(customerId);
                return Ok(customer);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);                 
            }


        }
        [HttpPost]
        public async Task<ActionResult<Customers>> CreateAsync([FromBody] Customers newCustomer)
        {
            try
            {
                Customers customer = await _customerService.CreateAsync(newCustomer);
                return Ok(customer);
            }
            catch (Exception ex)
            {
                throw new Exception (ex.Message, ex);                 
            }
        }
        [HttpPut]
        public async Task<ActionResult<bool>> UpdateAsync([FromBody] Customers editCustomer)
        {
            try
            {
                bool editCustomerSuccess = await _customerService.UpdateAsync(editCustomer);
                return Ok(editCustomerSuccess);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        [HttpDelete("DeletByEmail")]
        public async Task<ActionResult<bool>> DeletByEmailAsync([FromQuery(Name = "customerEmail")] string customerEmail)
        {
            try
            {
                bool deleteCustomerSuccess = await _customerService.DeletByEmailAsync(customerEmail);
                return Ok(deleteCustomerSuccess);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }


        }
        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteByIdAsync([FromQuery(Name = "customerId")] int customerId)
        {
            try
            {
                bool deleteCustomerSuccess = await _customerService.DeleteAsync(customerId);
                return Ok(deleteCustomerSuccess);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message  , ex);
                    
            }


        }
        [HttpGet("GetAllStatus")]
       public async Task<List<StatusCodeUser>> getAllStatusCodeUser()
        {
            try
            {
                return await _customerService.GetCustomerStatusAsync();
            }
            catch
            {
                return null;
            }
        }
        }
}