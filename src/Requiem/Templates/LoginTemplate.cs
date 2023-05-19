using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Requiem.Templates
{
    internal class LoginTemplate
    {
        public string? username { get; set; }
        public string? password { get; set; }
        public bool persistLogin { get; set; }
    }
}
