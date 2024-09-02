using System.Text.Json;

public class AccesoJson : IAccesoADatos
{
    public bool Existe(string nombreArchivo)
    {
        string ruta = "json/"+nombreArchivo;
        return File.Exists(ruta);
    }

    public List<Cadete> LeerCadetes(string nombreArchivo)
    {
        string ruta = "json/"+nombreArchivo;
        string cadetesJson;
        using (var archivoOpen = new FileStream(ruta, FileMode.Open))
        {
            using (var strReader = new StreamReader(archivoOpen))
            {
                cadetesJson = strReader.ReadToEnd();
                archivoOpen.Close();
            }
        }
        var cadetes = JsonSerializer.Deserialize<List<Cadete>>(cadetesJson);
        return cadetes;
    }

    public Cadeteria LeerCadeteria(string nombreArchivo)
    {
        string ruta = "json/"+nombreArchivo;
        string cadeteriaJson;
        using (var archivoOpen = new FileStream(ruta, FileMode.Open))
        {
            using (var strReader = new StreamReader(archivoOpen))
            {
                cadeteriaJson = strReader.ReadToEnd();
                archivoOpen.Close();
            }
        }
        var cadeteria = JsonSerializer.Deserialize<Cadeteria>(cadeteriaJson);
        return cadeteria;
    }
}