using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Practica1_Compiladores2_1S2017.Analizador;

namespace Practica1_Compiladores2_1S2017.Graficar
{
    class arbolDOT
    {


        private static int contador;
        private static String grafo;

        public static String getDOT(ParseTreeNode raiz)
        {

            grafo = "digraph G{";
            grafo += "nodo0[label=\"" + escapar(raiz.ToString()) + "\"];\n";
            contador = 1;
            recorrerAST("nodo0", raiz);
            grafo += "}";
            return grafo;
        }

        public static String getDOT2(ParseTreeNode raiz)
        {

            grafo = "digraph G{";
            grafo += "nodo0[label=\"" + escapar(raiz.ToString()) + "\"];\n";
            contador = 1;
            recorrerAST2("nodo0", raiz);
            grafo += "}";
            return grafo;
        }


        private static void recorrerAST(String padre, ParseTreeNode hijos)
        {

            foreach (ParseTreeNode hijo in hijos.ChildNodes)
            {

                String nombrehijo = "nodo" + contador.ToString();
                grafo += nombrehijo + "[label=\"" + escapar(hijo.ToString()) + "\"];\n";
                grafo += padre + "->" + nombrehijo + ";\n";
                contador++;
                recorrerAST(nombrehijo, hijo);
            }
        }

        private static void recorrerAST2(String padre, ParseTreeNode hijos)
        {

            foreach (ParseTreeNode hijo in hijos.ChildNodes)
            {

                String nombrehijo = "nodo" + contador.ToString();
                grafo += nombrehijo + "[label=\"" + escapar(hijo.ToString()) + "\"];\n";
                grafo += padre + "->" + nombrehijo + ";\n";
                contador++;
                if (hijo.ToString() != Constantes.EXPRESION)
                {
                    recorrerAST(nombrehijo, hijo);
                }
            }
        }

        private static String escapar(String cadena)
        {

            cadena = cadena.Replace("\\", "\\\\");
            cadena = cadena.Replace("\"", "\\\"");
            return cadena;
        }

    }
}
