using System.Collections.Generic;
using NTPackage;
using NTPackage.Functions;
using UnityEngine;


namespace NT.RainDoll.Ground
{
    public class GroundGenerate : NTBehaviour
    {
        public NTDictionary<string, GridObject> GridObjects;
        public GridObject GridObjectPrefab;
        public Transform Holder;

        public int Width;
        public int Height;
        public float GridSize;

        [NTButton]
        public void GenerateGround()
        {
            this.Clear();
            for (int y = 0; y < this.Height; y++){
                for (int x = 0; x < this.Width; x++){
                    if(x == 0){
                        GridObject midGridObject = GameObject.Instantiate(this.GridObjectPrefab, this.Holder);
                        midGridObject.transform.position = new Vector3(x*this.GridSize, -y*this.GridSize, 0);
                        midGridObject.x = x;
                        midGridObject.y = -y;
                        this.GridObjects.Add(x.ToString() + "_" + y.ToString(), midGridObject);
                    }else{
                        GridObject leftGridObject = GameObject.Instantiate(this.GridObjectPrefab, this.Holder);
                        leftGridObject.transform.position = new Vector3(-x*this.GridSize, -y*this.GridSize, 0);
                        leftGridObject.x = -x;
                        leftGridObject.y = -y;
                        this.GridObjects.Add(x.ToString() + "_" + y.ToString(), leftGridObject);

                        GridObject rightGridObject = GameObject.Instantiate(this.GridObjectPrefab, this.Holder);
                        rightGridObject.transform.position = new Vector3(x*this.GridSize, -y*this.GridSize, 0);
                        rightGridObject.x = x;
                        rightGridObject.y = -y;
                        this.GridObjects.Add(x.ToString() + "_" + y.ToString(), rightGridObject);
                    }
                }
            }

        }

        public void Clear(){
            if(this.GridObjects == null) return;
            List<GridObject> gridObjects = this.GridObjects.ToList();
            for (int i = gridObjects.Count-1; i >=0; i--)
            {
                if(gridObjects[i] == null) continue;
                DestroyImmediate(gridObjects[i].gameObject);
            }
            this.GridObjects.Clear();
        }
    }
}
