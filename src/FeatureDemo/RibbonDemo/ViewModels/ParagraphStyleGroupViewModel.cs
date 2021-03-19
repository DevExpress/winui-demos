using System.Windows.Input;
using DevExpress.Mvvm;
using Windows.UI.Text;
using ICommand = Microsoft.UI.Xaml.Input.ICommand;
using MarkerType = Microsoft.UI.Text.MarkerType;

namespace RibbonDemo {
    public class ParagraphStyleGroupViewModel : ViewModelBase {
        public ICommand IncreaseParagraphIndentCommand { get; private set; }
        public ICommand DecreaseParagraphIndentCommand { get; private set; }
        public ICommand<string> SetListStyleCommand { get; private set; }
        protected IRichEditorParagraphService RichEditorParagraphService { get { return GetService<IRichEditorParagraphService>(); } }

        public ParagraphStyleGroupViewModel() {
            IncreaseParagraphIndentCommand = new DelegateCommand(OnIncreaseIndent);
            DecreaseParagraphIndentCommand = new DelegateCommand(OnDecreaseIndent);
            SetListStyleCommand = new DelegateCommand<string>(OnSetListStyle);
        }
        public ParagraphStyleGroupViewModel(RibbonViewModel parent) : this() {
            ((ISupportParentViewModel)this).ParentViewModel = parent;
        }

        void OnSetListStyle(string obj) {
            if(obj == "bullet") {
                RichEditorParagraphService.SetListType(MarkerType.Bullet);
            } else if(obj == "arabic") {
                RichEditorParagraphService.SetListType(MarkerType.Arabic);
            } else if(obj == "none") {
                RichEditorParagraphService.SetListType(MarkerType.None);
            }
        }
        void OnDecreaseIndent() {
            RichEditorParagraphService.DecreaseParagraphIndent();
        }
        void OnIncreaseIndent() {
            RichEditorParagraphService.IncreaseParagraphIndent();
        }
    }
}
