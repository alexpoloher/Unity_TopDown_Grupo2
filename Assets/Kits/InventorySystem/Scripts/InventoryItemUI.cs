using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemUI : MonoBehaviour
{
    [SerializeField] InventoryItemDefinition definition;
    Image image;
    TextMeshProUGUI text;
    Button[] buttons;

    enum ButtonAction
    {
        Discard,
        Use,
        Give,
        Sell
    }
    private void Awake()
    {
        buttons = GetComponentsInChildren<Button>();
        image = GetComponentInChildren<Image>();
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        buttons[(int)ButtonAction.Discard].onClick.AddListener(OnDiscard);
        buttons[(int)ButtonAction.Use].onClick.AddListener(OnUse);
        buttons[(int)ButtonAction.Give].onClick.AddListener(OnGive);
        buttons[(int)ButtonAction.Sell].onClick.AddListener(OnSell);
    }

    private void OnDisable()
    {
        buttons[(int)ButtonAction.Discard].onClick.RemoveListener(OnDiscard);
        buttons[(int)ButtonAction.Use].onClick.RemoveListener(OnUse);
        buttons[(int)ButtonAction.Give].onClick.RemoveListener(OnGive);
        buttons[(int)ButtonAction.Sell].onClick.RemoveListener(OnSell);
    }

    public void Init(InventoryItemDefinition definition) {
        text.text = definition.itemName;
        image.sprite = definition.sprite;

    }

    private void Start()
    {
        Init(definition);
    }

    private void OnDiscard()
    {
        Debug.Log("Discard", this);
    }

    private void OnUse()
    {
        Debug.Log("Use", this);
    }

    private void OnGive()
    {
        Debug.Log("Give", this);
    }

    private void OnSell()
    {
        Debug.Log("Sell", this);

    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
