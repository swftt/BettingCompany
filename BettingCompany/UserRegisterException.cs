using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BettingCompany
{
    class UserRegistrationException:ApplicationException
    {
          public UserRegistrationException() { }
            public UserRegistrationException(string message) : base(message) { }
            public UserRegistrationException(string message, Exception inner) : base(message, inner) { }
            protected UserRegistrationException(
              System.Runtime.Serialization.SerializationInfo info,
              System.Runtime.Serialization.StreamingContext context)
                : base(info, context) { }

        
    }
}
