using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Unit Enemy;
    private AbillityWrapper unitBehaviour = new();

    public void Awake()
    {
        //Local.EventHandler.Register<>
    }

    public void SelectAction()
    {
        int num = Random.Range(0, 5);
        switch (num)
        {
            case 0:
                Local.EventHandler.Invoke<AbillityWrapper>(EnumType.EnemyAttack, unitBehaviour);
                Local.EventHandler.Invoke<AbillityWrapper>(EnumType.EnemyTurnAdd, unitBehaviour);
                break;
            case 1:
                Local.EventHandler.Invoke<AbillityWrapper>(EnumType.EnemyDefense, unitBehaviour);
                Local.EventHandler.Invoke<AbillityWrapper>(EnumType.EnemyTurnAdd, unitBehaviour);
                break;
            case 2:
                Local.EventHandler.Invoke<AbillityWrapper>(EnumType.EnemyRecovery, unitBehaviour);
                Local.EventHandler.Invoke<AbillityWrapper>(EnumType.EnemyTurnAdd, unitBehaviour);
                break;
            case 3:
                Local.EventHandler.Invoke<AbillityWrapper>(EnumType.EnemyBuff, unitBehaviour);
                Local.EventHandler.Invoke<AbillityWrapper>(EnumType.EnemyTurnAdd, unitBehaviour);
                break;
            case 4:
                Local.EventHandler.Invoke<AbillityWrapper>(EnumType.EnemySpecial, unitBehaviour);
                Local.EventHandler.Invoke<AbillityWrapper>(EnumType.EnemyTurnAdd, unitBehaviour);
                break;
        }
    }
}
