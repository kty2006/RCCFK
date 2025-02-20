using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitStatesUiView : MonoBehaviour
{
    public Slider PlayerHpbar;
    public Text PlayerHp;
    public Slider EnemyHpbar;
    public Text EnemyHp;

    public Slider PlayerDefensebar;
    public Text PlayerDefense;
    public Slider EnemyDefensebar;
    public Text EnemyDefense;

    public TextMeshProUGUI CostText;
    public void PlayerUiSet(States states)
    {
        PlayerHpbar.value = states.Hp / states.MaxHp;
        PlayerHp.text = $"{states.Hp}/{states.MaxHp}";
        PlayerDefensebar.value = states.Defense / states.MaxDefense;
        PlayerDefense.text = $"{states.Defense} / {states.MaxDefense}";
    }

    public void EnemyUiSet(States states)
    {
        EnemyHpbar.value = states.Hp / states.MaxHp;
        EnemyHp.text = $"{states.Hp}/{states.MaxHp}";
        EnemyDefensebar.value = states.Defense / states.MaxDefense;
        EnemyDefense.text = $"{states.Defense} / {states.MaxDefense}";
    }

    public void CostSet(States states)
    {
        CostText.text = $"{states.Cost}/{states.MaxCost}";
    }
}
