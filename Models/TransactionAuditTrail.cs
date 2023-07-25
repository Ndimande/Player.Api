﻿
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Player.Api.Models;

public class TransactionAuditTrail
{
    /// <summary>
    ///     Get or set the <see cref="int" /> primary Id property
    /// </summary>
    [Required]
    public int id { get; set; }

    /// <summary>
    ///     Get or set the <see cref="int" /> Foreign Key property
    /// </summary>
    /// 
    [ForeignKey("playerId")]
    public int playerId { get; set; }

    /// <summary>
    ///     Get or set the <see cref="float" /> Foreign Key property
    /// </summary>
    /// 
    [ForeignKey("transactionTypesId")]
    public int transactionTypesId { get; set; }

    /// <summary>
    ///     Get or set the <see cref="float" /> balance property
    /// </summary>
    public double? balance { get; set; }

    /// <summary>
    ///     Get or set the <see cref="float" /> value property
    /// </summary>
    public double? value { get; set; }

    /// <summary>
    ///     Get or set the <see cref="float" /> balance property
    /// </summary>
    /// 

    public string? updatedBy { get; set; }

    /// <summary>
    ///     Get or set the <see cref="float" /> balance property
    /// </summary>
    public string? notes { get; set; }

    /// <summary>
    ///     Get or set the <see cref="float" /> balance property
    /// </summary>
    public DateTime LastUpdatedOn = DateTime.Now.ToUniversalTime();


}

