namespace Zwedze.Demo.SignalR.SpeedControl.Contracts.Radar
{
    public class SpeedReport
    {
        /// <summary>
        ///     Contains the car plate information.
        /// </summary>
        /// <remarks>
        ///     To ease the exercise: Consider that we live in a world where all countries have a common pool of plates.
        ///     e.g: So French and Belgian have the same plate format and uniqueness of ID.
        /// </remarks>
        public string CarPlate { get; init; }

        /// <summary>
        ///     The speed measured from the radar.
        /// </summary>
        /// <remarks>Raw value, not rounded.</remarks>
        public double Speed { get; init; }

        /// <summary>
        ///     The radar unique ID.
        /// </summary>
        public string RadarId { get; init; }
    }
}