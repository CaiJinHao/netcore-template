using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;
using Common.Utility.Other;

namespace Common.Utility.Encryption.JSEncrypt
{
    public class Jsaaencode
    {

        /// <summary>
        /// 不会自动去注释 加密处理一定不能有中文 否则会执行错误
        /// </summary>
        /// <param name="_yourScript"></param>
        /// <returns></returns>
        public static string EncodeJs(string _yourScript)
        {
            //var javaScript = new ECMAScriptPacker().basicCompression(_yourScript);
            var javaScript = _yourScript;
            var t = "";
            var b = new string[] {
                "(c^_^o)",
                "(ﾟΘﾟ)",
                "((o^_^o) - (ﾟΘﾟ))",
                "(o^_^o)",
                "(ﾟｰﾟ)",
                "((ﾟｰﾟ) + (ﾟΘﾟ))",
                "((o^_^o) +(o^_^o))",
                "((ﾟｰﾟ) + (o^_^o))",
                "((ﾟｰﾟ) + (ﾟｰﾟ))",
                "((ﾟｰﾟ) + (ﾟｰﾟ) + (ﾟΘﾟ))",
                "(ﾟДﾟ) .ﾟωﾟﾉ",
                "(ﾟДﾟ) .ﾟΘﾟﾉ",
                "(ﾟДﾟ) ['c']",
                "(ﾟДﾟ) .ﾟｰﾟﾉ",
                "(ﾟДﾟ) .ﾟДﾟﾉ",
                "(ﾟДﾟ) [ﾟΘﾟ]"
            };
            var r = "ﾟωﾟﾉ= /｀ｍ´）ﾉ ~┻━┻   //*´∇｀*/ ['_']; o=(ﾟｰﾟ)  =_=3; c=(ﾟΘﾟ) =(ﾟｰﾟ)-(ﾟｰﾟ); ";

            r += "(ﾟДﾟ) =(ﾟΘﾟ)= (o^_^o)/ (o^_^o);" +
                "(ﾟДﾟ)={ﾟΘﾟ: '_' ,ﾟωﾟﾉ : ((ﾟωﾟﾉ==3) +'_') [ﾟΘﾟ] " +
                ",ﾟｰﾟﾉ :(ﾟωﾟﾉ+ '_')[o^_^o -(ﾟΘﾟ)] " +
                ",ﾟДﾟﾉ:((ﾟｰﾟ==3) +'_')[ﾟｰﾟ] }; (ﾟДﾟ) [ﾟΘﾟ] =((ﾟωﾟﾉ==3) +'_') [c^_^o];" +
                "(ﾟДﾟ) ['c'] = ((ﾟДﾟ)+'_') [ (ﾟｰﾟ)+(ﾟｰﾟ)-(ﾟΘﾟ) ];" +
                "(ﾟДﾟ) ['o'] = ((ﾟДﾟ)+'_') [ﾟΘﾟ];" +
                "(ﾟoﾟ)=(ﾟДﾟ) ['c']+(ﾟДﾟ) ['o']+(ﾟωﾟﾉ +'_')[ﾟΘﾟ]+ ((ﾟωﾟﾉ==3) +'_') [ﾟｰﾟ] + " +
                "((ﾟДﾟ) +'_') [(ﾟｰﾟ)+(ﾟｰﾟ)]+ ((ﾟｰﾟ==3) +'_') [ﾟΘﾟ]+" +
                "((ﾟｰﾟ==3) +'_') [(ﾟｰﾟ) - (ﾟΘﾟ)]+(ﾟДﾟ) ['c']+" +
                "((ﾟДﾟ)+'_') [(ﾟｰﾟ)+(ﾟｰﾟ)]+ (ﾟДﾟ) ['o']+" +
                "((ﾟｰﾟ==3) +'_') [ﾟΘﾟ];(ﾟДﾟ) ['_'] =(o^_^o) [ﾟoﾟ] [ﾟoﾟ];" +
                "(ﾟεﾟ)=((ﾟｰﾟ==3) +'_') [ﾟΘﾟ]+ (ﾟДﾟ) .ﾟДﾟﾉ+" +
                "((ﾟДﾟ)+'_') [(ﾟｰﾟ) + (ﾟｰﾟ)]+((ﾟｰﾟ==3) +'_') [o^_^o -ﾟΘﾟ]+" +
                "((ﾟｰﾟ==3) +'_') [ﾟΘﾟ]+ (ﾟωﾟﾉ +'_') [ﾟΘﾟ]; " +
                "(ﾟｰﾟ)+=(ﾟΘﾟ); (ﾟДﾟ)[ﾟεﾟ]='\\\\'; " +
                "(ﾟДﾟ).ﾟΘﾟﾉ=(ﾟДﾟ+ ﾟｰﾟ)[o^_^o -(ﾟΘﾟ)];" +
                "(oﾟｰﾟo)=(ﾟωﾟﾉ +'_')[c^_^o];" +
                "(ﾟДﾟ) [ﾟoﾟ]='\\\"';" +
                "(ﾟДﾟ) ['_'] ( (ﾟДﾟ) ['_'] (ﾟεﾟ+" +
                "(ﾟДﾟ)[ﾟoﾟ]+ ";

            var binaryHelper = new GenericBinaryHelper();

            for (var i = 0; i < javaScript.Length; i++)
            {
                var n = javaScript.ToCharArray()[i];
                t = "(ﾟДﾟ)[ﾟεﾟ]+";
                if (n <= 127)
                {
                    string OutPutMatch(Match match)
                    {
                        return b[Convert.ToInt64(match.Value)] + "+ ";
                    }
                    t += new Regex("[0-7]").Replace(binaryHelper.ConvertGenericBinary(Convert.ToInt64(n).ToString(), 10, 8), new MatchEvaluator(OutPutMatch));//将十进制转换为8进制
                }
                else
                {
                    string OutPutMatch(Match match)
                    {
                        var _d = binaryHelper.ConvertGenericBinary(match.Value, 16, 10);//将十六进制转换为十进制
                        return b[Convert.ToInt64(_d)] + "+ ";
                    }
                    var _m = new Regex("[0 - 9a - f]{ 4}$").Match("000" + binaryHelper.ConvertGenericBinary(Convert.ToInt64(n).ToString(), 10, 16)).Groups[0].Value;//将十进制转换为16进制
                    t += "(oﾟｰﾟo)+ " + new Regex("[0 - 9a - f] ").Replace(_m, OutPutMatch);
                }
                r += t;
            }
            r += "(ﾟДﾟ)[ﾟoﾟ]) (ﾟΘﾟ)) ('_');";
            return r;
        }

