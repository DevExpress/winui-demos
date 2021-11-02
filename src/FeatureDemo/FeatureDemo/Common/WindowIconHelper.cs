using System;
using System.Runtime.InteropServices;
using Microsoft.UI.Xaml;
using Windows.UI.ViewManagement;
using WinRT;

namespace FeatureDemo {
    public static class WindowIconHelper {
        static IntPtr activeDarkIcon;
        static IntPtr activeLightIcon;
        static IntPtr inactiveIcon;
        static UISettings settings;
        static Window window;
        public static void AttachIcon(this Window wnd, string activeDarkIconName, string activeLightIconName, string inactiveIconName) {
            activeDarkIcon = LoadIcon(activeDarkIconName);
            activeLightIcon = LoadIcon(activeLightIconName);
            inactiveIcon = LoadIcon(inactiveIconName);
            window = wnd;
            settings = new UISettings();
            wnd.Activated += OnWindowActivated;
        }

        private static void OnWindowActivated(object sender, WindowActivatedEventArgs args) {
            if(args.WindowActivationState == WindowActivationState.Deactivated) {
                SetIcon(window, inactiveIcon);
                return;
            }
            var accent = settings.GetColorValue(UIColorType.Accent);
            if((accent.R + accent.G + accent.B) / 3 > 150) {
                SetIcon(window, activeDarkIcon);
            } else {
                SetIcon(window, activeLightIcon);
            }
        }

        static IntPtr LoadIcon(string iconName)
            => LoadImage(IntPtr.Zero, iconName, 1, 16, 16, 16);
        public static void SetIcon(this Window wnd, string iconName)
            => SetIcon(wnd, LoadIcon(iconName));
        static void SetIcon(this Window wnd, IntPtr icon)
            => SendMessage(wnd.As<IWindowNative>().WindowHandle, WM_SETICON, 0, icon);
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
