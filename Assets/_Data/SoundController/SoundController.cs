using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rubik.Sound
{
    public class SoundController : MonoBehaviour
    {
        //Drag a reference to the audio source which will play the sound effects.
        public AudioSource efxSource1;
        public AudioSource efxSource2;
        public AudioSource efxSource3;
        //Drag a reference to the audio source which will play the music.
        public AudioSource musicSource;
        //Allows other scripts to call functions from SoundManager.             
        public static SoundController Instance { get; set; }
        //The lowest a sound effect will be randomly pitched.
        public float lowPitchRange = 0.95f;
        //The highest a sound effect will be randomly pitched.
        public float highPitchRange = 1.05f;

        void Awake()
        {
            if (Instance == null)
                Instance = this;
            else if (Instance != this)
                Destroy(gameObject);
            if (PlayerPrefs.GetInt("FirstPlay", 0) == 0)
            {
                PlayerPrefs.SetInt("FirstPlay", 1);
                PlayerPrefs.SetInt("MusicOn", 1);
                PlayerPrefs.SetInt("SoundOn", 1);
            }
            ToggleSound(PlayerPrefs.GetInt("SoundOn", 1) == 1 ? true : false);
            if (PlayerPrefs.GetInt("MusicOn", 1) == 1)
            {
                ContinueMusic();
            }
            else
            {
                Mute();
            }
            // DontDestroyOnLoad(gameObject);
        }


        public void PlaySingle(AudioClip clip)
        {
            if (efxSource1.isPlaying)
            {
                PlaySecond(clip);
            }
            else
            {
                efxSource1.PlayOneShot(clip);
            }
        }

        private void PlaySecond(AudioClip clip)
        {
            if (efxSource2.isPlaying)
            {
                PlayThird(clip);
            }
            else
            {
                efxSource2.PlayOneShot(clip);
            }
        }

        private void PlayThird(AudioClip clip)
        {
            efxSource3.PlayOneShot(clip);
        }

        public void PlayMusic(AudioClip clip)
        {
            //if (GameController.Instance.IsSoundOn)
            {
                musicSource.clip = clip;
                musicSource.Play();
            }
        }

        public void Mute()
        {
            musicSource.volume = 0;
            //efxSource1.volume = efxSource2.volume = efxSource3.volume = 0;
            PlayerPrefs.SetInt("MusicOn", 0);
        }

        public void ContinueMusic()
        {
            musicSource.volume = 0.5f;
            //efxSource1.volume = efxSource2.volume = efxSource3.volume = 1;
            PlayerPrefs.SetInt("MusicOn", 1);
        }
        public void ToggleSound(bool isOn)
        {

            if (!isOn)
            {
                efxSource1.volume = efxSource2.volume = efxSource3.volume = 0;
                PlayerPrefs.SetInt("SoundOn", 0);
            }
            else
            {
                efxSource1.volume = efxSource2.volume = efxSource3.volume = 1f;
                PlayerPrefs.SetInt("SoundOn", 1);
            }
        }

        public void ToggleMusic(bool isOn)
        {
            if (!isOn)
            {
                this.Mute();
            }
            else
            {
                this.ContinueMusic();
            }
        }

        #region Getter
        public bool IsSoundOn()
        {
            return PlayerPrefs.GetInt("SoundOn", 1) == 1;
        }
        public bool IsMusicOn()
        {
            return PlayerPrefs.GetInt("MusicOn", 1) == 1;
        }
        #endregion
    }
}