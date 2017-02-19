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
        public const String ERR_LEXICO = "Lexico";
        public const String ERR_SINTACTICO = "Sintactico";
        public const String ERR_SEMANTICO = "Semantico";
        public const String ERR_GENERAL = "General";
        public const int GLOBAL = 0;

        //Constantes para los tipos de valor
        public const String T_NUM = "Tipo Numero";
        public const String T_STR = "Tipo Cadena";
        public const String T_BOOL = "Tipo Booleando";
        public const String T_VOID = "Tipo Void";
        public const String T_ERROR = "Tipo Error";
        public const String T_DETENER = "Tipo Detener";
        public const String T_CONTINUAR = "Tipo Continuar";


        public const String VAL_FAIL = "Error en el Valor";
        public const String VAL_OK = "Valor Correcto";

        public const String NULO = "Nulo";
        public const String NUMERO = "Numero";
        public const String CADENA = "Cadena";
        public const String TRUE = "True";
        public const String FALSE = "False";
        public const String VARIABLE = "Variable";
        public const String LLAMADA = "Llamada";
        public const String ARITMETICA = "Aritmetica";
        public const String LOGICA = "Logica";
        public const String RELACIONAL = "Relacional";
        public const String EXPRESION = "Expresion";
        public const String VALOR = "Valor";
        public const String INCREMENTO = "Incremento";
        public const String DECREMENTO = "Decremento";


        public const String INICIO = "Inicio";
        public const String CUERPO_PROGRAMA = "Cuerpo Programa";
        public const String PRINCIPAL = "Principal";
        public const String FUNCION = "Funcion";
        public const String CUERPO_FUNCION = "Cuerpo Funcion";
        public const String ENCABEZADO = "Encabezado";
        public const String DEFINE = "Define";
        public const String INCERTEZA = "Incerteza";
        public const String RUTA = "Ruta";
        public const String INCLUYE = "Incluye";
        public const String DECLARACION = "Declaracion";
        public const String ASIGNACION = "Asignacion";
        public const String RETORNO = "Retorno";
        public const String MOSTRAR = "Mostrar";
        public const String SI_SINO = "Si-Sino";
        public const String SI = "Si";
        public const String SINO = "Sino";
        public const String SELECCIONA = "Selecciona";
        public const String MIENTRAS = "Mientras";
        public const String PARA = "Para";
        public const String HASTA = "Hasta";
        public const String DETENER = "Detener";
        public const String CONTINUAR = "Continuar";
        public const String DIBUJARAST = "DibujarAST";
        public const String DIBUJAREXP = "DibujarEXP";
        public const String PARAMETRO = "Parametro";
        public const String LISTA_PARAMETRO = "Parametros";
        public const String LISTA_ID = "Identificadores";
        public const String LISTA_VAL = "Valores";
        public const String DEFECTO = "Valores";
        public const String ID = "Identificador";
        public const String PRIMITIVO = "Primitivo";

        public const String OP_SUM = "Suma";
        public const String OP_RES = "Resta";
        public const String OP_MUL = "Multiplicacion";
        public const String OP_DIV = "Division";
        public const String OP_POT = "Potencia";
        public const String OP_MOD = "Modular";
        public const String OP_IGUAL = "Igual";
        public const String OP_DIF = "Diferente";
        public const String OP_MENOR = "Menor";
        public const String OP_MAYOR = "Mayor";
        public const String OP_MEN_Q = "Menor Que";
        public const String OP_MAY_Q = "Mayor Que";
        public const String OP_COMP = "Comparacion";
        public const String OP_AND = "And";
        public const String OP_OR = "Or";
        public const String OP_XOR = "Xor";
        public const String OP_NOT = "Not";

        public static readonly String[,] MT_SUMA = { 
            //T_NUM,T_STR,T_BOOL,T_VOID,T_ERROR
            { T_NUM,T_STR, T_NUM,T_ERROR,T_ERROR },//T_NUM
            { T_STR,T_STR, T_STR,T_ERROR,T_ERROR },//T_STR
            { T_NUM,T_STR, T_BOOL,T_ERROR,T_ERROR },//T_BOOL
            { T_ERROR,T_ERROR, T_ERROR,T_ERROR,T_ERROR },//T_VOID
            { T_ERROR,T_ERROR, T_ERROR,T_ERROR,T_ERROR },//T_ERROR
        };

        public static readonly String[,] MT_RESTA = { 
            //T_NUM,T_STR,T_BOOL,T_VOID,T_ERROR
            { T_NUM,T_ERROR, T_NUM,T_ERROR,T_ERROR },//T_NUM
            { T_ERROR,T_ERROR, T_ERROR,T_ERROR,T_ERROR },//T_STR
            { T_NUM,T_ERROR, T_ERROR,T_ERROR,T_ERROR },//T_BOOL
            { T_ERROR,T_ERROR, T_ERROR,T_ERROR,T_ERROR },//T_VOID
            { T_ERROR,T_ERROR, T_ERROR,T_ERROR,T_ERROR },//T_ERROR
        };

        public static readonly String[,] MT_MULTPLICACION = { 
            //T_NUM,T_STR,T_BOOL,T_VOID,T_ERROR
            { T_NUM,T_ERROR, T_NUM,T_ERROR,T_ERROR },//T_NUM
            { T_ERROR,T_ERROR, T_ERROR,T_ERROR,T_ERROR },//T_STR
            { T_NUM,T_ERROR, T_BOOL,T_ERROR,T_ERROR },//T_BOOL
            { T_ERROR,T_ERROR, T_ERROR,T_ERROR,T_ERROR },//T_VOID
            { T_ERROR,T_ERROR, T_ERROR,T_ERROR,T_ERROR },//T_ERROR
        };

        public static readonly String[,] MT_DIVISION = { 
            //T_NUM,T_STR,T_BOOL,T_VOID,T_ERROR
            { T_NUM,T_ERROR, T_NUM,T_ERROR,T_ERROR },//T_NUM
            { T_ERROR,T_ERROR, T_ERROR,T_ERROR,T_ERROR },//T_STR
            { T_NUM,T_ERROR, T_ERROR,T_ERROR,T_ERROR },//T_BOOL
            { T_ERROR,T_ERROR, T_ERROR,T_ERROR,T_ERROR },//T_VOID
            { T_ERROR,T_ERROR, T_ERROR,T_ERROR,T_ERROR },//T_ERROR
        };


        public static readonly String[,] MT_MODULO = { 
            //T_NUM,T_STR,T_BOOL,T_VOID,T_ERROR
            { T_NUM,T_ERROR, T_NUM,T_ERROR,T_ERROR },//T_NUM
            { T_ERROR,T_ERROR, T_ERROR,T_ERROR,T_ERROR },//T_STR
            { T_NUM,T_ERROR, T_ERROR,T_ERROR,T_ERROR },//T_BOOL
            { T_ERROR,T_ERROR, T_ERROR,T_ERROR,T_ERROR },//T_VOID
            { T_ERROR,T_ERROR, T_ERROR,T_ERROR,T_ERROR },//T_ERROR
        };


        public static readonly String[,] MT_POTENCIA =  { 
            //T_NUM,T_STR,T_BOOL,T_VOID,T_ERROR
            { T_NUM,T_ERROR, T_NUM,T_ERROR,T_ERROR },//T_NUM
            { T_ERROR,T_ERROR, T_ERROR,T_ERROR,T_ERROR },//T_STR
            { T_NUM,T_ERROR, T_ERROR,T_ERROR,T_ERROR },//T_BOOL
            { T_ERROR,T_ERROR, T_ERROR,T_ERROR,T_ERROR },//T_VOID
            { T_ERROR,T_ERROR, T_ERROR,T_ERROR,T_ERROR },//T_ERROR
        };


        public static readonly String[,] MT_ASIG =  { 
            //T_NUM,T_STR,T_BOOL,T_VOID,T_ERROR
            { T_NUM,T_ERROR, T_NUM,T_ERROR,T_ERROR },//T_NUM
            { T_STR,T_STR, T_STR,T_ERROR,T_ERROR },//T_STR
            { T_ERROR,T_ERROR, T_BOOL,T_ERROR,T_ERROR },//T_BOOL
            { T_ERROR,T_ERROR, T_ERROR,T_ERROR,T_ERROR },//T_VOID
            { T_ERROR,T_ERROR, T_ERROR,T_ERROR,T_ERROR },//T_ERROR
        };


        public static Boolean esAlgunoDeEstos(String rol, params String[] roles) {
            for (int i = 0; i<roles.Length;i++)
            {
                if (rol == roles[i]) { return true; }
            }
            return false;
        }


        private static int getPos(String op)
        {
            int pos = 0;
            switch (op)
            {
                case Constantes.T_NUM:
                    pos = 0;
                    break;
                case Constantes.T_STR:
                    pos = 1;
                    break;
                case Constantes.T_BOOL:
                    pos = 2;
                    break;
                case Constantes.T_VOID:
                    pos = 3;
                    break;
                case Constantes.T_ERROR:
                    pos = 4;
                    break;
                default:
                    pos = 5;
                    break;
            }
            return pos;
        }

        public static string ValidarTipos(String op1, String op2, String[,] Matriz)
        {
            //string res = ValidarTipos(Constante.T_STR,Constante.T_NUM,MT_SUMA);
            int fila = getPos(op1);
            int columna = getPos(op2);
            if (fila != 5 && columna != 5)
            {
                return Matriz[fila, columna];
            }
            else
            {
                return Constantes.T_ERROR;
            }
        }

    }
}
