using Microsoft.WindowsAzure.Storage.Blob.Protocol;

namespace Commons.Identity
{
    public interface IWorkSpace
    {
        string Id { get; }
        string Name { get; }
        string LongName { get; }
    }
}