using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventbriteNET.Http
{
    class QuestionRequestHandler : RequestBase<Question>
    {
        public QuestionRequestHandler(EventbriteContext context) : base(context) { }

        protected override IList<Question> OnGet()
        {
            if (Context.EventId <= 0)
                throw new ArgumentException("EventId not set in Context", "entity");

            List<Question> questionList = new List<Question>();
            return OnGet(questionList, 1);
        }

        protected List<Question> OnGet(List<Question> list, int page)
        {
            var request = new RestRequest("events/{id}/questions/");
            request.AddUrlSegment("id", Context.EventId.ToString());
            request.AddQueryParameter("token", Context.Token);
            if (page > 1)
                request.AddQueryParameter("page", page.ToString());

            var eventQuestions = this.Execute<EventQuestions>(request);
            list.AddRange(eventQuestions.Questions);

            Context.Pagination = eventQuestions.Pagination;

            if (page < eventQuestions.Pagination.PageCount)
                OnGet(list, page + 1);

            return list;
        }

        protected override Question OnGet(long id)
        {
            if (Context.EventId <= 0)
                throw new ArgumentException("EventId not set in Context", "entity");

            var request = new RestRequest("events/{id}/questions/{question_id}");
            request.AddUrlSegment("id", Context.EventId.ToString());
            request.AddUrlSegment("question_id", id.ToString());
            request.AddQueryParameter("token", Context.Token);

            if (Context.Page > 1)
                request.AddQueryParameter("page", Context.Page.ToString());

            return this.Execute<Question>(request);
        }

        protected override void OnCreate(Question entity)
        {
            throw new NotImplementedException();
        }

        protected override void OnUpdate(Question entity)
        {
            throw new NotImplementedException();
        }

        protected override Task<IList<Question>> OnGetAsync()
        {
            return Task.Run(() => OnGet());
        }

        protected override Task<Question> OnGetAsync(long id)
        {
            if (Context.EventId <= 0)
                throw new ArgumentException("EventId not set in Context");

            var request = new RestRequest("events/{id}/questions/{questionId}");
            request.AddUrlSegment("id", Context.EventId.ToString());
            request.AddUrlSegment("attendeesId", id.ToString());
            request.AddQueryParameter("token", Context.Token);

            if (Context.Page > 1)
                request.AddQueryParameter("page", Context.Page.ToString());

            return this.ExecuteAsync<Question>(request);
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
