using System;
using System.Collections.Generic;

namespace BookMyShow.DataAccessLayer.Models;

public partial class UserDetail
{
    public int UserDetailId { get; set; }

    public int? UserId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string MiddleName { get; set; } = null!;

    public string FullNameFl { get; set; } = null!;

    public string FullNameLf { get; set; } = null!;

    public string FullNameFml { get; set; } = null!;

    public int CreatedBy { get; set; }

    public DateTime? CreateOn { get; set; }

    public int? ChangedBy { get; set; }

    public DateTime? ChangedOn { get; set; }

    public int? DeletedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public virtual User? ChangedByNavigation { get; set; }

    public virtual User CreatedByNavigation { get; set; } = null!;

    public virtual User? DeletedByNavigation { get; set; }

    public virtual User? User { get; set; }
}
