using System;
using wademobancocibertec.Dto;
using wademobancocibertec.Models;
using wademobancocibertec.Repositories;

namespace wademobancocibertec.Services
{
    public class CuentaService
    {
        private readonly ICuentaRepository _cuentaRepository;
        private readonly BancoDbContext _context;

        public CuentaService(BancoDbContext context, ICuentaRepository cuentaRepository)
        {
            _context = context;
            _cuentaRepository = cuentaRepository;
        }

        public async Task<List<Cuenta>> ObtenerCuentasAsync()
        {
            return await _cuentaRepository.GetCuentasAsync();
        }

        /*public async Task TransferirAsync(int cuentaOrigenId, int cuentaDestinoId, decimal monto)
        {
            await _cuentaRepository.TransferirAsync(cuentaOrigenId, cuentaDestinoId, monto);
        }*/
        public async Task TransferirAsync(TransferenciaDTO transferencia)
        {
            // Inicia la transacción
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // Restar saldo de la cuenta de origen
                await _cuentaRepository.ActualizarSaldoAsync(transferencia.CuentaOrigenId.Value, -transferencia.Monto);

                // Sumar saldo a la cuenta de destino
                await _cuentaRepository.ActualizarSaldoAsync(transferencia.CuentaDestinoId.Value, transferencia.Monto);

                // Registrar la transacción
                await _cuentaRepository.RegistrarTransaccionAsync(
                    transferencia.CuentaOrigenId.Value,
                    transferencia.CuentaDestinoId.Value,
                    transferencia.Monto
                );

                // Confirmar la transacción
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                // Revertir cambios si ocurre un error
                await transaction.RollbackAsync();
                throw;
            }
        }

    }
}
