using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Irony.Parsing;

namespace Practica1_Compiladores2_1S2017.InterpreteSBS.Instrucciones
{
    abstract class InstruccionAbstracta
    {
        protected ParseTreeNode instruccion;
        protected bool permiteInterrupciones;

        InstruccionAbstracta(ParseTreeNode Instruccion, bool PermiteInterrupciones)
        {
            this.instruccion = Instruccion;
            this.permiteInterrupciones = PermiteInterrupciones;
        }

        public abstract Resultado ejecutar(Contexto ctx, int nivel);

        public static Variable obtenerVariable(Contexto ctx, String nombre)
        {
            Variable var = ctx.getVariable(nombre);
            if (var != null) { return var; }
            var = Interprete.getContextoGlobal().getVariable(nombre);
            return var;
        }

        protected void setVariable(Contexto ctx, Variable var) {
            if (ctx.existeVariable(var.Nombre)) {
                ctx.setVariable(var);
            }else if (Interprete.getContextoGlobal().existeVariable(var.Nombre))
            {
                Interprete.getContextoGlobal().setVariable(var);
            }
        }

        protected bool existeVariable(Contexto ctx, String nombre)
        {
            return ctx.existeVariable(nombre) || Interprete.getContextoGlobal().existeVariable(nombre);
        }

        protected bool asignacionValida(int tipoDestino, int tipoFuente)
        {
            //Una comprobación más extensa sería bien resuelta por una 
            //matriz de tipos, ver resolverSuma
            return tipoDestino == tipoFuente;
        }

    }
}
