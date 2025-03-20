using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class HitView : MonoBehaviour
{
    [SerializeField]
    Image image;
    [SerializeField]
    TextMeshProUGUI hitText;
    public void SetView(BallonData ballonData)
    {
        image.color = ballonData.color;

        hitText.text = "Count:" + ballonData.hit.ToString();

    }
}
