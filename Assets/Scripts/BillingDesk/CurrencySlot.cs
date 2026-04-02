using System;
using System.Collections.Generic;
using UnityEngine;

public class CurrencySlot : MonoBehaviour, IInteractableObject {


    public event EventHandler<OnGivingAmountRemovedEventArgs> OnGivingAmountRemoved;
    public class OnGivingAmountRemovedEventArgs : EventArgs {
        public int givingAmountRemoved;
    }

    [SerializeField] private CurrencyCollidersManager currencyCollidersManager;


    private float stackOffset;
    private CurrencySO currentCurrencySO;
    private int numberOfCurrencies = 0;
    private List<Currency> addedCurrencies = new List<Currency>();


    public void AddCurrency(Currency currency) {
        if (currentCurrencySO == null) {
            currentCurrencySO = currency.GetCurrencySO();
            stackOffset = currency.GetCurrencySO().stackOffset;
        }

        if(currency.GetCurrencySO().currencyValue == currentCurrencySO.currencyValue) {
            addedCurrencies.Add(currency);
        } else {
            Debug.LogError($"Doesn't match current currency value in {this.name}");
        }
    }

    public CurrencyValue GetCurrentCurrencyValue() {
        if(currentCurrencySO == null) {
            return CurrencyValue.Null;
        }
        return currentCurrencySO.currencyValue;
    }

    public Vector3 GetPositionToAddCurrency(out Transform parentTransform) {
        parentTransform = this.transform;
        return (this.transform.position + new Vector3(0f, numberOfCurrencies++ * stackOffset, 0f));
    }

    public void Interact() {
        TakeCurrencyOut();
    }

    private void TakeCurrencyOut() {
        if(currentCurrencySO == null) {
            return;
        }

        Currency topCurrency = addedCurrencies[numberOfCurrencies - 1];
        addedCurrencies.Remove(topCurrency);

        OnGivingAmountRemoved?.Invoke(this, new OnGivingAmountRemovedEventArgs {
            givingAmountRemoved = topCurrency.GetCurrencySO().currencyAmount
        });

        Transform targetTransform = currencyCollidersManager.GetCorrespondingCurrencyColliderTransform
            (currentCurrencySO.currencyValue);

        StartCoroutine(topCurrency.AnimateCurrencyToDestroy(targetTransform.position, Quaternion.identity));

        numberOfCurrencies--;
        // All the currency is taken out
        if(numberOfCurrencies == 0) {
            currentCurrencySO = null;
        }
    }

    public void ResetCurrencies() {

    }

    
}
