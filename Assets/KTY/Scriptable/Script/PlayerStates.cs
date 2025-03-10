using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStates", menuName = "Scriptable Objects/PlayerStates")]
public class PlayerStates : ScriptableObject
{
    public float Lv;
    public float Power;
    public float Speed;
    public float Defense;
    public float MaxHp;
    public float Hp;
    public int Maxcost;
    public int Cost;
}
