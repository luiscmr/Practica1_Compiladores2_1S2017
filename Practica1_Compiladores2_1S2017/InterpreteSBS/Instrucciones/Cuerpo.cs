using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Irony.Parsing;
using Practica1_Compiladores2_1S2017.Analizador;

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
                        break;
                    case Constantes.MOSTRAR:
                        res = new Mostrar(instruccion).ejecutar(ctx, nivel);
                        break;
                    case Constantes.DIBUJARAST:
                        break;
                    case Constantes.DIBUJAREXP:
                        break;
                    case Constantes.SI:
                        break;
                    case Constantes.SELECCIONA:
                        break;
                    case Constantes.PARA:
                        break;
                    case Constantes.HASTA:
                        break;
                    case Constantes.MIENTRAS:
                        break;
                    case Constantes.RETORNO:
                        break;
                    case Constantes.DETENER:
                        break;
                    case Constantes.CONTINUAR:
                        break;
                }
                if(res == null)
                {
                    //error
                }
                /*if (res.esRetorno())
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
                        //error
                    }
                }*/
            }

            return FabricarResultado.creaOk();
        }
    }
}
