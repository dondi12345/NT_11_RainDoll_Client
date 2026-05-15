using System.Collections;
using System.Collections.Generic;
using NTPackage.Functions;
using UnityEngine;

namespace Rubik.Sound
{
    public class FXSound : MonoBehaviour
    {
        public static FXSound Instance;
        void Awake()
        {
            if (FXSound.Instance != null)
            {
                NTLog.LogError("Only 1 Instance allow");
                return;
            }
            FXSound.Instance = this;
        }
        public AudioClip BGGamPlay;
        public AudioClip BgMainScene;
        public AudioClip Fx_Button1;
        public AudioClip Fx_Button2;
        public AudioClip FX_CardPlace;

        public AudioClip FadeIn;

        public AudioClip FX_Win;
        public AudioClip FX_Lose;

        public AudioClip Fx_coin;
        public AudioClip Fx_Gem;
        public AudioClip FX_Pop_up;
        public AudioClip FX_Star_1;
        public AudioClip FX_LevelUp;
        public AudioClip FX_Evolve;
        public AudioClip FX_Assen;
        public AudioClip FX_Summon;
        public AudioClip FX_Summon1;

        public AudioClip meleeSound;
        public AudioClip rangedSound;
        public AudioClip effectHit;

        public AudioClip claimedReward;
        public AudioClip effectOpen;
        public AudioClip effectAllOpen;
        public AudioClip effectToTheLocation;
        public AudioClip scoopingComplete;

        public AudioClip[] Fx_SoundEffects;

    }

}
