// WindManager.cs
// Beispiel-Script: Steuert ein oder mehrere Windräder von außen.
// Simuliert wechselnde Windstärke und zeigt die öffentliche API.

using UnityEngine;

public class WindManager : MonoBehaviour
{
    [Header("Windrad Referenz")]
    [SerializeField] private WindmillController windmill;

    [Header("Wind-Simulation")]
    [Tooltip("Minimale Windgeschwindigkeit (Grad/Sek)")]
    [SerializeField] private float minWindSpeed = 30f;

    [Tooltip("Maximale Windgeschwindigkeit (Grad/Sek)")]
    [SerializeField] private float maxWindSpeed = 180f;

    [Tooltip("Wie oft der Wind seine Stärke ändert (Sekunden)")]
    [SerializeField] private float windChangeInterval = 5f;

    private float _timer;

    private void Start()
    {
        if (windmill == null)
        {
            Debug.LogError("[WindManager] Kein WindmillController zugewiesen!");
            return;
        }

        ApplyRandomWind();
    }

    private void Update()
    {
        if (windmill == null) return;

        _timer += Time.deltaTime;
        if (_timer >= windChangeInterval)
        {
            _timer = 0f;
            ApplyRandomWind();
        }
    }

    // -------------------------------------------------------------------------
    // Öffentliche Steuermethoden
    // -------------------------------------------------------------------------

    /// <summary>Setzt Windgeschwindigkeit direkt (z.B. aus einem anderen System).</summary>
    public void SetWindSpeed(float speed)
    {
        if (windmill == null) return;

        if (speed <= 0f)
            windmill.StopRotating();
        else
            windmill.SetSpeedAndRotate(speed);
    }

    /// <summary>Stoppt alle Windräder (z.B. bei Sturm-Abschaltung).</summary>
    public void EmergencyStop()
    {
        windmill?.StopRotating();
        Debug.Log("[WindManager] Notabschaltung!");
    }

    // -------------------------------------------------------------------------
    // Intern
    // -------------------------------------------------------------------------

    private void ApplyRandomWind()
    {
        float newSpeed = Random.Range(minWindSpeed, maxWindSpeed);
        bool isWindy   = Random.value > 0.2f; // 80% Chance auf Wind

        if (isWindy)
        {
            windmill.SetSpeedAndRotate(newSpeed);
            Debug.Log($"[WindManager] Wind: {newSpeed:F1}°/s");
        }
        else
        {
            windmill.StopRotating();
            Debug.Log("[WindManager] Windstille – Windrad stoppt.");
        }
    }
}
