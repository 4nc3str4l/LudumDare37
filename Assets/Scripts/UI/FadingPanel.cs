using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FadingPanel : MonoBehaviour {

    public Image image;
    public Text text;

    public void Init(Sprite sprite, Color textColor, string t)
    {
        image.sprite = sprite;
        text.text = t;
        text.color = textColor;
        Destroy(gameObject, 3f);
    }


}
