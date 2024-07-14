
using Microsoft.AspNetCore.Components.Web;
using Bl.Interfaces;
using Dal;
using Dto.models;
using DTO.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Dal.Models;
using Bl;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LeadController : ControllerBase
    {
        public ILead _leadService { get; set; }
        public LeadController(ILead leadService)
        {
            this._leadService = leadService;
        }

        [HttpGet("ReadAll")]
        public async Task<ActionResult<List<Leads>>> ReadAllAsync()
        {
           
            try
            {
                List<Leads> lead = await _leadService.ReadAllAsync();
                if (lead.Count == 0) { return NotFound("Lead  Not exsist "); }
                return lead;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString(), ex);
            }
        }

        [HttpGet("GetById")]
        public async Task<ActionResult<Leads>> GetByIdAsync([FromQuery(Name = "id")] int id)
        {
            try
            {
                ActionResult<Leads> l = await _leadService.GetByIdAsync(id);
                if (l != null)
                    return l;
                else
                    return NotFound("Lead not found!");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            
        }

        [HttpPost]
        public async Task<ActionResult<Leads>> CreateAsync([FromBody] Leads lead)
        {
            try
            {
                if (lead == null)
                    return BadRequest("Invalid body request!");
                Leads createdLead = await _leadService.CreateAsync(lead);
                return Ok(createdLead);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
           
        }

        [HttpPut]
        public async Task<ActionResult<Lead>> UpdateAsync([FromBody] Leads lead)
        {
            try
            {
                bool UpdateLead = await _leadService.UpdateAsync(lead);
                if (UpdateLead)
                    return Content(UpdateLead.ToString());
                else
                    return NotFound("Lead not found!");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
           
        }

        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteAsync([FromQuery(Name = "id")] int id)
        {
            try
            {
                if (id < 0) return BadRequest("Invalid id!");
                bool deleted = await _leadService.DeleteAsync(id);
                if (deleted)
                    return true;
                else
                    return NotFound("Lead not found!");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            
        }
    }
}
