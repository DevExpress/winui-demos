using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeatureDemo.ViewModel;
using Microsoft.UI;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Documents;
using Microsoft.UI.Xaml.Media;
using Color = Windows.UI.Color;

namespace FeatureDemo.Common.Internal {
    public enum LexemeType { Error = 0, Block, Symbol, Object, Property, Value, Space, LineBreak, Complex, Comment, PlainText, String, KeyWord }
    public class CodeBlockPresenter {
        public CodeLanguage CodeLanguage { get; set; }
        public CodeBlockPresenter() : this(CodeLanguage.Plain) { }
        public CodeBlockPresenter(CodeLanguage language) {
            CodeLanguage = language;
        }

        public TextBlock ToTextBlock(string text) {
            TextBlock textBlock = new TextBlock();
            FillInlines(text, textBlock.Inlines);
            return textBlock;
        }
        public void FillInlines(string text, InlineCollection collection) {
            text = text.Replace("\r", "");
            CodeLexeme lex = new CodeLexeme(text);
            List<CodeLexeme> res = lex.Parse(CodeLanguage);
            foreach(CodeLexeme elem in res)
                collection.Add(elem.ToInline());
        }
    }
    public class CodeLexeme {
        public string Text { get; set; }
        public LexemeType Type { get; set; }
        public CodeLexeme() : this("") { }
        public CodeLexeme(string text) : this(LexemeType.Complex, text) { }
        public CodeLexeme(LexemeType type, string text) {
            Text = text;
            Type = type;
        }
        public List<CodeLexeme> Parse(CodeLanguage lang) {
            switch(lang) {
                case CodeLanguage.Plain: return (new BaseParser()).Parse(Text);
                case CodeLanguage.Xaml: return (new XamlParser()).Parse(Text);
                case CodeLanguage.CS: return (new CSParser()).Parse(Text);
                case CodeLanguage.VB: return (new VBParser()).Parse(Text);
            }
            return null;
        }
        protected Run CreateRun(string text, Color color) { return new Run() { Text = text, Foreground = new SolidColorBrush(color) }; }
        public Inline ToInline() {
            switch(Type) {
                case LexemeType.Complex: return CreateRun(Text, Colors.LightGray);
                case LexemeType.LineBreak: return CreateRun("\r", Colors.Black); 
                case LexemeType.Object: return CreateRun(Text, Colors.Brown);
                case LexemeType.Property: return CreateRun(Text, Colors.Red);
                case LexemeType.Space: return CreateRun(Text, Colors.Black);
                case LexemeType.Symbol: return CreateRun(Text, Colors.Blue);
                case LexemeType.Value: return CreateRun(Text, Colors.Blue);
                case LexemeType.PlainText: return CreateRun(Text, Colors.Black);
                case LexemeType.Comment: return CreateRun(Text, Colors.Green);
                case LexemeType.Error: return CreateRun(Text, Colors.LightGray);
                case LexemeType.String: return CreateRun(Text, Colors.Brown);
                case LexemeType.KeyWord: return CreateRun(Text, Colors.Blue);
            }
            return null;
        }
    }
    public class SourceString {
        string Source { get; set; }
        int StartIndex { get; set; }
        public SourceString(string source, int startIndex) {
            if(startIndex > source.Length)
                throw new ArgumentException();
            this.Source = source;
            this.StartIndex = startIndex;
        }

        public int Length { get { return Source.Length - StartIndex; } }
        public char this[int index] { get { return Source[StartIndex + index]; } }
        internal int IndexOf(string text) {
            int index = Source.IndexOf(text, StartIndex, StringComparison.Ordinal);
            if(index < 0)
                return index;
            return index - StartIndex;
        }
        internal int IndexOf(string value, int startIndex) {
            int index = Source.IndexOf(value, StartIndex + startIndex, StringComparison.Ordinal);
            if(index < 0)
                return index;
            return index - StartIndex;
        }
        internal int IndexOfAny(char[] anyOf) {
            int index = Source.IndexOfAny(anyOf, StartIndex);
            if(index < 0)
                return index;
            return index - StartIndex;
        }
        internal int IndexOf(char value) {
            int index = Source.IndexOf(value, StartIndex);
            if(index < 0)
                return index;
            return index - StartIndex;
        }

        internal string Substring(int startIndex, int length) {
            return Source.Substring(StartIndex + startIndex, length);
        }
        internal SourceString Substring(int startIndex) {
            return new SourceString(Source, StartIndex + startIndex);
        }

