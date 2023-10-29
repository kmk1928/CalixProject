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
    List<Text> enemy_textList = new List<Text>();
    List<CharStats> enemy_statsList = new List<CharStats>();
    List<float> text_enableTime = new List<float>();

    Camera enemy_cam = null;
    // Start is called before the first frame update
    void Start() {
        enemy_cam = Camera.main;

        GameObject[] t_objects = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < t_objects.Length; i++) 
        {
            text_enableTime.Add(0f);
            enemy_objectList.Add(t_objects[i].transform);
            GameObject t_HPbar = Instantiate(enemy_HPbarPrefab, t_objects[i].transform.position, Quaternion.identity, transform);
            enemy_HPbarList.Add(t_HPbar);
            enemy_textList.Add(t_HPbar.GetComponentInChildren<Text>());
        
            // �����̴� ������Ʈ���� Slider ������Ʈ�� ã��
            Slider slider = t_HPbar.GetComponentInChildren<Slider>();
            enemy_sliderList.Add(slider);

            // Slider ������Ʈ�� ����� CharStats ��ũ��Ʈ���� ���� ü�� �� �ִ� ü�� ���� ������
            CharStats charStats = t_objects[i].GetComponent<CharStats>();
            enemy_statsList.Add(charStats);
            float curHP = charStats.curHealth;
            float maxHP = charStats.maxHealth;

            // �����̴��� value�� ���� ü�� ������ ����
            float healthRatio = curHP / maxHP;
            slider.value = healthRatio;
        }
    }
    // Update is called once per frame
    void Update()
    {
        for(int i=0; i<enemy_objectList.Count; i++) {

            if (enemy_objectList[i] != null) {
                enemy_HPbarList[i].transform.position =
                enemy_cam.WorldToScreenPoint(enemy_objectList[i].position + new Vector3(0, 1.2f, 0));

                enemy_sliderList[i].value =
                    Mathf.Lerp(enemy_sliderList[i].value
                    , enemy_statsList[i].curHealth / enemy_statsList[i].maxHealth
                    , Time.deltaTime * 25);

                //enemy_textList[i].text = "-" + enemy_statsList[i].t_damage.ToString("0");
                EnableText(enemy_textList[i], enemy_statsList[i].t_damage.ToString("0"));


            }



            // �� ������Ʈ�� �ı��Ǿ��� �� ó��
            if (enemy_objectList[i] == null) {
                // �� ������Ʈ�� �ı��Ǿ����Ƿ� ü�¹ٸ� �ı�
                Destroy(enemy_HPbarList[i]);
                // �ش� ����Ʈ�� �����۵鵵 ����
                enemy_objectList.RemoveAt(i);
                enemy_HPbarList.RemoveAt(i);
                enemy_sliderList.RemoveAt(i);
                enemy_statsList.RemoveAt(i);
            }
        }
    }

    void EnableText(Text hpText, string s_damage)
    {
        hpText.text = s_damage;
        hpText.enabled = true;
    }

}
