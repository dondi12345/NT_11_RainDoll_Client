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
        static extern bool SetLayeredWindowAttributes(
            IntPtr hwnd, uint crKey, byte bAlpha, uint dwFlags);

        const int GWL_EXSTYLE = -20;

        const uint WS_EX_LAYERED = 0x80000;
        const uint WS_EX_TRANSPARENT = 0x20;

        const uint LWA_COLORKEY = 0x1;
#endif

        void Start()
        {
            this.SetTransparent(true);
        }

        public void SetTransparent(bool value)
        {
#if UNITY_STANDALONE_WIN && !UNITY_EDITOR
            IntPtr hwnd = GetActiveWindow();
            uint style = GetWindowLong(hwnd, GWL_EXSTYLE);

            if (value){
                SetWindowLong(hwnd, GWL_EXSTYLE, style | WS_EX_LAYERED);

                // Remove black color (0x000000)
                SetLayeredWindowAttributes(hwnd, 0x000000, 0, LWA_COLORKEY);
            }else{
                // Remove layered + transparent flags
                style &= ~WS_EX_LAYERED;
                style &= ~WS_EX_TRANSPARENT;

                SetWindowLong(hwnd, GWL_EXSTYLE, style);
            }
#endif
        }
    }
}

