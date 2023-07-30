namespace LifeCenter.Models.Personal;

using System.ComponentModel.DataAnnotations;
using LifeCenter.Entities;

public class CreateRequest
{
    
    [Required]    
    public string? Id { get; set;}
    
    [Required]
    [StringLength(10, ErrorMessage = "Name is too long.")]
    public string? Name { get; set;}
    
    [Required]    
    [EnumDataType(typeof(Rol))]
    public string? Rol { get; set;}
    
    [Required]    
    public string? FechaCreado  { get; set;}
    
    [Required]    
    public string? FechaModificado  { get; set;}
    
    [Required]    
    public string? CreadoPor  { get; set;}
    
    [Required]    
    public string? ModificadoPor  { get; set;}

    
/*
    [Required]
    [EmailAddress]
    public string? Email { get; set; }

    [Required]
    [MinLength(6)]
    public string? Password { get; set; }

    [Required]
    [Compare("Password")]
    public string? ConfirmPassword { get; set; }
    */
}