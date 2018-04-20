using System.Linq;
using System.Web.Http;

namespace Api.Security.One.Controllers
{
    public class ProductsController : ApiController
    {
        // Verwendung von User-Secret
        private const string SECRET_KEY = "top-secret";
        private const string Password = "123";
        
        public IHttpActionResult GetAllProducts()
        {
            if (!Request.Headers.Contains(SECRET_KEY) ||
                !Request.Headers.GetValues(SECRET_KEY).First().Equals(Password))
            {
                return Unauthorized();
            }

            return Ok("Hier die Liste der Produkte");
        }
    }
}
