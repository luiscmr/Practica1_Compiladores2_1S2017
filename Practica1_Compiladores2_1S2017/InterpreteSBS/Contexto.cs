using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica1_Compiladores2_1S2017.InterpreteSBS
{
    class Contexto
    {
        private Dictionary<String, Variable> variables;

        public Contexto() { variables = new Dictionary<string, Variable>(); }

        public void limpiarContexto(int Nivel) {
            Dictionary<String, Variable> Temporal = new Dictionary<string, Variable>();
            foreach (KeyValuePair<String, Variable> valor in variables) {
                if(valor.Value.Nivel != Nivel) { Temporal.Add(valor.Key, valor.Value); }
            }
            variables = new Dictionary<string, Variable>(Temporal);
        }

        public Variable getVariable(String nombre)
        {
            Variable tmp = null;
            variables.TryGetValue(nombre, out tmp);
            return tmp;
        }

        public bool existeVariable(String nombre)
        {
            Variable tmp;
            return variables.TryGetValue(nombre, out tmp);
        }

        public void setVariable(Variable var) { this.variables.Add(var.Nombre, var); }

        public void reporte() {
            foreach (KeyValuePair<String, Variable> valor in variables) {
                Console.WriteLine(valor.ToString());
            }
        }
    }
}
