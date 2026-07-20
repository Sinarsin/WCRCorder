namespace WCRCorder.Models;

public enum ApplicationState
{
    Starting,
    Ready,
    Recording,
    WaitingForCamera,
    WaitingForMicrophone,
    Stopping,
    DiskFull,
    Error,
    Closing
}