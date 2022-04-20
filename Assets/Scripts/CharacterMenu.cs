using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMenu : MonoBehaviour
{
    public Text levelText, hitpointText, pesosText, upgradeCostText, XpText;

    private int currentCharacterSelection = 0;
    public Image characterSelectionSprite;
    public Image weaponSprite;
    public RectTransform xpBar;

    public void onarrowClick(bool right)
    {
        if (right)
        {
            currentCharacterSelection++;

            if (currentCharacterSelection == GameManager.instance.playerSprites.Count)
                currentCharacterSelection = 0;

            OnSelectionChanged();
        }
        else
        {
            currentCharacterSelection--;

            if (currentCharacterSelection < 0)
                currentCharacterSelection = GameManager.instance.playerSprites.Count - 1;

            OnSelectionChanged();
        }
    }

    private void OnSelectionChanged()
    {
        characterSelectionSprite.sprite = GameManager.instance.playerSprites[currentCharacterSelection];
        GameManager.instance.player.SwapSprite(currentCharacterSelection);
    }

    public void UpdateMenu()
    {
        weaponSprite.sprite = GameManager.instance.weaponSprite[GameManager.instance.weapon.weaponLeve];
        if (GameManager.instance.weapon.weaponLeve == GameManager.instance.weaponPrices.Count)
            upgradeCostText.text = "MAX";
        else
            upgradeCostText.text = GameManager.instance.weaponPrices[GameManager.instance.weapon.weaponLeve].ToString();

        levelText.text = GameManager.instance.GetCurrenetLevel().ToString();
        hitpointText.text = GameManager.instance.player.hitpoint.ToString();

        pesosText.text = GameManager.instance.pesos.ToString();

        int currLevel = GameManager.instance.GetCurrenetLevel();
        if (currLevel == GameManager.instance.xpTable.Count)
        {
            XpText.text = GameManager.instance.experience.ToString() + "total experiecnce points";
            xpBar.localScale = Vector3.one;
        }
        else
        {
            int prevLevelXp = GameManager.instance.GetExpLevel(currLevel - 1);
            int currLevelXp = GameManager.instance.GetExpLevel(currLevel);

            int diff = currLevelXp - prevLevelXp;
            int currXpToLevel = GameManager.instance.experience - prevLevelXp;

            float completionRatio = (float)currXpToLevel / (float)diff;
            xpBar.localScale = new Vector3(completionRatio, 1, 1);
            XpText.text = currXpToLevel.ToString() + " / " + diff;
        }
        //GameManager.instance.GetExpLevel(currLevel-1);


    }
    public void OnUpgradeClick()
    {
        if (GameManager.instance.TryUpgradeWeapon())
            UpdateMenu();
    }
}
