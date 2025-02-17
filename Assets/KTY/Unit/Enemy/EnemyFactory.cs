using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    public GameObject EnemyModel;
    public Transform CenterPos;
    public static Unit CurrentGameObject;
    public void Awake()
    {
        Local.EventHandler.Register<UnitDead>(EnumType.EnemyDie, (unitDead) => { CreateEnum(); });
        CreateEnum();
    }
    public void CreateEnum()
    {
        Instantiate(EnemyModel, CenterPos.position, CenterPos.rotation).gameObject.transform.GetChild(0).TryGetComponent(out Unit unit);
        CurrentGameObject = unit;
    }
}
