using System;
using System.Text;

namespace TfsConvenience
{
    public enum ProgressType
    {
        Idle,
        Connecting,
        Querying,
        Updating,
        Disconnecting,
        Complete
    }

    public class QueryProgressEventArgs : EventArgs
    {
        public ProgressType ProgressType { get; set; }
        public long Current { get; set; }
        public long Total { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            switch (ProgressType)
            {
                case ProgressType.Idle:
                    sb.Append("Idle");
                    break;
                case ProgressType.Connecting:
                    sb.Append("Connecting ..");
                    break;
                case ProgressType.Querying:
                    sb.AppendFormat("Querying record {0}/{1}", Current, Total);
                    break;
                case ProgressType.Updating:
                    sb.AppendFormat("Updating Record {0}/{1}", Current, Total);
                    break;
                case ProgressType.Disconnecting:
                    sb.Append("Disconnecting ..");
                    break;
                case ProgressType.Complete:
                    sb.Append("Complete");
                    break;
                default:
                    sb.Append(".");
                    break;
            }
            return sb.ToString();
        }
    }
}
