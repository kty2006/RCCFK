using System;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[Serializable]
public class States
{
    [field: SerializeField] public float Lv { get; set; }
    [field: SerializeField] public float Attack { get; set; }
    [field: SerializeField] public float MaxDefense { get; set; }
    [field: SerializeField] public float Defense { get; set; }
    [field: SerializeField] public float MaxHp { get; set; }
    public float hp;
    public float Hp { get { return hp; } set { hp = value; if (hp <= 0) { DeadFunc?.Invoke(); } } }
    //public float Hp { get; set; }
    [field: SerializeField] public float speed { get; set; }

    public Action DeadFunc;
}
public abstract class Unit : MonoBehaviour
{
    public States UnitStates;
    public Unit TargetStates;

    public Action actionType { get; set; }
    [field: SerializeField] public Animator animator { get; private set; }

    protected Action<States> StatesSetFunc { get; set; }
    public virtual void Start()
    {
        StatesUiSet();
        UnitStates.DeadFunc = Die;
    }

    public void ToIdle()
    {
        animator.SetTrigger("ToIdle");
    }

    protected abstract void Die();

    public abstract void StatesUiSet();

    protected void StartAction()
    {
        actionType?.Invoke();
    }

    protected void SetUnitAttack(AbillityWrapper unitBehaviour)
    {
        unitBehaviour.AbillityFunc = new Attack(this).Invoke;
    }

    protected void SetUnitDefense(AbillityWrapper unitBehaviour)
    {
        unitBehaviour.AbillityFunc = new Defense(this, unitBehaviour.AbilityStates).Invoke;
    }

    protected void SetUnitRecovery(AbillityWrapper unitBehaviour)
    {
        unitBehaviour.AbillityFunc = new Recovery(this, unitBehaviour.AbilityStates).Invoke;
    }

    protected void SetUnitBuff(AbillityWrapper unitBehaviour)
    {
        unitBehaviour.AbillityFunc = new Buff(this, unitBehaviour.AbilityStates).Invoke;
    }

    protected void SetUnitSpecial(AbillityWrapper unitBehaviour)
    {
        unitBehaviour.AbillityFunc = new Special(this).Invoke;
    }
}
public class Attack : IAttack
{
    private Unit Unit;
    public Attack(Unit unit)
    {
        Unit = unit;
    }
    public void Invoke()
    {
        Unit.animator.SetTrigger("Attack");
        Unit.actionType = (() =>
        {
            Unit.TargetStates.UnitStates.Hp -= Unit.UnitStates.Attack;
            Unit.TargetStates.animator.SetTrigger("Hit");
            Debug.Log($"공격{Unit.name}");
            Unit.TargetStates.StatesUiSet();
        });
    }
}

public class Defense : IDefense
{
    public Unit Unit;
    public float States;
    public Defense(Unit unit, float states)
    {
        Unit = unit;
        States = states;
    }

    public void Invoke()
    {
        Unit.animator.SetTrigger("Defense");
        Unit.actionType = (() =>
        {
            Unit.UnitStates.Defense += States;
            Unit.StatesUiSet();
            Debug.Log($"방어력회복{Unit.name}");
        });
    }
}

public class Recovery : IRecovery
{
    public Unit Unit;
    public float States;
    public Recovery(Unit unit, float states)
    {
        Unit = unit;
        States = states;
    }


    public void Invoke()
    {
        Unit.animator.SetTrigger("Defense");
        Unit.actionType = (() =>
        {
            Unit.UnitStates.Hp += States;
            Unit.StatesUiSet();
            Debug.Log($"회복{Unit.name}");
        });
    }
}

public class Buff : IBuff
{
    public Unit Unit;
    public float States;
    public Buff(Unit unit, float states)
    {
        Unit = unit;
        States = states;
    }

    public void Invoke()
    {
        Unit.animator.SetTrigger("Buff");
        Unit.actionType = (() =>
        {
            Unit.TargetStates.UnitStates.Hp += States;
            Unit.StatesUiSet();
            Debug.Log($"스텟강화{Unit.name}");
        });
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
