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
            StringLiteral cadena = new StringLiteral("cadena", "\"");
            NumberLiteral numero = new NumberLiteral("numero");
            //Expresion regular para comentarios
            CommentTerminal comentarioBloque = new CommentTerminal("comentarioBloque", "#*", "*#");
            CommentTerminal comentarioLinea = new CommentTerminal("comentarioLinea", "#", "\n");
            //Con esto se omiten los comentarios  la hora de generar el arbol
            base.NonGrammarTerminals.Add(comentarioBloque);
            base.NonGrammarTerminals.Add(comentarioLinea);


            #endregion

            #region No Terminales

            NonTerminal
                START = new NonTerminal("Start"),
                HEADER = new NonTerminal("Header"),
                ELEMENTO = new NonTerminal("Elemento"),
                INCLUYE = new NonTerminal("Incluye"),
                DEFINE_INCERTEZA = new NonTerminal("Define incerteza"),
                DEFINE_RUTA = new NonTerminal("Define ruta"),
                BODY = new NonTerminal("Body");

            #endregion

            #region Gramatica

            START.Rule = HEADER + BODY;

            HEADER.Rule = MakeStarRule(HEADER, ELEMENTO);

            ELEMENTO.Rule = INCLUYE
                | DEFINE_INCERTEZA
                | DEFINE_RUTA
                ;


            INCLUYE.Rule = "Incluye" + idsbs;

            DEFINE_INCERTEZA.Rule = "Define" + numero;

            DEFINE_RUTA.Rule = "Define" + cadena;

            BODY.Rule = id;

            #endregion

            this.Root = START;

            ELEMENTO.ErrorRule = SyntaxError + Eos;
            MarkPunctuation("Incluye", "Define");
            MarkTransient(HEADER, BODY,ELEMENTO);
            AddToNoReportGroup(Eos);


        }
    }
}
