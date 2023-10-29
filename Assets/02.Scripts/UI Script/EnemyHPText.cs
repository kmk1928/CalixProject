using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHPText : MonoBehaviour
{
    public Text damageText;

    public void ShowDamage(float damage)
    {
        damageText.text = "-" + damage.ToString("0");
        // ���ϴ� ��Ÿ��, ũ��, ���� ���� ���� ����
        // ��: damageText.color = Color.red;

        // �ؽ�Ʈ�� ��������� �ڷ�ƾ�� ����ϰų�, Destroy �޼��带 ����Ͽ� ���� ����
        // StartCoroutine(FadeOutAndDestroy());
    }

    // ������ ���� �ؽ�Ʈ�� ��������� �ϴ� �ڷ�ƾ ����
    private IEnumerator FadeOutAndDestroy()
    {
        yield return new WaitForSeconds(1f); // ���ϴ� �ð�
        Destroy(gameObject);
    }
}
