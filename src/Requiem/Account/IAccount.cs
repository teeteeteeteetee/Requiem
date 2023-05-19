using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Requiem.Account
{
    public interface IAccount
    {
        string Username { get; }
        string Password { get; }
        string Summoner { get; }
        int Level { get; }
        int IconId { get; }
        int BlueEssense { get; }
        int RiotPoints { get; }
        Ranks Rank { get; }
        bool IsBanned { get; }
        DateTime Decay { get; }
        DateTime LastActivity { get; }

    }
}
