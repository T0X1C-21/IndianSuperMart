using System.Collections;
using UnityEngine;

public class Currency : MonoBehaviour {


    [SerializeField] private CurrencySO currencySO;
    [SerializeField] private AnimationCurve verticalAnimationCurve;


    public IEnumerator AnimateCurrencyToSlot(Vector3 targetPosition, Quaternion targetRotation) {
        Vector3 currencyStartPosition = this.transform.position;
        Quaternion currencyStartRotation = this.transform.rotation;
        float animationSpeed = 3f;
        float t = 0f;
        while(t <= 1f) {
            t += Time.deltaTime * animationSpeed;
            Vector3 linearLerpedPosition = Vector3.Lerp(currencyStartPosition, targetPosition, t);
            Vector3 extraVerticalPosition = new Vector3(0f, verticalAnimationCurve.Evaluate(t), 0f);
            this.transform.position = linearLerpedPosition + extraVerticalPosition;
            this.transform.localRotation = Quaternion.Slerp(currencyStartRotation, targetRotation, t);
            yield return null;
        }
        this.transform.SetPositionAndRotation(targetPosition, targetRotation);
    }

    public CurrencySO GetCurrencySO() {
        return currencySO;
    }

    
}
