using System;
using System.Collections.Generic;

namespace Library_Management_System.Models;

public partial class Book
{
    public int BookId { get; set; }

    public string Title { get; set; } = null!;

    public string Author { get; set; } = null!;

    public string Isbn { get; set; } = null!;

    public string? Genre { get; set; }

    public DateOnly? PublicationDate { get; set; }

    public int AvailableCopies { get; set; }

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
