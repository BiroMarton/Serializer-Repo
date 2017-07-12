using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PersonsSerializer
{
    public partial class Persons : Form
    {

        private PersonList personList;
        private const int PREVIOUS = -1;
        private const int NEXT = 1;
        private const int CURRENT = 0;

        public Persons()
        {
            InitializeComponent();
            personList = PersonList.INSTANCE;
            personList.DeserializeAll();
            UpdateTextBoxes();



        }


            private void btnSave_Click(object sender, EventArgs e)
        {


            Person p = personList.Add(txtName.Text,
            txtPhone.Text,
            txtAddress.Text);

            if (p != null)
            {
                personList.SerializePerson(p);
            }
            
            
                

            
        }

        private void UpdateTextBoxes()
        {
            Person current = personList.SelectedPerson;
            if (current != null)
            {
                txtName.Text = current.Name;
                txtAddress.Text = current.Address;
                txtPhone.Text = current.PhoneNumber;
            }

        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            personList.UpdateIndex(PREVIOUS);
            UpdateTextBoxes();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            personList.UpdateIndex(NEXT);
            UpdateTextBoxes();
        }
    }
}
