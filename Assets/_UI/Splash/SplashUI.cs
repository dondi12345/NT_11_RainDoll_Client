using NTPackage.UI;
using UnityEngine;
using Spine.Unity;
using System.Collections;
using TMPro;

namespace RainDoll.SplashUI
{
    public class SplashUI : PopupUI
    {
        public SkeletonGraphic SkeletonGraphic;
        public Transform Panel;
        public Slider Slider;

        public TextMeshProUGUI TextMeshProUGUI;

        protected override void Start()
        {
            this.ShowAnimation();
        }

        protected override void Update(){
            base.Update();
            this.TextMeshProUGUI.text = "Loading " + this.Slider.Value.ToString("F2") + "%";
        }

        public void ShowAnimation(){
            this.Slider.gameObject.SetActive(false);
            this.SkeletonGraphic.AnimationState.SetAnimation(0, "start", false);
            this.SkeletonGraphic.AnimationState.AddAnimation(0, "show", false, 0);
            // this.SkeletonGraphic.AnimationState.AddAnimation(0, "idle", false, 0);
            this.SkeletonGraphic.AnimationState.AddAnimation(0, "end", true, 0);
            if(this.Coroutine != null) StopCoroutine(this.Coroutine);
            this.StartCoroutine(this.ShowSliderCoroutine());
        }

        public IEnumerator ShowSliderCoroutine(){
            //Wait for the animation to do idle_end
            yield return new WaitUntil(() => this.SkeletonGraphic.AnimationState.GetCurrent(0).Animation.Name == "end");
            this.Slider.gameObject.SetActive(true);
            this.Slider.OnUI();
            this.Slider.SetValue(0);
            yield return new WaitForSeconds(0.2f); 
            this.Slider.SetValue(0.2f);
            yield return new WaitForSeconds(0.2f);
            this.Slider.SetValue(0.4f);
            yield return new WaitForSeconds(0.2f);
            this.Slider.SetValue(0.6f);
            yield return new WaitForSeconds(0.2f);
            this.Slider.SetValue(0.8f);
            yield return new WaitForSeconds(0.2f);
            this.Slider.SetValue(1);
            yield return new WaitForSeconds(0.5f);
            PopupManager.Instance.OnUI(PopupCode.LoadingUI);
            yield return new WaitForSeconds(2f);
            PopupManager.Instance.OffUI(PopupCode.LoadingUI);
        }
    }
}
