namespace OneSky.Services.Util{
    public class ServiceUris{

        const string baseRoute = "/SDSP";

        // Routing Services
        public const string PointToPointRouteUri = baseRoute + "/vehiclePath/pointToPoint";
        public const string Sgp4RouteUri = baseRoute + "/vehiclePath/sgp4";
        public const string SimpleFlightRouteUri = baseRoute + "/vehiclePath/simpleFlight";
        public const string GreatArcRouteUri = baseRoute + "/vehiclePath/greatArc";
        public const string TolRouteUri = baseRoute + "/vehiclePath/tol";
        public const string RasterRouteUri = baseRoute + "/vehiclePath/raster";
//Satellite Pass Services
        public const string AccessSatellitePassesSiteUri = baseRoute + "/access/satellitePasses/site";
        public const string AccessSatellitePassesSgp4Uri = baseRoute + "/access/satellitePasses/sgp4";
        public const string AccessSatellitePassesPointToPointUri = baseRoute + "/access/satellitePasses/pointToPoint";
        public const string AccessSatellitePassesRasterUri = baseRoute + "/access/satellitePasses/raster";
        public const string AccessSatellitePassesTolUri = baseRoute + "/access/satellitePasses/tol";
        public const string AccessSatellitePassesSimpleFlightUri = baseRoute + "/access/satellitePasses/simpleFlight";
        public const string AccessSatellitePassesGreatArcUri = baseRoute + "/access/satellitePasses/greatArc";
// Communication Services
        public const string CommunicationsLinkBudgetUri = baseRoute + "/communications/linkbudget";
// Navigation Services
        public const string NavigationGpsOutagesUri = baseRoute + "/navigation/gpsSatelliteOutages";
        public const string NavigationPafDataUri = baseRoute + "/navigation/PAFData";
        public const string NavigationPsfDataUri = baseRoute + "/navigation/PSFData";
        public const string NavigationSofDataUri = baseRoute + "/navigation/SOFData";
        public const string NavigationAlmanacDataUri = baseRoute + "/navigation/AlmanacData";
        public const string NavigationPredictedSiteUri = baseRoute + "/navigation/predicted/site";
        public const string NavigationPredictedPointToPointUri = baseRoute + "/navigation/predicted/pointToPoint";
        public const string NavigationPredictedTolUri = baseRoute + "/navigation/predicted/tol";
        public const string NavigationPredictedRasterUri = baseRoute + "/navigation/predicted/raster";
        public const string NavigationPredictedGreatArcUri = baseRoute + "/navigation/predicted/greatArc";
        public const string NavigationPredictedSimpleFlightUri = baseRoute + "/navigation/predicted/simpleFlight";
        public const string NavigationAssessedSiteUri = baseRoute + "/navigation/assessed/site";
        public const string NavigationAssessedPointToPointUri = baseRoute + "/navigation/assessed/pointToPoint";
        public const string NavigationAssessedTolUri = baseRoute + "/navigation/assessed/tol";
        public const string NavigationAssessedRasterUri = baseRoute + "/navigation/assessed/raster";
        public const string NavigationAssessedGreatArcUri = baseRoute + "/navigation/assessed/greatArc";
        public const string NavigationAssessedSimpleFlightUri = baseRoute + "/navigation/assessed/simpleFlight";
        public const string NavigationDopSiteUri = baseRoute + "/navigation/dop/site";
        public const string NavigationDopPointToPointUri = baseRoute + "/navigation/dop/pointToPoint";
        public const string NavigationDopTolUri = baseRoute + "/navigation/dop/tol";
        public const string NavigationDopRasterUri = baseRoute + "/navigation/dop/raster";
        public const string NavigationDopGreatArcUri = baseRoute + "/navigation/dop/greatArc";
        public const string NavigationDopSimpleFlightUri = baseRoute + "/navigation/dop/simpleFlight";
// Ovrflight Services
        public const string OverflightSgp4Uri = baseRoute + "/overflight/sgp4";
        public const string OverflightSimpleFlightUri = baseRoute + "/overflight/simpleFlight";
        public const string OverflightPointToPointUri = baseRoute + "/overflight/pointToPoint";
        public const string OverflightGreatArcUri = baseRoute + "/overflight/greatArc";
// Airspace Services
        public const string AirspaceSimpleFlightUri = baseRoute + "/airspace/simpleFlight";
        public const string AirspacePointToPointUri = baseRoute + "/airspace/pointToPoint";
        public const string AirspaceGreatArcUri = baseRoute + "/airspace/greatArc";
        public const string AirspacePointFlightUri = baseRoute + "/airspace/pointFlight";
        public const string AirspaceSelectAirspacesUri = baseRoute + "/airspace/selectAirspaces";
        public const string AirspaceRealTimeUri = baseRoute + "/airspace/realtime";
// CZML visualization
        public const string VehiclePathCzmlSgp4Uri = baseRoute + "/vehiclepath/czml/sgp4";
        public const string VehiclePathCzmlGpsUri = baseRoute + "/vehiclepath/czml/Gps";
        public const string AirspaceCzmlUri = baseRoute + "/airspace/czml";
// Terrain Services
        public const string TerrainHeightsSiteUri = baseRoute + "/terrain/heights";
        public const string TerrainHeightsPointToPointUri = baseRoute + "/terrain/pointToPoint";    
        public const string TerrainHeightsGreatArcUri = baseRoute + "/terrain/greatArc";
// Lighting Services
        public const string LightingSiteUri = baseRoute + "/lighting/site";
        public const string LightingPointToPointUri = baseRoute + "/lighting/pointToPoint";
        public const string LightingAnglesSiteUri = baseRoute + "/lighting/angles/site";
        public const string LightingAnglesPointToPointUri = baseRoute + "/lighting/angles/pointToPoint";
        public const string LightingSolarTransitSiteUri = baseRoute + "/lighting/transit/site";
// Population Services
        public const string PopulationPointToPointUri = baseRoute + "/population/pointToPoint";
        public const string PopulationSiteUri = baseRoute + "/population/site";
// Weather Services
        public const string WeatherPointToPointUri = baseRoute + "/weather/pointToPoint";
        public const string WeatherSiteUri = baseRoute + "/weather/site";

    }
}