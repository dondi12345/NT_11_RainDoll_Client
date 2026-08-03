using UnityEngine;

namespace NT.RainDoll.Ground
{

    public enum UserGridObjectType
    {
        Empty, // Was dug
        Earth, // Not was dug (default)
        Grass,        
    }

    public class UserGridObject
    {
        public int X;
        public int Y;
        public UserGridObjectType Type;
    }
}
