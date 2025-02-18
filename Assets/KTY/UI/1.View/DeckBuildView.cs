using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEngine.Rendering.GPUSort;

public class DeckBuildView : MonoBehaviour, IPointerClickHandler
{
    public GameObject MyDeckGrid;
    public GameObject RemainingCardGrid;
    public GameObject CardPrefab;
    public DeckBuildPresenter DeckBuildPresenter;
    public void Start()
    {
        DeckBuildPresenter.UiGridSet();
    }

    public void DeckUiView(List<Card> myDeckData, List<Card> remainingCards)
    {
        CardCreate(myDeckData, MyDeckGrid);
        CardCreate(remainingCards, RemainingCardGrid);
    }

    public void CardCreate(List<Card> cardList, GameObject cardGrid)
    {
        GameObject cardObject;
        foreach (Card card in cardList)
        {
            cardObject = Instantiate(CardPrefab, cardGrid.transform);
            cardObject.transform.GetChild(0).GetComponent<Image>().sprite = card.Sprite();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {

    }

}
