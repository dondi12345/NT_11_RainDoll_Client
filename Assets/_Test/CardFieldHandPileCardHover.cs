using UnityEngine;
using UnityEngine.EventSystems;

namespace Rubik.SpireOfShadows.CardField
{
    public class CardFieldHandPileCardHover : MonoBehaviour
    {
        public int SortingOrder = 1;
        public int MaxSortingOrder = 1;

        public bool IsHovering = false;

        public CardFieldHandPileCardUI CardFieldHandPileCardUI;

        public void HandleOnPointerEnter(PointerEventData eventData)
        {
            IsHovering = true;
            if(this.CardFieldHandPileCardUI.CardFieldHandPileUI.SetCardHover(this.CardFieldHandPileCardUI)){
                IsHovering = false;
                return;
            }
        }

        public void HandleOnPointerExit(PointerEventData eventData)
        {
            this.CardFieldHandPileCardUI.CardFieldHandPileUI.RemoveCardHover();
            IsHovering = false;
        }


        public void SetSortingOrder(int sortingOrder, int maxSortingOrder)
        {
            SortingOrder = sortingOrder;
            MaxSortingOrder = maxSortingOrder;
        }
    }
}

