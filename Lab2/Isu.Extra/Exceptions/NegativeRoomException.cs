namespace Isu.Extra.Exceptions;

public class NegativeRoomException : Exception
{
    public NegativeRoomException(int room)
        : base($"Error: Room Can't be Negative Number or Zero!")
    { }
    public NegativeRoomException(int room, string message)
        : base(message)
    { }
}