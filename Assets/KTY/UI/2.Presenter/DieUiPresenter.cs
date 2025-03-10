using System;
using UnityEngine;

public class DieUiPresenter : MonoBehaviour
{
    public DieUiView DieUiView;
    public Action<int> StageFunc;

    public void Awake()
    {
        Subscribe();
        StageFunc?.Invoke(1);
    }

    public void Subscribe()
    {
        StageFunc = DieUiView.StageSet;
        Local.EventHandler.Register<int>(EnumType.InformationUi, StageFunc);
        Local.EventHandler.Register<int>(EnumType.PlayerStatesUi, StageFunc);
    }
}
