using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ComPortReader
{
    public partial class ConfigForm : Form
    {
        public ConfigForm()
        {
            InitializeComponent();
            LoadCurrentSettings();
        }

        private void LoadCurrentSettings()
        {
            // 从配置文件加载当前设置
            string selectedComPort = ConfigurationManager.AppSettings["SelectedComPort"];
            string runCardApiUrl = ConfigurationManager.AppSettings["RunCardApiUrl"];
            string returnMesApiUrl = ConfigurationManager.AppSettings["ReturnMesApiUrl"];

            // 加载可用的COM端口到ComboBox
            comboBoxComPorts.Items.AddRange(SerialPort.GetPortNames());
            comboBoxComPorts.SelectedItem = selectedComPort;

            // 设置当前API URL到TextBox
            textBoxRunCardApiUrl.Text = runCardApiUrl;
            textBoxReturnMesApiUrl.Text = returnMesApiUrl;

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // 保存配置
            string selectedComPort = comboBoxComPorts.SelectedItem?.ToString();
            string runCardApiUrl = textBoxRunCardApiUrl.Text;
            string returnMesApiUrl = textBoxReturnMesApiUrl.Text;

            SaveConfig(selectedComPort, runCardApiUrl, returnMesApiUrl);
            MessageBox.Show("配置已保存！");
            this.DialogResult = DialogResult.OK; // 设置 DialogResult 为 OK
            this.Close(); // 关闭窗口
        }

        private void SaveConfig(string comPort, string runCardApiUrl, string returnMesApiUrl)
        {
            // 修改app.config文件中的配置
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            config.AppSettings.Settings["SelectedComPort"].Value = comPort ?? "";
            config.AppSettings.Settings["RunCardApiUrl"].Value = runCardApiUrl;
            config.AppSettings.Settings["ReturnMesApiUrl"].Value = returnMesApiUrl;

            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }

        private void ConfigForm_Load(object sender, EventArgs e)
        {

        }

        private void AddComboBoxItems(ComboBox comboBox)
        {
            // 添加下拉选项
            comboBox.Items.Clear(); // 清空现有选项（如果需要）
            comboBox.Items.Add("1");
            comboBox.Items.Add("2");
            comboBox.Items.Add("3");
            comboBox.Items.Add("4");

            // 设置默认选项（可选）
            comboBox.SelectedIndex = 0; // 默认选择第一个项
        }
    }
}
