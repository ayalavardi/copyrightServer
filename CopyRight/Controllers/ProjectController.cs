

using Dto.models;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class projectsController : ControllerBase
    {
        private Bl.Interfaces.ITasks _BlTasks;
        private Bl.Interfaces.IProject _BlProject;
        public projectsController(Bl.Interfaces.IProject p, Bl.Interfaces.ITasks t)
        {
            this._BlProject = p;
            this._BlTasks = t;
        }

        [HttpPost]
        public async Task<IActionResult> Add(Projects newProject)
        {

            if (await _BlProject.IsOnTheDB(newProject.CustomerId))
            {
                if (_BlProject.IsOnlyLetters(newProject.Name))
                {
                    if (_BlProject.IsCorrectDates(newProject.StartDate, newProject.EndDate))
                    {
                        if (await _BlProject.CreateAsync(newProject) != null)
                        {
                            return StatusCode(200, newProject);
                        }
                        return BadRequest(500);
                    }
                    return StatusCode(400, $"the startDate is  after the endDate");
                }
                return StatusCode(400, $"the name isnt valid");
            }
            return StatusCode(400, $"This customer not exist on db");


        }

        [HttpDelete]
        public async Task<ActionResult<Projects>> Delete([FromQuery(Name = "id")] int id)
        {
            if (id < 0) return BadRequest("Invalid id!");
            bool deleted = await _BlProject.DeleteAsync(id);
            if (deleted)
                return Content(deleted.ToString());
            else
                return NotFound("project not found!");
        }

        [HttpGet]
        public async Task<ActionResult<Projects[]>> GetAll()
        {
            List<Projects> p = await _BlProject.ReadAllAsync();
            return Ok(p);
        }
        [Route("getAllTasks")]
        [HttpGet]
        public async Task<ActionResult<Tasks>> GetAllTasks([FromQuery(Name = "id")] int id)
        {
           List<Tasks>p = await _BlTasks.ReadAsync(o=>o.Project.ProjectId==id);
            return Ok(p);

           
        }
      
        [HttpPut]
        public async Task<bool> UpdateProject(Dto.models.Projects p)
        {
            return await _BlProject.UpdateAsync(p);
        }
    }
}