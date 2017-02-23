using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Irony.Parsing;
using Practica1_Compiladores2_1S2017.Analizador;
using Practica1_Compiladores2_1S2017.InterpreteSBS.Expresiones;
using Practica1_Compiladores2_1S2017.Errores;

namespace Practica1_Compiladores2_1S2017.InterpreteSBS.Instrucciones
{
    class Para : InstruccionAbstracta
    {
        public Para(ParseTreeNode Instruccion, bool PermiteInterrupciones) : base(Instruccion, PermiteInterrupciones)
        {
        }

        public override Resultado ejecutar(Contexto ctx, int nivel)
        {
            //Obiene nombre de la variable, el valor, si es incremento o decremento y el cuerpo de la funcion
            String nombreVar = instruccion.ChildNodes[0].Token.Text;
            String tipoVar = Constantes.T_NUM;
            Resultado ValorVar = new Expresion(instruccion.ChildNodes[1].ChildNodes[0]).resolver(ctx);
            ParseTreeNode NodoCuerpo = instruccion.ChildNodes[4];
            String inc_dec = instruccion.ChildNodes[3].ToString();
            //si el valor de la variable no es el mismo a number se sale
            String castValue = "";
            switch (tipoVar)
            {
                case Constantes.T_BOOL:
                    castValue = "" + Convert.ToInt32(Convert.ToBoolean(ValorVar.Valor));
                    break;

                case Constantes.T_NUM:
                    castValue = ValorVar.Valor;
                    break;
                default:
                    return FabricarResultado.creaFail();
            }
            // Creo variablew
            Variable var = new Variable(nombreVar, castValue, tipoVar, nivel);
            //si existe, es error entonces
            if (ctx.existeVariable(nombreVar))
            {
                return FabricarResultado.creaFail();
            }
            ctx.setVariable(var);
            Resultado exec = null;

            while (true)
            {
                Resultado condicionPara = new Expresion(instruccion.ChildNodes[2].ChildNodes[0]).resolver(ctx);
                if(condicionPara.Tipo != Constantes.T_BOOL)
                {
                    ListaErrores.getInstance().setErrorSemantico(instruccion.Token.Location.Line, instruccion.Token.Location.Line, "La condicion no retorna un valor booleando", Interprete.archivo);
                    return FabricarResultado.creaFail();
                }

                bool cond = condicionPara.Boleano;

                if(!cond)
                {
                    exec = FabricarResultado.creaOk();
                    break;
                }

                Cuerpo cuerpo = new Cuerpo(NodoCuerpo,true);
                exec = cuerpo.ejecutar(ctx, nivel + 1);
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

                Variable asigTemp = ctx.getVariable(nombreVar);
                if (asigTemp != null)
                {
                    if(asigTemp.Tipo == Constantes.T_NUM)
                    {
                        double valor = Convert.ToDouble(asigTemp.Valor);
                        if(inc_dec == Constantes.INCREMENTO)
                        {
                            valor++;
                        }
                        else
                        {
                            valor--;
                        }
                        asigTemp.Valor = valor.ToString();
                    }
                    ctx.ActualizarValor(nombreVar, asigTemp);

                }
                ctx.limpiarContexto(nivel + 1);
            }

            ctx.limpiarContexto(nivel + 1);
            ctx.limpiarContexto(nivel);

            return exec;
        }
    }
}
