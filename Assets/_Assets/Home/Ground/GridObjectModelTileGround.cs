using UnityEngine;

namespace NT.RainDoll.Ground
{
    public class GridObjectModelTileGround : GridObjectModel
    {
        public Transform Con_1;
        public Transform Con_inv_1;
        public Transform Inner_1;
        public Transform Hor_1;
        public Transform Ver_1;

        public Transform Con_2;
        public Transform Con_inv_2;
        public Transform Inner_2;
        public Transform Hor_2;
        public Transform Ver_2;

        public Transform Con_3;
        public Transform Con_inv_3;
        public Transform Inner_3;
        public Transform Hor_3;
        public Transform Ver_3;

        public Transform Con_4;
        public Transform Con_inv_4;
        public Transform Inner_4;
        public Transform Hor_4;
        public Transform Ver_4;

        public override void UpdateData()
        {
            base.UpdateData();

            // 1
            bool top = false;
            bool top_left = false;
            bool top_right = false;
            bool bottom = false;
            bool bottom_left = false;
            bool bottom_right = false;
            bool left = false;
            bool right = false;

            if (GridObjectManager.Instance.GetGridObject(this.GridObject.Data.x, this.GridObject.Data.y + 1) != null)
            {
                if (GridObjectManager.Instance.GetGridObject(this.GridObject.Data.x, this.GridObject.Data.y + 1).Type == GridObjectType.Earth)
                {
                    top = true;
                }
            }
            if (GridObjectManager.Instance.GetGridObject(this.GridObject.Data.x - 1, this.GridObject.Data.y + 1) != null)
            {
                if (GridObjectManager.Instance.GetGridObject(this.GridObject.Data.x - 1, this.GridObject.Data.y + 1).Type == GridObjectType.Earth)
                {
                    top_left = true;
                }
            }
            if (GridObjectManager.Instance.GetGridObject(this.GridObject.Data.x + 1, this.GridObject.Data.y + 1) != null)
            {
                if (GridObjectManager.Instance.GetGridObject(this.GridObject.Data.x + 1, this.GridObject.Data.y + 1).Type == GridObjectType.Earth)
                {
                    top_right = true;
                }
            }
            if (GridObjectManager.Instance.GetGridObject(this.GridObject.Data.x, this.GridObject.Data.y - 1) != null)
            {
                if (GridObjectManager.Instance.GetGridObject(this.GridObject.Data.x, this.GridObject.Data.y - 1).Type == GridObjectType.Earth)
                {
                    bottom = true;
                }
            }
            if (GridObjectManager.Instance.GetGridObject(this.GridObject.Data.x - 1, this.GridObject.Data.y - 1) != null)
            {
                if (GridObjectManager.Instance.GetGridObject(this.GridObject.Data.x - 1, this.GridObject.Data.y - 1).Type == GridObjectType.Earth)
                {
                    bottom_left = true;
                }
            }
            if (GridObjectManager.Instance.GetGridObject(this.GridObject.Data.x + 1, this.GridObject.Data.y - 1) != null)
            {
                if (GridObjectManager.Instance.GetGridObject(this.GridObject.Data.x + 1, this.GridObject.Data.y - 1).Type == GridObjectType.Earth)
                {
                    bottom_right = true;
                }
            }
            if (GridObjectManager.Instance.GetGridObject(this.GridObject.Data.x - 1, this.GridObject.Data.y) != null)
            {
                if (GridObjectManager.Instance.GetGridObject(this.GridObject.Data.x - 1, this.GridObject.Data.y).Type == GridObjectType.Earth)
                {
                    left = true;
                }
            }
            if (GridObjectManager.Instance.GetGridObject(this.GridObject.Data.x + 1, this.GridObject.Data.y) != null)
            {
                if (GridObjectManager.Instance.GetGridObject(this.GridObject.Data.x + 1, this.GridObject.Data.y).Type == GridObjectType.Earth)
                {
                    right = true;
                }
            }

            this.Con_1.gameObject.SetActive(false);
            this.Con_inv_1.gameObject.SetActive(false);
            this.Inner_1.gameObject.SetActive(false);
            this.Hor_1.gameObject.SetActive(false);
            this.Ver_1.gameObject.SetActive(false);

            if(top && right && top_right) this.Inner_1.gameObject.SetActive(true);
            else if(top && right && !top_right) this.Con_inv_1.gameObject.SetActive(true);
            else if(top && !right) this.Ver_1.gameObject.SetActive(true);
            else if(!top && right) this.Hor_1.gameObject.SetActive(true);

            this.Con_2.gameObject.SetActive(false);
            this.Con_inv_2.gameObject.SetActive(false);
            this.Inner_2.gameObject.SetActive(false);
            this.Hor_2.gameObject.SetActive(false);
            this.Ver_2.gameObject.SetActive(false);

            if(top && left && top_left) this.Inner_2.gameObject.SetActive(true);
            else if(top && left && !top_left) this.Con_inv_2.gameObject.SetActive(true);
            else if(top && !left) this.Ver_2.gameObject.SetActive(true);
            else if(!top && left) this.Hor_2.gameObject.SetActive(true);

            this.Con_3.gameObject.SetActive(false);
            this.Con_inv_3.gameObject.SetActive(false);
            this.Inner_3.gameObject.SetActive(false);
            this.Hor_3.gameObject.SetActive(false);
            this.Ver_3.gameObject.SetActive(false);

            if(bottom && left && bottom_left) this.Inner_3.gameObject.SetActive(true);
            else if(bottom && left && !bottom_left) this.Con_inv_3.gameObject.SetActive(true);
            else if(bottom && !left) this.Ver_3.gameObject.SetActive(true);
            else if(!bottom && left) this.Hor_3.gameObject.SetActive(true);

            this.Con_4.gameObject.SetActive(false);
            this.Con_inv_4.gameObject.SetActive(false);
            this.Inner_4.gameObject.SetActive(false);
            this.Hor_4.gameObject.SetActive(false);
            this.Ver_4.gameObject.SetActive(false);

            if(bottom && right && bottom_right) this.Inner_4.gameObject.SetActive(true);
            else if(bottom && right && !bottom_right) this.Con_inv_4.gameObject.SetActive(true);
            else if(bottom && !right) this.Ver_4.gameObject.SetActive(true);
            else if(!bottom && right) this.Hor_4.gameObject.SetActive(true);

        }
    }
}
