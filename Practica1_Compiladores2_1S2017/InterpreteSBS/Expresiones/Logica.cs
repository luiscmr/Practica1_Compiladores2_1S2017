using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Irony.Parsing;
using Practica1_Compiladores2_1S2017.Analizador;

namespace Practica1_Compiladores2_1S2017.InterpreteSBS.Expresiones
{
    class Logica : ExpresioAbstracta
    {
        public Logica(ParseTreeNode izq, ParseTreeNode der, string operando) : base(izq, der, operando)
        {
        }
        public Logica(ParseTreeNode izq, string operando) : base(izq, null, operando)
        {
        }

        override
        public Resultado resolver(Contexto ctx)
        {
            Resultado res = new Resultado();
            if (der == null) {
                return resolverUnario(ctx);
            }
            return resolverBinaria(ctx);
        }

        private Resultado resolverBinaria(Contexto ctx)
        {
            Resultado resIzq = new Expresion(izq).resolver(ctx);
            Resultado resDer = new Expresion(der).resolver(ctx);
            String tipoIzq = resIzq.Tipo;
            String tipoDer = resDer.Tipo;

            if(tipoIzq == Constantes.T_BOOL && tipoIzq == tipoDer)
            {
                Console.WriteLine("Error los dos deben ser booleanos");
                return new Resultado();
            }

            bool bIzq = resIzq.Boleano;
            bool bDer = resDer.Boleano;
            switch (operando)
            {
                case "&&":
                    bIzq = bIzq && bDer;
                    break;
                case "||":
                    bIzq = bIzq || bDer;
                    break;
                case "!&":
                    bIzq = bIzq ^ bDer;
                    break;
                default:
                    Console.WriteLine("Error");
                    break;
            }
            return FabricarResultado.creaBooleano(bIzq);

        }

        private Resultado resolverUnario(Contexto ctx) {
            Resultado resIzq = new Expresion(izq).resolver(ctx);
            if(resIzq.Tipo != Constantes.T_BOOL)
            {
                return new Resultado();
            }
            if (operando == Constantes.OP_RES)
            {
                return resolverNot(resIzq);
            }
            return new Resultado();
        }

        private Resultado resolverNot(Resultado izq)
        {
            bool b = izq.Boleano;
            b = !b;
            return FabricarResultado.creaBooleano(b);
        }
    }
}
