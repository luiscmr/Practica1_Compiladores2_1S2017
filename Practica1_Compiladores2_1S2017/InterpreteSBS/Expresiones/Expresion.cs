using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Irony.Parsing;
using Practica1_Compiladores2_1S2017.Analizador;

namespace Practica1_Compiladores2_1S2017.InterpreteSBS.Expresiones
{
    class Expresion : ExpresioAbstracta
    {
        public Expresion(ParseTreeNode izq, ParseTreeNode der, string operando) : base(izq, der, operando)
        {}
       
        public Expresion(ParseTreeNode izq) : base(izq,null,"")
        {
        }

        override
        public Resultado resolver(Contexto ctx)
        {
            String tipo = izq.ToString();
            Resultado res = new Resultado();

            switch (tipo)
            {
                case Constantes.LOGICA:
                    if (izq.ChildNodes.Count == 3)
                    {
                        res = new Logica(izq.ChildNodes[0], izq.ChildNodes[2], izq.ChildNodes[1].Token.Text).resolver(ctx);
                    }
                    else if (izq.ChildNodes.Count == 2)
                    {
                        res = new Logica(izq.ChildNodes[1], izq.ChildNodes[0].Token.Text).resolver(ctx);
                    }
                    else
                    {
                        res = new Expresion(izq.ChildNodes[0]).resolver(ctx);
                    }
                    break;
                case Constantes.RELACIONAL:
                    if (izq.ChildNodes.Count == 3)
                    {
                        res = new Relacional(izq.ChildNodes[0], izq.ChildNodes[2], izq.ChildNodes[1].Token.Text).resolver(ctx);
                    }else
                    {
                        res = new Expresion(izq.ChildNodes[0]).resolver(ctx);
                    }
                    break;
                case Constantes.ARITMETICA:
                    if (izq.ChildNodes.Count == 3)
                    {
                        res = new Aritmetica(izq.ChildNodes[0], izq.ChildNodes[2],izq.ChildNodes[1].Token.Text).resolver(ctx);
                    }
                    else if(izq.ChildNodes.Count == 2) {
                        res = new Aritmetica(izq.ChildNodes[1], izq.ChildNodes[0].Token.Text).resolver(ctx);
                    }
                    else
                    {
                        res = new Expresion(izq.ChildNodes[0]).resolver(ctx);
                    }
                    break;
                case Constantes.VALOR:
                    res = new Expresion(izq.ChildNodes[0]).resolver(ctx);
                    break;
                case Constantes.PRIMITIVO:
                    res = new Expresion(izq.ChildNodes[0]).resolver(ctx);
                    break;
                case Constantes.LLAMADA:
                    break;
                case Constantes.ID:
                    String nombreVar = izq.ChildNodes[0].Token.Text;
                    Variable var = Instrucciones.InstruccionAbstracta.obtenerVariable(ctx, nombreVar);
                    if(var == null)
                    {
                        return new Resultado();
                    }
                    res = new Resultado(var.Valor, var.Tipo);
                    break;
                case Constantes.TRUE:
                    res = new Resultado(izq.ChildNodes[0].Token.Text,Constantes.T_BOOL);
                    break;
                case Constantes.FALSE:
                    res = new Resultado(izq.ChildNodes[0].Token.Text, Constantes.T_BOOL);
                    break;
                default:
                    String _tipo = izq.Term.ToString();
                    String cad = izq.Token.Text;
                    if (_tipo.CompareTo("numero") == 0)
                    {
                        res = new Resultado(cad, Constantes.T_NUM);
                    }
                    else {
                        res = new Resultado(cad.Substring(1,cad.Length-2), Constantes.T_STR);
                    }
                    break;
            }
            return res;
        }
    }
}
