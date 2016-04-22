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
    /// http://developer.eventbrite.com/docs/event-details/
    /// </summary>
    class VenueRequestHandler : RequestBase<Venue>
    {
        public VenueRequestHandler(EventbriteContext context) : base(context) { }

       
        protected override IList<Venue> OnGet()
        {
            var request = new RestRequest("venues/{id}/");
            if (Context.VenuesId.Count() == 0)
                throw new ArgumentException("VenuesId not set in Context", "entity");
            
            var venues = new List<Venue>();

            foreach (var venueId in Context.VenuesId)
            {
                request.AddUrlSegment("id", venueId.ToString());
                request.AddQueryParameter("token", Context.Token);
                var venue = this.Execute<Venue>(request);
                venues.Add(venue);
            }

            return venues;

            //throw new NotImplementedException();
        }

        protected override Venue OnGet(long id)
        {
            var request = new RestRequest("venues/{id}/");
            request.AddUrlSegment("id", id.ToString());
            request.AddQueryParameter("token", Context.Token);

            Context.VenuesId = new [] {id};
            return this.Execute<Venue>(request);
        }

        protected override void OnCreate(Venue entity)
        {
            var request = new RestRequest("venues/", HttpMethod.Post);
            request.AddQueryParameter("token", Context.Token);
            request.AddObject(entity, "venue", AcceptedPostFields());

            // execute Event create
            var response = this.Execute(request);

            if (response.IsSuccessStatusCode)
            {
                // get updated response
                var persisted = response.As<Venue>();
                // persist base properties onto entity
                entity.Id = persisted.Id;
                entity.ResourceUri = persisted.ResourceUri;
                Context.EventId = persisted.Id;
                // Venue create
            }
            else
            {
                this.ThrowResponseError(response);
            }
        }

        protected override void OnUpdate(Venue entity)
        {
            if (entity.Id <= 0)
                throw new ArgumentException("Id not set in Venue", "entity");

            var request = new RestRequest("venues/{id}/", HttpMethod.Post);
            request.AddUrlSegment("id", entity.Id.ToString());
            request.AddQueryParameter("token", Context.Token);
            request.AddObject(entity, "venue", AcceptedPostFields());

            // Execute Event update
            var response = this.Execute(request);

            if (response.IsSuccessStatusCode)
            {
                if (Context.VenuesId == null)
                {
                    Context.VenuesId = new long[]
                    {
                        entity.Id
                    };
                }
            }
            else
            {
                this.ThrowResponseError(response);
            }
        }

        protected override Task<IList<Venue>> OnGetAsync()
        {
            return Task.Run(() => OnGet());
        }

        protected override Task<Venue> OnGetAsync(long id)
        {
            var request = new RestRequest("venues/{id}/");
            request.AddUrlSegment("id", id.ToString());
            request.AddQueryParameter("token", Context.Token);

            Context.EventId = id;
            return this.ExecuteAsync<Venue>(request);
        }

        public void Publish(long id)
        {
            throw new NotImplementedException();
        }

        public void Unpublish(long id)
        {
            throw new NotImplementedException();
        }


        private string[] AcceptedPostFields()
        {
            return new[] 
            {
                "Name",
                "organizer_id",
                "address.address_1",
                "address.address_2",
                "address.city",
                "address.region",
                "address.postal_code", 
                "address.country",
                "address.latitude",
                "address.longitude"
            };
        }
    }
}
