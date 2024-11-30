using System;
using System.Collections.Generic;

namespace wademobancocibertec.Models;

public partial class Transaccion
{
    public int Id { get; set; }

    public int CuentaOrigenId { get; set; }

    public int CuentaDestinoId { get; set; }

    public decimal Monto { get; set; }

    public DateTime Fecha { get; set; }

    public virtual Cuenta CuentaDestino { get; set; } = null!;

    public virtual Cuenta CuentaOrigen { get; set; } = null!;
}
