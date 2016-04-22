using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using EventbriteNET;

namespace EventbriteNETConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var eventbriteNET = new EventbriteContext("LLGPFYIY2UBUUHOK5DP6");


            var users = eventbriteNET.Get<User>();

            Console.WriteLine(users.Count);

            var user = users[0];

            Console.WriteLine(user.Emails.Count);
            Console.WriteLine(user.Name);
            Console.WriteLine(user.FirstName);
            Console.WriteLine(user.LastName);
          

            //var ebEvents = eventbriteNET.Get<Event>().ToList();

            //var pagedEvents = eventbriteNET.GetOwnedEvents();

            var pagedEvents = eventbriteNET.Search();

            eventbriteNET.Pagination = pagedEvents.Pagination;

            var ebEvents = pagedEvents.Events;

            Console.WriteLine(string.Format("Pagination: ObjectCount {0} PageCount {1} PageNumber {2} PageSize {3}", eventbriteNET.Pagination.ObjectCount, eventbriteNET.Pagination.PageCount, eventbriteNET.Pagination.PageNumber, eventbriteNET.Pagination.PageSize));
            ebEvents.ForEach(e =>
            {
                Console.WriteLine(string.Format("{0} {1} {2}", e.Description.Text, e.Start.Local, e.End.Local));
                eventbriteNET.EventId = e.Id;
                
                Console.WriteLine(string.Format("Location {0}", e.OnlineEvent ? "Online Event" : string.Format("{0} {1} {2}", e.VenueId, e.Venue.Address.Address1, e.Venue.Address.City)));
                if (!e.OnlineEvent)
                {
                    var venuue = eventbriteNET.Get<Venue>(e.VenueId ?? 0);
                    e.Venue = venuue;
                    Console.WriteLine(e.Venue.ResourceUri);
                    Console.WriteLine(string.Format("Location {0}", e.Venue.Name));
                }

         
                


                var attendees = eventbriteNET.Get<Attendee>().ToList();

                Console.WriteLine(string.Format("Attendee Pagination: ObjectCount {0} PageCount {1} PageNumber {2} PageSize {3}", eventbriteNET.Pagination.ObjectCount, eventbriteNET.Pagination.PageCount, eventbriteNET.Pagination.PageNumber, eventbriteNET.Pagination.PageSize));

                Console.WriteLine(string.Format("Attendees {0}", attendees.Count));

               

               
                attendees.ForEach(a => Console.WriteLine(a.profile.email));

            });

            //
          

            Console.ReadLine();
            // eventbriteNET.Get<List<Event>>("")
            // Console
        }
    }
}
