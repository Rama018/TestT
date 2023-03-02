using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using TestT.Models;


namespace TestT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminApiController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CompanyTest>>> Get()
        {
            List<CompanyTest> str = CompanyTest.GetAll();
            return Ok(str);
        }
        //
        [HttpGet("{PNum}/{PSize}")]
        public IActionResult GetDataPaging(int PNum,int PSize)
        {
            List<CompanyTest> data = CompanyTest.Allpagin(PNum, PSize);
            int totalItem = data.Count;
            return Ok(new { data, totalItem });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CompanyTest>> Get(int id)
        {
            CompanyTest list = CompanyTest.getID(id);
            if (list == null)
            {
                return NotFound("There are no Item id for " + id);
            }
            return Ok(list);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CompanyTest test)
        {
            bool effected = CompanyTest.Create2(test);
            return Ok(effected);

        }

        [HttpPut("{id}")]
        public ActionResult Put(int id)
        {
            CompanyTest test = CompanyTest.UpdateID2(id);
            if(test == null)
            {
                return NotFound();
            
            }
            else
                return Ok(test);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            bool isRemoved = CompanyTest.DeleteByID(id);
            return Ok(isRemoved);
        }
    }
}
