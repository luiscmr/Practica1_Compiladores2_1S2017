using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Irony.Parsing;
using Practica1_Compiladores2_1S2017.InterpreteSBS.Expresiones;
using Practica1_Compiladores2_1S2017.Analizador;
using Practica1_Compiladores2_1S2017.Errores;

namespace Practica1_Compiladores2_1S2017.InterpreteSBS.Instrucciones
{
    class Si : InstruccionAbstracta
    {
        public Si(ParseTreeNode Instruccion, bool PermiteInterrupciones) : base(Instruccion, PermiteInterrupciones)
        {
        }

        public Si(ParseTreeNode Instruccion) : base(Instruccion, false)
        {

        }

        override
        public Resultado ejecutar(Contexto ctx, int nivel)
        {
            var _Si = instruccion.ChildNodes[0];
            var _Sino = instruccion.ChildNodes[1];
            Resultado cond = new Expresion(_Si.ChildNodes[0].ChildNodes[0]).resolver(ctx);
            if(cond.Tipo != Constantes.T_BOOL)
            {
                ListaErrores.getInstance().setErrorSemantico(instruccion.Token.Location.Line, instruccion.Token.Location.Line, "Error de tipo", Interprete.archivo);
                return FabricarResultado.creaFail();
            }

            Resultado res = FabricarResultado.creaOk();
            bool cond_b = cond.Boleano;
            if (cond_b)
            {
                Cuerpo cp = new Cuerpo(_Si.ChildNodes[1], permiteInterrupciones);
                res = cp.ejecutar(ctx, nivel +1 );
            }else
            {
                if (_Sino.ChildNodes.Count > 0)
                {
                    Cuerpo cp = new Cuerpo(_Sino.ChildNodes[0], permiteInterrupciones);
                    res = cp.ejecutar(ctx, nivel + 1);
                }
            }
            ctx.limpiarContexto(nivel);
            return res;
        }
    }
}
