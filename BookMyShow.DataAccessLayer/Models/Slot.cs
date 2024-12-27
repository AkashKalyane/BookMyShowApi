using System;
using System.Collections.Generic;

namespace BookMyShow.DataAccessLayer.Models;

public partial class Slot
{
    public int SlotId { get; set; }

    public string SlotName { get; set; } = null!;

    public bool IsAvailable { get; set; }

    public int CreatedBy { get; set; }

    public DateTime? CreateOn { get; set; }

    public int? ChangedBy { get; set; }

    public DateTime? ChangedOn { get; set; }

    public int? DeletedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual User? ChangedByNavigation { get; set; }

    public virtual User CreatedByNavigation { get; set; } = null!;

    public virtual User? DeletedByNavigation { get; set; }
}
