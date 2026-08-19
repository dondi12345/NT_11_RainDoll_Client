using UnityEngine;

namespace NT.RainDoll.Ground
{
    [System.Serializable]
    public class UserGridObject
    {
        public int x;
        public int y;
        public GridObjectType Type;

        public UserGridObject(int x, int y, GridObjectType type){
            this.x = x;
            this.y = y;
            this.Type = type;
        }

        public string GetKey(){
            return $"{this.x}_{this.y}";
        }
    }
}
