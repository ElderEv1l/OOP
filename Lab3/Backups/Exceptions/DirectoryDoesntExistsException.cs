namespace Backups.Exceptions;

public class DirectoryDoesntExistsException : Exception
{
    public DirectoryDoesntExistsException(string path)
        : base($"Error: This Directory doesn't exist!")
    { }
    public DirectoryDoesntExistsException(string path, string message)
        : base(message)
    { }
}