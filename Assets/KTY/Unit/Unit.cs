using System;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[Serializable]
public class States //unit스텟 
{
    public Action DeadFunc;
    public float Lv { get; set; }
    public float MaxHp { get { return MaxHp; } set { MaxHp = value; hp = MaxHp; } }
    public float MaxDefense { get { return MaxDefense; } set { MaxDefense = value; defense = MaxDefense; } }

    [SerializeField] private float power;
    [SerializeField] private float defense;
    [SerializeField] private float speed;
    [SerializeField] private float hp;

    public float Power { get { return power; } set => power += value; }
    public float Defense { get { return defense; } set => Mathf.Clamp(defense += value, 0, MaxDefense); }
    public float Hp { get { return hp; } set { Mathf.Clamp(hp += value, 0, MaxHp); if (hp <= 0) { DeadFunc?.Invoke(); } } }
    public float Speed { get { return speed; } set => speed += value; }


}

public abstract class Unit : MonoBehaviour
{
    [field: SerializeField] public States UnitStates { get; set; }
    [field: SerializeField] public Unit TargetStates { get; set; }
    [field: SerializeField] public Animator animator { get; private set; }
    public Action EndAniFunc { get; set; }
    protected Action<States> StatesSetFunc { get; set; }


    public virtual void Start()
    {
        StatesUiSet();
        UnitStates.DeadFunc = Die;
    }

    public void ToIdle()//애니메이션 종료후 idle상태로 돌리기
    {
        animator.SetTrigger("ToIdle");
    }

    protected abstract void Die();

    public abstract void StatesUiSet();//스텟ui동기화

    protected void StartAction()//애니메이션 끝났을때 실행할 함수
    {
        EndAniFunc?.Invoke();
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
        Unit.EndAniFunc = (() =>
        {
            Unit.TargetStates.UnitStates.Hp = -Unit.UnitStates.Power;
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
        Unit.EndAniFunc = (() =>
        {
            Unit.UnitStates.Defense = States;
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
        Unit.EndAniFunc = (() =>
        {
            Unit.UnitStates.Hp = States;
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
        Unit.EndAniFunc = (() =>
        {
            Unit.TargetStates.UnitStates.Hp = States;
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
