using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeBar : MonoBehaviour {
    public Transform life;

    public void SetLife (float value) {
        value = Mathf.Clamp01 (value);
        life.localScale = new Vector3 (value, 1, 1);
    }
}
