using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Practica1_Compiladores2_1S2017.Errores;

namespace Practica1_Compiladores2_1S2017.UI
{
    public partial class ReporteErrores : Form
    {
        public ReporteErrores()
        {
            InitializeComponent();
            List<Error> lista = ListaErrores.getInstance().getReporte();
            dataGridView1.DataSource = lista;
        }
        

    }
}
