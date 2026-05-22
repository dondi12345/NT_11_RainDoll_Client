using System.Collections;
using UnityEngine;
using Spine.Unity;
using NTPackage.UI;

namespace RainDoll.LoadingUI
{
    public class LoadingUI : PopupUI
    {
        public SkeletonGraphic SkeletonGraphic;
        public Transform Panel;

        [SerializeField] Animator animator;

        const int Layer = 0;
        const string StateOn = "OnUI";
        const string StateIdle = "Idle";
        const string StateClose = "OffUI";

        Coroutine uiRoutine;

        [ContextMenu("OnUI")]
        public void TestOnUI(){
            this.OnUI();
        }

        [ContextMenu("OffUI")]
        public void TestOffUI(){
            this.OffUI();
        }

        public override void Show(){
            base.Show();
             if (uiRoutine != null)
                StopCoroutine(uiRoutine);
            if(this.animator.gameObject.activeSelf) uiRoutine = StartCoroutine(OnUICoroutine());
        }

        public override void Hide(){
            if (uiRoutine != null)
                StopCoroutine(uiRoutine);
            if(this.animator.gameObject.activeSelf) uiRoutine = StartCoroutine(OffUICoroutine());
        }

        IEnumerator OnUICoroutine()
        {
            if (Panel != null)
                Panel.gameObject.SetActive(true);
            if (animator != null)
            {
                SkeletonGraphic.AnimationState.SetAnimation(0, "start", false);
                animator.Play(StateOn, Layer, 0f);
                yield return null;
                yield return WaitForCurrentStateEnd(Layer, StateOn);
                SkeletonGraphic.AnimationState.SetAnimation(0, "idle", true);
                animator.Play(StateIdle, Layer, 0f);
            }
            uiRoutine = null;
        }

        IEnumerator OffUICoroutine()
        {
            if (animator != null)
            {
                SkeletonGraphic.AnimationState.SetAnimation(0, "end", false);
                animator.Play(StateClose, Layer, 0f);
                yield return null;
                yield return WaitForCurrentStateEnd(Layer, StateClose);
            }
            if (Panel != null)
                Panel.gameObject.SetActive(false);
            uiRoutine = null;
        }

        IEnumerator WaitForCurrentStateEnd(int layer, string stateName)
        {
            while (!animator.GetCurrentAnimatorStateInfo(layer).IsName(stateName))
                yield return null;
            while (animator.GetCurrentAnimatorStateInfo(layer).normalizedTime < 1f)
                yield return null;
        }
    }
}

