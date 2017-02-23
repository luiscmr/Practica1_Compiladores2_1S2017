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
    class Asignacion : InstruccionAbstracta
    {
        private object izq;

        public Asignacion(ParseTreeNode Instruccion, bool PermiteInterrupciones) : base(Instruccion, PermiteInterrupciones)
        {
        }
        public Asignacion(ParseTreeNode Instruccion) : base(Instruccion, false) { }

        override
        public Resultado ejecutar(Contexto ctx, int nivel)
        {
            String nombreVar = instruccion.ChildNodes[0].Token.Text;
            if (!existeVariable(ctx, nombreVar))
            {
                return FabricarResultado.creaFail();
            }
            Resultado res = new Expresion(instruccion.ChildNodes[1].ChildNodes[0]).resolver(ctx);
            String tipo = res.Tipo;
            if(tipo == Constantes.T_ERROR)
            {
                ListaErrores.getInstance().setErrorSemantico(instruccion.Token.Location.Line, instruccion.Token.Location.Line, "No se puedo resolver la expresion", Interprete.archivo);
                return FabricarResultado.creaFail();
                //error en la expresion
            }
            Variable destino = obtenerVariable(ctx, nombreVar);
            String tipoVar = destino.Tipo;
            String casteo = Constantes.ValidarTipos(tipoVar, tipo, Constantes.MT_ASIG);
            String valor = null;
            if (casteo == Constantes.T_ERROR)
            {
                ListaErrores.getInstance().setErrorSemantico(instruccion.Token.Location.Line, instruccion.Token.Location.Line, "No se puede asignar ese tipo de dato", Interprete.archivo);
                //no se puede asignar ese tipo
            }
            else
            {
                String tmp = "";
                switch (tipoVar)
                {
                    case Constantes.T_STR:
                        switch (tipo)
                        {
                            case Constantes.T_BOOL:
                                tmp = "" + Convert.ToInt32(Convert.ToBoolean(res.Valor));
                                break;
                            default:
                                tmp = res.Valor;
                                break;
                        }
                        break;
                    case Constantes.T_NUM:
                        switch (tipo)
                        {

                            case Constantes.T_BOOL:
                                tmp = "" + Convert.ToInt32(Convert.ToBoolean(res.Valor));
                                break;
                            case Constantes.T_NUM:
                                tmp = res.Valor;
                                break;
                            default:

                                ListaErrores.getInstance().setErrorSemantico(instruccion.Token.Location.Line, instruccion.Token.Location.Line, "error de casteo", Interprete.archivo);
                                //error por si llegara a pasar aunque no lo creo
                                break;
                        }
                        break;
                    case Constantes.T_BOOL:
                        switch (tipo)
                        {
                            case Constantes.T_BOOL:
                                tmp = res.Valor;
                                break;
                            default:
                                ListaErrores.getInstance().setErrorSemantico(instruccion.Token.Location.Line, instruccion.Token.Location.Line, "error de casteo", Interprete.archivo);
                                //error por si llegara a pasar aunque no lo creo
                                break;
                        }
                        break;
                }
                valor = tmp;
            }
            destino.Valor = valor;

            ctx.ActualizarValor(nombreVar, destino);
            return FabricarResultado.creaOk();
        }
    }
}
