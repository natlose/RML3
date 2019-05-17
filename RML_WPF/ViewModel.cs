using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RML_WPF
{
    public class ViewModel : INotifyPropertyChanged
    {
        private LinkedList<Person> list;

        public void LoadData()
        {
            using (MyData mydata = new MyData())
            {
                list = new LinkedList<Person>(mydata.Persons.Include(nameof(Person.Addresses)).ToList());
                Node = list.First;
            }
        }

        private LinkedListNode<Person> node;
        private LinkedListNode<Person> Node
        {
            get { return node; }
            set
            {
                node = value;
                OnPropertyChanged(nameof(this.IsNextEnabled));
                OnPropertyChanged(nameof(this.IsPreviousEnabled));
                Person = node.Value;
            }
        }

        public bool IsNextEnabled
        {
            get { return Node?.Next != null; }
        }

        public bool IsPreviousEnabled
        {
            get { return Node?.Previous != null; }
        }

        public void Next()
        {
            if (Node.Next != null) Node = Node.Next;
        }

        public void Previous()
        {
            if (Node.Previous != null) Node = Node.Previous;
        }

        private Person person;
        public Person Person
        {
            get { return person; }
            set
            {
                person = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
