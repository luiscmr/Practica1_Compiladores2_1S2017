using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica1_Compiladores2_1S2017.Errores
{
    public class Error
    {
        int linea;
        int columna;
        String tipo;
        String descripcion;
        String archivo;

        public int Linea
        {
            get
            {
                return linea;
            }

            set
            {
                linea = value;
            }
        }

        public int Columna
        {
            get
            {
                return columna;
            }

            set
            {
                columna = value;
            }
        }

        public string Tipo
        {
            get
            {
                return tipo;
            }

            set
            {
                tipo = value;
            }
        }

        public string Descripcion
        {
            get
            {
                return descripcion;
            }

            set
            {
                descripcion = value;
            }
        }

        public string Archivo
        {
            get
            {
                return archivo;
            }

            set
            {
                archivo = value;
            }
        }

        public Error(int linea, int columna, String tipo, String descripcion, String archivo)
        {
            this.Linea = linea;
            this.Columna = columna;
            this.Tipo = tipo;
            this.Descripcion = descripcion;
            this.Archivo = archivo;
        }
        
    }
}
