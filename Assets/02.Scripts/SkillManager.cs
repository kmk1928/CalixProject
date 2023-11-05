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
            DontDestroyOnLoad(this.gameObject); //씬 옮겨도 노파괴
            Debug.LogWarning("UI 매니저 할당!!!");
        }
        else
        {
            // instance에 이미 다른 GameManager 오브젝트가 할당되어 있는 경우
            // 씬에 두개 이상의 GameManager 오브젝트가 존재한다는 의미.
            // 싱글톤 오브젝트는 하나만 존재해야 하므로 자신의 게임 오브젝트를 파괴
            Debug.LogWarning("씬에 두 개 이상의 UI매니저가 존재합니다!");
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
