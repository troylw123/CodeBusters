using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CodeBusters.Models.Token
{
    public class TokenResponse
    {
        public string Token { get; set; }
        public DateTime IssuedAt { get; set; }
        public DateTime Expires { get; set; }
    }
}