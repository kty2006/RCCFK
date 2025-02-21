using System;
using UnityEngine;

public interface IEvent
{
    public void Execute();
}
public interface ITurnObj
{
    public bool Invoke();
}

public interface Observer
{
    public void Update();
}

public interface UnitBehaviour
{
    public void Invoke();
}

[System.Serializable]
public class AbillityWrapper
{
    [HideInInspector] public CardType type;
    [SerializeField] public Action AbillityFunc;
    [SerializeField] public int RepNumber;
    [SerializeField] public float AbilityStates;
}

public interface IAttack : UnitBehaviour
{

}

public interface IDefense : UnitBehaviour
{

}

public interface IRecovery : UnitBehaviour
{

}

public interface IBuff : UnitBehaviour
{

}

public interface ISpecial : UnitBehaviour
{

}

#region //Factory∆–≈œ
public interface IProduct
{
    public void Setting();
}

public interface Ifactory //
{
    IProduct SomeOperation()
    {
        IProduct product = CreateProduct();
        product.Setting();
        return product;
    }

    public IProduct CreateProduct();
}
#endregion
