using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica1_Compiladores2_1S2017.Analizador
{
    public abstract class Constantes
    {
        //Constantes para los tipos de Errores que pueden venir en la gramatica
        public static readonly int ERR_LEXICO = 0;
        public static readonly int ERR_SINTACTICO = 1;
        public static readonly int ERR_SEMANTICO = 2;
        public static readonly int ERR_GENERAL = 3;

        public static readonly String[] ERRORES = { "Lexico", "Sintactico" , "Semantico" , "General"};

        //Constantes para los tipos de valor
        public static readonly int T_NUM = 0;
        public static readonly int T_STR = 1;
        public static readonly int T_BOOL = 2;
        public static readonly int T_VOID = 3;
        public static readonly int T_ERROR = 4;
        public static readonly int T_DETENER = 5;
        public static readonly int T_CONTINUAR = 6;

        public static readonly String[] TIPOS = { "Numero", "Cadena", "Booleano", "Void" , "Error" };



    }
}
