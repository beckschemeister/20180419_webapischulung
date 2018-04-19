using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Api.AdventureWorks2012.Productmanagement.Models;

namespace Api.AdventureWorks2012.Productmanagement.Controllers
{
    public class ProductController : ApiController
    {
        private readonly ProductDbContext _productDbContext;

        public ProductController()
        {
            _productDbContext = new ProductDbContext();
        }

        /// <summary>
        /// 1. Möglichkeit mti HttpResponseMessage
        /// </summary>
        /// <returns></returns>
        public HttpResponseMessage GetAllProducts()
        {
            //return new HttpResponseMessage(HttpStatusCode.Forbidden);
            //return new HttpResponseMessage(HttpStatusCode.NotFound);
            return new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent("Alle Produkte abfragen") };
            
        }

        /// <summary>
        /// 2. Möglichkeit mit IHttpActionResult
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IHttpActionResult GetProduct(int id)
        {
            // Success
            return StatusCode(HttpStatusCode.NoContent); // 204
            return Created(new Uri(Request.RequestUri.ToString()), new { }); // 201
            return Ok("liefere Produkt mit Id " + id); // 200

            // Client errors;
            return NotFound();
            return BadRequest();
            return Unauthorized();

            // Userdefinied: https://stackoverflow.com/questions/28343117/creating-new-ihttpactionresult-action-result-methods?utm_medium=organic&utm_source=google_rich_qa&utm_campaign=google_rich_qa


        }
    }
}
