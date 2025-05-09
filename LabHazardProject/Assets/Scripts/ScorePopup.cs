using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScorePopup : MonoBehaviour
{
    private RectTransform rectTransform;
    private TextMeshProUGUI textBox;
    private float timer = 0f;

    [SerializeField]
    private float popupLifetime = 1.0f;
    [SerializeField]
    private Vector2 popupSpeed;
    
    // Start is called before the first frame update
    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        textBox = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        rectTransform.anchoredPosition = rectTransform.anchoredPosition + (popupSpeed * Time.deltaTime);
        if (timer > popupLifetime)
        {
            Destroy(gameObject);
        }
        timer += Time.deltaTime;
    }

    public void SetPopupValue(Vector3 popupWorldPosition, string text)
    {
        rectTransform.position = popupWorldPosition;
        textBox.text = text;
    }
}

