using System;
using UnityEngine;
using static UnityEditor.Progress;
using UnityEngine.SocialPlatforms;

public class BattleUiPresenter : MonoBehaviour
{
    public BattleUiView UiView;
    private Action<Card> UiSetFunc;
    public void Start()
    {
        UiSetFunc = UiSet;
        Local.EventHandler.Register<Card>(EnumType.BattleUI, UiSetFunc);
    }
    public void UiSet(Card card)
    {
        UiView.Name.text = card.name;
        UiView.Panel.TryGetComponent(out Animator animation);
        animation.SetTrigger("Start");
    }
}
