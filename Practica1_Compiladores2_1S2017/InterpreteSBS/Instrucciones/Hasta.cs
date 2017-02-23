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
    class Hasta : InstruccionAbstracta
    {
        public Hasta(ParseTreeNode Instruccion, bool PermiteInterrupciones) : base(Instruccion, PermiteInterrupciones)
        {
        }

        public override Resultado ejecutar(Contexto ctx, int nivel)
        {

            Resultado exec = null;

            while (true)
            {
                Resultado condicion = new Expresion(instruccion.ChildNodes[0].ChildNodes[0]).resolver(ctx);
                if (condicion.Tipo != Constantes.T_BOOL)
                {
                    ListaErrores.getInstance().setErrorSemantico(instruccion.Token.Location.Line, instruccion.Token.Location.Line, "No devuelve un valor booleano la expresion", Interprete.archivo);
                    return FabricarResultado.creaFail();
                }
                bool cond = condicion.Boleano;
                if (cond)
                {
                    exec = FabricarResultado.creaOk();
                    break;
                }
                Cuerpo cuerpo = new Cuerpo(instruccion.ChildNodes[1], true);
                exec = cuerpo.ejecutar(ctx, nivel);

                if (exec.esContinuar())
                {
                    continue;
                }
                if (exec.esDetener())
                {
                    exec = FabricarResultado.creaOk();
                    break;
                }
                if (exec.esRetorno())
                {
                    break;
                }
                ctx.limpiarContexto(nivel);

            }
            ctx.limpiarContexto(nivel);
            return exec;

        }
    }
}
