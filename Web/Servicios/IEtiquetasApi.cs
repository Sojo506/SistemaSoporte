using Web.Models;

namespace Web.Servicios;

/// <summary>
/// Contrato del cliente HTTP tipado para el módulo de etiquetas.
/// </summary>
public interface IEtiquetasApi
{
    Task<List<EtiquetaVm>> ListarAsync(CancellationToken ct = default);

    /// <summary>Devuelve null si la etiqueta no existe (404). Cualquier otro error lanza ApiException.</summary>
    Task<EtiquetaVm?> ObtenerAsync(int id, CancellationToken ct = default);

    Task<EtiquetaVm> CrearAsync(EtiquetaCrearVm datos, CancellationToken ct = default);
}
