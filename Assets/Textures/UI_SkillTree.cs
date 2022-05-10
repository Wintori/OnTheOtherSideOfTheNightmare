/* 
    ------------------- Code Monkey -------------------

    Thank you for downloading this package
    I hope you find it useful in your projects
    If you have any questions let me know
    Cheers!

               unitycodemonkey.com
    --------------------------------------------------
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;

public class UI_SkillTree : MonoBehaviour {

    [SerializeField] private Material skillLockedMaterial;
    [SerializeField] private Material skillUnlockableMaterial;
    [SerializeField] private SkillUnlockPath[] skillUnlockPathArray;
    [SerializeField] private Sprite lineSprite;
    [SerializeField] private Sprite lineGlowSprite;

    private PlayerSkills playerSkills;
    private List<SkillButton> skillButtonList;
    private TMPro.TextMeshProUGUI skillPointsText;

    private Animator isVisible;


    private void Awake() {
        skillPointsText = transform.Find("skillPointsText").GetComponent<TMPro.TextMeshProUGUI>();
        isVisible = GetComponent<Animator>();
    }

    public void SetPlayerSkills(PlayerSkills playerSkills) {
        this.playerSkills = playerSkills;

        skillButtonList = new List<SkillButton>();
        skillButtonList.Add(new SkillButton(transform.Find("earthshatterBtn"), playerSkills, PlayerSkills.SkillType.Earthshatter, skillLockedMaterial, skillUnlockableMaterial));
        skillButtonList.Add(new SkillButton(transform.Find("whirlwindBtn"), playerSkills, PlayerSkills.SkillType.Whirlwind, skillLockedMaterial, skillUnlockableMaterial));
        skillButtonList.Add(new SkillButton(transform.Find("moveSpeed1Btn"), playerSkills, PlayerSkills.SkillType.MoveSpeed_1, skillLockedMaterial, skillUnlockableMaterial));
        skillButtonList.Add(new SkillButton(transform.Find("moveSpeed2Btn"), playerSkills, PlayerSkills.SkillType.MoveSpeed_2, skillLockedMaterial, skillUnlockableMaterial));
        skillButtonList.Add(new SkillButton(transform.Find("healthMax1Btn"), playerSkills, PlayerSkills.SkillType.HealthMax_1, skillLockedMaterial, skillUnlockableMaterial));
        skillButtonList.Add(new SkillButton(transform.Find("healthMax2Btn"), playerSkills, PlayerSkills.SkillType.HealthMax_2, skillLockedMaterial, skillUnlockableMaterial));
        skillButtonList.Add(new SkillButton(transform.Find("firstBtn"), playerSkills, PlayerSkills.SkillType.FirstSkill, skillLockedMaterial, skillUnlockableMaterial));
        skillButtonList.Add(new SkillButton(transform.Find("secondBtn"), playerSkills, PlayerSkills.SkillType.SecondSkill, skillLockedMaterial, skillUnlockableMaterial));
        skillButtonList.Add(new SkillButton(transform.Find("third1Btn"), playerSkills, PlayerSkills.SkillType.ThirdSkill1, skillLockedMaterial, skillUnlockableMaterial));
        skillButtonList.Add(new SkillButton(transform.Find("third2Btn"), playerSkills, PlayerSkills.SkillType.ThirdSkill2, skillLockedMaterial, skillUnlockableMaterial));
        skillButtonList.Add(new SkillButton(transform.Find("fourth1Btn"), playerSkills, PlayerSkills.SkillType.FourthSkill1, skillLockedMaterial, skillUnlockableMaterial));
        skillButtonList.Add(new SkillButton(transform.Find("fourth2Btn"), playerSkills, PlayerSkills.SkillType.FourthSkill2, skillLockedMaterial, skillUnlockableMaterial));



        playerSkills.OnSkillUnlocked += PlayerSkills_OnSkillUnlocked;
        playerSkills.OnSkillPointsChanged += PlayerSkills_OnSkillPointsChanged;

        UpdateVisuals();
        UpdateSkillPoints();
    }

    private void PlayerSkills_OnSkillPointsChanged(object sender, System.EventArgs e) {
        UpdateSkillPoints();
    }

    private void PlayerSkills_OnSkillUnlocked(object sender, PlayerSkills.OnSkillUnlockedEventArgs e) {
        UpdateVisuals();
    }

    private void UpdateSkillPoints() {
        skillPointsText.SetText(playerSkills.GetSkillPoints().ToString());
    }

    private void UpdateVisuals() {
        foreach (SkillButton skillButton in skillButtonList) {
            skillButton.UpdateVisual();
        }

        // Darken all links
        foreach (SkillUnlockPath skillUnlockPath in skillUnlockPathArray) {
            foreach (Image linkImage in skillUnlockPath.linkImageArray) {
                linkImage.color = new Color(.5f, .5f, .5f);
                linkImage.sprite = lineSprite;
            }
        }
        
        foreach (SkillUnlockPath skillUnlockPath in skillUnlockPathArray) {
            if (playerSkills.IsSkillUnlocked(skillUnlockPath.skillType) || playerSkills.CanUnlock(skillUnlockPath.skillType)) {
                // Skill unlocked or can be unlocked
                foreach (Image linkImage in skillUnlockPath.linkImageArray) {
                    linkImage.color = Color.white;
                    linkImage.sprite = lineGlowSprite;
                }
            }
        }
    }

    /*
     * Represents a single Skill Button
     * */
    private class SkillButton {

        private Transform transform;
        private Image image;
        private Image backgroundImage;
        private PlayerSkills playerSkills;
        private PlayerSkills.SkillType skillType;
        private Material skillLockedMaterial;
        private Material skillUnlockableMaterial;

        public SkillButton(Transform transform, PlayerSkills playerSkills, PlayerSkills.SkillType skillType, Material skillLockedMaterial, Material skillUnlockableMaterial) {
            this.transform = transform;
            this.playerSkills = playerSkills;
            this.skillType = skillType;
            this.skillLockedMaterial = skillLockedMaterial;
            this.skillUnlockableMaterial = skillUnlockableMaterial;

            image = transform.Find("image").GetComponent<Image>();
            backgroundImage = transform.Find("background").GetComponent<Image>();

            transform.GetComponent<Button_UI>().ClickFunc = () => {
                if (!playerSkills.IsSkillUnlocked(skillType)) {
                    // Skill not yet unlocked
                    if (!playerSkills.TryUnlockSkill(skillType)) {
                        Tooltip_Warning.ShowTooltip_Static("Cannot unlock !");
                    }
                }
            };
        }

        public void UpdateVisual() {
            if (playerSkills.IsSkillUnlocked(skillType)) {
                image.material = null;
                backgroundImage.color = Color.green;
            } else {
                if (playerSkills.CanUnlock(skillType)) {
                    image.color = new Color(1f, 1f, 1f, 1f);
                    backgroundImage.color = new Color(1f, 1f, 1f);/*UtilsClass.GetColorFromString("4B677D");*/
                    transform.GetComponent<Button_UI>().enabled = true;
                } else {
                    image.color = new Color(1f,1f,1f,.5f);
                    backgroundImage.color = new Color(1f, 0f, 0f);
                    transform.GetComponent<Button_UI>().enabled = false;
                }
            }
        }

    }


    [System.Serializable]
    public class SkillUnlockPath {
        public PlayerSkills.SkillType skillType;
        public Image[] linkImageArray;
    }

    bool triggerVisible = false;

    public bool isActive()
    {
        if (triggerVisible)
        {
            return true;
        }
        else return false;
    }

    public void Show()
    {
        isVisible.SetBool("isVisible", true);
        triggerVisible = true;
    }

    public void Hide()
    {
        isVisible.SetBool("isVisible", false);
        triggerVisible = false;
    }

}
