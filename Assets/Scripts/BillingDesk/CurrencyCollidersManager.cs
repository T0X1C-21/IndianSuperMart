using UnityEngine;

public class CurrencyCollidersManager : MonoBehaviour {


    [SerializeField] private CurrencyCollider[] currencyColliderTransformArray;


    public Transform GetCorrespondingCurrencyColliderTransform(CurrencyValue currencyValue) {
        foreach(CurrencyCollider currencyCollider in currencyColliderTransformArray) {
            CurrencyValue colliderCurrencyValue = currencyCollider.GetCurrencySO().currencyValue;
            if(currencyValue == colliderCurrencyValue) {
                return currencyCollider.transform;
            }
        }
        return null;
    }

    
}
