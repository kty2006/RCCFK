using System;
using UnityEngine;

public class DieUiPresenter : MonoBehaviour
{
    public DieUiView DieUiView;
    public Action<States> StageFunc;

    public void Awake()
    {
        Subscribe();
    }

    public void Subscribe()
    {
        StageFunc = DieUiView.StageSet;
        Local.EventHandler.Register<States>(EnumType.DieUi, StageFunc);
    }
}
