using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public abstract class Socio
    {
        protected List<Descuento> ld;
        
        public string  Legajo { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime FechaIngreso { get; set; }



        public void AsignarDescuento(Descuento pDto)
        {
            try
            {
                ld.Add(pDto);
            }
            catch (Exception ex) { throw ex; }

        }
        public void DesasignarDescuento(Descuento pDto)
        {
            try
            {
                //ld.Remove(pDto);
                ld.Remove(ld.Find(x => x.Codigo == pDto.Codigo));

            }
            catch (Exception ex) { throw ex; }

        }

        public abstract decimal ValorCuota();
    }
}
