using System;
using ECommerce.API.DataAccess;
using ECommerce.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Cors;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GiangVienController : ControllerBase
    {
        readonly IGiangVien dataAccess;
        public GiangVienController(IGiangVien dataAccess, IConfiguration configuration)
        {
            this.dataAccess = dataAccess;
            
        }

        //[HttpGet("GetProductReviews/{productId}")]
        //public IActionResult GetProductReviews(int productId)
        //{
        //    var result = dataAccess.GetProductReviews(productId);
        //    return Ok(result);
        //}
        [HttpGet("GetGiangVien/id")]
        public IActionResult GetGiangVien(int id)
        {
            var result = dataAccess.GetGiangVien(id);
            return Ok(result);
        }
        [HttpPut("Update")]
        public IActionResult Update(GiangVien gv)
        {
            var result = dataAccess.Update(gv);
            return Ok(result ? "updated" : "update fail");
        }

        [HttpDelete("Delete")]
        public IActionResult Delete(int id)
        {
            var result = dataAccess.Delete(id);
            return Ok(result ? "deleted" : "delete fail");
        }

        [HttpPost("InsertTeacher")]
        public IActionResult InsertGiangVien(GiangVien gv)
        {
            var result = dataAccess.InsertGiangVien(gv);
            return Ok(result ? "inserted" : "inserted fail");
        }
    }
}

