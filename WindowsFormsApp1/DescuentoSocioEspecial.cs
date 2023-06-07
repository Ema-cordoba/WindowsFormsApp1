using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class DescuentoSocioEspecial : Descuento
    {
        public decimal Porcentaje { get; set; }

        public override decimal CalcularDescuento(decimal pImporte)
        {
            return pImporte  * Porcentaje / 100;
        }
    }
}
