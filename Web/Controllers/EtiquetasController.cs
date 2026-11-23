using Microsoft.AspNetCore.Mvc;
using Web.Models;
using Web.Servicios;

namespace Web.Controllers;

/// <summary>
/// Controlador MVC de etiquetas: lista, detalle y creación.
/// </summary>
public class EtiquetasController : Controller
{
    private readonly IEtiquetasApi _api;

    public EtiquetasController(IEtiquetasApi api) => _api = api;

    public async Task<IActionResult> Index(CancellationToken ct = default) =>
        View(await _api.ListarAsync(ct));

    public async Task<IActionResult> Detalle(int id, CancellationToken ct = default)
    {
        var etiqueta = await _api.ObtenerAsync(id, ct);
        return etiqueta is null ? NotFound() : View(etiqueta);
    }

    [HttpGet]
    public IActionResult Crear() => View(new EtiquetaCrearVm());

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Crear(EtiquetaCrearVm datos, CancellationToken ct)
    {
        if (!ModelState.IsValid) return View(datos);

        try
        {
            var creada = await _api.CrearAsync(datos, ct);
            TempData["Exito"] = $"Etiqueta '{creada.Nombre}' creada.";

            // Post-Redirect-Get: redirige al detalle para evitar reenvíos con F5.
            return RedirectToAction(nameof(Detalle), new { id = creada.Id });
        }
        catch (ApiException ex) when (ex.Codigo is 400 or 409 or 422)
        {
            AgregarErroresDelApi(ex);
            return View(datos);
        }
    }

    private void AgregarErroresDelApi(ApiException ex)
    {
        if (ex.Errores.Count == 0) ModelState.AddModelError(string.Empty, ex.Message);
        foreach (var (campo, mensajes) in ex.Errores)
            foreach (var mensaje in mensajes)
                ModelState.AddModelError(campo, mensaje);
    }
}
