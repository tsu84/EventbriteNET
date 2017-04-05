using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace EventbriteNET
{
    public class Question : EventbriteObject
    {
        public string resource_uri { get; set; }
        public long id { get; set; }
        public MultipartTextField question { get; set; }
        public string type { get; set; }
        public Boolean required { get; set; }
        public List<Choice> choices { get; set; }
        public List<QuestionTicketClass> ticket_classes { get; set; }
    }

    public class Choice
    {
        public MultipartTextField answer { get; set; }
        public long id { get; set; }
        public List<long> subquestion_ids;
    }

    public class QuestionTicketClass
    {
        public long id { get; set; }
        public string name { get; set; }
    }

    public class EventQuestions : EventbriteObject
    {
        [JsonProperty("pagination")]
        public Pagination Pagination { get; set; }
        [JsonProperty("questions")]
        public List<Question> Questions { get; set; }
    }
}
