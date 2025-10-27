using System;
using System.Collections.Generic;

namespace popfragg.Domain.Models.Nades;

public partial class NadeImage
{
    public Guid Id { get; set; }

    public Guid NadeId { get; set; }

    public int ImageOrder { get; set; }

    public string ImageUrl { get; set; } = null!;

    public DateTime UploadedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Nade Nade { get; set; } = null!;
}
