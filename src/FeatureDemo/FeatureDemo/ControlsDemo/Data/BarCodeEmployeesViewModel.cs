using System;
using System.Windows.Input;
using DevExpress.WinUI.Core;
using Microsoft.UI.Xaml;
using System.Threading.Tasks;

namespace FeatureDemo.ControlsDemo {
    public class BarCodeEmployeesViewModel : DevExpress.Mvvm.BindableBase {
        
        
        Employe selectedEmploye;
        public Employe SelectedEmploye {
            get { return selectedEmploye; }
            set { SetProperty(ref selectedEmploye, value, nameof(SelectedEmploye)); }
        }
        public BarCodeEmployeesViewModel() {
            Accounts = new ObservableCollectionCore<Employe>();
            var dataStorage = new FeatureDemo.Data.DataStorage();
            
            
            
            
                for(int i = 0; i < 10; i++) {
                    Accounts.Add(new Employe(this, dataStorage.Employees[i]));
                }
            
            SelectedEmploye = Accounts[0];
            PrintCommand = new DevExpress.Mvvm.DelegateCommand(PrintDocument);
        }
        public ObservableCollectionCore<Employe> Accounts { get; private set; }
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        

        async void PrintDocument() {
            
            
            await Task.Yield();
            
            
            
        }
        public ICommand PrintCommand { get; set; }
    }
}