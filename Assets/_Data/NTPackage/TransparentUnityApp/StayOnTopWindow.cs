using System;
using System.Runtime.InteropServices;
using UnityEngine;


namespace NTPackage.TransparentUnityApp
{
    public class StayOnTopWindow : MonoBehaviour
    {
#if UNITY_STANDALONE_WIN && !UNITY_EDITOR
        [DllImport("user32.dll")]
        private static extern IntPtr GetActiveWindow();

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool SetWindowPos(
        IntPtr hWnd,
        IntPtr hWndInsertAfter,
        int X,
        int Y,
        int cx,
        int cy,
        uint uFlags);

        private static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        private static readonly IntPtr HWND_NOTOPMOST = new IntPtr(-2);

        private const uint SWP_NOMOVE = 0x0002;
        private const uint SWP_NOSIZE = 0x0001;
#endif

        private void Start()
        {
            SetAlwaysOnTop(true);
        }

        public void SetAlwaysOnTop(bool value)
        {
#if UNITY_STANDALONE_WIN && !UNITY_EDITOR
            IntPtr hWnd = GetActiveWindow();

            if (value)
            {
                SetWindowPos(hWnd, HWND_TOPMOST, 0, 0, 0, 0,
                    SWP_NOMOVE | SWP_NOSIZE);
            }
            else
            {
                SetWindowPos(hWnd, HWND_NOTOPMOST, 0, 0, 0, 0,
                    SWP_NOMOVE | SWP_NOSIZE);
            }
#endif
        }
    }
}