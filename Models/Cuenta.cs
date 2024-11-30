using System;
using System.Collections.Generic;

namespace wademobancocibertec.Models;

public partial class Cuenta
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public decimal Saldo { get; set; }

    public virtual ICollection<Transaccion> TransaccionCuentaDestino { get; set; } = new List<Transaccion>();

    public virtual ICollection<Transaccion> TransaccionCuentaOrigen { get; set; } = new List<Transaccion>();
}
