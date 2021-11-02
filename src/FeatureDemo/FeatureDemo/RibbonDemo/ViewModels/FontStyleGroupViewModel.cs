using System.Collections.Generic;
using System.Windows.Input;
using DevExpress.Mvvm;
using Microsoft.UI;
using Microsoft.UI.Xaml.Media;
using DevExpress.Mvvm.Native;
using DevExpress.WinUI.Ribbon;
using System.Collections.ObjectModel;
using System;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml;
using DevExpress.WinUI.Editors;
using Color = Windows.UI.Color;
using DevExpress.Mvvm.CodeGenerators;

namespace RibbonDemo {
    [GenerateViewModel]
    public partial class FontStyleGroupViewModel : ViewModelBase {
        public FontStyleGroupViewModel(RibbonToolBarViewModel parent) {
            ((ISupportParentViewModel)this).ParentViewModel = parent;
            MainColorsList = new ColorPalette();
            MainColorsList.Title = "Standard colors";
            Colors = new ColorGroups();
            Colors.Add(MainColorsList);
            AddColorLine(MainColorsList, 0, 0, 0, 11, 2, 1);
            for (int x = 0; x < 1280; x += 128) {
                byte r = 0;
                byte g = 0;
                byte b = 0;
                if (x < 256) {
                    r = 255;
                    g = (byte)x;
                }
                if (x >= 256 && x < 512) {
                    g = 255;
                    r = (byte)(511 - x);
                }
                if (x >= 512 && x < 768) {
                    g = 255;
                    b = (byte)(x - 512);
                }
                if (x >= 768 && x < 1024) {
                    b = 255;
                    g = (byte)(1023 - x);
                }
                if (x >= 1024 && x < 1280) {
                    b = 255;
                    r = (byte)(x - 1024);
                }
                AddColorLine(MainColorsList, r, g, b, 9, -9, 2);
            }
            Foreground = MainColorsList.Items[15];
            Background = MainColorsList.Items[1];
        }

        [GenerateProperty]
        SolidColorBrush _Background;

        [GenerateProperty]
        SolidColorBrush _Foreground;

        [GenerateProperty]
        FontFamily _FontFamily;

        [GenerateProperty]
        ColorGroups _Colors;

        [GenerateProperty]
        ColorPalette _DocumentColors;

        protected ColorPalette MainColorsList { get; set; }
        protected IRichEditorFontService RichEditorFontService => Parent.Service as IRichEditorFontService;
        RibbonToolBarViewModel Parent => ((ISupportParentViewModel)this).ParentViewModel as RibbonToolBarViewModel;

        protected override void OnParentViewModelChanged(object parentViewModel) {
            base.OnParentViewModelChanged(parentViewModel);
            (parentViewModel as RibbonToolBarViewModel).Do(x => { DocumentColors = x.DocumentColors; });
            if (DocumentColors != null)
                Colors.Insert(0, DocumentColors);
        }

        protected virtual void OnForegroundChanged() {
            if (Foreground != null)
                RichEditorFontService.Do(x => x.SetForeground(Foreground));
            else
                Foreground = MainColorsList.Items[0];
        }

        void OnBackgroundChanged() {
            if (Background != null)
                RichEditorFontService.Do(x => x.SetBackground(Background));
            else
                Background = MainColorsList.Items[1];
        }

        [GenerateCommand]
        void IncreaseFontSize() => RichEditorFontService?.IncreaseFontFize();
        [GenerateCommand]
        void DecreaseFontSize() => RichEditorFontService?.DecreaseFontSize();
        [GenerateCommand]
        void SetBackgroundColor() => RichEditorFontService?.SetBackground(Background);
        [GenerateCommand]
        void SetForegroundColor() => RichEditorFontService?.SetForeground(Foreground);

        void AddColorLine(ColorPalette ColorsList, byte r, byte g, byte b, int hi, int lo, int step) {
            ColorsList.Items.Add(new SolidColorBrush(Color.FromArgb(255, r, g, b)));
            for (int n = hi; n > lo; n -= step) {
                int colorDiff = 24 * n;
                byte r1 = (byte)(r + colorDiff > 255 ? 255 : (r + colorDiff < 0 ? 0 : r + colorDiff));
                byte g1 = (byte)(g + colorDiff > 255 ? 255 : (g + colorDiff < 0 ? 0 : g + colorDiff));
                byte b1 = (byte)(b + colorDiff > 255 ? 255 : (b + colorDiff < 0 ? 0 : b + colorDiff));
                ColorsList.Items.Add(new SolidColorBrush(Color.FromArgb(255, r1, g1, b1)));
            }
        }
    }
    public class ColorSet : ObservableCollection<SolidColorBrush> { }

    [GenerateViewModel]
    public partial class ColorPalette {
        public ColorPalette() {
            Items = new ColorSet();
        }
        [GenerateProperty]
        string _Title;
        public ColorSet Items { get; }
    }

    public class ColorGroups : ObservableCollection<ColorPalette> { }
}