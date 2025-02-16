using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "CardsDataBase", menuName = "Scriptable Objects/CardsDataBase")]
public class CardsDataBase : ScriptableObject
{
    public Sprite[] CardBacks; //Ä¸½¶È­**
    public Sprite[] PlayingCards;//Ä¸½¶È­**

    public string CardsImageName;
    public string CardBackImageName;

    [ContextMenu("GetCards")]
    public void GetCardsData()
    {
        PlayingCards = Resources.LoadAll<Sprite>(CardsImageName);
    }

    [ContextMenu("GetCardBacks")]
    public void GetCardBacksData()
    {
        CardBacks = Resources.LoadAll<Sprite>(CardBackImageName);
    }


}