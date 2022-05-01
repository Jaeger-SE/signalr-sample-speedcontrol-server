namespace Zwedze.Demo.SignalR.SpeedControl.Contracts.Notifications
{
    public class SpeedViolationReport
    {
        public string CarPlate { get; init; }
        public double SpeedReported { get; init; }
        public string RadarId { get; init; }
    }

    public class AssignationModel
    {
        public string RadarId { get; init; }
    }
}