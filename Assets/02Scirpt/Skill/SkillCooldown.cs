using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillCooldown : MonoBehaviour
{
    [SerializeField] Image[] CooldownImg;
    [SerializeField] TextMeshProUGUI[] Cooldowntext;
    public void CoolDownUpdate(int skillIndex, float cooldawnRatio, float remain)
    {
        if (skillIndex < 0 || skillIndex >= CooldownImg.Length) return;

        CooldownImg[skillIndex].fillAmount = cooldawnRatio;

        if(Cooldowntext != null)
        {
            if (remain > 0f)
            {
                Cooldowntext[skillIndex].text = Mathf.CeilToInt(remain).ToString(); // 정수로 보이게
            }
            else
                Cooldowntext[skillIndex].text = "";
        }

    }
}
