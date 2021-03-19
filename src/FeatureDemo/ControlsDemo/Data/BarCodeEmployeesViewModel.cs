using System;
using System.Windows.Input;
using DevExpress.WinUI.Core;
using Windows.ApplicationModel;
using Windows.UI.Core;
using Windows.UI.Popups;
using Microsoft.UI.Xaml;
using Windows.Graphics.Printing;
using ICommand = Microsoft.UI.Xaml.Input.ICommand;

namespace FeatureDemo.ControlsDemo {
    public class BarCodeEmployeesViewModel : DevExpress.Mvvm.BindableBase {
        CoreDispatcher dispatcher = Window.Current.Dispatcher;
        Employe selectedEmploye;
        public Employe SelectedEmploye {
            get { return selectedEmploye; }
            set { SetProperty(ref selectedEmploye, value); }
        }
        public BarCodeEmployeesViewModel() {
            Accounts = new ObservableCollectionCore<Employe>();
            var dataStorage = new FeatureDemo.Data.DataStorage();
            if (DesignMode.DesignModeEnabled)
                Accounts.Add(new Employe(this, dataStorage.Employees[0]));
            else {
                for(int i = 0; i < 10; i++) {
                    Accounts.Add(new Employe(this, dataStorage.Employees[i]));
                }
            }
            SelectedEmploye = Accounts[0];
            PrintCommand = new DevExpress.Mvvm.DelegateCommand(PrintDocument);
        }
        public ObservableCollectionCore<Employe> Accounts { get; private set; }
        void printManager_PrintTaskRequested(PrintManager sender, PrintTaskRequestedEventArgs args) {
            PrintTask printTask = null;
            printTask = args.Request.CreatePrintTask("BarCode Demo Print Task", sourceRequested => {
                printTask.Completed += async (s, e) => {
                    if(e.Completion == PrintTaskCompletion.Failed) {
                        await dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, async () => {
                            await new MessageDialog("Printing error.").ShowAsync();
                        });
                    }
                };
                sourceRequested.SetSource(SelectedEmploye.PrintDocumentSource);
            });
        }

        async void PrintDocument() {
            PrintManager.GetForCurrentView().PrintTaskRequested += printManager_PrintTaskRequested;
            await PrintManager.ShowPrintUIAsync();
            PrintManager.GetForCurrentView().PrintTaskRequested -= printManager_PrintTaskRequested;
        }
        public ICommand PrintCommand { get; set; }
    }
}