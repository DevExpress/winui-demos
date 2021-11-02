using FeatureDemo.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridDemo {
    public class OutlookDataViewModel {
        List<OutlookData> items;

        public List<OutlookData> Items { get { return items ?? (items = OutlookDataGenerator.CreateData(Count != 0 ? Count : 100)); } }
        public int Count { get; set; }
    }
    public static class OutlookDataGenerator {
        static Random rnd = new Random();
        public static readonly string[] Subjects = new string[] { "Integrating Developer Express MasterView control into an Accounting System.",
                                                "Web Edition: Data Entry Page. There is an issue with date validation.",
                                                "Payables Due Calculator is ready for testing.",
                                                "Web Edition: Search Page is ready for testing.",
                                                "Main Menu: Duplicate Items. Somebody has to review all menu items in the system.",
                                                "Receivables Calculator. Where can I find the complete specs?",
                                                "Ledger: Inconsistency. Please fix it.",
                                                "Receivables Printing module is ready for testing.",
                                                "Screen Redraw. Somebody has to look at it.",
                                                "Email System. What library are we going to use?",
                                                "Cannot add new vendor. This module doesn't work!",
                                                "History. Will we track sales history in our system?",
                                                "Main Menu: Add a File menu. File menu item is missing.",
                                                "Currency Mask. The current currency mask in completely unusable.",
                                                "Drag & Drop operations are not available in the scheduler module.",
                                                "Data Import. What database types will we support?",
                                                "Reports. The list of incomplete reports.",
                                                "Data Archiving. We still don't have this features in our application.",
                                                "Email Attachments. Is it possible to add multiple attachments? I haven't found a way to do this.",
                                                "Check Register. We are using different paths for different modules.",
                                                "Data Export. Our customers asked us for export to Microsoft Excel"};

        public static string GetSubject() {
            return Subjects[rnd.Next(Subjects.Length - 1)];
        }
        public static string GetFrom() {
            return User.Users[GetFromId()].Name;
        }
        public static DateTime GetSentDate() {
            DateTime ret = DateTime.Today;
            int r = rnd.Next(12);
            if (r > 1)
                ret = ret.AddDays(-rnd.Next(50));
            return ret;
        }
        public static int GetSize(bool largeData) {
            return 1000 + (largeData ? 20 * rnd.Next(10000) : 30 * rnd.Next(100));
        }
        public static bool GetHasAttachment() {
            return rnd.Next(10) > 5;
        }
        public static Priority GetPriority() {
            return (Priority)rnd.Next(3);
        }
        public static int GetHoursActive() {
            return (int)Math.Round(rnd.NextDouble() * 1000, 1);
        }
        public static int GetFromId() {
            return rnd.Next(User.Users.Length);
        }
        public static OutlookData CreateNewObject() {
            OutlookData obj = new OutlookData();
            obj.Subject = GetSubject();
            obj.From = GetFrom();
            obj.Sent = GetSentDate();
            obj.HasAttachment = GetHasAttachment();
            obj.Size = GetSize(obj.HasAttachment);
            obj.Priority = GetPriority();
            obj.UserId = GetFromId();
            obj.HoursActive = GetHoursActive();
            return obj;
        }
        public static OutlookData CreateOutlookData(int id) {
            OutlookData data = new OutlookData();
            data.OID = id;
            data.From = GetFrom();
            data.UserId = GetFromId();
            data.Sent = GetSentDate();
            data.HasAttachment = GetHasAttachment();
            data.Priority = GetPriority();
            data.HoursActive = GetHoursActive();
            data.Subject = GetSubject();
            return data;
        }
        public static List<OutlookData> CreateData(int rowCount) {
            List<OutlookData> dataList = new List<OutlookData>(rowCount);
            for (int i = 0; i < rowCount; i++) {
                dataList.Add(CreateOutlookData(i));
            }
            return dataList;
        }
    }
    public class OutlookData {
        public int OID { get; set; }
        public string Subject { get; set; }
        public string From { get; set; }
        public DateTime Sent { get; set; }
        public bool HasAttachment { get; set; }
        public long Size { get; set; }
        public double HoursActive { get; set; }
        public Priority Priority { get; set; }
        public int UserId { get; set; }
    }
}