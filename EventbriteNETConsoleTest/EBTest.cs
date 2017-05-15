using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventbriteNET;

namespace EventbriteNETConsoleTest
{
    class EBTest
    {
        static void Main(string[] args)
        {
            var eventbriteNET = new EventbriteContext("R344HVVVI6TFIJNGIRUZ");

            //29080164555
            var ev = eventbriteNET.Get<Event>(29080164555);
            var users = eventbriteNET.Get<Attendee>();

        }
    }
}
