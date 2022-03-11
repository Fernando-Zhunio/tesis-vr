[System.Serializable]
public class Location {
    public string latitud;
    public string longitud;
    public string altitude;

    public double latitudDouble;
    public double longitudDouble;

    public Location(double latitud, double longitud, double altitude = 0)
    {
        latitudDouble = latitud;
        longitudDouble = longitud;
        this.latitud = latitud.ToString().Replace(",", ".");
        this.longitud = longitud.ToString().Replace(",",".");
        this.altitude = altitude.ToString().Replace(",",".");
    }

    public string GetLatLonString()
    {
        return this.latitud + "," + this.longitud;
    }

    public string GetLatLonAltString(string separator)
    {
        return this.latitud + "," + this.longitud + "," + this.altitude;
    }

}