namespace ComPortReader
{
    partial class ConfigForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            comboBoxComPorts = new ComboBox();
            textBoxRunCardApiUrl = new TextBox();
            textBoxReturnMesApiUrl = new TextBox();
            btnSave = new Button();
            lbComPort = new Label();
            lbRuncardApi = new Label();
            lbReturnMES = new Label();
            SuspendLayout();
            // 
            // comboBoxComPorts
            // 
            comboBoxComPorts.FormattingEnabled = true;
            comboBoxComPorts.Location = new Point(238, 69);
            comboBoxComPorts.Name = "comboBoxComPorts";
            comboBoxComPorts.Size = new Size(462, 28);
            comboBoxComPorts.TabIndex = 0;
            // 
            // textBoxRunCardApiUrl
            // 
            textBoxRunCardApiUrl.Location = new Point(238, 123);
            textBoxRunCardApiUrl.Name = "textBoxRunCardApiUrl";
            textBoxRunCardApiUrl.Size = new Size(462, 27);
            textBoxRunCardApiUrl.TabIndex = 1;
            // 
            // textBoxReturnMesApiUrl
            // 
            textBoxReturnMesApiUrl.Location = new Point(238, 181);
            textBoxReturnMesApiUrl.Name = "textBoxReturnMesApiUrl";
            textBoxReturnMesApiUrl.Size = new Size(462, 27);
            textBoxReturnMesApiUrl.TabIndex = 2;
            // 
            // btnSave
            // 
            btnSave.Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnSave.Location = new Point(335, 254);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(153, 53);
            btnSave.TabIndex = 3;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // lbComPort
            // 
            lbComPort.AutoSize = true;
            lbComPort.Location = new Point(151, 72);
            lbComPort.Name = "lbComPort";
            lbComPort.Size = new Size(81, 20);
            lbComPort.TabIndex = 4;
            lbComPort.Text = "COM PORT";
            // 
            // lbRuncardApi
            // 
            lbRuncardApi.AutoSize = true;
            lbRuncardApi.Location = new Point(98, 126);
            lbRuncardApi.Name = "lbRuncardApi";
            lbRuncardApi.Size = new Size(134, 20);
            lbRuncardApi.TabIndex = 5;
            lbRuncardApi.Text = "RUNCARD API URL";
            // 
            // lbReturnMES
            // 
            lbReturnMES.AutoSize = true;
            lbReturnMES.Location = new Point(79, 184);
            lbReturnMES.Name = "lbReturnMES";
            lbReturnMES.Size = new Size(153, 20);
            lbReturnMES.TabIndex = 6;
            lbReturnMES.Text = "RETURN MES API URL";
            // 
            // ConfigForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(823, 358);
            Controls.Add(lbReturnMES);
            Controls.Add(lbRuncardApi);
            Controls.Add(lbComPort);
            Controls.Add(btnSave);
            Controls.Add(textBoxReturnMesApiUrl);
            Controls.Add(textBoxRunCardApiUrl);
            Controls.Add(comboBoxComPorts);
            Name = "ConfigForm";
            Text = "ConfigForm";
            Load += ConfigForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox comboBoxComPorts;
        private TextBox textBoxRunCardApiUrl;
        private TextBox textBoxReturnMesApiUrl;
        private Button btnSave;
        private Label lbComPort;
        private Label lbRuncardApi;
        private Label lbReturnMES;
    }
}