﻿using HKCCinemas.Interfaces;
using HKCCinemas.Models;
using Microsoft.AspNetCore.Mvc;


namespace HKCCinemas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SendMailController : ControllerBase
    {
        public readonly ISendMailService _sendMailService;
        public SendMailController(ISendMailService sendMailService) {
            _sendMailService = sendMailService;
        }
        [HttpPost]
        public async Task<IActionResult> sendMail([FromForm] MailContent mailContent )
        {
            if (await _sendMailService.SendMail(mailContent))
            {
                return Ok("Gửi email thành công");
            }
            else { return BadRequest("Gửi email thất bại"); }
        }

       

        
    }
}
