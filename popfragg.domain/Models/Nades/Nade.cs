using popfragg.Domain.Models.Users;
using System;
using System.Collections.Generic;

namespace popfragg.Domain.Models.Nades;

public partial class Nade
{
    public Guid Id { get; set; }

    public Guid AuthorId { get; set; }

    public string Map { get; set; } = null!;

    public string? Side { get; set; }

    public string Type { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public string? VideoUrl { get; set; }

    public string Visibility { get; set; } = null!;

    public string? AccuracyLevel { get; set; }

    public string Technique { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public decimal? Rating { get; set; }

    public virtual User Author { get; set; } = null!;

    public virtual ICollection<NadeAccess> NadeAccesses { get; set; } = new List<NadeAccess>();

    public virtual ICollection<NadeImage> NadeImages { get; set; } = new List<NadeImage>();

    public virtual ICollection<NadeRating> NadeRatings { get; set; } = new List<NadeRating>();

    public virtual ICollection<NadeTag> NadeTags { get; set; } = new List<NadeTag>();
}
