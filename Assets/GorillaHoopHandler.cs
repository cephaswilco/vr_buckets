using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GorillaHoopHandler : MonoBehaviour
{
    public int HoopID;

    [SerializeField]
    GameObject Disabler;

    public void EnableHoop()
    {
        Disabler.SetActive(false);
    }
    public void DisableHoop()
    {
        Disabler.SetActive(true);
    }
}
