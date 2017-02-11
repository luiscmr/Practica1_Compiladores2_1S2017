using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Irony.Ast;
using Irony.Parsing;

namespace Practica1_Compiladores2_1S2017.Analizador
{
    class Gramatica : Grammar
    {
        public Gramatica (): base(caseSensitive : true)
        {


            #region Expresiones Regulares

            //Expresion regular para ids con terminacion .sbs, cadena y numero
            RegexBasedTerminal idsbs = new RegexBasedTerminal("[a-zA-Z]([a-zA-Z0-9])*.sbs");
            IdentifierTerminal id = new IdentifierTerminal("identificador");
            StringLiteral cadena = new StringLiteral("cadena", "\"",StringOptions.AllowsLineBreak);
            NumberLiteral numero = new NumberLiteral("numero");
            //Expresion regular para comentarios
            CommentTerminal comentarioBloque = new CommentTerminal("comentarioBloque", "#*", "*#");
            CommentTerminal comentarioLinea = new CommentTerminal("comentarioLinea", "#", "\n");
            //Con esto se omiten los comentarios
            base.NonGrammarTerminals.Add(comentarioBloque);
            base.NonGrammarTerminals.Add(comentarioLinea);


            #endregion

            #region No Terminales

            NonTerminal
                START = new NonTerminal("Start"),
                HEADER = new NonTerminal("Header"),
                LISTA_HEADER = new NonTerminal("Lista Header"),
                INCLUYE = new NonTerminal("Incluye"),
                DEFINE_INCERTEZA = new NonTerminal("Define Incerteza"),
                DEFINE_RUTA = new NonTerminal("Define Ruta"),
                BODY = new NonTerminal("Body"),
                DECLARACION = new NonTerminal("Declaracion"),
                TIPO_VARIABLE = new NonTerminal("Tipo Variable"),
                ASIG_VARIABLE = new NonTerminal("Asignacion Variable"),
                L_ID = new NonTerminal("Identificadores"),
                FUNCION = new NonTerminal("Funcion"),
                TIPO_FUNCION = new NonTerminal("Tipo Funcion"),
                ELEMENTO_BODY = new NonTerminal("Elemento Body"),
                LISTA_BODY = new NonTerminal("Lista Body"),
                PRINCIPAL = new NonTerminal("Principal"),
                EXPRESION = new NonTerminal("Expresion");

            #endregion

            #region Gramatica

            // START -> HEADER BODY
            START.Rule = HEADER + BODY;

            #region HEADER


            /*
             HEADER -> HEADER ELEMENTO
                      |ELEMENTO
                      |null
                      ;
             */
            HEADER.Rule = MakeStarRule(HEADER, LISTA_HEADER);


            /*
             ELEMENTO -> INCLUYE
                        |DEFINE_INCERTEZA
                        |DEFINE_RUTA
                        ;
             */
            LISTA_HEADER.Rule = INCLUYE
                | DEFINE_INCERTEZA
                | DEFINE_RUTA
                ;

            // INCLUYE -> 'Incluye' idsbs
            INCLUYE.Rule = "Incluye" + idsbs;

            // DEFINE_INCERTEZA -> 'Define' numero
            DEFINE_INCERTEZA.Rule = "Define" + numero;

            // DEFINE_RUTA -> 'Define' cadena
            DEFINE_RUTA.Rule = "Define" + cadena;

            #endregion

            #region BODY

            /*
                 BODY -> BODY LISTA_BODY
                        | LISTA_BODY
                        
            */
            BODY.Rule = MakePlusRule(BODY, ELEMENTO_BODY);
            /*
                LISTA_BODY = VARIABLE
                            | FUNCION
                            | PRINCIPAL
                            ;
            */

            ELEMENTO_BODY.Rule = PRINCIPAL
                              | FUNCION
                              | DECLARACION
                              ;

            #region Principal

            PRINCIPAL.Rule = "Principal" + "(" + ")" + "{" + "}";

            #endregion

            #region Funcion

            FUNCION.Rule = TIPO_FUNCION + id + "(" + ")" + "{" + "}";

            #endregion


            TIPO_FUNCION.Rule = ToTerm("Void") | TIPO_VARIABLE;

            DECLARACION.Rule = TIPO_VARIABLE + L_ID + ASIG_VARIABLE;

            TIPO_VARIABLE.Rule = ToTerm("Number") | ToTerm("String") | ToTerm("Bool");


            L_ID.Rule = MakePlusRule(L_ID,ToTerm(","),id);

            ASIG_VARIABLE.Rule = "=" + id
                |Empty
                ;


            #endregion
            
            #endregion

            this.Root = START;

            LISTA_HEADER.ErrorRule = SyntaxError + Eos;
            
            MarkPunctuation("Incluye", "Define");
            MarkTransient(LISTA_HEADER,ELEMENTO_BODY,TIPO_VARIABLE, TIPO_FUNCION, ASIG_VARIABLE);
            AddToNoReportGroup(Eos);


        }
    }
}
