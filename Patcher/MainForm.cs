using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Patcher
{
    public partial class MainForm : Form
    {
        ILogger<MainForm> Logger;

        public MainForm(ILogger<MainForm> Logger)
        {
            this.Logger = Logger;

            InitializeComponent();

            textBoxResPath.Text = Globals.Settings.ResourceDirectory;
            textBoxImgPath.Text = Globals.Settings.ImageSourceDirectory;
            textBoxWorkPath.Text = Globals.Settings.WorkingDirectory;

            var fonts = new InstalledFontCollection();
            var names = fonts.Families.Select(x => x.Name).ToList();

            cbBoxFont.Items.AddRange(names.ToArray());

            if (string.IsNullOrEmpty(Globals.Settings.RenderFont))
            {
                cbBoxFont.SelectedIndex = 0;
            }
            else
            {
                var fontIdx = names.FindIndex(x => x == Globals.Settings.RenderFont);
                if (fontIdx >= 0)
                {
                    cbBoxFont.SelectedIndex = fontIdx;
                }
            }

            numericFontSize.Value = Globals.Settings.RenderFontSize;
        }

        private async void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            await Globals.UpdateAppSettings();
        }

        private void btnResDirDialog_Click(object sender, EventArgs e) => BrowserFolder(textBoxResPath);
        private void btnImgDirDialog_Click(object sender, EventArgs e) => BrowserFolder(textBoxImgPath);
        private void btnWorkDirDialog_Click(object sender, EventArgs e) => BrowserFolder(textBoxWorkPath);
        void BrowserFolder(TextBox tbox)
        {
            folderBrowserDialog1.SelectedPath = tbox.Text;
            var result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                tbox.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void btnResDirOpen_Click(object sender, EventArgs e) => RunExplorerProcess(textBoxResPath.Text);
        private void btnResImgOpen_Click(object sender, EventArgs e) => RunExplorerProcess(textBoxImgPath.Text);
        private void btnWorkDirOpen_Click(object sender, EventArgs e) => RunExplorerProcess(textBoxWorkPath.Text);
        void RunExplorerProcess(string dir)
        {
            if (Directory.Exists(dir))
            {
                Process.Start(new ProcessStartInfo { Arguments = dir, FileName = "explorer.exe" });
            }
        }

        private void btnLoad_Click(object sender, EventArgs e) => RunProgress(BackgroundProcess.Initialize);
        private void btnPatchTextJson_Click(object sender, EventArgs e) => RunProgress(BackgroundProcess.PatchTextJson);
        private void btnPatchTextHtml_Click(object sender, EventArgs e) => RunProgress(BackgroundProcess.PatchTextHtml);
        private void btnPatchTextTxtZip_Click(object sender, EventArgs e) => RunProgress(BackgroundProcess.PatchTextTxtZip);

        bool RunProgress(Func<ValueTask<bool>> func)
        {
            var enLocalResPath = Constants.EnLocalizationPath(textBoxResPath.Text);
            if (!Directory.Exists(enLocalResPath))
                return ShowError("Invalid Resource Directory.");

            if (!Directory.Exists(textBoxImgPath.Text))
                return ShowError("Invalid Image Source Directory.");

            if (!Directory.Exists(textBoxWorkPath.Text))
                return ShowError("Invalid Working Directory.");

            Globals.Settings.ResourceDirectory = textBoxResPath.Text;
            Globals.Settings.ImageSourceDirectory = textBoxImgPath.Text;
            Globals.Settings.WorkingDirectory = textBoxWorkPath.Text;
            Globals.Settings.RenderFont = cbBoxFont.SelectedItem.ToString();
            Globals.Settings.RenderFontSize = (int)numericFontSize.Value;

            var progress = new ProgressForm();
            if (!progress.Start(this, func))
            {
                return ShowError(progress.ErrMessage, progress.ErrAll);
            }

            return true;
        }

        bool ShowError(string message, string content = default)
        {
            MessageBox.Show(message);
            if (!String.IsNullOrEmpty(content))
                Logger.LogError(content);

            return false;
        }

        private void cbBoxFont_SelectedIndexChanged(object sender, EventArgs e)
        {
            Globals.Settings.RenderFont = cbBoxFont.SelectedItem.ToString();
        }

        private void numericFontSize_ValueChanged(object sender, EventArgs e)
        {
            Globals.Settings.RenderFontSize = (int)numericFontSize.Value;
        }
    }
}
