namespace LifeCenter.Models.Personal;

using System.ComponentModel.DataAnnotations;
using LifeCenter.Entities;

public class UpdateRequest
{
    public string? Id { get; set;}
    public string? Name { get; set;}
    [EnumDataType(typeof(Rol))]
    public string? Rol { get; set;}
    public string? FechaCreado  { get; set;}
    public string? FechaModificado  { get; set;}
    public string? CreadoPor  { get; set;}
    public string? ModificadoPor  { get; set;}
/*
    [EmailAddress]
    public string? Email { get; set; }

    // treat empty string as null for password fields to 
    // make them optional in front end apps
    private string? _password;
    [MinLength(6)]
    public string? Password
    {
        get => _password;
        set => _password = replaceEmptyWithNull(value);
    }

    private string? _confirmPassword;
    [Compare("Password")]
    public string? ConfirmPassword 
    {
        get => _confirmPassword;
        set => _confirmPassword = replaceEmptyWithNull(value);
    }
*/
    // helpers

    private string? replaceEmptyWithNull(string? value)
    {
        // replace empty string with null to make field optional
        return string.IsNullOrEmpty(value) ? null : value;
    }
}