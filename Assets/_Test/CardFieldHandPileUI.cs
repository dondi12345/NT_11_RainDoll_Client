using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using NTPackage.Functions;
using NTPackage.UI;
using UnityEngine;

namespace Rubik.SpireOfShadows.CardField
{
    public class CardFieldHandPileUI : PopupUI
    {
        public CardFieldHandPileCardUI CardPrefab;
        public List<CardFieldHandPileCardUI> CardList = new List<CardFieldHandPileCardUI>();

        public RectTransform DrawPileContainer;
        public RectTransform DiscardPileContainer;
        public Transform HandPileContainer;

        public Transform HandPileSelectPos;
        public Transform HandPilePos;

        public Transform CardHoverPos;
        public Transform CardSelectPos;

        public Transform Left;
        public Transform Right;

        public Transform LeftCard;
        public Transform RightCard;

        public CardFieldHandPileCardUI CardHover;
        public CardFieldHandPileCardUI CardSelected;

        public List<(int, float, float)> _fanSettings = new List<(int, float, float)>{
            (2, 4f, 600f),
            (3, 6f, 650f),
            (4, 8f, 700f),
            (5, 10f, 750f),
            (6, 12f, 800f),
            (7, 14f, 850f),
            (8, 16f, 900f),
            (9, 18f, 950f),
            (10, 20f, 1000f),
        };

        public float xHoverRight = 90f;
        public float xHoverLeft = 90f;



        private readonly List<CardFieldHandPileCardUI> _spawnedCards = new List<CardFieldHandPileCardUI>();

        [NTButton]
        public void Initialize()
        {
        }

        public IEnumerator RecycleDiscardToDraw(int amount)
        {
            Debug.Log("TO DO: effect recycle discard to draw");
            yield return new WaitForSeconds(0.1f);
        }

        public bool SetCardHover(CardFieldHandPileCardUI cardFieldHandPileCardUI)
        {
            if (CardHover != null) return false;
            if (cardFieldHandPileCardUI == CardHover)
            {
                return false;
            }
            CardHover = cardFieldHandPileCardUI;
            if (this.CardHover != null && this.CardSelected == null) this.CardHover.transform.SetParent(this.CardHoverPos);
            SortHandPile();
            return true;
        }

        public void RemoveCardHover()
        {
            if (this.CardHover != null && this.CardSelected == null) this.CardHover.transform.SetParent(this.HandPileContainer);
            CardHover = null;
            SortHandPile();
        }

        public void SetCardSelected(CardFieldHandPileCardUI cardFieldHandPileCardUI)
        {
            CardSelected = cardFieldHandPileCardUI;
            this.CardSelected.transform.SetParent(this.CardSelectPos);
            this.HandPileContainer.DOKill();
            this.HandPileContainer.DOMove(this.HandPileSelectPos.position, 0.2f).SetEase(Ease.OutQuad);
            this.CardSelected.DOKill();
            this.CardSelected.GetComponent<RectTransform>().DOAnchorPosY(0f, 0.2f).SetEase(Ease.OutQuad);
        }

        public void RemoveCardSelected()
        {
            if (this.CardSelected != null) this.CardSelected.transform.SetParent(this.HandPileContainer);
            CardSelected = null;
            this.HandPileContainer.DOKill();
            this.HandPileContainer.DOMove(this.HandPilePos.position, 0.2f).SetEase(Ease.OutQuad);
            SortHandPile();
        }

