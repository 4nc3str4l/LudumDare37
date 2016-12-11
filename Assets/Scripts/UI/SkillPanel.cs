using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Assets.Scripts.Skills;

public class SkillPanel : MonoBehaviour {

    public Text SkillName;
    public Image Filling;
    private Skill _trackingSkill;

    private Color _fillingColor;
    private Color _fullColor;

    public void SetTrackingSkill(Skill newSkill)
    {
        _fullColor = Filling.color;
        _fillingColor = new Color(170, 0, 0, 40);
        _trackingSkill = newSkill;
        SkillName.text = newSkill.Name;
    }

    void Update()
    {
        if (_trackingSkill == null) return;
        float fillAmount = _trackingSkill.GetFillAmount();
        Filling.fillAmount = fillAmount;
        Filling.color = fillAmount >= 1 ? _fullColor : _fillingColor;
  
    }
}
