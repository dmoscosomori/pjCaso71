using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pjCaso71
{
    public class Boleta
    {
        public int numero { get; set; }
        public string cliente { get; set; }
        public string direccion { get; set; }
        public DateTime fecha { get; set; }
        public string dni { get; set; }
        public string producto { get; set; }
        public int cantidad { get; set; }

        //Metodo para determinar el precio del producto

        public double determinaPrecio()
        {
            switch (producto)
            {
                case "PS4 + 1 MANDO DS4": return 2049;
                case "PS4 + 2 MANDO DS4": return 1899;
                case "PS3 (500GB)":return 1349; 
                case "MANDO PS4/DS4": return 219;
                case "MANDO PS3/DS4": return 199;
            }
            return 0;
        }
        // Metodo para determinar el importe

        public double calculaImporte()
        {
            return cantidad * determinaPrecio();
        }
    }
}
