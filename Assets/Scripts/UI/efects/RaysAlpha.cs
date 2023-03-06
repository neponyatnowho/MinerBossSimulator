using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class RaysAlpha : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 100f;
    [SerializeField] private float minAlpha = 0.1f;
    [SerializeField] private float maxAlpha = 0.99f;
    [SerializeField] private float flickerFrequency = 0.05f;
    [SerializeField] private float flickerSpeed = 0.05f;

    private Image image;
    private RectTransform rectTransform;

    private bool addAlpha = false;
    private float currentAlpha;


    private void Start()
    {
        image = GetComponent<Image>();
        rectTransform = GetComponent<RectTransform>();
        currentAlpha = maxAlpha;
        StartCoroutine(Flicker());
    }

    private void Update()
    {
        rectTransform.Rotate(new Vector3(0, 0, rotationSpeed * Time.deltaTime));
    }

    private IEnumerator Flicker()
    {
        while (true)
        {
            if (addAlpha)
            {
                currentAlpha += flickerSpeed;
            }
            else
            {
                currentAlpha -= flickerSpeed;
            }

            if (currentAlpha >= maxAlpha)
            {
                currentAlpha = maxAlpha;
                addAlpha = false;
            }
            else if (currentAlpha <= minAlpha)
            {
                currentAlpha = minAlpha;
                addAlpha = true;
            }

            image.color = new Color(1, 1, 1, currentAlpha);
            yield return new WaitForSeconds(flickerFrequency);
        }
    }

}
