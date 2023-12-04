using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CMSoft.Common.MsIdentity.Dtos
{
    public class GetTokenRequest
    {
        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        [StringLength(50, MinimumLength =8)]
        public string Password { get; set; }
    }
}
