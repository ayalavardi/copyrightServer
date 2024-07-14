using Dto.models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using Bl.Interfaces;
using Dto.models;
namespace WebApplication1.Controller
{
    [Route("[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITasks _taskService;

        public TaskController(ITasks taskService)
        {
            _taskService = taskService;
        }
        [HttpPost]
        public async Task<Tasks> CreateAsync([FromBody] Tasks task)
        {
            if (task == null)
            {
                return null;
            }

            try
            {
                var createdTask = await _taskService.CreateAsync(task);
                return createdTask;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            return Ok(await _taskService.DeleteAsync(id));
        }


        [HttpGet]
        public async Task<ActionResult<List<Tasks>>> ReadAll()
        {
            var tasks = await _taskService.ReadAllAsync();
            return Ok(tasks);
        }


        [HttpPut()]
        public ActionResult Update([FromBody] Tasks task)
        {
            if (task == null)
            {
                return BadRequest();
            }
            try
            {
                return Ok(_taskService.UpdateAsync(task));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<ActionResult<Tasks>> GetByIdAsync([FromQuery(Name = "id")] int id)
        {
            try
            {
                var tasks = await _taskService.GetById(id);
                return Ok(tasks);
            }
            catch
            {
                return null;
            }
        }
        [HttpGet]
        [Route("ReadAllStatus")]
        public async Task<List<StatusCodeProject>> ReadAllStatus()
        {
            try
            {
                return await _taskService.ReadAllStatusAsync();
            }
            catch
            {
                return null;
            }
        }
        [HttpGet]
        [Route("ReadAllPriority")]
        public async Task<List<PriorityCode>> ReadAllPriority()
        {
            try
            {
                return await _taskService.ReadAllPriorityAsync();
            }
            catch
            {
                return null;
            }
        }
    }
}
