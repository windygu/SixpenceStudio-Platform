using SixpenceStudio.Core.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SixpenceStudio.Encryption
{
    public partial class Encryption : Form
    {
        public Encryption()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("请输入需要加密的内容", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            textBox1.Text = DecryptAndEncryptHelper.AESEncrypt(textBox2.Text);
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("请输入需要解密的内容", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            textBox2.Text = DecryptAndEncryptHelper.AESDecrypt(textBox1.Text);
        }
    }
}
