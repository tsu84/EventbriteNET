using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace EventbriteNET
{
    /// <summary>
    /// Represents an Eventbrite Ticket Class <see cref="https://developer.eventbrite.com/docs/attendee-object/"/>
    /// </summary>

    public class BasePrice
    {
        public string display { get; set; }
        public string currency { get; set; }
        public int value { get; set; }
        public string major_value { get; set; }
    }

    public class EventbriteFee
    {
        public string display { get; set; }
        public string currency { get; set; }
        public int value { get; set; }
        public string major_value { get; set; }
    }

    public class Gross
    {
        public string display { get; set; }
        public string currency { get; set; }
        public int value { get; set; }
        public string major_value { get; set; }
    }

    public class PaymentFee
    {
        public string display { get; set; }
        public string currency { get; set; }
        public int value { get; set; }
        public string major_value { get; set; }
    }

    public class Tax
    {
        public string display { get; set; }
        public string currency { get; set; }
        public int value { get; set; }
        public string major_value { get; set; }
    }

    public class Costs
    {
        public BasePrice base_price { get; set; }
        public EventbriteFee eventbrite_fee { get; set; }
        public Gross gross { get; set; }
        public PaymentFee payment_fee { get; set; }
        public Tax tax { get; set; }
    }

    public class Address
    {
        public string address_1 { get; set; }
        public string address_2 { get; set; }
        public string city { get; set; }
        public string region { get; set; }
        public string postal_code { get; set; }
        public string country { get; set; }
        public string country_name { get; set; }
    }

    public class Addresses
    {
        public Address home { get; set; }
        public Address ship { get; set; }
        public Address work { get; set; }
        public Address bill { get; set; }
    }

    public class Profile
    {
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public string name { get; set; }
        public Addresses addresses { get; set; }
    }

    public class Barcode
    {
        public string status { get; set; }
        public string barcode { get; set; }
        public int checkin_type { get; set; }
        public string created { get; set; }
        public string changed { get; set; }
    }

    public class Attendee : EventbriteObject
    {
        public object team { get; set; }
        public Costs costs { get; set; }
        public string resource_uri { get; set; }
        public string id { get; set; }
        public string changed { get; set; }
        public string created { get; set; }
        public int quantity { get; set; }
        public Profile profile { get; set; }
        public List<Barcode> barcodes { get; set; }
        public List<object> answers { get; set; }
        public bool checked_in { get; set; }
        public bool cancelled { get; set; }
        public bool refunded { get; set; }
        public string affiliate { get; set; }
        public string status { get; set; }
        public string event_id { get; set; }
        public string order_id { get; set; }
        public string ticket_class_id { get; set; }
    }

    public class EventAttendees : EventbriteObject
    {
        [JsonProperty("pagination")]
        public Pagination Pagination { get; set; }
        [JsonProperty("attendees")]
        public List<Attendee> Attendees { get; set; }
    }
}
