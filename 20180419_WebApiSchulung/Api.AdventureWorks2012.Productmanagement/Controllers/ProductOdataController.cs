using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.OData;
using System.Web.Http.OData.Routing;
using Api.AdventureWorks2012.Productmanagement.Models;

namespace Api.AdventureWorks2012.Productmanagement.Controllers
{
    /*
    Die Klasse "WebApiConfig" erfordert ggf. weitere Änderungen zum Hinzufügen einer Route für diesen Controller. Führen Sie diese Anweisungen in der Methode "Register" der Klasse "WebApiConfig" ordnungsgemäß zusammen. Beachten Sie, dass für OData-URLs zwischen Groß- und Kleinschreibung unterschieden wird.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using Api.AdventureWorks2012.Productmanagement.Models;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<Product>("ProductOdata");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class ProductOdataController : ODataController
    {
        private ProductDbContext db = new ProductDbContext();

        // GET: odata/ProductOdata
        [EnableQuery]
        public IQueryable<Product> GetProductOdata()
        {
            return db.Product;
        }

        // GET: odata/ProductOdata(5)
        [EnableQuery]
        public SingleResult<Product> GetProduct([FromODataUri] int key)
        {
            return SingleResult.Create(db.Product.Where(product => product.ProductID == key));
        }

        // PUT: odata/ProductOdata(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<Product> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Product product = await db.Product.FindAsync(key);
            if (product == null)
            {
                return NotFound();
            }

            patch.Put(product);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(product);
        }

        // POST: odata/ProductOdata
        public async Task<IHttpActionResult> Post(Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Product.Add(product);
            await db.SaveChangesAsync();

            return Created(product);
        }

        // PATCH: odata/ProductOdata(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<Product> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Product product = await db.Product.FindAsync(key);
            if (product == null)
            {
                return NotFound();
            }

            patch.Patch(product);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(product);
        }

        // DELETE: odata/ProductOdata(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            Product product = await db.Product.FindAsync(key);
            if (product == null)
            {
                return NotFound();
            }

            db.Product.Remove(product);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductExists(int key)
        {
            return db.Product.Count(e => e.ProductID == key) > 0;
        }
    }
}