        /// <summary>
        /// 自动去除注释 加密处理一定不能有中文
        /// </summary>
        /// <param name="_yourScript"></param>
        /// <returns></returns>
        public static string EncodeJsCompression(string _yourScript)
        {
            var javaScript = new ECMAScriptPacker().basicCompression(_yourScript);
            var t = "";
            var b = new string[] {
                "(c^_^o)",
                "(ﾟΘﾟ)",
                "((o^_^o) - (ﾟΘﾟ))",
                "(o^_^o)",
                "(ﾟｰﾟ)",
                "((ﾟｰﾟ) + (ﾟΘﾟ))",
                "((o^_^o) +(o^_^o))",
                "((ﾟｰﾟ) + (o^_^o))",
                "((ﾟｰﾟ) + (ﾟｰﾟ))",
                "((ﾟｰﾟ) + (ﾟｰﾟ) + (ﾟΘﾟ))",
                "(ﾟДﾟ) .ﾟωﾟﾉ",
                "(ﾟДﾟ) .ﾟΘﾟﾉ",
                "(ﾟДﾟ) ['c']",
                "(ﾟДﾟ) .ﾟｰﾟﾉ",
                "(ﾟДﾟ) .ﾟДﾟﾉ",
                "(ﾟДﾟ) [ﾟΘﾟ]"
            };
            var r = "ﾟωﾟﾉ= /｀ｍ´）ﾉ ~┻━┻   //*´∇｀*/ ['_']; o=(ﾟｰﾟ)  =_=3; c=(ﾟΘﾟ) =(ﾟｰﾟ)-(ﾟｰﾟ); ";

            r += "(ﾟДﾟ) =(ﾟΘﾟ)= (o^_^o)/ (o^_^o);" +
                "(ﾟДﾟ)={ﾟΘﾟ: '_' ,ﾟωﾟﾉ : ((ﾟωﾟﾉ==3) +'_') [ﾟΘﾟ] " +
                ",ﾟｰﾟﾉ :(ﾟωﾟﾉ+ '_')[o^_^o -(ﾟΘﾟ)] " +
                ",ﾟДﾟﾉ:((ﾟｰﾟ==3) +'_')[ﾟｰﾟ] }; (ﾟДﾟ) [ﾟΘﾟ] =((ﾟωﾟﾉ==3) +'_') [c^_^o];" +
                "(ﾟДﾟ) ['c'] = ((ﾟДﾟ)+'_') [ (ﾟｰﾟ)+(ﾟｰﾟ)-(ﾟΘﾟ) ];" +
                "(ﾟДﾟ) ['o'] = ((ﾟДﾟ)+'_') [ﾟΘﾟ];" +
                "(ﾟoﾟ)=(ﾟДﾟ) ['c']+(ﾟДﾟ) ['o']+(ﾟωﾟﾉ +'_')[ﾟΘﾟ]+ ((ﾟωﾟﾉ==3) +'_') [ﾟｰﾟ] + " +
                "((ﾟДﾟ) +'_') [(ﾟｰﾟ)+(ﾟｰﾟ)]+ ((ﾟｰﾟ==3) +'_') [ﾟΘﾟ]+" +
                "((ﾟｰﾟ==3) +'_') [(ﾟｰﾟ) - (ﾟΘﾟ)]+(ﾟДﾟ) ['c']+" +
                "((ﾟДﾟ)+'_') [(ﾟｰﾟ)+(ﾟｰﾟ)]+ (ﾟДﾟ) ['o']+" +
                "((ﾟｰﾟ==3) +'_') [ﾟΘﾟ];(ﾟДﾟ) ['_'] =(o^_^o) [ﾟoﾟ] [ﾟoﾟ];" +
                "(ﾟεﾟ)=((ﾟｰﾟ==3) +'_') [ﾟΘﾟ]+ (ﾟДﾟ) .ﾟДﾟﾉ+" +
                "((ﾟДﾟ)+'_') [(ﾟｰﾟ) + (ﾟｰﾟ)]+((ﾟｰﾟ==3) +'_') [o^_^o -ﾟΘﾟ]+" +
                "((ﾟｰﾟ==3) +'_') [ﾟΘﾟ]+ (ﾟωﾟﾉ +'_') [ﾟΘﾟ]; " +
                "(ﾟｰﾟ)+=(ﾟΘﾟ); (ﾟДﾟ)[ﾟεﾟ]='\\\\'; " +
                "(ﾟДﾟ).ﾟΘﾟﾉ=(ﾟДﾟ+ ﾟｰﾟ)[o^_^o -(ﾟΘﾟ)];" +
                "(oﾟｰﾟo)=(ﾟωﾟﾉ +'_')[c^_^o];" +
                "(ﾟДﾟ) [ﾟoﾟ]='\\\"';" +
                "(ﾟДﾟ) ['_'] ( (ﾟДﾟ) ['_'] (ﾟεﾟ+" +
                "(ﾟДﾟ)[ﾟoﾟ]+ ";

            var binaryHelper = new GenericBinaryHelper();

            for (var i = 0; i < javaScript.Length; i++)
            {
                var n = javaScript.ToCharArray()[i];
                t = "(ﾟДﾟ)[ﾟεﾟ]+";
                if (n <= 127)
                {
                    string OutPutMatch(Match match)
                    {
                        return b[Convert.ToInt64(match.Value)] + "+ ";
                    }
                    t += new Regex("[0-7]").Replace(binaryHelper.ConvertGenericBinary(Convert.ToInt64(n).ToString(), 10, 8), new MatchEvaluator(OutPutMatch));//将十进制转换为8进制
                }
                else
                {
                    string OutPutMatch(Match match)
                    {
                        var _d = binaryHelper.ConvertGenericBinary(match.Value, 16, 10);//将十六进制转换为十进制
                        return b[Convert.ToInt64(_d)] + "+ ";
                    }
                    var _m = new Regex("[0 - 9a - f]{ 4}$").Match("000" + binaryHelper.ConvertGenericBinary(Convert.ToInt64(n).ToString(), 10, 16)).Groups[0].Value;//将十进制转换为16进制
                    t += "(oﾟｰﾟo)+ " + new Regex("[0 - 9a - f] ").Replace(_m, OutPutMatch);
                }
                r += t;
            }
            r += "(ﾟДﾟ)[ﾟoﾟ]) (ﾟΘﾟ)) ('_');";
            return r;
        }

        #region DecodeJs


        #endregion
    }
}
