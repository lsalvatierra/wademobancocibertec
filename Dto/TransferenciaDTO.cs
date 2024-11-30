using System.ComponentModel.DataAnnotations;

namespace wademobancocibertec.Dto
{
    public class TransferenciaDTO
    {

        [Required(ErrorMessage = "Seleccione la cuenta de origen.")]
        public int? CuentaOrigenId { get; set; }

        [Required(ErrorMessage = "Seleccione la cuenta de destino.")]
        public int? CuentaDestinoId { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "El monto debe ser mayor a cero.")]
        public decimal Monto { get; set; }
    }
}
