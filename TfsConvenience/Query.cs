using System;
using System.Threading.Tasks;
using Microsoft.TeamFoundation.WorkItemTracking.Client;

namespace TfsConvenience
{
    public class Query : IQuery
    {
        private readonly ConnectionParameters _parameters;
        public string QueryString { get; set; }

        public Query(ConnectionParameters parameters) 
        {
            ProgressGranularity = 13;
            _parameters = parameters;
        }

        public event EventHandler<QueryProgressEventArgs> QueryProgress;
        public int ProgressGranularity { get; set; }

        public QueryResults Execute()
        {

            OnProgress(ProgressType.Connecting);
            var results = new QueryResults();
            try
            {
                if (_parameters == null)
                {
                    results.ErrorMessage = "Connection parameters not set!";
                }
                else
                {
                    using (var tfs = Connection.Connect(_parameters))
                    {
                        var store = tfs.GetService<WorkItemStore>();
                        var itemCollection = store.Query(QueryString);

                        var total = itemCollection.Count;
                        var current = 1;
                        OnProgress(ProgressType.Querying, current, total);
                        foreach (WorkItem wi in itemCollection)
                        {
                            results.Add(new ResultItem(wi));
                            if (current++%ProgressGranularity == 0)
                            {
                                OnProgress(ProgressType.Querying, current, total);
                            }
                        }

                    }
                }
            }
            catch (Exception e)
            {
                results.ErrorMessage = e.Message;
            }
            finally
            {
                OnProgress(ProgressType.Complete);
            }
            return results;
        }

        public Task<QueryResults> ExecuteAsync()
        {
            return Task.Run(() => Execute());
        }
        
        private void OnProgress(ProgressType type)
        {
            OnProgress(type, 0, 0);
        }
        private void OnProgress(ProgressType type, long current, long total)
        {
            if (QueryProgress != null)
            {
                QueryProgress(this, new QueryProgressEventArgs { ProgressType = type, Current = current, Total = total });
            }
        }
    }
}
