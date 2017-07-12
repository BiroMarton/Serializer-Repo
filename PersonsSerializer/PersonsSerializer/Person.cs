using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PersonsSerializer
{
    [Serializable]
    class Person : IDeserializationCallback
    {
        private string name;

        private string phoneNumber;

        private string address;

        private DateTime creationDate;

        [NonSerialized]
        private int serial;

        public Person(string name, string phoneNumber, string eMail, int serial) {

            this.name = name;
            this.phoneNumber = phoneNumber;
            this.address = eMail;
            this.creationDate = DateTime.Now;
            this.serial = serial;

        }

        public String Name {
            get { return name; }
            set { name = value; }
        }

        public String PhoneNumber
        {
            get { return phoneNumber; }
            set { phoneNumber = value; }
        }

        public String Address
        {
            get { return address; }
            set {  address = value; }
        }

        public DateTime CreationDate
        {
            get { return creationDate; }
            set { creationDate= value; }
        }

        public int Serial
        {
            get { return serial; }
            set { serial = value; }
        }

        public void OnDeserialization(object sender)
        {
            serial = PersonList.INSTANCE.CalculateNextSerial();

        }
    }
}
