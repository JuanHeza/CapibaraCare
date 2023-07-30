namespace LifeCenter.Entities;
using System.ComponentModel.DataAnnotations;

public class Personal{
    public int Id { get; set;}

    [Required(ErrorMessage = "Nombre Requerido")]
    //[StringLength(10, ErrorMessage = "Name is too long.")]
    //[MinLength(3, ErrorMessage = "Name is too short.")]
    public string? Name { get; set;}

    [Required(ErrorMessage = "Apellido Paterno requerido.")]
    public string? MiddleName { get; set;}

    [Required(ErrorMessage = "Apellido Materno requerido.")]
    public string? LastName { get; set;}
    
    [Required(ErrorMessage = "Usuario requerido.")]
    public string? Username { get; set;}

    [StringLength(10, ErrorMessage = "Contraseña requerido.")]
    public string? Password { get; set;}

    [Compare(nameof(Password), ErrorMessage = "Las contraseñas no son iguales.")]
    public string? ConfirmPassword { get; set;}

    [Required(ErrorMessage = "Correo requerido.")]
    [EmailAddress]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Numero Requerido")]
    [RegularExpression("^([0-9]{10})$", ErrorMessage = "Invalid Mobile Number.")]
    public int? Phone { get; set; }
    
    [Required]
    [EnumDataType(typeof(Rol), ErrorMessage = "nada")]
    [Range(1, int.MaxValue, ErrorMessage = "Rol no asignado")]
    public Rol Rol { get; set;}
    
    public DateTime FechaCreado  { get; set;}
    
    public DateTime FechaModificado  { get; set;}
    
    public int CreadoPor  { get; set;}
    
    public int ModificadoPor  { get; set;}
    
    // public DateOnly Cita
    public string Stringify(){
        return string.Format("Id: {0}\nName: {1}\nLastName: {2}\nUsername: {3}\nPassword: {4}\nEmail: {5}\nPhone: {6}\nRol: {7}\nFechaCreado: {8}\nFechaModificado: {9}\nCreadoPor: {10}\nModificadoPor {11}", Id, Name, LastName, Username, Password, Email, Phone, Rol, FechaCreado, FechaModificado, CreadoPor, ModificadoPor);
    }
}

public enum Rol{
    Ninguno,
    Admin = 1,
    Medico,
    Enfermera
}