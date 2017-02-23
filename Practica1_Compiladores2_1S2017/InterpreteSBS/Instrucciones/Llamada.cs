using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Irony.Parsing;
using Practica1_Compiladores2_1S2017.InterpreteSBS.Expresiones;
using Practica1_Compiladores2_1S2017.Errores;

namespace Practica1_Compiladores2_1S2017.InterpreteSBS.Instrucciones
{
    class Llamada : InstruccionAbstracta
    {
        public Llamada(ParseTreeNode Instruccion, bool PermiteInterrupciones) : base(Instruccion, PermiteInterrupciones)
        {
        }
        public Llamada(ParseTreeNode Instruccion) : base(Instruccion, false)
        {
        }


        public override Resultado ejecutar(Contexto ctx, int nivel)
        {
            ParseTreeNode fun = Interprete.getFuncion(instruccion, ctx);
            if(fun == null)
            {
                ListaErrores.getInstance().setErrorSemantico(instruccion.Token.Location.Line, instruccion.Token.Location.Line, "El metodo no ha sido identificado", Interprete.archivo);
                return FabricarResultado.creaFail();
            }
            Contexto contexto = new Contexto();
            Resultado ej;
            Cuerpo cuerpo;
            int cantParam = instruccion.ChildNodes[1].ChildNodes.Count;
            if (cantParam > 0)
            {
                ParseTreeNode Params = fun.ChildNodes[2];
                for(int i = 0; i<cantParam; i++)
                {
                    String tipoVar = Params.ChildNodes[i].ChildNodes[0].ToString();
                    String nombreVar = Params.ChildNodes[i].ChildNodes[1].Token.Text;
                    Resultado valor = new Expresion(instruccion.ChildNodes[1].ChildNodes[i].ChildNodes[0]).resolver(ctx);

                    Variable var = new Variable(nombreVar,valor.Valor,tipoVar,nivel);
                    contexto.setVariable(var);
                }
                cuerpo = new Cuerpo(fun.ChildNodes[3], permiteInterrupciones);
            }
            else
            {
                cuerpo = new Cuerpo(fun.ChildNodes[2],permiteInterrupciones);
            }

            ej = cuerpo.ejecutar(contexto, nivel);
            if(ej.Tipo != fun.ChildNodes[0].ToString())
            {

                ListaErrores.getInstance().setErrorSemantico(instruccion.Token.Location.Line, instruccion.Token.Location.Line, "No retorna el mismo tipo de la funcion", Interprete.archivo);
                return FabricarResultado.creaFail();
            }
            
            return ej;
        }
    }
}
