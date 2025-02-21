using UnityEngine;
using Cysharp.Threading.Tasks;


[DefaultExecutionOrder(1)]
public class EnemyTurnSystem : SubTurnSystem
{
    public void Awake()
    {
        base.Start();
        Local.EventHandler.Register<Turn>(EnumType.EnemyTurnSystem, (turn) => { Local.TurnSystem.Register(this); });
        Local.EventHandler.Register<AbillityWrapper>(EnumType.EnemyTurnAdd, (UBclass) => { this.Register(UBclass.AbillityFunc.Invoke); });
        Local.EventHandler.Register<AbillityWrapper>(EnumType.EnemyTurnRemove, (UBclass) => { this.UnRegister(UBclass.AbillityFunc.Invoke); });
    }
}
