using System;
using UnityEditor;
using UnityEngine;

public class Player : Unit
{
    public void Awake()
    {
        Local.EventHandler.Register<AbillityWrapper>(EnumType.PlayerAttack, (unit) => { SetUnitAttack(unit); });
        Local.EventHandler.Register<AbillityWrapper>(EnumType.PlayerDefense, (unit) => { SetUnitDefense(unit); });
        Local.EventHandler.Register<AbillityWrapper>(EnumType.PlayerRecovery, (unit) => { SetUnitRecovery(unit); });
        Local.EventHandler.Register<AbillityWrapper>(EnumType.PlayerBuff, (unit) => { SetUnitBuff(unit); });
        Local.EventHandler.Register<AbillityWrapper>(EnumType.PlayerSpecial, (unit) => { SetUnitSpecial(unit); });
        Local.EventHandler.Register<UnitDead>(EnumType.EnemyDie, (unitDead) => { UnitStates.Cost = UnitStates.MaxCost; StatesUiSet(); });
        Local.EventHandler.Register<ResetCost>(EnumType.ResetCost, (reset) => { UnitStates.Cost = UnitStates.MaxCost; StatesUiSet(); });
    }

    public override void StatesUiSet()
    {
        Local.EventHandler.Invoke<States>(EnumType.PlayerStatesUi, UnitStates);
    }

    protected override void Die()
    {
        Debug.Log("플레이어죽음");
    }
}

