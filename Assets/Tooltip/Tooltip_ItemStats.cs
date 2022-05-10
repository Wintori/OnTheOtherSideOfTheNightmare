/* 
    ------------------- Code Monkey -------------------

    Thank you for downloading this package
    I hope you find it useful in your projects
    If you have any questions let me know
    Cheers!

               unitycodemonkey.com
    --------------------------------------------------
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;

public class Tooltip_ItemStats : MonoBehaviour {

    private static Tooltip_ItemStats instance;
    
    [SerializeField]
    private Camera uiCamera;
    [SerializeField]
    private RectTransform canvasRectTransform;

    private Image image;
    private Text nameText;
    private Text descriptionText;
    private Text dexText;
    private Text strText;
    private Text conText;
    private RectTransform backgroundRectTransform;

    private void Awake() {
        instance = this;
        backgroundRectTransform = transform.Find("background").GetComponent<RectTransform>();
        image = transform.Find("image").GetComponent<Image>();
        nameText = transform.Find("nameText").GetComponent<Text>();
        descriptionText = transform.Find("descriptionText").GetComponent<Text>();
        strText = transform.Find("strText").GetComponent<Text>();
        conText = transform.Find("conText").GetComponent<Text>();
        dexText = transform.Find("dexText").GetComponent<Text>();

        HideTooltip();
    }

    private void Update() {
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponent<RectTransform>(), Input.mousePosition, uiCamera, out localPoint);
        transform.localPosition = localPoint;

        Vector2 anchoredPosition = transform.GetComponent<RectTransform>().anchoredPosition;
        if (anchoredPosition.x + backgroundRectTransform.rect.width > canvasRectTransform.rect.width) {
            anchoredPosition.x = canvasRectTransform.rect.width - backgroundRectTransform.rect.width;
        }
        if (anchoredPosition.y - backgroundRectTransform.rect.height > canvasRectTransform.rect.height) {
            anchoredPosition.y = canvasRectTransform.rect.height + backgroundRectTransform.rect.height;
        }
        transform.GetComponent<RectTransform>().anchoredPosition = anchoredPosition;
    }

    private void ShowTooltip(Sprite itemSprite, string itemName, string itemDescription, int dex, int con, int str) {
        gameObject.SetActive(true);
        transform.SetAsLastSibling();
        nameText.text = itemName;
        descriptionText.text = itemDescription;
        dexText.text = dex.ToString();
        conText.text = con.ToString();
        strText.text = str.ToString();
        image.sprite = itemSprite;
        Update();
    }

    private void HideTooltip() {
        gameObject.SetActive(false);
    }

    public static void ShowTooltip_Static(Sprite itemSprite, string itemName, string itemDescription, int dex, int con, int str) {
        instance.ShowTooltip(itemSprite, itemName, itemDescription, dex, con, str);
    }

    public static void HideTooltip_Static() {
        instance.HideTooltip();
    }



    

    public static void AddTooltip(Transform transform, Sprite itemSprite, string itemName, string itemDescription, int dex, int con, int str) {
        if (transform.GetComponent<Button_UI>() != null) {
            transform.GetComponent<Button_UI>().MouseOverOnceTooltipFunc = () => Tooltip_ItemStats.ShowTooltip_Static(itemSprite, itemName, itemDescription, dex, con, str);
            transform.GetComponent<Button_UI>().MouseOutOnceTooltipFunc = () => Tooltip_ItemStats.HideTooltip_Static();
        }
    }

}