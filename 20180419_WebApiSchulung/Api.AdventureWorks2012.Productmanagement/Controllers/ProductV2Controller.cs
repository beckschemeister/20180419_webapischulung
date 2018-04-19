using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Api.AdventureWorks2012.Productmanagement.Filters;
using Api.AdventureWorks2012.Productmanagement.Models;
using Api.AdventureWorks2012.Productmanagement.ViewModels;
using AutoMapper;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Web.Http.Cors;

namespace Api.AdventureWorks2012.Productmanagement.Controllers
{
    //[EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/v2/Product")]
    public class ProductV2Controller : ApiController
    {
        private readonly ProductDbContext _productDbContext;

        public ProductV2Controller()
        {
            _productDbContext = new ProductDbContext();
        }

        /// <summary>
        /// Neuere verbesserte Version, die wirklich alles liefert.
        /// </summary>
        [Route("")]
        public async Task<IHttpActionResult> GetAllProducts()
        {
            var products = await _productDbContext.Product.ToListAsync(); /* .ToListAsync() braucht using System.Data.Entity; */

            return Ok(products);
        }
    }
}
