using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DieUiView : MonoBehaviour
{
    public TextMeshProUGUI StageText;
    public void StageSet(States states)
    {
        StageText.text = $"{Local.Stage}";
    }
}
