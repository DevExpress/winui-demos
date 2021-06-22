using System;
using System.Collections.Generic;
using System.Windows.Input;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;

namespace ControlsDemo {
    public class SimplePadViewModel : DevExpress.Mvvm.BindableBase {
        double fontSize = 14;
        List<double> fontSizes;
        FontFamily font;
        List<FontFamily> fonts;
        TextAlignment textAlignment;

        public TextAlignment TextAlignment {
            get { return textAlignment; }
            set { SetProperty(ref textAlignment, value, nameof(TextAlignment)); }
        }
        public double FontSize {
            get { return fontSize; }
            set { SetProperty(ref fontSize, value, nameof(FontSize)); }
        }
        public List<double> FontSizes { get { return fontSizes ?? (fontSizes = CreateFontSizes()); } }
        public FontFamily Font {
            get { return font; }
            set { SetProperty(ref font, value, nameof(Font)); }
        }
        public List<FontFamily> Fonts { get { return fonts ?? (fonts = CreateFonts()); } }
        public ICommand CutCommand { get; private set; }
        public ICommand CopyCommand { get; private set; }
        public ICommand PasteCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public ICommand SelectAllCommand { get; private set; }
        public ICommand TextAlignmentCommand { get; private set; }

        public SimplePadViewModel() {
            CutCommand = new DevExpress.Mvvm.DelegateCommand<TextBox>(Cut, CanCut);
            CopyCommand = new DevExpress.Mvvm.DelegateCommand<TextBox>(Copy, CanCopy);
            PasteCommand = new DevExpress.Mvvm.DelegateCommand<TextBox>(Paste, CanPaste);
            DeleteCommand = new DevExpress.Mvvm.DelegateCommand<TextBox>(Delete, CanDelete);
            SelectAllCommand = new DevExpress.Mvvm.DelegateCommand<TextBox>(SelectAll, CanSelectAll);
            TextAlignmentCommand = new DevExpress.Mvvm.DelegateCommand<TextAlignment>(SetTextAlignment);
        }
        void Cut(TextBox tb) {
            
            
            
            
            
        }
        bool CanCut(TextBox tb) {
            return !string.IsNullOrEmpty(tb.SelectedText);
        }
        void Copy(TextBox tb) {
            if (string.IsNullOrEmpty(tb.Text))
                return;
            
            
            
            
        }
        bool CanCopy(TextBox tb) {
            return true;
        }
        void Paste(TextBox tb) {
            try {
                
                
                
                
                
            }
            catch (Exception) {
            }
        }
        bool CanPaste(TextBox tb) {
            return true;
        }
        void Delete(TextBox tb) {
            tb.Text = string.Empty;
        }
        bool CanDelete(TextBox tb) {
            return true;
        }
        void SelectAll(TextBox tb) {
            tb.SelectAll();
        }
        bool CanSelectAll(TextBox tb) {
            return true;
        }
        void SetTextAlignment(TextAlignment ta) {
            TextAlignment = ta;
        }
        List<FontFamily> CreateFonts() {
            List<FontFamily> list = new List<FontFamily>();
            list.Add(new FontFamily("Cambria"));
            list.Add(new FontFamily("Colibri"));
            list.Add(new FontFamily("Angsana New"));
            list.Add(new FontFamily("Verdana"));
            return list;
        }
        List<double> CreateFontSizes() {
            var list = new List<double>();
            for (double i = 6; i < 72.5; i += 2d)
                list.Add(i);
            return list;
        }
    }
}
