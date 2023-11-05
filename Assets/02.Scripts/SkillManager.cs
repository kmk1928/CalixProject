using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject); //�� �Űܵ� ���ı�
            Debug.LogWarning("UI �Ŵ��� �Ҵ�!!!");
        }
        else
        {
            // instance�� �̹� �ٸ� GameManager ������Ʈ�� �Ҵ�Ǿ� �ִ� ���
            // ���� �ΰ� �̻��� GameManager ������Ʈ�� �����Ѵٴ� �ǹ�.
            // �̱��� ������Ʈ�� �ϳ��� �����ؾ� �ϹǷ� �ڽ��� ���� ������Ʈ�� �ı�
            Debug.LogWarning("���� �� �� �̻��� UI�Ŵ����� �����մϴ�!");
            Destroy(gameObject);
        }
    }

    public static bool isRapidAssaultEquipped = true;
    public static bool isFlyMechEquipped = false;
    public static bool isOneSlashEquipped = true;
    public static bool isBloodRainEquipped = false;

    public static int rapidAssault_Lv = 1;
    public static int flyMech_Lv = 1;
    public static int oneSlash_Lv = 1;
    public static int bloodRain_Lv = 1;

}
