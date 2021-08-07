using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pjCaso71
{
    public partial class frmBoleta : Form
    {
        //Variables GLOBALES
        static int n;
        ListViewItem item;

        //Objeto de la clase Boleta
        Boleta objB = new Boleta();
        public frmBoleta()
        {
            InitializeComponent();
        }

        private void frmBoleta_Load(object sender, EventArgs e)
        {
            lblNumero.Text = generaNumero();
            txtFecha.Text = DateTime.Now.ToShortDateString();
        }

        private void cboProductos_SelectedIndexChanged(object sender, EventArgs e)
        {
            objB.producto = cboProductos.Text;
            txtPrecio.Text = objB.determinaPrecio().ToString("C");
        }

        private void btnAñadir_Click(object sender, EventArgs e)
        {
            if (valida() == "")
            {
                // capturar los datos
                capturaDatos();

                //Dterminar los calculos de la aplicacion
                double precio = objB.determinaPrecio();
                double importe = objB.calculaImporte();

                //Imprimir el detalle de la venta
                imprimirDetalle(precio, importe);

                //Imprimir el total acumulado
                lblTotal.Text = determinaTotal().ToString("C");
            }
            else
            {
                MessageBox.Show("El error se encuentra en" + valida());
            }
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            ListViewItem fila = new
            ListViewItem("2021-" + (int.Parse(lblNumero.Text).ToString("00000")));
            fila.SubItems.Add(txtFecha.Text);
            fila.SubItems.Add(totalCantidad().ToString("0.00"));
            fila.SubItems.Add(acumuladoImportes().ToString("C"));
            lvEstadisticas.Items.Add(fila);
            limpiarControles();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            DialogResult r = MessageBox.Show("Esta seguro de salir?", "Boleta", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (r == DialogResult.Yes) this.Close();
        }
        //Metodo que genra un numero aleatoria usando lambda
        Func<string> generaNumero = () =>
         {
             n++;
             return n.ToString("00000");
         };

        //Capturar los datos del formulario
        void capturaDatos()
        {
            objB.numero = int.Parse(lblNumero.Text);
            objB.cliente = txtCliente.Text;
            objB.direccion = txtDireccion.Text;
            objB.fecha = DateTime.Parse(txtFecha.Text);
            objB.dni = txtDNI.Text;
            objB.producto = cboProductos.Text;
            objB.cantidad = int.Parse(txtCantidad.Text);
        }

        void imprimirDetalle(double precio, double importe)
        {
            ListViewItem fila = new ListViewItem(objB.cantidad.ToString());
            fila.SubItems.Add(objB.producto);
            fila.SubItems.Add(precio.ToString("0.00"));
            fila.SubItems.Add(importe.ToString("0.00"));            
            lvDetalle.Items.Add(fila);
        }

        // Metodo que calcula el monto acumulado de importes
        double determinaTotal()
        {
            double total = 0;
            for(int i=0; i < lvDetalle.Items.Count; i++)
            {
                total += double.Parse(lvDetalle.Items[i].SubItems[3].Text);
            }
            return total;
        }

        // Total de prodcutos por bolets
        int totalCantidad()
        {
            int total = 0;
            for(int i=0; i < lvDetalle.Items.Count; i++)
            {
                total += int.Parse(lvDetalle.Items[i].SubItems[0].Text);
            }
            return total;
        }

        // Monto acumulado de los importes por boleta
        double acumuladoImportes()
        {
            double acumulado = 0;
            for(int i = 0; i < lvDetalle.Items.Count; i++)
            {
                acumulado += double.Parse(lvDetalle.Items[i].SubItems[3].Text);
            }
            return acumulado;
        }

        //Validar el ingreso de datos
        string valida()
        {
            if (txtCliente.Text.Trim().Length == 0)
            {
                txtCliente.Focus();
                return "nombre del cliente";
            }else if (txtDireccion.Text.Trim().Length == 0)
            {
                txtDireccion.Focus();
                return "direccion del cliente";
            } else if (txtDNI.Text.Trim().Length == 0)
            {
                txtDNI.Focus();
                return "DNI del cliente";
            }else if (cboProductos.SelectedIndex == -1)
            {
                cboProductos.Focus();
                return "descripcion del producto";
            }else if (txtCantidad.Text.Trim().Length == 0)
            {
                txtCantidad.Focus();
                return "cantidad comprada";
            }
            return "";
        }
        void limpiarControles()
        {
            lblNumero.Text = generaNumero();
            txtCliente.Clear();
            txtDNI.Clear();
            cboProductos.Text= "(Seleccione)";
            txtPrecio.Clear();
            txtCantidad.Clear();
            lvDetalle.Items.Clear();
        }

        private void lvDetalle_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            item = lvDetalle.GetItemAt(e.X, e.Y);
            String producto = lvDetalle.Items[item.Index].SubItems[1].Text;
            DialogResult r = MessageBox.Show("Esta seguro de eliminar el producto>"+producto, "Boleta", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (r == DialogResult.Yes)
            {
                lvDetalle.Items.Remove(item);
                lblTotal.Text = acumuladoImportes().ToString("C");
                MessageBox.Show("Detalle eliminado correctamente....!!!");
            }
        }
    }
}
