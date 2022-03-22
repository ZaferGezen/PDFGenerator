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

namespace PDFGenerator
{
    public partial class frmPDFGenerator : Form
    {
        public frmPDFGenerator()
        {
            InitializeComponent();

            //rtbPDFTempData.Location = new Point(12, 6);
            //rtbPDFTempData.Size = new Size(rtbPDFTempData.Width, rtbPDFTempData.Height + 30);

            ofdPDFTemplate.FilterIndex = 1;
            ofdPDFTemplate.Filter = "txt files (*.txt)|*.txt|Json files (*.json)|*.json";
        }
        public void CreateLabel(string templateContent)
        {
            templateContent = templateContent.Replace("{IsIossSymbol}", "true");

            var parsedLabelContent = PDFParser.Generate(templateContent, 0);

            string path = $"Test.pdf";
            File.WriteAllBytes(path, parsedLabelContent);

            System.Diagnostics.ProcessStartInfo info = new System.Diagnostics.ProcessStartInfo(path)
            {
                UseShellExecute = true,
                Verb = "open"
            };
            System.Diagnostics.Process.Start(info);

        }

        private string ReadFile(string file)
        {
            string templatePath = $"{file}";
            if (!File.Exists(templatePath))
            {
                throw new FileNotFoundException($"Cannot find file in {templatePath}");
            }
            return File.ReadAllText(templatePath);
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            DialogResult dr = ofdPDFTemplate.ShowDialog();

            if (dr == DialogResult.OK)
            {
                tbTemplateFile.Text = ofdPDFTemplate.FileName;

                rtbPDFTempData.Text = ReadFile(tbTemplateFile.Text);
            }
        }

        private void btnGenerateShowPDF_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tbTemplateFile.Text) || !string.IsNullOrEmpty(rtbPDFTempData.Text))
            {
                CreateLabel(rtbPDFTempData.Text);
            }
        }

        int startindex = 0;

        private void frmPDFGenerator_KeyUp(object sender, KeyEventArgs e)
        {
            if ((e.Control && e.KeyCode == Keys.F))
            {
                //tbFind.Visible = true;
                //btnFindHide.Visible = true;
                //rtbPDFTempData.Location = new Point(12, 36);
                //rtbPDFTempData.Size = new Size(rtbPDFTempData.Width, rtbPDFTempData.Height - 30);

                tbFind.Focus();
            }

            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.F3)
            {

                int find = 0;

                //while (startindex < rtbPDFTempData.TextLength)
                //{
                int wordstartIndex = rtbPDFTempData.Find(tbFind.Text, startindex, RichTextBoxFinds.None);

                if (wordstartIndex != -1)
                {
                    rtbPDFTempData.SelectionStart = wordstartIndex;
                    //rtbPDFTempData.SelectionLength = tbFind.Text.Length;
                    //rtbPDFTempData.SelectionBackColor = Color.Yellow;
                    find++;
                }
                else
                {
                    startindex = 0;
                    
                    return;
                }
                //else
                //    break;

                startindex = wordstartIndex + tbFind.Text.Length;
                //}

                if (find > 0)
                {
                    rtbPDFTempData.Focus();
                }
            }

        }

        private void btnFindHide_Click(object sender, EventArgs e)
        {
            //tbFind.Visible = false;
            //btnFindHide.Visible = false;
            //rtbPDFTempData.Location = new Point(12,6);
            //rtbPDFTempData.Size = new Size(rtbPDFTempData.Width, rtbPDFTempData.Height+30);
            startindex = 0;
            tbFind.Text = "";
        }

        private void frmPDFGenerator_Shown(object sender, EventArgs e)
        {
            rtbPDFTempData.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("File will be saved? ", "Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dr == DialogResult.Yes)
            {
                File.WriteAllText(tbTemplateFile.Text, rtbPDFTempData.Text);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Clear All Text? ", "Clear", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dr == DialogResult.Yes)
            {
                tbTemplateFile.Text = "";

                rtbPDFTempData.Text = "";

                rtbPDFTempData.Focus();
            }
        }

        private void frmPDFGenerator_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dr = MessageBox.Show("Form is closing? ", "Close", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dr == DialogResult.No)
            {
                e.Cancel = true;
            }
        }
    }
}
