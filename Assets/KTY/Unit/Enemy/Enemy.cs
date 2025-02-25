using System;
using UnityEngine;

public enum EnemyType { SmallBlind = 1, BigBline, BossBlind }
public class Enemy : Unit
{
    public void Awake()
    {
        EnemyEnumSet();
        Local.EventHandler.Register<AbillityWrapper>(EnumType.EnemyAttack, (unit) => { SetUnitAttack(unit); });
        Local.EventHandler.Register<AbillityWrapper>(EnumType.EnemyDefense, (unit) => { SetUnitDefense(unit); });
        Local.EventHandler.Register<AbillityWrapper>(EnumType.EnemyRecovery, (unit) => { SetUnitRecovery(unit); });
        Local.EventHandler.Register<AbillityWrapper>(EnumType.EnemyBuff, (unit) => { SetUnitBuff(unit); });
        Local.EventHandler.Register<AbillityWrapper>(EnumType.EnemySpecial, (unit) => { SetUnitSpecial(unit); });
    }

    public override void StatesUiSet()
    {
        Local.EventHandler.Invoke<States>(EnumType.EnemyStatesUi, UnitStates);
    }

    protected override void Die()//수정 필요
    {
        DieInside();
        animator.SetTrigger("Die");
        Local.Stage = 1;
        Local.Gold += 100;

    }

    public void DieInside()
    {
        Local.EventHandler.Invoke<States>(EnumType.EnemyDie, UnitStates);
        Local.EventHandler.Invoke<int>(EnumType.InformationUi,1);
        Local.EventHandler.Invoke<DataSave>(EnumType.SaveData, Local.DataSave);
        Destroy(gameObject.transform.gameObject);
    }

    public void EnemyEnumSet()
    {
        Local.EventHandler.UnRegister(EnumType.EnemyAttack);
        Local.EventHandler.UnRegister(EnumType.EnemyBuff);
        Local.EventHandler.UnRegister(EnumType.EnemyDefense);
        Local.EventHandler.UnRegister(EnumType.EnemyRecovery);
        Local.EventHandler.UnRegister(EnumType.EnemySpecial);
        Local.EventHandler.UnRegister(EnumType.EnemyTurnSystem);
        Local.EventHandler.UnRegister(EnumType.EnemyTurnAdd);
        Local.EventHandler.UnRegister(EnumType.EnemyTurnRemove);
    }
}
