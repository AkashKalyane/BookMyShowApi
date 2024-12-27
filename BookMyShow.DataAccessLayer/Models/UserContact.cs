using System;
using System.Collections.Generic;

namespace BookMyShow.DataAccessLayer.Models;

public partial class UserContact
{
    public int UserContactId { get; set; }

    public int? UserId { get; set; }

    public string PhoneNumber { get; set; } = null!;

    public string CountryCode { get; set; } = null!;

    public bool IsActive { get; set; }

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
