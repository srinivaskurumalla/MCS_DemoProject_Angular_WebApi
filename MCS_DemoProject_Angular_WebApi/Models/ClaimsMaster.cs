using System;
using System.Collections.Generic;

namespace MCS_DemoProject_Angular_WebApi.Models
{
    public partial class ClaimsMaster
    {
        public int ClaimId { get; set; }
        public string? ClaimName { get; set; }
        public string? ClaimType { get; set; }
        public DateTime? ClaimDate { get; set; }
        public decimal? ClaimAmount { get; set; }
    }
}
