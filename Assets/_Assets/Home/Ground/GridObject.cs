using NTPackage.Functions;
using UnityEngine;

namespace NT.RainDoll.Ground
{
    [System.Serializable]
    public class GridObjectData
    {
        public int x;
        public int y;
        public GridObjectType Type;

        public string GetKey(){
            return this.x.ToString() + "_" + this.y.ToString();
        }
    }
    public class GridObject : MonoBehaviour
    {
        public GridObjectData Data;

        public GridObjectModel Model;

        public virtual void UpdateData(){

            GridObjectSlot gridObjectSlot = GridObjectSlotManager.Instance.GetGridObjectSlot(this.Data.x, this.Data.y);
            if(gridObjectSlot == null){
                NTLog.LogError("GridObjectSlot is null");
                return;
            }

            transform.position = gridObjectSlot.transform.position;

            if(this.Model != null) DestroyImmediate(this.Model.gameObject);
            this.Model = Instantiate(GridObjectModelResources.Instance.GetGridObjectModel(this.Data.Type), this.transform);
            this.Model.GridObject = this;
            this.Model.UpdateData();
            NTFunction.ResetPosition(this.Model.transform);
        }

        public string GetKey(){
            return this.Data.GetKey();
        }
    }

    public enum GridObjectType
    {
        Empty, // Was dug
        Earth, // Not was dug (default)
        Grass,        
    }
}
