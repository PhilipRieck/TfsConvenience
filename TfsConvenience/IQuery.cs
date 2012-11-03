using System;
using System.Threading.Tasks;
using Microsoft.TeamFoundation.WorkItemTracking.Client;

namespace TfsConvenience
{
    public interface IQuery
    {
        string QueryString { get; set; }
        event EventHandler<QueryProgressEventArgs> QueryProgress;
        int ProgressGranularity { get; set; }
        QueryResults Execute();
        Task<QueryResults> ExecuteAsync();
    }
}