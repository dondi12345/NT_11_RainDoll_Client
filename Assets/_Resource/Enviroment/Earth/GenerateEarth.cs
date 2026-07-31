using System.Collections.Generic;
using NTPackage.Functions;
using UnityEngine;

namespace NT.RainDoll.Enviroment.Earth
{
    public class GenerateEarth : NTBehaviour
    {
        public Transform EarthPrefab;

        public List<Transform> EarthList = new List<Transform>();
        public Transform Holder;

        public int Width = 10;
        public int Height = 10;
        public float EarthSize = 10.0f;

        [NTButton]
        public void Generate()
        {
            this.Clear();

            //Generate new earth
            for (int i = 0; i < this.Height; i++)
            {
                for (int j = 0; j < this.Width; j++)
                {
                    if (j == 0)
                    {
                        Transform middleEarth = Instantiate(this.EarthPrefab, this.Holder);
                        middleEarth.position = new Vector3(0, -(i) * this.EarthSize, 0);
                        this.EarthList.Add(middleEarth);
                    }

                    if (j > 0)
                    {
                        Transform rightEarth = Instantiate(this.EarthPrefab, this.Holder);
                        rightEarth.position = new Vector3((j) * this.EarthSize, -(i) * this.EarthSize, 0);
                        this.EarthList.Add(rightEarth);
                        Transform leftEarth = Instantiate(this.EarthPrefab, this.Holder);
                        leftEarth.position = new Vector3((-j) * this.EarthSize, -(i) * this.EarthSize, 0);
                        this.EarthList.Add(leftEarth);
                    }
                }
            }
        }

        public void Clear()
        {
            for (int i = this.EarthList.Count - 1; i >= 0; i--)
            {
                DestroyImmediate(this.EarthList[i].gameObject);
            }
            this.EarthList.Clear();
        }
    }
}