using System.Collections.Generic;
using NTPackage;
using NTPackage.Functions;
using UnityEngine;

namespace NT.RainDoll.Ground
{
    public class GridObjectSlotManager : NTBehaviour
    {
        public NTDictionary<string, GridObjectSlot> GridObjectSlots;
        public GridObjectSlot GridObjectSlotPrefab;
        public Transform GridObjectSlotHolder;

        public int Width;
        public int Height;
        public float GridSize;
        public float GridYOffset = -0.63f;

        public static GridObjectSlotManager Instance;
        protected override void Awake()
        {
            base.Awake();
            if (GridObjectSlotManager.Instance != null){
               NTLog.LogError("Only 1 Instance allow");
               return;
             }
            GridObjectSlotManager.Instance = this;
        }

        public void LoadData(){
            this.GenerateGridObjectSlots();
        }

        [NTButton]
        public void GenerateGridObjectSlots(){
            this.ClearGridObjectSlots();
            for(int y = 0; y < this.Height; y++){
                for(int x = 0; x < this.Width; x++){
                    if(x == 0){
                        GridObjectSlot gridObjectSlotMiddle = GameObject.Instantiate(this.GridObjectSlotPrefab, this.GridObjectSlotHolder);
                        gridObjectSlotMiddle.x = x;
                        gridObjectSlotMiddle.y = -y;
                        gridObjectSlotMiddle.transform.position = new Vector3(x * this.GridSize, -y * this.GridSize + this.GridYOffset, 0);
                        this.GridObjectSlots.Add(gridObjectSlotMiddle.GetKey(), gridObjectSlotMiddle);
                    }else{   
                        GridObjectSlot gridObjectSlotLeft = GameObject.Instantiate(this.GridObjectSlotPrefab, this.GridObjectSlotHolder);
                        gridObjectSlotLeft.x = -x;
                        gridObjectSlotLeft.y = -y;
                        gridObjectSlotLeft.transform.position = new Vector3(-x * this.GridSize, -y * this.GridSize + this.GridYOffset, 0);
                        this.GridObjectSlots.Add(gridObjectSlotLeft.GetKey(), gridObjectSlotLeft);
                        
                        GridObjectSlot gridObjectSlotRight = GameObject.Instantiate(this.GridObjectSlotPrefab, this.GridObjectSlotHolder);
                        gridObjectSlotRight.x = x;
                        gridObjectSlotRight.y = -y;
                        gridObjectSlotRight.transform.position = new Vector3(x * this.GridSize, -y * this.GridSize + this.GridYOffset, 0);
                        this.GridObjectSlots.Add(gridObjectSlotRight.GetKey(), gridObjectSlotRight);
                    }
                }
            }
        }

        public void ClearGridObjectSlots(){
            List<GridObjectSlot> gridObjectSlots = this.GridObjectSlots.ToList();
            for(int i = gridObjectSlots.Count-1; i >= 0; i--){
                if(gridObjectSlots[i] == null) continue;
                DestroyImmediate(gridObjectSlots[i].gameObject);
            }
            this.GridObjectSlots.Clear();
        }

        public GridObjectSlot GetGridObjectSlot(int x, int y){
            return this.GridObjectSlots.Get(x.ToString() + "_" + y.ToString());
        }
    }
}