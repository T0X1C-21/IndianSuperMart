using System;
using UnityEngine;

public class CurrencySlotsManager : MonoBehaviour {


    public event EventHandler<OnGivingAmountAddedEventArgs> OnGivingAmountAdded;
    public class OnGivingAmountAddedEventArgs : EventArgs {
        public int givingAmountAdded;
    }


    [SerializeField] private CurrencySlot[] coinCurrencySlots;
    [SerializeField] private CurrencySlot[] noteCurrencySlots;


    public Vector3 AddCurrencyAndGetPosition(Currency currency, out Transform parentTransform) {
        CurrencyType currencyType = currency.GetCurrencySO().currencyType;

        OnGivingAmountAdded.Invoke(this, new OnGivingAmountAddedEventArgs {
            givingAmountAdded = currency.GetCurrencySO().currencyAmount
        });

        Vector3 positionToReturn;
        if (currencyType == CurrencyType.Coin) {
            positionToReturn = GetMatchingCurrencyCoinSlot(currency, out parentTransform);

            if (positionToReturn == Vector3.zero) {
                positionToReturn = GetFreeCurrencyCoinSlot(currency, out parentTransform);
            }
        } else {
            positionToReturn = GetMatchingCurrencyNoteSlot(currency, out parentTransform);
            
            if (positionToReturn == Vector3.zero) {
                positionToReturn = GetFreeCurrencyNoteSlot(currency, out parentTransform);
            }
        }

        return positionToReturn;
    }

    private Vector3 GetMatchingCurrencyCoinSlot(Currency currency, out Transform parentTransform) {
        foreach(CurrencySlot currencySlot in coinCurrencySlots) {
            if(currencySlot.GetCurrentCurrencyValue() == currency.GetCurrencySO().currencyValue) {
                currencySlot.AddCurrency(currency);
                return currencySlot.GetPositionToAddCurrency(out parentTransform);
            }
        }

        parentTransform = null;
        return Vector3.zero;
    }

    private Vector3 GetMatchingCurrencyNoteSlot(Currency currency, out Transform parentTransform) {
        foreach(CurrencySlot currencySlot in noteCurrencySlots) {
            if(currencySlot.GetCurrentCurrencyValue() == currency.GetCurrencySO().currencyValue) {
                currencySlot.AddCurrency(currency);
                return currencySlot.GetPositionToAddCurrency(out parentTransform);
            }
        }

        parentTransform = null;
        return Vector3.zero;
    }

    private Vector3 GetFreeCurrencyCoinSlot(Currency currency, out Transform parentTransform) {
        foreach(CurrencySlot currencySlot in coinCurrencySlots) {
            if(currencySlot.GetCurrentCurrencyValue() == CurrencyValue.Null) {
                currencySlot.AddCurrency(currency);
                return currencySlot.GetPositionToAddCurrency(out parentTransform);
            }
        }

        parentTransform = null;
        return Vector3.zero;
    }

    private Vector3 GetFreeCurrencyNoteSlot(Currency currency, out Transform parentTransform) {
        foreach(CurrencySlot currencySlot in noteCurrencySlots) {
            if(currencySlot.GetCurrentCurrencyValue() == CurrencyValue.Null) {
                currencySlot.AddCurrency(currency);
                return currencySlot.GetPositionToAddCurrency(out parentTransform);
            }
        }

        parentTransform = null;
        return Vector3.zero;
    }

    
}
