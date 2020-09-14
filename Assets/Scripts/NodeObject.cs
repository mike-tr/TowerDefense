using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeObject : MonoBehaviour {
    private static int NEXT_ID = 1;
    public int id;
    public Tower[] upgradeList;

    public void SetUniqueID () {
        if (id > 0) {
            id = NEXT_ID;
            NEXT_ID++;
        }
    }
}
