using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
       
        Club club;
        
        
        public Form1()
        {
            InitializeComponent();
            club = new Club();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.MultiSelect = true;
            dataGridView2.MultiSelect = true;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void Mostrar(DataGridView pDGV, object pO) 
        { 
            pDGV.DataSource = null;pDGV.DataSource = pO; 
            if(pDGV.Name=="dataGridView2")
            {
                pDGV.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                pDGV.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            }
        }
        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                string legajo = Interaction.InputBox("Legajo: ");
                // Validación del legajo Forma 1
                var re = new Regex(@"[A-Z]\d{3}[a-z]");
                if (!re.IsMatch(legajo)) throw new Exception("El legajo no posee el formato correcto");
                // Validación del legajo Forma 2
                //char letra1 = char.Parse(legajo.Substring(0, 1));
                //int numero = int.Parse(legajo.Substring(1, 3));
                //char letra2 = char.Parse(legajo.Substring(4, 1));
                //if(!((letra1>=65 && letra1<=90) && (numero>=000 && numero <=999) && (letra2 >= 97 && letra2 <= 122))) throw new Exception("El legajo no posee el formato correcto");
                /////
                // Verificacón de Legajo existente
                if(club.RetornaSocios().Exists(x => x.Legajo==legajo)) throw new Exception("El legajo ya existe !!!");
                string nombre = Interaction.InputBox("Nombre: ");
                string apellido = Interaction.InputBox("Apellido: ");
                string fechaIngreso = Interaction.InputBox("fecha de Ingreso: ");
                // Validación de la fecha
                if (!Information.IsDate(fechaIngreso)) throw new Exception("Fecha de ingreso inválida !!!");
                // Se instancia el tipo de socio que corresponde
                Socio s = null;
                if (radioButton1.Checked) { s = new SocioPrincipal(); } else { s = new SocioFamiliar(); }
                // Se carga el estado del socio
                s.Legajo = legajo;s.Nombre = nombre;s.Apellido = apellido;s. FechaIngreso = DateTime.Parse(fechaIngreso);
                // Se agrega el socio al club
                club.AgregarSocio(s);
                Mostrar(dataGridView1, club.RetornaSocios());
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

        }

        private void button2_Click(object sender, EventArgs e)
        {

            try
            {
                if(dataGridView1.Rows.Count==0) throw new Exception("No hay socios para modificar");
                Socio s = dataGridView1.SelectedRows[0].DataBoundItem as Socio;
                s.Nombre=Interaction.InputBox("Nombre: ","Modificando el nombre...",s.Nombre);
                s.Apellido = Interaction.InputBox("Apellido: ", "Modificando el apellido...", s.Apellido);
                string fechaIngreso = Interaction.InputBox("fecha de Ingreso: ", "Modificando la fecha de ingreso...", s.FechaIngreso.ToShortDateString());
                // Validación de la fecha
                if (!Information.IsDate(fechaIngreso)) throw new Exception("Fecha de ingreso inválida !!!");
                s.FechaIngreso = DateTime.Parse(fechaIngreso);
                club.ModificarSocio(s);
                Mostrar(dataGridView1, club.RetornaSocios());
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

        }

        private void button3_Click(object sender, EventArgs e)
        {

            try
            {
                if (dataGridView1.Rows.Count == 0) throw new Exception("No hay socios para modificar");
                club.BorrarSocio(dataGridView1.SelectedRows[0].DataBoundItem as Socio);
                Mostrar(dataGridView1, club.RetornaSocios());
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                string codigo = Interaction.InputBox("Código: ");
                // Validación del código
                if (!Information.IsNumeric(codigo)) throw new Exception("Código Incorrecto !!!");
                /////
                // Verificacón de código existente
                if (club.RetornaDescuentos().Exists(x => x.Codigo == int.Parse(codigo))) throw new Exception("El codigo ya existe !!!");
                string descripcion = Interaction.InputBox("Descripción: ");
                // Se instancia el tipo de descuento que corresponde
                Descuento d = null;
                if (radioButton4.Checked) { d = new DescuentoImporteAlto(); } else { d = new DescuentoSocioEspecial(); }
                // Se carga el estado del socio
                d.Codigo = int.Parse(codigo);d.Descripcion = descripcion;
                // Si es dto Importe alto cargo el importe
                if (radioButton4.Checked) 
                {
                    string importe = Interaction.InputBox("Importe: ");
                    if (!Information.IsNumeric(importe)) throw new Exception("El importe es incorrecto !!!");
                    (d as DescuentoImporteAlto).Importe = decimal.Parse(importe);
                }
                else // Caso contrario cargo el porcentaje
                {
                    string porcentaje = Interaction.InputBox("Porcentaje entre 0 y 100: ");
                    if (!Information.IsNumeric(porcentaje)) throw new Exception("El porcentaje no es numérico !!!");
                    if(decimal.Parse(porcentaje)<0 || decimal.Parse(porcentaje)>100) throw new Exception("El porcentaje debe ser mayor a 0 y menor o igual a 100 !!!");
                    (d as DescuentoSocioEspecial).Porcentaje = decimal.Parse(porcentaje);
                }
                
                // Se agrega el socio al clubv
                club.AgregarDescuento(d);
                var ie = from dd in club.RetornaDescuentos() select new { Codigo = dd.Codigo, 
                                                                          Descripción = dd.Descripcion, 
                                                                          Importe = dd is DescuentoImporteAlto ? (dd as DescuentoImporteAlto).Importe.ToString() : "--", 
                                                                          Porcentaje = dd is DescuentoSocioEspecial ? (dd as DescuentoSocioEspecial).Porcentaje.ToString() : "--" };
                Mostrar(dataGridView2, ie.ToList());
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void button5_Click(object sender, EventArgs e)
        {

            try
            {
                if (dataGridView2.Rows.Count == 0) throw new Exception("No hay descuentos para modificar");
                Descuento d = club.RetornaDescuentos().Find(x=>x.Codigo==int.Parse(dataGridView2.SelectedRows[0].Cells[0].Value.ToString()));
                if(d is null) throw new Exception("Error al modificar !!!");
                d.Descripcion = Interaction.InputBox("Descripción: ", "Modificando la descripción...", d.Descripcion);
                // Si es dto Importe alto cargo el importe
                if (d is DescuentoImporteAlto)
                {
                    string importe = Interaction.InputBox("Importe: ","Modificando el importe...",(d as DescuentoImporteAlto).Importe.ToString());
                    if (!Information.IsNumeric(importe)) throw new Exception("El importe es incorrecto !!!");
                    (d as DescuentoImporteAlto).Importe = decimal.Parse(importe);
                }
                else // Caso contrario cargo el porcentaje
                {
                    string porcentaje = Interaction.InputBox("Porcentaje entre 0 y 100: ","Modificando el porcentaje...",(d as DescuentoSocioEspecial).Porcentaje.ToString());
                    if (!Information.IsNumeric(porcentaje)) throw new Exception("El porcentaje no es numérico !!!");
                    if (decimal.Parse(porcentaje) < 0 || decimal.Parse(porcentaje) > 100) throw new Exception("El porcentaje debe ser mayor a 0 y menor o igual a 100 !!!");
                    (d as DescuentoSocioEspecial).Porcentaje = decimal.Parse(porcentaje);
                }
                // Se modifica el socio al club No hace falta llamar al método Modificardescuento
                // del club porque ya estamos tyrabajando con nla lista que nos retorna el club.
                // Si el método RetornaDescuento del club nos devuelve un clon de su lista sería
                // Necesario ejecutar la siguiente línea de código comentada
                //club.ModificarDescuento(d);
                var ie = from dd in club.RetornaDescuentos()
                         select new
                         {
                             Codigo = dd.Codigo,
                             Descripción = dd.Descripcion,
                             Importe = dd is DescuentoImporteAlto ? (dd as DescuentoImporteAlto).Importe.ToString() : "--",
                             Porcentaje = dd is DescuentoSocioEspecial ? (dd as DescuentoSocioEspecial).Porcentaje.ToString() : "--"
                         };
                Mostrar(dataGridView2, ie.ToList());

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

        }

        private void button4_Click(object sender, EventArgs e)
        {

            try
            {
                if (dataGridView2.Rows.Count == 0) throw new Exception("No hay descuentos para modificar");
                Descuento d = club.RetornaDescuentos().Find(x => x.Codigo == int.Parse(dataGridView2.SelectedRows[0].Cells[0].Value.ToString()));
                if (d is null) throw new Exception("Error al modificar !!!");
                club.BorrarDescuento(d);
                var ie = from dd in club.RetornaDescuentos()
                         select new
                         {
                             Codigo = dd.Codigo,
                             Descripción = dd.Descripcion,
                             Importe = dd is DescuentoImporteAlto ? (dd as DescuentoImporteAlto).Importe.ToString() : "--",
                             Porcentaje = dd is DescuentoSocioEspecial ? (dd as DescuentoSocioEspecial).Porcentaje.ToString() : "--"
                         };
                Mostrar(dataGridView2, ie.ToList());

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

        }
    }

}
