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
    public override void Die()
    {
        throw new NotImplementedException();
    }
}

public class EnemySkill : IAttack, IEvent
{
    public void Execute()
    {
        throw new NotImplementedException();
    }

    public virtual void Invoke()
    {
        Debug.Log("Àû°ø°Ý");
    }
}
