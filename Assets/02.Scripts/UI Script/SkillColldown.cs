using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillColldown : MonoBehaviour
{
    public Image filledImage;
    public float duration = 10.0f; // 10�� ���� ����
    private bool isFilling = false;
    private float fillStartTime;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (!isFilling)
            {

                isFilling = true;
                fillStartTime = Time.time; // ���� �ð� ���
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
                isFilling = false; // �ð��� �ʰ��Ǹ� ����
                SkillManager.isSkill_R_ready = true;
            }
        }
    }
}
