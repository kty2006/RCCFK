using System;
using UnityEngine;

public class Player : Unit
{
    public override void Awake()
    {
        base.Awake();
        Local.EventHandler.Register<AbillityWrapper>(EnumType.PlayerAttack, (unit) => { SetUnitAttack(unit); });
        Local.EventHandler.Register<AbillityWrapper>(EnumType.PlayerDefense, (unit) => { SetUnitDefense(unit); });
        Local.EventHandler.Register<AbillityWrapper>(EnumType.PlayerRecovery, (unit) => { SetUnitRecovery(unit); });
        Local.EventHandler.Register<AbillityWrapper>(EnumType.PlayerBuff, (unit) => { SetUnitBuff(unit); });
        Local.EventHandler.Register<AbillityWrapper>(EnumType.PlayerSpecial, (unit) => { SetUnitSpecial(unit); });
    }

    
    public override void Die()
    {
        throw new NotImplementedException();
    }
}

