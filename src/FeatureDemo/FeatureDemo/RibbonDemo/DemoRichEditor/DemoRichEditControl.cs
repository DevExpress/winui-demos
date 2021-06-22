using DevExpress.Mvvm.Native;
using DevExpress.WinUI.Core.Internal;
using DevExpress.WinUI.Ribbon;
using DevExpress.WinUI.Ribbon.Internal;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Color = Windows.UI.Color;
using FormatEffect = Microsoft.UI.Text.FormatEffect;
using ITextSelection = Microsoft.UI.Text.ITextSelection;
using MarkerType = Microsoft.UI.Text.MarkerType;
using ParagraphAlignment = Microsoft.UI.Text.ParagraphAlignment;
using TextGetOptions = Microsoft.UI.Text.TextGetOptions;
using TextSetOptions = Microsoft.UI.Text.TextSetOptions;
using UnderlineType = Microsoft.UI.Text.UnderlineType;

namespace RibbonDemo {
    public class DemoScrollArea : ContentControl {
        double? zoomFactor = null;
        public double? ZoomFactor {
            get {
                return zoomFactor;
            }
            set {
                if (zoomFactor != value) {
                    zoomFactor = value;
                    OnZoomFactorChanged();
                }
            }
        }

        ScrollViewer scroll;
        bool zoomLocker;

        public DemoScrollArea() {
            DefaultStyleKey = typeof(DemoScrollArea);
            zoomLocker = false;
        }

        protected override void OnApplyTemplate() {
            scroll = LayoutHelper.FindElementByName<ScrollViewer>(this, "PART_Scroll");
            scroll.Loaded += Scroll_Loaded;
        }
        
