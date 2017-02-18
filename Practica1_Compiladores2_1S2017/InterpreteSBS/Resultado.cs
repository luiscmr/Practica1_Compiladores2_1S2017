using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Practica1_Compiladores2_1S2017.Analizador;

namespace Practica1_Compiladores2_1S2017.InterpreteSBS
{
    class Resultado
    {
        private String valor;
        private String tipo;
        private bool retorno;

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

        public double Doble {
            get
            {
                return Convert.ToDouble(valor);
            }
        }

        public bool Boleano {
            get {
                return valor.CompareTo(Constantes.TRUE) == 0;
            }
            set
            {
                valor = (value) ? Constantes.TRUE : Constantes.FALSE;
            }
        }

        public bool esContinuar() { return tipo == Constantes.CONTINUAR; }

        public bool esDetener() { return tipo == Constantes.DETENER; }

        public bool esRetorno() { return retorno; }

        public bool esError() { return tipo == Constantes.T_ERROR; }

        public bool esFail() { return (tipo == Constantes .T_ERROR && valor.CompareTo(Constantes.VAL_FAIL)==0); }

        public bool esOk() { return (tipo == Constantes.T_VOID && valor.CompareTo(Constantes.VAL_OK) == 0); }

        public Resultado(String valor, String tipo, bool retorno)
        {
            this.Valor = valor;
            this.Tipo = tipo;
            this.retorno = retorno;
        }

        public Resultado(String valor, String tipo)
        {
            this.Valor = valor;this.Tipo = tipo; this.retorno = false;
        }

        public Resultado() {
            this.Valor = ""; this.Tipo = Constantes.T_ERROR; this.retorno = false;
        }

    }
}
