using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace WindowsFormsApp1
{
    public class SocioPrincipal : Socio
    {
        public SocioPrincipal()
        {
            ld = new List<Descuento>();
        }
        public override decimal ValorCuota()
        {
            decimal rdo = 0;
            try
            {
                var axosAntiguedad=  DateAndTime.DateDiff(DateInterval.Year, FechaIngreso, DateTime.Now.Date);
                decimal importeAntiguedad = axosAntiguedad * 1000;
                decimal valorFinal = 8000 - importeAntiguedad < 6000 ? 6000 : 8000 - importeAntiguedad;
                foreach (var d in ld)
                {
                   valorFinal-= d.CalcularDescuento(valorFinal);
                }
                rdo = valorFinal < 0 ? 0 : valorFinal;
            }   
           
            catch (Exception ex) { throw ex; }
            return rdo;
        }
    }
}
