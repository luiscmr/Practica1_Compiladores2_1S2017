using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Irony.Parsing;

namespace Practica1_Compiladores2_1S2017.InterpreteSBS
{
    class Encabezado
    {
        private String valor;
        private ParseTreeNode raiz;
        private String archivo;

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

        public string Valor
        {
            get
            {
                return valor;
            }

            set
            {
                valor = value;
            }
        }

        public ParseTreeNode Raiz
        {
            get
            {
                return raiz;
            }

            set
            {
                raiz = value;
            }
        }

        public Encabezado( String Valor, String archivo) {
            this.Valor = Valor;
            this.archivo = archivo;
            this.Raiz = null;
        }

    }
}
