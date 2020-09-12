using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour {
    public static UIManager instance;

    private List<UpgradeButton> buttons = new List<UpgradeButton> ();
    public UpgradeButton ButtonPerfab;
    public Transform content;

    private int active = 0;
    // Start is called before the first frame update
    void Start () {
        instance = this;
    }

    public void ShowUpgrades (Node focus) {
        var upgrades = focus.GetUpgradeList ();
        for (int i = 0; i < active; i++) {
            buttons[i].gameObject.SetActive (false);
        }

        if (upgrades.Length < buttons.Count) {
            for (int i = 0; i < upgrades.Length; i++) {
                buttons[i].gameObject.SetActive (true);
                buttons[i].ShowUpgrade (focus, upgrades[i], i);
            }
        } else {
            for (int i = 0; i < buttons.Count; i++) {
                buttons[i].gameObject.SetActive (true);
                buttons[i].ShowUpgrade (focus, upgrades[i], i);
            }

            for (int i = buttons.Count; i < upgrades.Length; i++) {
                var button = CreateButton ();
                button.ShowUpgrade (focus, upgrades[i], i);
            }
        }
        active = upgrades.Length;
    }

    public UpgradeButton CreateButton () {
        var button = Instantiate (ButtonPerfab);
        button.transform.parent = content;
        buttons.Add (button);
        return button;
    }
    // Update is called once per frame
    void Update () {

    }
}
