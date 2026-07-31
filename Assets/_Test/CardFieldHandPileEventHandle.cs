using UnityEngine;
using UnityEngine.EventSystems;

namespace Rubik.SpireOfShadows.CardField
{
    public class CardFieldHandPileEventHandle : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public CardFieldHandPileCardUI CardFieldHandPileCardUI;

        public void OnPointerEnter(PointerEventData eventData)
        {
            this.CardFieldHandPileCardUI.CardFieldHandPileCardHover.HandleOnPointerEnter(eventData);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            this.CardFieldHandPileCardUI.CardFieldHandPileCardHover.HandleOnPointerExit(eventData);
        }
    }
}
