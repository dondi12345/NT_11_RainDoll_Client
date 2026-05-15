using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using NTPackage.Functions;
using SimpleJSON;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using NTPackage;
using System;

namespace Rubik.LogSender
{
    public class CacheLog
    {
        public List<string> Logs = new List<string>();
    }

    public class LogSender : MonoBehaviour
    {
        public const string KeyCacheLog = "Multiplayer:CacheLog";
        public const string KeyVersion = "Multiplayer:Version";
        public string Version;
        public const string LogSender_SendLog = "/api/multiplayer/log_sender/send_log";
        public NTDictionary<string, string> LogHis = new NTDictionary<string, string>();
        public CacheLog CacheLog;


        void OnEnable()
        {
            if(PlayerPrefs.HasKey(KeyVersion)){
                this.Version = PlayerPrefs.GetString(KeyVersion);
            }else{
                this.Version = "";
            }
            if(!this.Version.EndsWith(Application.version)){
                PlayerPrefs.DeleteKey(KeyCacheLog);
                PlayerPrefs.DeleteKey(KeyVersion);
                this.Version = Application.version;
                PlayerPrefs.SetString(KeyVersion, this.Version);
                this.CacheLog = new CacheLog();
                PlayerPrefs.SetString(KeyCacheLog, JsonUtility.ToJson(this.CacheLog));
            }

            this.LogHis = new NTDictionary<string, string>();
            CacheLog = JsonUtility.FromJson<CacheLog>(PlayerPrefs.GetString(KeyCacheLog));
            if (CacheLog != null)
            {
                foreach (string item in CacheLog.Logs)
                {
                    this.LogHis.Add(item, "1");
                }
            }
            Application.logMessageReceived += HandleLog;
        }

        void OnDisable()
        {
            Application.logMessageReceived -= HandleLog;
        }

        void HandleLog(string logString, string stackTrace, LogType type)
        {
            if (type == LogType.Error || type == LogType.Exception || type == LogType.Assert)
            {
                NTLog.LogMessage("HandleLog: " + logString);
                if (this.LogHis.Get(logString + stackTrace) != null) return;
                this.LogHis.Add(logString + stackTrace, stackTrace);
                if (CacheLog == null) CacheLog = new CacheLog();
                this.CacheLog.Logs.Add(logString + stackTrace);
                PlayerPrefs.SetString(KeyCacheLog, JsonUtility.ToJson(this.CacheLog));
                try
                {
                    string fileName = GetFileNameFromStackTrace(stackTrace);
                    int lineNumber = GetLineNumberFromStackTrace(stackTrace);

                    string logData = $"Log: {logString}-File: {fileName}-Line: {lineNumber}-Stack Trace: {stackTrace}-Type: {type}";
#if !UNITY_EDITOR
                    SendLogToServer(logData);
#endif
                }
                catch (System.Exception e)
                {
                    Debug.Log($"Failed to send log: {e.Message}");
                }

            }
        }

        // Extract file name from stack trace using regex
        string GetFileNameFromStackTrace(string stackTrace)
        {
            // Example stack trace line: "at Namespace.Class.Method (fileName.cs:lineNumber)"
            var match = Regex.Match(stackTrace, @"\(at (.+):\d+\)");
            return match.Success ? match.Groups[1].Value : "Unknown File";
        }

        // Extract line number from stack trace using regex
        int GetLineNumberFromStackTrace(string stackTrace)
        {
            // Example stack trace line: "at Namespace.Class.Method (fileName.cs:lineNumber)"
            var match = Regex.Match(stackTrace, @":(\d+)\)");
            return match.Success ? int.Parse(match.Groups[1].Value) : -1;
        }

        void SendLogToServer(string logData)
        {
            return;
            JSONNode jdata = new JSONObject();
            // jdata["userID"] = UserDataManager.Instance.UserData._id.Length >= 0 ? UserDataManager.Instance.UserData._id.Length : "Unknown";
            jdata["log"] = logData;
            jdata["deviceModel"] = SystemInfo.deviceModel;
            jdata["operatingSystem"] = SystemInfo.operatingSystem;
            jdata["gameVersion"] = Application.version;
            Debug.Log($"Sending log: {jdata["log"]}");

            StartCoroutine(Rubik.Server.APIManager.Instance.PostDataUrl(jdata.ToString(), Rubik.Config.URL_Config.BASE_API_URL + LogSender_SendLog, (UnityWebRequest request) =>
            {

            }));
        }
    }

}
