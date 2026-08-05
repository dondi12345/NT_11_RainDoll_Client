using UnityEngine;
using NTPackage;
using NTPackage.Functions;
using System.Collections.Generic;

namespace NT.RainDoll.Ground
{
    public class GridObjectManager : NTBehaviour
    {
        public NTDictionary<string, GridObjectData> GridObjectDatas;
        public NTDictionary<string, GridObject> GridObjects;
        public GridObject GridObjectPrefab;
        public Transform GridObjectHolder;

        public int Width;
        public int Height;

        public static GridObjectManager Instance;
        protected override void Awake()
        {
            base.Awake();
            if (GridObjectManager.Instance != null){
               NTLog.LogError("Only 1 Instance allow");
               return;
             }
            GridObjectManager.Instance = this;
        }

        public void LoadData(){
            this.GenerateGridObjects();
        }

        [NTButton]
        public void GenerateGridObjects()
        {
            this.GridObjectDatas = new NTDictionary<string, GridObjectData>();
            for(int y = 0; y < this.Height; y++){
                for(int x = 0; x < this.Width; x++){
                    if(x == 0){
                        GridObjectData gridObjectMiddle = new GridObjectData();
                        gridObjectMiddle.x = x;
                        gridObjectMiddle.y = -y;
                        gridObjectMiddle.Type = GridObjectType.Earth;
                        this.GridObjectDatas.Add(gridObjectMiddle.GetKey(), gridObjectMiddle);
                    }else{
                        GridObjectData gridObjectLeft = new GridObjectData();
                        gridObjectLeft.x = -x;
                        gridObjectLeft.y = -y;
                        gridObjectLeft.Type = GridObjectType.Earth;
                        this.GridObjectDatas.Add(gridObjectLeft.GetKey(), gridObjectLeft);
                        GridObjectData gridObjectRight = new GridObjectData();
                        gridObjectRight.x = x;
                        gridObjectRight.y = -y;
                        gridObjectRight.Type = GridObjectType.Earth;
                        this.GridObjectDatas.Add(gridObjectRight.GetKey(), gridObjectRight);
                    }
                }
            }
        }

        [NTButton]
        public void GenerateGridObjectModel(){
            List<GridObject> gridObjects = this.GridObjects.ToList();
            foreach (GridObject gridObject in gridObjects){
                if(gridObject != null){
                    DestroyImmediate(gridObject.gameObject);
                }
            }
            this.GridObjects = new NTDictionary<string, GridObject>();
            foreach (GridObjectData gridObjectData in this.GridObjectDatas.ToList()){
                GridObject gridObject = Instantiate(this.GridObjectPrefab, this.GridObjectHolder);
                gridObject.Data = gridObjectData;
                gridObject.UpdateData();
                this.GridObjects.Add(gridObject.GetKey(), gridObject);
            }
        }

        public GridObjectData GetGridObject(int x, int y){
            return this.GridObjectDatas.Get(x.ToString() + "_" + y.ToString());
        }

    }
}