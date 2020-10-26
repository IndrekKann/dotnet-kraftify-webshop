using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.v1
{
    public class Destination
    {
        public Guid Id { get; set; }
        
        [MaxLength(128)]
        public string Location { get; set; } = default!;
    }
}
