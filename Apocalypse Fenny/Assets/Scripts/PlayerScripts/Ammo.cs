using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Ammo : MonoBehaviour {

    public Text ammo;

	void Start () {
        updateText();
		
	}
	
	void Update () {
        updateText();

    }

    void updateText()
    {
        ammo.text = "Ammo: " + PlayerScript.munizioni.ToString();
    }
}
