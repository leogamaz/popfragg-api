using System;
using System.Collections.Generic;

namespace popfragg.Domain.Models;

public partial class Error
{
    public Guid Id { get; set; }

    public string Message { get; set; } = null!;

    public string? StackTrace { get; set; }

    public string? Source { get; set; }

    public string? Level { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? HttpStatus { get; set; }

    public string? Path { get; set; }

    public string? Method { get; set; }

    public Guid? UserId { get; set; }

    public string? TraceId { get; set; }

    public string? MessageUser { get; set; }

    public string? CodeMessage { get; set; }

    public string? Properties { get; set; }
}
