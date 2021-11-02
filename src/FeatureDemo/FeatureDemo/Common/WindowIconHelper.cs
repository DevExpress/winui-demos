using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.UI.Xaml;
using WinRT;

namespace FeatureDemo {
    public static class WindowIconHelper {
        public static void SetIcon(this Window wnd, string iconName)
            => SendMessage(wnd.As<IWindowNative>().WindowHandle, WM_SETICON, 0,
                LoadImage(IntPtr.Zero, iconName, 1, 16, 16, 16));

        const uint WM_SETICON = 0x0080;

        [ComImport]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        [Guid("EECDBF0E-BAE9-4CB6-A68E-9598E1CB57BB")]
        interface IWindowNative {
            IntPtr WindowHandle { get; }
        }
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, nuint wParam, nint lParam);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        static extern IntPtr LoadImage(IntPtr hinst, string lpszName, uint uType, int cxDesired, int cyDesired, uint fuLoad);
    }
}
