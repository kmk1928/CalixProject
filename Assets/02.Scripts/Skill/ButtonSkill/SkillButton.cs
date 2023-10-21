using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillButton : MonoBehaviour
{
    public SOSkill skill;
    public PlayerController playerController;
    public Image imgIcon;
    public Image imgCool;

    // Start is called before the first frame update
    void Start()
    {
        imgIcon.sprite = skill.icon;
        imgCool.fillAmount = 0;
    }

    public void PressSkill() {
        if (imgCool.fillAmount > 0) return;

        playerController.ActivateSkill(skill);

        StartCoroutine(SC_Cool());
    }

IEnumerator SC_Cool() {
        float tick = 1f / skill.cool;
        float t = 0;

        imgCool.fillAmount = 1;

        while(imgCool.fillAmount > 0) {
            imgCool.fillAmount = Mathf.Lerp(1, 0, t);
            t += (Time.deltaTime * tick);
            yield return null;
        }
    }
}
