using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public SkillData[] skillList;   // ScriptableObject ��ų �������� �迭

    // JSON ���Ϸ� ��ų ������ ���� (������)
    public void SaveSkills(string fileName, SkillData skillData) {
        string json = JsonUtility.ToJson(skillData);
        File.WriteAllText(fileName, json);
    }

    // JSON ���Ͽ��� ��ų ������ �ҷ�����
    public void LoadSkills(string fileName) {
        string json = File.ReadAllText(fileName);
        var data = JsonUtility.FromJson<SkillData>(json);
        // �ε��� �����͸� �ʿ��� �迭�� �߰� �Ǵ� ó��
    }
}
