using System;
using System.Collections.Generic;

namespace ProblemSolvingPlatform.Models;

public partial class Classement
{
    public int ClassementId { get; set; }

    public int UserId { get; set; }

    public string Score { get; set; } = null!;

    public int Rang { get; set; }

    public virtual User User { get; set; } = null!;
}
