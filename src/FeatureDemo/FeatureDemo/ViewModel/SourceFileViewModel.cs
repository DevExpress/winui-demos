using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;


namespace FeatureDemo.ViewModel {
    public enum CodeLanguage { Plain = 0, Xaml, CS, VB }
    public class SourceFileViewModel {
        const string CSSuffix = ".cs";
        const string VBSuffix = ".vb";
        const string XamlSuffix = ".xaml";
        public SourceFileViewModel(string fileName) {
            FileName = fileName;
            Language = GetCodeLanguage();
        }
        public string GetContent() {
            string resourceFile = "." + FileName.Replace("/", ".");
            Assembly assembly = GetType().GetTypeInfo().Assembly;
            List<string> resourceNameList = new List<string>();
            foreach(string resourceName in assembly.GetManifestResourceNames().Where(x=>!x.Contains(".net"))) {
                if(resourceName.EndsWith(resourceFile, StringComparison.Ordinal)) {
                    resourceNameList.Add(resourceName);
                }
            }
            if(resourceNameList.Count == 0)
                return "Source not found";
            if(resourceNameList.Count > 1) {
                return $"There are many sources with such a name: {string.Join("; ", resourceNameList)}";
            }

            
            using (StreamReader reader = new StreamReader(assembly.GetManifestResourceStream(resourceNameList[0]))) {
                return reader.ReadToEnd();
            }            
        }

        public string FileName { get; private set; }
        public CodeLanguage Language { get; private set; }
        CodeLanguage GetCodeLanguage() {
            if(FileName == null) {
                return CodeLanguage.Plain;
            }
            if(FileName.EndsWith(CSSuffix)) {
                return CodeLanguage.CS;
            }
            if(FileName.EndsWith(VBSuffix)) {
                return CodeLanguage.VB;
            }
            if(FileName.EndsWith(XamlSuffix)) {
                return CodeLanguage.Xaml;
            }           
            return CodeLanguage.Plain;            
        }
    }
}
