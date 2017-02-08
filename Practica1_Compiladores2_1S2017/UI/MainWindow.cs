using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Practica1_Compiladores2_1S2017.UI
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
        }

        private void bAdd_Click(object sender, EventArgs e)
        {
            string title = "Pestaña " + (tabControl.TabPages.Count + 1).ToString();
            CrearTab(title,"");
        }

        private void bClose_Click(object sender, EventArgs e)
        {
            if (tabControl.TabPages.Count > 0)
            {
                tabControl.TabPages.Remove(tabControl.SelectedTab);
            }
        }

        private void bOpen_Click(object sender, EventArgs e)
        {
            OpenFile();

        }

        private void bSave_Click(object sender, EventArgs e)
        {
            SaveFile();
        }

        private void bSaveAs_Click(object sender, EventArgs e)
        {
            SaveFileAs();
        }

        /*
            Metodo para Crear Pestañas
            recibe como parametros el titulo de la pestaña y el Texto 
        */

        public void CrearTab(String title, String text)
        {
            //---------- Nueva Pestaña(TabPage) ----------//
            TabPage newPage = new TabPage(title);
            //---------- Text box ----------//
            TextBox inner_text = new TextBox();
            inner_text.Name = "texto";
            inner_text.Text = text;
            inner_text.Multiline = true;
            inner_text.ScrollBars = ScrollBars.Vertical;
            inner_text.SetBounds(0, 0, tabControl.Width - 10, tabControl.Height - 28);

            //---------- Agregar text box a la nueva pestaña ----------//
            newPage.Controls.Add(inner_text);
            //---------- Agrega pestaña al tabcontrol ----------//
            tabControl.TabPages.Add(newPage);
            tabControl.SelectedTab = newPage;
            tabControl.ResetText();

        }


        /*
            Metodos para Administrar Archivos
                 Crear
                 Abrir
                 Guardar
                 Guardar Como
        */

       
       //----------- Abrir Archivo ----------//

        public void OpenFile() {
            OpenFileDialog OFD1 = new OpenFileDialog();
            OFD1.Filter = "SBScript Files | *.sbs";
            OFD1.Title = "Seleccione un archivo";
            if (OFD1.ShowDialog() == DialogResult.OK)
            {
                StreamReader str = new StreamReader(OFD1.FileName);
                CrearTab(OFD1.FileName, str.ReadToEnd());
            }
        }

        //---------- Guardar Archivo ------------//

        public void SaveFile() {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "SBScript Files | *.sbs";
            saveFile.Title = "Guardar Archivo";

            String title = tabControl.SelectedTab.Text;
            string search = "Pestaña";
            StringComparison comp = StringComparison.InvariantCultureIgnoreCase;
            if (title.StartsWith(search, comp) == true)
            {
                if(saveFile.ShowDialog() == DialogResult.OK)
                {
                    title = saveFile.FileName;
                }
            }

            String text = "";

            if (tabControl.SelectedTab.Controls.ContainsKey("texto")) {
                text = tabControl.SelectedTab.Controls["texto"].Text;
            }

            File.WriteAllText(title, text);

            tabControl.SelectedTab.Text = title;
            
        }

        public void SaveFileAs() {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "SBScript Files | *.sbs";
            saveFile.Title = "Guardar Archivo";

            string title = tabControl.SelectedTab.Text;

            string[] fileName = title.Split('\\');

            saveFile.FileName = fileName[fileName.Length -1 ];
            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                title = saveFile.FileName;

                String text = "";

                if (tabControl.SelectedTab.Controls.ContainsKey("texto"))
                {
                    text = tabControl.SelectedTab.Controls["texto"].Text;
                }

                File.WriteAllText(title, text);

                tabControl.SelectedTab.Text = title;
            }
            
        }



        //---------- Guardar Archivo Como -----------//



    }
}
