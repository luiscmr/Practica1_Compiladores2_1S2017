using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Irony.Ast;
using Irony.Interpreter;
using Irony.Parsing;
using Practica1_Compiladores2_1S2017.Analizador;
using Practica1_Compiladores2_1S2017.InterpreteSBS.Instrucciones;
using System.Windows.Forms;

namespace Practica1_Compiladores2_1S2017.InterpreteSBS
{
    class Interprete
    {
        private String Entrada = "";
        private static Contexto global = new Contexto();
        private static String salida = "";
        private List<ParseTreeNode> variables = new List<ParseTreeNode>();
        private List<ParseTreeNode> metodos = new List<ParseTreeNode>();
        private List<Encabezado> encabezado = new List<Encabezado>();
        private ParseTreeNode Principal = null;
        private ParseTreeNode Arbol = null;
        private static Double incerteza = 0.5;
        private static String ruta = "";

        public static String Salida
        {
            get
            {
                return salida;
            }
            set
            {
                salida = value;
            }
        }

        public Interprete(String Entrada) {
            this.Entrada = Entrada;
        }

        public void Analizar() {
            Gramatica gramatica = new Gramatica();
            LanguageData lenguaje = new LanguageData(gramatica);
            Parser parser = new Parser(lenguaje);
            ParseTree arbol = parser.Parse(Entrada);
            ParseTreeNode raiz = arbol.Root;

            if (raiz == null)
            {
                MessageBox.Show("You're Drunk Irony, Go Home!!");
                return;
            }
            Arbol = raiz;
            Gramatica.getImage(raiz);
        }

        public void Ejecutar() {
            reiniciarEjecucion();
            definirContextoGlobal();
            Contexto local = new Contexto();
            
            foreach (ParseTreeNode declaracion in variables) {
                Declaracion decla = new Declaracion(declaracion);
                decla.ejecutar(local, Constantes.GLOBAL);
            }
            Console.WriteLine("contexto global creado");
            if (Principal == null)
            {
                //error no se definio el main
                return;
            }
            Cuerpo _principal = new Cuerpo(Principal.ChildNodes[0], false);
            _principal.ejecutar(local, Constantes.GLOBAL + 1);
            Console.WriteLine("Ejecucion Exitosa");
            Console.WriteLine("Salida");
            Interprete.getContextoGlobal().reporte();
            local.reporte();
            Console.WriteLine(Interprete.Salida);
        }

        private void reiniciarEjecucion() {
            global = new Contexto();
            Salida = "";
        }

        public static Contexto getContextoGlobal() { return Interprete.global; }
        
        public static String Ruta{
            get{
                return ruta;
            }
            set {
                ruta = value;
            }
        }

        public static Double Incerteza
        {
            get
            {
                return incerteza;
            }
            set
            {
                incerteza = value;
            }
        }

        private void definirContextoGlobal() {
            if (Arbol != null) {
                if (Arbol.ChildNodes.Count == 1)
                {
                    var Nodo = Arbol.ChildNodes[0];
                    switch (Nodo.ToString())
                    {
                        case Constantes.ENCABEZADO:
                            foreach (var nodo_encabezado in Nodo.ChildNodes) {
                                switch (nodo_encabezado.ToString()) {
                                    case Constantes.INCERTEZA:
                                        Interprete.Incerteza = Convert.ToDouble(nodo_encabezado.ChildNodes[0].Token.Text);
                                        break;
                                    case Constantes.RUTA:
                                        Interprete.Ruta = nodo_encabezado.ChildNodes[0].Token.Text;
                                        break;
                                    case Constantes.INCLUYE:
                                        Encabezado e = new Encabezado(nodo_encabezado.ChildNodes[0].Token.Text);
                                        encabezado.Add(e);
                                        break;
                                }
                            }
                            break;
                        case Constantes.CUERPO_PROGRAMA:
                            foreach (var nodo in Nodo.ChildNodes)
                            {
                                foreach (var nodo_cuerpo in Nodo.ChildNodes) {
                                    switch (nodo_cuerpo.ToString())
                                    {
                                        case Constantes.DECLARACION:
                                            variables.Add(nodo_cuerpo);
                                            break;
                                        case Constantes.FUNCION:
                                            metodos.Add(nodo_cuerpo);
                                            break;
                                        case Constantes.PRINCIPAL:
                                            if (Principal != null)
                                            {
                                                Console.WriteLine("No pueden haber dos metodos principales");
                                            }else
                                            {
                                                Principal = nodo_cuerpo;
                                            }
                                            break;
                                    }
                                }
                            }
                            break;
                        
                    }
                }
                else if (Arbol.ChildNodes.Count == 2)
                {
                    foreach(var Nodo in Arbol.ChildNodes)
                    {
                        switch (Nodo.ToString())
                        {
                            case Constantes.ENCABEZADO:
                                foreach (var nodo_encabezado in Nodo.ChildNodes)
                                {
                                    switch (nodo_encabezado.ToString())
                                    {
                                        case Constantes.INCERTEZA:
                                            Interprete.Incerteza = Convert.ToDouble(nodo_encabezado.ChildNodes[0].Token.Text);
                                            break;
                                        case Constantes.RUTA:
                                            Interprete.Ruta = nodo_encabezado.ChildNodes[0].Token.Text;
                                            break;
                                        case Constantes.INCLUYE:
                                            Encabezado e = new Encabezado(nodo_encabezado.ChildNodes[0].Token.Text);
                                            encabezado.Add(e);
                                            break;
                                    }
                                }
                                break;
                            case Constantes.CUERPO_PROGRAMA:
                                foreach (var nodo_cuerpo in Nodo.ChildNodes)
                                {
                                    switch (nodo_cuerpo.ToString())
                                    {
                                        case Constantes.DECLARACION:
                                            variables.Add(nodo_cuerpo);
                                            break;
                                        case Constantes.FUNCION:
                                            metodos.Add(nodo_cuerpo);
                                            break;
                                        case Constantes.PRINCIPAL:
                                            if (Principal != null)
                                            {
                                                Console.WriteLine("No pueden haber dos metodos principales");
                                            }
                                            else
                                            {
                                                Principal = nodo_cuerpo;
                                            }
                                            break;
                                    }
                                }
                                break;
                            default:
                                break;
                        }
                    }
                }
                else {
                    Console.WriteLine("Arbol Vacio!");
                }
            }
        }

        public static void ConcatenarSalida(String cad)
        {
            Interprete.salida += cad;
        }

    }
}
