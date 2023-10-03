using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] public ItemScriptableObjects ItemScriptableObjects;
    [SerializeField] public int Amount;
    [SerializeField] public bool IsEmpty;
    [SerializeField] private GameObject _icon;
    [SerializeField] private TMP_Text _itemAmountText;
    public TMP_Text ItemAmountText { get => _itemAmountText; }
   
   
    private void Awake()
    {
        _icon = transform.GetChild(0).gameObject;
        _itemAmountText = transform.GetChild(1).GetComponent<TMP_Text>();
    }

    public void SetIcon(Sprite icon)
    {
        _icon.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        _icon.GetComponent<Image>().sprite = icon;
    }

    private void Update()
    {
        if(Amount == 1)
        {
            _itemAmountText.enabled = false;
        }
        else
        {
            _itemAmountText.enabled = true;
        }
    }
}
