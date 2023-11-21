namespace BootesConsulta.Models;

public class InformeZ
{
    public int IdReport { get; set; }
    public DateTime Date { get; set; }
    public string QuadraticErrorOrthogonalityCelestialSphere1 { get; set; }
    public string QuadraticErrorOrthogonalityCelestialSphere2 { get; set; }
    public int Frames { get; set; }
    public string ObservatoryAdjustmentStart { get; set; }
    public string ObservatoryAdjustmentEnd { get; set; }
    public decimal DihedralAngleBetweenTrajectoryPlanes { get; set; }
    public decimal StatisticalWeight { get; set; }
    public string ErrorArRadiant { get; set; }
    public string AstronomicalCoordinatesRadiantEcliptic { get; set; }
    public string AstronomicalCoordinatesRadiantJ2000 { get; set; }
    public decimal Azimuth { get; set; }
    public decimal ZenithalDistance { get; set; }
    public string StartTrajectoryObservatory1 { get; set; }
    public string EndTrajectoryObservatory1 { get; set; }
    public string StartTrajectoryObservatory2 { get; set; }
    public string EndTrajectoryObservatory2 { get; set; }
    public string IntersectionEarthTrajectory { get; set; }
    public decimal TravelDistanceObservatory1 { get; set; }
    public decimal ErrorDistanceObservatory1 { get; set; }
    public decimal ErrorHeightObservatory1 { get; set; }
    public decimal TravelDistanceObservatory2 { get; set; }
    public decimal ErrorDistanceObservatory2 { get; set; }
    public decimal ErrorHeightObservatory2 { get; set; }
    public decimal TrajectoryTimeObservatory1 { get; set; }
    public decimal AverageVelocity { get; set; }
    public decimal TrajectoryTimeObservatory2 { get; set; }
    public string MovementEcuationKms { get; set; }
    public string MovementEcuationG { get; set; }
    public decimal ErrorSpeed { get; set; }
    public decimal InitialSpeedObservatory2 { get; set; }
    public decimal AccelerationKms { get; set; }
    public decimal AccelerationG { get; set; }
    //TODO
    public int Method { get; set; }
    //Foreign Keys
    public int IdObservatory1 { get; set; }
    public int IdObservatory2 { get; set; }
    public int IdParametricEcuation { get; set; }
    public int IdMeteor { get; set; }
}

public class InformeZ2
{
    //Report Data
    public DateTime Date { get; set; }
    public ObservatoryMeteorData Observatorio1 { get; set; }
    public ObservatoryMeteorData Observatorio2 { get; set; }
    public int Frames { get; set; }
    public decimal DihedralAngleBetweenTrajectoryPlanes { get; set; }

    //Radiant
    public string AstronomicalCoordinatesRadiantEcliptic { get; set; }
    public string AstronomicalCoordinatesRadiantJ2000 { get; set; }
    public decimal Azimuth { get; set; }
    public decimal ZenithalDistance { get; set; }

    //Atmospheric Trajectory
    public GeographicalCoordinates CoordinatesIntersectionEarthTrajectory { get; set; }
    public decimal AverageVelocity { get; set; }
    public decimal AccelerationKms { get; set; }
    public decimal AccelerationG { get; set; }

    //Frames

    //Trajectory


    public int IdReport { get; set; }
    public string QuadraticErrorOrthogonalityCelestialSphere1 { get; set; }
    public string QuadraticErrorOrthogonalityCelestialSphere2 { get; set; }
    public string ObservatoryAdjustmentStart { get; set; }
    public string ObservatoryAdjustmentEnd { get; set; }
    public decimal StatisticalWeight { get; set; }
    public string ErrorArRadiant { get; set; }

    public decimal TravelDistanceObservatory1 { get; set; }
    public decimal ErrorDistanceObservatory1 { get; set; }
    public decimal ErrorHeightObservatory1 { get; set; }
    public decimal TravelDistanceObservatory2 { get; set; }
    public decimal ErrorDistanceObservatory2 { get; set; }
    public decimal ErrorHeightObservatory2 { get; set; }
    public decimal TrajectoryTimeObservatory1 { get; set; }
    public decimal TrajectoryTimeObservatory2 { get; set; }
    public string MovementEcuationKms { get; set; }
    public string MovementEcuationG { get; set; }
    public decimal ErrorSpeed { get; set; }
    public decimal InitialSpeedObservatory2 { get; set; }

    //TODO
    public int Method { get; set; }
    //Foreign Keys

    public int IdParametricEcuation { get; set; }
    public int IdMeteor { get; set; }
}
public class ObservatoryMeteorData
{
    public int Id { get; set; }
    public string Name { get; set; }
    public GeographicalCoordinates CoordinatesStart { get; set; }
    public decimal InitialDistance { get; set; }
    public decimal InitialAltitud { get; set; }
    public GeographicalCoordinates CoordinatesEnd { get; set; }
    public decimal FinalDistance { get; set; }
    public decimal FinalAltitud { get; set; }
    public string DistanceTraveled { get; set; }
    public string TrajectoryTime { get; set; }
}

public class GeographicalCoordinates
{
    public string Longitude { get; set; }
    public string Latitude { get; set; }
}