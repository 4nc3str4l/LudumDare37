using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class UITimeController : MonoBehaviour {

    public Text TxtTimer;
    public Image Background;
    public Image ToFill;
    public static UITimeController Instance;

	// Use this for initialization
	void Start () {
        Instance = this;
	}
	
	// Update is called once per frame
	void Update () {
        ToFill.fillAmount = MapController.Instance.GetActiveTime();
    }

    public void ChangeData()
    {
        Color col = MapController.Instance.GetActualColor();
        TxtTimer.text = MapController.Instance.GetActualText();
        TxtTimer.color = col;
        Background.color = new Color(col.r, col.g, col.b, 0.2f);
        ToFill.color = col;     
    }
}
