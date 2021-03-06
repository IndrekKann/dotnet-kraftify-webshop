﻿using System;

namespace PublicApi.DTO.v1
{
    public class PaymentCreate
    {
        public Guid AppUserId { get; set; }
        
        public Guid PaymentTypeId { get; set; }

        public Guid DestinationId { get; set; }
        
        public Guid ShoppingCartId { get; set; }
    }
}