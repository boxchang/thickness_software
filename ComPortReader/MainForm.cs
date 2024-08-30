using System;
using System.Configuration;
using System.IO.Ports;
using System.Reflection;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Text;
using System.Net;
using System.Net.Security;
using Newtonsoft.Json.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace ComPortReader
{

    public partial class MainForm : Form
    {
        private SerialPort serialPort;

        private string roll = "";
        private string cuff = "";
        private string palm = "";
        private string finger = "";
        private string fingerTip = "";
        private string rollLower = "";
        private string cuffLower = "";
        private string palmLower = "";
        private string fingerLower = "";
        private string fingerTipLower = "";
        private string runCard = "";
        private string runCardApiUrl;
        private string returnMESApiUrl;
        private string selectedComPort;
        private string RollPort;
        private string CuffPort;
        private string PalmPort;
        private string FingerPort;
        private string FingerTipPort;
        private IpqcStd ipqcStd;
        private delegate void AddRowAndScrollDelegate(string runCard, string roll, string cuff, string palm, string finger, string fingerTip);
        private StringBuilder receivedDataBuffer = new StringBuilder();
        private const int FixedMessageLength = 12;
        private static string logDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");

        private void AddRowAndScroll(string runCard, string roll, string cuff, string palm, string finger, string fingerTip)
        {
            if (dataGridView1.InvokeRequired)
            {
                // Call the method on the UI thread
                dataGridView1.Invoke(new AddRowAndScrollDelegate(AddRowAndScroll), runCard, roll, cuff, palm, finger, fingerTip);
            }
            else
            {
                // Add a new row to the DataGridView
                int rowIndex = dataGridView1.Rows.Add(runCard, roll, cuff, palm, finger, fingerTip);

                dataGridView1.Rows[rowIndex].Cells["RollLower"].Value = rollLower;
                dataGridView1.Rows[rowIndex].Cells["CuffLower"].Value = cuffLower;
                dataGridView1.Rows[rowIndex].Cells["PalmLower"].Value = palmLower;
                dataGridView1.Rows[rowIndex].Cells["FingerLower"].Value = fingerLower;
                dataGridView1.Rows[rowIndex].Cells["FingerTipLower"].Value = fingerTipLower;

                // Scroll to the newly added row
                dataGridView1.FirstDisplayedScrollingRowIndex = rowIndex;
            }
        }


        public void message(string msg)
        {
            txtReceivedData.AppendText(msg + "\r\n");
        }

        public void Invoke_message(string msg)
        {
            string value = msg + "\r\n";
            if (txtReceivedData.InvokeRequired)
            {
                txtReceivedData.Invoke(new Action(() => txtReceivedData.AppendText(value)));
            }
            else
            {
                txtReceivedData.AppendText(value);
            }
        }

        public void Invoke_TextBox_Color(TextBox textBox, Color color)
        {
            if (textBox.InvokeRequired)
            {
                textBox.Invoke(new Action(() => textBox.ForeColor = color));
            }
            else
            {
                textBox.ForeColor = color;
            }
        }

        private void UpdateTextBox(TextBox textBox, string value)
        {
            if (textBox.InvokeRequired)
            {
                textBox.Invoke(new Action(() => textBox.Text = value));
            }
            else
            {
                textBox.Text = value;
            }
        }

        private void UpdateControls()
        {
            if (txtRunCard.InvokeRequired || txtRoll.InvokeRequired || txtCuff.InvokeRequired || txtPalm.InvokeRequired || txtFinger.InvokeRequired || txtFingerTip.InvokeRequired)
            {
                // Use Invoke to ensure the update is done on the UI thread
                this.Invoke(new Action(() =>
                {
                    txtRunCard.Clear();
                    txtRoll.Clear();
                    txtCuff.Clear();
                    txtPalm.Clear();
                    txtFinger.Clear();
                    txtFingerTip.Clear();

                    // Re-focus the first input box
                    txtRunCard.Focus();
                }));
            }
            else
            {
                // Directly update the controls if already on the UI thread
                txtRunCard.Clear();
                txtRoll.Clear();
                txtCuff.Clear();
                txtPalm.Clear();
                txtFinger.Clear();
                txtFingerTip.Clear();

                // Re-focus the first input box
                txtRunCard.Focus();
            }
        }

        public MainForm()
        {
            InitializeComponent();
            InitializeDataGridView();
            LoadConfig();

            dataGridView1.CellValueChanged += DataGridView1_CellValueChanged;

            // 绑定 FormClosing 事件处理程序
            this.FormClosing += new FormClosingEventHandler(MainForm_FormClosing);

            // 假设你的DataGridView名字叫dataGridView1
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter; // 可选：将标题也居中
            }

        }

        private void DataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            // Get the row being edited
            DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
            UpdateRowColors(row); // Call a function to update colors
        }

        private void UpdateRowColors(DataGridViewRow row)
        {
            if (float.TryParse(row.Cells["Roll"].Value?.ToString(), out float rollValue) &&
                float.TryParse(row.Cells["RollLower"].Value?.ToString(), out float rollLowerValue))
            {
                row.Cells["Roll"].Style.BackColor = rollValue < rollLowerValue ? Color.Red : Color.White;
            }

            if (float.TryParse(row.Cells["Cuff"].Value?.ToString(), out float cuffValue) &&
                float.TryParse(row.Cells["CuffLower"].Value?.ToString(), out float cuffLowerValue))
            {
                row.Cells["Cuff"].Style.BackColor = cuffValue < cuffLowerValue ? Color.Red : Color.White;
            }

            if (float.TryParse(row.Cells["Palm"].Value?.ToString(), out float palmValue) &&
                float.TryParse(row.Cells["PalmLower"].Value?.ToString(), out float palmLowerValue))
            {
                row.Cells["Palm"].Style.BackColor = palmValue < palmLowerValue ? Color.Red : Color.White;
            }

            if (float.TryParse(row.Cells["Finger"].Value?.ToString(), out float fingerValue) &&
                float.TryParse(row.Cells["FingerLower"].Value?.ToString(), out float fingerLowerValue))
            {
                row.Cells["Finger"].Style.BackColor = fingerValue < fingerLowerValue ? Color.Red : Color.White;
            }

            if (float.TryParse(row.Cells["FingerTip"].Value?.ToString(), out float fingerTipValue) &&
                float.TryParse(row.Cells["FingerTipLower"].Value?.ToString(), out float fingerTipLowerValue))
            {
                row.Cells["FingerTip"].Style.BackColor = fingerTipValue < fingerTipLowerValue ? Color.Red : Color.White;
            }
        }


        private async void txtRunCard_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    e.Handled = true; // 阻止"叮"声和让TextBox不处理这个键事件

                    string runCardValue = txtRunCard.Text.Trim();

                    if (!string.IsNullOrEmpty(runCardValue))
                    {
                        if (!string.IsNullOrEmpty(runCardApiUrl))
                        {
                            // 使用配置文件中的 API URL
                            string apiUrl = $"{runCardApiUrl}{runCardValue}/";

                            RunCardInfoApiResponse result = await CallRunCardInfoApiAsync(apiUrl);

                            //MessageBox.Show("API 返回结果: " + result);
                            if (result == null)
                            {
                                string msg = "API máy chủ không phản hồi. API Server not response.";
                                message(msg);
                                LogMessage(msg);
                            }
                            else if (result.Status != "OK")
                            {
                                txtRunCard.Text = "";
                                message(result.Message);
                                LogMessage(result.Message);
                            }
                            else
                            {
                                ipqcStd = result.Ipqc_std;
                                rollLower = ipqcStd.LowerRoll;
                                cuffLower = ipqcStd.LowerCuff;
                                palmLower = ipqcStd.LowerPalm;
                                fingerLower = ipqcStd.LowerFinger;
                                fingerTipLower = ipqcStd.LowerFingerTip;
                                message(result.Message);
                                LogMessage(result.Message);
                                txtRoll.Focus();
                            }
                        }
                        else
                        {
                            MessageBox.Show("配置文件中未找到 RunCardApiUrl。");
                        }
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"请求失败: {ex.Message}");
                message(ex.Message);
            }

        }

        public static async Task<ReturnMESApiResponse> PutDataAsync(string url, object data)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    // 将数据对象序列化为 JSON 字符串
                    string json = JsonConvert.SerializeObject(data);

                    // 创建 HttpContent 对象并将 JSON 数据放入请求体
                    HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

                    // 发送 PUT 请求
                    HttpResponseMessage response = await client.PutAsync(url, content);

                    // 确保响应状态码表示成功
                    response.EnsureSuccessStatusCode();

                    // 读取响应内容
                    string responseBody = await response.Content.ReadAsStringAsync();

                    // 反序列化 JSON 字符串为 C# 对象
                    ReturnMESApiResponse apiResponse = JsonConvert.DeserializeObject<ReturnMESApiResponse>(responseBody);

                    return apiResponse;
                }

            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"请求失败: {e.Message}");
                return null;
            }
        }

        public async Task<RunCardInfoApiResponse> CallRunCardInfoApiAsync(string apiUrl)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResponse = await response.Content.ReadAsStringAsync();

                        // 反序列化 JSON 字符串为 C# 对象
                        RunCardInfoApiResponse apiResponse = JsonConvert.DeserializeObject<RunCardInfoApiResponse>(jsonResponse);

                        return apiResponse;
                    }
                    else
                    {
                        // MessageBox.Show($"请求失败，状态码: {response.StatusCode}");
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"请求时发生异常: {ex.Message}");
                return null;
            }
        }

        private void AddRowToDataGridView(string runCard, string roll, string cuff, string palm, string finger, string fingerTip)
        {
            if (dataGridView1.InvokeRequired)
            {
                // Call the method on the UI thread
                dataGridView1.Invoke(new AddRowAndScrollDelegate(AddRowAndScroll), runCard, roll, cuff, palm, finger, fingerTip);
            }
            else
            {
                // Add a new row to the DataGridView
                int rowIndex = dataGridView1.Rows.Add(runCard, roll, cuff, palm, finger, fingerTip);

                // Scroll to the newly added row
                dataGridView1.FirstDisplayedScrollingRowIndex = rowIndex;
            }
        }
        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void InitializeDataGridView()
        {
            // 添加列到 DataGridView
            dataGridView1.Columns.Add("RunCard", "RunCard");
            dataGridView1.Columns.Add("Roll", "Roll");
            dataGridView1.Columns.Add("Cuff", "Cuff");
            dataGridView1.Columns.Add("Palm", "Palm");
            dataGridView1.Columns.Add("Finger", "Finger");
            dataGridView1.Columns.Add("FingerTip", "FingerTip");

            // 設定每個列的寬度
            dataGridView1.Columns["RunCard"].Width = 320;
            dataGridView1.Columns["Roll"].Width = 150;
            dataGridView1.Columns["Cuff"].Width = 150;
            dataGridView1.Columns["Palm"].Width = 150;
            dataGridView1.Columns["Finger"].Width = 150;
            dataGridView1.Columns["FingerTip"].Width = 150;

            // Assuming your DataGridView is named dataGridView1
            dataGridView1.Columns.Add("RollLower", "Roll Lower");
            dataGridView1.Columns["RollLower"].Visible = false;

            dataGridView1.Columns.Add("CuffLower", "Cuff Lower");
            dataGridView1.Columns["CuffLower"].Visible = false;

            dataGridView1.Columns.Add("PalmLower", "Palm Lower");
            dataGridView1.Columns["PalmLower"].Visible = false;

            dataGridView1.Columns.Add("FingerLower", "Finger Lower");
            dataGridView1.Columns["FingerLower"].Visible = false;

            dataGridView1.Columns.Add("FingerTipLower", "FingerTip Lower");
            dataGridView1.Columns["FingerTipLower"].Visible = false;
        }

        // 将字符串格式化为指定的小数位数
        private string FormatDecimal(string text, int decimalPlaces)
        {
            if (decimal.TryParse(text, out decimal value))
            {
                // 使用标准格式字符串 "F" 和指定的小数位数来格式化小数
                return value.ToString($"F{decimalPlaces}");
            }
            return "0"; // 如果不能解析为小数，返回 "0"
        }

        public static string[] GetLocalIPv4Addresses()
        {
            // 获取本地主机名
            string hostName = Dns.GetHostName();

            // 获取本地计算机的 IP 地址
            IPAddress[] ipAddresses = Dns.GetHostAddresses(hostName);

            // 过滤出 IPv4 地址
            var ipv4Addresses = ipAddresses
                .Where(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                .Select(ip => ip.ToString())
                .ToArray();

            return ipv4Addresses;
        }

        private void btnConfig_Click(object sender, EventArgs e)
        {
            using (ConfigForm configForm = new ConfigForm())
            {
                if (configForm.ShowDialog() == DialogResult.OK)
                {
                    // 配置已保存，重新加载配置
                    LoadConfig();
                }
            }
        }

        private void LoadConfig()
        {
            // 从配置文件加载COM端口和API URL
            selectedComPort = ConfigurationManager.AppSettings["SelectedComPort"];
            runCardApiUrl = ConfigurationManager.AppSettings["RunCardApiUrl"];
            returnMESApiUrl = ConfigurationManager.AppSettings["ReturnMesApiUrl"];
            RollPort = ConfigurationManager.AppSettings["RollPort"];
            CuffPort = ConfigurationManager.AppSettings["CuffPort"];
            PalmPort = ConfigurationManager.AppSettings["PalmPort"];
            FingerPort = ConfigurationManager.AppSettings["FingerPort"];
            FingerTipPort = ConfigurationManager.AppSettings["FingerTipPort"];

            if (string.IsNullOrEmpty(returnMESApiUrl))
            {
                // Set txtRunCard to read-only and show an error message
                txtRunCard.ReadOnly = true;
                message("Return MES API URL is not configured. Please check the settings.");
            }
            else if (string.IsNullOrEmpty(runCardApiUrl))
            {
                // Set txtRunCard to read-only and show an error message
                txtRunCard.ReadOnly = true;
                message("RunCard Info API URL is not configured. Please check the settings.");
            }
            else
            {
                // Set txtRunCard to editable if returnMESApiUrl is configured
                txtRunCard.ReadOnly = false;
            }

            // 初始化 COM 端口（如果配置了的话）
            if (!string.IsNullOrEmpty(selectedComPort))
            {
                if (serialPort != null)
                {
                    serialPort.Close();
                }

                serialPort = new SerialPort(selectedComPort, 9600);
                serialPort.DataReceived += new SerialDataReceivedEventHandler(SerialPort_DataReceived);

                try
                {
                    // Attempt to open the serial port
                    serialPort.Open();
                    serialPort.DiscardInBuffer();
                    message("Cổng nối tiếp được mở thành công. Serial port opened successfully.");
                }
                catch (UnauthorizedAccessException ex)
                {
                    // Check if the port is already open or access is denied
                    if (ex.Message.Contains("is denied"))
                    {
                        MessageBox.Show($"Access to the port '{serialPort.PortName}' is denied. Ensure that the port is not in use by another application and you have the required permissions.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show($"Unauthorized access: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (IOException ex)
                {
                    // Handle IO exceptions, such as invalid port name or hardware failure
                    MessageBox.Show($"I/O error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    // Handle any other exceptions that might occur
                    MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // 关闭 COM 端口
            if (serialPort != null && serialPort.IsOpen)
            {
                try
                {
                    serialPort.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"关闭 COM 端口时出错: {ex.Message}");
                }
                finally
                {
                    serialPort.Dispose();
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            // 清空 DataGridView 中的所有行
            dataGridView1.Rows.Clear();

            // 清空 TextBox 中的所有文本
            txtRunCard.Clear();
            txtRoll.Clear();
            txtCuff.Clear();
            txtPalm.Clear();
            txtFinger.Clear();
            txtFingerTip.Clear();
            txtOriginal.Clear();
            txtReceivedData.Clear();

        }

        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            
            string data = sp.ReadExisting(); // 读取所有接收到的数据

            receivedDataBuffer.Append(data);

            // Check if the buffer contains a complete message (ending with '\n')
            if (receivedDataBuffer.ToString().Contains("\r"))
            {
                data = receivedDataBuffer.ToString();

                if (ipqcStd == null)
                {
                    // Display an error message to the user on the UI thread
                    Invoke_message("Giá trị Runcard không hợp lệ. Vui lòng nhập lại giá trị Runcard trước khi tiếp tục. ");
                    return; // Exit the method to prevent further processing
                }

                UpdateTextBox(txtOriginal, data);

                // 处理每一项数据
                if (data.StartsWith("01B+")
                    || data.StartsWith("1B+")
                    || data.StartsWith("02B+")
                    || data.StartsWith("2B+")
                    || data.StartsWith("03B+")
                    || data.StartsWith("3B+")
                    || data.StartsWith("04B+")
                    || data.StartsWith("4B+"))
                {
                    // 去除前缀 "01B+"
                    string value = data.Substring(4);

                    if (data.Length >= 8)
                    {
                        value = data.Substring(data.Length - 8);
                    }
                    else
                    {
                        value = "0";
                    }

                    if (float.TryParse(value, out float floatValue))
                    {
                        // Step 3: Assign the converted float value to the TextBox
                        AssignValueToTextBox(floatValue);
                    }
                    else
                    {
                        // Handle the case where the string could not be converted to a float
                        MessageBox.Show("Invalid data format: Unable to convert to a float.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    CheckAndInsertData();
                }

                receivedDataBuffer.Clear();
                
            }
            
        }

        private void AssignValueToTextBox(float value)
        {
            try
            {
                if (string.IsNullOrEmpty(txtRoll.Text))
                {
                    if (ipqcStd != null && ipqcStd.LowerRoll != null && value < float.Parse(ipqcStd.LowerRoll))
                    {
                        Invoke_TextBox_Color(txtRoll, Color.Red);
                    }
                    else
                    {
                        Invoke_TextBox_Color(txtRoll, Color.Black);
                    }

                    roll = value.ToString("F2"); //Round 2
                    UpdateTextBox(txtRoll, roll);
                }
                else if (string.IsNullOrEmpty(txtCuff.Text))
                {
                    if (ipqcStd != null && ipqcStd.LowerCuff != null && value < float.Parse(ipqcStd.LowerCuff))
                    {
                        Invoke_TextBox_Color(txtCuff, Color.Red);
                    }
                    else
                    {
                        Invoke_TextBox_Color(txtCuff, Color.Black);
                    }

                    cuff = value.ToString("F2");
                    UpdateTextBox(txtCuff, cuff);
                }
                else if (string.IsNullOrEmpty(txtPalm.Text))
                {
                    if (ipqcStd != null && ipqcStd.LowerPalm != null && value < float.Parse(ipqcStd.LowerPalm))
                    {
                        Invoke_TextBox_Color(txtPalm, Color.Red);
                    }
                    else
                    {
                        Invoke_TextBox_Color(txtPalm, Color.Black);
                    }

                    palm = value.ToString("F2");
                    UpdateTextBox(txtPalm, palm);
                }
                else if (string.IsNullOrEmpty(txtFinger.Text))
                {
                    if (ipqcStd != null && ipqcStd.LowerFinger != null && value < float.Parse(ipqcStd.LowerFinger))
                    {
                        Invoke_TextBox_Color(txtFinger, Color.Red);
                    }
                    else
                    {
                        Invoke_TextBox_Color(txtFinger, Color.Black);
                    }
                    
                    finger = value.ToString("F2");
                    UpdateTextBox(txtFinger, finger);
                }
                else if (string.IsNullOrEmpty(txtFingerTip.Text))
                {
                    if (ipqcStd != null && ipqcStd.LowerFingerTip != null && value < float.Parse(ipqcStd.LowerFingerTip))
                    {
                        Invoke_TextBox_Color(txtFingerTip, Color.Red);
                    }
                    else
                    {
                        Invoke_TextBox_Color(txtFingerTip, Color.Black);
                    }
                    
                    fingerTip = value.ToString("F3"); //Round 3
                    UpdateTextBox(txtFingerTip, fingerTip);
                }
            }
            catch (Exception ex)
            {
                Invoke_message(ex.Message);
            }
            
        }

        private async void CheckAndInsertData()
        {
            // 检查所有输入框是否都有值
            if (!string.IsNullOrWhiteSpace(txtRunCard.Text) &&
                !string.IsNullOrWhiteSpace(txtRoll.Text) &&
                !string.IsNullOrWhiteSpace(txtCuff.Text) &&
                !string.IsNullOrWhiteSpace(txtPalm.Text) &&
                !string.IsNullOrWhiteSpace(txtFinger.Text) &&
                !string.IsNullOrWhiteSpace(txtFingerTip.Text))
            {

                runCard = txtRunCard.Text;
                roll = txtRoll.Text;
                cuff = txtCuff.Text;
                palm = txtPalm.Text;
                finger = txtFinger.Text;
                fingerTip = txtFingerTip.Text;

                float fingerValue = float.Parse(finger);
                float fingerTipValue = float.Parse(fingerTip);

                // Perform the arithmetic and rounding
                finger = Math.Round(fingerValue / 2, 2).ToString();
                fingerTip = Math.Round(fingerTipValue / 2, 3).ToString();

                // 要发送的 JSON 数据
                var data = new
                {
                    runcard = runCard,
                    local_ip = GetLocalIPv4Addresses()[0],
                    roll = roll,
                    cuff = cuff,
                    palm = palm,
                    finger = finger,
                    finger_tip = fingerTip
                };

                // 调用 API 并获取响应
                string apiUrl = $"{returnMESApiUrl}";
                ReturnMESApiResponse response = await MainForm.PutDataAsync(apiUrl, data);
                if (response != null)
                {
                    Invoke_message(response.message);
                    LogMessage(response.message);
                }

                // 插入数据到 DataGridView 中
                AddRowToDataGridView(runCard, roll, cuff, palm, finger, fingerTip);

                // 清空 TextBox 以便下次输入 重新聚焦第一个输入框
                UpdateControls();

                runCard = "";
                roll = "";
                cuff = "";
                palm = "";
                finger = "";
                fingerTip = "";
                ipqcStd = null;
                Invoke_TextBox_Color(txtRoll, Color.Black);
                Invoke_TextBox_Color(txtCuff, Color.Black);
                Invoke_TextBox_Color(txtPalm, Color.Black);
                Invoke_TextBox_Color(txtFinger, Color.Black);
                Invoke_TextBox_Color(txtFingerTip, Color.Black);
            }
        }

        private void txtRoll_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                txtCuff.Focus();
            }
        }

        private void txtCuff_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                txtPalm.Focus();
            }
        }

        private void txtPalm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                txtFinger.Focus();
            }
        }

        private void txtFinger_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                txtFingerTip.Focus();
            }
        }

        private void txtFingerTip_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                CheckAndInsertData(); // Execute the function to check and insert data
            }
        }

        public static void LogMessage(string message)
        {
            try
            {
                // Get the current date
                DateTime now = DateTime.Now;

                // Create the directory structure: Logs/YYYY/MM/
                string year = now.ToString("yyyy");
                string month = now.ToString("MM");
                string day = now.ToString("yyyy-MM-dd");

                string monthDirectory = Path.Combine(logDirectory, year, month);

                // Ensure the directory exists
                if (!Directory.Exists(monthDirectory))
                {
                    Directory.CreateDirectory(monthDirectory);
                }

                // Define the log file name
                string logFilePath = Path.Combine(monthDirectory, $"{day}.log");

                // Format the log message with a timestamp
                string logMessage = $"{now:yyyy-MM-dd HH:mm:ss} - {message}";

                // Write the log message to the file, appending if it already exists
                File.AppendAllText(logFilePath, logMessage + Environment.NewLine);
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., log to a fallback location or notify the user)
                Console.WriteLine($"Error logging message: {ex.Message}");
            }
        }

    }
}
