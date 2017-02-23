using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Irony.Parsing;
using Practica1_Compiladores2_1S2017.Analizador;

namespace Practica1_Compiladores2_1S2017.InterpreteSBS.Instrucciones
{
    class GraficarEXP : InstruccionAbstracta
    {
        public GraficarEXP(ParseTreeNode Instruccion, bool PermiteInterrupciones) : base(Instruccion, PermiteInterrupciones)
        {
        }

        public GraficarEXP(ParseTreeNode Instruccion) : base(Instruccion, false)
        {
        }

        override
        public Resultado ejecutar(Contexto ctx, int nivel)
        {
            //construir nombre

            String Nombre = "";
            Nombre = Interprete.Ruta + "EXP_" + Interprete.NoEXP + ".png";
            Gramatica.getEXP(instruccion.ChildNodes[0], Nombre);
            Interprete.NoEXP++;
            return FabricarResultado.creaOk();
        }
    }
}
