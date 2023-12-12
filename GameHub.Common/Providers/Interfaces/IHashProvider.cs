namespace GameHub.Common.Providers.Interfaces
{
    public interface IHashProvider
    {
        string GetHash(string password, string salt);
    }
}
