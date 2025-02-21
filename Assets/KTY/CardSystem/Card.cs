using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum CardType { Attack, Defense, Recovery, Buff, TargetTurnReove, CardDrowUp, TargetPowerDown, TargetDefenseDown }
public enum CardState { None, Drow }
[System.Serializable]
public class Card
{
    [field: SerializeField] public Sprite sprite { get; set; }//이미지
    [field: SerializeField] public CardType type { get; set; }//타입
    [field: SerializeField] public int cost { get; set; }//타입
    [field: SerializeField] public AbillityWrapper Ability { get; set; }//추가능력

    public Card(Sprite image, CardType type, AbillityWrapper ability)
    {
        this.sprite = image;
        this.type = type;
        this.Ability = ability;

    }

    public Sprite Sprite()
    {
        return sprite;
    }

    public CardType Type()
    {
        return type;
    }

    public void SelectAction()
    {
        Ability.type = type;
        switch (type)
        {
            case CardType.Attack:
                Local.EventHandler.Invoke<AbillityWrapper>(EnumType.PlayerAttack, Ability);
                break;
            case CardType.Defense:
                Local.EventHandler.Invoke<AbillityWrapper>(EnumType.PlayerDefense, Ability);
                break;
            case CardType.Recovery:
                Local.EventHandler.Invoke<AbillityWrapper>(EnumType.PlayerRecovery, Ability);
                break;
            case CardType.Buff:
                Local.EventHandler.Invoke<AbillityWrapper>(EnumType.PlayerBuff, Ability);
                break;
            case CardType.CardDrowUp:
                Local.EventHandler.Invoke<AbillityWrapper>(EnumType.PlayerSpecial, Ability);
                break;
            case CardType.TargetTurnReove:
                Local.EventHandler.Invoke<AbillityWrapper>(EnumType.PlayerSpecial, Ability);
                break;
            case CardType.TargetDefenseDown:
                Local.EventHandler.Invoke<AbillityWrapper>(EnumType.PlayerSpecial, Ability);
                break;
            case CardType.TargetPowerDown:
                Local.EventHandler.Invoke<AbillityWrapper>(EnumType.PlayerSpecial, Ability);
                break;
        }
    }

}