        [NTButton]
        public void SortHandPile()
        {
            SortHandPile_v3();
            return;
            if (CardSelected != null) return;
            int count = CardList.Count;

            if (count == 0)
                return;

            if (count == 1)
            {
                CardFieldHandPileCardUI card = CardList[0];
                card.transform.DOKill();
                card.GetComponent<RectTransform>().DOAnchorPos(new Vector2(0f, this.CardHoverPos.position.y), 0.2f).SetEase(Ease.OutQuad);
                card.transform.DOLocalRotateQuaternion(Quaternion.identity, 0.2f).SetEase(Ease.OutQuad);
                return;
            }

            float angle_cf = this._fanSettings[this._fanSettings.Count - 1].Item2;
            float radius_cf = this._fanSettings[this._fanSettings.Count - 1].Item3;

            for (int i = 0; i < _fanSettings.Count; i++)
            {
                if (count < _fanSettings[i].Item1)
                {
                    angle_cf = this._fanSettings[i - 1].Item2;
                    radius_cf = this._fanSettings[i - 1].Item3;
                    break;
                }
            }

            float centerIndex = (count - 1) / 2f;

            bool isTrigHover = false;

            for (int i = 0; i < count; i++)
            {
                CardFieldHandPileCardUI card = CardList[i];
                card.transform.SetAsLastSibling();
                card.SetCanvasSortingOrder(i, count);

                float normalized = (i - centerIndex) / centerIndex; // -1 -> 1

                float angle = normalized * angle_cf;

                float rad = angle * Mathf.Deg2Rad;

                float x = Mathf.Sin(rad) * radius_cf;
                float y = Mathf.Cos(rad) * radius_cf - radius_cf;

                if (card.CardFieldHandPileCardHover.IsHovering && !isTrigHover)
                {
                    isTrigHover = true;
                    card.SetCanvasSortingOrder(count, count);
                    card.transform.DOKill();
                    card.GetComponent<RectTransform>().DOAnchorPos(new Vector2(x, this.CardHoverPos.position.y), 0.2f).SetEase(Ease.OutQuad);
                    card.transform.DOLocalRotate(new Vector3(0f, 0f, 0f), 0.2f).SetEase(Ease.OutQuad);
                    card.transform.DOScale(Vector3.one * 1.1f, 0.2f).SetEase(Ease.OutQuad);
                }
                else
                {
                    if (this.CardHover != null)
                    {
                        if (isTrigHover)
                        {
                            x += xHoverRight;
                        }
                        else
                        {
                            x -= xHoverLeft;
                        }
                    }
                    card.transform.DOKill();
                    card.GetComponent<RectTransform>().DOAnchorPos(new Vector2(x, y), 0.2f).SetEase(Ease.OutQuad);
                    card.transform.DOLocalRotate(new Vector3(0f, 0f, -angle), 0.2f).SetEase(Ease.OutQuad);
                    card.transform.DOScale(Vector3.one, 0.2f).SetEase(Ease.OutQuad);
                }

            }
        }

        public void SortHandPile_v2()
        {
            if (CardSelected != null) return;
            int count = CardList.Count;

            if (count == 0)
                return;

            if (count == 1)
            {
                CardFieldHandPileCardUI card = CardList[0];
                card.transform.DOKill();
                card.GetComponent<RectTransform>().DOAnchorPos(new Vector2(0f, this.CardHoverPos.position.y), 0.2f).SetEase(Ease.OutQuad);
                card.transform.DOLocalRotateQuaternion(Quaternion.identity, 0.2f).SetEase(Ease.OutQuad);
                return;
            }

            float angle_cf = this._fanSettings[this._fanSettings.Count - 1].Item2;
            float radius_cf = this._fanSettings[this._fanSettings.Count - 1].Item3;

            for (int i = 0; i < _fanSettings.Count; i++)
            {
                if (count < _fanSettings[i].Item1)
                {
                    angle_cf = this._fanSettings[i - 1].Item2;
                    radius_cf = this._fanSettings[i - 1].Item3;
                    break;
                }
            }

            float centerIndex = (count - 1) / 2f;

            bool isTrigHover = false;

            float indexHover = -1;
            for (int i = 0; i < count; i++)
            {
                if (CardList[i].CardFieldHandPileCardHover.IsHovering)
                {
                    indexHover = i;
                    break;
                }
            }

            for (int i = 0; i < count; i++)
            {
                CardFieldHandPileCardUI card = CardList[i];
                card.transform.SetAsLastSibling();
                card.SetCanvasSortingOrder(i, count);

                float normalized = (i - centerIndex) / centerIndex; // -1 -> 1

                float angle = normalized * angle_cf;

                float rad = angle * Mathf.Deg2Rad;

                if (indexHover <= -1) indexHover = centerIndex;
                float angleHover = 0;
                if (i < indexHover) angleHover = (i - indexHover) / (indexHover + 0.1f) * angle_cf;
                else angleHover = (i - indexHover) / (count - indexHover) * angle_cf;

                float x = Mathf.Sin(rad) * radius_cf;
                float y = Mathf.Cos(rad) * radius_cf - radius_cf;

                if (card.CardFieldHandPileCardHover.IsHovering && !isTrigHover)
                {
                    isTrigHover = true;
                    card.SetCanvasSortingOrder(count, count);
                    card.transform.DOKill();
                    card.GetComponent<RectTransform>().DOAnchorPos(new Vector2(x, this.CardHoverPos.position.y), 0.2f).SetEase(Ease.OutQuad);
                    card.transform.DOLocalRotate(new Vector3(0f, 0f, 0f), 0.2f).SetEase(Ease.OutQuad);
                    card.transform.DOScale(Vector3.one * 1.1f, 0.2f).SetEase(Ease.OutQuad);
                }
                else
                {
                    if (this.CardHover != null)
                    {
                        if (i > indexHover)
                        {
                            x += xHoverRight;
                        }
                        else if (i < indexHover)
                        {
                            x -= xHoverLeft;
                        }
                    }
                    card.transform.DOKill();
                    card.GetComponent<RectTransform>().DOAnchorPos(new Vector2(x, y), 0.2f).SetEase(Ease.OutQuad);
                    card.transform.DOLocalRotate(new Vector3(0f, 0f, -angleHover), 0.2f).SetEase(Ease.OutQuad);
                    card.transform.DOScale(Vector3.one, 0.2f).SetEase(Ease.OutQuad);
                }

            }
        }

