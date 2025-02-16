using System;
using UnityEngine;

[Serializable]
public struct States
{
    public float Lv;
    public float Attack { get; set; }
    public float Defense { get; set; }
    public float Hp { get; set; }
    [field: SerializeField] public float speed { get; set; }
}
public abstract class Unit : MonoBehaviour
{
    [field: SerializeField] public States UnitStates { get; private set; }
    [field: SerializeField] public Animator animator { get; private set; }

    public Action actionType { get; set; }
    public virtual void Awake()
    {

    }

    public void ToIdle()
    {
        animator.SetTrigger("ToIdle");
    }

    public abstract void Die();

    public void StartAction()
    {
        actionType?.Invoke();
    }

    public void SetUnitAttack(AbillityWrapper unitBehaviour)
    {
        unitBehaviour.AbillityFunc += new Attack(this).Invoke;
    }

    public void SetUnitDefense(AbillityWrapper unitBehaviour)
    {
        unitBehaviour.AbillityFunc = new Defense(this).Invoke;
    }

    public void SetUnitRecovery(AbillityWrapper unitBehaviour)
    {
        unitBehaviour.AbillityFunc = new Recovery(this).Invoke;
    }

    public void SetUnitBuff(AbillityWrapper unitBehaviour)
    {
        unitBehaviour.AbillityFunc = new Buff(this).Invoke;
    }

    public void SetUnitSpecial(AbillityWrapper unitBehaviour)
    {
        unitBehaviour.AbillityFunc = new Special(this).Invoke;
    }
}
public class Attack : IAttack
{
    public Unit Unit;

    public Attack(Unit unit)
    {
        Unit = unit;
    }
    public void Invoke()
    {

        Unit.animator.SetTrigger("Attack");
        Unit.actionType = (() => { Debug.Log($"공격{Unit.name}"); });
    }

    public void Atting()
    {
        Debug.Log($"공격{Unit.name}");
    }
}

public class Defense : IDefense, IEvent
{
    public Unit Unit;

    public Defense(Unit unit)
    {
        Unit = unit;
    }

    public void Execute()
    {
        throw new NotImplementedException();
    }

    public void Invoke()
    {
        Debug.Log($"방어{Unit.name}");
    }
}

public class Recovery : IRecovery, IEvent
{
    public Unit Unit;

    public Recovery(Unit unit)
    {
        Unit = unit;
    }

    public void Execute()
    {
        throw new NotImplementedException();
    }

    public void Invoke()
    {
        Debug.Log($"회복{Unit.name}");
    }
}

public class Buff : IBuff, IEvent
{
    public Unit Unit;

    public Buff(Unit unit)
    {
        Unit = unit;
    }

    public void Execute()
    {
        throw new NotImplementedException();
    }

    public void Invoke()
    {
        Debug.Log($"스텟강화{Unit.name}");
    }
}

public class Special : ISpecial, IEvent
{
    public Unit Unit;

    public Special(Unit unit)
    {
        Unit = unit;
    }

    public void Execute()
    {
        throw new NotImplementedException();
    }

    public void Invoke()
    {
        Debug.Log($"특수행동{Unit.name}");
    }
}
