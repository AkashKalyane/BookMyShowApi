using System;
using System.Collections.Generic;

namespace BookMyShow.DataAccessLayer.Models;

public partial class Booking
{
    public int BookingId { get; set; }

    public int? MovieId { get; set; }

    public int? TheaterScreenId { get; set; }

    public int Price { get; set; }

    public int? SlotId { get; set; }

    public int NumberOfSlot { get; set; }

    public int CreatedBy { get; set; }

    public DateTime? CreateOn { get; set; }

    public int? ChangedBy { get; set; }

    public DateTime? ChangedOn { get; set; }

    public int? DeletedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public virtual User? ChangedByNavigation { get; set; }

    public virtual User CreatedByNavigation { get; set; } = null!;

    public virtual User? DeletedByNavigation { get; set; }

    public virtual Movie? Movie { get; set; }

    public virtual Slot? Slot { get; set; }

    public virtual TheaterScreen? TheaterScreen { get; set; }
}
