using UnityEngine;
using Cysharp.Threading.Tasks;
using System;


[DefaultExecutionOrder(1)]
public class EnemyTurnSystem : SubTurnSystem
{
    public void Awake()
    {
        base.Start();
        Local.EventHandler.Register<Turn>(EnumType.EnemyTurnSystem, (turn) => { Local.TurnSystem.Register(this); });
        Local.EventHandler.Register<Action>(EnumType.EnemyTurnAdd, (UBclass) => { this.Register(UBclass); });
        Local.EventHandler.Register<Action>(EnumType.EnemyTurnRemove, (UBclass) => { this.UnRegister(UBclass); });
    }
}
