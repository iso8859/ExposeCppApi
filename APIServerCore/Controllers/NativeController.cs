using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace APIServerCore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NativeController : ControllerBase
    {
        private readonly ILogger<NativeController> _logger;

        public NativeController(ILogger<NativeController> logger)
        {
            _logger = logger;
        }

        delegate int AddDelegate(int a, int b);

        [HttpGet("{a}/{b}")]
        public IActionResult Get([FromRoute]int a, [FromRoute]int b)
        {
            IntPtr ptr = NativeLibrary.Load("Dll1.dll");
            if (ptr != IntPtr.Zero)
            {
                if (NativeLibrary.TryGetExport(ptr, "Add", out IntPtr AddPtr))
                {
                    var _add = (AddDelegate)Marshal.GetDelegateForFunctionPointer(AddPtr, typeof(AddDelegate));
                    return Ok(new { a = a, b = b, result = _add(a, b) });
                }
            }
            return NotFound();
        }
    }
}
