using NTPackage.Functions;
using UnityEngine;

namespace NTPackage.UI
{
    public class Slider : NTBehaviour
    {
        public RectTransform Parent;
        public RectTransform Fill;
        public float Value;
        public float NewValue;
        public float Step = 0.01f;
        public float Duration = 0.2f;

        protected override void Update()
        {
            base.Update();
            if (this.Value < this.NewValue)
            {
                this.Value += this.Step * Time.deltaTime;
                if (this.Value >= this.NewValue) this.Value = this.NewValue;
                float width = this.Parent.rect.width;
                float right = width * this.Value;
                this.Fill.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0, right);
            }
        }

        public void OnUI()
        {
            this.Value = 0;
            this.NewValue = 0;
            this.Fill.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0, 0);
        }

        public void SetValue(float value)
        {
            if (this.Parent == null || this.Fill == null)
            {
                NTLog.LogError("Slider is not initialized");
                return;
            }

            this.NewValue = value;
            this.Step = (this.NewValue - this.Value) / this.Duration;
        }
    }
}