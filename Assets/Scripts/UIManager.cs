using UnityEngine;

public class UIManager : MonoBehaviour {


    public static UIManager Instance { get; private set; }


    [SerializeField] private SetPriceMenuUI setPriceMenuUI;
    [SerializeField] private GameObject crosshairUIGameObject;


    private void Awake() {
        Instance = this;
    }

    public void OnPriceTagInteract(ItemSO itemSO) {
        setPriceMenuUI.EnableUI(itemSO);
    }

    public void EnableCrosshair() {
        crosshairUIGameObject.SetActive(true);
    }

    public void DisableCrosshair() {
        crosshairUIGameObject.SetActive(false);
    }


}
