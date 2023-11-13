using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillColldown : MonoBehaviour
{
    public Image filledImage;
    public float duration = 10.0f; // 10초 동안 증가
    private bool isFilling = false;
    private float fillStartTime;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (!isFilling)
            {

                isFilling = true;
                fillStartTime = Time.time; // 시작 시간 기록
            }
        }

        if (isFilling)
        {
            float timeElapsed = Time.time - fillStartTime;
            if (timeElapsed < duration)
            {
                float fillAmount = Mathf.Lerp(1f, 0f, timeElapsed / duration);
                filledImage.fillAmount = fillAmount;
            }
            else
            {
                isFilling = false; // 시간이 초과되면 멈춤
                SkillManager.isSkill_R_ready = true;
            }
        }
    }
}