        internal bool StartsWith(string value, StringComparison comparisonType) {
            return string.Compare(Source, StartIndex, value, 0, value.Length, comparisonType) == 0;
        }
    }
    public class BaseParser {
        bool caseSensitive;
        char[] SpaceChars = { ' ', '	' };
        public BaseParser() : this(true) { }
        public BaseParser(bool caseSensitive) {
            this.caseSensitive = caseSensitive;
        }
        protected char previousSymbol;
        protected string StringCut(ref SourceString text, int count) {
            if(count == 0)
                return string.Empty;
            previousSymbol = text[count - 1];
            string result = text.Substring(0, count);
            text = text.Substring(count);
            return result;
        }
        protected void TrySpace(List<CodeLexeme> res, ref SourceString text) {
            StringBuilder spaces = new StringBuilder();
            while(SpaceChars.Contains(text[0]))
                spaces.Append(StringCut(ref text, 1));
            if(spaces.Length > 0)
                res.Add(new CodeLexeme(LexemeType.Space, spaces.ToString()));
        }
        protected bool TryExtract(List<CodeLexeme> res, ref SourceString text, string lex, LexemeType type) {
            if(text.StartsWith(lex, StringComparison.Ordinal)) {
                res.Add(new CodeLexeme(type, StringCut(ref text, lex.Length)));
                return true;
            }
            return false;
        }
        protected void TryExtractTo(List<CodeLexeme> res, ref SourceString text, string lex, LexemeType type, string except) {
            int index = text.IndexOf(lex);
            if(except != null)
                while(index >= 0 && text.Substring(0, index + 1).EndsWith(except, StringComparison.Ordinal))
                    index = text.IndexOf(lex, index + 1);
            if(index < 0) return;
            BreakLines(res, ref text, index + lex.Length, type);
        }

