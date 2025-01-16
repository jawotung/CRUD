using System;
using System.Collections.Generic;

namespace WebAPI;

public partial class User
{
    public int Id { get; set; }

    public string? UserId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Email { get; set; }

    public string? MobileNo { get; set; }

    public int? CreateId { get; set; }

    public DateTime? CreateDate { get; set; }

    public int? UpdateId { get; set; }

    public DateTime? UpdateDate { get; set; }
}
