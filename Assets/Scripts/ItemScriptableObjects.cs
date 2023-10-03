using UnityEngine;

[CreateAssetMenu(fileName = "Item",menuName ="Inventory/Items/New Item")]
public class ItemScriptableObjects : ScriptableObject
{
    [SerializeField]private string _name;
    [SerializeField] private int _maximumAmount;
    [SerializeField] private GameObject _itemPrefab;
    [SerializeField] private Sprite _icon;
    [SerializeField] private int _idItem;
    public Sprite Icon
    {get => _icon; }

}
