using System;
using System.Collections.Generic;

namespace ProblemSolvingPlatform.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

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
