using NT.RainDoll.Ground;
using NTPackage.Functions;
using UnityEngine;

namespace NT.RainDoll.Home
{
    public class HomeSceneController : NTBehaviour
    {
        protected override void Start()
        {
            base.Start();
            this.LoadResource();
            this.LoadData();
        }


        public void LoadResource(){
            GridObjectModelResources.Instance.LoadData();
        }

        public void LoadData(){
            UserGridObjectManager.Instance.LoadData();
            GridObjectSlotManager.Instance.LoadData();
            GridObjectManager.Instance.LoadData();
        }
    }
}