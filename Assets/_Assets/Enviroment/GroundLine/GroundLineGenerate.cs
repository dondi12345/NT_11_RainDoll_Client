using System.Collections.Generic;
using NTPackage.Functions;
using UnityEngine;
namespace NT.RainDoll.Enviroment.GroundLine
{

    public class GroundLineGenerate : NTBehaviour
    {
        public GroundLine Pit;
        public GroundLine Right;
        public GroundLine Left;

        public GroundLine LinePrefab;
        public Transform Holder;
        public List<GroundLine> LineList = new List<GroundLine>();
        public float LineSize = 1.25f;
        public int Width = 10;

        [NTButton]
        public void Generate(){
            this.Clear();
            for(int i = 2; i < this.Width; i++){
                GroundLine lineLeft = Instantiate(this.LinePrefab, this.Holder);
                lineLeft.transform.position = new Vector3(-i * this.LineSize, -0.625f, 0);
                this.LineList.Add(lineLeft);
                GroundLine lineRight = Instantiate(this.LinePrefab, this.Holder);
                lineRight.transform.position = new Vector3(i * this.LineSize, -0.625f, 0);
                this.LineList.Add(lineRight);
            }
        }

        public void Clear(){
            for(int i = this.LineList.Count - 1; i >= 0; i--){
                DestroyImmediate(this.LineList[i].gameObject);
            }
            this.LineList.Clear();
        }
    }
}