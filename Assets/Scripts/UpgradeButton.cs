using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour {
    // Start is called before the first frame update

    [SerializeField] private Image icon;
    private Node current;
    private Tower currentUpgrade;
    private int id;
    void Start () {

    }

    public void ShowUpgrade (Node focus, Tower upgrade, int id) {
        this.current = focus;
        this.currentUpgrade = upgrade;
        this.id = id;

        var iconData = upgrade.GetUpgradeIcon ();
        icon.color = iconData.Item2;
        icon.sprite = iconData.Item1;
    }

    public void ApplyUpgrade () {
        // on click
        current.HandleUpgrade (id);
        UIManager.instance.ShowUpgrades (current);
    }

    // Update is called once per frame
    void Update () {

    }
}
