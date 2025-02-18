using NUnit.Framework;
using UnityEngine;

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


    public void UpdateView()
    {

    }
}
