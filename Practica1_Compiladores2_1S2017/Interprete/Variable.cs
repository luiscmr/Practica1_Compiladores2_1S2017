using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica1_Compiladores2_1S2017.Interprete
{
    class Variable
    {
        private String Nombre;
        private String Valor;
        private String Tipo;
        private int Nivel;

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

    }
}
