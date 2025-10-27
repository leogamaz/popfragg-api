using popfragg.Domain.Models.Users;
using System;
using System.Collections.Generic;

namespace popfragg.Domain.Models.Nades;

public partial class NadeRating
{
    public Guid Id { get; set; }

    public Guid NadeId { get; set; }

    public Guid UserId { get; set; }

    public decimal Rating { get; set; }

    public DateTime RatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Nade Nade { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
