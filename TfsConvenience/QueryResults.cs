using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace TfsConvenience
{
    public class QueryResults : ICollection<ResultItem>
    {
        private readonly List<ResultItem> _results = new List<ResultItem>();

        public ResultItem this[int idx]
        {
            get { return _results[idx]; }
        }

        public IEnumerator<ResultItem> GetEnumerator()
        {
            return _results.GetEnumerator();
        }

        public string ErrorMessage { get; set; }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _results.GetEnumerator();
        }

        public void AddRange(IEnumerable<ResultItem> resultItems)
        {
            _results.AddRange(resultItems);
        }

        public void Add(ResultItem item)
        {
            _results.Add(item);
        }

        public void Clear()
        {
            _results.Clear();
        }

        public bool Contains(ResultItem item)
        {
            return _results.Contains(item);
        }

        public void CopyTo(ResultItem[] array, int arrayIndex)
        {
            _results.CopyTo(array, arrayIndex);
        }

        public bool Remove(ResultItem item)
        {
            return _results.Remove(item);
        }

        public int Count
        {
            get { return _results.Count; }
        }
        public bool IsReadOnly
        {
            get { return false; }
        }

        public string GetAsText(string separator)
        {
            if (!string.IsNullOrEmpty(ErrorMessage)) return ErrorMessage;
            if (Count == 0) return "No Results";

            var sb = new StringBuilder();
            var first = true;
            foreach (var field in _results[0].Fields)
            {
                sb.AppendFormat("{0}{1}", first ? "" : separator, field);
                first = false;
            }
            sb.AppendLine();
            first = true;
            foreach (var record in _results)
            {
                foreach (var field in record.Fields)
                {
                    sb.AppendFormat("{0}{1}", first ? "" : separator, record[field]);
                    first = false;
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }
    }
}
