using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    Player player;
    Slider hpslider;
    Slider manaslider;
    Slider playerManaSlider;
    GameObject _player;

    TextMeshProUGUI manaText;
    TextMeshProUGUI hpText;
    
    [SerializeField] TextMeshProUGUI[] skillManaText;
    

    SkillManager skillManager;
    
    private void Awake()
    {
        player = FindAnyObjectByType<Player>();
        manaslider = GameObject.Find("ManaBar").GetComponent<Slider>();
        hpslider = GameObject.Find("HPBar").GetComponent<Slider>();
        playerManaSlider = GameObject.Find("PlayerManaBar").GetComponent<Slider>();
        _player = GameObject.Find("Player");
        skillManager = FindAnyObjectByType<SkillManager>();

        manaText = GameObject.Find("ManaText").GetComponent<TextMeshProUGUI>();
        hpText = GameObject.Find("HpText").GetComponent<TextMeshProUGUI>();
        
        

        InitMana(player.maxMP);
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
        player.OnManaChanged += UpdateMana;
    }

    private void OnDisable()
    {
        player.OnManaChanged -= UpdateMana;
    }


    void UpdateMana()
    {
        manaslider.value = player.curMP;
        playerManaSlider.value = player.curMP;
        manaText.text = $"{player.curMP}";
    }

    public void InitMana(int maxMP)
    {
        manaslider.maxValue = maxMP;
        manaslider.value = maxMP;
        playerManaSlider.maxValue = maxMP;
        playerManaSlider.value = maxMP;
        manaText.text = maxMP.ToString();

    }


    public void SetMana(int index,SkillBase skills)
    {
        skillManaText[index].text = skills.manaCost.ToString();
    }
}
