using UnityEngine;


public enum CurrencyType {
    Coin,
    Note
}

public enum CurrencyValue {
    Null,
    One,
    Five,
    Ten,
    Twenty,
    TwentyFive,
    Fifty,
    SeventyFive,
    Hundred,
    TwoHundred,
    FiveHundred
}


[CreateAssetMenu(fileName = "CurrencySO", menuName = "ScriptableObject/CurrencySO")]
public class CurrencySO : ScriptableObject {
    

    public GameObject gameObject;
    public int currencyAmount;
    public CurrencyType currencyType;
    public CurrencyValue currencyValue;
    public float stackOffset;


}
