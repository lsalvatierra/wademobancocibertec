using Microsoft.AspNetCore.Mvc.ModelBinding;
using wademobancocibertec.Dto;
using wademobancocibertec.Models;

namespace wademobancocibertec.ViewModel
{
    public class TransferenciaViewModel
    {
        public TransferenciaDTO Transferencia { get; set; }

        public List<Cuenta> Cuentas { get; set; }
    }
}