        protected void BreakLines(List<CodeLexeme> res, ref SourceString text, int to, LexemeType type) {
            while(text.Length > 0 && to > 0) {
                int index = text.IndexOf("\n");
                if(index >= to) {
                    res.Add(new CodeLexeme(type, StringCut(ref text, to)));
                    break;
                }
                if(index != 0) res.Add(new CodeLexeme(type, StringCut(ref text, index)));
                res.Add(new CodeLexeme(LexemeType.LineBreak, StringCut(ref text, 1)));
                to -= index + 1;
            }
        }
        public List<CodeLexeme> Parse(string text) {
            return Parse(new SourceString(text + "\n", 0));
        }
        protected virtual List<CodeLexeme> Parse(SourceString text) {
            List<CodeLexeme> res = new List<CodeLexeme>();
            SourceString extendedText = text;
            BreakLines(res, ref extendedText, extendedText.Length, LexemeType.PlainText);
            return res;
        }
    }
    public class CSParser : BaseParser {
        char[] CSEndOfTerm = { ' ', '\t', '\n', '=', '/', '>', '<', '"', '{', '}', ',', '(', ')', ';', '\0', '?' };
        string[] CSKeyWords = { "abstract","event","new","struct","as","explicit","null",
								"switch","base","extern","object","this","bool","false",
								"operator","throw","break","finally","out","true","byte",
								"fixed","override","try","case","float","params","typeof",
								"catch","for","private","uint","char","foreach","protected",
								"ulong","checked","goto","public","unchecked","class",
								"if","readonly","unsafe","const","implicit","ref","ushort",
								"continue","in","return","using","decimal","int","sbyte",
								"virtual","default","interface","sealed","volatile","delegate",
								"internal","short","void","do","is","sizeof","while",
								"double","lock","stackalloc","else","long","static","enum",
								"namespace","string","from","get","group","into","join","let",
								"orderby","partial","select","set","var","where","yield", "async", "await",
							    "#region","#endregion","#if","#endif"};
        public CSParser() { }
        protected override List<CodeLexeme> Parse(SourceString text) {
            SourceString extendedText = text;
            List<CodeLexeme> res = new List<CodeLexeme>();
            while(extendedText.Length > 0) {
                if(TryExtract(res, ref extendedText, "/*", LexemeType.Comment)) {
                    TryExtractTo(res, ref extendedText, "*/", LexemeType.Comment, null);
                }
                if(TryExtract(res, ref extendedText, "//", LexemeType.Comment)) {
                    TryExtractTo(res, ref extendedText, "\n", LexemeType.Comment, null);
                }
                if(TryExtract(res, ref extendedText, "\"", LexemeType.String)) {
                    TryExtractTo(res, ref extendedText, "\"", LexemeType.String, "\\\"");
                }
                if(TryExtract(res, ref extendedText, "'", LexemeType.String)) {
                    TryExtractTo(res, ref extendedText, "'", LexemeType.String, null);
                }
                ParseCSKeyWord(res, ref extendedText, LexemeType.KeyWord);
                ParseCSSymbol(res, ref extendedText, LexemeType.PlainText);
                TrySpace(res, ref extendedText);
                TryExtract(res, ref extendedText, "\n", LexemeType.LineBreak);
            }
            return res;
        }
        int lastLength = -1;
        void ParseCSSymbol(List<CodeLexeme> res, ref SourceString text, LexemeType lt) {
            if(lastLength == -1 || lastLength != text.Length) {
                lastLength = text.Length;
                return;
            }
            CodeLexeme cl = res.Count > 0 ? res.Last() : null;
            if(cl != null && cl.Type == LexemeType.PlainText)
                cl.Text += StringCut(ref text, 1);
            else
                res.Add(new CodeLexeme(LexemeType.PlainText, StringCut(ref text, 1)));
        }
        void ParseCSKeyWord(List<CodeLexeme> res, ref SourceString text, LexemeType type) {
            int index = -1;
            if(!CSEndOfTerm.Contains(previousSymbol)) return;
            foreach(string str in CSKeyWords) {
                if(text.StartsWith(str, StringComparison.Ordinal)) {
                    if(!CSEndOfTerm.Contains(text[str.Length])) continue;
                    index = str.Length;
                    break;
                }
            }
            if(index < 0) return;
            res.Add(new CodeLexeme(type, StringCut(ref text, index)));
        }
    }
    public class XamlParser : BaseParser {
        char[] XamlEndOfTerm = { ' ', '\t', '\n', '=', '/', '>', '<', '"', '{', '}', ',' };
        char[] XamlSymbol = { '=', '/', '>', '"', '{', '}', ',' };
        char[] XamlNamespaceDelimeter = { ':' };
        public XamlParser() { }
        protected bool IsInsideBlock = false;
        protected override List<CodeLexeme> Parse(SourceString text) {
            SourceString extendedText = text;
            List<CodeLexeme> res = new List<CodeLexeme>();
            while(extendedText.Length > 0) {
                if(TryExtract(res, ref extendedText, "<!--", LexemeType.Comment)) {
                    TryExtractTo(res, ref extendedText, "-->", LexemeType.Comment, null);
                }
                if(extendedText.StartsWith("<", StringComparison.Ordinal)) IsInsideBlock = false;
                if(TryExtract(res, ref extendedText, "\"{}", LexemeType.Value))
                    TryExtractTo(res, ref extendedText, "\"", LexemeType.Value, null);
                if(TryExtract(res, ref extendedText, "</", LexemeType.Symbol) ||
                   TryExtract(res, ref extendedText, "<", LexemeType.Symbol) ||
                   TryExtract(res, ref extendedText, "{", LexemeType.Symbol) ||
                   TryExtract(res, ref extendedText, "\"{", LexemeType.Symbol)) {
                    ParseXamlKeyWord(res, ref extendedText, LexemeType.Object);
                }
                if(TryExtract(res, ref extendedText, "\"", LexemeType.Value)) {
                    TryExtractTo(res, ref extendedText, "\"", LexemeType.Value, null);
                }
                ParseXamlKeyWord(res, ref extendedText, IsInsideBlock ? LexemeType.Object : LexemeType.Property);
                TryExtract(res, ref extendedText, "}\"", LexemeType.Symbol);
                if(extendedText.StartsWith(">", StringComparison.Ordinal)) IsInsideBlock = true;
                ParseSymbol(res, ref extendedText, LexemeType.Symbol);
                TrySpace(res, ref extendedText);
                TryExtract(res, ref extendedText, "\n", LexemeType.LineBreak);
            }
            return res;
        }
        void ParseSymbol(List<CodeLexeme> res, ref SourceString text, LexemeType lt) {
            int index = text.IndexOfAny(XamlSymbol);
            if(index != 0) return;
            res.Add(new CodeLexeme(LexemeType.Symbol, text.Substring(0, 1)));
            text = text.Substring(1);
        }
        void ParseXamlKeyWord(List<CodeLexeme> res, ref SourceString text, LexemeType type) {
            int index = text.IndexOfAny(XamlEndOfTerm);
            if(index <= 0) return;
            int nsIndex = text.IndexOf(':');
            if(nsIndex > 0 && nsIndex < index) {
                res.Add(new CodeLexeme(type, StringCut(ref text, nsIndex)));
                res.Add(new CodeLexeme(LexemeType.Symbol, StringCut(ref text, 1)));
                res.Add(new CodeLexeme(type, StringCut(ref text, index - nsIndex - 1)));
            } else {
                res.Add(new CodeLexeme(type, StringCut(ref text, index)));
            }
        }
    }
    public class VBParser : BaseParser {
        char[] VBEndOfTerm = { ' ', '\t', '\n', '=', '/', '>', '<', '"', '{', '}', ',', '(', ')', ';', ':', '\0', '?' };
        string[] VBKeyWords = { "attribute","addhandler","andalso","byte","catch","cdate","cint","const",
								"csgn","culgn","declare","directcast","else","enum","exit",
								"friend","getxmlnamespace","handles","in","is","like","mod",
								"mybase","new","noinheritable","on","or","overrides","property",
								"readonly","resume","set","single","string","then","try",
								"ulong","wend","with","addressof","as","byval",
								"cbool","cdbl","class","continue","cstr","cushort","default",
								"do","elseif","erase","false","function","global","if",
								"inherits","isnot","long","module","myclass","next",
								"notoverridable","operator","orelse","paramarray","protected","redim","return",
								"shadows","static","structure","throw","trycast","ushort","when",
								"withevents","alias","boolean","call","cbyte","cdec","clng","csbyte",
								"ctype","date","delegate","double","end","error","finally","get",
							    "gosub","implements","integer","let","loop","mustinherit","namespace",
								"not","object","option","overloads","partial","public","rem",
								"sbyte","shared","step","sub","to","typeof","using","while", "async", "await",
								"writeonly","and","byref","case","cchar","char","cobj","cshort",
							    "cuint","decimal","dim","each","endif","event","for","gettype","goto","imports",
                                "interface","lib","me","mustoverride","narrowing","nothing","of","optional",
                                "overridable","private","raiseevent","removehandler","select","short","stop",
                                "synclock","true","uinteger","variant","widening","xor" };
        public VBParser() : base(false) { }
        protected override List<CodeLexeme> Parse(SourceString text) {
            SourceString extendedText = text;
            List<CodeLexeme> res = new List<CodeLexeme>();
            while(extendedText.Length > 0) {
                if(TryExtract(res, ref extendedText, "'", LexemeType.Comment)) {
                    TryExtractTo(res, ref extendedText, "\n", LexemeType.Comment, null);
                }
                if(TryExtract(res, ref extendedText, "`", LexemeType.Comment)) {
                    TryExtractTo(res, ref extendedText, "\n", LexemeType.Comment, null);
                }
                TryExtract(res, ref extendedText, "rem\n", LexemeType.Comment);
                if(TryExtract(res, ref extendedText, "rem ", LexemeType.Comment)) {
                    TryExtractTo(res, ref extendedText, "\n", LexemeType.Comment, null);
                }
                if(TryExtract(res, ref extendedText, "rem\t", LexemeType.Comment)) {
                    TryExtractTo(res, ref extendedText, "\n", LexemeType.Comment, null);
                }
                if(TryExtract(res, ref extendedText, "\"", LexemeType.String)) {
                    TryExtractTo(res, ref extendedText, "\"", LexemeType.String, "\"\"");
                    TryExtract(res, ref extendedText, "c", LexemeType.String);
                }
                ParseVBKeyWord(res, ref extendedText, LexemeType.KeyWord);
                ParseVBSymbol(res, ref extendedText, LexemeType.PlainText);
                TrySpace(res, ref extendedText);
                TryExtract(res, ref extendedText, "\n", LexemeType.LineBreak);
            }
            return res;
        }
        int lastLength = -1;
        void ParseVBSymbol(List<CodeLexeme> res, ref SourceString text, LexemeType lt) {
            if(lastLength == -1 || lastLength != text.Length) {
                lastLength = text.Length;
                return;
            }
            CodeLexeme cl = res.Count > 0 ? res.Last() : null;
            if(cl != null && cl.Type == LexemeType.PlainText)
                cl.Text += StringCut(ref text, 1);
            else
                res.Add(new CodeLexeme(LexemeType.PlainText, StringCut(ref text, 1)));
        }
        void ParseVBKeyWord(List<CodeLexeme> res, ref SourceString text, LexemeType type) {
            int index = -1;
            if(!VBEndOfTerm.Contains(previousSymbol)) return;
            foreach(string str in VBKeyWords) {
                if(text.StartsWith(str, StringComparison.OrdinalIgnoreCase)) {
                    if(!VBEndOfTerm.Contains(text[str.Length])) continue;
                    index = str.Length;
                    break;
                }
            }
            if(index < 0) return;
            res.Add(new CodeLexeme(type, StringCut(ref text, index)));
        }
    }
}
