using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Object;
using FishNet.Connection;
using Unity.Mathematics;

public class CharacterSelection : NetworkBehaviour
{
    [SerializeField] private List<GameObject> character = new List<GameObject>();
    [SerializeField] private GameObject characterSelectorPanel;
    [SerializeField] private GameObject canvasObject;

    public override void OnStartClient()
    {
        base.OnStartClient();

        if (!base.IsOwner)
            canvasObject.SetActive(false);
    }

    public void SpawnBoy()
    {
        characterSelectorPanel.SetActive(false);
        SpawnCharacter(0);
    }

    public void SpawnGirl()
    {
        characterSelectorPanel.SetActive(false);
        SpawnCharacter(1);
    }

    private void SpawnCharacter(int spawnIndex)
    {
        if (base.IsOwner)
        {
            SpawnCharacterServerRpc(spawnIndex, Owner);
        }
    }

    [ServerRpc(RequireOwnership = false)]
    private void SpawnCharacterServerRpc(int spawnIndex, NetworkConnection conn)
    {
        GameObject player = Instantiate(character[spawnIndex], SpawnPoint.instance.transform.position, Quaternion.identity);
        Spawn(player, conn);
    }
}