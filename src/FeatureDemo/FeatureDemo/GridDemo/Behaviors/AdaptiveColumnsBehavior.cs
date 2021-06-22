using DevExpress.WinUI.Grid;
using DevExpress.WinUI.Core;
using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridDemo {
    public sealed class AdaptiveColumnsBehavior : Behavior<GridControl> {
        public static readonly DependencyProperty ViewportModeProperty;
        public static readonly DependencyProperty SmallViewportColumnsProperty;
        public static readonly DependencyProperty MiddleViewportColumnsProperty;

        static AdaptiveColumnsBehavior() {
            var ownerType = typeof(AdaptiveColumnsBehavior);
            ViewportModeProperty = DependencyProperty.Register(nameof(ViewportMode), typeof(ViewportMode), ownerType, new PropertyMetadata(ViewportMode.Small, (d, e) => ((AdaptiveColumnsBehavior)d).OnViewportModeChanged()));
            SmallViewportColumnsProperty = DependencyProperty.Register(nameof(SmallViewportColumns), typeof(string), ownerType, new PropertyMetadata(null));
            MiddleViewportColumnsProperty = DependencyProperty.Register(nameof(MiddleViewportColumns), typeof(string), ownerType, new PropertyMetadata(null));
        }

        void OnViewportModeChanged() => UpdateViewport();

        public ViewportMode ViewportMode {
            get { return (ViewportMode)GetValue(ViewportModeProperty); }
            set { SetValue(ViewportModeProperty, value); }
        }
        public string SmallViewportColumns {
            get { return (string)GetValue(SmallViewportColumnsProperty); }
            set { SetValue(SmallViewportColumnsProperty, value); }
        }
        public string MiddleViewportColumns {
            get { return (string)GetValue(MiddleViewportColumnsProperty); }
            set { SetValue(MiddleViewportColumnsProperty, value); }
        }

        protected override void OnAttached() {
            base.OnAttached();
            UpdateViewport();
        }

        void UpdateViewport() {
            var visibleColumns = GetVisibleColumns();
            AssociatedObject.Columns.BeginUpdate();
            for(int i = 0; i < AssociatedObject.Columns.Count; i++) {
                var column = AssociatedObject.Columns[i];
                column.Visible = visibleColumns.Contains(column);
                column.VisibleIndex = i;
            }
            AssociatedObject.Columns.EndUpdate();
        }
        ColumnBase[] GetVisibleColumns() {
            switch(ViewportMode) {
                case ViewportMode.Small: return GetColumns(SmallViewportColumns);
                case ViewportMode.Middle: return GetColumns(MiddleViewportColumns);
                case ViewportMode.Full: return AssociatedObject.Columns.ToArray();
                default: throw new NotSupportedException();
            }
        }

        ColumnBase[] GetColumns(string columnsStr) => columnsStr?.Split(";").Select(x => AssociatedObject.Columns.SingleOrDefault(c => c.FieldName == x)).ToArray() ?? new ColumnBase[0];
    }

    public enum ViewportMode {
        Small, Middle, Full
    }
}
