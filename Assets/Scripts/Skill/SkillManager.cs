using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public SkillData[] skillList;   // ScriptableObject 스킬 데이터의 배열

    // JSON 파일로 스킬 데이터 저장 (선택적)
    public void SaveSkills(string fileName, SkillData skillData) {
        string json = JsonUtility.ToJson(skillData);
        File.WriteAllText(fileName, json);
    }

    // JSON 파일에서 스킬 데이터 불러오기
    public void LoadSkills(string fileName) {
        string json = File.ReadAllText(fileName);
        var data = JsonUtility.FromJson<SkillData>(json);
        // 로드한 데이터를 필요한 배열에 추가 또는 처리
    }
}
