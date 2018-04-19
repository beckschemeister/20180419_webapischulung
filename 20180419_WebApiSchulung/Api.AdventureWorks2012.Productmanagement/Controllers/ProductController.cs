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
    [RoutePrefix("katalog")]
    public class ProductController : ApiController
    {
        private readonly ProductDbContext _productDbContext;

        public ProductController()
        {
            _productDbContext = new ProductDbContext();
        }

        /// <summary>
        /// 1. Möglichkeit mit HttpResponseMessage zurückgeben.
        /// Wird in der Schulung nicht verwendet, geht aber auch.
        /// </summary>
        /// <returns></returns>
        //public HttpResponseMessage GetAllProducts()
        //{
        //    //return new HttpResponseMessage(HttpStatusCode.Forbidden);
        //    //return new HttpResponseMessage(HttpStatusCode.NotFound);
        //    return new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent("Alle Produkte abfragen") };
        //}

        /// <summary>
        /// Asynchrone Methode
        /// </summary>
        /// <returns></returns>
        public async Task<IHttpActionResult> GetAllProducts()
        {
            var products = await _productDbContext.Product.Take(10).ToListAsync(); /* .ToListAsync() braucht using System.Data.Entity; */

            return Ok(products);
        }

        /// <summary>
        /// 2. Möglichkeit mit IHttpActionResult zurückgeben.
        /// Diese Möglichkeit verfolgen wir in der Schulung
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("produkt/{id:int:min(1)}")]
        public IHttpActionResult GetProduct(int id)
        {
            var products = _productDbContext.Product.AsQueryable();

            var product = products.FirstOrDefault(p => p.ProductID.Equals(id));

            if (product == null)
                return NotFound();

            // Siehe Beschreibung des AutoMappers in CreateProduct 
            var productViewModel = Mapper.Map<Product, ProductViewModel>(product);

            return Ok(productViewModel);

            // Success
            return Ok("liefere Produkt mit Id " + id); // 200
            return Created(new Uri(Request.RequestUri.ToString()), new { }); // 201
            return StatusCode(HttpStatusCode.NoContent); // 204

            // Client errors;
            return NotFound();
            return BadRequest();
            return Unauthorized();

            // Userdefinied: https://stackoverflow.com/questions/28343117/creating-new-ihttpactionresult-action-result-methods?utm_medium=organic&utm_source=google_rich_qa&utm_campaign=google_rich_qa
        }

        // POST api/Product
        [HttpPost] // <- damit der Methodenname mit Create und nicht mit POST beginnen kann. (Swagger kapiert das dankenswerterweise so).
        public IHttpActionResult CreateProduct(ProductViewModel productViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                // Einige Felder braucht die Datenbank, die nicht im ProductViewModel übergeben wurden... das muss umgangen werden!
                // Das lösen wir mit dem Automapper von Jimmy Bogard (NuGet-Paket).
                // 1. über NuGet einbinden.
                // 2. Global.asax.cs => Mapper initialisieren.
                // 3. class MappingProfile in App_Start hinterlegen (dort ist dann das Mapping konfiguriert)
                // 4. Mapping hier in der Klasse verwenden
                var product = Mapper.Map<ProductViewModel, Product>(productViewModel);

                _productDbContext.Product.Add(product);
                _productDbContext.SaveChanges();

                productViewModel.ProductID = product.ProductID;

                // HttpResponse hat dann z.B. -> "location": "http://localhost:3987/api/Product/1011" -> Uri im Created, die zum angelegten Datensatz führt!
                return Created(new Uri(Request.RequestUri + "/" + productViewModel.ProductID), productViewModel);
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception(ex.Message));
            }
        }

        [HttpPut]
        public IHttpActionResult UpdateProduct(int id, ProductViewModel productViewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var products = _productDbContext.Product.AsQueryable();

            var product = products.FirstOrDefault(p => p.ProductID.Equals(id));

            if (product == null)
                return NotFound();

            Mapper.Map(productViewModel, product);

            _productDbContext.SaveChanges();

            return Ok();
        }

        //[CustomExceptionFilter] // oder einzelne Methoden mit dem ActionFilter versehen.
        [HttpDelete]
        public IHttpActionResult DeleteProduct(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            throw new Exception("Wir wollen mal so tun, als ob diese Methode nicht funktionieren würde!");

            var products = _productDbContext.Product.AsQueryable();

            var product = products.FirstOrDefault(p => p.ProductID.Equals(id));

            if (product == null)
                return NotFound();

            _productDbContext.Product.Remove(product);
            _productDbContext.SaveChanges();

            return Ok();
        }

        ~ProductController()
        {
            _productDbContext.Dispose();// Verbindung mit DB schließen (wird aber nicht zwingend gebraucht).
        }
    }
}
