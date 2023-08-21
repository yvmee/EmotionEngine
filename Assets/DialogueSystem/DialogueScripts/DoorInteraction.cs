using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteraction : MonoBehaviour, IInteractable
{
    public void Interact(GameObject player)
    {
        GetComponent<LocationSwitch>().Teleport(player);
    }
}
