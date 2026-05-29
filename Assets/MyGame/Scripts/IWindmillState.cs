// IWindmillState.cs
// Interface für alle Windrad-States

public interface IWindmillState
{
    void Enter(WindmillController controller);
    void Update(WindmillController controller);
    void Exit(WindmillController controller);
}
