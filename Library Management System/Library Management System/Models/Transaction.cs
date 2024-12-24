using System;
using System.Collections.Generic;

namespace Library_Management_System.Models;

public partial class Transaction
{
    public int TransactionId { get; set; }

    public int BookId { get; set; }

    public int UserId { get; set; }

    public DateOnly IssueDate { get; set; }

    public DateOnly DueDate { get; set; }

    public DateOnly? ReturnDate { get; set; }

    public string Status { get; set; } = null!;

    public virtual Book Book { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
