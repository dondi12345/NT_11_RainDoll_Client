using System.Collections.Generic;
using NTPackage.Functions;
using UnityEngine;

namespace NT.RainDoll.Ground
{
    public class UserGridObjectManager : NTBehaviour
    {
        public List<UserGridObject> UserGridObjects = new List<UserGridObject>();
        public Dictionary<string, UserGridObject> UserGridObjectsDict = new Dictionary<string, UserGridObject>();

        public int GridWidth = 3;
        public int GridHeight = 3;

        public static UserGridObjectManager Instance;
        protected override void Awake()
        {
            base.Awake();
            if (UserGridObjectManager.Instance != null)
            {
                NTLog.LogError("Only 1 Instance allow");
                return;
            }
            UserGridObjectManager.Instance = this;
        }

        public void LoadData(){
            this.DefaultUserGridObjects();
        }

        public void DefaultUserGridObjects(){
            this.UserGridObjects.Clear();
            this.UserGridObjectsDict.Clear();
            // 0_0
            this.UserGridObjects.Add(new UserGridObject(0, 0, GridObjectType.Empty));
            //0_-1
            this.UserGridObjects.Add(new UserGridObject(0, -1, GridObjectType.Empty));
            for(int i = 0; i < this.GridHeight; i++){
                for(int j = 0; j < this.GridWidth; j++){
                    if(j == 0){
                        this.UserGridObjects.Add(new UserGridObject(j, -i-2, GridObjectType.Empty));
                    }else{
                        this.UserGridObjects.Add(new UserGridObject(j, -i-2, GridObjectType.Empty));
                        this.UserGridObjects.Add(new UserGridObject(-j, -i-2, GridObjectType.Empty));
                    }
                }
            } 
        }

    }
}