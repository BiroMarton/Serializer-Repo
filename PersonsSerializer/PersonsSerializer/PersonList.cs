using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PersonsSerializer
{
    class PersonList
    {

        public static PersonList INSTANCE { get; } = new PersonList();
        private enum SerializationType { Serialization, Deserialization }
        private List<Person> persons;
        public int SelectedIndex { get; private set; } = 0;
        public int LastSerial
        {
            get { return persons.Count - 1; }
        }
        public Person SelectedPerson
        {
            get
            {
                try
                {
                    return persons[SelectedIndex];
                }
                catch (ArgumentOutOfRangeException)
                {
                    return null;
                }

            }
        }

        private PersonList()
        {
            persons = new List<Person>();
        }

        public Person Add(string name, string address, string phone)
        {
            Person p = null;
            if (persons.Count <= 99)
            {
                p = new Person(name, address, phone, CalculateNextSerial());
                persons.Add(p);
            }
            else
            {
                MessageBox.Show("Cannot create more than 99 person objects.", "Error");
            }
            return p;
        }

        public void UpdateIndex(int change)
        {
            if (SelectedIndex + change >= 0 && SelectedIndex + change < persons.Count)
            {
                SelectedIndex += change;
            }
            else
            {
                MessageBox.Show("Reached the end of the list.", "Error");
            }
        }

        public int CalculateNextSerial()
        {
            return persons.Count;
        }

        public void SerializePerson(Person personToSerialize)
        {
            FormatObject(
                personToSerialize.Serial,
                FileMode.Create,
                personToSerialize,
                serialize: new BinaryFormatter().Serialize
                );
        }

        public void DeserializePerson(int serial)
        {
            FormatObject<Person>(
                serial,
                FileMode.Open,
                deserialize: new BinaryFormatter().Deserialize
                );
        }

        private delegate void Serializer<T>(FileStream fileStream, T objectToSerialize);
        private delegate Object Deserializer(FileStream fileStream);


        private void FormatObject<T>(
            int serial,
            FileMode fileMode,
            T objectToSerialize = null,
            Serializer<T> serialize = null,
            Deserializer deserialize = null
            )
            where T : class
        {
            string fileName = typeof(T).Name.ToLower() + serial + ".dat";
            using (FileStream fileStream = new FileStream(fileName, fileMode))
            {
                if (serialize != null && deserialize == null)
                {
                    serialize.Invoke(fileStream, objectToSerialize);
                    MessageBox.Show("Data saved");
                }
                else if (serialize == null && deserialize != null)
                {
                    persons.Add((Person)deserialize.Invoke(fileStream));
                }
            }
        }

        public void DeserializeAll()
        {
            for (int i = 0; i < 99; i++)
            {
                try
                {
                    FormatObject<Person>(i, FileMode.Open,
                        deserialize: new BinaryFormatter().Deserialize);
                }
                catch (FileNotFoundException)
                {
                    SelectedIndex = (i - 1) > 0 ? i - 1 : i;
                    MessageBox.Show("Deserialized " + i + " objects.", "Deserialized");
                    break;
                }

            }
        }

    }
}
