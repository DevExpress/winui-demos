using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FeatureDemo.ViewModel;
using Microsoft.UI.Xaml.Documents;

namespace FeatureDemo.Common.Internal {
    public class TextRange {
        public TextRange(TextPointer start, TextPointer end) {
            Start = start;
            End = end;
        }
        public TextPointer Start { get; private set; }
        public TextPointer End { get; private set; }
    }
    public static class RichTextExtensions {
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static int CompareTo(this TextPointer t, TextPointer position) {
            return t.Offset - position.Offset;
        }
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static int GetOffsetToPosition(this TextPointer t, TextPointer position) {
            return position.Offset - t.Offset;
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
        }
    }
    public interface IRichTextPresenter {
        ICollection<Block> Blocks { get; }
        TextPointer ContentStart { get; }
        TextPointer ContentEnd { get; }
        void Select(TextPointer start, TextPointer end);
    }
    public static class RichTextHelper {
        public static void SetText(IRichTextPresenter rtb, string text, CodeLanguage codeLanguage) {
            if(rtb == null) return;
            rtb.Blocks.Clear();
            if(string.IsNullOrEmpty(text)) return;
            Paragraph paragraph = new Paragraph();

            CodeBlockPresenter presenter = new CodeBlockPresenter(codeLanguage);
            presenter.FillInlines(text, paragraph.Inlines);
            int index = 0, length = 0, currentIndex = 0;
            do {
                currentIndex = text.IndexOf('\r', index);
                length = Math.Max(length, currentIndex - index);
                index = currentIndex + 1;
            }
            while(currentIndex > -1);
            rtb.Blocks.Add(paragraph);
            rtb.Select(rtb.ContentStart, rtb.ContentStart);
        }
        public static string GetText(IRichTextPresenter rtb, TextRange range = null) {
            StringBuilder text = new StringBuilder();
            foreach(Run run in GetRuns(rtb, range)) {
                int start;
                int end;
                GetRunOffsets(range, run, out start, out end);
                text.Append(run.Text.Substring(start, end - start));
            }
            return text.ToString();
        }
        public static TextRange FindText(IRichTextPresenter rtb, string text) {
            return FindText(rtb, text, null, StringComparison.OrdinalIgnoreCase);
        }
        public static TextRange FindText(IRichTextPresenter rtb, string text, TextRange range) {
            return FindText(rtb, text, range, StringComparison.OrdinalIgnoreCase);
        }
        public static TextRange FindText(IRichTextPresenter rtb, string text, StringComparison comparisonType) {
            return FindText(rtb, text, null, comparisonType);
        }
        public static TextRange FindText(IRichTextPresenter rtb, string text, TextRange range, StringComparison comparisonType) {
            if(string.IsNullOrEmpty(text)) return null;
            StringBuilder s = new StringBuilder(text);
            TextPointer textStart = null;
            LinkedListNode<Run> run = GetRuns(rtb, range).First;
            LinkedListNode<Run> savedRun = null;
            while(run != null) {
                int runTextStart;
                int runTextEnd;
                GetRunOffsets(range, run.Value, out runTextStart, out runTextEnd);
                int start;
                int end;
                if(FindSubstring(run.Value.Text, s, out start, out end, runTextStart, runTextEnd, comparisonType, textStart != null)) {
                    if(textStart == null) {
                        savedRun = run;
                        textStart = run.Value.ContentStart.GetPositionAtOffset(start, LogicalDirection.Forward);
                    }
                    if(s.Length == 0) {
                        return new TextRange(textStart, run.Value.ContentStart.GetPositionAtOffset(end, LogicalDirection.Backward));
                    }
                } else {
                    if(textStart != null) {
                        run = savedRun;
                        s = new StringBuilder(text);
                        textStart = null;
                    }
                }
                run = run.Next;
            }
            return null;
        }
        internal static void GetRunOffsets(TextRange range, Run run, out int runTextStart, out int runTextEnd) {
            runTextStart = 0;
            if(range != null && range.Start != null) {
                runTextStart = run.ContentStart.GetOffsetToPosition(range.Start);
                if(runTextStart < 0)
                    runTextStart = 0;
            }
            int runLength = run.ContentStart.GetOffsetToPosition(run.ContentEnd);
            runTextEnd = runLength;
            if(range != null && range.End != null) {
                runTextEnd = range.End.GetOffsetToPosition(run.ContentEnd);
                if(runTextEnd < 0)
                    runTextEnd = 0;
                runTextEnd = runLength - runTextEnd;
            }
        }
        internal static LinkedList<Run> GetRuns(IRichTextPresenter rtb, TextRange range) {
            LinkedList<Run> list = new LinkedList<Run>();
            foreach(Block block in rtb.Blocks) {
                if(range != null) {
                    if(range.Start != null && block.ElementEnd.CompareTo(range.Start) < 0) continue;
                    if(range.End != null && block.ElementStart.CompareTo(range.End) > 0) break;
                }
                Paragraph paragraph = block as Paragraph;
                if(paragraph == null) continue;
                foreach(Inline inline in paragraph.Inlines) {
                    if(range != null) {
                        if(range.Start != null && inline.ElementEnd.CompareTo(range.Start) < 0) continue;
                        if(range.End != null && inline.ElementStart.CompareTo(range.End) > 0) break;
                    }
                    Run run = inline as Run;
                    if(run == null) continue;
                    list.AddLast(run);
                }
            }
            return list;
        }
        public static bool FindSubstring(string text, StringBuilder substring, out int start, out int end, int textStart, int textEnd, StringComparison comparisonType, bool startsOnly) { 
            for(int ti = textStart; ti < (startsOnly ? textStart + 1 : textEnd); ++ti) {
                if(!CharEquals(text[ti], substring[0], comparisonType)) continue;
                int ti2 = ti;
                int si = 0;
                while(true) {
                    ++ti2;
                    ++si;
                    if(si == substring.Length || ti2 == text.Length) {
                        start = ti;
                        end = ti2;
                        substring.Remove(0, si);
                        return true;
                    }
                    if(ti2 >= textEnd || !CharEquals(text[ti2], substring[si], comparisonType)) break;
                }
            }
            start = -1;
            end = -1;
            return false;
        }
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        internal static bool CharEquals(char c1, char c2, StringComparison comparisonType) {
            switch(comparisonType) {
                case StringComparison.CurrentCultureIgnoreCase: return char.ToLower(c1) == char.ToLower(c2);
                case StringComparison.OrdinalIgnoreCase: return char.ToLowerInvariant(c1) == char.ToLowerInvariant(c2);
                default: return c1 == c2;
            }
        }
    }
}
