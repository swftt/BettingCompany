using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BettingCompany
{
    class FirstDepositeException:ApplicationException
    {
        public FirstDepositeException() { }
        public FirstDepositeException(string message) : base(message) { }
        public FirstDepositeException(string message, Exception inner) : base(message, inner) { }
        protected FirstDepositeException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
       
    }
}
