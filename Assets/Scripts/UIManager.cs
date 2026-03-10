using UnityEngine;

public class UIManager : MonoBehaviour {


    public static UIManager Instance { get; private set; }


    [SerializeField] private SetPriceMenuUI setPriceMenuUI;


    private void Awake() {
        Instance = this;
    }

    public void OnPriceTagInteract(ItemSO itemSO) {
        setPriceMenuUI.EnableUI(itemSO);
    }


}
