using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Practica1_Compiladores2_1S2017.Analizador;

namespace Practica1_Compiladores2_1S2017.Errores
{
    class ListaErrores
    {
        private static ListaErrores Errores = null;
        private List<Error> listaerrores = null;

        private ListaErrores() {
            listaerrores = new List<Error>();
        }

        public static ListaErrores getInstance()
        {
            if(Errores == null)
            {
                Errores = new ListaErrores();
            }
            return Errores;
        }

        public static ListaErrores resetInstance() {
            Errores = new ListaErrores();
            return Errores;
        }

        public void setErrorSintactico(int linea, int columna, string descripcion, string archivo) {
            Error er = new Error(linea, columna, Constantes.ERR_LEXICO, descripcion, archivo);
            listaerrores.Add(er);
        }

        public void setErrorSemantico(int linea, int columna, string descripcion, string archivo)
        {
            Error er = new Error(linea, columna, Constantes.ERR_SEMANTICO, descripcion, archivo);
            listaerrores.Add(er);
        }


        public void nuevoErrorGeneral(string descripcion, string archivo)
        {
            Error er = new Error(-1, -1, Constantes.ERR_GENERAL, descripcion, archivo);
            listaerrores.Add(er);
        }

        public List<Error> getReporte()
        {
            return listaerrores;
        }

    }
}
