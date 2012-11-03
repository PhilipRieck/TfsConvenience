using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Dynamic;
using Microsoft.TeamFoundation.WorkItemTracking.Client;

namespace TfsConvenience
{
    public class ResultItem : DynamicObject
    {
        private readonly ConcurrentDictionary<string, object> _fieldValues = new ConcurrentDictionary<string, object>();
        private readonly List<string> _fieldNames = new List<string>();

        public int Id { get; private set; }

        public IEnumerable<string> Fields
        {
            get { return _fieldNames; }
        }

        public ResultItem(WorkItem workItem)
        {
            foreach (Field field in workItem.Fields)
            {
                _fieldNames.Add(field.Name);
                SetValue(field.Name, field.Value);
            }
        }

        public object GetValue(string fieldName)
        {
            object val;
            _fieldValues.TryGetValue(fieldName, out val);
            return val;
        }

        public T GetValueOrDefault<T>(string fieldName)
        {
            var value = GetValue(fieldName);
            return (T) (value ?? default(T));
        }

        public object this[string idx]
        {
            get 
            {
                return GetValue(idx);
            }
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            result = GetValue(binder.Name);
            return true;
        }


        public override bool TryGetIndex(GetIndexBinder binder, object[] indexes, out object result)
        {
            result = null;
            if (indexes.Length != 1 || indexes[0] is string)
            {
                return false;
            }
            var field = (string)indexes[0];
            result = GetValue(field);
            return true;
        }

        private void SetValue(string fieldName, object value)
        {
            _fieldValues.AddOrUpdate(fieldName, value, (k, v) => v);
            if (fieldName == "ID")
            {
                Id = (int)value;
            }
        }
    }
}