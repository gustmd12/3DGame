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

    GameObject _player;

    TextMeshProUGUI manaText;
    TextMeshProUGUI hpText;
    
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

        playerManaSlider = GameObject.Find("PlayerManaBar").GetComponent<Slider>();
        playerHPSlider = GameObject.Find("PlayerHPBar").GetComponent<Slider>();
        enemyHPSlider = GameObject.Find("EnemyHPBar").GetComponent<Slider>();
        


        _player = GameObject.Find("Player");
        skillManager = FindAnyObjectByType<SkillManager>();

        manaText = GameObject.Find("ManaText").GetComponent<TextMeshProUGUI>();
        hpText = GameObject.Find("HpText").GetComponent<TextMeshProUGUI>();
        
        eventBus = FindAnyObjectByType<EventBus>();

        InitStats(player.maxMP,player.maxHP);
        EnemyInit(enemy.maxHP);
    }

    private void Update()
    {
        //if (_player != null)
        //{
        //    Vector3 PlayerScreenPos = Camera.main.WorldToScreenPoint(_player.transform.position);
        //    playerManaSlider.transform.position = PlayerScreenPos;
        //}
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
