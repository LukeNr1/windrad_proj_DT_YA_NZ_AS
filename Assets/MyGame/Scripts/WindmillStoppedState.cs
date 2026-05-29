// WindmillStoppedState.cs
// State: Windrad steht still

using UnityEngine;

public class WindmillStoppedState : IWindmillState
{
    public void Enter(WindmillController controller)
    {
        Debug.Log("[Windmill] State: Stopped");
        // Optional: Sanft abbremsen statt abrupt stoppen
        // (Smooth stop wird im Update behandelt)
    }

    public void Update(WindmillController controller)
    {
        // Aktuelle Drehgeschwindigkeit sanft auf 0 abbauen
        if (controller.CurrentSpeed > 0f)
        {
            controller.CurrentSpeed = Mathf.MoveTowards(
                controller.CurrentSpeed,
                0f,
                controller.BrakeForce * Time.deltaTime
            );

            controller.RotorTransform.Rotate(
                Vector3.forward,
                controller.CurrentSpeed * Time.deltaTime,
                Space.Self
            );
        }
    }

    public void Exit(WindmillController controller)
    {
        Debug.Log("[Windmill] Leaving Stopped State");
    }
}
