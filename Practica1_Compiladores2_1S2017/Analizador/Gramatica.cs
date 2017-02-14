using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Irony.Ast;
using Irony.Parsing;
using System.Drawing;
using WINGRAPHVIZLib;
using System.IO;

namespace Practica1_Compiladores2_1S2017.Analizador
{
    class Gramatica : Grammar
    {
        public Gramatica() : base(caseSensitive: true)
        {


            #region Expresiones Regulares

            //Expresion regular para ids con terminacion .sbs, cadena y numero
            RegexBasedTerminal idsbs = new RegexBasedTerminal("[a-zA-Z]([a-zA-Z0-9])*.sbs");
            IdentifierTerminal id = new IdentifierTerminal("identificador");
            StringLiteral cadena = new StringLiteral("cadena", "\"", StringOptions.AllowsLineBreak);
            NumberLiteral numero = new NumberLiteral("numero");
            //Expresion regular para comentarios
            CommentTerminal comentarioBloque = new CommentTerminal("comentarioBloque", "#*", "*#");
            CommentTerminal comentarioLinea = new CommentTerminal("comentarioLinea", "#", "\n");
            //Con esto se omiten los comentarios
            base.NonGrammarTerminals.Add(comentarioBloque);
            base.NonGrammarTerminals.Add(comentarioLinea);


            KeyTerm t_number = ToTerm("Number"),
                    t_str = ToTerm("String"),
                    t_bool = ToTerm("Bool"),
                    t_void = ToTerm("Void"),
                    define = ToTerm("Define"),
                    incluye = ToTerm("Incluye"),
                    principal = ToTerm("Principal"),
                    mas = ToTerm("+"),
                    menos = ToTerm("-"),
                    por = ToTerm("*"),
                    div = ToTerm("/"),
                    mod = ToTerm("%"),
                    pot = ToTerm("^"),
                    igual = ToTerm("=="),
                    diferente = ToTerm("!="),
                    menor = ToTerm("<"),
                    mayor = ToTerm(">"),
                    menor_igual = ToTerm("<="),
                    mayor_igual = ToTerm(">="),
                    comparacion = ToTerm("~"),
                    and = ToTerm("&&"),
                    or = ToTerm("||"),
                    xor = ToTerm("|&"),
                    not = ToTerm("!"),
                    fun_dibujar_exp = ToTerm("DibujarEXP"),
                    fun_dibujar_ast = ToTerm("DibujarAST"),
                    fun_mostrar = ToTerm("Mostrar"),
                    mientras = ToTerm("Mientras"),
                    detener = ToTerm("Detener"),
                    continuar = ToTerm("Continuar"),
                    retorno = ToTerm("Retorno"),
                    para = ToTerm("Para"),
                    incremento = ToTerm("++"),
                    decremento = ToTerm("--"),
                    hasta = ToTerm("Hasta"),
                    v_true = ToTerm("true"),
                    v_false = ToTerm("flase"),
                    si = ToTerm("Si"),
                    sino = ToTerm("Sino"),
                    selecciona = ToTerm("Selecciona"),
                    defecto = ToTerm("Defecto")
                    ;

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
                EXP = new NonTerminal("Expresion"),
                L_PARAM = new NonTerminal("Lista Parametros"),
                L_PARAM_2 = new NonTerminal("Parametros"),
                CUERPO_FUNC = new NonTerminal("Cuerpo Funcion"),
                CUERPO_FUNC_CONT = new NonTerminal("Cuerpo Funcion Con Continuar"),
                CUERPO_FUNC_DET = new NonTerminal("Cuerpo Funcion Con Detener"),
                INSTRUCCION = new NonTerminal("Instruccion"),
                INSTRUCCION_CONT = new NonTerminal("Instruccion con Continuar"),
                INSTRUCCION_DET = new NonTerminal("Instruccion con Detener"),
                LLAMADA = new NonTerminal("Llamada"),
                VALORES_LLAMADA = new NonTerminal("Lista de Valores"),
                RETORNO = new NonTerminal("Retorno"),
                SI = new NonTerminal(""),
                SELECCIONA = new NonTerminal("Lista de Valores"),
                PARA = new NonTerminal("Lista de Valores"),
                HASTA = new NonTerminal("Lista de Valores"),
                MIENTRAS = new NonTerminal("Lista de Valores"),
                DETENER = new NonTerminal("Lista de Valores"),
                CONTINUAR = new NonTerminal("Lista de Valores"),
                MOSTRAR = new NonTerminal("Mostrar"),
                PARAM_MOSTRAR = new NonTerminal("Parametros Mostrar TEMP"),
                PARAM_MOSTRAR_2 = new NonTerminal("Parametros Mostrar"),
                DIBUJAR_AST = new NonTerminal("Dibujar AST"),
                DIBUJAR_EXP = new NonTerminal("Dibujar EXP"),
                EXP_RETORNO = new NonTerminal("EXP Retorno"),
                OP = new NonTerminal("Op"),
                SI_SINO = new NonTerminal("Si-Sino"),
                SI_ANIDADO = new NonTerminal("Si Anidado"),
                SINO = new NonTerminal("Sino"),
                CUERPO_SELECCIONA = new NonTerminal("Cuerpo Selecciona"),
                VALOR_N = new NonTerminal("Valor N"),
                DEFECTO = new NonTerminal("Defecto"),
                TIPO_VALOR = new NonTerminal("Tipo Valor"),
                VALOR_ANIDADO = new NonTerminal("Valor Anidado"),
                ASIGNACION = new NonTerminal("Asignacion")

                ;
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
            INCLUYE.Rule = incluye + idsbs;

            // DEFINE_INCERTEZA -> 'Define' numero
            DEFINE_INCERTEZA.Rule = define + numero;

            // DEFINE_RUTA -> 'Define' cadena
            DEFINE_RUTA.Rule = define + cadena;

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

            PRINCIPAL.Rule = principal + "(" + ")" + "{" + CUERPO_FUNC + "}";

            #endregion

            #region Funcion

            FUNCION.Rule = t_void + id + "(" + L_PARAM + ")" + "{" + CUERPO_FUNC + "}"
                            | TIPO_VARIABLE + id + "(" + L_PARAM + ")" + "{" + CUERPO_FUNC + "}"
                            ;

            TIPO_FUNCION.Rule = t_void | TIPO_VARIABLE;

            L_PARAM.Rule = MakeStarRule(L_PARAM, ToTerm(","), L_PARAM_2);

            L_PARAM_2.Rule = TIPO_VARIABLE + EXP;



            #endregion

            /*
            Con las instrucciones me heche una buena fumada
            y separe en varios cuerpos de funciones ya que una funcion void puede tener un retorno
            pero deberia de ir vacio, ahora las funciones tipo string, bool y number tiene que obligatoriamente
            retornar un valor de su tipo de funcion.
            el cuerpo de funciones void tambien es para el metodo principal 
            */

            #region Instrucciones


            CUERPO_FUNC.Rule = MakeStarRule(CUERPO_FUNC, INSTRUCCION);

            CUERPO_FUNC_CONT.Rule = MakeStarRule(CUERPO_FUNC_CONT,INSTRUCCION_CONT);

            CUERPO_FUNC_DET.Rule = MakeStarRule(CUERPO_FUNC_DET, INSTRUCCION_DET);


            INSTRUCCION_DET.Rule = INSTRUCCION
                               | DETENER
                               ;

            INSTRUCCION_CONT.Rule = INSTRUCCION_DET
                               | CONTINUAR
                               ;
            INSTRUCCION.Rule =
                                 LLAMADA
                               | DIBUJAR_AST
                               | DIBUJAR_EXP
                               | MOSTRAR
                               | MIENTRAS
                               | SI_SINO
                               | SELECCIONA
                               | PARA
                               | HASTA
                               | DECLARACION
                               | RETORNO
                               | ASIGNACION
                               ;

            #region Llamada

            LLAMADA.Rule = id + "(" + VALORES_LLAMADA + ")" + ";";
            VALORES_LLAMADA.Rule = MakeStarRule(VALORES_LLAMADA, ToTerm(","), EXP);

            #endregion

            #region Dibujar AST
            DIBUJAR_AST.Rule = fun_dibujar_ast + "(" + id + ")" + ";";
            #endregion

            #region Dibujar EXP
            DIBUJAR_EXP.Rule = fun_dibujar_exp + "(" + EXP + ")" + ";";
            #endregion

            #region Mostrar
            MOSTRAR.Rule = fun_mostrar + "(" + cadena + PARAM_MOSTRAR + ")" + ";";

            PARAM_MOSTRAR.Rule = "," + PARAM_MOSTRAR_2
                                | Empty
                                ;
            PARAM_MOSTRAR_2.Rule = MakePlusRule(PARAM_MOSTRAR_2, ToTerm(","), EXP);

            #endregion

            #region Detener
            DETENER.Rule = detener + ";";
            #endregion

            #region Continuar
            CONTINUAR.Rule = continuar + ";";
            #endregion

            #region Retorno
            RETORNO.Rule = retorno + EXP_RETORNO + ";";
            EXP_RETORNO.Rule = EXP | Empty;
            #endregion

            #region Mientras
            MIENTRAS.Rule = mientras + "(" + EXP + ")" + "{" + CUERPO_FUNC_CONT + "}";
            #endregion

            #region Para
            PARA.Rule = para + "(" + t_number + id + "=" + EXP + ";" + EXP + ";" + OP + ")" + "{" + CUERPO_FUNC_CONT + "}";
            OP.Rule = incremento | decremento;
            #endregion

            #region Hasta
            HASTA.Rule = hasta + "(" + EXP + ")" + "{" + CUERPO_FUNC_CONT + "}";
            #endregion

            #region Si
            SI_SINO.Rule = SI + SINO;
            SI.Rule = MakePlusRule(SI, SI_ANIDADO);
            SI_ANIDADO.Rule = si + "(" + EXP + ")" + "{" + CUERPO_FUNC_CONT + "}";
            SINO.Rule = sino + "{" + CUERPO_FUNC_CONT + "}" | Empty;
            #endregion

            #region Seleccion
            SELECCIONA.Rule = selecciona + "(" + EXP + ")" + "{" + CUERPO_SELECCIONA + "}";
            CUERPO_SELECCIONA.Rule = VALOR_ANIDADO + DEFECTO;
            VALOR_ANIDADO.Rule = MakePlusRule(CUERPO_SELECCIONA, VALOR_N);
            VALOR_N.Rule = TIPO_VALOR + ":" + "{" + CUERPO_FUNC_DET + "}";
            TIPO_VALOR.Rule = numero | cadena ;
            DEFECTO.Rule = defecto + ":" + "{" + "}";

            #endregion

            #region Asignacion
            ASIGNACION.Rule = id + "=" + EXP + ";";
            #endregion

            #endregion

            #region Declaracion

            DECLARACION.Rule = TIPO_VARIABLE + L_ID + ASIG_VARIABLE + ";";

            TIPO_VARIABLE.Rule = t_number | t_str | t_bool;
            
            L_ID.Rule = MakePlusRule(L_ID,ToTerm(","),id);

            ASIG_VARIABLE.Rule = "=" + EXP
                |Empty
                ;

            DECLARACION.ErrorRule = SyntaxError + ";";

            #endregion

            #endregion

            #region Expresion

            EXP.Rule = EXP + mas + EXP
                       | EXP + menos + EXP
                       | EXP + por + EXP
                       | EXP + div + EXP
                       | EXP + mod + EXP
                       | EXP + pot + EXP
                       | "(" + EXP + ")"
                       | menos + EXP
                       | EXP + igual + EXP
                       | EXP + diferente + EXP
                       | EXP + menor + EXP
                       | EXP + mayor + EXP
                       | EXP + menor_igual + EXP
                       | EXP + mayor_igual + EXP
                       | EXP + comparacion + EXP
                       | EXP + and + EXP
                       | EXP + or + EXP
                       | EXP + xor + EXP
                       | not + EXP
                       | id
                       | numero
                       | LLAMADA
                       | v_false
                       | v_true
                       | cadena
                       ;

            #endregion

            #endregion

            this.Root = START;

            LISTA_HEADER.ErrorRule = SyntaxError + Eos;
            
            RegisterOperators(100, Associativity.Right, pot);
            RegisterOperators(95, Associativity.Left, por, div, mod);
            RegisterOperators(90, Associativity.Left, mas, menos);
            RegisterOperators(85,  igual, diferente, menor, mayor, menor_igual, mayor_igual);
            RegisterOperators(80, not);
            RegisterOperators(75, and);
            RegisterOperators(70, xor);
            RegisterOperators(65, or);


            MarkPunctuation("{", "}", "(", ")", ";", "," , "Mostrar","Define","Incluye");
            MarkTransient(LISTA_HEADER,ELEMENTO_BODY,TIPO_VARIABLE, TIPO_FUNCION, ASIG_VARIABLE,L_PARAM,PARAM_MOSTRAR);
            AddToNoReportGroup(Eos);


        }

        public static Image getImage(ParseTreeNode raiz)
        {
            String grafoDOT = Graficar.arbolDOT.getDOT(raiz);
            WINGRAPHVIZLib.DOT dot = new WINGRAPHVIZLib.DOT();
            WINGRAPHVIZLib.BinaryImage img = dot.ToJPEG(grafoDOT);
            img.Save("ast.jpeg");
            byte[] imageBytes = Convert.FromBase64String(img.ToBase64String());
            MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
            ms.Write(imageBytes, 0, imageBytes.Length);
            Image image = Image.FromStream(ms, true);
            return image;


        }
        
    }
}
