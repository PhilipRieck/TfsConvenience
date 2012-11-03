using Microsoft.TeamFoundation.Client;

namespace TfsConvenience
{
    public interface IConnection
    {
        bool IsValid { get; set; }
        TfsTeamProjectCollection Connect();

        bool Test();
    }
}