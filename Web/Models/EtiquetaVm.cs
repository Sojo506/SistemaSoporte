namespace Web.Models;

/// <summary>
/// ViewModel de una etiqueta devuelta por el API.
/// Es una clase propia del MVC; el contrato con el API es el JSON.
/// </summary>
public class EtiquetaVm
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;
}
