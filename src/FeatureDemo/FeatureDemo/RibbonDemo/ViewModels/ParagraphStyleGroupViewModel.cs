using System.Windows.Input;
using DevExpress.Mvvm;
using Windows.UI.Text;
using MarkerType = Microsoft.UI.Text.MarkerType;

namespace RibbonDemo {
    public class ParagraphStyleGroupViewModel : ViewModelBase {
        public ParagraphStyleGroupViewModel(RibbonToolBarViewModel parent){
            ((ISupportParentViewModel)this).ParentViewModel = parent;
            IncreaseParagraphIndentCommand = new DelegateCommand(OnIncreaseIndent);
            DecreaseParagraphIndentCommand = new DelegateCommand(OnDecreaseIndent);
            SetListStyleCommand = new DelegateCommand<string>(OnSetListStyle);
        }

        RibbonToolBarViewModel Parent => ((ISupportParentViewModel)this).ParentViewModel as RibbonToolBarViewModel;
        public ICommand IncreaseParagraphIndentCommand { get; }
        public ICommand DecreaseParagraphIndentCommand { get; }
        public ICommand<string> SetListStyleCommand { get; }
        protected IRichEditorParagraphService RichEditorParagraphService => Parent.Service as IRichEditorParagraphService;

        void OnSetListStyle(string obj) {
            if (obj == "bullet")
                RichEditorParagraphService.SetListType(MarkerType.Bullet);

            else if (obj == "arabic")
                RichEditorParagraphService.SetListType(MarkerType.Arabic);

            else if (obj == "none")
                RichEditorParagraphService.SetListType(MarkerType.None);

        }
        void OnDecreaseIndent() => RichEditorParagraphService.DecreaseParagraphIndent();
        void OnIncreaseIndent() => RichEditorParagraphService.IncreaseParagraphIndent();
    }
}
