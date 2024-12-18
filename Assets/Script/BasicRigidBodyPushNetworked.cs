using UnityEngine;
using FishNet.Object;
using FishNet.Connection;


public class BasicRigidBodyPushNetworked : NetworkBehaviour
{
    public LayerMask pushLayers;
    private NetworkObject networkObject;
    public bool canPush;
    [Range(0.5f, 5f)] public float strength = 1.1f;

    public override void OnStartClient()
    {
        base.OnStartClient();
        networkObject = GetComponent<NetworkObject>();
        if (networkObject == null)
        {
            Debug.LogError("BasicRigidBodyPushNetworked requires a NetworkObject component.");
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (canPush) PushRigidBodies(hit);
    }

    private void PushRigidBodies(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;
        if (body == null || body.isKinematic) return;



        var bodyLayerMask = 1 << body.gameObject.layer;
        if ((bodyLayerMask & pushLayers.value) == 0) return;

        if (hit.moveDirection.y < -0.3f) return;

        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0.0f, hit.moveDirection.z);
        
        GameObject selectedObject = hit.collider.gameObject;

        // Debug output to check the object being pushed
        Debug.Log($"Attempting to push: {selectedObject.name}");

        TransferObjectOwnership(selectedObject);

        // Apply the push force and take strength into account
        body.AddForce(pushDir * strength, ForceMode.Impulse);
        RemoveTransferObjectOwnership(selectedObject);
    }

	public void TransferObjectOwnership(GameObject selectedObject)
	{
		if (networkObject == null || networkObject.Owner == null)
		{
			Debug.LogWarning("Cannot transfer ownership: No valid owner.");
			return;
		}

		NetworkObject objectNetworkObject = selectedObject.GetComponent<NetworkObject>();
		if (objectNetworkObject != null)
		{
			// Optionally log the current owner
			Debug.Log($"{selectedObject.name} current owner: {objectNetworkObject.Owner?.ClientId}");

			// If the object has an owner, consider how to handle that
			if (objectNetworkObject.Owner != null)
			{
				Debug.LogWarning($"{selectedObject.name} already has an owner. Transferring ownership to the new owner.");
			}

			// Transfer ownership regardless of current ownership
			TransferObjectOwnershipServer(objectNetworkObject, networkObject.Owner);
		}
		else
		{
			Debug.LogError($"{selectedObject.name} does not have a NetworkObject component!");
		}
	}

    public void RemoveTransferObjectOwnership(GameObject selectedObject)
    {
        // Check if networkObject is valid and has an owner
        if (networkObject == null || networkObject.Owner == null)
        {
            return;
        }

        // Get the NetworkObject component from the object
        NetworkObject ObjectNetworkObject = selectedObject.GetComponent<NetworkObject>();
        if (ObjectNetworkObject != null)
        {
            // Transfer ownership server-side
            RemoveTransferObjectOwnershipServer(ObjectNetworkObject);
        }


    }


    [ServerRpc(RequireOwnership = false)]
    private void TransferObjectOwnershipServer(NetworkObject targetObject, NetworkConnection newOwner)
    {
        // Transfer ownership of the targetObject to the newOwner
        targetObject.GiveOwnership(newOwner);
        Debug.Log($"Transfer Complete: {targetObject.name} to {newOwner.ClientId}");
    }

    [ServerRpc(RequireOwnership = false)]
    private void RemoveTransferObjectOwnershipServer(NetworkObject targetObject)
    {
        // Transfer ownership of the targetObject to the newOwner
        targetObject.RemoveOwnership();
        Debug.Log($"Transfer Complete: {targetObject.name}");
    }

}
