using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

namespace Rubik.SpireOfShadows.CardField
{
    public class CardFieldHandPileCardUI : MonoBehaviour
    {
        public CardFieldHandPileUI CardFieldHandPileUI;
        public CardFieldHandPileEventHandle CardFieldHandPileEventHandle;
        public CardFieldHandPileCardHover CardFieldHandPileCardHover;
        public Transform ContentTransform;

        public void SetCanvasSortingOrder(int sortingOrder, int maxSortingOrder)
        {
            this.CardFieldHandPileCardHover.SetSortingOrder(sortingOrder, maxSortingOrder);
        }

    }
}
