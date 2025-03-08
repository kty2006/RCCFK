using System;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[Serializable]
public class States //unit스텟 
{
    public Action DeadFunc;
    [field: SerializeField] public int SetExp { get; set; }
    [SerializeField] private float lv;
    [SerializeField] private float setpower;
    [SerializeField] private float power;
    [SerializeField] private float speed;
    [SerializeField] private float setdefense;
    [SerializeField] private float defense;
    [SerializeField] private float maxhp;
    [SerializeField] private float hp;
    [SerializeField] private int maxcost;
    [SerializeField] private int cost;
    [SerializeField] private float exp;

    public float Lv { get { return lv; } set { lv += value; MaxHp = 30 * value; SetDefense = 1 * value; Power = 1 * value; Speed = 1 * value; MaxExp += 1; MaxCost = 1; Debug.Log("레벨업"); } }
    public float MaxHp { get { return maxhp; } set { maxhp += value; hp = MaxHp; } }
    public float SetDefense { get { return setdefense; } set { setdefense = value; defense = SetDefense; } }
    public float SetPower { get { return setpower; } set { setpower += value; power = SetPower; } }
    public int MaxCost { get { return maxcost; } set { maxcost += value; cost = maxcost; } }
    public float MaxExp { get; set; } = 10;


    public float Power { get { return power; } set => power += value; }
    public float Defense { get { return defense; } set { defense += value; Debug.Log($"{defense} : {value}"); } }
    public float Hp { get { return hp; } set { hp = Mathf.Clamp(hp += value, 0, MaxHp); if (hp <= 0) { DeadFunc?.Invoke(); } } }
    public float Speed { get { return speed; } set => speed += value; }
    public int Cost { get { return cost; } set { cost = value; } }
    public float Exp { get { return exp; } set { exp += value; Debug.Log($"{exp} : {value}"); while (exp >= MaxExp) { exp -= MaxExp; Lv = 1; } } }
}
[Serializable]
public abstract class Unit : MonoBehaviour
{
    [field: SerializeField] public States UnitStates { get; set; }
    [field: SerializeField] public Unit TargetStates { get; set; }
    [field: SerializeField] public Animator animator { get; private set; }
    public Action EndAniFunc { get; set; }
    protected Action<States> StatesSetFunc { get; set; }

    private UnitBehaviour action;
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

    public virtual void AllStateSum(States state)
    {
        UnitStates.Power = state.Power;
        UnitStates.Defense = state.Defense;
        UnitStates.MaxHp = state.Hp;
        UnitStates.Speed = state.Speed;
    }

    public virtual void AllStateMinus(States state)
    {
        UnitStates.Power = -state.Power;
        UnitStates.Defense = -state.Defense;
        UnitStates.MaxHp = -state.Hp;
        UnitStates.Speed = -state.Speed;
    }

    protected void StartAction()//애니메이션 끝났을때 실행할 함수
    {
        EndAniFunc?.Invoke();
    }

    protected void SetUnitAttack(AbillityWrapper unitBehaviour)
    {
        action = new Attack(this, unitBehaviour.RepNumber, unitBehaviour.AbilityStates);
        unitBehaviour.Abillity = action;
        unitBehaviour.AbillityFunc = unitBehaviour.Abillity.Invoke;
    }

    protected void SetUnitDefense(AbillityWrapper unitBehaviour)
    {
        unitBehaviour.Abillity = new Defense(this, unitBehaviour.AbilityStates);
        unitBehaviour.AbillityFunc = unitBehaviour.Abillity.Invoke;
    }

    protected void SetUnitRecovery(AbillityWrapper unitBehaviour)
    {
        unitBehaviour.Abillity = new Recovery(this, unitBehaviour.AbilityStates);
        unitBehaviour.AbillityFunc = unitBehaviour.Abillity.Invoke;
    }

    protected void SetUnitBuff(AbillityWrapper unitBehaviour)
    {
        unitBehaviour.Abillity = new PowerUp(this, unitBehaviour.AbilityStates);
        unitBehaviour.AbillityFunc = unitBehaviour.Abillity.Invoke;
    }

