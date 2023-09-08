using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using MyProject.Data;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class EMailController : ControllerBase
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public EMailController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        
        [HttpPost]
        public async Task<IActionResult> EMailAdd(EmailModel emailModel)
        {
            try
            {
                var Email = new EMailData
                {
                    EMail = emailModel.EMail
                };
                _applicationDbContext.EmailDatas.Add(Email);
                await _applicationDbContext.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
           
        }
    }
}