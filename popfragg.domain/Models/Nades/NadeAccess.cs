using popfragg.Domain.Models.Users;
using System;
using System.Collections.Generic;

namespace popfragg.Domain.Models.Nades;

public partial class NadeAccess
{
    public Guid Id { get; set; }

    public Guid NadeId { get; set; }

    public Guid UserId { get; set; }

    public DateTime GrantedAt { get; set; }

    public virtual Nade Nade { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
