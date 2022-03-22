using System.IO;

namespace PDFGenerator
{
    partial class frmPDFGenerator
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ofdPDFTemplate = new System.Windows.Forms.OpenFileDialog();
            this.btnOpenFile = new System.Windows.Forms.Button();
            this.tbTemplateFile = new System.Windows.Forms.TextBox();
            this.btnGenerateShowPDF = new System.Windows.Forms.Button();
            this.rtbPDFTempData = new System.Windows.Forms.RichTextBox();
            this.tbFind = new System.Windows.Forms.TextBox();
            this.btnFindHide = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ofdPDFTemplate
            // 
            this.ofdPDFTemplate.FileName = "openFileDialog1";
            // 
            // btnOpenFile
            // 
            this.btnOpenFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOpenFile.Location = new System.Drawing.Point(12, 610);
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new System.Drawing.Size(186, 47);
            this.btnOpenFile.TabIndex = 0;
            this.btnOpenFile.Text = "Select PDF Template";
            this.btnOpenFile.UseVisualStyleBackColor = true;
            this.btnOpenFile.Click += new System.EventHandler(this.btnOpenFile_Click);
            // 
            // tbTemplateFile
            // 
            this.tbTemplateFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbTemplateFile.Location = new System.Drawing.Point(12, 581);
            this.tbTemplateFile.Name = "tbTemplateFile";
            this.tbTemplateFile.ReadOnly = true;
            this.tbTemplateFile.Size = new System.Drawing.Size(758, 23);
            this.tbTemplateFile.TabIndex = 1;
            // 
            // btnGenerateShowPDF
            // 
            this.btnGenerateShowPDF.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGenerateShowPDF.Location = new System.Drawing.Point(572, 610);
            this.btnGenerateShowPDF.Name = "btnGenerateShowPDF";
            this.btnGenerateShowPDF.Size = new System.Drawing.Size(198, 47);
            this.btnGenerateShowPDF.TabIndex = 2;
            this.btnGenerateShowPDF.Text = "Generate && Show PDF";
            this.btnGenerateShowPDF.UseVisualStyleBackColor = true;
            this.btnGenerateShowPDF.Click += new System.EventHandler(this.btnGenerateShowPDF_Click);
            // 
            // rtbPDFTempData
            // 
            this.rtbPDFTempData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbPDFTempData.Location = new System.Drawing.Point(12, 36);
            this.rtbPDFTempData.Name = "rtbPDFTempData";
            this.rtbPDFTempData.Size = new System.Drawing.Size(758, 542);
            this.rtbPDFTempData.TabIndex = 3;
            this.rtbPDFTempData.Text = "";
            // 
            // tbFind
            // 
            this.tbFind.Location = new System.Drawing.Point(13, 6);
            this.tbFind.Name = "tbFind";
            this.tbFind.PlaceholderText = "Aranacak kelimeyi yazınız...(CTRL + F) (F3 ile bir sonraki aramayı yapabilirsiniz" +
    ")";
            this.tbFind.Size = new System.Drawing.Size(719, 23);
            this.tbFind.TabIndex = 4;
            // 
            // btnFindHide
            // 
            this.btnFindHide.Location = new System.Drawing.Point(737, 2);
            this.btnFindHide.Name = "btnFindHide";
            this.btnFindHide.Size = new System.Drawing.Size(33, 28);
            this.btnFindHide.TabIndex = 5;
            this.btnFindHide.Text = "X";
            this.btnFindHide.UseVisualStyleBackColor = true;
            this.btnFindHide.Click += new System.EventHandler(this.btnFindHide_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(392, 610);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(174, 47);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "Save PDF Template To File";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnClear.Location = new System.Drawing.Point(204, 610);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(174, 47);
            this.btnClear.TabIndex = 7;
            this.btnClear.Text = "Clear All";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // frmPDFGenerator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 662);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnFindHide);
            this.Controls.Add(this.tbFind);
            this.Controls.Add(this.rtbPDFTempData);
            this.Controls.Add(this.btnGenerateShowPDF);
            this.Controls.Add(this.tbTemplateFile);
            this.Controls.Add(this.btnOpenFile);
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(800, 45);
            this.Name = "frmPDFGenerator";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PDF Generator";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmPDFGenerator_FormClosing);
            this.Shown += new System.EventHandler(this.frmPDFGenerator_Shown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmPDFGenerator_KeyUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog ofdPDFTemplate;
        private System.Windows.Forms.Button btnOpenFile;
        private System.Windows.Forms.TextBox tbTemplateFile;
        private System.Windows.Forms.Button btnGenerateShowPDF;
        private System.Windows.Forms.RichTextBox rtbPDFTempData;
        private System.Windows.Forms.TextBox tbFind;
        private System.Windows.Forms.Button btnFindHide;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClear;
    }
}
