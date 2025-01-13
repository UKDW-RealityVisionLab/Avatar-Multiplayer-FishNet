using UnityEngine;

public class ToggleScripts : MonoBehaviour
{
    public MonoBehaviour[] scriptsToEnable; // Daftar script yang ingin diaktifkan
    public MonoBehaviour[] scriptsToDisable; // Daftar script yang ingin dinonaktifkan

    public void Toggle()
    {
        // Aktifkan script yang ada di scriptsToEnable
        foreach (MonoBehaviour script in scriptsToEnable)
        {
            if (script != null)
                script.enabled = true;
        }

        // Nonaktifkan script yang ada di scriptsToDisable
        foreach (MonoBehaviour script in scriptsToDisable)
        {
            if (script != null)
                script.enabled = false;
        }
    }
}
