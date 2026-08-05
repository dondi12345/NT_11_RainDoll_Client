using UnityEngine;

namespace NT.RainDoll.Ground
{
    public class GridObjectModelTileGround : GridObjectModel
    {
        public SpriteRenderer Sprite_1;
        public SpriteRenderer Sprite_2;
        public SpriteRenderer Sprite_3;
        public SpriteRenderer Sprite_4;


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

                if (top && right && top_right) this.Sprite_1.sprite = GridObjectModelResources.Instance.GridObjectModelSprites[4];
                else if (top && right && !top_right) this.Sprite_1.sprite = GridObjectModelResources.Instance.GridObjectModelSprites[1];
                else if (top && !right) this.Sprite_1.sprite = GridObjectModelResources.Instance.GridObjectModelSprites[2];
                else if (!top && right) this.Sprite_1.sprite = GridObjectModelResources.Instance.GridObjectModelSprites[3];
                else this.Sprite_1.sprite = GridObjectModelResources.Instance.GridObjectModelSprites[0];

                if (top && left && top_left) this.Sprite_2.sprite = GridObjectModelResources.Instance.GridObjectModelSprites[4];
                else if (top && left && !top_left) this.Sprite_2.sprite = GridObjectModelResources.Instance.GridObjectModelSprites[1];
                else if (top && !left) this.Sprite_2.sprite = GridObjectModelResources.Instance.GridObjectModelSprites[2];
                else if (!top && left) this.Sprite_2.sprite = GridObjectModelResources.Instance.GridObjectModelSprites[3];
                else this.Sprite_2.sprite = GridObjectModelResources.Instance.GridObjectModelSprites[0];

                if (bottom && left && bottom_left) this.Sprite_3.sprite = GridObjectModelResources.Instance.GridObjectModelSprites[4];
                else if (bottom && left && !bottom_left) this.Sprite_3.sprite = GridObjectModelResources.Instance.GridObjectModelSprites[1];
                else if (bottom && !left) this.Sprite_3.sprite = GridObjectModelResources.Instance.GridObjectModelSprites[2];
                else if (!bottom && left) this.Sprite_3.sprite = GridObjectModelResources.Instance.GridObjectModelSprites[3];
                else this.Sprite_3.sprite = GridObjectModelResources.Instance.GridObjectModelSprites[0];

                if (bottom && right && bottom_right) this.Sprite_4.sprite = GridObjectModelResources.Instance.GridObjectModelSprites[4];
                else if (bottom && right && !bottom_right) this.Sprite_4.sprite = GridObjectModelResources.Instance.GridObjectModelSprites[1];
                else if (bottom && !right) this.Sprite_4.sprite = GridObjectModelResources.Instance.GridObjectModelSprites[2];
                else if (!bottom && right) this.Sprite_4.sprite = GridObjectModelResources.Instance.GridObjectModelSprites[3];
                else this.Sprite_4.sprite = GridObjectModelResources.Instance.GridObjectModelSprites[0];
        }
    }
}
