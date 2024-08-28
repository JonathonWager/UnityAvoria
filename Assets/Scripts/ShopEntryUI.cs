using UnityEngine;
using UnityEngine.UI;

public class ShopEntryUI : MonoBehaviour
{
    public Text weaponNameText;
    public Image weaponIcon;
    public Text goldAmountText;

    public void SetWeaponName(string name)
    {
        weaponNameText.text = name;
    }
    public void SetWeaponIcon(Sprite icon)
    {
        weaponIcon.sprite = icon;
    }

    public void SetGoldAmount(int goldAmount)
    {
        goldAmountText.text = goldAmount.ToString();
    }
}
