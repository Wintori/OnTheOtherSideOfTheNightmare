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

public class HealthBar : MonoBehaviour {

    public class Border {
        public float thickness;
        public Color color;
    }

    public static HealthBar Create(Vector3 position, Vector3 size, Color barColor, Color backgroundColor, Border border = null) {
        // Main Health Bar
        GameObject healthBarGameObject = new GameObject("HealthBar");
        healthBarGameObject.transform.position = position;

        if (border != null) {
            // Border
            GameObject borderGameObject = new GameObject("Border", typeof(SpriteRenderer));
            borderGameObject.transform.SetParent(healthBarGameObject.transform);
            borderGameObject.transform.localPosition = Vector3.zero;
            borderGameObject.transform.localScale = size + Vector3.one * border.thickness;
            borderGameObject.GetComponent<SpriteRenderer>().color = border.color;
            borderGameObject.GetComponent<SpriteRenderer>().sprite = GameAssets.i.whitePixelSprite;
            borderGameObject.GetComponent<SpriteRenderer>().sortingOrder = 90;
        }

        // Background
        GameObject backgroundGameObject = new GameObject("Background", typeof(SpriteRenderer));
        backgroundGameObject.transform.SetParent(healthBarGameObject.transform);
        backgroundGameObject.transform.localPosition = Vector3.zero;
        backgroundGameObject.transform.localScale = size;
        backgroundGameObject.GetComponent<SpriteRenderer>().color = backgroundColor;
        backgroundGameObject.GetComponent<SpriteRenderer>().sprite = GameAssets.i.whitePixelSprite;
        backgroundGameObject.GetComponent<SpriteRenderer>().sortingOrder = 100;

        // Bar
        GameObject barGameObject = new GameObject("Bar");
        barGameObject.transform.SetParent(healthBarGameObject.transform);
        barGameObject.transform.localPosition = new Vector3(- size.x / 2f, 0f);

        // Bar Sprite
        GameObject barSpriteGameObject = new GameObject("BarSprite", typeof(SpriteRenderer));
        barSpriteGameObject.transform.SetParent(barGameObject.transform);
        barSpriteGameObject.transform.localPosition = new Vector3(size.x / 2f, 0f);
        barSpriteGameObject.transform.localScale = size;
        barSpriteGameObject.GetComponent<SpriteRenderer>().color = barColor;
        barSpriteGameObject.GetComponent<SpriteRenderer>().sprite = GameAssets.i.whitePixelSprite;
        barSpriteGameObject.GetComponent<SpriteRenderer>().sortingOrder = 110;

        HealthBar healthBar = healthBarGameObject.AddComponent<HealthBar>();

        return healthBar;
    }

    private Transform bar;

	private void Awake () {
        bar = transform.Find("Bar");
	}

    public void SetSize(float sizeNormalized) {
        bar.localScale = new Vector3(sizeNormalized, 1f);
    }

    public void SetColor(Color color) {
        bar.Find("BarSprite").GetComponent<SpriteRenderer>().color = color;
    }
}
