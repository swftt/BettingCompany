using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BettingCompany
{
    public  class StatusEventArgs
    {
        public readonly string msg;
        public StatusEventArgs(string message)
        {
            msg = message;
        }
    }
}
