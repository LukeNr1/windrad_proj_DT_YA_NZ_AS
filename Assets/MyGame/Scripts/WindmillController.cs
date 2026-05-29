// WindmillController.cs
// Hauptscript für das Windrad – steuert die State Machine
// 
// Setup in Unity:
//   1. Dieses Script auf das Windrad-GameObject ziehen
//   2. Im Inspector das "Rotor Transform" Feld auf den Rotor (Flügel) setzen
//   3. Optional: TargetSpeed, Acceleration und BrakeForce im Inspector anpassen

using UnityEngine;

public class WindmillController : MonoBehaviour
{
    // -------------------------------------------------------------------------
    // Inspector-Felder
    // -------------------------------------------------------------------------

    [Header("Referenzen")]
    [Tooltip("Das Transform des Rotors (Flügel), das sich drehen soll")]
    [SerializeField] private Transform rotorTransform;

    [Header("Geschwindigkeit (Grad/Sekunde)")]
    [Tooltip("Zielgeschwindigkeit des Rotors")]
    [SerializeField] private float targetSpeed = 90f;

    [Tooltip("Wie schnell die Drehzahl hoch-/runtergefahren wird")]
    [SerializeField] private float acceleration = 30f;

    [Tooltip("Bremskraft wenn das Windrad stoppt")]
    [SerializeField] private float brakeForce = 60f;

    [Header("Debug")]
    [SerializeField] private bool startRotatingOnPlay = false;

    // -------------------------------------------------------------------------
    // Properties (öffentlich lesbar, für States und externe Scripts)
    // -------------------------------------------------------------------------

    /// <summary>Das Transform des Rotors.</summary>
    public Transform RotorTransform => rotorTransform;

    /// <summary>Aktuelle Ist-Drehgeschwindigkeit in Grad/Sekunde.</summary>
    public float CurrentSpeed { get; set; }

    /// <summary>
    /// Ziel-Drehgeschwindigkeit in Grad/Sekunde.
    /// Kann von externen Scripts gesetzt werden.
    /// </summary>
    public float TargetSpeed
    {
        get => targetSpeed;
        set => targetSpeed = Mathf.Max(0f, value); // Keine negativen Werte
    }

    /// <summary>Beschleunigung in Grad/Sekunde².</summary>
    public float Acceleration => acceleration;

    /// <summary>Bremskraft in Grad/Sekunde².</summary>
    public float BrakeForce => brakeForce;

    /// <summary>Aktueller State (read-only für externe Scripts).</summary>
    public IWindmillState CurrentState { get; private set; }

    // -------------------------------------------------------------------------
    // Vordefinierte State-Instanzen (wiederverwendet, kein GC-Druck)
    // -------------------------------------------------------------------------

    public readonly WindmillStoppedState  StateStoped   = new WindmillStoppedState();
    public readonly WindmillRotatingState StateRotating = new WindmillRotatingState();

    // -------------------------------------------------------------------------
    // Unity Lifecycle
    // -------------------------------------------------------------------------

    private void Awake()
    {
        if (rotorTransform == null)
        {
            Debug.LogWarning("[WindmillController] Kein RotorTransform gesetzt! " +
                             "Bitte im Inspector zuweisen.");
        }
    }

    private void Start()
    {
        // Initialzustand setzen
        if (startRotatingOnPlay)
            TransitionTo(StateRotating);
        else
            TransitionTo(StateStoped);
    }

    private void Update()
    {
        CurrentState?.Update(this);
    }

    // -------------------------------------------------------------------------
    // Öffentliche API
    // -------------------------------------------------------------------------

    /// <summary>Wechselt in einen neuen State.</summary>
    public void TransitionTo(IWindmillState newState)
    {
        if (newState == CurrentState) return;

        CurrentState?.Exit(this);
        CurrentState = newState;
        CurrentState.Enter(this);
    }

    /// <summary>Startet die Rotation mit der aktuellen TargetSpeed.</summary>
    public void StartRotating() => TransitionTo(StateRotating);

    /// <summary>Stoppt die Rotation (fährt sanft auf 0 herunter).</summary>
    public void StopRotating() => TransitionTo(StateStoped);

    /// <summary>
    /// Setzt Zielgeschwindigkeit und startet die Rotation.
    /// Praktisch für externe Scripts wie Wind-Manager.
    /// </summary>
    public void SetSpeedAndRotate(float degreesPerSecond)
    {
        TargetSpeed = degreesPerSecond;
        StartRotating();
    }

    /// <summary>Gibt zurück ob das Windrad gerade dreht.</summary>
    public bool IsRotating => CurrentState == StateRotating;
}
