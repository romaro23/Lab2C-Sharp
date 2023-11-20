using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    internal class Listener<TKey>
    {
        private List<ListEntry<TKey>> listeners = new List<ListEntry<TKey>>();
        public void EmployeesChanged(object sender, EmployeesChangedEventArgs<TKey> e)
        {
            ListEntry<TKey> listener = new ListEntry<TKey>(e.CollectionName, e.Update, e.PropertyName, e.Key.ToString());
            listeners.Add(listener);
        }
        public override string ToString()
        {
            return string.Join("\n", listeners);
        }
    }
}
