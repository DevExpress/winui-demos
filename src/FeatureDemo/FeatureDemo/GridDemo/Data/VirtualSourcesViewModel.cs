using DevExpress.WinUI.Grid;
using FeatureDemo.Data;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GridDemo {
    public class VirtualSourcesViewModel : DevExpress.Mvvm.BindableBase {
        InfiniteAsyncSource _items;
        public InfiniteAsyncSource Items {
            get { return _items; }
            private set { SetProperty(ref _items, value, nameof(Items)); }
        }

        public VirtualSourcesViewModel() {
            InitSource();
        }

        void InitSource() {
            Items = new InfiniteAsyncSource() {
                ElementType = typeof(IssueData)
            };
            Items.FetchRows += (o, e) => {
                e.Result = FetchRowsAsync(e);
            };
            Items.GetUniqueValues += (o, e) => {
                e.Result = GetUniqueValuesAsync(e);
            };
            Items.GetTotalSummaries += (o, e) => {
                e.Result = GetTotalSummariesAsync(e);
            };
        }

        async Task<object[]> GetUniqueValuesAsync(GetUniqueValuesAsyncEventArgs e) {
            if (e.PropertyName == "Priority") {
                return await Task.FromResult(Enum.GetValues(typeof(Priority)).Cast<object>().ToArray());
            } else if (e.PropertyName == "User") {
                return await Task.FromResult(User.Users.Select((x) => { return x.Name; }).ToArray<object>());
            } else {
                throw new InvalidOperationException(String.Format("Column {0} does not support unique values", e.PropertyName));
            }
        }

        async Task<FetchRowsResult> FetchRowsAsync(FetchRowsAsyncEventArgs e) {
            IssueSortOrder sortOrder = IssuesHelper.GetIssueSortOrder(e);
            IssueFilter filter = IssuesHelper.MakeIssueFilter(e.Filter);
            const int pageSize = 30;
            var issues = await IssuesService.GetIssuesAsync(
                page: e.Skip / pageSize,
                pageSize: pageSize,
                sortOrder: sortOrder,
                filter: filter);
            return new FetchRowsResult(issues, hasMoreRows: issues.Length == pageSize);
        }

        async Task<object[]> GetTotalSummariesAsync(GetSummariesAsyncEventArgs e) {
            IssueFilter filter = IssuesHelper.MakeIssueFilter(e.Filter);
            var summaryValues = await IssuesService.GetSummariesAsync(filter);
            return e.Summaries.Select(x => {
                if (x.SummaryType == SummaryType.Count)
                    return (object)summaryValues.Count;
                throw new InvalidOperationException();
            }).ToArray();
        }
    }
}