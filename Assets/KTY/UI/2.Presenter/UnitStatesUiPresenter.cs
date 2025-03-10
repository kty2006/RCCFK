using System;
using UnityEngine;

public class UnitStatesUiPresenter : MonoBehaviour
{
    public UnitStatesUiView UnitStatesUiView;
    public Action<States> PlayerFunc;
    public Action<States> EnemyFunc;
    public Action<States> CostFunc;
    public Action<States> LvFunc;

    public void Awake()
    {
        Subscribe();
    }

    public void Subscribe()
    {
        PlayerFunc = UnitStatesUiView.PlayerUiSet;
        EnemyFunc = UnitStatesUiView.EnemyUiSet;
        CostFunc = UnitStatesUiView.CostSet;
        LvFunc = UnitStatesUiView.LvSet;
        Local.EventHandler.Register<States>(EnumType.PlayerStatesUi, PlayerFunc);
        Local.EventHandler.Register<States>(EnumType.PlayerStatesUi, CostFunc);
        Local.EventHandler.Register<States>(EnumType.PlayerStatesUi, LvFunc);
        Local.EventHandler.Register<States>(EnumType.EnemyStatesUi, EnemyFunc);
    }
}
