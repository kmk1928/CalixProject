using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHPbar : MonoBehaviour
{
    [SerializeField] GameObject enemy_HPbarPrefab = null;

    List<Transform> enemy_objectList = new List<Transform>();
    List<GameObject> enemy_HPbarList = new List<GameObject>();
    List<Slider> enemy_sliderList = new List<Slider>();

    List<Slider> enemy_YellowSliderList = new List<Slider>();
    List<Text> enemy_textList = new List<Text>();
    List<CharStats> enemy_statsList = new List<CharStats>();
    float text_enableTime = 0f;

    Camera enemy_cam = null;


    public GameObject bossHpbar;
    private GameObject bossObj;
    private Slider bossHpSlider;
    private CharStats bossStats;
    private Text bossDamageTxt;
    private bool bossOn;

    // Start is called before the first frame update
    void Start()
    {
        enemy_cam = Camera.main;

        GameObject[] t_objects = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < t_objects.Length; i++)
        {
            enemy_objectList.Add(t_objects[i].transform);
            GameObject t_HPbar = Instantiate(enemy_HPbarPrefab, t_objects[i].transform.position, Quaternion.identity, transform);
            enemy_HPbarList.Add(t_HPbar);
            enemy_textList.Add(t_HPbar.GetComponentInChildren<Text>());

            // 슬라이더 오브젝트에서 Slider 컴포넌트를 찾음
            Slider[] slider = t_HPbar.GetComponentsInChildren<Slider>();
            enemy_sliderList.Add(slider[0]);
            enemy_YellowSliderList.Add(slider[1]);

            // Slider 컴포넌트에 연결된 CharStats 스크립트에서 현재 체력 및 최대 체력 값을 가져옴
            CharStats charStats = t_objects[i].GetComponent<CharStats>();
            enemy_statsList.Add(charStats);
            float curHP = charStats.curHealth;
            float maxHP = charStats.maxHealth;

            // 슬라이더의 value를 현재 체력 비율로 설정
            float healthRatio = curHP / maxHP;
            slider[0].value = healthRatio;
            slider[1].value = healthRatio;

        }

        bossObj = GameObject.FindGameObjectWithTag("Boss");
        if(bossObj != null)
        {
            bossStats = bossObj.GetComponent<CharStats>();
            bossHpSlider = bossHpbar.GetComponentInChildren<Slider>();
            bossDamageTxt = bossHpbar.GetComponentInChildren<Text>();
        }
    }
    // Update is called once per frame
    void Update()
    {

        for (int i = 0; i < enemy_objectList.Count; i++)
        {
            if (enemy_objectList[i] != null)
            {

                // 적 객체가 카메라에 보이는지 확인
                if (enemy_cam.WorldToViewportPoint(enemy_objectList[i].position).z > 0)
                {
                    enemy_HPbarList[i].transform.position = enemy_cam.WorldToScreenPoint(enemy_objectList[i].position + new Vector3(0, 1.2f, 0));
                    enemy_sliderList[i].value = Mathf.Lerp(enemy_sliderList[i].value, enemy_statsList[i].curHealth / enemy_statsList[i].maxHealth, Time.deltaTime * 25);
                    StartCoroutine( YellowBar(enemy_YellowSliderList[i], enemy_statsList[i].curHealth, enemy_statsList[i].maxHealth));
                    if ( text_enableTime != enemy_statsList[i].t_damage)
                    {
                        EnableHPbar(enemy_HPbarList[i]);
                        EnableText(enemy_textList[i], enemy_statsList[i].t_damage.ToString("0"));
                    }
                    else if(text_enableTime == enemy_statsList[i].t_damage)
                    {
                        DisableText(enemy_textList[i]);
                        DisableHPbar(enemy_HPbarList[i]);
                    }
                    //enemy_textList[i].text = "-" + enemy_statsList[i].t_damage.ToString("0");
                }
            }
            // 적 오브젝트가 파괴되었을 때 처리
            if (enemy_objectList[i] == null)
            {

                // 적 오브젝트가 파괴되었으므로 체력바를 파괴
                Destroy(enemy_HPbarList[i]);

                // 해당 리스트의 아이템들도 삭제
                enemy_objectList.RemoveAt(i);
                enemy_HPbarList.RemoveAt(i);
                enemy_sliderList.RemoveAt(i);
                enemy_textList.RemoveAt(i);
                enemy_statsList.RemoveAt(i);
                enemy_YellowSliderList.RemoveAt(i);

                i--; // 인덱스 감소 (리스트가 조정되었으므로 같은 인덱스 재확인)
            }



        }

        //보스
        bossOn = GameManager.instance.isBossBattle;
        if(bossObj != null)
        {
            if (bossOn)
            {
                EnableHPbar(bossHpbar);
                bossHpSlider.value = Mathf.Lerp(bossHpSlider.value, bossStats.curHealth / bossStats.maxHealth, Time.deltaTime * 25);
                if (text_enableTime != bossStats.t_damage)
                {
                    EnableText(bossDamageTxt, bossStats.t_damage.ToString("0"));
                }
                else if (text_enableTime == bossStats.t_damage)
                {
                    DisableText(bossDamageTxt);
                }
            }
            else
            {
                DisableHPbar(bossHpbar);
            }
        }
        else
        {
            DisableHPbar(bossHpbar);
        }

    }

    IEnumerator YellowBar(Slider slider, float curHP, float maxHP)
    {
        yield return new WaitForSeconds(1f);
        slider.value = Mathf.Lerp(slider.value,curHP / maxHP, Time.deltaTime * 80);
    }
    void EnableText(Text hpText, string s_damage)
    {
        hpText.text = s_damage;
        hpText.enabled = true;
    }
    void DisableText(Text hpText)
    {
        hpText.enabled = false;
    }

    void EnableHPbar(GameObject hpbar)
    {
        hpbar.SetActive(true);
    }
    void DisableHPbar(GameObject hpbar)
    {
        hpbar.SetActive(false);
    }
}
