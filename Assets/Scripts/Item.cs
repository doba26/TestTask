using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private ItemScriptableObjects _itemSO;
    [SerializeField] private int _amount;
    public int Amount { get => _amount; }
    public ItemScriptableObjects ItemSO { get => _itemSO; }
}
