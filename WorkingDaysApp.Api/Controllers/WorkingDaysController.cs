using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WorkingDaysApp.Data;

namespace WorkingDaysApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkingDaysController : ControllerBase
    {
        private IWorkingdays _workingdays;

        public WorkingDaysController(IWorkingdays workingdays)
        {
            _workingdays = workingdays ?? throw new ArgumentNullException(nameof(workingdays));
        }

        [HttpGet]
        public ActionResult<int> Get([FromQuery] string fromDate, [FromQuery] string toDate)
        {
            var from = DateTime.ParseExact(fromDate, "d/M/yyyy", CultureInfo.InvariantCulture);
            var to = DateTime.ParseExact(toDate, "d/M/yyyy", CultureInfo.InvariantCulture);

            return Ok(_workingdays.GetWorkingDays(from, to));
        }
    }
}
