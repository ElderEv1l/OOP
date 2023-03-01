using System.Text.Json.Serialization;
using Backups.Entities;
using Backups.Extra.Services;
using Newtonsoft.Json;

namespace Backups.Extra;

public class Serialization
{
    public void SaveBackupTaskExtra(BackupTaskExtra backupTaskExtra, string path)
    {
        string json = JsonConvert.SerializeObject(backupTaskExtra);
        var writer = new StreamWriter(path);
        writer.WriteLine(json);
    }

    public BackupTaskExtra UploadBackupTaskExtra(string path)
    {
        using var reader = new StreamReader(path);
        string json = reader.ReadToEnd();
        return JsonConvert.DeserializeObject<BackupTaskExtra>(json);
    }
}