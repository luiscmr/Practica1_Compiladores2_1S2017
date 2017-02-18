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

        private int getPos(String op)
        {
            int pos = 0;
            switch (op)
            {
                case Constantes.T_NUM:
                    pos = 0;
                    break;
                case Constantes.T_STR:
                    pos = 1;
                    break;
                case Constantes.T_BOOL:
                    pos = 2;
                    break;
                case Constantes.T_VOID:
                    pos = 3;
                    break;
                case Constantes.T_ERROR:
                    pos = 4;
                    break;
                default:
                    pos = 5;
                    break;
            }
            return pos;
        }

        public string ValidarTipos(String op1, String op2, String[,] Matriz)
        {
            //string res = ValidarTipos(Constante.T_STR,Constante.T_NUM,MT_SUMA);
            int fila = getPos(op1);
            int columna = getPos(op2);
            if (fila != 5 && columna != 5)
            {
                return Matriz[fila, columna];
            }
            else
            {
                return Constantes.T_ERROR;
            }
        }
    }
}
