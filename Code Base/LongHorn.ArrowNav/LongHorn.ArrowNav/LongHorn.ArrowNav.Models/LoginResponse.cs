using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LongHorn.ArrowNav.Models
{
    public class LoginResponse
    {
        public string Message { get; set; } = "";
        public bool IsAuthorized { get; set; } = false;
    }
}
