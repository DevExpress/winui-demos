using System.Collections.Generic;
using System;
using DevExpress.WinUI.Data;
using System.Threading.Tasks;
using System.Linq;
using System.ComponentModel;
using DevExpress.WinUI.Data.Filtering;

namespace GridDemo {
    public static class IssuesHelper {
        public static IssueFilter MakeIssueFilter(CriteriaOperator filter) {
            return filter.Match(
                binary: (propertyName, value, type) => {
                    if (propertyName == "Votes") {
                        int? _value = null;
                        if (value != null) {
                            double v;
                            if (double.TryParse(value.ToString(), out v)) {
                                _value = (int)v;
                            }
                        }
                        switch (type) {
                            case BinaryOperatorType.Greater:
                                return new IssueFilter(minVotes: _value == null ? 0 : (int)(double)_value + 1); 
                            case BinaryOperatorType.GreaterOrEqual:
                                return new IssueFilter(minVotes: _value == null ? 0 : (int)(double)_value);
                            case BinaryOperatorType.Less:
                                 return new IssueFilter(maxVotes: _value == null || (double)_value < 0 ? int.MaxValue : (int)(double)_value - 1);
                            case BinaryOperatorType.LessOrEqual:
                                return new IssueFilter(maxVotes: _value == null || (double)_value < 0 ? int.MaxValue : (int)(double)_value);
                        }
                    }
                    if (propertyName == "Created") {
                        switch (type) {
                            case BinaryOperatorType.Greater: 
                                return new IssueFilter(createdFrom: value == null ? DateTime.MinValue : ((DateTime)value).AddTicks(1));
                            case BinaryOperatorType.GreaterOrEqual: 
                                return new IssueFilter(createdFrom: value == null ? DateTime.MinValue : (DateTime)value);
                            case BinaryOperatorType.Less: 
                                return new IssueFilter(createdTo: value == null ? DateTime.MaxValue : ((DateTime)value).AddTicks(-1));
                            case BinaryOperatorType.LessOrEqual: 
                                return new IssueFilter(createdTo: value == null ? DateTime.MaxValue : (DateTime)value);
                        }   
                    }
                    if (propertyName == "Priority" && type == BinaryOperatorType.Equal)
                        return new IssueFilter(priority: (Priority)value);
                    if (propertyName == "User" && type == BinaryOperatorType.Equal) 
                        return new IssueFilter(user: (string)value);

                    throw new InvalidOperationException(string.Format("Can't convert '{0}' FilterCriteria to IssueFilter (type = '{1}', value = '{2}')", propertyName, type, value));
                },
                and: filters => {
                    var result = new IssueFilter(
                        priority: filters.Select(x => x.Priority).SingleOrDefault(x => x != null),
                        createdFrom: filters.Select(x => x.CreatedFrom).SingleOrDefault(x => x != null),
                        createdTo: filters.Select(x => x.CreatedTo).SingleOrDefault(x => x != null),
                        minVotes: filters.Select(x => x.MinVotes).SingleOrDefault(x => x != null),
                        maxVotes: filters.Select(x => x.MaxVotes).SingleOrDefault(x => x != null),
                        user: filters.Select(x => x.User).SingleOrDefault(x => x != null)
                    );
                    return result;
                },
                @null: default(IssueFilter)
            );
        }
        public static IssueSortOrder GetIssueSortOrder(FetchRowsEventArgsBase e) {
            if (e.SortOrder.Length > 0) {
                var sort = e.SortOrder.Single();
                if (sort.PropertyName == "Created") {
                    return sort.Direction == ListSortDirection.Ascending
                        ? IssueSortOrder.CreatedAscending
                        : IssueSortOrder.CreatedDescending;
                }
                if (sort.PropertyName == "Votes") {
                    return sort.Direction == ListSortDirection.Ascending
                        ? IssueSortOrder.VotesAscending
                        : IssueSortOrder.VotesDescending;
                }
                if (sort.PropertyName == "User") {
                    return sort.Direction == ListSortDirection.Ascending
                        ? IssueSortOrder.UserAscending
                        : IssueSortOrder.UserDescending;
                }
                if (sort.PropertyName == "Priority") {
                    return sort.Direction == ListSortDirection.Ascending
                        ? IssueSortOrder.PriorityAscending
                        : IssueSortOrder.PriorityDescending;
                }
            }
            return IssueSortOrder.Default;
        }
        public static IEnumerable<IssueData> FilterIssues(IssueFilter filter, IEnumerable<IssueData> issues) {
            if (filter == null)
                return issues;
            issues = issues.Where(x => {
                return (!filter.CreatedFrom.HasValue || x.Created >= filter.CreatedFrom.Value) &&
                       (!filter.CreatedTo.HasValue || x.Created <= filter.CreatedTo.Value) &&
                       (!filter.MinVotes.HasValue || x.Votes >= filter.MinVotes.Value) &&
                       (!filter.MaxVotes.HasValue || x.Votes <= filter.MaxVotes.Value) &&
                       (!filter.Priority.HasValue || x.Priority == filter.Priority.Value) &&
                       (filter.User == null || x.User == filter.User);
            });            
            return issues;
        }
        public static IEnumerable<IssueData> SortIssues(IssueSortOrder sortOrder, IEnumerable<IssueData> issues) {
            switch (sortOrder) {
                case IssueSortOrder.Default:
                    return issues.OrderByDescending(x => x, new DefaultSortComparer()).ThenByDescending(x => x.Created);
                case IssueSortOrder.CreatedDescending:
                    return issues.OrderByDescending(x => x.Created);
                case IssueSortOrder.CreatedAscending:
                    return issues.OrderBy(x => x.Created);
                case IssueSortOrder.VotesAscending:
                    return issues.OrderBy(x => x.Votes).ThenByDescending(x => x.Created);
                case IssueSortOrder.VotesDescending:
                    return issues.OrderByDescending(x => x.Votes).ThenByDescending(x => x.Created);
                case IssueSortOrder.UserAscending:
                    return issues.OrderBy(x => x.User).ThenByDescending(x => x.Created);
                case IssueSortOrder.UserDescending:
                    return issues.OrderByDescending(x => x.User).ThenByDescending(x => x.Created);
                case IssueSortOrder.PriorityAscending:
                    return issues.OrderBy(x => x.Priority).ThenByDescending(x => x.Created);
                case IssueSortOrder.PriorityDescending:
                    return issues.OrderByDescending(x => x.Priority).ThenByDescending(x => x.Created);
                default:
                    return issues;
            }
        }
    }

