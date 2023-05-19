using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Requiem.Account
{
    public class Account : IAccount
    {
        public Account()
        {

        }
        public string Username => throw new NotImplementedException();

        public string Password => throw new NotImplementedException();

        public string Summoner => throw new NotImplementedException();

        public int Level => throw new NotImplementedException();

        public int IconId => throw new NotImplementedException();

        public int BlueEssense => throw new NotImplementedException();

        public int RiotPoints => throw new NotImplementedException();

        public Ranks Rank => throw new NotImplementedException();

        public bool IsBanned => throw new NotImplementedException();

        public DateTime Decay => throw new NotImplementedException();

        public DateTime LastActivity => throw new NotImplementedException();
    }
}
