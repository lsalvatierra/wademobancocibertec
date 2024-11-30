using wademobancocibertec.Models;

namespace wademobancocibertec.Repositories
{
    public interface ICuentaRepository
    {
        Task<List<Cuenta>> GetCuentasAsync();
        Task<Cuenta> GetCuentaByIdAsync(int id);
        /*Task TransferirAsync(int cuentaOrigenId, int cuentaDestinoId, decimal monto);*/

        Task ActualizarSaldoAsync(int cuentaId, decimal monto);
        Task RegistrarTransaccionAsync(int cuentaOrigenId, int cuentaDestinoId, decimal monto);



    }
}
