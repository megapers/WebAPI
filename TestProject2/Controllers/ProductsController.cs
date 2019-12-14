using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DeLoachAero.WebApi;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace TestProject2.Controllers
{
    [RoutePrefix("api/products")]
    public class ProductsController : ApiController
    {
        //Assignment 1
        [HttpGet, Route("accounts/{accountId:validAccount}")]
        public string GetAccountWithConstraint(string accountId)
        {
            return "Account: " + accountId;
        }

        [JsonConverter(typeof(StringEnumConverter))]
        public enum Widgets
        {
            Bolt,
            Screw,
            Nut,
            Motor
        };

        //GET: Products/widget/xxx
        [HttpGet, Route("widget/{widget:enum(TestProject2.Controllers.ProductsController+Widgets)}")]
        public string GetProductsWithWidget(Widgets widget)
        {
            return "widget-" + widget.ToString();
        }

        [HttpGet, Route("status/{status:alpha=pending}/{id:int=5}")]
        public string GetProductsWithStatus(string status, int id)
        {
            return String.IsNullOrEmpty(status) ? "NULL" : status;
        }

        // GET: api/Products
        [HttpGet, Route("")]
        [Route("~/prods")]
        public IEnumerable<string> ReturnAllTheProducts()
        {
            return new string[] { "product1", "product2" };
        }

        // GET: api/Products/5
        [HttpGet, Route("{id:int:range(1000, 3000)}", Name = "GetById")]
        public string GetProduct(int id)
        {
            return "product";
        }

        // POST: api/Products
        [HttpPost, Route("")]
        public void CreateProduct([FromBody]string value)
        {
        }
 
        [HttpPost, Route("{id:int:range(1000, 3000)}")]
        public HttpResponseMessage CreateProduct([FromBody]int prodId)
        {
            //do some create logic, then...

            var response = Request.CreateResponse(HttpStatusCode.Created);

            //Create the self-referencing link to the new item and set the response Location headed
            string uri = Url.Link("GetBuId", new {id = prodId });
            response.Headers.Location = new Uri(uri);
            return response;

        }


        // PUT: api/Products/5
        [HttpPut, Route("{id:int:range(1000, 3000)}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Products/5
        [HttpDelete, Route("{id:int:range(1000, 3000)}")]
        public void Delete(int id)
        {
        }

        // GET: api/Products/5/orders/custid
        [HttpGet, Route("{id}/orders/{custid}")]
        public string Get(int id, string custid)
        {
            return "product-orders-" + custid;
        }
    }
}
