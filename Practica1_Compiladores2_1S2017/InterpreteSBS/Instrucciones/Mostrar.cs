using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Practica1_Compiladores2_1S2017.Analizador;
using Practica1_Compiladores2_1S2017.InterpreteSBS.Expresiones;
using Irony.Parsing;
using System.Text.RegularExpressions;

namespace Practica1_Compiladores2_1S2017.InterpreteSBS.Instrucciones
{
    class Mostrar : InstruccionAbstracta
    {
        public Mostrar(ParseTreeNode Instruccion) : base(Instruccion, false) { }

        override
        public Resultado ejecutar(Contexto ctx, int nivel)
        {
            string cad = instruccion.ChildNodes[0].Token.Text;
            string Cadena = cad.Substring(1, cad.Length - 2);

            string[] param = Regex.Split(Cadena, @"\{\d+\}");
            int tamParamCad = param.Length - 1;
            int tamParam = instruccion.ChildNodes[1].ChildNodes.Count;
            String result = "";

            if (tamParam >= 1)
            {
                if(tamParam == tamParamCad)
                {
                    for (int i = 0; i <= tamParam; i++)
                    {
                        result += param[i];
                        if (i < tamParamCad)
                        {
                            Resultado res = new Expresion(instruccion.ChildNodes[1].ChildNodes[i].ChildNodes[0]).resolver(ctx);
                            if (res.Tipo == Constantes.T_ERROR)
                            {
                                //error
                                continue;
                            }
                            result += res.Valor;
                        }
                    }
                    Interprete.ConcatenarSalida(result);
                }
                else
                {
                    //error no vinieron la misma cantidad de parametros
                }
            }
            else
            {
                Interprete.ConcatenarSalida(Cadena);
            }
            Interprete.ConcatenarSalida("\r\n");
            return FabricarResultado.creaOk();
        }
    }
}
