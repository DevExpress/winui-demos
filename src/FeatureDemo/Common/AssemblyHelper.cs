using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Storage;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;

namespace FeatureDemo.Common {
    public static class AssemblyHelper {
        const string NullS = "NULL";
        const string MsAppXScheme = "ms-appx:///";
        static Dictionary<Assembly, string> defaultNamespaces = new Dictionary<Assembly,string>();
        static Assembly entryAssembly;
        static Assembly[] packageAssemblies = new Assembly[] { };

        public static async Task InitAsync() {
            if(packageAssemblies.Length == 0)
                packageAssemblies = await GetPackageAssembliesCoreAsync();
        }
        public static Assembly EntryAssembly {
            get {
                if(entryAssembly == null)
                    entryAssembly = Application.Current.GetType().GetTypeInfo().Assembly;
                return entryAssembly;
            }
        }
        public static ImageSource GetImage(Assembly assembly, string path) {
            string assemblyPath = assembly == EntryAssembly ? "" : assembly.GetName().Name + "/";
            Uri uri = new Uri(MsAppXScheme + assemblyPath + path, UriKind.RelativeOrAbsolute);
            return new BitmapImage(uri);
        }
        public static string GetAssemblyQualifiedName(Type type) {
            return type == null ? NullS : type.AssemblyQualifiedName;
        }
        public static Type GetTypeByAssemblyQualifiedName(string typeString) {
            return typeString == NullS ? null : Type.GetType(typeString);
        }
        public static Assembly[] GetPackageAssemblies() {
            return packageAssemblies;
        }
        public static Assembly GetAssemblyByFullName(string assemblyFullName) {
            return assemblyFullName == NullS ? null : (from d in packageAssemblies where d.FullName == assemblyFullName select d).FirstOrDefault();
        }
        public static Assembly GetAssemblyByName(string assemblyName) {
            return assemblyName == NullS ? null : (from d in packageAssemblies where GetAssemblyName(d) == assemblyName select d).FirstOrDefault();
        }
        public static string GetAssemblyFullName(Assembly assembly) {
            return assembly == null ? NullS : assembly.FullName;
        }
        public static string GetAssemblyName(Assembly assembly) {
            return assembly == null ? NullS : GetAssemblyNameByFullName(assembly.FullName);
        }
        public static string GetAssemblyNameByFullName(string assemblyFullName) {
            if(assemblyFullName == NullS) return NullS;
            AssemblyName name = new AssemblyName(assemblyFullName);
            return name.Name;
        }
        public static IEnumerable<Type> GetExportedTypes(Assembly assembly) {
            try {
                return assembly.ExportedTypes;
            } catch {
                return new Type[] { };
            }
        }
        static async Task<Assembly[]> GetPackageAssembliesCoreAsync() {
            StorageFolder folder = Package.Current.InstalledLocation;
            List<Assembly> assemblies = new List<Assembly>();
            IReadOnlyList<StorageFile> fff = await folder.GetFilesAsync();
            foreach(StorageFile file in await folder.GetFilesAsync()) {
                if(file.FileType == ".dll" || file.FileType == ".exe") {
                    try {
                        AssemblyName assemblyName = new AssemblyName() { Name = file.Name.Remove(file.Name.Length - file.FileType.Length) };
                        Assembly assembly = Assembly.Load(assemblyName);
                        assemblies.Add(assembly);
                    } catch(FileNotFoundException) { }
                }
            }
            return assemblies.ToArray();
        }
        public static Stream GetEmbeddedResourceStream(Assembly assembly, string name, bool nameIsFull) {
            name = name.Replace('/', '.');
            Stream stream = GetEmbeddedResourceStreamCore(assembly, name, nameIsFull);
            return stream;
        }
        static Stream GetEmbeddedResourceStreamCore(Assembly assembly, string name, bool nameIsFull) {
            string fullName = GetDefultNamespace(assembly) + name;
            Stream stream = assembly.GetManifestResourceStream(fullName);
            if(stream != null || nameIsFull) return stream;
            foreach(string resourceName in assembly.GetManifestResourceNames()) {
                if(resourceName.EndsWith("." + name, StringComparison.Ordinal))
                    return assembly.GetManifestResourceStream(resourceName);
            }
            return null;
        }
        public static string GetDefultNamespace(Assembly assembly) {
            string defaultNamespace;
            if(!defaultNamespaces.TryGetValue(assembly, out defaultNamespace)) {
                defaultNamespace = GetDefultNamespaceCore(assembly);
                defaultNamespaces.Add(assembly, defaultNamespace);
            }
            return defaultNamespace;
        }
        public static string GetCommonPart(string[] strings, string[] excludedSuffixes) {
            string commonPart = null;
            foreach(string s in strings) {
                bool hasExcludedSuffix = false;
                foreach(string excludedSuffix in excludedSuffixes) {
                    if(s.EndsWith(excludedSuffix, StringComparison.Ordinal)) {
                        hasExcludedSuffix = true;
                        break;
                    }
                }
                if(hasExcludedSuffix) continue;
                if(commonPart == null) {
                    commonPart = s;
                    continue;
                }
                if(s.IndexOf(commonPart, StringComparison.Ordinal) != 0) {
                    int differ = 0;
                    for(int d = 0; d < commonPart.Length & d < s.Length; ++d) {
                        if(d >= commonPart.Length || d >= s.Length || commonPart[d] != s[d]) {
                            differ = d;
                            break;
                        }
                    }
                    commonPart = commonPart.Remove(differ);
                }
            }
            return commonPart == null ? string.Empty : commonPart;
        }
        static string GetDefultNamespaceCore(Assembly assembly) {
            string[] names = assembly.GetManifestResourceNames();
            if(names.Length == 0) return string.Empty;
            return GetCommonPart(names.Length > 1 ? names : new string[] { names[0], assembly.GetName().Name }, new string[] { ".csdl", ".ssdl", ".msl" });
        }
    }
    public static class RuntimeHelper {
        public static void Run() {
            string commandString = ApplicationData.Current.RoamingSettings.Values["RuntimeHelperData"] as string;
            if(string.IsNullOrEmpty(commandString)) return;
            string[] parts = commandString.Split(new string[] { "___" }, StringSplitOptions.RemoveEmptyEntries);
            if(parts.Length != 3) return;
            Assembly assembly = AssemblyHelper.GetAssemblyByName(parts[0]);
            if(assembly == null) return;
            Type type = assembly.GetType(parts[1]);
            if(type == null) return;
            MethodInfo method = type.GetRuntimeMethod(parts[2], new Type[] { });
            if(method == null) return;
            method.Invoke(null, new object[] { });
        }
    }
}
