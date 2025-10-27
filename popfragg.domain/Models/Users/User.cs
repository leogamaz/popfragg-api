using popfragg.Domain.Models.Nades;
using System;
using System.Collections.Generic;

namespace popfragg.Domain.Models.Users;

public partial class User
{
    public Guid Id { get; set; }

    public string? SteamId { get; set; }

    public string Nickname { get; set; } = null!;

    public string? AvatarUrl { get; set; }

    public string? ProfileUrl { get; set; }

    public string Role { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Primary Name
    /// </summary>
    public string? GivenName { get; set; }

    public string? FamilyName { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public virtual ICollection<NadeAccess> NadeAccesses { get; set; } = new List<NadeAccess>();

    public virtual ICollection<NadeRating> NadeRatings { get; set; } = new List<NadeRating>();

    public virtual ICollection<Nade> Nades { get; set; } = new List<Nade>();
}
