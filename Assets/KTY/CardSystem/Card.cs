using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum CardType { Attack, Defense, Recovery, Buff, Special }
public enum CardState { None, Drow }
[System.Serializable]
public class Card
{
    //특수카드같은경우 특수능력 실행 클래스를 만들고  종류를 enum으로 만들어 특수카드 능력 실행시 Card클래스에 상태만 특수능력 클래스 함수에 넘겨주어 타입에 따라 능력실행되게 
    private Image image;//이미지
    private CardType type;//타입
    private CardState state = CardState.None;
    public string name { get; set; }
    public AbillityWrapper Ability { get; private set; }//추가능력

    public Card(Image image, CardType type, AbillityWrapper ability, string name)
    {
        this.image = image;
        this.type = type;
        this.Ability = ability;
        this.name = name;
    }

    public Image Image()
    {
        return image;
    }

    public CardType Type()
    {
        return type;
    }


    public CardState State()
    {
        return state = (state == CardState.None) ? CardState.Drow : CardState.None;
    }
}

public class CardBuild
{
    private Image image;//이미지
    private CardType type;//타입
    private AbillityWrapper ability = new();//추가능력
    private string name;
    public CardBuild Image(Image image)
    {
        this.image = image;
        return this;
    }



    public CardBuild Type(int number)
    {
        switch (number)
        {
            case <= 12:
                type = CardType.Attack;
                Local.EventHandler.Invoke<AbillityWrapper>(EnumType.PlayerAttack, ability);
                break;
            case <= 25:
                type = CardType.Defense;
                ability.AbilityStates = number;
                Local.EventHandler.Invoke<AbillityWrapper>(EnumType.PlayerDefense, ability);
                break;
            case <= 38:
                type = CardType.Recovery;
                ability.AbilityStates = number;
                Local.EventHandler.Invoke<AbillityWrapper>(EnumType.PlayerRecovery, ability);
                break;
            case <= 51:
                type = CardType.Buff;
                ability.AbilityStates = number;
                Local.EventHandler.Invoke<AbillityWrapper>(EnumType.PlayerBuff, ability);
                break;
        }
        return this;
    }

    public Card Build()
    {
        return new Card(image, type, ability, name);
    }
}
