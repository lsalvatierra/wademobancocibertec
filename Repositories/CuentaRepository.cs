using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using wademobancocibertec.Models;

namespace wademobancocibertec.Repositories
{
    public class CuentaRepository : ICuentaRepository
    {
        private readonly BancoDbContext _context;

        public CuentaRepository(BancoDbContext context)
        {
            _context = context;
        }

        public async Task ActualizarSaldoAsync(int cuentaId, decimal monto)
        {
            /*var parametros = new[]
            {
            new SqlParameter("@CuentaId", cuentaId),
            new SqlParameter("@Monto", monto)
            };*/

            await _context.Database.ExecuteSqlRawAsync(
                "EXEC ActualizarSaldo @CuentaId, @Monto", new SqlParameter("@CuentaId", cuentaId),
            new SqlParameter("@Monto", monto));
        }

        public async Task<Cuenta> GetCuentaByIdAsync(int id)
        {
            return await _context.Cuenta.FindAsync(id); 
        }

        public async Task<List<Cuenta>> GetCuentasAsync()
        {
            return await _context.Cuenta.ToListAsync();
        }

        public async Task RegistrarTransaccionAsync(int cuentaOrigenId, int cuentaDestinoId, decimal monto)
        {
            /*var parametros = new[]
            {
            new SqlParameter("@CuentaOrigenId", cuentaOrigenId),
            new SqlParameter("@CuentaDestinoId", cuentaDestinoId),
            new SqlParameter("@Monto", monto)
            };*/

            await _context.Database.ExecuteSqlRawAsync(
                "EXEC RegistrarTransaccion @CuentaOrigenId, @CuentaDestinoId, @Monto",
                 new SqlParameter("@CuentaOrigenId", cuentaOrigenId),
            new SqlParameter("@CuentaDestinoId", cuentaDestinoId),
            new SqlParameter("@Monto", monto));
        }

        /*public async Task TransferirAsync(int cuentaOrigenId, int cuentaDestinoId, decimal monto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // Obtener las cuentas involucradas
                var cuentaOrigen = await GetCuentaByIdAsync(cuentaOrigenId);
                var cuentaDestino = await GetCuentaByIdAsync(cuentaDestinoId);

                if (cuentaOrigen == null || cuentaDestino == null)
                    throw new Exception("Una o ambas cuentas no existen.");

                if (cuentaOrigen.Saldo < monto)
                    throw new Exception("Saldo insuficiente en la cuenta de origen.");

                // Actualizar los saldos
                cuentaOrigen.Saldo -= monto;
                cuentaDestino.Saldo += monto;

                // Guardar cambios
                await _context.SaveChangesAsync();

                // Confirmar la transacción
                await transaction.CommitAsync();
            }
            catch
            {
                // Revertir la transacción si ocurre un error
                await transaction.RollbackAsync();
                throw;
            }
        }*/
    }
}
