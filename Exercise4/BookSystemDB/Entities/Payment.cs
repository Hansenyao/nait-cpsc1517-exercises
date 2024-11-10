﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BookSystemDB.Entities;

public partial class Payment
{
    [Key]
    public int PaymentID { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime PaymentDate { get; set; }

    [Column(TypeName = "smallmoney")]
    public decimal Amount { get; set; }

    public byte PaymentTypeID { get; set; }

    public int OrderID { get; set; }

    public Guid TransactionID { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ClearedDate { get; set; }

    [ForeignKey("OrderID")]
    [InverseProperty("Payments")]
    public virtual Order Order { get; set; }

    [ForeignKey("PaymentTypeID")]
    [InverseProperty("Payments")]
    public virtual PaymentType PaymentType { get; set; }
}