    public static class IssuesService {
        static Lazy<IssueData[]> AllIssues = new Lazy<IssueData[]>(() => {
            var date = DateTime.Today;
            var rnd = new Random(0);
            return Enumerable.Range(0, rnd.Next(900000, 1000000))
                .Select(i => {
                    date = date.AddSeconds(-rnd.Next(20 * 60));
                    return new IssueData(
                        subject: OutlookDataGenerator.GetSubject(),
                        user: OutlookDataGenerator.GetFrom(),
                        created: date,
                        votes: rnd.Next(100),
                        priority: OutlookDataGenerator.GetPriority());
                }).ToArray();
        });

        public async static Task<IssueData[]> GetIssuesAsync(int page, int pageSize, IssueSortOrder sortOrder, IssueFilter filter) {
            return await Task.Run(() => {
                var issues = IssuesHelper.SortIssues(sortOrder, AllIssues.Value);
                if (filter != null)
                    issues = IssuesHelper.FilterIssues(filter, issues);
                issues = issues.Skip(page * pageSize).Take(pageSize);
                return issues.ToArray();
            });
        }

        public async static Task<IssuesSummaries> GetSummariesAsync(IssueFilter filter) {
            return await Task.Run(() => {
                return new IssuesSummaries(count: IssuesHelper.FilterIssues(filter, AllIssues.Value).Count());
            });
        }
    }

    public enum IssueSortOrder {
        Default,

        CreatedDescending,
        CreatedAscending,

        VotesAscending,
        VotesDescending,

        UserAscending,
        UserDescending,

        PriorityAscending,
        PriorityDescending,
    }

    public class IssuesSummaries {
        public IssuesSummaries(int count) {
            Count = count;
        }
        public int Count { get; private set; }
    }

    public class IssueFilter {
        public IssueFilter(Priority? priority = null, DateTime? createdFrom = null, DateTime? createdTo = null, int? minVotes = null, int? maxVotes = null, string user = null) {
            Priority = priority;
            CreatedFrom = createdFrom;
            CreatedTo = createdTo;
            MinVotes = minVotes;
            MaxVotes = maxVotes;
            User = user;
        }
        public string User { get; private set; }
        public Priority? Priority { get; private set; }
        public DateTime? CreatedFrom { get; private set; }
        public DateTime? CreatedTo { get; private set; }
        public int? MinVotes { get; private set; }
        public int? MaxVotes { get; private set; }
    }
    [Windows.UI.Xaml.Data.Bindable]
    public class IssueData {
        public IssueData(string subject, string user, DateTime created, int votes, Priority priority) {
            Subject = subject;
            User = user;
            Created = created;
            Votes = votes;
            Priority = priority;
        }
        public string Subject { get; private set; }
        public string User { get; private set; }
        public DateTime Created { get; private set; }
        public int Votes { get; private set; }
        public Priority Priority { get; private set; }
    }

    public enum Priority { Low, BelowNormal, Normal, AboveNormal, High }

    class DefaultSortComparer : IComparer<IssueData> {
        int IComparer<IssueData>.Compare(IssueData x, IssueData y) {
            if (x.Created.Date != y.Created.Date)
                return Comparer<DateTime>.Default.Compare(x.Created.Date, y.Created.Date);
            return Comparer<int>.Default.Compare(x.Votes, y.Votes);
        }
    }
}