        public void SortHandPile_v3()
        {
            int count = CardList.Count;
            if (count == 0)
                return;
            Vector2 leftPos = Left.position;
            Vector2 rightPos = Right.position;
            Vector2 centerPos = (leftPos + rightPos) / 2;
            int indexHover = -1;
            for (int i = 0; i < CardList.Count; i++)
            {
                if (CardList[i].CardFieldHandPileCardHover.IsHovering)
                {
                    indexHover = i;
                    break;
                }
            }

            if (count == 1)
            {
                float x = centerPos.x;
                float y = this.HandPileContainer.position.y;
                CardFieldHandPileCardUI card = CardList[0];
                card.transform.DOKill();
                card.transform.DOMove(new Vector2(x, y), 0.2f).SetEase(Ease.OutQuad);
                card.transform.DOLocalRotate(new Vector3(0f, 0f, 0), 0.2f).SetEase(Ease.OutQuad);
                if (indexHover == 0)
                {
                    card.transform.DOScale(Vector3.one * 1.1f, 0.2f).SetEase(Ease.OutQuad);
                }
                else
                {
                    card.transform.DOScale(Vector3.one, 0.2f).SetEase(Ease.OutQuad);

                }
                return;
            }
            float x_space = (rightPos.x - leftPos.x) / (CardList.Count - 1);
            if (x_space > (this.RightCard.position.x - this.LeftCard.position.x) * 3 / 4)
            {
                x_space = (this.RightCard.position.x - this.LeftCard.position.x) * 3 / 4;
            }
            Debug.Log("x_space: " + x_space);
            float centerIndex = ((float)CardList.Count - 1) / 2;
            float angle_all = 20f;
            float angle_space = angle_all / (CardList.Count - 1);
            float y_all = this.HandPileContainer.position.y - this.Left.position.y;
            float y_space = y_all / ((float)CardList.Count - 1);
            float space = (this.RightCard.position.x - this.LeftCard.position.x) - x_space;
            if (space < 0) space = 0;
            float space_left = space * indexHover / (CardList.Count - 1);
            space_left = 0;
            float space_right = space - space_left;
            Debug.Log("space_left: " + space_left + " space_right: " + space_right);
            for (int i = 0; i < CardList.Count; i++)
            {
                CardFieldHandPileCardUI card = CardList[i];
                card.transform.SetAsLastSibling();
                card.SetCanvasSortingOrder(i, CardList.Count);
                float x = centerPos.x + (i - centerIndex) * x_space;
                float y = this.HandPileContainer.position.y - (Mathf.Abs(i - centerIndex)) * y_space;
                float angle = (i - centerIndex) * -angle_space;
                if (indexHover >= 0)
                {
                    if (i <= indexHover)
                    {
                        // x -= space_left;
                    }
                    else
                    {

                        x += space_right;
                    }
                }
                if (card.CardFieldHandPileCardHover.IsHovering)
                {
                    y = this.CardHoverPos.position.y;
                    angle = 0;
                }
                card.transform.DOKill();
                card.transform.DOMove(new Vector2(x, y), 0.2f).SetEase(Ease.OutQuad);
                card.transform.DOLocalRotate(new Vector3(0f, 0f, angle), 0.2f).SetEase(Ease.OutQuad);
                if(indexHover == i){
                    card.transform.DOScale(Vector3.one * 1.1f, 0.2f).SetEase(Ease.OutQuad);
                }else{
                    card.transform.DOScale(Vector3.one, 0.2f).SetEase(Ease.OutQuad);
                }
            }
        }
    }
}
