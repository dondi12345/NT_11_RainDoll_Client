using UnityEngine;
using UnityEngine.UI;
#if UNITY_STANDALONE_WIN && !UNITY_EDITOR
using System.Runtime.InteropServices;
#endif
namespace NTPackage.TransparentUnityApp
{
    public class SafeTaskBarWindow : MonoBehaviour
    {

#if UNITY_STANDALONE_WIN && !UNITY_EDITOR

[DllImport("user32.dll")]
static extern bool SystemParametersInfo(
    uint uiAction,
    uint uiParam,
    ref RECT pvParam,
    uint fWinIni);

const uint SPI_GETWORKAREA = 0x0030;

[StructLayout(LayoutKind.Sequential)]
public struct RECT
{
    public int Left;
    public int Top;
    public int Right;
    public int Bottom;
}
#endif

        public RectTransform Panel;

        void Start()
        {
            ApplyTaskbarMargin();
        }

        void ApplyTaskbarMargin()
        {
            int taskbarHeight = GetBottomTaskbarHeight();

            // Add bottom margin
            Panel.offsetMin = new Vector2(
                Panel.offsetMin.x,
                taskbarHeight
            );
        }

        int GetBottomTaskbarHeight()
        {
#if UNITY_STANDALONE_WIN && !UNITY_EDITOR

            RECT workArea = new RECT();
            SystemParametersInfo(SPI_GETWORKAREA, 0, ref workArea, 0);

            int screenHeight = Screen.currentResolution.height;
            int workHeight = workArea.Bottom - workArea.Top;

            return screenHeight - workHeight;

#else
            return 0;
#endif
        }

    }
}