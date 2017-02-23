using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Irony.Parsing;
using Practica1_Compiladores2_1S2017.Analizador;

namespace Practica1_Compiladores2_1S2017.InterpreteSBS.Instrucciones
{
    class GraficarAST : InstruccionAbstracta
    {
        public GraficarAST(ParseTreeNode Instruccion, bool PermiteInterrupciones) : base(Instruccion, PermiteInterrupciones)
        {
        }

        public GraficarAST(ParseTreeNode Instruccion) : base(Instruccion, false)
        {
        }

        override
        public Resultado ejecutar(Contexto ctx, int nivel)
        {

            String Nombre = instruccion.ChildNodes[0].Token.Text;
            List<ParseTreeNode> ListaFunciones = Interprete.getListaMetodos(Nombre);
            if(ListaFunciones != null)
            {
                /*Nombre = Interprete.Ruta + "\\EXP_" + Nombre + ".png";
                Gramatica.getEXP(fun, Nombre);
                Interprete.NoEXP++;
                */
                int i = 0;
                String Ruta = "";
                foreach(var nodo in ListaFunciones)
                {
                    Ruta = Interprete.Ruta + "AST_" + Nombre + i + ".png";
                    Gramatica.getAST(nodo, Ruta);
                    i++;
                }

            }
            return FabricarResultado.creaOk();
        }
    }
}
