using System.Collections;
using UnityEngine;
using Spine.Unity;

namespace NTPackage.UI
{
    public class LoadingUI : PopupUI
    {
        public SkeletonGraphic SkeletonGraphic;
        public CanvasGroup CanvasGroup;
        public Transform Panel;

        [SerializeField] Animator animator;

        const int Layer = 0;
        const string StateOn = "OnUI";
        const string StateIdle = "Idle";
        const string StateClose = "OffUI";

        Coroutine uiRoutine;

        [ContextMenu("OnUI")]
        public override void OnUI(object data = null)
        {
            if (uiRoutine != null)
                StopCoroutine(uiRoutine);
            uiRoutine = StartCoroutine(OnUICoroutine());
        }

        [ContextMenu("OffUI")]
        public override void OffUI()
        {
            if (uiRoutine != null)
                StopCoroutine(uiRoutine);
            uiRoutine = StartCoroutine(OffUICoroutine());
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

