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
                VARIABLE = new NonTerminal("Variable"),
                FUNCION = new NonTerminal("Funcion"),
                LISTA_BODY = new NonTerminal("Lista Body");

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
                        | NULL
                        ;
                        
            */

            BODY.Rule = MakePlusRule(BODY, LISTA_BODY);
            /*
                LISTA_BODY = VARIABLE
                            | FUNCION
                            ; 
            */
            LISTA_BODY.Rule = VARIABLE
                              | FUNCION
                              ;

            VARIABLE.Rule = "Number" + id 
                            | "String" + id
                            | "Bool" + id
                            ;

            FUNCION.Rule = id;


            #endregion
            
            #endregion

            this.Root = START;

            LISTA_HEADER.ErrorRule = SyntaxError + Eos;
            MarkPunctuation("Incluye", "Define");
            MarkTransient(LISTA_HEADER);
            AddToNoReportGroup(Eos);


        }
    }
}
