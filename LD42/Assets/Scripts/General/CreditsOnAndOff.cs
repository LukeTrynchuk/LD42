using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsOnAndOff : MonoBehaviour {

    public void EnableCredits(GameObject menuPanel)
    {
        menuPanel.SetActive(false);
        this.transform.Find("Credits").gameObject.SetActive(true);
    }

    public void DisableCredits(GameObject menuPanel)
    {
        menuPanel.SetActive(true);
        this.transform.Find("Credits").gameObject.SetActive(false);
    }
}
