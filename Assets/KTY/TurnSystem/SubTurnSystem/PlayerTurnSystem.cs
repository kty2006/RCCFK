using System;
using UnityEngine;

[DefaultExecutionOrder(-1)]
public class PlayerTurnSystem : SubTurnSystem
{
    public void Awake()
    {
        base.Start();
        Local.EventHandler.Register<Turn>(EnumType.PlayerTurnSystem, (turn) => { Local.TurnSystem.Register(this); });
        Local.EventHandler.Register<Action>(EnumType.PlayerTurnAdd, (UBclass) => { this.Register(UBclass);  });
        Local.EventHandler.Register<Action>(EnumType.PlayerTurnRemove, (UBclass) => { this.UnRegister(UBclass); });
        //Local.EventHandler.Register<Defense>(Type.PlayerDefense, (De) => { this.Register(new Defense().Invoke); });
    }
}
