using System;

namespace PublicApi.DTO.v1
{
    public class OrderCreateDTO
    {
        public Guid ShoppingCartId { get; set; }
        public Guid AppUserId { get; set; }
    }
}