using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

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

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int x;
            public int y;
        }

            [DllImport("user32.dll")]
        static extern bool GetCursorPos(out POINT lpPoint);

        const int GWL_EXSTYLE = -20;
        const uint WS_EX_TRANSPARENT = 0x20;

        const uint SWP_NOMOVE = 0x0002;
        const uint SWP_NOSIZE = 0x0001;
        const uint SWP_NOZORDER = 0x0004;
        const uint SWP_FRAMECHANGED = 0x0020;

        const uint WS_EX_NOACTIVATE = 0x08000000;
        IntPtr hwnd;
#endif

        public bool ClickthroughEnabled = true;
        public bool ClickthroughStatus = true;

        // UI raycast cache (avoid GC)
        List<RaycastResult> uiResults = new List<RaycastResult>();
        PointerEventData pointerData;

        void Start()
        {
#if UNITY_STANDALONE_WIN && !UNITY_EDITOR
            hwnd = GetActiveWindow();
#endif
            if (EventSystem.current != null)
                pointerData = new PointerEventData(EventSystem.current);
            this.ClickthroughStatus = false;
            this.SetClickthrough(true);
        }

        private void Update()
        {
            Vector2 mousePos = GetMousePosition();

            bool overUI = IsPointerOverUI(mousePos);
            if (overUI)
            {
                SetClickthrough(false);
                return;
            }
            bool overCollider = IsPointerOver2D(mousePos);
            if (overCollider)
            {
                SetClickthrough(false);
                return;
            }
            SetClickthrough(true);
        }

        public void SetClickthrough(bool enable)
        {
            if(!this.ClickthroughEnabled){
                enable = false;
            }
#if UNITY_STANDALONE_WIN && !UNITY_EDITOR
            if(ClickthroughStatus == enable){
                return;
            }
            ClickthroughStatus = enable;
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

        Vector2 GetMousePosition()
        {
#if UNITY_STANDALONE_WIN && !UNITY_EDITOR
        POINT p;
        GetCursorPos(out p);

        // Convert from screen (top-left origin) to Unity (bottom-left origin)
        return new Vector2(p.x, Screen.height - p.y);
#else
            return Input.mousePosition;
#endif
        }

        #region UI Detection

        bool IsPointerOverUI(Vector2 mousePos)
        {
            if (EventSystem.current == null)
                return false;

            pointerData.position = mousePos;

            uiResults.Clear();
            EventSystem.current.RaycastAll(pointerData, uiResults);

            return uiResults.Count > 0;
        }

        #endregion

        #region 2D Detection

        bool IsPointerOver2D(Vector2 mousePos)
        {
            if (Camera.main == null)
                return false;

            Vector3 world = Camera.main.ScreenToWorldPoint(mousePos);
            world.z = 0f;

            return Physics2D.OverlapPoint(world) != null;
        }

        #endregion
    }
}