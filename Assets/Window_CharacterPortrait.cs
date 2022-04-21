using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class Window_CharacterPortrait : MonoBehaviour {

    private Transform cameraTransform;
    private Transform followTransform;

    private void Awake() {
        cameraTransform = transform.Find("camera");

        transform.Find("closeBtn").GetComponent<Button_UI>().ClickFunc = Hide;

        Hide();
    }

    private void Update() {
        cameraTransform.position = new Vector3(followTransform.position.x, followTransform.position.y, Camera.main.transform.position.z);
    }

    public void Show(Transform followTransform) {
        gameObject.SetActive(true);
        this.followTransform = followTransform;
    }

    public void Hide() {
        gameObject.SetActive(false);
    }
}
