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
    private List<int> cards = new();
    private List<GameObject> cardsObj = new();
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

    public void CardChange(List<int> cardList)
    {
        Sprite saveCard = cardsObj[0].transform.GetChild(0).GetComponent<Image>().sprite;
        cardsObj[0].transform.GetChild(0).GetComponent<Image>().sprite = cardsObj[1].transform.GetChild(0).GetComponent<Image>().sprite;
        cardsObj[1].transform.GetChild(0).GetComponent<Image>().sprite = saveCard;
        cards.Clear();
        cardsObj.Clear();

    }


    public void FindCard(Image card, GameObject ParentObj)
    {
        Image[] myDecks = ParentObj.GetComponentsInChildren<Image>();
        Debug.Log(myDecks.Length);
        for (int i = 0; i < myDecks.Length; i++)
        {
            if (myDecks[i] == card)
            {
                cards.Add(i / 2);
                break;
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        GameObject clickedObject = eventData.pointerCurrentRaycast.gameObject;

        if (clickedObject.TryGetComponent(out Image image) && clickedObject.transform.CompareTag("Card"))
        {
            if (cards.Count == 0 && clickedObject.transform.parent.name == RemainingCardGrid.name)
            {
                FindCard(image, RemainingCardGrid);
                cardsObj.Add(clickedObject);
            }
            else if (cards.Count == 1 && clickedObject.transform.parent.name == MyDeckGrid.name)
            {
                FindCard(image, MyDeckGrid);
                cardsObj.Add(clickedObject);
                DeckBuildPresenter.ChangeCard(cards);
            }

        }
    }


}
