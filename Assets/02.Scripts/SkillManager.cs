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
            Debug.LogWarning("��ų �Ŵ��� �Ҵ�!!!");
        }
        else
        {
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

    public static bool isSkill_R_ready = true;

}
