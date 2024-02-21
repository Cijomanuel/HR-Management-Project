using System;
using System.Collections.Generic;

namespace HRApplicationAPI.Model
{
    public partial class ClaimStoreDTO
    {
        public int ClaimId { get; set; }
        public string? ClaimType { get; set; }
        public string? ClaimValue { get; set; }
    }
}
