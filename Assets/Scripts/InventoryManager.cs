using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private GameObject _panel;
    [SerializeField] private Transform _inventoryPanel;
    [SerializeField] private List<InventorySlot> slots = new List<InventorySlot>();

    void Start()
    {
        _panel.SetActive(false);
        for(int i = 0; i < _inventoryPanel.childCount;i++)
        {
            if (_inventoryPanel.GetChild(i).GetComponent<InventorySlot>() != null)
            {
                slots.Add(_inventoryPanel.GetChild(i).GetComponent<InventorySlot>());
            }
        }
    }
    private void Awake()
    {
        _panel.SetActive(true);
    }

    public void InventoryShow()
    {
        if (_panel.activeSelf == false)
        {
            _panel.SetActive(true);
        }
        else if(_panel.activeSelf == true)
        {
            _panel.SetActive(false);
        }
    }

    public void AddItem(ItemScriptableObjects _item , int _amount)
    {
        foreach (InventorySlot slot in slots)
        {
            if (slot.ItemScriptableObjects == _item)
            {
                slot.Amount += _amount;
                slot.ItemAmountText.text = slot.Amount.ToString();
                slot.SetIcon(_item.Icon);
                return;
            }
        }
        foreach(InventorySlot slot in slots)
        {
            if (slot.IsEmpty == true)
            {;
                slot.ItemScriptableObjects = _item;
                slot.Amount = _amount;
                slot.IsEmpty = false;
                slot.SetIcon(_item.Icon);
                slot.ItemAmountText.text = _amount.ToString();
                break;
            }
        }
    }

}
