namespace OneSky.Services.Util{
    public class ServiceUris{
// Routing Services
        public const string PointToPointRouteUri = "/SDSP/vehiclePath/pointToPoint";
        public const string Sgp4RouteUri = "/SDSP/vehiclePath/sgp4";
        public const string SimpleFlightRouteUri = "/SDSP/vehiclePath/simpleFlight";
        public const string GreatArcRouteUri = "/SDSP/vehiclePath/greatArc";
        public const string TolRouteUri = "/SDSP/vehiclePath/tol";
        public const string RasterRouteUri = "/SDSP/vehiclePath/raster";
//Satellite Pass Services
        public const string AccessSatellitePassesSiteUri = "/SDSP/access/satellitePasses/site";
        public const string AccessSatellitePassesSgp4Uri = "/SDSP/access/satellitePasses/sgp4";
        public const string AccessSatellitePassesPointToPointUri = "/SDSP/access/satellitePasses/pointToPoint";
        public const string AccessSatellitePassesRasterUri = "/SDSP/access/satellitePasses/raster";
        public const string AccessSatellitePassesTolUri = "/SDSP/access/satellitePasses/tol";
        public const string AccessSatellitePassesSimpleFlightUri = "/SDSP/access/satellitePasses/simpleFlight";
        public const string AccessSatellitePassesGreatArcUri = "/SDSP/access/satellitePasses/greatArc";
// Communication Services
        public const string CommunicationsLinkBudgetUri = "/SDSP/communications/linkbudget";
// Navigation Services
        public const string NavigationGpsOutagesUri = "/SDSP/navigation/gpsSatelliteOutages";
        public const string NavigationPafDataUri = "/SDSP/navigation/PAFData";
        public const string NavigationPsfDataUri = "/SDSP/navigation/PSFData";
        public const string NavigationSofDataUri = "/SDSP/navigation/SOFData";
        public const string NavigationAlmanacDataUri = "/SDSP/navigation/AlmanacData";
        public const string NavigationPredictedSiteUri = "/SDSP/navigation/predicted/site";
        public const string NavigationPredictedPointToPointUri = "/SDSP/navigation/predicted/pointToPoint";
        public const string NavigationPredictedTolUri = "/SDSP/navigation/predicted/tol";
        public const string NavigationPredictedRasterUri = "/SDSP/navigation/predicted/raster";
        public const string NavigationPredictedGreatArcUri = "/SDSP/navigation/predicted/greatArc";
        public const string NavigationPredictedSimpleFlightUri = "/SDSP/navigation/predicted/simpleFlight";
        public const string NavigationAssessedSiteUri = "/SDSP/navigation/assessed/site";
        public const string NavigationAssessedPointToPointUri = "/SDSP/navigation/assessed/pointToPoint";
        public const string NavigationAssessedTolUri = "/SDSP/navigation/assessed/tol";
        public const string NavigationAssessedRasterUri = "/SDSP/navigation/assessed/raster";
        public const string NavigationAssessedGreatArcUri = "/SDSP/navigation/assessed/greatArc";
        public const string NavigationAssessedSimpleFlightUri = "/SDSP/navigation/assessed/simpleFlight";
        public const string NavigationDopSiteUri = "/SDSP/navigation/dop/site";
        public const string NavigationDopPointToPointUri = "/SDSP/navigation/dop/pointToPoint";
        public const string NavigationDopTolUri = "/SDSP/navigation/dop/tol";
        public const string NavigationDopRasterUri = "/SDSP/navigation/dop/raster";
        public const string NavigationDopGreatArcUri = "/SDSP/navigation/dop/greatArc";
        public const string NavigationDopSimpleFlightUri = "/SDSP/navigation/dop/simpleFlight";
// Ovrflight Services
        public const string OverflightSgp4Uri = "/SDSP/overflight/sgp4";
        public const string OverflightSimpleFlightUri = "/SDSP/overflight/simpleFlight";
        public const string OverflightPointToPointUri = "/SDSP/overflight/pointToPoint";
        public const string OverflightGreatArcUri = "/SDSP/overflight/greatArc";
// Airspace Services
        public const string AirspaceSimpleFlightUri = "/SDSP/airspace/simpleFlight";
        public const string AirspacePointToPointUri = "/SDSP/airspace/pointToPoint";
        public const string AirspaceGreatArcUri = "/SDSP/airspace/greatArc";
        public const string AirspacePointFlightUri = "/SDSP/airspace/pointFlight";
        public const string AirspaceSelectAirspacesUri = "/SDSP/airspace/selectAirspaces";
        public const string AirspaceRealTimeUri = "/SDSP/airspace/realtime";
// CZML visualization
        public const string VehiclePathCzmlSgp4Uri = "/SDSP/vehiclepath/czml/sgp4";
        public const string VehiclePathCzmlGpsUri = "/SDSP/vehiclepath/czml/Gps";
        public const string AirspaceCzmlUri = "/SDSP/airspace/czml";
// Terrain Services
        public const string TerrainHeightsSiteUri = "/SDSP/terrain/heights";
        public const string TerrainHeightsPointToPointUri = "/SDSP/terrain/pointToPoint";    
        public const string TerrainHeightsGreatArcUri = "/SDSP/terrain/greatArc";
// Lighting Services
        public const string LightingSiteUri = "/SDSP/lighting/site";
        public const string LightingPointToPointUri = "/SDSP/lighting/pointToPoint";
        public const string LightingAnglesSiteUri = "/SDSP/lighting/angles/site";
        public const string LightingAnglesPointToPointUri = "/SDSP/lighting/angles/pointToPoint";
        public const string LightingSolarTransitSiteUri = "/SDSP/lighting/transit/site";
// Population Services
        public const string PopulationPointToPointUri = "/SDSP/population/pointToPoint";
        public const string PopulationSiteUri = "/SDSP/population/site";
// Weather Services
        public const string WeatherPointToPointUri = "/SDSP/weather/pointToPoint";
        public const string WeatherSiteUri = "/SDSP/weather/site";

    }
}