using System;
using System.Collections.Generic;

namespace popfragg.Domain.Models.Nades;

public partial class NadeTag
{
    public Guid Id { get; set; }

    public Guid NadeId { get; set; }

    public string Tag { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Nade Nade { get; set; } = null!;
}