    protected void SetUnitSpecial(AbillityWrapper unitBehaviour)
    {
        switch (unitBehaviour.type)
        {
            case CardType.TargetTurnReove:
                unitBehaviour.Abillity = new TargetTurnSkip(this);
                unitBehaviour.AbillityFunc = unitBehaviour.Abillity.Invoke;
                break;
            case CardType.CardDrowUp:
                unitBehaviour.Abillity = new CardDrowUp(this);
                unitBehaviour.AbillityFunc = unitBehaviour.Abillity.Invoke;
                break;
            case CardType.TargetPowerDown:
                unitBehaviour.Abillity = new TargetPowerDown(this, unitBehaviour.AbilityStates);
                unitBehaviour.AbillityFunc = unitBehaviour.Abillity.Invoke;
                break;
            case CardType:
                unitBehaviour.Abillity = new TargetDefenseDown(this, unitBehaviour.AbilityStates);
                unitBehaviour.AbillityFunc = unitBehaviour.Abillity.Invoke;
                break;
        }
    }
}
[Serializable]
public class Attack : IAttack
{
    private Unit Unit;
    private int repnumber;
    private float States;
    public Attack(Unit unit, int repnumber, float states)
    {
        Unit = unit;
        Debug.Log(Unit);
        this.repnumber = repnumber;
        States = states;
    }
    public void Invoke()
    {
        Unit.animator.SetTrigger("Attack");
        int repsavenum = repnumber;
        repsavenum -= 1;
        Unit.EndAniFunc = (() =>
        {

            if (Unit.TargetStates.UnitStates.Defense > 0)
            {
                Unit.TargetStates.UnitStates.Defense = -(Unit.UnitStates.Power + States);
                if (Unit.TargetStates.UnitStates.Defense < 0)
                {
                    Unit.TargetStates.UnitStates.Hp = Unit.TargetStates.UnitStates.Defense;
                    Unit.TargetStates.UnitStates.SetDefense = 0;
                }
            }
            else
            {
                Unit.TargetStates.UnitStates.Hp = -(Unit.UnitStates.Power + States);
            }

            Unit.TargetStates.animator.SetTrigger("Hit");

            Unit.TargetStates.StatesUiSet();
            if (repsavenum > 0)
            {
                Unit.animator.SetTrigger("Attack");
                repsavenum -= 1;
            }
            Unit.StatesUiSet();

        });
    }
}
[Serializable]
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
[Serializable]
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
[Serializable]
public class PowerUp : IBuff
{
    public Unit Unit;
    public float States;
    public PowerUp(Unit unit, float states)
    {
        Unit = unit;
        States = states;
    }

    public void Invoke()
    {
        Unit.animator.SetTrigger("Buff");
        Unit.EndAniFunc = (() =>
        {
            Unit.TargetStates.UnitStates.Power = States;
            Unit.StatesUiSet();
            Debug.Log($"스텟강화{Unit.name}");
        });
    }
}

[Serializable]//애니메이션 추가 아래로 다
public class TargetTurnSkip : ISpecial
{
    private Unit unit;

    public TargetTurnSkip(Unit unit)
    {
        this.unit = unit;
    }


    public void Invoke()
    {
        unit.animator.SetTrigger("Buff");//고쳐야함
        unit.EndAniFunc = (() =>
        {
            Local.TurnSystem.Turnskip = true;
            unit.StatesUiSet();
            Debug.Log($"턴삭제{unit.name}");
        });
    }
}
[Serializable]
public class CardDrowUp : ISpecial
{
    private Unit unit;

    public CardDrowUp(Unit unit)
    {
        this.unit = unit;
    }

    public void Invoke()
    {
        unit.animator.SetTrigger("Buff");
        Local.EventHandler.Invoke<int>(EnumType.CardDrowUp, 1);
        Debug.Log($"다음턴 카드 한장 추가{unit.name}");
    }
}
[Serializable]
public class TargetPowerDown : ISpecial
{
    private Unit unit;
    private float state;
    public TargetPowerDown(Unit unit, float repnumber)
    {
        this.unit = unit;
        this.state = repnumber;
    }
    public void Invoke()
    {
        unit.animator.SetTrigger("Buff");
        unit.EndAniFunc = (() =>
        { unit.TargetStates.UnitStates.Power = -state; });
        Debug.Log($"적 공격력 다운{unit.name}");
    }
}
[Serializable]
public class TargetDefenseDown : ISpecial
{
    private Unit unit;
    private float state;
    public TargetDefenseDown(Unit unit, float repnumber)
    {
        this.unit = unit;
        this.state = repnumber;
    }
    public void Invoke()
    {
        unit.animator.SetTrigger("Buff");
        unit.EndAniFunc = (() =>
        { unit.TargetStates.UnitStates.Defense = -state; });
        Debug.Log($"적 방어력 다운{unit.name}");
    }
}

