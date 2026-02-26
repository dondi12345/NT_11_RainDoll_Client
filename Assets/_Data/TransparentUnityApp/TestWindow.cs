using UnityEngine;

namespace NTPackage.TransparentUnityApp
{
    public class TestWindow : MonoBehaviour
    {
        public TransparentWindow TransparentWindow;
        public bool TransparentWindowEnabled = true;
        public ClickthroughWindow ClickthroughWindow;
        public bool ClickthroughWindowEnabled = true;
        public StayOnTopWindow StayOnTopWindow;
        public bool StayOnTopWindowEnabled = true;

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                TransparentWindowEnabled = !TransparentWindowEnabled;
                TransparentWindow.SetTransparent(TransparentWindowEnabled);
            }
            if (Input.GetKeyDown(KeyCode.C))
            {
                ClickthroughWindowEnabled = !ClickthroughWindowEnabled;
                ClickthroughWindow.SetClickthrough(ClickthroughWindowEnabled);
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                StayOnTopWindowEnabled = !StayOnTopWindowEnabled;
                StayOnTopWindow.SetAlwaysOnTop(StayOnTopWindowEnabled);
            }
        }
    }
}
