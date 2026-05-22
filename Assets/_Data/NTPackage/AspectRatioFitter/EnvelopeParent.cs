using NTPackage.Functions;
using UnityEngine;

namespace NTPackage.UI
{
public class EnvelopeParent : NTBehaviour
{
    public RectTransform Parent;
    public RectTransform RectTransform;

    protected override void Update()
    {
        base.Update();
        if(this.Parent == null){
            this.Parent = transform.parent as RectTransform;
        }
        float parentWidth = this.Parent.rect.width;
        float parentHeight = this.Parent.rect.height;
        float width = this.RectTransform.rect.width;
        float height = this.RectTransform.rect.height;
        float scale = 1;
        float scaleX = parentWidth / width;
        float scaleY = parentHeight / height;
        if(scaleX > scaleY){
            scale = scaleX;
        }
        else{
            scale = scaleY;
        }
        this.RectTransform.localScale = new Vector2(scale, scale);
    }
}
}
