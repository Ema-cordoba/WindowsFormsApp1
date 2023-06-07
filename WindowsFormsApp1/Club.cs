using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class Club
    {
        List<Socio> ls;
        List<Descuento> ld;
        public Club()
        {
            ls = new List<Socio>();
            ld = new List<Descuento>();
        }
        public void AgregarSocio(Socio pSocio)
        {
            try
            {
                ls.Add(pSocio); 
            }
            catch (Exception ex) { throw ex; }
                
        }
        public void AgregarDescuento(Descuento pDto)
        {
            try
            {
                ld.Add(pDto);
            }
            catch (Exception ex) { throw ex; }

        }
        public void BorrarSocio(Socio pSocio)
        {

            try
            {
                ls.Remove(pSocio);
            }
            catch (Exception ex) { throw ex; }

        }
        public void BorrarDescuento(Descuento pDto)
        {

            try
            {
                ld.Remove(pDto);
            }
            catch (Exception ex) { throw ex; }

        }
        public void ModificarSocio(Socio pSocio)
        {

            try
            {
                Socio s= ls.Find(x => x.Legajo == pSocio.Legajo);
                if(s!=null)
                {
                    s.Nombre = pSocio.Nombre;
                    s.Apellido = pSocio.Apellido;
                    s.FechaIngreso = pSocio.FechaIngreso;
                }
            }
            catch (Exception ex) { throw ex; }

        }
        public void ModificarDescuento(Descuento pDto)
        {

            try
            {
                Descuento d = ld.Find(x => x.Codigo == pDto.Codigo);
                if (d != null)
                {
                    
                    d.Descripcion = pDto.Descripcion;
                    if (pDto is DescuentoImporteAlto)
                    { (d as DescuentoImporteAlto).Importe = (pDto as DescuentoImporteAlto).Importe; }
                    else { (d as DescuentoSocioEspecial).Porcentaje = (pDto as DescuentoSocioEspecial).Porcentaje; }
                 
                }
            }
            catch (Exception ex) { throw ex; }

        }
        public List<Socio> RetornaSocios() { return ls; } 
        public List<Descuento> RetornaDescuentos(){ return ld; }
        
    }
}
