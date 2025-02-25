using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    public GameObject EnemyModel;
    public Transform CenterPos;
    public static Unit CurrentGameObject;
    public void Awake()
    {
        Local.EventHandler.Register<States>(EnumType.EnemyDie, (enemyState) => { CreateEnum(); });
        CreateEnum();
    }
    public void CreateEnum()
    {
        Instantiate(EnemyModel, CenterPos.position, CenterPos.rotation).gameObject.transform.TryGetComponent(out Unit unit);
        CurrentGameObject = unit;
        //if(IsInFirstSequence(Local.Stage))//중간보스
        //{
        //}
        //else if (IsInSecondSequence(Local.Stage))//보스
        //{

        //}
        //else//일반 몬스터
        //{

        //}
    }

    public bool IsInFirstSequence(int x)//5,15,25,35,45 이 수열인지 획인
    {
        return (x + 5) % 10 == 0 && (x + 5) / 10 >= 1;
    }

    public bool IsInSecondSequence(int x)//10,20,30,40,50 이 수열인지 확인
    {
        return x % 10 == 0 && x / 10 >= 1;
    }
}





