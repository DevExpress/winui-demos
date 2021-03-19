using Windows.Storage.Streams;
using Windows.UI.Text;
using Microsoft.UI.Xaml.Media;
using MarkerType = Microsoft.UI.Text.MarkerType;

namespace RibbonDemo {
    public interface IRichEditorContentService {
        void SetRawContent(string content);
        void OpenFileFromStream(IRandomAccessStream rtfFileStream);
        void SaveFileToStream(IRandomAccessStream rtfFileStream);
        string FileName { get; set; }
        string FilePath { get; set; }
    }
    public interface IRichEditorInsertService {
        void InsertImage(FileRandomAccessStream imageFileStream);
    }
    public interface IRichEditorFontService {
        void IncreaseFontFize();
        void DecreaseFontSize();
        void SetForeground(SolidColorBrush foreground);
        void SetBackground(SolidColorBrush background);
        void SetDocumentColorsContainer(ColorPalette colorPalette);
    }
    public interface IRichEditorParagraphService {
        void SetListType(MarkerType type);
        void IncreaseParagraphIndent();
        void DecreaseParagraphIndent();
    }
    public interface IRichEditorCommonActionsService {
        void Copy();
        bool CanCopy();
        void Paste();
        bool CanPaste();
        void Cut();
        bool CanCut();
        void Undo();
        bool CanUndo();
        void Redo();
        bool CanRedo();
    }
}
