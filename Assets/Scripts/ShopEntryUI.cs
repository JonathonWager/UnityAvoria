using UnityEngine;
using UnityEngine.UI;

public class ShopEntryUI : MonoBehaviour
{
    public Text weaponNameText,goldAmountText,damage,range,speed,knockback,atkArc,type;
    public Image weaponIcon;


    public void SetUI(string name,Sprite icon,int goldAmount, float Damage, float Range, float Speed, float Knockback,float AtkArc,string Type)
    {
        weaponNameText.text = name;
        weaponIcon.sprite = icon;
        goldAmountText.text = goldAmount.ToString();
        damage.text = Damage.ToString();
        range.text = Range.ToString();
        speed.text = Speed.ToString();
        knockback.text = Knockback.ToString();
        atkArc.text = AtkArc.ToString();
        type.text = Type;
    }
 
}
