using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastUpdate : MonoBehaviour {
    // Start is called before the first frame update

    void Start () {
        var transforms = GetComponentsInChildren<Transform> ();

        List<GameObject> wereOff = new List<GameObject> ();
        foreach (Transform item in transforms) {
            if (item.transform.gameObject.activeSelf) {
                wereOff.Add (item.gameObject);
                item.gameObject.SetActive (true);
            }
        }

        foreach (var item in wereOff) {
            item.SetActive (false);
        }
    }
}
