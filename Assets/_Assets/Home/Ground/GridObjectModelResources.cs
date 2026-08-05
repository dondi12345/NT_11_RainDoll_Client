using System.Collections.Generic;
using NTPackage;
using NTPackage.Functions;
using UnityEngine;

namespace NT.RainDoll.Ground
{
    public class GridObjectModelResources : NTBehaviour
    {
        public List<GridObjectModel> GridObjectModels;
        public NTDictionary<GridObjectType, GridObjectModel> GridObjectModelDictionary;

        public List<Sprite> GridObjectModelSprites;

        public static GridObjectModelResources Instance;
        protected override void Awake()
        {
            base.Awake();
            if (GridObjectModelResources.Instance != null){
               NTLog.LogError("Only 1 Instance allow");
               return;
             }
            GridObjectModelResources.Instance = this;
        }

        public void LoadData(){
            this.GridObjectModelDictionary = new NTDictionary<GridObjectType, GridObjectModel>();
            foreach (var gridObjectModel in this.GridObjectModels){
                this.GridObjectModelDictionary.Add(gridObjectModel.GridObjectType, gridObjectModel);
            }
        }

        public GridObjectModel GetGridObjectModel(GridObjectType gridObjectType){
            return this.GridObjectModelDictionary.Get(gridObjectType);
        }
    }
}