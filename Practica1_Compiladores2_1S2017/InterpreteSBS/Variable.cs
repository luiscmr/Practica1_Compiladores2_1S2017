using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica1_Compiladores2_1S2017.InterpreteSBS
{
    class Variable
    {
        private String nombre;
        private String valor;
        private String tipo;
        private int nivel;

        public string Nombre
        {
            get
            {
                return nombre;
            }

            set
            {
                nombre = value;
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

        public int Nivel
        {
            get
            {
                return nivel;
            }

            set
            {
                nivel = value;
            }
        }

        public Variable(String Nombre, String Valor, String Tipo, int Ambito)
        {
            this.Nombre = Nombre;
            this.Valor = Valor;
            this.Tipo = Tipo;
            this.Nivel = Ambito;
        }

        public Variable(String Nombre, String Tipo, int Ambito)
        {
            this.Nombre = Nombre;
            this.Valor = null;
            this.Tipo = Tipo;
            this.Nivel = Ambito;
        }

        public override string ToString()
        {
            return "Variable{" + "nombre=" + nombre + ", valor=" + valor + ", tipo=" + tipo + ", nivel=" + nivel + '}';
        }

    }
}
