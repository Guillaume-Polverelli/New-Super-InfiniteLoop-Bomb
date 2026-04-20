using UnityEngine;
using TMPro;

public class UIItemCounter : MonoBehaviour
{
    [SerializeField] private PlayerInventory _inventory;
    [SerializeField] private TextMeshProUGUI _text;

    private void OnEnable()
    {
        _inventory.OnItemCountChanged += UpdateText;
    }

    private void OnDestroy()
    {
        _inventory.OnItemCountChanged -= UpdateText;
    }

    private void UpdateText(int value)
    {
        _text.text = value.ToString();
    }
}
