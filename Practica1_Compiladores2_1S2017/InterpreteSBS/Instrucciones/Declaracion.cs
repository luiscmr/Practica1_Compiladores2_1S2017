using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Irony.Parsing;
using Practica1_Compiladores2_1S2017.InterpreteSBS.Expresiones;
using Practica1_Compiladores2_1S2017.Analizador;

namespace Practica1_Compiladores2_1S2017.InterpreteSBS.Instrucciones
{
    class Declaracion : InstruccionAbstracta
    {
        public Declaracion(ParseTreeNode instruccion): base(instruccion,false) { }
        
        override
        public Resultado ejecutar(Contexto ctx, int nivel)
        {
            String valor = null;
            String varTipo = instruccion.ChildNodes[0].ToString();
            if(instruccion.ChildNodes.Count == 3)
            {
                Resultado res = new Expresion(instruccion.ChildNodes[2].ChildNodes[0]).resolver(ctx);
                String tipo = res.Tipo;
                String casteo = Constantes.ValidarTipos(varTipo, tipo, Constantes.MT_ASIG);
                if (tipo == Constantes.T_ERROR)
                {

                }else if (casteo == Constantes.T_ERROR)
                {

                }
                else
                {
                    String tmp = "";
                    switch (varTipo)
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
                                    //error por si llegara a pasar aunque no lo creo
                                    break;
                            }
                            break;
                    }
                    valor = tmp;
                }
            }

            foreach (var id in instruccion.ChildNodes[1].ChildNodes)
            {
                String nombre = id.Token.Text;
                Variable var = new Variable(nombre,valor, varTipo, nivel);
                if (nivel == Constantes.GLOBAL)
                {
                    if (existeVariableGlobal(nombre))
                    {
                        //error ya existe la variable
                    }
                    crearVariableGlobal(var);
                }
                else
                {
                    if (existeVariableLocal(ctx, nombre))
                    {
                        //ya existe la variable local
                    }
                    crearVariableLocal(ctx, var);
                }
            }
            
            return FabricarResultado.creaOk();
        }

        private void crearVariableLocal(Contexto ctx, Variable nueva)
        {
            ctx.setVariable(nueva);
        }

        private void crearVariableGlobal(Variable nueva)
        {
            Interprete.getContextoGlobal().setVariable(nueva);
        }

        private bool existeVariableLocal(Contexto ctx, String nombre)
        {
            return ctx.existeVariable(nombre);
        }

        private bool existeVariableGlobal(String nombre)
        {
            return Interprete.getContextoGlobal().existeVariable(nombre);
        }


    }
}
