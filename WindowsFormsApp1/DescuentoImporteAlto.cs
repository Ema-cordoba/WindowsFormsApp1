using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class DescuentoImporteAlto : Descuento
    {
        public decimal Importe { get; set; }

        public override decimal CalcularDescuento(decimal pImporte=0)
        {
            return Importe;
        }
    }
}
