using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace WatchDog
{
    public partial class Main : Form
    {
        private string _fileExtensionName = "";

        public Main()
        {
            InitializeComponent();
        }

        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = true;
            fileDialog.Title = "请选择文件";
            fileDialog.Filter = "所有文件(*.*)|*.*";
            fileDialog.RestoreDirectory = true;
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                string file = fileDialog.FileName;
                _fileExtensionName = Path.GetExtension(file);
                this.txtselectedFile.Text = file;
            }
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            string sourceFileName = "";
            string code = "";
            string savefileName = "";
            if (this.txtselectedFile.Text.Trim() != "")
            {
                sourceFileName = this.txtselectedFile.Text.Trim();
            }
            else
            {
                MessageBox.Show("请选择加密文件！");
                return;
            }
            if (this.txtCode.Text.Trim() != "")
            {
                code = this.txtCode.Text.Trim();
            }
            else
            {
                MessageBox.Show("请输入密码！");
                return;
            }

            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Title = "请选择保存路径";
            saveFile.Filter = "所有文件(*.*)|*.*";
            saveFile.RestoreDirectory = true;
            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                savefileName = saveFile.FileName + _fileExtensionName;
            }
            else
            {
                return;
            }
            
            try
            {
                DECFileEncryption.EncryptFile(sourceFileName, savefileName, code);
                MessageBox.Show("加密成功！");
                this.txtCode.Text = "";
                this.txtselectedFile.Text = "";
            }
            catch
            {
                MessageBox.Show("加密失败！");
            }
        }

        private void btnDectypt_Click(object sender, EventArgs e)
        {
            string sourceFileName = "";
            string code = "";
            string savefileName = "";
            if (this.txtselectedFile.Text.Trim() != "")
            {
                sourceFileName = this.txtselectedFile.Text.Trim();
            }
            else
            {
                MessageBox.Show("请选择解密文件！");
                return;
            }
            if (this.txtCode.Text.Trim() != "")
            {
                code = this.txtCode.Text.Trim();
            }
            else
            {
                MessageBox.Show("请输入密码！");
                return;
            }

            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Title = "请选择保存路径";
            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                if (Path.GetExtension(saveFile.FileName) != _fileExtensionName)
                {
                    savefileName = saveFile.FileName + _fileExtensionName;
                }
                else
                {
                    savefileName = saveFile.FileName;
                }
            }
            else
            {
                return;
            }
            
            try
            {
                DECFileEncryption.DecryptFile(sourceFileName, savefileName, code);
                MessageBox.Show("解密成功！");
                this.txtselectedFile.Text = "";
                this.txtCode.Text = "";
            }
            catch
            {
                MessageBox.Show("解密失败！");
            }
        }
    }
}
