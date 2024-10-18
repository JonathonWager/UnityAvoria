using UnityEngine.UI;
using UnityEngine;

public class AbilityEntry : MonoBehaviour
{
    // Start is called before the first frame update
    public Image abilityIcon;
    public Text abilityName; 
    public void SetUI(string name,Sprite icon)
    {
        abilityIcon.sprite = icon;
        abilityName.text = name;
    }
}
