using System.Web.Http;

namespace Api.Security.One.Controllers
{
    public class ProductsController : ApiController
    {
        public IHttpActionResult GetAllProducts()
        {
            return Ok("Hier die Liste der Produkte");
        }
    }
}
