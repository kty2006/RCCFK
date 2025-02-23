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
        if (CardModel.ChangeCard(Cards))
        {
            View.CardChange(CardModel.CardDeck.Count - 1);
        }
        Local.EventHandler.Invoke<DataSave>(EnumType.SaveData, Local.DataSave);
    }

    public void CardRelease(GameObject releaseCard)
    {
        View.CardRelease(releaseCard, CardModel.CardDeck.Count);
        Local.EventHandler.Invoke<DataSave>(EnumType.SaveData, Local.DataSave);
    }

    public void CardRelease(List<int> Cards)
    {
        CardModel.CardRelease(Cards);
        Local.EventHandler.Invoke<DataSave>(EnumType.SaveData, Local.DataSave);
    }
    public void UpdateView()
    {

    }
}
