using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;


namespace Assets.Scripts.Economy.BuyMenu
{
	public class UIBuyMenuSlot : MonoBehaviour
	{
        [SerializeField] private Image _icon;
		[SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private TextMeshProUGUI _priceText;
		[SerializeReference] private BuyMenuItemBase _item;
        [SerializeField] private Button _button;

        public static event UnityAction<IBuyMenuItem> SlotClicked;

        private void Awake()
        {
            _icon.sprite = _item.Sprite ?? null;
            _nameText.text = _item.Name;
            _priceText.text = _item.Price.ToString();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(OnButtonClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveAllListeners();
        }

        private void OnButtonClick()
        {
            SlotClicked?.Invoke(_item);
        }
    }
}