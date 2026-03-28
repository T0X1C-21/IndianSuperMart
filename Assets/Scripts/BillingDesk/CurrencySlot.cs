using System.Collections.Generic;
using UnityEngine;

public class CurrencySlot : MonoBehaviour {


    private float stackOffset;
    private CurrencyValue currentCurrencyValue;
    private int numberOfCurrencies = 0;
    private List<Currency> addedCurrencies = new List<Currency>();


    public void AddCurrency(Currency currency) {
        if (currentCurrencyValue == CurrencyValue.Null) {
            currentCurrencyValue = currency.GetCurrencySO().currencyValue;
            stackOffset = currency.GetCurrencySO().stackOffset;
        }

        if(currency.GetCurrencySO().currencyValue == currentCurrencyValue) {
            addedCurrencies.Add(currency);
        } else {
            Debug.LogError($"Doesn't match current currency value in {this.name}");
        }
    }

    public CurrencyValue GetCurrentCurrencyValue() {
        return currentCurrencyValue;
    }

    public Vector3 GetPositionToAddCurrency(out Transform parentTransform) {
        parentTransform = this.transform;
        return (this.transform.position + new Vector3(0f, numberOfCurrencies++ * stackOffset, 0f));
    }

    public void ResetCurrencies() {

    }

    
}
