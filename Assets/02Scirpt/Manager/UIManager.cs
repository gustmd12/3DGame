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
    GameObject _player;

    TextMeshProUGUI manaText;
    TextMeshProUGUI hpText;
    
    [SerializeField] TextMeshProUGUI[] skillManaText;

    EventBus eventBus;

    SkillManager skillManager;
    
    private void Awake()
    {
        player = FindAnyObjectByType<Player>();
        manaslider = GameObject.Find("ManaBar").GetComponent<Slider>();
        

        hpslider = GameObject.Find("HPBar").GetComponent<Slider>();
        playerManaSlider = GameObject.Find("PlayerManaBar").GetComponent<Slider>();
        playerHPSlider = GameObject.Find("PlayerHPBar").GetComponent<Slider>();


        _player = GameObject.Find("Player");
        skillManager = FindAnyObjectByType<SkillManager>();

        manaText = GameObject.Find("ManaText").GetComponent<TextMeshProUGUI>();
        hpText = GameObject.Find("HpText").GetComponent<TextMeshProUGUI>();
        
        eventBus = FindAnyObjectByType<EventBus>();

        InitStats(player.maxMP,player.maxHP);
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
    }

    private void OnDisable()
    {
        eventBus.OnManaChanged -= UpdateMana;
    }


    void UpdateMana()
    {
        manaslider.value = player.curMP;
        playerManaSlider.value = player.curMP;
        manaText.text = $"{player.curMP}";
    }

    public void InitStats(int maxMP,int maxHP)
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
