using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillColldown1 : MonoBehaviour
{
    public Image filledImage;
    public float duration = 4.0f; // 10�� ���� ����
    private bool isFilling = false;
    private float fillStartTime;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
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
                SkillManager.isSkill_F_ready = true;
            }
        }
    }
}
