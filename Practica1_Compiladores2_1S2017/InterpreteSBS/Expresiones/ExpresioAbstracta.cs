using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Irony.Parsing;
using Practica1_Compiladores2_1S2017.Analizador;

namespace Practica1_Compiladores2_1S2017.InterpreteSBS.Expresiones
{
    abstract class ExpresioAbstracta
    {
        protected ParseTreeNode izq;
        protected ParseTreeNode der;
        protected string operando;

        public ExpresioAbstracta(ParseTreeNode izq, ParseTreeNode der, string operando) {
            this.izq = izq; this.der = der; this.operando = operando;
        }

        public abstract Resultado resolver(Contexto ctx);

    }
}
