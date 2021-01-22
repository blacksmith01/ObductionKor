using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Patcher
{
    public partial class ProgressForm : Form, IDisposable
    {
        BackgroundWorker worker = new();
        public bool Succeed { get; private set; }
        public string ErrMessage { get; private set; }
        public string ErrAll { get; private set; }
        public ProgressForm()
        {
            InitializeComponent();
        }

        public bool Start(Form parent, Func<ValueTask<bool>> DoWork)
        {
            worker.DoWork += (sender, e) => {
                try
                {
                    Succeed = DoWork().Result;
                }
                catch(Exception ex)
                {
                    ErrMessage = ex.Message;
                    ErrAll = ex.ToString();
                }
            };

            ShowDialog(parent);

            return Succeed;
        }

        private void ProgressForm_Load(object sender, EventArgs e)
        {
            CenterToParent();

            // ui thread call
            worker.RunWorkerCompleted += (_, _) => Close();
            worker.RunWorkerAsync();
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            worker.Dispose();
        }
    }
}
