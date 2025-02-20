using System;
using UnityEngine;

public enum EnemyType { SmallBlind = 1, BigBline, BossBlind }
public class Enemy : Unit
{
    public void Awake()
    {
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

    protected override void Die()
    {
        animator.SetTrigger("Die");

    }

    public void DieInside()
    {
        Local.EventHandler.Invoke<UnitDead>(EnumType.EnemyDie, UnitDead.UnitDead);
        Destroy(gameObject.transform.parent.gameObject);
    }
}
