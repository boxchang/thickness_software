namespace ComPortReader
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
            txtReceivedData = new TextBox();
            txtOriginal = new TextBox();
            dataGridView1 = new DataGridView();
            txtRunCard = new TextBox();
            txtRoll = new TextBox();
            txtCuff = new TextBox();
            txtPalm = new TextBox();
            txtFinger = new TextBox();
            txtFingerTip = new TextBox();
            lbRuncard = new Label();
            lbRoll = new Label();
            lbCuff = new Label();
            lbPalm = new Label();
            lbFinger = new Label();
            lbFingerTip = new Label();
            btnConfig = new Button();
            btnClear = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // txtReceivedData
            // 
            txtReceivedData.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtReceivedData.Font = new Font("Segoe UI", 14F);
            txtReceivedData.Location = new Point(12, 511);
            txtReceivedData.Multiline = true;
            txtReceivedData.Name = "txtReceivedData";
            txtReceivedData.ReadOnly = true;
            txtReceivedData.Size = new Size(1290, 302);
            txtReceivedData.TabIndex = 6;
            // 
            // txtOriginal
            // 
            txtOriginal.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtOriginal.Location = new Point(12, 819);
            txtOriginal.Name = "txtOriginal";
            txtOriginal.ReadOnly = true;
            txtOriginal.Size = new Size(1290, 27);
            txtOriginal.TabIndex = 7;
            // 
            // dataGridView1
            // 
            dataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(12, 169);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(1290, 336);
            dataGridView1.TabIndex = 5;
            // 
            // txtRunCard
            // 
            txtRunCard.Font = new Font("Microsoft Sans Serif", 19.8000011F);
            txtRunCard.Location = new Point(82, 118);
            txtRunCard.Name = "txtRunCard";
            txtRunCard.Size = new Size(290, 45);
            txtRunCard.TabIndex = 0;
            txtRunCard.KeyPress += txtRunCard_KeyPress;
            // 
            // txtRoll
            // 
            txtRoll.Font = new Font("Microsoft Sans Serif", 19.8000011F);
            txtRoll.Location = new Point(390, 118);
            txtRoll.Name = "txtRoll";
            txtRoll.Size = new Size(140, 45);
            txtRoll.TabIndex = 1;
            txtRoll.TextAlign = HorizontalAlignment.Center;
            txtRoll.KeyDown += txtRoll_KeyDown;
            // 
            // txtCuff
            // 
            txtCuff.Font = new Font("Microsoft Sans Serif", 19.8000011F);
            txtCuff.Location = new Point(538, 118);
            txtCuff.Name = "txtCuff";
            txtCuff.Size = new Size(140, 45);
            txtCuff.TabIndex = 2;
            txtCuff.TextAlign = HorizontalAlignment.Center;
            txtCuff.KeyDown += txtCuff_KeyDown;
            // 
            // txtPalm
            // 
            txtPalm.Font = new Font("Microsoft Sans Serif", 19.8000011F);
            txtPalm.Location = new Point(685, 118);
            txtPalm.Name = "txtPalm";
            txtPalm.Size = new Size(140, 45);
            txtPalm.TabIndex = 3;
            txtPalm.TextAlign = HorizontalAlignment.Center;
            txtPalm.KeyDown += txtPalm_KeyDown;
            // 
            // txtFinger
            // 
            txtFinger.Font = new Font("Microsoft Sans Serif", 19.8000011F);
            txtFinger.Location = new Point(831, 118);
            txtFinger.Name = "txtFinger";
            txtFinger.Size = new Size(140, 45);
            txtFinger.TabIndex = 4;
            txtFinger.TextAlign = HorizontalAlignment.Center;
            txtFinger.KeyDown += txtFinger_KeyDown;
            // 
            // txtFingerTip
            // 
            txtFingerTip.Font = new Font("Microsoft Sans Serif", 19.8000011F);
            txtFingerTip.Location = new Point(981, 118);
            txtFingerTip.Name = "txtFingerTip";
            txtFingerTip.Size = new Size(140, 45);
            txtFingerTip.TabIndex = 5;
            txtFingerTip.TextAlign = HorizontalAlignment.Center;
            txtFingerTip.KeyDown += txtFingerTip_KeyDown;
            // 
            // lbRuncard
            // 
            lbRuncard.AutoSize = true;
            lbRuncard.Font = new Font("Microsoft Sans Serif", 12F);
            lbRuncard.Location = new Point(82, 90);
            lbRuncard.Name = "lbRuncard";
            lbRuncard.Size = new Size(85, 25);
            lbRuncard.TabIndex = 13;
            lbRuncard.Text = "Runcard";
            // 
            // lbRoll
            // 
            lbRoll.AutoSize = true;
            lbRoll.Font = new Font("Microsoft Sans Serif", 12F);
            lbRoll.Location = new Point(388, 90);
            lbRoll.Name = "lbRoll";
            lbRoll.Size = new Size(148, 25);
            lbRoll.TabIndex = 14;
            lbRoll.Text = "Cuốn biên(dày)";
            // 
            // lbCuff
            // 
            lbCuff.AutoSize = true;
            lbCuff.Font = new Font("Microsoft Sans Serif", 12F);
            lbCuff.Location = new Point(542, 90);
            lbCuff.Name = "lbCuff";
            lbCuff.Size = new Size(120, 25);
            lbCuff.TabIndex = 15;
            lbCuff.Text = "Cổ tay (dày)";
            // 
            // lbPalm
            // 
            lbPalm.AutoSize = true;
            lbPalm.Font = new Font("Microsoft Sans Serif", 12F);
            lbPalm.Location = new Point(688, 90);
            lbPalm.Name = "lbPalm";
            lbPalm.Size = new Size(129, 25);
            lbPalm.TabIndex = 16;
            lbPalm.Text = "Bàn tay (dày)";
            // 
            // lbFinger
            // 
            lbFinger.AutoSize = true;
            lbFinger.Font = new Font("Microsoft Sans Serif", 12F);
            lbFinger.Location = new Point(833, 90);
            lbFinger.Name = "lbFinger";
            lbFinger.Size = new Size(141, 25);
            lbFinger.TabIndex = 17;
            lbFinger.Text = "Ngón tay (dày)";
            // 
            // lbFingerTip
            // 
            lbFingerTip.AutoSize = true;
            lbFingerTip.Font = new Font("Microsoft Sans Serif", 12F);
            lbFingerTip.Location = new Point(980, 90);
            lbFingerTip.Name = "lbFingerTip";
            lbFingerTip.Size = new Size(160, 25);
            lbFingerTip.TabIndex = 18;
            lbFingerTip.Text = "D Ngón tay (dày)";
            // 
            // btnConfig
            // 
            btnConfig.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnConfig.Location = new Point(1200, 12);
            btnConfig.Name = "btnConfig";
            btnConfig.Size = new Size(94, 29);
            btnConfig.TabIndex = 19;
            btnConfig.Text = "Setting";
            btnConfig.UseVisualStyleBackColor = true;
            btnConfig.Click += btnConfig_Click;
            // 
            // btnClear
            // 
            btnClear.Font = new Font("Microsoft Sans Serif", 16.2F);
            btnClear.Location = new Point(1132, 118);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(162, 43);
            btnClear.TabIndex = 21;
            btnClear.Text = "Xóa";
            btnClear.UseVisualStyleBackColor = true;
            btnClear.Click += btnClear_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1322, 858);
            Controls.Add(btnClear);
            Controls.Add(btnConfig);
            Controls.Add(lbFingerTip);
            Controls.Add(lbFinger);
            Controls.Add(lbPalm);
            Controls.Add(lbCuff);
            Controls.Add(lbRoll);
            Controls.Add(lbRuncard);
            Controls.Add(txtFingerTip);
            Controls.Add(txtFinger);
            Controls.Add(txtPalm);
            Controls.Add(txtCuff);
            Controls.Add(txtRoll);
            Controls.Add(txtRunCard);
            Controls.Add(dataGridView1);
            Controls.Add(txtOriginal);
            Controls.Add(txtReceivedData);
            Name = "MainForm";
            Text = "IPQC Thickness Software V1.1";
            Load += MainForm_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private TextBox txtReceivedData;
        private TextBox txtOriginal;
        private DataGridView dataGridView1;
        private TextBox txtRunCard;
        private TextBox txtRoll;
        private TextBox txtCuff;
        private TextBox txtPalm;
        private TextBox txtFinger;
        private TextBox txtFingerTip;
        private Label lbRuncard;
        private Label lbRoll;
        private Label lbCuff;
        private Label lbPalm;
        private Label lbFinger;
        private Label lbFingerTip;
        private Button btnConfig;
        private Button btnClear;
    }
}
