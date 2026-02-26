using UnityEngine;
using System.Collections.Generic;
using Spine.Unity;
using Spine;

public class CharacterConner : MonoBehaviour
{
    public SkeletonGraphic skeletonAnimation;

    public List<string> SkinsToCombine = new List<string>();
    public string AnimationName = "idle";

    void Start()
    {
        var skeleton = skeletonAnimation.Skeleton;

        // Create new empty skin
        Skin combinedSkin = new Skin("combined");

        foreach (var skinName in SkinsToCombine)
        {
            var skin = skeletonAnimation.Skeleton.Data.FindSkin(skinName);

            if (skin != null)
            {
                combinedSkin.AddSkin(skin);
            }
            else
            {
                Debug.LogWarning("Skin not found: " + skinName);
            }
        }

        skeleton.Skin = combinedSkin;
        skeleton.SetToSetupPose();
        skeletonAnimation.AnimationState.Apply(skeleton);
    }
}