        void OnZoomFactorChanged() {
            if (!zoomLocker)
                scroll.Do(x => x.ChangeView(0, 0, (float?)ZoomFactor));
        }
        void Scroll_Loaded(object sender, RoutedEventArgs e) {
            OnZoomFactorChanged();
            scroll.ViewChanged += Scroll_ViewChanged;
        }
        void Scroll_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e) {
            zoomLocker = true;
            ZoomFactor = scroll.ZoomFactor;
            zoomLocker = false;
        }
    }

    public class DemoRichEditBox : RichEditBox {
        public static readonly DependencyProperty RTFTextProperty = DependencyProperty.Register(nameof(RTFText), typeof(string), typeof(DemoRichEditBox), new PropertyMetadata(null, (d, e) => ((DemoRichEditBox)d).OnRtfTextChanged()));
        public static readonly DependencyProperty InitialTextProperty = DependencyProperty.Register(nameof(FileName), typeof(string), typeof(DemoRichEditBox), new PropertyMetadata(string.Empty));
        public static readonly DependencyProperty FileNameProperty = DependencyProperty.Register(nameof(InitialText), typeof(string), typeof(DemoRichEditBox), new PropertyMetadata(null));

        public DemoRichEditBox() {
            FileName = string.Empty;
            FilePath = string.Empty;
            DocumentLoaded = false;
            textLocked = false;
            selectionLocked = false;
        }
       
        public void UpdateSelection() {
            if (!selectionLocked) {
                selectionLocked = true;
                Document.Selection.SetRange(Document.Selection.EndPosition, Document.Selection.EndPosition);
                selectionLocked = false;
            }
        }
        public string RTFText {
            get { return (string)GetValue(RTFTextProperty); }
            set { SetValue(RTFTextProperty, value); }
        }
                public string FileName {
            get { return (string)GetValue(FileNameProperty); }
            set { SetValue(FileNameProperty, value); }
        }
        public string InitialText { get; private set; }
        protected bool DocumentLoaded;
        public string FilePath { get; set; }
        public bool RTFContentIsUpdated { get; set; }
        protected override void OnGotFocus(RoutedEventArgs e) { }
        protected override void OnLostFocus(RoutedEventArgs e) { }
        protected override void OnPointerExited(PointerRoutedEventArgs e) { }
        protected override void OnPointerEntered(PointerRoutedEventArgs e) { }
        protected override void OnApplyTemplate() {
            base.OnApplyTemplate();
            SelectionChanged -= OnSelectionChanged;
            SelectionChanged += OnSelectionChanged;
        }

        protected bool selectionLocked, textLocked;

        protected virtual void OnSelectionChanged(object sender, RoutedEventArgs e) {
            if (!selectionLocked) {
                textLocked = true;
                string currentText;
                Document.GetText(TextGetOptions.FormatRtf, out currentText);
                RTFText = currentText;
                if (Document.UndoLimit == 0) {
                    InitialText = RTFText;
                    Document.UndoLimit = 32;
                }
                textLocked = false;
            }
        }
        protected virtual void OnRtfTextChanged() {
           if (!textLocked && RTFText != null) {
                selectionLocked = true;
                Document.SetText(TextSetOptions.FormatRtf, RTFText);
                
                
                InitialText = RTFText;
                Document.UndoLimit = 0;
                Task.Delay(10);
                selectionLocked = false;
            }
        }
        protected int GetDocumentLength() {
            string currentContent;
            Document.GetText(TextGetOptions.None, out currentContent);
            return currentContent.Length;
        }
    }
    public class DemoRichEditBoxExtended : DemoRichEditBox {
        public static readonly DependencyProperty CurrentSelectionFontSizeProperty = DependencyProperty.Register(nameof(CurrentSelectionFontSize), typeof(double), typeof(DemoRichEditBoxExtended), new PropertyMetadata(12d, (d, e) => ((DemoRichEditBoxExtended)d).OnCurrentSelectionFontSizeChanged()));

        public static readonly DependencyProperty CurrentSelectionFontFamilyProperty = DependencyProperty.Register(nameof(CurrentSelectionFontFamily), typeof(string), typeof(DemoRichEditBoxExtended), new PropertyMetadata(null, (d, e) => ((DemoRichEditBoxExtended)d).OnCurrentSelectionFontFamilyChanged()));
        public static readonly DependencyProperty CurrentSelectionAlignmentProperty = DependencyProperty.Register(nameof(CurrentSelectionAlignment), typeof(ParagraphAlignment), typeof(DemoRichEditBoxExtended), new PropertyMetadata(ParagraphAlignment.Left, (d, e) => ((DemoRichEditBoxExtended)d).OnCurrentSelectionAlignmentChanged()));
        public static readonly DependencyProperty CurrentSelectionBoldDecorationProperty = DependencyProperty.Register(nameof(CurrentSelectionBoldDecoration), typeof(bool), typeof(DemoRichEditBoxExtended), new PropertyMetadata(false, (d, e) => ((DemoRichEditBoxExtended)d).OnCurrentSelectionBoldDecorationChanged()));
        public static readonly DependencyProperty CurrentSelectionItalicDecorationProperty = DependencyProperty.Register(nameof(CurrentSelectionItalicDecoration), typeof(bool), typeof(DemoRichEditBoxExtended), new PropertyMetadata(false, (d, e) => ((DemoRichEditBoxExtended)d).OnCurrentSelectionItalicDecorationChanged()));
        public static readonly DependencyProperty CurrentSelectionUnderlineDecorationProperty = DependencyProperty.Register(nameof(CurrentSelectionUnderlineDecoration), typeof(bool), typeof(DemoRichEditBoxExtended), new PropertyMetadata(false, (d, e) => ((DemoRichEditBoxExtended)d).OnCurrentSelectionUnderlineDecorationChanged()));
        public static readonly DependencyProperty CurrentSelectionBulletsListProperty = DependencyProperty.Register(nameof(CurrentSelectionBulletsList), typeof(bool), typeof(DemoRichEditBoxExtended), new PropertyMetadata(false, (d, e) => ((DemoRichEditBoxExtended)d).OnBulletsListChanged()));
        public static readonly DependencyProperty CurrentSelectionListLevelProperty = DependencyProperty.Register(nameof(CurrentSelectionListLevel), typeof(int), typeof(DemoRichEditBoxExtended), new PropertyMetadata(1, (d, e) => ((DemoRichEditBoxExtended)d).OnCurrentSelectionListLevelChanged()));
        public static readonly DependencyProperty CurrentSelectionSubscriptProperty = DependencyProperty.Register(nameof(CurrentSelectionSubscript), typeof(bool), typeof(DemoRichEditBoxExtended), new PropertyMetadata(false, (d, e) => ((DemoRichEditBoxExtended)d).OnCurrentSelectionSubscriptChanged()));
        public static readonly DependencyProperty CurrentSelectionSuperscriptProperty = DependencyProperty.Register(nameof(CurrentSelectionSuperscript), typeof(bool), typeof(DemoRichEditBoxExtended), new PropertyMetadata(false, (d, e) => ((DemoRichEditBoxExtended)d).OnCurrentSelectionSuperscriptChanged()));
        public static readonly DependencyProperty IsImageSelectedProperty = DependencyProperty.Register(nameof(IsImageSelected), typeof(bool), typeof(DemoRichEditBoxExtended), new PropertyMetadata(false, (d, e) => ((DemoRichEditBoxExtended)d).OnIsImageSelectedChanged()));
        public static readonly DependencyProperty CurrentForegroundProperty = DependencyProperty.Register(nameof(CurrentForeground), typeof(Color), typeof(DemoRichEditBoxExtended), new PropertyMetadata(Colors.Black, (d, e) => ((DemoRichEditBoxExtended)d).OnCurrentForegroundChanged()));
        public static readonly DependencyProperty DocumentColorsProperty = DependencyProperty.Register(nameof(DocumentColors), typeof(ColorPalette), typeof(DemoRichEditBoxExtended), new PropertyMetadata(null, (d, e) => ((DemoRichEditBoxExtended)d).OnDocumentColorsChanged()));
        public static readonly DependencyProperty SelectedImageWidthProperty = DependencyProperty.Register(nameof(SelectedImageWidth), typeof(int), typeof(DemoRichEditBoxExtended), new PropertyMetadata(0, (d, e) => ((DemoRichEditBoxExtended)d).OnSelectedImageWidthChanged()));
        public static readonly DependencyProperty SelectedImageHeightProperty = DependencyProperty.Register(nameof(SelectedImageHeight), typeof(int), typeof(DemoRichEditBoxExtended), new PropertyMetadata(0, (d, e) => ((DemoRichEditBoxExtended)d).OnSelectedImageHeightChanged()));


        private void OnIsImageSelectedChanged() {
            if(IsImageSelected) {
                UpdateImageParams();
            }
        }
        public int SelectedImageHeight {
            get { return (int)GetValue(SelectedImageHeightProperty); }
            set { SetValue(SelectedImageHeightProperty, value); }
        }
        public int SelectedImageWidth {
            get { return (int)GetValue(SelectedImageWidthProperty); }
            set { SetValue(SelectedImageWidthProperty, value); }
        }
        public ColorPalette DocumentColors {
            get { return (ColorPalette)GetValue(DocumentColorsProperty); }
            set { SetValue(DocumentColorsProperty, value); }
        }

        public Color CurrentForeground {
            get { return (Color)GetValue(CurrentForegroundProperty); }
            set { SetValue(CurrentForegroundProperty, value); }
        }
                public bool IsImageSelected {
            get { return (bool)GetValue(IsImageSelectedProperty); }
            set { SetValue(IsImageSelectedProperty, value); }
        }
        public bool CurrentSelectionSuperscript {
            get { return (bool)GetValue(CurrentSelectionSuperscriptProperty); }
            set { SetValue(CurrentSelectionSuperscriptProperty, value); }
        }
        public bool CurrentSelectionSubscript {
            get { return (bool)GetValue(CurrentSelectionSubscriptProperty); }
            set { SetValue(CurrentSelectionSubscriptProperty, value); }
        }
        public int CurrentSelectionListLevel {
            get { return (int)GetValue(CurrentSelectionListLevelProperty); }
            set { SetValue(CurrentSelectionListLevelProperty, value); }
        }
        public bool CurrentSelectionBulletsList {
            get { return (bool)GetValue(CurrentSelectionBulletsListProperty); }
            set { SetValue(CurrentSelectionBulletsListProperty, value); }
        }
        public ParagraphAlignment CurrentSelectionAlignment {
            get { return (ParagraphAlignment)GetValue(CurrentSelectionAlignmentProperty); }
            set { SetValue(CurrentSelectionAlignmentProperty, value); }
        }
        public double CurrentSelectionFontSize {
            get { return (double)GetValue(CurrentSelectionFontSizeProperty); }
            set { SetValue(CurrentSelectionFontSizeProperty, value); }
        }
        public string CurrentSelectionFontFamily {
            get { return (string)GetValue(CurrentSelectionFontFamilyProperty); }
            set { SetValue(CurrentSelectionFontFamilyProperty, value); }
        }
        public bool CurrentSelectionBoldDecoration {
            get { return (bool)GetValue(CurrentSelectionBoldDecorationProperty); }
            set { SetValue(CurrentSelectionBoldDecorationProperty, value); }
        }
        public bool CurrentSelectionItalicDecoration {
            get { return (bool)GetValue(CurrentSelectionItalicDecorationProperty); }
            set { SetValue(CurrentSelectionItalicDecorationProperty, value); }
        }
        public bool CurrentSelectionUnderlineDecoration {
            get { return (bool)GetValue(CurrentSelectionUnderlineDecorationProperty); }
            set { SetValue(CurrentSelectionUnderlineDecorationProperty, value); }
        }

        public DemoRichEditBoxExtended() : base() {
            DefaultStyleKey = typeof(DemoRichEditBoxExtended);
            FileName = "Document";
            documentColorsMap = new Dictionary<Color, SolidColorBrush>();
            RTFContentIsUpdated = true;
            
        }

        ITextSelection currentSelection;
        
        bool isShiftPressed;
        Dictionary<Color, SolidColorBrush> documentColorsMap;
        
        void AddDocumentColor(SolidColorBrush brush) {
            if (!documentColorsMap.ContainsKey(brush.Color) && DocumentColors != null) {
                documentColorsMap.Add(brush.Color, brush);
                DocumentColors.Items.Add(brush);
            }
        }

        private void OnDocumentColorsChanged() {
            UpdateDocumentColors(true);
        }

        protected override void OnRtfTextChanged() {
            base.OnRtfTextChanged();
            UpdateDocumentColors(RTFContentIsUpdated);
            if (RTFContentIsUpdated) RTFContentIsUpdated = false;
        }
        protected override void OnLostFocus(RoutedEventArgs e) {
            base.OnLostFocus(e);
            var focusedElement = FocusManager.GetFocusedElement() as DependencyObject;
            if (LayoutHelper.FindAmongParents<RibbonControlBase>(focusedElement, null) != null) {
                if (focusedElement is Control &&
                   (focusedElement as Control).FocusState == FocusState.Pointer &&
                   !(focusedElement is TextBox || focusedElement is RibbonFlyoutTitleControl))
                    FocusRepair();
            }
        }
        async void FocusRepair() {
            await Task.Delay(100);
            Focus(FocusState.Programmatic);
        }
        void UpdateDocumentColors(bool clear) {
            if (DocumentColors != null && RTFText != null) {
                if (clear) {
                    DocumentColors.Items.Clear();
                    documentColorsMap.Clear();
                }
                Regex rgx = new Regex(@"\\red([0-9]{1,3})\\green([0-9]{1,3})\\blue([0-9]{1,3})");
                MatchCollection colorsMatches = rgx.Matches(RTFText);
                foreach (Match match in colorsMatches) {
                    if (match.Groups.Count == 4) {
                        Color color = Color.FromArgb(255, byte.Parse(match.Groups[1].Value), byte.Parse(match.Groups[2].Value), byte.Parse(match.Groups[3].Value));
                        AddDocumentColor(new SolidColorBrush(color));
                    }
                }
            }
        }
        protected override void OnApplyTemplate() {
            base.OnApplyTemplate();
            base.OnRtfTextChanged();
        }

        protected override void OnKeyUp(KeyRoutedEventArgs e) {
            if (e.Key == Windows.System.VirtualKey.Shift) 
                isShiftPressed = false;
            base.OnKeyUp(e);
        }
        protected override void OnKeyDown(KeyRoutedEventArgs e) {
            if (e.Key == Windows.System.VirtualKey.Shift) {
                isShiftPressed = true;
            }
            else if (currentSelection != null) {
                bool isArrowPressed = (e.Key == Windows.System.VirtualKey.Left || 
                                       e.Key == Windows.System.VirtualKey.Up || 
                                       e.Key == Windows.System.VirtualKey.Right || 
                                       e.Key == Windows.System.VirtualKey.Down);
                if (!(isShiftPressed && isArrowPressed)) {
                    currentSelection.CharacterFormat.ForegroundColor = CurrentForeground;
                    if (!double.IsNaN(CurrentSelectionFontSize)) {
                        if (currentSelection.CharacterFormat.Size != CurrentSelectionFontSize)
                            currentSelection.CharacterFormat.Size = (float)CurrentSelectionFontSize;
                    }
                    if (CurrentSelectionFontFamily != "" && CurrentSelectionFontFamily != null && currentSelection.CharacterFormat.Name != CurrentSelectionFontFamily)
                        currentSelection.CharacterFormat.Name = CurrentSelectionFontFamily;
                    if (CurrentSelectionBoldDecoration != (currentSelection.CharacterFormat.Bold == FormatEffect.On)) {
                        if (CurrentSelectionBoldDecoration)
                            currentSelection.CharacterFormat.Bold = FormatEffect.On;
                        else
                            currentSelection.CharacterFormat.Bold = FormatEffect.Off;
                    }
                    if (CurrentSelectionItalicDecoration != (currentSelection.CharacterFormat.Italic == FormatEffect.On)) {
                        if (CurrentSelectionItalicDecoration)
                            currentSelection.CharacterFormat.Italic = FormatEffect.On;
                        else
                            currentSelection.CharacterFormat.Italic = FormatEffect.Off;
                    }
                    if (CurrentSelectionUnderlineDecoration != (currentSelection.CharacterFormat.Underline == UnderlineType.Single)) {
                        if (CurrentSelectionUnderlineDecoration)
                            currentSelection.CharacterFormat.Underline = UnderlineType.Single;
                        else
                            currentSelection.CharacterFormat.Underline = UnderlineType.None;
                    }
                    if (CurrentSelectionSubscript != (currentSelection.CharacterFormat.Subscript == FormatEffect.On)) {
                        if (CurrentSelectionSubscript)
                            currentSelection.CharacterFormat.Subscript = FormatEffect.On;
                        else
                            currentSelection.CharacterFormat.Subscript = FormatEffect.Off;
                    }
                    if (CurrentSelectionSuperscript != (currentSelection.CharacterFormat.Superscript == FormatEffect.On)) {
                        if (CurrentSelectionSuperscript)
                            currentSelection.CharacterFormat.Superscript = FormatEffect.On;
                        else
                            currentSelection.CharacterFormat.Superscript = FormatEffect.Off;
                    }
                }
            }
            base.OnKeyDown(e);
        }
        double GetCurrentFontSize(string currentSelectionText) {
            var fontSizeRx = new Regex(@"\\fs([0-9]+)\s?");
            MatchCollection matches = fontSizeRx.Matches(currentSelectionText);
            if (matches.Count > 1 || matches.Count == 0)
                return double.NaN;
            else
                return (double.Parse(matches[0].Groups[1].Value)/2);
        }

        string GetCurrentFontFamily(string currentSelectionText) {            
            return currentSelection.CharacterFormat.Name;
        }
        
        protected override void OnSelectionChanged(object sender, RoutedEventArgs e) {
            base.OnSelectionChanged(sender, e);
            if (!selectionLocked) {
                selectionLocked = true;
                
                currentSelection = Document.Selection;
                string currentSelectionText;
                try {
                    currentSelection.GetClone().GetText(TextGetOptions.FormatRtf, out currentSelectionText);
                } catch {
                    currentSelectionText = "";
                }
                if (currentSelection.Length == 0) {
                    CurrentSelectionFontSize = currentSelection.CharacterFormat.Size;
                } else {
                    CurrentSelectionFontSize = GetCurrentFontSize(currentSelectionText);
                }

                CurrentSelectionFontFamily = GetCurrentFontFamily(currentSelectionText);
                if (currentSelection.Length == 0)
                {
                    CurrentSelectionBoldDecoration = false;
                    CurrentSelectionItalicDecoration = false;
                    CurrentSelectionUnderlineDecoration = (currentSelection.CharacterFormat.Underline == UnderlineType.Single);
                    CurrentSelectionSubscript = (currentSelection.CharacterFormat.Subscript == FormatEffect.On);
                    CurrentSelectionSuperscript = (currentSelection.CharacterFormat.Superscript == FormatEffect.On);
                }
                else {
                    CurrentSelectionBoldDecoration = (currentSelection.CharacterFormat.Bold == FormatEffect.On);
                    CurrentSelectionItalicDecoration = (currentSelection.CharacterFormat.Italic == FormatEffect.On);
                    CurrentSelectionUnderlineDecoration = (currentSelection.CharacterFormat.Underline == UnderlineType.Single);
                    CurrentSelectionSubscript = (currentSelection.CharacterFormat.Subscript == FormatEffect.On);
                    CurrentSelectionSuperscript = (currentSelection.CharacterFormat.Superscript == FormatEffect.On);
                }
                CurrentSelectionAlignment = currentSelection.ParagraphFormat.Alignment;
                CurrentSelectionBulletsList = !(currentSelection.ParagraphFormat.ListLevelIndex == 0 || currentSelection.ParagraphFormat.ListType != MarkerType.Bullet);
                bool imageWasSelected = IsImageSelected;
                bool imageIsSelected = false;
                if (currentSelection.Length == 0) {
                    var tempRange = Document.GetRange(currentSelection.StartPosition - 1, currentSelection.StartPosition).GetClone();
                    string tempRangeContent = "";
                    try {
                        tempRange.GetText(TextGetOptions.FormatRtf, out tempRangeContent);
                    } catch {
                        tempRangeContent = "";
                    }
                    if (tempRangeContent.Contains("\\pict")) {
                        currentSelection.StartPosition -= 1;
                        currentSelection.EndPosition += 1;
                        imageIsSelected = true;
                    } else {
                        tempRange = Document.GetRange(currentSelection.StartPosition, currentSelection.StartPosition + 1).GetClone();
                        try {
                            tempRange.GetText(TextGetOptions.FormatRtf, out tempRangeContent);
                        } catch {
                            tempRangeContent = "";
                        }
                        if (tempRangeContent.Contains("\\pict")) {
                            currentSelection.EndPosition += 2;
                            imageIsSelected = true;
                        } else {
                            imageIsSelected = false;
                        }
                    }
                } else {
                    string tempRangeContent;
                    currentSelection.GetClone().GetText(TextGetOptions.FormatRtf, out tempRangeContent);
                    if (Math.Abs(currentSelection.Length) > 2) imageIsSelected = false;
                    else imageIsSelected = tempRangeContent.Contains("\\pict");
                }
                IsImageSelected = imageIsSelected;
                if (imageWasSelected && !imageLocker.IsLocked)
                    UpdateImageParams();
                selectionLocked = false;
            }
        }
        protected async void OnSelectedImageHeightChanged() {
            if (IsImageSelected && !imageLocker.IsLocked) {
                selectionLocked = true;
                imageLocker.Lock();
                string tempRangeContent;
                currentSelection.GetClone().GetText(TextGetOptions.FormatRtf, out tempRangeContent);
                string newHeight = "\\pichgoal" + SelectedImageHeight * 10;
                Regex hRegex = new Regex(@"\\pichgoal[0-9]*");
                string valueToReplace = hRegex.Match(tempRangeContent).Value;
                tempRangeContent = tempRangeContent.Replace(valueToReplace, newHeight);
                currentSelection.SetText(TextSetOptions.FormatRtf, tempRangeContent);
                await Task.Delay(100);
                imageLocker.Unlock();
                selectionLocked = false;
            }
        }
        protected async void OnSelectedImageWidthChanged() {
            if (IsImageSelected && !imageLocker.IsLocked) {
                selectionLocked = true;
                imageLocker.Lock();
                string tempRangeContent;
                currentSelection.GetClone().GetText(TextGetOptions.FormatRtf, out tempRangeContent);
                string newHeight = "\\picwgoal" + SelectedImageWidth * 10;
                Regex hRegex = new Regex(@"\\picwgoal[0-9]*");
                string valueToReplace = hRegex.Match(tempRangeContent).Value;
                tempRangeContent = tempRangeContent.Replace(valueToReplace, newHeight);
                currentSelection.SetText(TextSetOptions.FormatRtf, tempRangeContent);
                await Task.Delay(100);
                imageLocker.Unlock();
                selectionLocked = false;
            }
        }
        Locker imageLocker = new Locker();
        void UpdateImageParams() {
            if (!imageLocker.IsLocked) {
                imageLocker.Lock();
                string tempRangeContent;
                currentSelection.GetClone().GetText(TextGetOptions.FormatRtf, out tempRangeContent);
                if (!string.IsNullOrEmpty(tempRangeContent)) {
                    var matchesHeigth = new Regex(@"\\pichgoal([0-9]*)").Matches(tempRangeContent);
                    if (matchesHeigth.Count > 0 && matchesHeigth[0].Groups.Count > 1) {
                        int newHeight = int.Parse(matchesHeigth[0].Groups[1].Value) / 10;
                        if (SelectedImageHeight != newHeight)
                            SelectedImageHeight = newHeight;
                    }
                    var mathesWidth = new Regex(@"\\picwgoal([0-9]*)").Matches(tempRangeContent);
                    if (mathesWidth.Count > 0 && mathesWidth[0].Groups.Count > 1) {
                        int newWidth = int.Parse(mathesWidth[0].Groups[1].Value) / 10;
                        if (newWidth != SelectedImageWidth)
                            SelectedImageWidth = newWidth;
                    }
                }
                imageLocker.Unlock();
            }
        }
        void OnCurrentForegroundChanged() {
            if (currentSelection != null) {
                currentSelection.CharacterFormat.ForegroundColor = CurrentForeground;
                UpdateSelection();
            } else {
                var currentFormat = Document.GetDefaultCharacterFormat();
                currentFormat.ForegroundColor = CurrentForeground;
                Document.SetDefaultCharacterFormat(currentFormat);
            }
        }
        protected virtual void OnCurrentSelectionListLevelChanged() {
            if (CurrentSelectionListLevel < 0)
                CurrentSelectionListLevel = 0;
            if (currentSelection != null) {
                currentSelection.ParagraphFormat.ListLevelIndex = CurrentSelectionListLevel;
                UpdateSelection();
            }
        }
        protected virtual void OnBulletsListChanged() {
            if (currentSelection != null) {
                if (CurrentSelectionBulletsList) {
                    string selectedTextRtf;
                    currentSelection.GetClone().GetText(TextGetOptions.FormatRtf, out selectedTextRtf);
                    currentSelection.ParagraphFormat.ListType = MarkerType.Bullet;
                    currentSelection.ParagraphFormat.ListLevelIndex = CurrentSelectionListLevel;
                }
                UpdateSelection();
            }
        }
        protected virtual void OnCurrentSelectionUnderlineDecorationChanged() {
            if (currentSelection != null) {
                if (CurrentSelectionUnderlineDecoration != (currentSelection.CharacterFormat.Underline == UnderlineType.Single))
                    if (CurrentSelectionUnderlineDecoration)
                        currentSelection.CharacterFormat.Underline = UnderlineType.Single;
                    else
                        currentSelection.CharacterFormat.Underline = UnderlineType.None;
                UpdateSelection();
            } 
        }
        protected virtual void OnCurrentSelectionItalicDecorationChanged() {
            if (currentSelection != null) {
                if (CurrentSelectionItalicDecoration != (currentSelection.CharacterFormat.Italic == FormatEffect.On))
                    if (CurrentSelectionItalicDecoration)
                        currentSelection.CharacterFormat.Italic = FormatEffect.On;
                    else
                        currentSelection.CharacterFormat.Italic = FormatEffect.Off;
                UpdateSelection();
            } 
        }
        protected virtual void OnCurrentSelectionBoldDecorationChanged() {
            if (currentSelection != null && currentSelection.Length != 0) {
                if (CurrentSelectionBoldDecoration != (currentSelection.CharacterFormat.Bold == FormatEffect.On))
                    if (CurrentSelectionBoldDecoration)
                        currentSelection.CharacterFormat.Bold = FormatEffect.On;
                    else
                        currentSelection.CharacterFormat.Bold = FormatEffect.Off;
                UpdateSelection();
            } 
        }
        protected virtual void OnCurrentSelectionFontSizeChanged() {
            if (currentSelection != null && currentSelection.Length != 0 && !double.IsNaN(CurrentSelectionFontSize)) {
                if (CurrentSelectionFontSize != currentSelection.CharacterFormat.Size)
                    currentSelection.CharacterFormat.Size = (float)CurrentSelectionFontSize;
                UpdateSelection();
            }
        }
        protected virtual void OnCurrentSelectionFontFamilyChanged() {
            if (CurrentSelectionFontFamily != null) {
                if (currentSelection != null) {
                    if (CurrentSelectionFontFamily != currentSelection.CharacterFormat.Name && CurrentSelectionFontFamily != "")
                        currentSelection.CharacterFormat.Name = CurrentSelectionFontFamily;
                    UpdateSelection();
                }
            }
        }
        protected virtual void OnCurrentSelectionAlignmentChanged() {
            if (currentSelection != null) {
                if (CurrentSelectionAlignment != currentSelection.ParagraphFormat.Alignment)
                    currentSelection.ParagraphFormat.Alignment = CurrentSelectionAlignment;
                UpdateSelection();
            } 
        }
        protected virtual void OnCurrentSelectionSubscriptChanged() {
            if (CurrentSelectionSubscript)
                CurrentSelectionSuperscript = false;
            if (currentSelection != null && currentSelection.Length != 0) {
                if (CurrentSelectionSubscript != (currentSelection.CharacterFormat.Subscript == FormatEffect.On))
                    if (CurrentSelectionSubscript) {
                        currentSelection.CharacterFormat.Subscript = FormatEffect.On;
                    } else {
                        currentSelection.CharacterFormat.Subscript = FormatEffect.Off;
                    }
                UpdateSelection();
            } 
        }
        protected virtual void OnCurrentSelectionSuperscriptChanged() {
            if (CurrentSelectionSuperscript)
                CurrentSelectionSubscript = false;
            if (currentSelection != null && currentSelection.Length != 0) {
                if (CurrentSelectionSuperscript != (currentSelection.CharacterFormat.Superscript == FormatEffect.On))
                    if (CurrentSelectionSuperscript) {
                        currentSelection.CharacterFormat.Superscript = FormatEffect.On;
                    } else {
                        currentSelection.CharacterFormat.Superscript = FormatEffect.Off;
                    }
                UpdateSelection();
            } 
        }
    }
}

