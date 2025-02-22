using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeckBuildPresenter : MonoBehaviour
{
    public DeckBuildView View;
    public InGameData InGameModel;
    public CardsDataBase CardModel;

    public void UiGridSet()
    {
        View.DeckUiView
            (
             CardModel.CardDeck,
             CardModel.UiGridSet()
            );
    }
    public void ChangeCard(List<int> Cards)
    {
        CardModel.ChangeCard(Cards);
        View.CardChange(Cards);
    }

    public void UpdateView()
    {

    }
}
