using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace ProblemSolvingPlatform.Models;

public partial class User : IdentityUser<int>
{
    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public DateTime RegistrationDate { get; set; }

    public string? ProfilePicture { get; set; }

    public int Role { get; set; }

    public bool IsActive { get; set; }

    public virtual Classement? Classement { get; set; }

    public virtual ICollection<Commentaire> Commentaires { get; set; } = new List<Commentaire>();

    public virtual ICollection<Soumission> Soumissions { get; set; } = new List<Soumission>();
}
