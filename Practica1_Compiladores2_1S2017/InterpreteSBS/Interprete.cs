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
using Practica1_Compiladores2_1S2017.InterpreteSBS.Expresiones;
using Practica1_Compiladores2_1S2017.Errores;
using System.Windows.Forms;
using System.IO;

namespace Practica1_Compiladores2_1S2017.InterpreteSBS
{
    class Interprete
    {
        
        private String Entrada = "";
        private static Contexto global = new Contexto();
        private static String salida = "";
        private static int noexp = 0;
        private static List<ParseTreeNode> variables = new List<ParseTreeNode>();
        private static Dictionary<String,List<ParseTreeNode>> metodos = new Dictionary<string, List<ParseTreeNode>>();
        private static List<Encabezado> encabezado = new List<Encabezado>();
        private ParseTreeNode Principal = null;
        private ParseTreeNode Arbol = null;
        private static Double incerteza = 0.5;
        private static String ruta = "";
        public static String archivo = "";
        public static int NoEXP
        {
            get
            {
                return noexp;
            }
            set
            {
                noexp = value;
            }
        }

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

        public Interprete(String Entrada,String Archivo) {
            this.Entrada = Entrada;
            Interprete.archivo = Archivo;
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
//            Gramatica.getImage(raiz);
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
                ListaErrores.getInstance().nuevoErrorGeneral("El metodo principal no ha sido definido",archivo);
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
            NoEXP = 0;
            Ruta = "";
            Incerteza = 0.5;
            variables = new List<ParseTreeNode>();
            metodos = new Dictionary<string, List<ParseTreeNode>>();
            ListaErrores.resetInstance();
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
                                        String _ruta = nodo_encabezado.ChildNodes[0].Token.Text;
                                        if (Directory.Exists(_ruta))
                                        {
                                            Interprete.Ruta = _ruta;
                                        }
                                        break;
                                    case Constantes.INCLUYE:
                                        Encabezado e = new Encabezado(nodo_encabezado.ChildNodes[0].Token.Text,archivo);
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
                                            Interprete.addMetodo(nodo_cuerpo);
                                            break;
                                        case Constantes.PRINCIPAL:
                                            if (Principal != null)
                                            {
                                                ListaErrores.getInstance().nuevoErrorGeneral("No pueden haber dos funciones principales",archivo);
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
                                            String _ruta = nodo_encabezado.ChildNodes[0].Token.Text;
                                            _ruta = _ruta.Substring(1, _ruta.Length - 2);
                                            if (Directory.Exists(_ruta)==true)
                                            {
                                                Interprete.Ruta = _ruta;
                                            }
                                            break;
                                        case Constantes.INCLUYE:
                                            Encabezado e = new Encabezado(nodo_encabezado.ChildNodes[0].Token.Text,archivo);
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
                                            Interprete.addMetodo(nodo_cuerpo);
                                            break;
                                        case Constantes.PRINCIPAL:
                                            if (Principal != null)
                                            {
                                                ListaErrores.getInstance().nuevoErrorGeneral("No pueden haber dos funciones principales", archivo);
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

        public static List<ParseTreeNode> getListaMetodos(String metodo) {
            List<ParseTreeNode> met=null;
            Interprete.metodos.TryGetValue(metodo,out met);
            return met;
        }

        public static void addMetodo(ParseTreeNode fun)
        {
            String nom = fun.ChildNodes[1].Token.Text;
            List<ParseTreeNode> funciones;
            bool sePuedeGuardar = true;
            int cantNodos = fun.ChildNodes.Count;
            if (metodos.TryGetValue(nom,out funciones))
            {
                foreach(ParseTreeNode nodo in funciones)
                {
                    if(cantNodos == 3 && nodo.ChildNodes.Count == cantNodos)
                    {
                        sePuedeGuardar = false;
                        break;
                    }
                    else if(cantNodos == 4 && nodo.ChildNodes.Count == cantNodos)
                    {
                        int par1 = nodo.ChildNodes[2].ChildNodes.Count;
                        int par2 = fun.ChildNodes[2].ChildNodes.Count;
                        if(par1 == par2)
                        {
                            int i, j = 0;
                            for(i = 0; i < par1; i++)
                            {
                                String tipo1 = fun.ChildNodes[2].ChildNodes[i].ChildNodes[0].ToString();
                                String tipo2 = nodo.ChildNodes[2].ChildNodes[i].ChildNodes[0].ToString();
                                if (tipo1 == tipo2)
                                {
                                    j++;
                                }
                            }
                            if(i == j)
                            {
                                sePuedeGuardar = false;
                                ListaErrores.getInstance().setErrorSemantico(nodo.Token.Location.Line, nodo.Token.Location.Line, "No se puede crear la funcion con los mismos parametros y mismo nombre",Interprete.archivo);
                                break;
                            }
                        }
                    }
                }

                if (sePuedeGuardar)
                {
                    funciones.Add(fun);
                    metodos[nom] = funciones;
                }
                else
                {
                    Console.WriteLine("una funcion ya ha sido declarada con el mismo nombre y mismos parametros");
                }
            }
            else
            {
                funciones = new List<ParseTreeNode>();
                funciones.Add(fun);
                metodos.Add(nom, funciones);
            }

        }

        public static void ConcatenarSalida(String cad)
        {
            Interprete.salida += cad;
        }

        public static ParseTreeNode getFuncion(ParseTreeNode fun, Contexto ctx)
        {
            String nombreFuncion = fun.ChildNodes[0].Token.Text;
            List<ParseTreeNode> Lista = Interprete.getListaMetodos(nombreFuncion);

            if (Lista == null)
            {
                //No existe la funcion
                return null;
            }
            ParseTreeNode funcion = null;
            int parametrosFuncion = fun.ChildNodes[1].ChildNodes.Count;
            foreach (ParseTreeNode nodo in Lista)
            {
                int nodosFuncionLista = nodo.ChildNodes.Count;
                if(nodosFuncionLista == 3 && parametrosFuncion == 0)
                {
                    return nodo;
                }
                else if(nodosFuncionLista == 4)
                {
                    int par1 = nodo.ChildNodes[2].ChildNodes.Count;
                    if (par1 == parametrosFuncion)
                    {
                        int i, j = 0;
                        for (i = 0; i < par1; i++)
                        {
                            Resultado res = new Expresion(fun.ChildNodes[1].ChildNodes[i].ChildNodes[0]).resolver(ctx);
                            String tipo1 = res.Tipo;
                            String tipo2 = nodo.ChildNodes[2].ChildNodes[i].ChildNodes[0].ToString();
                            if (tipo1 == tipo2)
                            {
                                j++;
                            }
                        }
                        if (i == j)
                        {
                            return nodo;
                        }
                    }
                }
            }
            return funcion;
        }

    }
}
