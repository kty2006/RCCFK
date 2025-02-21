using System;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[Serializable]
public class States //unit스텟 
{
    public Action DeadFunc;
    public float Lv { get { return lv; } set { lv += value; MaxHp = 30 * value; SetDefense = 1 * value; Power = 1 * value; Speed = 1 * value; MaxExp += 1; } }
    public float MaxHp { get { return maxhp; } set { maxhp += value; hp = MaxHp; } }
    public float SetDefense { get { return setdefense; } set { setdefense += value; defense = SetDefense; } }
    public float SetPower { get { return setpower; } set { setpower += value; power = SetPower; } }
    public int MaxCost { get { return maxcost; } set { maxcost += value; maxcost = cost; } }

    [field: SerializeField] public float MaxExp { get; set; } = 10;

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
    [field: SerializeField] public int SetExp { get; set; }

    public float Power { get { return power; } set => power += value; }
    public float Defense { get { return defense; } set => defense += value; }
    public float Hp { get { return hp; } set { hp = Mathf.Clamp(hp += value, 0, MaxHp); if (hp <= 0) { DeadFunc?.Invoke(); } } }
    public float Speed { get { return speed; } set => speed += value; }
    public int Cost { get { return cost; } set { cost = Mathf.Clamp(cost = value, 0, MaxCost); } }

    public float Exp { get { return exp; } set { exp += value; while (exp >= MaxExp) { exp -= MaxExp; Lv = 1; } } }


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
        unitBehaviour.AbillityFunc += new Attack(this, unitBehaviour.RepNumber).Invoke;
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
        unitBehaviour.AbillityFunc = new PowerUp(this, unitBehaviour.AbilityStates).Invoke;
    }

    protected void SetUnitSpecial(AbillityWrapper unitBehaviour)
    {
        switch (unitBehaviour.type)
        {
            case CardType.TargetTurnReove:
                unitBehaviour.AbillityFunc = new TargetTurnSkip(this).Invoke;
                break;
            case CardType.CardDrowUp:
                unitBehaviour.AbillityFunc = new CardDrowUp(this).Invoke;
                break;
            case CardType.TargetPowerDown:
                unitBehaviour.AbillityFunc = new TargetPowerDown(this, unitBehaviour.AbilityStates).Invoke;
                break;
            case CardType.TargetDefenseDown:
                unitBehaviour.AbillityFunc = new TargetDefenseDown(this, unitBehaviour.AbilityStates).Invoke;
                break;
        }
    }
}
public class Attack : IAttack
{
    private Unit Unit;
    private int repnumber;
    public Attack(Unit unit, int repnumber)
    {
        Unit = unit;
        this.repnumber = repnumber;
    }
    public void Invoke()
    {
        Unit.animator.SetTrigger("Attack");
        Unit.EndAniFunc = (() =>
        {
            repnumber -= 1;
            Unit.TargetStates.UnitStates.Hp = -Unit.UnitStates.Power;
            Unit.TargetStates.animator.SetTrigger("Hit");
            Debug.Log($"공격{Unit.name}");
            Unit.TargetStates.StatesUiSet();
            if (repnumber > 0 && Unit.TargetStates.UnitStates.Hp > 0)
            {
                Unit.animator.SetTrigger("Attack");
                repnumber -= 1;
            }
            else
                Unit.StatesUiSet();
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

//애니메이션 추가 아래로 다
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

