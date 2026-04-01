using UnityEngine;

public class FaceTowardsCamera : MonoBehaviour {


    private void LateUpdate() {
        this.transform.forward = -Camera.main.transform.forward;
    }


}
