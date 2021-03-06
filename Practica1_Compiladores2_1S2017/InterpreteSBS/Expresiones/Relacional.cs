﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Irony.Parsing;
using Practica1_Compiladores2_1S2017.Analizador;
using Practica1_Compiladores2_1S2017.Errores;

namespace Practica1_Compiladores2_1S2017.InterpreteSBS.Expresiones
{
    class Relacional : ExpresioAbstracta
    {
        public Relacional(ParseTreeNode izq, ParseTreeNode der, string operando) : base(izq, der, operando) { }
        
        override
        public Resultado resolver(Contexto ctx)
        {
            Resultado resIzq = new Expresion(izq).resolver(ctx);
            Resultado resDer = new Expresion(der).resolver(ctx);
            String tipoIzq = resIzq.Tipo;
            String tipoDer = resDer.Tipo;

            if ((tipoIzq != Constantes.T_STR && tipoIzq != Constantes.T_ERROR && tipoIzq != Constantes.T_VOID ) &&
                (tipoDer != Constantes.T_STR && tipoDer != Constantes.T_ERROR && tipoDer != Constantes.T_VOID))
            {
                return resolverNumeros(resIzq,resDer,ctx);
            }
            else if (tipoIzq == Constantes.T_STR && tipoIzq == tipoDer)
            {
                return resolverCadenas(resIzq, resDer, ctx);
            }

            ListaErrores.getInstance().setErrorSemantico(izq.Token.Location.Line, izq.Token.Location.Line, "Ambas tienen que ser numeros o cadenas", Interprete.archivo);
            return new Resultado();
        }

        private Resultado resolverNumeros(Resultado resIzq, Resultado resDer, Contexto ctx) {
            double dIzq, dDer;
            if (resIzq.Tipo == Constantes.T_BOOL)
            {
                dIzq = Convert.ToInt32(Convert.ToBoolean(resIzq.Valor));
            }else
            {
                dIzq = resIzq.Doble;

            }

            if(resDer.Tipo == Constantes.T_BOOL)
            {
                dDer = Convert.ToInt32(Convert.ToBoolean(resDer.Valor));
            }
            else
            {
                dDer = resDer.Doble;
            }
            bool b = false;

            switch (operando)
            {
                case "==":
                    b = dIzq == dDer;
                    break;
                case "!=":
                    b = dIzq != dDer;
                    break;
                case "<":
                    b = dIzq < dDer;
                    break;
                case ">":
                    b = dIzq > dDer;
                    break;
                case "<=":
                    b = dIzq <= dDer;
                    break;
                case ">=":
                    b = dIzq >= dDer;
                    break;
                case "~":
                    b = Math.Abs(dIzq - dDer)<=Interprete.Incerteza;
                    break;
                default:
                    ListaErrores.getInstance().setErrorSemantico(izq.Token.Location.Line, izq.Token.Location.Line, "opearacion fuera de rango", Interprete.archivo);
                    break;
            }

            return FabricarResultado.creaBooleano(b);
        }

        private Resultado resolverCadenas(Resultado resIzq, Resultado resDer, Contexto ctx)
        {
            int comparacion = resIzq.Valor.CompareTo(resDer.Valor);
            bool b = false;
            switch (operando)
            {
                case "==":
                    b = comparacion == 0;
                    break;
                case "!=":
                    b = comparacion != 0;
                    break;
                case "<":
                    b = comparacion < 0;
                    break;
                case ">":
                    b = comparacion > 0;
                    break;
                case "<=":
                    b = comparacion <= 0;
                    break;
                case ">=":
                    b = comparacion >= 0;
                    break;
                case "~":
                    String v1 = resIzq.Valor.Trim();
                    String v2 = resDer.Valor.Trim();
                    b = String.Compare(v1,v2,true) == 0;
                    break;
                default:
                    ListaErrores.getInstance().setErrorSemantico(izq.Token.Location.Line, izq.Token.Location.Line, "opreardor invalido", Interprete.archivo);
                    break;
            }

            return FabricarResultado.creaBooleano(b);
        }
    }
}
