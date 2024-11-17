using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftEngine : MonoBehaviour
{
    // Reference to floor GameObjects
    [SerializeField] private GameObject lantai1;
    [SerializeField] private GameObject lantai2;
    [SerializeField] private GameObject lantai3;
    [SerializeField] private GameObject lantai4;
    [SerializeField] private GameObject lantai5;
    [SerializeField] private GameObject rooftop;

    // Reference to the player
    [SerializeField] private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
        {
            Debug.LogError("Player GameObject is not assigned!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Teleport player to the position of the selected floor
    public void TeleportToFloor1()
    {
        if (lantai1 != null && player != null)
        {
            player.transform.position = lantai1.transform.position;
            Debug.Log($"Teleported to {lantai1.name}");
        }
        else
        {
            Debug.LogError("Floor or Player GameObject is not assigned!");
        }
    }
    public void TeleportToFloor2()
    {
        if (lantai2 != null && player != null)
        {
            player.transform.position = lantai2.transform.position;
            Debug.Log($"Teleported to {lantai2.name}");
        }
        else
        {
            Debug.LogError("Floor or Player GameObject is not assigned!");
        }
    }
    public void TeleportToFloor3()
    {
        if (lantai3 != null && player != null)
        {
            player.transform.position = lantai3.transform.position;
            Debug.Log($"Teleported to {lantai3.name}");
        }
        else
        {
            Debug.LogError("Floor or Player GameObject is not assigned!");
        }
    }
    public void TeleportToFloor4()
    {
        if (lantai4 != null && player != null)
        {
            player.transform.position = lantai4.transform.position;
            Debug.Log($"Teleported to {lantai4.name}");
        }
        else
        {
            Debug.LogError("Floor or Player GameObject is not assigned!");
        }
    }
    public void TeleportToFloor5()
    {
        if (lantai5 != null && player != null)
        {
            player.transform.position = lantai5.transform.position;
            Debug.Log($"Teleported to {lantai5.name}");
        }
        else
        {
            Debug.LogError("Floor or Player GameObject is not assigned!");
        }
    }
    public void TeleportToRooftop()
    {
        if (rooftop != null && player != null)
        {
            player.transform.position = rooftop.transform.position;
            Debug.Log($"Teleported to {rooftop.name}");
        }
        else
        {
            Debug.LogError("Floor or Player GameObject is not assigned!");
        }
    }
}
