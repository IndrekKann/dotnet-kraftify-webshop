using System;
using System.Collections.Generic;

namespace BLL.App.DTO
{
    public class ShoppingCartView
    {
        public Guid Id { get; set; }

        public IEnumerable<ProductView>? Products { get; set; }
    }
}