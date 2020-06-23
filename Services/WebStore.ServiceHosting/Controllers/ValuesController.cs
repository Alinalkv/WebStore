using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebStore.ServiceHosting.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private static readonly List<string> _Values = Enumerable
            .Range(1, 10)
            .Select(i => $"Value - {i}")
            .ToList();

        [HttpGet]
        public ActionResult<IEnumerable<string>> Get() => _Values;

        public ActionResult<string> Get(int id)
        {
            if (id < 0)
                return BadRequest();
            if (id >= _Values.Count)
                return NotFound();
            return _Values[id];
        }
    }
}
