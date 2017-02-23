using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Irony.Parsing;
using Practica1_Compiladores2_1S2017.Analizador;
using Practica1_Compiladores2_1S2017.Errores;

namespace Practica1_Compiladores2_1S2017.InterpreteSBS.Instrucciones
{
    class Cuerpo : InstruccionAbstracta
    {
        public Cuerpo(ParseTreeNode Instruccion, bool PermiteInterrupciones) : base(Instruccion, PermiteInterrupciones)
        {
        }

        override
        public Resultado ejecutar(Contexto ctx, int nivel)
        {
            if (instruccion.ToString() != Constantes.CUERPO_FUNCION)
            {
                return new Resultado();
            }
            foreach(var instruccion in instruccion.ChildNodes)
            {
                Resultado res = null;
                switch (instruccion.ToString())
                {
                    case Constantes.DECLARACION:
                        res = new Declaracion(instruccion).ejecutar(ctx, nivel);
                        break;
                    case Constantes.ASIGNACION:
                        res = new Asignacion(instruccion).ejecutar(ctx, nivel);
                        break;
                    case Constantes.LLAMADA:
                        res = new Llamada(instruccion).ejecutar(ctx, nivel);
                        break;
                    case Constantes.MOSTRAR:
                        res = new Mostrar(instruccion).ejecutar(ctx, nivel);
                        break;
                    case Constantes.DIBUJARAST:
                        res = new GraficarAST(instruccion).ejecutar(ctx, nivel);
                        break;
                    case Constantes.DIBUJAREXP:
                        res = new GraficarEXP(instruccion).ejecutar(ctx, nivel);
                        break;
                    case Constantes.SI_SINO:
                        res = new Si(instruccion,permiteInterrupciones).ejecutar(ctx, nivel + 1);
                        break;
                    case Constantes.SELECCIONA:
                        res = new Selecciona(instruccion,true).ejecutar(ctx, nivel + 1);
                        break;
                    case Constantes.PARA:
                        res = new Para(instruccion, true).ejecutar(ctx, nivel + 1);
                        break;
                    case Constantes.HASTA:
                        res = new Hasta(instruccion, true).ejecutar(ctx, nivel + 1);
                        break;
                    case Constantes.MIENTRAS:
                        res = new Mientras(instruccion, true).ejecutar(ctx, nivel + 1);
                        break;
                    case Constantes.RETORNO:
                        if(instruccion.ChildNodes.Count == 1)
                        {
                            res = new Retorno(instruccion.ChildNodes[0]).ejecutar(ctx, nivel);
                        }
                        break;
                    case Constantes.DETENER:
                        res = FabricarResultado.creaDetener();
                        break;
                    case Constantes.CONTINUAR:
                        res = FabricarResultado.creaContinuar();
                        break;
                }
                if(res == null)
                {
                    //error
                    continue;
                }
                if (res.esRetorno())
                {
                    return res;
                }
                if(res.esDetener() || res.esContinuar())
                {
                    if (permiteInterrupciones)
                    {
                        return res;
                    }
                    else
                    {
                        ListaErrores.getInstance().setErrorSemantico(instruccion.Token.Location.Line, instruccion.Token.Location.Line, "La instruccion no pertence al contexto", Interprete.archivo);
                    }
                }
            }

            return FabricarResultado.creaOk();
        }
    }
}
