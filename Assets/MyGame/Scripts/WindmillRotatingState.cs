// WindmillRotatingState.cs
// State: Windrad dreht sich

using UnityEngine;

public class WindmillRotatingState : IWindmillState
{
    public void Enter(WindmillController controller)
    {
        Debug.Log("[Windmill] State: Rotating");
    }

    public void Update(WindmillController controller)
    {
        // Geschwindigkeit sanft auf Zielwert angleichen (Spin-Up / Spin-Down)
        controller.CurrentSpeed = Mathf.MoveTowards(
            controller.CurrentSpeed,
            controller.TargetSpeed,
            controller.Acceleration * Time.deltaTime
        );

        // Rotor um lokale Z-Achse drehen (ggf. auf X oder Y anpassen)
        controller.RotorTransform.Rotate(
            Vector3.forward,
            controller.CurrentSpeed * Time.deltaTime,
            Space.Self
        );
    }

    public void Exit(WindmillController controller)
    {
        Debug.Log("[Windmill] Leaving Rotating State");
    }
}
