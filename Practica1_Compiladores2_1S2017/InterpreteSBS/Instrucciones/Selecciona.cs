using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Irony.Parsing;
using Practica1_Compiladores2_1S2017.InterpreteSBS.Expresiones;
using Practica1_Compiladores2_1S2017.Analizador;
using Practica1_Compiladores2_1S2017.Errores;

namespace Practica1_Compiladores2_1S2017.InterpreteSBS.Instrucciones
{
    class Selecciona : InstruccionAbstracta
    {
        public Selecciona(ParseTreeNode Instruccion, bool PermiteInterrupciones) : base(Instruccion, PermiteInterrupciones)
        {
        }

        public Selecciona(ParseTreeNode Instruccion) : base(Instruccion, false)
        {
        }


        public override Resultado ejecutar(Contexto ctx, int nivel)
        {
            bool cumplio = false;
            Resultado res = FabricarResultado.creaOk();

            Resultado cond = new Expresion(instruccion.ChildNodes[0].ChildNodes[0]).resolver(ctx);
            ParseTreeNode CuerpoSelecciona = instruccion.ChildNodes[1].ChildNodes[0];


            String val = cond.Valor;
            String tipo = cond.Tipo;

            if(tipo == Constantes.T_BOOL || tipo == Constantes.T_ERROR)
            {
                ListaErrores.getInstance().setErrorSemantico(instruccion.Token.Location.Line, instruccion.Token.Location.Line, "Error de tipo", Interprete.archivo);
                return FabricarResultado.creaFail();
            }
            foreach(var ValorN in CuerpoSelecciona.ChildNodes )
            {
                String valN = ValorN.ChildNodes[0].Token.Text;
                String tipoN = ValorN.ChildNodes[0].Term.ToString();

                if(tipoN == Constantes.T_STR)
                {
                    valN = valN.Substring(1, valN.Length - 2);
                }
                if (tipo != tipoN)
                {
                    ListaErrores.getInstance().setErrorSemantico(instruccion.Token.Location.Line, instruccion.Token.Location.Line, "los tipos no coinciden", Interprete.archivo);
                    res = FabricarResultado.creaFail();
                    break;
                }
                if ( cumplio == true || val.CompareTo(valN)!=0) { continue; }
                cumplio = true;
                Cuerpo cuerpo = new Cuerpo(ValorN.ChildNodes[1], true);
                res = cuerpo.ejecutar(ctx, nivel);
                if (res.esRetorno())
                {
                    break;
                }
                if (res.esDetener())
                {
                    res = FabricarResultado.creaOk();
                    break;
                }
                ctx.limpiarContexto(nivel);
            }

            if (instruccion.ChildNodes[1].ChildNodes.Count > 1)
            {
                if(cumplio == false)
                {
                    var defecto = instruccion.ChildNodes[1].ChildNodes[1];
                    Cuerpo cuerpo = new Cuerpo(defecto.ChildNodes[0], true);
                    res = cuerpo.ejecutar(ctx, nivel);
                    if (res.esDetener())
                    {
                        res = FabricarResultado.creaOk();
                    }
                }
            }
            
            return res;
        }
    }
}
