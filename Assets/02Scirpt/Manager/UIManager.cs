using System.Runtime.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    Player player;
    Enemy enemy;

    Slider hpslider;
    Slider manaslider;
    Slider playerManaSlider;
    Slider playerHPSlider;
    Slider enemyHPSlider;
    Slider ShieldSlider;

    GameObject ShieldObj;

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
        enemy = FindAnyObjectByType<Enemy>();
        manaslider = GameObject.Find("ManaBar").GetComponent<Slider>();
        hpslider = GameObject.Find("HPBar").GetComponent<Slider>();
        ShieldSlider = GameObject.Find("ShieldBar").GetComponent<Slider>();
        ShieldObj = ShieldSlider.gameObject;
        

        playerManaSlider = GameObject.Find("PlayerManaBar").GetComponent<Slider>();
        playerHPSlider = GameObject.Find("PlayerHPBar").GetComponent<Slider>();
        enemyHPSlider = GameObject.Find("EnemyHPBar").GetComponent<Slider>();

        _player = GameObject.Find("Player");
        skillManager = FindAnyObjectByType<SkillManager>();

        manaText = GameObject.Find("ManaText").GetComponent<TextMeshProUGUI>();
        hpText = GameObject.Find("HpText").GetComponent<TextMeshProUGUI>();
        shieldText = GameObject.Find("ShieldText").GetComponent<TextMeshProUGUI>();
        
        eventBus = FindAnyObjectByType<EventBus>();

        InitStats(player.maxMP,player.maxHP);
        EnemyInit(enemy.maxHP);

        ShieldObj.SetActive(false);
    }

    private void Update()
    {
        
    }

    private void OnEnable()
    {
        eventBus.OnManaChanged += UpdateMana;
        eventBus.OnHPChanged += UpdateHP;
    }

    private void OnDisable()
    {
        eventBus.OnManaChanged -= UpdateMana;
        eventBus.OnHPChanged -= UpdateHP;
    }

    public void UpdateShield(float amount)
    {
        float shield = amount;
        if (shield > 0)
        {
            if(!ShieldObj.activeSelf)
                ShieldObj.SetActive(true);

            //ShieldSlider.value = Mathf.Clamp01(amount / 5);
            shieldText.text = $"{amount}";
        }
        else
        {
            if(ShieldObj.activeSelf)
                ShieldObj.SetActive(false);
        }
    }

    void UpdateHP()
    {
        enemyHPSlider.value = enemy.curHP;
        Debug.Log("enemy hp");
    }

    void UpdateMana()
    {
        manaslider.value = player.curMP;
        playerManaSlider.value = player.curMP;
        manaText.text = $"{player.curMP:F0}";
    }

    public void EnemyInit(float maxHP)
    {
        enemyHPSlider.maxValue = maxHP;
        enemyHPSlider.value = maxHP;
    }

    public void InitStats(float maxMP,float maxHP)
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
        skillManaText[index].text = skills.manaCost.ToString();
    }
}
