using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OpenBox : MonoBehaviour
{
    [SerializeField] private Image _prizeImage;
    [SerializeField] private TMP_Text _prizeText;


    public void FillBoxInfo(Sprite prizeSprite, float prizevalue)
    {
        _prizeImage.sprite = prizeSprite;
        _prizeText.text = FormatNumsHelper.FormatNum(prizevalue);
    }
}
