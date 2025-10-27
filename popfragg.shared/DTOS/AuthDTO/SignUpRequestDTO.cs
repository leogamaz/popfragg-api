using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace popfragg.Shared.DTOS.Auth
{
    public class SignUpRequestDTO
    {
        [MaxLength(50)]
        public string? SteamId { get; private set; }

        [MaxLength(100)]
        [Required]
        public required string Nickname { get; set; }

        [MaxLength(100)]
        [Required]
        public required string GivenName { get; set; }

        [MaxLength(255)]
        [Required]
        public required string FamilyName { get; set; }

        [MaxLength(255)]
        [Required]
        public required string Email { get; set; }

        [MaxLength(80)]
        [Required]
        public required string Password { get; set; }

        [MaxLength(80)]
        [Required]
        public required string ConfirmPassword { get; set; }

        public void AddSteamId(string steamId)
        {
            SteamId = steamId;
        }

    }
}
