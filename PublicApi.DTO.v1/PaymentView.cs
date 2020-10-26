using System;

namespace PublicApi.DTO.v1
{
    public class PaymentView
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Phone { get; set; } = default!;
        public string OrderNumber { get; set; } = default!;
        public string PaymentType { get; set; } = default!;
        public string Location { get; set; } = default!;
        public DateTime Date { get; set; }
    }
}