using DevExpress.WinUI.Data;
using DevExpress.WinUI.Grid;
using DevExpress.WinUI.Mvvm.UI.Interactivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridDemo {
    public sealed class ActiveTasksCountBehavior : Behavior<GridControl> {
        protected override void OnAttached() {
            base.OnAttached();
            AssociatedObject.CustomSummary += AssociatedObject_CustomSummary;
        }
        protected override void OnDetaching() {
            base.OnDetaching();
            AssociatedObject.CustomSummary -= AssociatedObject_CustomSummary;
        }

        void AssociatedObject_CustomSummary(object sender, CustomSummaryEventArgs e) {
            if(!e.IsTotalSummary)
                return;
            switch(e.SummaryProcess) {
                case CustomSummaryProcess.Start:
                    e.TotalValue = 0;
                    break;
                case CustomSummaryProcess.Calculate:
                    if(e.FieldValue as bool? == false)
                        e.TotalValue = (int)e.TotalValue + 1;
                    break;
                case CustomSummaryProcess.Finalize:
                    break;
            }
        }
    }
}