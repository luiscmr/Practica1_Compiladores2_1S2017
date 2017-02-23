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
            if(instruccion.ChildNodes.Count>0){ 
                string cad = instruccion.ChildNodes[0].Token.Text;
                //            string Cadena = cad.Substring(1, cad.Length - 2);

                if (instruccion.ChildNodes.Count == 1)
                {
                    Interprete.ConcatenarSalida(cad);
                    Interprete.ConcatenarSalida("\r\n");
                    return FabricarResultado.creaOk();
                }
                ParseTreeNode Param = instruccion.ChildNodes[1];

                List<String> valores = new List<String>();

                foreach (var parametro in Param.ChildNodes)
                {
                    Resultado res = new Expresion(parametro.ChildNodes[0]).resolver(ctx);
                    String tempval = "";
                    if (res.Tipo == Constantes.T_ERROR)
                    {

                    }
                    tempval = res.Valor;
                    valores.Add(tempval);
                }

                String[] parametrosMostrar = valores.ToArray();

                String resultado = String.Format(cad, parametrosMostrar);

                Interprete.ConcatenarSalida(resultado);
                Interprete.ConcatenarSalida("\r\n");
            }
            return FabricarResultado.creaOk();
        }
    }
}
