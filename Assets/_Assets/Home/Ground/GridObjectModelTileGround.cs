using System.Collections.Generic;
using UnityEngine;

namespace NT.RainDoll.Ground
{
    public class GridObjectModelTileGround : GridObjectModel
    {
        public SpriteRenderer Sprite_1;
        public SpriteRenderer Sprite_2;
        public SpriteRenderer Sprite_3;
        public SpriteRenderer Sprite_4;

        public Sprite Conner;
        public Sprite Corner_Inv;
        public Sprite Hor;
        public Sprite Ver;
        public Sprite Inner;


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

                if (top && right && top_right) this.Sprite_1.sprite = Inner;
                else if (top && right && !top_right) this.Sprite_1.sprite = Corner_Inv;
                else if (top && !right) this.Sprite_1.sprite = Ver;
                else if (!top && right) this.Sprite_1.sprite = Hor;
                else this.Sprite_1.sprite = Conner;

                if (top && left && top_left) this.Sprite_2.sprite = Inner;
                else if (top && left && !top_left) this.Sprite_2.sprite = Corner_Inv;
                else if (top && !left) this.Sprite_2.sprite = Ver;
                else if (!top && left) this.Sprite_2.sprite = Hor;
                else this.Sprite_2.sprite = Conner;

                if (bottom && left && bottom_left) this.Sprite_3.sprite = Inner;
                else if (bottom && left && !bottom_left) this.Sprite_3.sprite = Corner_Inv;
                else if (bottom && !left) this.Sprite_3.sprite = Ver;
                else if (!bottom && left) this.Sprite_3.sprite = Hor;
                else this.Sprite_3.sprite = Conner;

                if (bottom && right && bottom_right) this.Sprite_4.sprite = Inner;
                else if (bottom && right && !bottom_right) this.Sprite_4.sprite = Corner_Inv;
                else if (bottom && !right) this.Sprite_4.sprite = Ver;
                else if (!bottom && right) this.Sprite_4.sprite = Hor;
                else this.Sprite_4.sprite = Conner;
        }
    }
}
