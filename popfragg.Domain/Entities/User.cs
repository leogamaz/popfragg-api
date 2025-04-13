using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace popfragg.Domain.Entities
{
    [Table("authorizer_users")]
    public class User 
    {
        [Column("id")]
        public Guid Id { get; set; } // character(36) → Guid

        [Column("key")]
        public string? Key { get; set; }

        [Column("email")]
        public string? Email { get; set; }

        [Column("email_verified")]
        public long? EmailVerified { get; set; } // bigint → Unix timestamp

        [Column("password")]
        public string? Password { get; set; }

        [Column("signup_methods")]
        public string? SignupMethods { get; set; }

        [Column("given_name")]
        public string? GivenName { get; set; }

        [Column("family_name")]
        public string? FamilyName { get; set; }

        [Column("middle_name")]
        public string? MiddleName { get; set; }

        [Column("nickname")]
        public string? Nickname { get; set; }

        [Column("gender")]
        public string? Gender { get; set; }

        [Column("birthdate")]
        public string? Birthdate { get; set; } // string for now, can be parsed as DateTime if format is known

        [Column("phone_number")]
        public string? PhoneNumber { get; set; }

        [Column("phone_number_verified_at")]
        public long? PhoneNumberVerified { get; set; }

        [Column("picture")]
        public string? Picture { get; set; }

        [Column("roles")]
        public string? Roles { get; set; }

        [Column("revoked_timestamp")]
        public long? RevokedTimestamp { get; set; }

        [Column("is_multi_factor_auth_enabled")]
        public bool? IsMultiFactorAuthEnabled { get; set; }

        [Column("updated_at")]
        public long? UpdatedAt { get; set; }

        [Column("created_at")]
        public long? CreatedAt { get; set; }

        [Column("app_data")]
        public string? AppData { get; set; }
    }
}
