using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// script on Pause, Winner Panel 

public class PanelUp : MonoBehaviour
{
    public GameObject Panel; // inspector

    #region PanelUp() : Open Panel : Animation

        public void PanelUpp()
        {
            Panel.GetComponent<Animator>().SetBool("close", false);
        }

    #endregion
}
