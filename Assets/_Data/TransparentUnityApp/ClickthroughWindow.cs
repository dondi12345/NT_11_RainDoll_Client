using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace NTPackage.TransparentUnityApp
{
    public class ClickthroughWindow : MonoBehaviour
    {
#if UNITY_STANDALONE_WIN && !UNITY_EDITOR

        [DllImport("user32.dll")]
        static extern IntPtr GetActiveWindow();

        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, uint dwNewLong);

        [DllImport("user32.dll")]
        static extern uint GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter,
            int X, int Y, int cx, int cy, uint uFlags);

        const int GWL_EXSTYLE = -20;
        const uint WS_EX_TRANSPARENT = 0x20;

        const uint SWP_NOMOVE = 0x0002;
        const uint SWP_NOSIZE = 0x0001;
        const uint SWP_NOZORDER = 0x0004;
        const uint SWP_FRAMECHANGED = 0x0020;

#endif

        public void SetClickthrough(bool enable)
        {
#if UNITY_STANDALONE_WIN && !UNITY_EDITOR
            IntPtr hwnd = GetActiveWindow();
            uint style = GetWindowLong(hwnd, GWL_EXSTYLE);

            if (enable)
            {
                style |= WS_EX_TRANSPARENT;   // Enable click-through
            }
            else
            {
                style &= ~WS_EX_TRANSPARENT;  // Disable click-through
            }

            SetWindowLong(hwnd, GWL_EXSTYLE, style);

            // Force Windows to refresh window style
            SetWindowPos(hwnd, IntPtr.Zero, 0, 0, 0, 0,
                SWP_NOMOVE | SWP_NOSIZE | SWP_NOZORDER | SWP_FRAMECHANGED);
#endif
        }
    }
}