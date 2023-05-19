using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Requiem.Templates
{
    internal class ConfigTemplate
    {
        public string? RiotPath { get; set; } = @"C:\Riot Games\Riot Client\RiotClientServices.exe";
        public bool IsTray { get; set; } = true;
        public bool IsCloseExit { get; set; } = true;
        public bool IsDetailed { get; set; } = false;
        public bool IsMultiLauncher { get; set; } = false;
    }
}
