using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DieUiView : MonoBehaviour
{
    public TextMeshProUGUI StageText;

    public TextMeshProUGUI GoldText;
    public void StageSet(int num)
    {
        StageText.text = $"{Local.Stage}";
        GoldText.text = $"{Local.Gold}";
        Debug.Log(Local.Stage);
    }
}
