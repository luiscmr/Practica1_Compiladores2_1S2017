using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Irony.Parsing;
using Practica1_Compiladores2_1S2017.InterpreteSBS.Expresiones;

namespace Practica1_Compiladores2_1S2017.InterpreteSBS.Instrucciones
{
    class Retorno : InstruccionAbstracta
    {
        public Retorno(ParseTreeNode Instruccion, bool PermiteInterrupciones) : base(Instruccion, PermiteInterrupciones)
        {
        }


        public Retorno(ParseTreeNode Instruccion) : base(Instruccion, false)
        {
        }

        public override Resultado ejecutar(Contexto ctx, int nivel)
        {
            Resultado res = new Expresion(instruccion.ChildNodes[0]).resolver(ctx);
            return FabricarResultado.creaRetorno(res.Valor, res.Tipo);
        }
    }
}
