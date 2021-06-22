using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.Storage;

namespace FeatureDemo.Common {
    public static class DemoDataLoader {
        public static string GetFileContentFromResource(string resourceRelativePath) {
            resourceRelativePath = resourceRelativePath.Replace('\\', '/');
            resourceRelativePath = resourceRelativePath.TrimStart('/');
            string uriPath = "ms-appx:///" + resourceRelativePath;
            Task<StorageFile> operation = StorageFile.GetFileFromApplicationUriAsync(new Uri(uriPath)).AsTask();
            StorageFile file = operation.Result;
            Task<string> fileReadingTask = FileIO.ReadTextAsync(file).AsTask();
            return fileReadingTask.Result;
        }
    }
}
