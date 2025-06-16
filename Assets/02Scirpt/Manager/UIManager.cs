using System.Runtime.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    Player player;

    Slider hpslider;
    Slider manaslider;
    Slider playerManaSlider;
    Slider playerHPSlider;
    Slider ShieldSlider;
    Slider playerShieldSlider;

    Button menuBTN;

    GameObject ShieldObj;
    GameObject playerShield;

    Image[] skillIcons;

    [SerializeField] GameObject menuUI;

    GameObject _player;

    TextMeshProUGUI manaText;
    TextMeshProUGUI hpText;
    TextMeshProUGUI shieldText;
    
    [SerializeField] TextMeshProUGUI[] skillManaText;

    EventBus eventBus;

    SkillManager skillManager;
    
    private void Awake()
    {
        player = FindAnyObjectByType<Player>();
        
        manaslider = GameObject.Find("ManaBar").GetComponent<Slider>();
        hpslider = GameObject.Find("HPBar").GetComponent<Slider>();
        ShieldSlider = GameObject.Find("ShieldBar").GetComponent<Slider>();
        ShieldObj = ShieldSlider.gameObject;
        playerShieldSlider = GameObject.Find("PlayerShieldBar").GetComponent<Slider>();
        playerShield = playerShieldSlider.gameObject;


        playerManaSlider = GameObject.Find("PlayerManaBar").GetComponent<Slider>();
        playerHPSlider = GameObject.Find("PlayerHPBar").GetComponent<Slider>();

        menuBTN = GameObject.Find("MenuBTN").GetComponent<Button>();
        menuBTN.onClick.AddListener(MenuClick);

        menuUI = GameObject.Find("Menu");
        menuUI.SetActive(false);
        _player = GameObject.Find("Player");
        skillManager = FindAnyObjectByType<SkillManager>();

        manaText = GameObject.Find("ManaText").GetComponent<TextMeshProUGUI>();
        hpText = GameObject.Find("HpText").GetComponent<TextMeshProUGUI>();
        shieldText = GameObject.Find("ShieldText").GetComponent<TextMeshProUGUI>();
        
        eventBus = FindAnyObjectByType<EventBus>();

        InitStats(player.maxMP,player.maxHP);

        ShieldObj.SetActive(false);
        playerShield.SetActive(false);
    }

    private void OnEnable()
    {
        if(eventBus != null)
        {
            eventBus.OnManaChanged += UpdateMana;
        }
    }

    private void OnDisable()
    {
        if(eventBus != null)
        {
            eventBus.OnManaChanged -= UpdateMana;
        }
    }

    public void UpdateShield(float amount)
    {
        float shield = amount;
        if (shield > 0)
        {
            if(!ShieldObj.activeSelf)
                ShieldObj.SetActive(true);
            if(!playerShield.activeSelf)
                playerShield.SetActive(true);
            
            shieldText.text = $"{amount}";
        }
        else
        {
            if(ShieldObj.activeSelf)
                ShieldObj.SetActive(false);
            if(playerShield.activeSelf)
                playerShield.SetActive(false);
        }
    }

    private void UpdateMana()
    {
        manaslider.value = player.curMP;
        playerManaSlider.value = player.curMP;
        manaText.text = $"{player.curMP:F0}";
    }

    private void InitStats(float maxMP,float maxHP)
    {
        manaslider.maxValue = maxMP;
        manaslider.value = maxMP;
        hpslider.maxValue = maxHP;
        hpslider.value = maxHP;

        playerManaSlider.maxValue = maxMP;
        playerManaSlider.value = maxMP;
        playerHPSlider.maxValue = maxHP;
        playerHPSlider.value = maxHP;

        manaText.text = maxMP.ToString();
        hpText.text = maxHP.ToString();
    }


    public void SetMana(int index,SkillBase skills)
    {
        if (index < 0 || index >= skillManaText.Length) return;
        skillManaText[index].text = skills.manaCost.ToString();
    }



    public void MenuClick()
    {
        if(skillManager.SkillonCooldown())
        {
            Debug.Log("스킬 쿨타임중에 열수없습니다");
            return;
        }
        skillManager.isSwappingSkill = true;
        menuUI.SetActive(true);
    }
    
    public void MenuExit()
    {
        skillManager.isSwappingSkill = false;
        menuUI.SetActive(false);
    }
    
}
