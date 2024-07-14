using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;
using Bl;
using Bl.Interfaces;

using Microsoft.AspNetCore.Identity.Data;


namespace WebApplication1.Controller
{
    [Route("[controller]")]
    [ApiController]
    public class ResetPasswordController : ControllerBase
    {
        private IUser _BlUser;
        public ResetPasswordController(IUser u)
        {
            this._BlUser = u;
        }
       
        [Route("Reset")]
        [HttpPost]
        public async Task<IActionResult> reset([FromBody] ResetPasswordRequest request)
        {    //בדיקה האם המייל תקין
            if (_BlUser.CheckCorrect(request.email))
            {   // בודקת האם קיים כזה מייל בדטה בייס
                List<Dto.models.User> l = await _BlUser.ReadAsync(p => p.Email == request.email);
                Dto.models.User k = l.FirstOrDefault();
                if (k != null)
                {
                    string code = _BlUser.RandomaPassword();
                    if (await _BlUser.SendResetEmail(request.email, code))
                        return StatusCode(200, code);
                    return StatusCode(500, $"failed on the send");
                }
                return StatusCode(400, $"Email is not exist in the system");
            }
            return StatusCode(400, $"Email is not correct");
        }


    }
    public class ResetPasswordRequest
    {
        public string email { get; set; }
    }
}
