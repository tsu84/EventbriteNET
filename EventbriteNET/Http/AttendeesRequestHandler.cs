using EventbriteNET.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace EventbriteNET.Http
{
    /// <summary>
    /// https://developer.eventbrite.com/docs/ticket-classes/
    /// </summary>
    class AttendeesRequestHandler : RequestBase<Attendee>
    {
        public AttendeesRequestHandler(EventbriteContext context) : base(context) { }

        protected override IList<Attendee> OnGet()
        {
            if (Context.EventId <= 0)
                throw new ArgumentException("EventId not set in Context", "entity");

            List<Attendee> attendeeList = new List<Attendee>();
            return OnGet(attendeeList, 1);
        }

        protected List<Attendee> OnGet(List<Attendee> list, int page)
        {
            var request = new RestRequest("events/{id}/attendees/");
            request.AddUrlSegment("id", Context.EventId.ToString());
            request.AddQueryParameter("token", Context.Token);
            if (page > 1)
                request.AddQueryParameter("page", page.ToString());

            var eventAttendees = this.Execute<EventAttendees>(request);
            list.AddRange(eventAttendees.Attendees);

            Context.Pagination = eventAttendees.Pagination;

            if (page < eventAttendees.Pagination.PageCount)
                OnGet(list, page + 1);

            return list;
        }

        protected override Attendee OnGet(long id)
        {
            if (Context.EventId <= 0)
                throw new ArgumentException("EventId not set in Context", "entity");

            var request = new RestRequest("events/{id}/attendees/{attendee_id}/");
            request.AddUrlSegment("id", Context.EventId.ToString());
            request.AddUrlSegment("attendee_id", id.ToString());
            request.AddQueryParameter("token", Context.Token);

            if (Context.Page > 1)
                request.AddQueryParameter("page", Context.Page.ToString());

            return this.Execute<Attendee>(request);
        }

        protected override void OnCreate(Attendee entity)
        {
            throw new NotImplementedException();
        }

        protected override void OnUpdate(Attendee entity)
        {
            throw new NotImplementedException();
        }


        protected override Task<IList<Attendee>> OnGetAsync()
        {
            return Task.Run(() => OnGet());
        }

        protected override Task<Attendee> OnGetAsync(long id)
        {
            if (Context.EventId <= 0)
                throw new ArgumentException("EventId not set in Context", "entity");

            var request = new RestRequest("events/{id}/attendees/{attendeeId}/");
            request.AddUrlSegment("id", Context.EventId.ToString());
            request.AddUrlSegment("attendeesId", id.ToString());
            request.AddQueryParameter("token", Context.Token);

            if (Context.Page > 1)
                request.AddQueryParameter("page", Context.Page.ToString());

            return this.ExecuteAsync<Attendee>(request);
        }

        private string[] AcceptedPostFields()
        {
            return new[] 
                {
                    "status",
                    "changed_since"
                };
        }
    }
}


