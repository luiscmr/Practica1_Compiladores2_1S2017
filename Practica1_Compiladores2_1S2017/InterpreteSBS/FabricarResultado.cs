using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Practica1_Compiladores2_1S2017.Analizador;

namespace Practica1_Compiladores2_1S2017.InterpreteSBS
{
    abstract class FabricarResultado
    {
        public static Resultado creaOk()
        {
            return new Resultado(Constantes.VAL_OK, Constantes.T_VOID);
        }

        public static Resultado creaFail()
        {
            return new Resultado(Constantes.VAL_FAIL, Constantes.T_ERROR);
        }

        public static Resultado creaRetorno(String valor, String tipo)
        {
            return new Resultado(valor, tipo, true);
        }

        /**
         * Crea un resultado con el tipo Constantes.T_DETENER.
         * @return El resultado DETENER.
         */
        public static Resultado creaDetener()
        {
            return new Resultado("", Constantes.T_DETENER);
        }

        /**
         * Crea un resultado con el tipo Constantes.T_CONTINUAR.
         * @return El resultado CONTINUAR.
         */
        public static Resultado creaContinuar()
        {
            return new Resultado("", Constantes.T_CONTINUAR);
        }

        /**
         * Crea un resultado con el tipo Constantes.T_STR y con el valor indicado
         * por el atributo cadena.
         * @param cadena Valor de la cadena.
         * @return El resultado CADENA.
         */
        public static Resultado creaCadena(String cadena)
        {
            return new Resultado(cadena, Constantes.T_STR);
        }

        /**
         * Crea un resultado con el tipo Constantes.T_NUM y con el valor numerico
         * que trae el parametro numero, si la cadena termina con ".0" se elimina
         * el ".0" para que el valor pueda mostrarse como entero (sin parte decimal).
         * @param numero Valor del numero.
         * @return El resultado NUMERO.
         */
        public static Resultado creaNumero(String numero)
        {
            if (numero.EndsWith(".0"))
            {
                numero = numero.Replace(".0", "");
            }
            return new Resultado(numero, Constantes.T_NUM);
        }

        /**
         * Crea un resultado con el tipo Constantes.T_BOOL y se modifica la cadena
         * segun el valor del parametro bool.
         * @param bool Valor del booleano.
         * @return El resultado BOOLEANO.
         */
        public static Resultado creaBooleano(bool b)
        {
            Resultado res = new Resultado("", Constantes.T_BOOL);
            res.Boleano = b;
            return res;
        }
    }
}
