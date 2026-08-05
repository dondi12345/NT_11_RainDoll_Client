using UnityEngine;

namespace NT.RainDoll.Ground
{
    public class GridObjectSlot : MonoBehaviour{
        public int x;
        public int y;

        public string GetKey(){
            return this.x.ToString() + "_" + this.y.ToString();
        }
    }
}
