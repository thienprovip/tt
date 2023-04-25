using System;
using ECommerce.API.DataAccess;
using ECommerce.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Cors;
using ECommerce.API.DataAccess.Interface;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeController : ControllerBase
    {
        readonly IDe dataAccess;
    
        public DeController(IDe dataAccess, IConfiguration configuration)
        {
            this.dataAccess = dataAccess;
            
        }
        //[HttpGet("GetProductReviews/{productId}")]
        //public IActionResult GetProductReviews(int productId)
        //{
        //    var result = dataAccess.GetProductReviews(productId);
        //    return Ok(result);
        //}
        [HttpPut("Update")]
        public IActionResult UpdateDe(De de)
        {
            var result = dataAccess.UpdateDe(de);
            return Ok(result ? "updated" : "update fail");
        }
        [HttpPost("InsertDe")]
        public IActionResult InsertDe(De de)
        {
            var result = dataAccess.InsertDe(de);
            return Ok(result ? "inserted" : "inserted fail");
        }
        [HttpDelete("Delete")]
        public IActionResult Delete(int id)
        {
            var result=dataAccess.Delete(id);
            return Ok(result ? "Deleted" : "Delete fail");
        }
    }
}

