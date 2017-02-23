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
        
        public static Resultado creaDetener()
        {
            return new Resultado("", Constantes.T_DETENER);
        }
        
        public static Resultado creaContinuar()
        {
            return new Resultado("", Constantes.T_CONTINUAR);
        }
        
        public static Resultado creaCadena(String cadena)
        {
            return new Resultado(cadena, Constantes.T_STR);
        }


        public static Resultado creaNumero(String numero)
        {
            if (numero.EndsWith(".0"))
            {
                numero = numero.Replace(".0", "");
            }
            return new Resultado(numero, Constantes.T_NUM);
        }
        
        public static Resultado creaBooleano(bool b)
        {
            Resultado res = new Resultado("", Constantes.T_BOOL);
            res.Boleano = b;
            return res;
        }
    }
}
