using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    internal class ListEntry<TKey>
    {
        public string CollectionName { get; set; }
        public Update Update { get; set; }
        public string PropertyName { get; set; }
        public string Key { get; set; }
        public ListEntry(string collectionName, Update update, string propertyName, string key)
        {
            CollectionName = collectionName;
            Update = update;
            PropertyName = propertyName;
            Key = key;
        }
        public override string ToString()
        {
            return $"Name of collection: {CollectionName}, Update: {Update}, Name of property: {PropertyName}, Key: {Key}";
        }
    }
}
