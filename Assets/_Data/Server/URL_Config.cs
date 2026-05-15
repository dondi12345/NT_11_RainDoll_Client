using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rubik.Config
{
    public class URL_Config
    {
        public static string BASE_API_URL
        {
            get
            {
                return "";
                // return SystemManager.Instance.SystemData.API_Url;
                // return LocalHost_Sever;
                // return DataCenterManager.instance.SystemData.API_Url;
                // return Live_Sever;
                // return Test_Sever;
                // return NT_Sever;
            }
        }

        public static string Socket_URL
        {
            get
            {
                return "";
                // return SystemManager.Instance.SystemData.Socket_Url;
                // return LocalHost_Socket;
                // return DataCenterManager.instance.SystemData.Socket_Url;
                // return Test_Sever_Socket;
                // return Live_Sever_Socket;
            }
        }

        public static string Colyseus_Url
        {
            get
            {
                return "";
                // return SystemManager.Instance.SystemData.Colyseus_Url;
            }
        }

    }
}