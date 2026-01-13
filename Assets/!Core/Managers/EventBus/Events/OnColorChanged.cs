public interface IEvent { }

public struct OnColorChanged : IEvent
{
    public PlayerColors PlayerColor;
}
