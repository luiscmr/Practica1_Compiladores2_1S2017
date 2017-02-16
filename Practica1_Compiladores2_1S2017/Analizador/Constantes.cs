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
        public static readonly String ERR_LEXICO = "Lexico";
        public static readonly String ERR_SINTACTICO = "Sintactico";
        public static readonly String ERR_SEMANTICO = "Semantico";
        public static readonly String ERR_GENERAL = "General";
        

        //Constantes para los tipos de valor
        public static readonly String T_NUM = "Tipo Numero";
        public static readonly String T_STR = "Tipo Cadena";
        public static readonly String T_BOOL = "Tipo Booleando";
        public static readonly String T_VOID = "Tipo Void";
        public static readonly String T_ERROR = "Tipo Error";
        public static readonly String T_DETENER = "Tipo Detener";
        public static readonly String T_CONTINUAR = "Tipo Continuar";

        public static readonly String NULO = "Nulo";
        public static readonly String NUMERO = "Numero";
        public static readonly String CADENA = "Cadena";
        public static readonly String TRUE = "True";
        public static readonly String FALSE = "False";
        public static readonly String VARIABLE = "Variable";
        public static readonly String LLAMADA = "Llamada";
        public static readonly String ARITMETICA = "Aritmetica";
        public static readonly String LOGICA = "Logica";
        public static readonly String RELACIONAL = "Relacional";
        public static readonly String EXPRESION = "Expresion";
        public static readonly String VALOR = "Valor";
        public static readonly String INCREMENTO = "Incremento";
        public static readonly String DECREMENTO = "Decremento";


        public static readonly String INICIO = "Inicio";
        public static readonly String CUERPO_PROGRAMA = "Cuerpo Programa";
        public static readonly String PRINCIPAL = "Principal";
        public static readonly String FUNCION = "Funcion";
        public static readonly String CUERPO_FUNCION = "Cuerpo Funcion";
        public static readonly String ENCABEZADO = "Encabezado";
        public static readonly String DEFINE = "Define";
        public static readonly String INCERTEZA = "Incerteza";
        public static readonly String RUTA = "Ruta";
        public static readonly String INCLUYE = "Incluye";
        public static readonly String DECLARACION = "Declaracion";
        public static readonly String ASIGNACION = "Asignacion";
        public static readonly String RETORNO = "Retorno";
        public static readonly String MOSTRAR = "Mostrar";
        public static readonly String SI_SINO = "Si-Sino";
        public static readonly String SI = "Si";
        public static readonly String SINO = "Sino";
        public static readonly String SELECCIONA = "Selecciona";
        public static readonly String MIENTRAS = "Mientras";
        public static readonly String PARA = "Para";
        public static readonly String HASTA = "Hasta";
        public static readonly String DETENER = "Detener";
        public static readonly String CONTINUAR = "Continuar";
        public static readonly String DIBUJARAST = "DibujarAST";
        public static readonly String DIBUJAREXP = "DibujarEXP";
        public static readonly String PARAMETRO = "Parametro";
        public static readonly String LISTA_PARAMETRO = "Parametros";
        public static readonly String LISTA_ID = "Identificadores";
        public static readonly String LISTA_VAL = "Valores";
        public static readonly String DEFECTO = "Valores";

        public static readonly String OP_SUM = "Suma";
        public static readonly String OP_RES = "Resta";
        public static readonly String OP_MUL = "Multiplicacion";
        public static readonly String OP_DIV = "Division";
        public static readonly String OP_POT = "Potencia";
        public static readonly String OP_MOD = "Modular";
        public static readonly String OP_IGUAL = "Igual";
        public static readonly String OP_DIF = "Diferente";
        public static readonly String OP_MENOR = "Menor";
        public static readonly String OP_MAYOR = "Mayor";
        public static readonly String OP_MEN_Q = "Menor Que";
        public static readonly String OP_MAY_Q = "Mayor Que";
        public static readonly String OP_COMP = "Comparacion";
        public static readonly String OP_AND = "And";
        public static readonly String OP_OR = "Or";
        public static readonly String OP_XOR = "Xor";
        public static readonly String OP_NOT = "Not";




    }
}
