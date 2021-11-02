using System.Windows.Input;
using DevExpress.Mvvm;
using Windows.UI.Text;
using MarkerType = Microsoft.UI.Text.MarkerType;
using DevExpress.Mvvm.CodeGenerators;

namespace RibbonDemo {
    [GenerateViewModel]
    public partial class ParagraphStyleGroupViewModel : ViewModelBase {
        public ParagraphStyleGroupViewModel(RibbonToolBarViewModel parent){
            ((ISupportParentViewModel)this).ParentViewModel = parent;
        }

        RibbonToolBarViewModel Parent => ((ISupportParentViewModel)this).ParentViewModel as RibbonToolBarViewModel;
        protected IRichEditorParagraphService RichEditorParagraphService => Parent.Service as IRichEditorParagraphService;

        [GenerateCommand]
        void SetListStyle(string obj) {
            if (obj == "bullet")
                RichEditorParagraphService.SetListType(MarkerType.Bullet);

            else if (obj == "arabic")
                RichEditorParagraphService.SetListType(MarkerType.Arabic);

            else if (obj == "none")
                RichEditorParagraphService.SetListType(MarkerType.None);

        }
        [GenerateCommand]
        void IncreaseParagraphIndent() => RichEditorParagraphService.DecreaseParagraphIndent();
        [GenerateCommand]
        void DecreaseParagraphIndent() => RichEditorParagraphService.IncreaseParagraphIndent();
    }
}
