using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CSVLib;

namespace LabTest
{
    public partial class DailyExpenseUI : Form
    {
        private string location = @"G:\My Made Program\DailyInfo.csv";
        public DailyExpenseUI()
        {
            InitializeComponent();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            string amount = textBoxAmount.Text;
            string caterory = comboBoxCategory.Text;
            string particular = textBoxParticular.Text;
            string aRows = amount + "," + caterory + "," + particular;
            if (textBoxAmount.Text == ""
             || textBoxParticular.Text == ""
             || comboBoxCategory.Text == "")
            {
                MessageBox.Show("No Value");
                return;
            }

            FileStream afFileStream = new FileStream(location, FileMode.Append);
            StreamWriter aStreamWriter = new StreamWriter(afFileStream);
            aStreamWriter.Write(aRows);
            aStreamWriter.WriteLine();
            aStreamWriter.Close();
            MessageBox.Show("Save Succes");


            textBoxAmount.Text = "";
            textBoxParticular.Text = "";
        }

        private void buttonShow_Click(object sender, EventArgs e)
        {
            double total = 0;
            listViewShow.Items.Clear();

            string cost = comboBoxCategory.Text;
            FileStream aStream = new FileStream(location, FileMode.Open);

            CsvFileReader aReader = new CsvFileReader(aStream);
            List<string> dailycost = new List<string>();

            ListViewItem listViewItem;

            if (new FileInfo(location).Length != 0)
            {
                while (aReader.ReadRow(dailycost))
                {
                    if (dailycost[1] == cost)
                    {

                        listViewItem = new ListViewItem(
                        new string[]
                        {
                            
                            dailycost[0],
                            dailycost[1],
                            dailycost[2]
                        }
                    );

                        listViewShow.Items.Add(listViewItem);

                        total += Convert.ToDouble(dailycost[0]);
                    }
                }
            }
            aStream.Close();


            textBoxTotal.Text = total.ToString();
        }
    }
}
