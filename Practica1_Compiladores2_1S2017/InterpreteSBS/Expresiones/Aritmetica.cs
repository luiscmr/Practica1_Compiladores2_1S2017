using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Irony.Parsing;
using Practica1_Compiladores2_1S2017.Analizador;

namespace Practica1_Compiladores2_1S2017.InterpreteSBS.Expresiones
{
    class Aritmetica : ExpresioAbstracta
    {
        public Aritmetica(ParseTreeNode izq, ParseTreeNode der, string operando) : base(izq, der, operando)
        {
        }
        public Aritmetica(ParseTreeNode izq, string operando) : base(izq, null, operando)
        {
        }

        override
        public Resultado resolver(Contexto ctx)
        {
            if (der == null) {
                return resolverUnaria(ctx);
            }

            return resolverBinaria(ctx);
        }

        private Resultado resolverUnaria(Contexto ctx) {
            Resultado resIzq = new Expresion(izq).resolver(ctx);
            if(resIzq.Tipo != Constantes.T_NUM)
            {
                return new Resultado();
            }
            if(operando == Constantes.OP_RES)
            {
                return resolverMenos(resIzq);
            }
            return new Resultado();
        }

        private Resultado resolverMenos(Resultado resIzq)
        {
            Double dIzq = resIzq.Doble;
            dIzq *= -1;
            return FabricarResultado.creaNumero(Convert.ToString(dIzq));
        }

        private Resultado resolverBinaria(Contexto ctx) {
            Resultado resIzq = new Expresion(izq).resolver(ctx);
            if (resIzq.esError())
            {
                return new Resultado();
            }
            Resultado resDer = new Expresion(der).resolver(ctx);
            if (resDer.esError())
            {
                return new Resultado();
            }
            if (operando == "+")
            {
                return resolverSuma(resIzq,resDer);
            }
            else if (operando == "*")
            {
                return resolverMultiplicacion(resIzq, resDer);
            }
            else if (Constantes.esAlgunoDeEstos(operando,"-","/","%","^"))
            {
                return resolverNumerica(resIzq, resDer);
            }
            return new Resultado();
        }

        private Resultado resolverSuma(Resultado resIzq, Resultado resDer) {
            String validarTipo = ValidarTipos(resIzq.Tipo, resDer.Tipo, Constantes.MT_SUMA);
            if(validarTipo == Constantes.T_NUM)
            {
                return resolverNumerica(resIzq, resDer);
            }
            if(validarTipo == Constantes.T_STR)
            {
                return FabricarResultado.creaCadena(String.Concat(resIzq.Valor,resDer.Valor));
            }
            if(validarTipo == Constantes.T_BOOL)
            {
                return FabricarResultado.creaBooleano(resIzq.Boleano||resDer.Boleano);
            }
            return new Resultado();
        }

        private Resultado resolverNumerica(Resultado resIzq, Resultado resDer)
        {
            double dIzq = resIzq.Doble;
            double dDer = resDer.Doble;
            switch (operando)
            {
                case "+":
                    if (ValidarTipos(resIzq.Tipo, resDer.Tipo, Constantes.MT_SUMA) == Constantes.T_NUM)
                    {
                        dIzq += dDer;
                    }
                    return new Resultado();
                case "-":
                    if (ValidarTipos(resIzq.Tipo, resDer.Tipo, Constantes.MT_RESTA) == Constantes.T_NUM)
                    {
                        dIzq -= dDer;
                    }
                    return new Resultado();
                case "*":
                    if (ValidarTipos(resIzq.Tipo, resDer.Tipo, Constantes.MT_MULTPLICACION) == Constantes.T_NUM)
                    {
                        dIzq *= dDer;
                    }
                    return new Resultado();
                case "/":
                    if(dDer == 0)
                    {
                        Console.WriteLine("No se Puede dividir entre 0");
                        return new Resultado();
                    }
                    if (ValidarTipos(resIzq.Tipo, resDer.Tipo, Constantes.MT_DIVISION) == Constantes.T_NUM)
                    {
                        dIzq /= dDer;
                    }
                    return new Resultado();
                case "%":
                    if (dDer == 0)
                    {
                        Console.WriteLine("No se Puede dividir entre 0");
                        return new Resultado();
                    }

                    if (ValidarTipos(resIzq.Tipo, resDer.Tipo, Constantes.MT_MODULO) == Constantes.T_NUM)
                    {
                        int mod = (int)dIzq % (int)dDer;
                        return FabricarResultado.creaNumero(Convert.ToString(mod));
                    }
                    return new Resultado();
                case "^":
                    if (ValidarTipos(resIzq.Tipo, resDer.Tipo, Constantes.MT_POTENCIA) == Constantes.T_NUM)
                    {
                        dIzq = Math.Pow(dIzq, dDer);
                    }
                    return new Resultado();
            }
            return FabricarResultado.creaNumero(Convert.ToString(dIzq));
        }

        private Resultado resolverMultiplicacion(Resultado resIzq, Resultado resDer)
        {
            String validarTipo = ValidarTipos(resIzq.Tipo, resDer.Tipo, Constantes.MT_MULTPLICACION);
            if(validarTipo == Constantes.T_NUM)
            {
                return resolverNumerica(resIzq, resDer);
            }
            if(validarTipo == Constantes.T_BOOL)
            {
                return FabricarResultado.creaBooleano(resIzq.Boleano && resDer.Boleano);
            }
            return new Resultado();
        }
    }
}
