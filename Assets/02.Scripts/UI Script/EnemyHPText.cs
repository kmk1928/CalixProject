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
        // 원하는 스타일, 크기, 색상 등의 설정 가능
        // 예: damageText.color = Color.red;

        // 텍스트가 사라지도록 코루틴을 사용하거나, Destroy 메서드를 사용하여 삭제 가능
        // StartCoroutine(FadeOutAndDestroy());
    }

    // 다음과 같이 텍스트가 사라지도록 하는 코루틴 예시
    private IEnumerator FadeOutAndDestroy()
    {
        yield return new WaitForSeconds(1f); // 원하는 시간
        Destroy(gameObject);
    }
}
