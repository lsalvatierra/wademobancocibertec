using Microsoft.AspNetCore.Mvc;
using System;
using wademobancocibertec.Dto;
using wademobancocibertec.Models;
using wademobancocibertec.Services;
using wademobancocibertec.ViewModel;

namespace wademobancocibertec.Controllers
{
    public class CuentaController : Controller
    {

        private readonly CuentaService _cuentaService;

        public CuentaController(CuentaService cuentaService)
        {
            _cuentaService = cuentaService;
        }

        [HttpGet]
        public async Task<IActionResult> Transferir()
        {
            
            var cuentas = await _cuentaService.ObtenerCuentasAsync();

            var viewModel = new TransferenciaViewModel
            {
                Transferencia = new TransferenciaDTO(),
                Cuentas = cuentas
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Transferir(TransferenciaViewModel viewModel)
        {
            ModelState.Remove("Cuentas");
            if (!ModelState.IsValid)
            {
                viewModel.Cuentas = await _cuentaService.ObtenerCuentasAsync();
                return View(viewModel);
            }
            try
            {
                // Llamar al servicio para realizar la transferencia
                /*await _cuentaService.TransferirAsync(
                    viewModel.Transferencia.CuentaOrigenId.Value,
                    viewModel.Transferencia.CuentaDestinoId.Value,
                    viewModel.Transferencia.Monto
                );*/
                await _cuentaService.TransferirAsync(viewModel.Transferencia);

                TempData["Mensaje"] = "Transferencia realizada con éxito.";
                return RedirectToAction("Transferir");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Error: {ex.Message}");
                viewModel.Cuentas = await _cuentaService.ObtenerCuentasAsync();
                return View(viewModel);
            }
        }
    }
}
