using System;
using System.Collections;
using System.Collections.Generic;
using NTPackage.Functions;
using NTPackage.UI;
using Rubik.Sound;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Rubik.Config
{
    // Scene
    public class SceneConfig
    {
        public const string Login_Screen = "Login";
        public const string Home_Screen = "Home";
        public const string MiniGame01 = "MiniGame01";
        public const string MiniGame02 = "MiniGame02";
        public const string MiniGame03 = "MiniGame03";
        public const string MiniGame04 = "MiniGame04";
        public const string MiniGame05 = "MiniGame05";
        public const string City_Screen = "City";
        public const string Restaurant_Screen = "Restaurant";
        public const string CityLand = "CityLand";
        public const string Loading_Screen = "Loading";
        public const string Waiting_Room = "WaitingRoom";
        public const string ChangeCharacter = "ChangeCharacter";
        public const string MiniGameForestGame = "MiniGameForestGame"; 
        public const string MiniGameGuessNumber = "MiniGameGuessNumber";
        public const string MiniGameFishing = "Park";
        public const string MiniGameJumping= "MinigameJumping";
        public const string MiniGameScooping_Screen = "MiniGameScooping";
    }

    public class SceneController : NTBehaviour
    {
        public static SceneController Instance;
        protected override void Awake()
        {
            base.Awake();
            if (Instance != null)
            {
                NTLog.LogWarning("Only 1 Instance allow");
                return;
            }
            Instance = this;
        }

        public void LoadScreenWithLoading(string screenName, Action done = null)
        {
            StartCoroutine(LoadScreenProgress(screenName, done));
        }

        public IEnumerator LoadScreenProgress(string screenName, Action done = null)
        {
            // HUDCanvas.Instance.ShowLoadingPanel();
            if (!Application.CanStreamedLevelBeLoaded(screenName))
            {
                NTLog.LogError("Scene not exist: " + screenName);
                PopupManager.Instance.OnUI(PopupCode.MessagePanel, null, (popupUI) => {
                    // MessagePanel messagePanel = popupUI as MessagePanel;
                    // messagePanel.SetData("Scene not exist", "The scene " + screenName + " is not exist");
                });
                // HUDCanvas.Instance.HideLoadingPanel();
                yield break;
            }
            switch (screenName){
                case SceneConfig.MiniGameScooping_Screen:
                    SoundController.Instance.PlayMusic(FXSound.Instance.BGGamPlay);
                    break;
                default:
                    SoundController.Instance.PlayMusic(FXSound.Instance.BgMainScene);
                    break;
            }
            // bl_SceneLoaderManager.LoadScene(SceneConfig.Loading_Screen);
            NTLog.LogMessage("Loading Screen: " + SceneConfig.Loading_Screen);
            yield return WaitForSceneLoad(SceneConfig.Loading_Screen);
            // yield return new WaitForSeconds(1f);
            // bl_SceneLoaderManager.LoadScene(screenName);
            yield return WaitForSceneLoad(screenName);
            NTLog.LogMessage("Loading Screen: " + screenName);
            done?.Invoke();
            // HUDCanvas.Instance.HideLoadingPanel();
        }

        public IEnumerator WaitForSceneLoad(string sceneName, Action done = null)
        {
            yield return new WaitUntil(() => SceneManager.GetSceneByName(sceneName).isLoaded);
            done?.Invoke();
        }
    }
}