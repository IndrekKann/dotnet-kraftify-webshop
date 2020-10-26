﻿using System;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.webshop.indrek.Domain.Base;

namespace Domain.App
{
    public class LangStrTranslation : DomainEntityIdMetadata
    {
        [MaxLength(5)] 
        public string Culture { get; set; } = default!;
        [MaxLength(10240)] 
        public string Value { get; set; } = default!;

        public Guid LangStrId { get; set; }
        public LangStr? LangStr { get; set; }
    }
}