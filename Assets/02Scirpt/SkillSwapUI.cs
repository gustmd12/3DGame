using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillSwapUI : MonoBehaviour
{
    [SerializeField] private Button[] skillSlotButtons;         
    [SerializeField] private Image[] skillSlotIcons;            
    [SerializeField] private Image[] skillSlotIcons2;            
    [SerializeField] private TextMeshProUGUI[] skillManaTexts;
    [SerializeField] Outline[] skilloutlines;

    OutlineBlink outlineBlink;

    [SerializeField] private SkillManager skillManager;

    private int selectedSlotIndex = -1;

    private Dictionary<KeyCode, int> keyToSlotIndex = new()
    {
        { KeyCode.Q, 0 },
        { KeyCode.W, 1 },
        { KeyCode.E, 2 },
        { KeyCode.R, 3 }
    };

    private void Awake()
    {
        outlineBlink = FindAnyObjectByType<OutlineBlink>();
    }

    private void Start()
    {
        for (int i = 0; i < skillSlotButtons.Length; i++)
        {
            int index = i;
            skillSlotButtons[i].onClick.AddListener(() => OnSkillSlotButtonClicked(index));
        }

        UpdateSkillUI();
    }

    private void Update()
    {
        if (selectedSlotIndex == -1) return;

        foreach (var pair in keyToSlotIndex)
        {
            if (Input.GetKeyDown(pair.Key))
            {
                int targetIndex = pair.Value;
                SwapSkills(selectedSlotIndex, targetIndex);
                selectedSlotIndex = -1;
                outlineBlink.ClearSelect();
                break;
            }
        }
    }

    private void OnSkillSlotButtonClicked(int index)
    {
        selectedSlotIndex = index;
        outlineBlink.SelectBTN(index);
        Debug.Log($"스킬 슬롯 {index} 선택됨");
    }

    private void SwapSkills(int indexA, int indexB)
    {
        var temp = skillManager.euqippedSkills[indexA];
        skillManager.euqippedSkills[indexA] = skillManager.euqippedSkills[indexB];
        skillManager.euqippedSkills[indexB] = temp;

        UpdateSkillUI();
        Debug.Log($"스킬 {indexA} <-> {indexB} 교체 완료");
    }

    private void UpdateSkillUI()
    {
        for (int i = 0; i < skillSlotIcons.Length; i++)
        {
            SkillBase skill = skillManager.euqippedSkills[i];
            if (skill != null)
            {
                skillSlotIcons[i].sprite = skill.skillIcon;
                skillSlotIcons2[i].sprite = skill.skillIcon;
                skillManaTexts[i].text = skill.manaCost.ToString();
            }
        }
    }
}
