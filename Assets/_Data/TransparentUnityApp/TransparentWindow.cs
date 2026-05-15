using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace NTPackage.TransparentUnityApp
{
    public class TransparentWindow : MonoBehaviour
    {
#if UNITY_STANDALONE_WIN && !UNITY_EDITOR

        [DllImport("user32.dll")]
        static extern IntPtr GetActiveWindow();

        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, uint dwNewLong);

        [DllImport("user32.dll")]
        static extern uint GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        static extern bool SetLayeredWindowAttributes(IntPtr hwnd, uint crKey, byte bAlpha, uint dwFlags);

        [StructLayout(LayoutKind.Sequential)]
        public struct MARGINS
        {
            public int cxLeftWidth;
            public int cxRightWidth;
            public int cyTopHeight;
            public int cyBottomHeight;
        }

        [DllImport("dwmapi.dll")]
        static extern int DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS pMargins);

        const int GWL_EXSTYLE = -20;

        const uint WS_EX_LAYERED = 0x00080000;
        const uint WS_EX_TRANSPARENT = 0x00000020;

        const uint LWA_COLORKEY = 0x00000001;
        const uint LWA_ALPHA = 0x00000002;

        IntPtr hwnd;
#endif

        void Start()
        { 
            #if UNITY_STANDALONE_WIN && !UNITY_EDITOR
            hwnd = GetActiveWindow();
            #endif
            this.SetTransparent(true);
        }

        public void SetTransparent(bool value)
        {
#if UNITY_STANDALONE_WIN && !UNITY_EDITOR
            uint style = GetWindowLong(hwnd, GWL_EXSTYLE);

            if (value){
                MARGINS margins = new MARGINS() { cxLeftWidth = -1 };
                DwmExtendFrameIntoClientArea(hwnd, ref margins);
                style |= WS_EX_LAYERED;
                SetWindowLong(hwnd, GWL_EXSTYLE, style | WS_EX_LAYERED);
                // SetLayeredWindowAttributes(hwnd, 0, 0, LWA_COLORKEY);
            }else{
                // Remove layered + transparent flags
                // style &= ~WS_EX_LAYERED;
                // style &= ~WS_EX_TRANSPARENT;

                // SetWindowLong(hwnd, GWL_EXSTYLE, style);
            }
#endif
        }
    }
}

