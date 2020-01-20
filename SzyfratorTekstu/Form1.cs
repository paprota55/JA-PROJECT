using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SzyfratorTekstu.Model;

namespace SzyfratorTekstu
{
    public partial class Form1 : Form
    {
        TextFiller process = new TextFiller();
        FileSupporter filesSupp = new FileSupporter();
        DateTime startTime;
        DateTime stopTime;

        public Form1()
        {
            InitializeComponent();
            label2.Text = hScrollBar1.Value.ToString();
            hScrollBar1.Value = Environment.ProcessorCount;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Wybierz plik do szyfrowania.");
            }
            else
            {
                if (checkBox1.Checked == true && checkBox2.Checked == true)
                {
                    MessageBox.Show("Możesz wybrać tylko jedną opcję.");
                }
                else if (checkBox1.Checked == true)
                {
                    process.fillGapInText(hScrollBar1.Value);
                    startTime = DateTime.Now;
                    process.encryptionCpp();
                    stopTime = DateTime.Now;
                    label6.Text = ((stopTime - startTime).TotalMilliseconds).ToString() + " ms";
                    label6.Visible = true;
                    label5.Visible = true;
                    filesSupp.writeToFileCpp(process.getByteArray());
                }
                else if (checkBox2.Checked == true)
                {
                    process.fillGapInText(hScrollBar1.Value);
                    startTime = DateTime.Now;
                    process.encryptionAsm();
                    stopTime = DateTime.Now;
                    label6.Text = ((stopTime - startTime).TotalMilliseconds).ToString() + " ms";
                    label6.Visible = true;
                    label5.Visible = true;
                    filesSupp.writeToFileAsm(process.getByteArray());
                }
                else
                {
                    MessageBox.Show("Wybierz jakas opcje.");
                }
            }
        }

        private void hScrollBar1_ValueChanged(object sender, EventArgs e)
        {
            label2.Text = hScrollBar1.Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            textBox1.Text = openFileDialog1.FileNames[0];
            process.setText(filesSupp.getDataFromFile(textBox1.Text));
            textBox1.Text = filesSupp.getDataFromFile(textBox1.Text).Length.ToString();
        }
    }
}
