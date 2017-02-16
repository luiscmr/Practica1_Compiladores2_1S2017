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


            KeyTerm t_number = ToTerm("Number",Constantes.T_NUM),
                    t_str = ToTerm("String", Constantes.T_STR),
                    t_bool = ToTerm("Bool", Constantes.T_BOOL),
                    t_void = ToTerm("Void", Constantes.T_VOID),
                    define = ToTerm("Define"),
                    incluye = ToTerm("Incluye"),
                    principal = ToTerm("Principal"),
                    mas = ToTerm("+",Constantes.OP_SUM),
                    menos = ToTerm("-",Constantes.OP_RES),
                    por = ToTerm("*",Constantes.OP_MUL),
                    div = ToTerm("/",Constantes.OP_DIV),
                    mod = ToTerm("%",Constantes.OP_MOD),
                    pot = ToTerm("^",Constantes.OP_POT),
                    igual = ToTerm("==", Constantes.OP_IGUAL),
                    diferente = ToTerm("!=", Constantes.OP_DIF),
                    menor = ToTerm("<", Constantes.OP_MENOR),
                    mayor = ToTerm(">", Constantes.OP_MAYOR),
                    menor_igual = ToTerm("<=", Constantes.OP_MEN_Q),
                    mayor_igual = ToTerm(">=", Constantes.OP_MAY_Q),
                    comparacion = ToTerm("~", Constantes.OP_COMP),
                    and = ToTerm("&&", Constantes.OP_AND),
                    or = ToTerm("||", Constantes.OP_OR),
                    xor = ToTerm("|&", Constantes.OP_XOR),
                    not = ToTerm("!", Constantes.OP_NOT),
                    fun_dibujar_exp = ToTerm("DibujarEXP"),
                    fun_dibujar_ast = ToTerm("DibujarAST"),
                    fun_mostrar = ToTerm("Mostrar"),
                    mientras = ToTerm("Mientras"),
                    detener = ToTerm("Detener"),
                    continuar = ToTerm("Continuar"),
                    retorno = ToTerm("Retorno"),
                    para = ToTerm("Para"),
                    incremento = ToTerm("++",Constantes.INCREMENTO),
                    decremento = ToTerm("--",Constantes.DECREMENTO),
                    hasta = ToTerm("Hasta"),
                    v_true = ToTerm("true",Constantes.TRUE),
                    v_false = ToTerm("flase",Constantes.FALSE),
                    si = ToTerm("Si"),
                    sino = ToTerm("Sino"),
                    selecciona = ToTerm("Selecciona"),
                    defecto = ToTerm("Defecto")
                    ;

            #endregion

            #region No Terminales

            NonTerminal
                START = new NonTerminal(Constantes.INICIO),
                HEADER = new NonTerminal(Constantes.ENCABEZADO),
                LISTA_HEADER = new NonTerminal("Lista Header"),
                INCLUYE = new NonTerminal(Constantes.INCLUYE),
                DEFINE_INCERTEZA = new NonTerminal(Constantes.INCERTEZA),
                DEFINE_RUTA = new NonTerminal(Constantes.RUTA),
                BODY = new NonTerminal(Constantes.CUERPO_PROGRAMA),
                DECLARACION = new NonTerminal(Constantes.DECLARACION),
                TIPO_VARIABLE = new NonTerminal("Tipo Variable"),
                ASIG_VARIABLE = new NonTerminal("Asignacion Variable"),
                L_ID = new NonTerminal(Constantes.LISTA_ID),
                FUNCION = new NonTerminal(Constantes.FUNCION),
                TIPO_FUNCION = new NonTerminal("Tipo Funcion"),
                ELEMENTO_BODY = new NonTerminal("Elemento Body"),
                LISTA_BODY = new NonTerminal("Lista Body"),
                PRINCIPAL = new NonTerminal(Constantes.PRINCIPAL),
                EXP = new NonTerminal(Constantes.EXPRESION),
                LOG = new NonTerminal(Constantes.LOGICA),
                ARIT = new NonTerminal(Constantes.ARITMETICA),
                REL = new NonTerminal(Constantes.RELACIONAL),
                VAL = new NonTerminal(Constantes.VALOR),
                PRIMITIVO = new NonTerminal("Primitivo"),
                L_PARAM = new NonTerminal("Lista Parametros"),
                L_PARAM_2 = new NonTerminal(Constantes.LISTA_PARAMETRO),
                CUERPO_FUNC = new NonTerminal(Constantes.CUERPO_FUNCION),
                CUERPO_FUNC_CONT = new NonTerminal(Constantes.CUERPO_FUNCION),
                CUERPO_FUNC_DET = new NonTerminal(Constantes.CUERPO_FUNCION),
                INSTRUCCION = new NonTerminal("Instruccion"),
                INSTRUCCION_CONT = new NonTerminal("Instruccion con Continuar"),
                INSTRUCCION_DET = new NonTerminal("Instruccion con Detener"),
                LLAMADA = new NonTerminal(Constantes.LLAMADA),
                VALORES_LLAMADA_T = new NonTerminal("Val llamada"),
                VALORES_LLAMADA = new NonTerminal(Constantes.LISTA_VAL),
                RETORNO = new NonTerminal(Constantes.RETORNO),
                SI = new NonTerminal(Constantes.SI),
                SELECCIONA = new NonTerminal(Constantes.SELECCIONA),
                PARA = new NonTerminal(Constantes.PARA),
                HASTA = new NonTerminal(Constantes.HASTA),
                MIENTRAS = new NonTerminal(Constantes.MIENTRAS),
                DETENER = new NonTerminal(Constantes.DETENER),
                CONTINUAR = new NonTerminal(Constantes.CONTINUAR),
                MOSTRAR = new NonTerminal(Constantes.MOSTRAR),
                PARAM_MOSTRAR = new NonTerminal("Parametros Mostrar TEMP"),
                DIBUJAR_AST = new NonTerminal(Constantes.DIBUJARAST),
                DIBUJAR_EXP = new NonTerminal(Constantes.DIBUJAREXP),
                EXP_RETORNO = new NonTerminal("EXP Retorno"),
                OP = new NonTerminal("Op"),
                SI_SINO = new NonTerminal(Constantes.SI_SINO),
                SINO = new NonTerminal(Constantes.SINO),
                CUERPO_SELECCIONA = new NonTerminal("Cuerpo Selecciona"),
                VALOR_N = new NonTerminal("Valor N"),
                DEFECTO = new NonTerminal(Constantes.DEFECTO),
                TIPO_VALOR = new NonTerminal("Tipo Valor"),
                VALOR_ANIDADO = new NonTerminal("Valor Anidado"),
                ASIGNACION = new NonTerminal(Constantes.ASIGNACION),
                LLAMADA_ENTRE_EXP = new NonTerminal("Llamada Entre EXP"),
                NUMBER = new NonTerminal(Constantes.T_NUM),
                STRING = new NonTerminal(Constantes.T_STR),
                BOOLEAN = new NonTerminal(Constantes.T_BOOL),
                VOID = new NonTerminal(Constantes.T_VOID),
                INCREMENTO = new NonTerminal(Constantes.INCREMENTO),
                DECREMENTO = new NonTerminal(Constantes.DECREMENTO),
                TRUE = new NonTerminal(Constantes.TRUE),
                FALSE = new NonTerminal(Constantes.FALSE)
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

            FUNCION.Rule = VOID + id + "(" + L_PARAM + ")" + "{" + CUERPO_FUNC + "}"
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
                                 LLAMADA + ";"
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
            
            LLAMADA.Rule = id + "(" + VALORES_LLAMADA_T + ")";
            VALORES_LLAMADA_T.Rule = VALORES_LLAMADA | Empty;
            VALORES_LLAMADA.Rule = MakeStarRule(VALORES_LLAMADA, ToTerm(","), EXP);
            LLAMADA.ErrorRule = SyntaxError + ";";

            #endregion

            #region Dibujar AST
            DIBUJAR_AST.Rule = fun_dibujar_ast + "(" + id + ")" + ";";
            #endregion

            #region Dibujar EXP
            DIBUJAR_EXP.Rule = fun_dibujar_exp + "(" + EXP + ")" + ";";
            #endregion

            #region Mostrar
            MOSTRAR.Rule = fun_mostrar + "(" + cadena + PARAM_MOSTRAR + ")" + ";";

            PARAM_MOSTRAR.Rule = "," + VALORES_LLAMADA
                                | Empty
                                ;

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
            OP.Rule = INCREMENTO | DECREMENTO;
            INCREMENTO.Rule = incremento;
            DECREMENTO.Rule = decremento;
            #endregion

            #region Hasta
            HASTA.Rule = hasta + "(" + EXP + ")" + "{" + CUERPO_FUNC_CONT + "}";
            #endregion

            #region Si
            SI_SINO.Rule = SI + SINO;
            SI.Rule = si + "(" + EXP + ")" + "{" + CUERPO_FUNC_CONT + "}";
            SINO.Rule = Empty | sino + "{" + CUERPO_FUNC_CONT + "}" ;
            #endregion

            #region Seleccion
            SELECCIONA.Rule = selecciona + "(" + EXP + ")"  + CUERPO_SELECCIONA ;
            CUERPO_SELECCIONA.Rule = VALOR_ANIDADO + DEFECTO;
            VALOR_ANIDADO.Rule = MakePlusRule(VALOR_ANIDADO, VALOR_N);
            VALOR_N.Rule = TIPO_VALOR + ":" + "{" + CUERPO_FUNC_DET + "}";
            TIPO_VALOR.Rule = numero | cadena ;
            DEFECTO.Rule = defecto + ":" + "{" + CUERPO_FUNC_DET + "}";

            #endregion

            #region Asignacion
            ASIGNACION.Rule = id + "=" + EXP + ";";
            #endregion

            #endregion

            #region Declaracion

            DECLARACION.Rule = TIPO_VARIABLE + L_ID + ASIG_VARIABLE + ";";

            TIPO_VARIABLE.Rule = NUMBER | STRING | BOOLEAN;

            NUMBER.Rule = t_number;
            STRING.Rule = t_str;
            BOOLEAN.Rule = t_bool;
            VOID.Rule = t_void;

            L_ID.Rule = MakePlusRule(L_ID,ToTerm(","),id);

            ASIG_VARIABLE.Rule = "=" + EXP
                |Empty
                ;

            DECLARACION.ErrorRule = SyntaxError + ";";

            #endregion

            #endregion

            #region Expresion

            EXP.Rule = LOG
                       ;
            LOG.Rule = LOG + and + LOG
                        | LOG + or + LOG
                        | LOG + xor + LOG
                        | not + LOG
                        | REL
                        ;
            REL.Rule = REL + igual + REL
                        | REL + diferente + REL
                        | REL + menor + REL
                        | REL + mayor + REL
                        | REL + mayor_igual + REL
                        | REL + menor_igual + REL
                        | REL + comparacion + REL
                        | ARIT
                        ;
            ARIT.Rule = ARIT + mas + ARIT
                        | ARIT + menos + ARIT
                        | ARIT + por + ARIT
                        | ARIT + div + ARIT
                        | ARIT + mod + ARIT
                        | ARIT + pot + ARIT
                        | menos + ARIT
                        | VAL
                        | "(" + EXP + ")"
                        ;

            VAL.Rule = id | LLAMADA | PRIMITIVO;
            PRIMITIVO.Rule = numero | cadena | TRUE | FALSE;
            TRUE.Rule = v_true;
            FALSE.Rule = v_false;

            #endregion

            #endregion

            this.Root = START;

            LISTA_HEADER.ErrorRule = SyntaxError + Eos;
            
            RegisterOperators(100, Associativity.Right, pot);
            RegisterOperators(95, Associativity.Left, por, div, mod);
            RegisterOperators(90, Associativity.Left, mas, menos);
            RegisterOperators(85,  igual, diferente, menor, mayor, menor_igual, mayor_igual,comparacion);
            RegisterOperators(80, not);
            RegisterOperators(75, and);
            RegisterOperators(70, xor);
            RegisterOperators(65, or);


            MarkPunctuation("{", "}", "(", ")", ";", "," , "Mostrar","Define","Incluye","Principal","=","Retorno", "Si",
                "Sino","Selecciona","Defecto",":","Para","Mientras","Hasta","Number", "Void", "String","Bool","Detener"
                ,"Continuar","Retorno","++","--");
            MarkTransient(LISTA_HEADER,ELEMENTO_BODY,TIPO_VARIABLE, TIPO_FUNCION, ASIG_VARIABLE,L_PARAM,PARAM_MOSTRAR
                ,INSTRUCCION,INSTRUCCION_DET, INSTRUCCION_CONT,TIPO_VALOR,EXP_RETORNO,OP,VALORES_LLAMADA_T);
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
