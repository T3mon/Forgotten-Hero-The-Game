using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthText : MonoBehaviour
{
    public float timeToLive = 1f;
    public float floatSpeed = 0.5f;

    private RectTransform rectTransform;
    public TextMeshProUGUI textMesh;
    private Color startingColor;
    private float timeElapsed = 0f;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        startingColor = textMesh.color;
    }

    void Update()
    {
        timeElapsed += Time.deltaTime;
        var floatDirection = new Vector3(0, 1, 0);
        rectTransform.position += floatDirection * floatSpeed * Time.deltaTime;

        textMesh.color = new Color(startingColor.r, startingColor.g, startingColor.b, startingColor.a - (timeElapsed / timeToLive));

        if (timeElapsed > timeToLive)
            Destroy(gameObject);

    }
}
