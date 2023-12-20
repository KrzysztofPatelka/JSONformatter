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
using Newtonsoft.Json;

namespace JSONformatter
{
    public partial class Form1 : Form
    {
        public string filePatch = "";
        public Encoding kodowanie = Encoding.UTF8;
        public string programName = "JSON formatter editor";

        private void textLength()
        {

            toolStripLabel1.Text = "Znaki: " + richTextBox1.Text.Length.ToString();
            toolStripLabel2.Text = "Linie: " + richTextBox1.Lines.Length.ToString();
        }

        public void getLineNumber()
        {
            toolStripLabel5.Text = "Aktualna linia: " + (richTextBox1.GetLineFromCharIndex(richTextBox1.SelectionStart) + 1);
        }

        public Form1()
        {
            InitializeComponent();
            openFileDialog1.Filter = "Wszystkie pliki|*.*|Text plik|*.txt|JSON plik|*.json";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.Multiselect = false;
            openFileDialog1.FileName = "";
            openFileDialog1.Title = "Otwórz plik";
            openFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            saveFileDialog1.Filter = "Wszystkie pliki|*.*|Text plik|*.txt|JSON plik|*.json";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.FileName = "";
            saveFileDialog1.Title = "Zapisz plik";
            saveFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            textLength();
            getLineNumber();

        }

        private void zakonczToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void otworzToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    StreamReader sr = new StreamReader(openFileDialog1.FileName);
                    toolStripLabel3.Text = sr.CurrentEncoding.ToString();
                    kodowanie = sr.CurrentEncoding;
                    sr.Close();
                    richTextBox1.Text = File.ReadAllText(openFileDialog1.FileName, kodowanie);
                    filePatch = openFileDialog1.FileName;
                    this.Text = programName + " - " + Path.GetFileName(filePatch);
                    toolStripLabel4.Text = filePatch;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd: " + ex.Message, "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void formatujToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var noFormatText = richTextBox1.Text;
                var objectFormat = JsonConvert.DeserializeObject(noFormatText);
                richTextBox1.Text = "";
                richTextBox1.Text = JsonConvert.SerializeObject(objectFormat, Formatting.Indented);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd: " + ex.Message, "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bezFormatowaniaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var noFormatText = richTextBox1.Text;
                var objectFormat = JsonConvert.DeserializeObject(noFormatText);
                richTextBox1.Text = "";
                richTextBox1.Text = JsonConvert.SerializeObject(objectFormat, Formatting.None);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd: " + ex.Message, "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cofnijToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Undo();
        }

        private void ponowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Redo();
        }

        private void wytnijToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Cut();
        }

        private void kopiujToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Copy();
        }

        private void wklejToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Paste();
        }

        private void zaznaczWszystkoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectAll();
        }

        private void nowyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            filePatch = "";
            this.Text = programName;
            toolStripLabel4.Text = "";
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            textLength();
            this.Text = programName + " - " + Path.GetFileName(filePatch) + " *";
        }

        private void zapiszJakoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    //richTextBox1.Text = File.ReadAllText(openFileDialog1.FileName);
                    //richTextBox1.SaveFile(saveFileDialog1.FileName, );
                    File.WriteAllText(saveFileDialog1.FileName, richTextBox1.Text, kodowanie);
                    filePatch = saveFileDialog1.FileName;
                    this.Text = programName + " - " + Path.GetFileName(filePatch);
                    toolStripLabel4.Text = filePatch;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd: " + ex.Message, "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void zapiszToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (filePatch == "")
            {
                zapiszJakoToolStripMenuItem.PerformClick();
            }
            else
            {
                File.WriteAllText(saveFileDialog1.FileName, richTextBox1.Text, kodowanie);
                filePatch = saveFileDialog1.FileName;
                this.Text = programName + " - " + Path.GetFileName(filePatch);
            }
        }

        private void richTextBox1_SelectionChanged(object sender, EventArgs e)
        {
            getLineNumber();
        }
    }
}
