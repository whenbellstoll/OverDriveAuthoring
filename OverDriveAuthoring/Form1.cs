using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace OverDriveAuthoring
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
         
        /// <summary>
        /// Loads the Label track from a file given it has appropriate data.
        /// </summary>
        /// <param name="file"></param>
        private void fileLoad(string file)
        {

            string s = File.ReadAllText(file);

            int yPosition = 24;
            int rowNumber = 1;
            foreach( string line in s.Split('\n') )
            {
                
                string[] values = line.Split(' ');
                if( values.Length < 3 )
                {
                    continue;
                }
                ComboBox itemBox = new ComboBox();
                ComboBox placeBox = new ComboBox();
                TextBox timeBox = new TextBox();
                //Time Box
                timeBox.Location = new System.Drawing.Point(12, yPosition);
                timeBox.Name = "timeBox" + rowNumber;
                timeBox.Size = new System.Drawing.Size(212, 20);
                timeBox.TabIndex = (rowNumber * 3) - 3;
                timeBox.Text = values[0];

                //Item Box
                itemBox.FormattingEnabled = true;
                itemBox.Items.AddRange(new object[] {
                 "Note",
                 "Cone",
                 "Pothole",
                 "Oil",
                 "Spec",
                 "Car1",
                 "Car2",
                 "Car3",
                 "Car4",
                 "Car5",
                 "Health",
                 "Coin",
                 "Win",
                 "TutNote",
                 "TutOil",
                 "TutObs",
                 "TutSpec"});
                itemBox.Location = new System.Drawing.Point(230, yPosition);
                itemBox.Name = "itemBox" + rowNumber;
                itemBox.Text = values[1];
                itemBox.Size = new System.Drawing.Size(82, 21);
                itemBox.TabIndex = (rowNumber * 3) - 2;

                //Place Box
                placeBox.FormattingEnabled = true;
                placeBox.Items.AddRange(new object[] {
                "Mid",
                "Left",
                "Right"});
                placeBox.Text = values[2];
                placeBox.Location = new System.Drawing.Point(318, yPosition);
                placeBox.Name = "placeBox" + rowNumber;
                placeBox.Size = new System.Drawing.Size(82, 21);
                placeBox.TabIndex = (rowNumber * 3) - 1;

                Controls.Add(timeBox);
                Controls.Add(itemBox);
                Controls.Add(placeBox);

                yPosition += 26;
                rowNumber++;
                if(values[1] == "Win")
                {
                    break;
                }
            }

        }

        /// <summary>
        /// Saves all relevant fields to the text file of your choosing 
        /// </summary>
        /// <param name="file"></param>
        private void fileSave(string file)
        {
            //Nothing right now
            int rowNumber = 1;
            ComboBox itemBox = Controls.Find("itemBox" + rowNumber, true).FirstOrDefault() as ComboBox;
            ComboBox placeBox = Controls.Find("placeBox" + rowNumber, true).FirstOrDefault() as ComboBox;
             TextBox timeBox = Controls.Find("timeBox" + rowNumber, true).FirstOrDefault() as TextBox;

            string text = "";
            while(placeBox != null)
            {
                text = text + timeBox.Text + " " + itemBox.Text + " " + placeBox.Text + "\n";
                rowNumber++;
                itemBox = Controls.Find("itemBox" + rowNumber, true).FirstOrDefault() as ComboBox;
                placeBox = Controls.Find("placeBox" + rowNumber, true).FirstOrDefault() as ComboBox;
                timeBox = Controls.Find("timeBox" + rowNumber, true).FirstOrDefault() as TextBox;
            }

            File.WriteAllText(file, text);
        }

        private void fileButton_Click(object sender, EventArgs e)
        {
            var openFileDialog1 = new OpenFileDialog();
            DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                string file = openFileDialog1.FileName;
                try
                {
                    fileLoad(file);
                }
                catch (IOException)
                {
                }
            }

        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            var openFileDialog1 = new OpenFileDialog();
            DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                string file = openFileDialog1.FileName;
                try
                {
                    // Save file
                    fileSave(file);
                }
                catch (IOException)
                {
                }
            }
        }
    }
}
