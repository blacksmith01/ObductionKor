
namespace Patcher
{
    partial class MainForm
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
            this.GroupInit = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.numericFontSize = new System.Windows.Forms.NumericUpDown();
            this.cbBoxFont = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnImgDirDialog = new System.Windows.Forms.Button();
            this.btnResImgOpen = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxImgPath = new System.Windows.Forms.TextBox();
            this.btnResDirDialog = new System.Windows.Forms.Button();
            this.btnResDirOpen = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxResPath = new System.Windows.Forms.TextBox();
            this.btnWorkDirDialog = new System.Windows.Forms.Button();
            this.btnWorkDirOpen = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxWorkPath = new System.Windows.Forms.TextBox();
            this.btnPatchTextJson = new System.Windows.Forms.Button();
            this.btnLoad = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.btnPatchTextHtml = new System.Windows.Forms.Button();
            this.btnPatchTextTxtZip = new System.Windows.Forms.Button();
            this.GroupInit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericFontSize)).BeginInit();
            this.SuspendLayout();
            // 
            // GroupInit
            // 
            this.GroupInit.Controls.Add(this.label4);
            this.GroupInit.Controls.Add(this.numericFontSize);
            this.GroupInit.Controls.Add(this.cbBoxFont);
            this.GroupInit.Controls.Add(this.label3);
            this.GroupInit.Controls.Add(this.btnImgDirDialog);
            this.GroupInit.Controls.Add(this.btnResImgOpen);
            this.GroupInit.Controls.Add(this.label2);
            this.GroupInit.Controls.Add(this.textBoxImgPath);
            this.GroupInit.Controls.Add(this.btnResDirDialog);
            this.GroupInit.Controls.Add(this.btnResDirOpen);
            this.GroupInit.Controls.Add(this.label5);
            this.GroupInit.Controls.Add(this.textBoxResPath);
            this.GroupInit.Controls.Add(this.btnWorkDirDialog);
            this.GroupInit.Controls.Add(this.btnWorkDirOpen);
            this.GroupInit.Controls.Add(this.label1);
            this.GroupInit.Controls.Add(this.textBoxWorkPath);
            this.GroupInit.Location = new System.Drawing.Point(12, 12);
            this.GroupInit.Name = "GroupInit";
            this.GroupInit.Size = new System.Drawing.Size(509, 210);
            this.GroupInit.TabIndex = 0;
            this.GroupInit.TabStop = false;
            this.GroupInit.Text = "Settings";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(252, 163);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(98, 15);
            this.label4.TabIndex = 28;
            this.label4.Text = "Render Font Size";
            // 
            // numericFontSize
            // 
            this.numericFontSize.Location = new System.Drawing.Point(252, 181);
            this.numericFontSize.Name = "numericFontSize";
            this.numericFontSize.Size = new System.Drawing.Size(240, 23);
            this.numericFontSize.TabIndex = 27;
            this.numericFontSize.ValueChanged += new System.EventHandler(this.numericFontSize_ValueChanged);
            // 
            // cbBoxFont
            // 
            this.cbBoxFont.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBoxFont.FormattingEnabled = true;
            this.cbBoxFont.Location = new System.Drawing.Point(6, 181);
            this.cbBoxFont.Name = "cbBoxFont";
            this.cbBoxFont.Size = new System.Drawing.Size(240, 23);
            this.cbBoxFont.TabIndex = 26;
            this.cbBoxFont.SelectedIndexChanged += new System.EventHandler(this.cbBoxFont_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 163);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 15);
            this.label3.TabIndex = 25;
            this.label3.Text = "Render Font";
            // 
            // btnImgDirDialog
            // 
            this.btnImgDirDialog.Location = new System.Drawing.Point(426, 89);
            this.btnImgDirDialog.Name = "btnImgDirDialog";
            this.btnImgDirDialog.Size = new System.Drawing.Size(31, 23);
            this.btnImgDirDialog.TabIndex = 24;
            this.btnImgDirDialog.Text = "📁";
            this.btnImgDirDialog.UseVisualStyleBackColor = true;
            this.btnImgDirDialog.Click += new System.EventHandler(this.btnImgDirDialog_Click);
            // 
            // btnResImgOpen
            // 
            this.btnResImgOpen.Location = new System.Drawing.Point(463, 88);
            this.btnResImgOpen.Name = "btnResImgOpen";
            this.btnResImgOpen.Size = new System.Drawing.Size(31, 23);
            this.btnResImgOpen.TabIndex = 22;
            this.btnResImgOpen.Text = "📤";
            this.btnResImgOpen.UseVisualStyleBackColor = true;
            this.btnResImgOpen.Click += new System.EventHandler(this.btnResImgOpen_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(134, 15);
            this.label2.TabIndex = 23;
            this.label2.Text = "Image Source Directory";
            // 
            // textBoxImgPath
            // 
            this.textBoxImgPath.Location = new System.Drawing.Point(4, 89);
            this.textBoxImgPath.Name = "textBoxImgPath";
            this.textBoxImgPath.Size = new System.Drawing.Size(416, 23);
            this.textBoxImgPath.TabIndex = 21;
            // 
            // btnResDirDialog
            // 
            this.btnResDirDialog.Location = new System.Drawing.Point(426, 41);
            this.btnResDirDialog.Name = "btnResDirDialog";
            this.btnResDirDialog.Size = new System.Drawing.Size(31, 23);
            this.btnResDirDialog.TabIndex = 14;
            this.btnResDirDialog.Text = "📁";
            this.btnResDirDialog.UseVisualStyleBackColor = true;
            this.btnResDirDialog.Click += new System.EventHandler(this.btnResDirDialog_Click);
            // 
            // btnResDirOpen
            // 
            this.btnResDirOpen.Location = new System.Drawing.Point(463, 40);
            this.btnResDirOpen.Name = "btnResDirOpen";
            this.btnResDirOpen.Size = new System.Drawing.Size(31, 23);
            this.btnResDirOpen.TabIndex = 12;
            this.btnResDirOpen.Text = "📤";
            this.btnResDirOpen.UseVisualStyleBackColor = true;
            this.btnResDirOpen.Click += new System.EventHandler(this.btnResDirOpen_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(5, 19);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(314, 15);
            this.label5.TabIndex = 13;
            this.label5.Text = "Resource Directory (. ..steamapps\\common\\Obduction)";
            // 
            // textBoxResPath
            // 
            this.textBoxResPath.Location = new System.Drawing.Point(4, 41);
            this.textBoxResPath.Name = "textBoxResPath";
            this.textBoxResPath.Size = new System.Drawing.Size(416, 23);
            this.textBoxResPath.TabIndex = 11;
            // 
            // btnWorkDirDialog
            // 
            this.btnWorkDirDialog.Location = new System.Drawing.Point(425, 137);
            this.btnWorkDirDialog.Name = "btnWorkDirDialog";
            this.btnWorkDirDialog.Size = new System.Drawing.Size(31, 23);
            this.btnWorkDirDialog.TabIndex = 10;
            this.btnWorkDirDialog.Text = "📁";
            this.btnWorkDirDialog.UseVisualStyleBackColor = true;
            this.btnWorkDirDialog.Click += new System.EventHandler(this.btnWorkDirDialog_Click);
            // 
            // btnWorkDirOpen
            // 
            this.btnWorkDirOpen.Location = new System.Drawing.Point(461, 136);
            this.btnWorkDirOpen.Name = "btnWorkDirOpen";
            this.btnWorkDirOpen.Size = new System.Drawing.Size(31, 23);
            this.btnWorkDirOpen.TabIndex = 1;
            this.btnWorkDirOpen.Text = "📤";
            this.btnWorkDirOpen.UseVisualStyleBackColor = true;
            this.btnWorkDirOpen.Click += new System.EventHandler(this.btnWorkDirOpen_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 115);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 15);
            this.label1.TabIndex = 4;
            this.label1.Text = "Working Directory";
            // 
            // textBoxWorkPath
            // 
            this.textBoxWorkPath.Location = new System.Drawing.Point(4, 137);
            this.textBoxWorkPath.Name = "textBoxWorkPath";
            this.textBoxWorkPath.Size = new System.Drawing.Size(416, 23);
            this.textBoxWorkPath.TabIndex = 0;
            // 
            // btnPatchTextJson
            // 
            this.btnPatchTextJson.Location = new System.Drawing.Point(142, 228);
            this.btnPatchTextJson.Name = "btnPatchTextJson";
            this.btnPatchTextJson.Size = new System.Drawing.Size(116, 23);
            this.btnPatchTextJson.TabIndex = 19;
            this.btnPatchTextJson.Text = "Json Text Patch";
            this.btnPatchTextJson.UseVisualStyleBackColor = true;
            this.btnPatchTextJson.Click += new System.EventHandler(this.btnPatchTextJson_Click);
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(17, 228);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(116, 23);
            this.btnLoad.TabIndex = 18;
            this.btnLoad.Text = "Initialize";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // btnPatchTextHtml
            // 
            this.btnPatchTextHtml.Location = new System.Drawing.Point(274, 228);
            this.btnPatchTextHtml.Name = "btnPatchTextHtml";
            this.btnPatchTextHtml.Size = new System.Drawing.Size(116, 23);
            this.btnPatchTextHtml.TabIndex = 21;
            this.btnPatchTextHtml.Text = "Html Text Patch";
            this.btnPatchTextHtml.UseVisualStyleBackColor = true;
            this.btnPatchTextHtml.Click += new System.EventHandler(this.btnPatchTextHtml_Click);
            // 
            // btnPatchTextTxtZip
            // 
            this.btnPatchTextTxtZip.Location = new System.Drawing.Point(405, 228);
            this.btnPatchTextTxtZip.Name = "btnPatchTextTxtZip";
            this.btnPatchTextTxtZip.Size = new System.Drawing.Size(116, 23);
            this.btnPatchTextTxtZip.TabIndex = 22;
            this.btnPatchTextTxtZip.Text = "Txt.Zip Text Patch";
            this.btnPatchTextTxtZip.UseVisualStyleBackColor = true;
            this.btnPatchTextTxtZip.Click += new System.EventHandler(this.btnPatchTextTxtZip_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(541, 270);
            this.Controls.Add(this.btnPatchTextTxtZip);
            this.Controls.Add(this.btnPatchTextHtml);
            this.Controls.Add(this.GroupInit);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.btnPatchTextJson);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "MainForm";
            this.Text = "ObductionKor";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.GroupInit.ResumeLayout(false);
            this.GroupInit.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericFontSize)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox GroupInit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnWorkDirOpen;
        private System.Windows.Forms.TextBox textBoxWorkPath;
        private System.Windows.Forms.Button btnWorkDirDialog;
        private System.Windows.Forms.Button btnResDirDialog;
        private System.Windows.Forms.Button btnResDirOpen;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxResPath;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button btnPatchTextJson;
        private System.Windows.Forms.Button btnImgDirDialog;
        private System.Windows.Forms.Button btnResImgOpen;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxImgPath;
        private System.Windows.Forms.ComboBox cbBoxFont;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numericFontSize;
        private System.Windows.Forms.Button btnPatchTextHtml;
        private System.Windows.Forms.Button btnPatchTextTxtZip;
    }
}

