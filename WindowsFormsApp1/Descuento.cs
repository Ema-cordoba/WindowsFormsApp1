using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public abstract class Descuento
    {
        public int Codigo { get; set; }
        public string Descripcion { get; set; }
        public abstract decimal CalcularDescuento(decimal pImporte);
    }
}
