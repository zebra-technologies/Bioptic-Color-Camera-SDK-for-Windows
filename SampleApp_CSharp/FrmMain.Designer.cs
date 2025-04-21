namespace CameraSDKSampleApp
{
    partial class FrmMain
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
            msImage.Dispose();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.picImage = new System.Windows.Forms.PictureBox();
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.btnSetDefaults = new System.Windows.Forms.Button();
            this.btnReboot = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnWriteToFlash = new System.Windows.Forms.Button();
            this.chkContImageEvent = new System.Windows.Forms.CheckBox();
            this.chkSnapshotImageEvent = new System.Windows.Forms.CheckBox();
            this.chkProduceImageEvent = new System.Windows.Forms.CheckBox();
            this.cboDevices = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnOpen = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtHardwareVersion = new System.Windows.Forms.TextBox();
            this.txtFirmwareVersion = new System.Windows.Forms.TextBox();
            this.btnSnapshot = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.txtDateOfFirstProgram = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.txtDateOfManufacture = new System.Windows.Forms.TextBox();
            this.txtSerialNumber = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtDateOfService = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtModelNumber = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label34 = new System.Windows.Forms.Label();
            this.txtWeightData = new System.Windows.Forms.TextBox();
            this.pictureBoxDeviceAwake = new System.Windows.Forms.PictureBox();
            this.label33 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.txtDecodeData = new System.Windows.Forms.TextBox();
            this.txtImageRes = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.txtTimestamp = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.txtImageFormat = new System.Windows.Forms.TextBox();
            this.txtImageType = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.txtImageSize = new System.Windows.Forms.TextBox();
            this.label26 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.chkCameraStreamStatusEvent = new System.Windows.Forms.CheckBox();
            this.chkDetectBoundingBox = new System.Windows.Forms.CheckBox();
            this.btnSetBackground = new System.Windows.Forms.Button();
            this.chkDecodeSessionStatusEvent = new System.Windows.Forms.CheckBox();
            this.btnRetieveConfig = new System.Windows.Forms.Button();
            this.btnLoadConfig = new System.Windows.Forms.Button();
            this.label32 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.btnWBCSet = new System.Windows.Forms.Button();
            this.btnWBCGet = new System.Windows.Forms.Button();
            this.txtWBCRed = new System.Windows.Forms.TextBox();
            this.txtWBCBlue = new System.Windows.Forms.TextBox();
            this.label30 = new System.Windows.Forms.Label();
            this.cbPowerUserMode = new System.Windows.Forms.ComboBox();
            this.trkExposure = new System.Windows.Forms.TrackBar();
            this.trkGain = new System.Windows.Forms.TrackBar();
            this.trkBacklight = new System.Windows.Forms.TrackBar();
            this.chkAutoWhiteBalanceRed = new System.Windows.Forms.CheckBox();
            this.txtWhiteBalanceRed = new System.Windows.Forms.TextBox();
            this.trkWhiteBalanceRed = new System.Windows.Forms.TrackBar();
            this.label28 = new System.Windows.Forms.Label();
            this.chkAutoGain = new System.Windows.Forms.CheckBox();
            this.chkAutoBacklight = new System.Windows.Forms.CheckBox();
            this.chkAutoGamma = new System.Windows.Forms.CheckBox();
            this.chkAutoSharpness = new System.Windows.Forms.CheckBox();
            this.chkAutoSaturation = new System.Windows.Forms.CheckBox();
            this.chkAutoContrast = new System.Windows.Forms.CheckBox();
            this.chkAutoBrightness = new System.Windows.Forms.CheckBox();
            this.chkAutoExposure = new System.Windows.Forms.CheckBox();
            this.chkAutoWhiteBalanceBlue = new System.Windows.Forms.CheckBox();
            this.txtExposure = new System.Windows.Forms.TextBox();
            this.txtGain = new System.Windows.Forms.TextBox();
            this.txtBacklight = new System.Windows.Forms.TextBox();
            this.txtWhiteBalanceBlue = new System.Windows.Forms.TextBox();
            this.txtGamma = new System.Windows.Forms.TextBox();
            this.txtSharpness = new System.Windows.Forms.TextBox();
            this.txtSaturation = new System.Windows.Forms.TextBox();
            this.txtContrast = new System.Windows.Forms.TextBox();
            this.txtBrightness = new System.Windows.Forms.TextBox();
            this.trkWhiteBalanceBlue = new System.Windows.Forms.TrackBar();
            this.trkGamma = new System.Windows.Forms.TrackBar();
            this.trkSharpness = new System.Windows.Forms.TrackBar();
            this.cboImageFormat = new System.Windows.Forms.ComboBox();
            this.cboImageResolution = new System.Windows.Forms.ComboBox();
            this.cboIlluminationMode = new System.Windows.Forms.ComboBox();
            this.cboVideoMode = new System.Windows.Forms.ComboBox();
            this.trkSaturation = new System.Windows.Forms.TrackBar();
            this.trkContrast = new System.Windows.Forms.TrackBar();
            this.trkBrightness = new System.Windows.Forms.TrackBar();
            this.chkDecodeImageEvent = new System.Windows.Forms.CheckBox();
            this.label21 = new System.Windows.Forms.Label();
            this.lbl = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.btnCancelFWDownload = new System.Windows.Forms.Button();
            this.progressFWDownload = new System.Windows.Forms.ProgressBar();
            this.btnLaunch = new System.Windows.Forms.Button();
            this.btnFirmwareUpdate = new System.Windows.Forms.Button();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.txtFwFile = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.chkAutoSave = new System.Windows.Forms.CheckBox();
            this.label27 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnBrowsImageSaveLocation = new System.Windows.Forms.Button();
            this.txtImageSaveLocation = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtCameraStream = new System.Windows.Forms.TextBox();
            this.label35 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picImage)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDeviceAwake)).BeginInit();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trkExposure)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkGain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkBacklight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkWhiteBalanceRed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkWhiteBalanceBlue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkGamma)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkSharpness)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkSaturation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkContrast)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkBrightness)).BeginInit();
            this.groupBox7.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // picImage
            // 
            this.picImage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.picImage.Location = new System.Drawing.Point(16, 25);
            this.picImage.Margin = new System.Windows.Forms.Padding(2);
            this.picImage.Name = "picImage";
            this.picImage.Size = new System.Drawing.Size(402, 323);
            this.picImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picImage.TabIndex = 1;
            this.picImage.TabStop = false;
            // 
            // txtStatus
            // 
            this.txtStatus.BackColor = System.Drawing.SystemColors.ControlLight;
            this.txtStatus.Location = new System.Drawing.Point(7, 22);
            this.txtStatus.Margin = new System.Windows.Forms.Padding(2);
            this.txtStatus.Multiline = true;
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.ReadOnly = true;
            this.txtStatus.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtStatus.Size = new System.Drawing.Size(458, 234);
            this.txtStatus.TabIndex = 4;
            this.txtStatus.WordWrap = false;
            // 
            // btnSetDefaults
            // 
            this.btnSetDefaults.Location = new System.Drawing.Point(378, 171);
            this.btnSetDefaults.Margin = new System.Windows.Forms.Padding(2);
            this.btnSetDefaults.Name = "btnSetDefaults";
            this.btnSetDefaults.Size = new System.Drawing.Size(90, 26);
            this.btnSetDefaults.TabIndex = 8;
            this.btnSetDefaults.Text = "Set Defaults";
            this.btnSetDefaults.UseVisualStyleBackColor = true;
            this.btnSetDefaults.Click += new System.EventHandler(this.btnSetDefaults_Click);
            // 
            // btnReboot
            // 
            this.btnReboot.Location = new System.Drawing.Point(378, 200);
            this.btnReboot.Margin = new System.Windows.Forms.Padding(2);
            this.btnReboot.Name = "btnReboot";
            this.btnReboot.Size = new System.Drawing.Size(90, 26);
            this.btnReboot.TabIndex = 9;
            this.btnReboot.Text = "Reboot";
            this.btnReboot.UseVisualStyleBackColor = true;
            this.btnReboot.Click += new System.EventHandler(this.btnReboot_Click);
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.Location = new System.Drawing.Point(375, 260);
            this.btnClear.Margin = new System.Windows.Forms.Padding(2);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(90, 26);
            this.btnClear.TabIndex = 10;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnWriteToFlash
            // 
            this.btnWriteToFlash.Location = new System.Drawing.Point(378, 142);
            this.btnWriteToFlash.Margin = new System.Windows.Forms.Padding(2);
            this.btnWriteToFlash.Name = "btnWriteToFlash";
            this.btnWriteToFlash.Size = new System.Drawing.Size(90, 26);
            this.btnWriteToFlash.TabIndex = 12;
            this.btnWriteToFlash.Text = "Write to Flash";
            this.btnWriteToFlash.UseVisualStyleBackColor = true;
            this.btnWriteToFlash.Click += new System.EventHandler(this.btnWriteToFlash_Click);
            // 
            // chkContImageEvent
            // 
            this.chkContImageEvent.AutoSize = true;
            this.chkContImageEvent.Location = new System.Drawing.Point(548, 182);
            this.chkContImageEvent.Margin = new System.Windows.Forms.Padding(2);
            this.chkContImageEvent.Name = "chkContImageEvent";
            this.chkContImageEvent.Size = new System.Drawing.Size(142, 17);
            this.chkContImageEvent.TabIndex = 13;
            this.chkContImageEvent.Text = "Continuous Image Event";
            this.chkContImageEvent.UseVisualStyleBackColor = true;
            this.chkContImageEvent.CheckedChanged += new System.EventHandler(this.chkContImageEvent_CheckedChanged);
            // 
            // chkSnapshotImageEvent
            // 
            this.chkSnapshotImageEvent.AutoSize = true;
            this.chkSnapshotImageEvent.Location = new System.Drawing.Point(548, 211);
            this.chkSnapshotImageEvent.Margin = new System.Windows.Forms.Padding(2);
            this.chkSnapshotImageEvent.Name = "chkSnapshotImageEvent";
            this.chkSnapshotImageEvent.Size = new System.Drawing.Size(134, 17);
            this.chkSnapshotImageEvent.TabIndex = 14;
            this.chkSnapshotImageEvent.Text = "Snapshot Image Event";
            this.chkSnapshotImageEvent.UseVisualStyleBackColor = true;
            this.chkSnapshotImageEvent.CheckedChanged += new System.EventHandler(this.chkSnapshotImageEvent_CheckedChanged);
            // 
            // chkProduceImageEvent
            // 
            this.chkProduceImageEvent.AutoSize = true;
            this.chkProduceImageEvent.Location = new System.Drawing.Point(548, 271);
            this.chkProduceImageEvent.Margin = new System.Windows.Forms.Padding(2);
            this.chkProduceImageEvent.Name = "chkProduceImageEvent";
            this.chkProduceImageEvent.Size = new System.Drawing.Size(129, 17);
            this.chkProduceImageEvent.TabIndex = 15;
            this.chkProduceImageEvent.Text = "Produce Image Event";
            this.chkProduceImageEvent.UseVisualStyleBackColor = true;
            this.chkProduceImageEvent.CheckedChanged += new System.EventHandler(this.chkProduceImageEvent_CheckedChanged);
            // 
            // cboDevices
            // 
            this.cboDevices.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDevices.FormattingEnabled = true;
            this.cboDevices.Location = new System.Drawing.Point(122, 20);
            this.cboDevices.Margin = new System.Windows.Forms.Padding(2);
            this.cboDevices.Name = "cboDevices";
            this.cboDevices.Size = new System.Drawing.Size(250, 21);
            this.cboDevices.TabIndex = 16;
            this.cboDevices.SelectionChangeCommitted += new System.EventHandler(this.cboDevices_SelectionChangeCommitted);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 26);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 13);
            this.label1.TabIndex = 17;
            this.label1.Text = "Selected Camera";
            // 
            // btnOpen
            // 
            this.btnOpen.Enabled = false;
            this.btnOpen.Location = new System.Drawing.Point(378, 20);
            this.btnOpen.Margin = new System.Windows.Forms.Padding(2);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(90, 26);
            this.btnOpen.TabIndex = 18;
            this.btnOpen.Text = "Open";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtHardwareVersion);
            this.groupBox1.Controls.Add(this.btnWriteToFlash);
            this.groupBox1.Controls.Add(this.btnSetDefaults);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtFirmwareVersion);
            this.groupBox1.Controls.Add(this.btnSnapshot);
            this.groupBox1.Controls.Add(this.btnReboot);
            this.groupBox1.Controls.Add(this.cboDevices);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txtDateOfFirstProgram);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.btnOpen);
            this.groupBox1.Controls.Add(this.btnClose);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtDateOfManufacture);
            this.groupBox1.Controls.Add(this.txtSerialNumber);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtDateOfService);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtModelNumber);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(8, 8);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(472, 260);
            this.groupBox1.TabIndex = 19;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Camera Properties";
            // 
            // txtHardwareVersion
            // 
            this.txtHardwareVersion.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.txtHardwareVersion.Location = new System.Drawing.Point(122, 228);
            this.txtHardwareVersion.Margin = new System.Windows.Forms.Padding(2);
            this.txtHardwareVersion.Name = "txtHardwareVersion";
            this.txtHardwareVersion.ReadOnly = true;
            this.txtHardwareVersion.Size = new System.Drawing.Size(250, 20);
            this.txtHardwareVersion.TabIndex = 30;
            // 
            // txtFirmwareVersion
            // 
            this.txtFirmwareVersion.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.txtFirmwareVersion.Location = new System.Drawing.Point(122, 198);
            this.txtFirmwareVersion.Margin = new System.Windows.Forms.Padding(2);
            this.txtFirmwareVersion.Name = "txtFirmwareVersion";
            this.txtFirmwareVersion.ReadOnly = true;
            this.txtFirmwareVersion.Size = new System.Drawing.Size(250, 20);
            this.txtFirmwareVersion.TabIndex = 29;
            // 
            // btnSnapshot
            // 
            this.btnSnapshot.Location = new System.Drawing.Point(378, 113);
            this.btnSnapshot.Margin = new System.Windows.Forms.Padding(2);
            this.btnSnapshot.Name = "btnSnapshot";
            this.btnSnapshot.Size = new System.Drawing.Size(90, 26);
            this.btnSnapshot.TabIndex = 13;
            this.btnSnapshot.Text = "Capture";
            this.btnSnapshot.UseVisualStyleBackColor = true;
            this.btnSnapshot.Click += new System.EventHandler(this.btnSnapshot_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(4, 230);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(91, 13);
            this.label8.TabIndex = 23;
            this.label8.Text = "Hardware Version";
            // 
            // txtDateOfFirstProgram
            // 
            this.txtDateOfFirstProgram.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.txtDateOfFirstProgram.Location = new System.Drawing.Point(122, 169);
            this.txtDateOfFirstProgram.Margin = new System.Windows.Forms.Padding(2);
            this.txtDateOfFirstProgram.Name = "txtDateOfFirstProgram";
            this.txtDateOfFirstProgram.ReadOnly = true;
            this.txtDateOfFirstProgram.Size = new System.Drawing.Size(250, 20);
            this.txtDateOfFirstProgram.TabIndex = 28;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(4, 201);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(87, 13);
            this.label7.TabIndex = 22;
            this.label7.Text = "Firmware Version";
            // 
            // btnClose
            // 
            this.btnClose.Enabled = false;
            this.btnClose.Location = new System.Drawing.Point(378, 49);
            this.btnClose.Margin = new System.Windows.Forms.Padding(2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(90, 26);
            this.btnClose.TabIndex = 19;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(4, 171);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(106, 13);
            this.label6.TabIndex = 21;
            this.label6.Text = "Date of First Program";
            // 
            // txtDateOfManufacture
            // 
            this.txtDateOfManufacture.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.txtDateOfManufacture.Location = new System.Drawing.Point(122, 110);
            this.txtDateOfManufacture.Margin = new System.Windows.Forms.Padding(2);
            this.txtDateOfManufacture.Name = "txtDateOfManufacture";
            this.txtDateOfManufacture.ReadOnly = true;
            this.txtDateOfManufacture.Size = new System.Drawing.Size(250, 20);
            this.txtDateOfManufacture.TabIndex = 27;
            // 
            // txtSerialNumber
            // 
            this.txtSerialNumber.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.txtSerialNumber.Location = new System.Drawing.Point(122, 50);
            this.txtSerialNumber.Margin = new System.Windows.Forms.Padding(2);
            this.txtSerialNumber.Name = "txtSerialNumber";
            this.txtSerialNumber.ReadOnly = true;
            this.txtSerialNumber.Size = new System.Drawing.Size(250, 20);
            this.txtSerialNumber.TabIndex = 24;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(4, 142);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(81, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "Date of Service";
            // 
            // txtDateOfService
            // 
            this.txtDateOfService.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.txtDateOfService.Location = new System.Drawing.Point(122, 140);
            this.txtDateOfService.Margin = new System.Windows.Forms.Padding(2);
            this.txtDateOfService.Name = "txtDateOfService";
            this.txtDateOfService.ReadOnly = true;
            this.txtDateOfService.Size = new System.Drawing.Size(250, 20);
            this.txtDateOfService.TabIndex = 26;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 53);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Serial Number";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 113);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(105, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Date of Manufacture";
            // 
            // txtModelNumber
            // 
            this.txtModelNumber.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.txtModelNumber.Location = new System.Drawing.Point(122, 80);
            this.txtModelNumber.Margin = new System.Windows.Forms.Padding(2);
            this.txtModelNumber.Name = "txtModelNumber";
            this.txtModelNumber.ReadOnly = true;
            this.txtModelNumber.Size = new System.Drawing.Size(250, 20);
            this.txtModelNumber.TabIndex = 25;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 82);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Model Number";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label35);
            this.groupBox5.Controls.Add(this.txtCameraStream);
            this.groupBox5.Controls.Add(this.label34);
            this.groupBox5.Controls.Add(this.txtWeightData);
            this.groupBox5.Controls.Add(this.pictureBoxDeviceAwake);
            this.groupBox5.Controls.Add(this.label33);
            this.groupBox5.Controls.Add(this.label29);
            this.groupBox5.Controls.Add(this.txtDecodeData);
            this.groupBox5.Controls.Add(this.txtImageRes);
            this.groupBox5.Controls.Add(this.picImage);
            this.groupBox5.Controls.Add(this.label22);
            this.groupBox5.Controls.Add(this.txtTimestamp);
            this.groupBox5.Controls.Add(this.label23);
            this.groupBox5.Controls.Add(this.txtImageFormat);
            this.groupBox5.Controls.Add(this.txtImageType);
            this.groupBox5.Controls.Add(this.label24);
            this.groupBox5.Controls.Add(this.txtImageSize);
            this.groupBox5.Controls.Add(this.label26);
            this.groupBox5.Controls.Add(this.label25);
            this.groupBox5.Location = new System.Drawing.Point(494, 9);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox5.Size = new System.Drawing.Size(692, 366);
            this.groupBox5.TabIndex = 23;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Event Viewer";
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(446, 203);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(67, 13);
            this.label34.TabIndex = 41;
            this.label34.Text = "Weight Data";
            // 
            // txtWeightData
            // 
            this.txtWeightData.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.txtWeightData.Enabled = false;
            this.txtWeightData.Location = new System.Drawing.Point(548, 200);
            this.txtWeightData.Name = "txtWeightData";
            this.txtWeightData.Size = new System.Drawing.Size(130, 20);
            this.txtWeightData.TabIndex = 40;
            // 
            // pictureBoxDeviceAwake
            // 
            this.pictureBoxDeviceAwake.BackColor = System.Drawing.SystemColors.ControlLight;
            this.pictureBoxDeviceAwake.Location = new System.Drawing.Point(548, 264);
            this.pictureBoxDeviceAwake.Name = "pictureBoxDeviceAwake";
            this.pictureBoxDeviceAwake.Size = new System.Drawing.Size(22, 22);
            this.pictureBoxDeviceAwake.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBoxDeviceAwake.TabIndex = 39;
            this.pictureBoxDeviceAwake.TabStop = false;
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(446, 268);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(77, 13);
            this.label33.TabIndex = 38;
            this.label33.Text = "Device Awake";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(446, 175);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(71, 13);
            this.label29.TabIndex = 37;
            this.label29.Text = "Decode Data";
            // 
            // txtDecodeData
            // 
            this.txtDecodeData.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.txtDecodeData.Enabled = false;
            this.txtDecodeData.Location = new System.Drawing.Point(548, 172);
            this.txtDecodeData.Name = "txtDecodeData";
            this.txtDecodeData.Size = new System.Drawing.Size(130, 20);
            this.txtDecodeData.TabIndex = 36;
            // 
            // txtImageRes
            // 
            this.txtImageRes.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.txtImageRes.Enabled = false;
            this.txtImageRes.Location = new System.Drawing.Point(548, 141);
            this.txtImageRes.Margin = new System.Windows.Forms.Padding(2);
            this.txtImageRes.Name = "txtImageRes";
            this.txtImageRes.Size = new System.Drawing.Size(130, 20);
            this.txtImageRes.TabIndex = 35;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(446, 22);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(62, 13);
            this.label22.TabIndex = 2;
            this.label22.Text = "Event Type";
            // 
            // txtTimestamp
            // 
            this.txtTimestamp.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.txtTimestamp.Enabled = false;
            this.txtTimestamp.Location = new System.Drawing.Point(548, 112);
            this.txtTimestamp.Margin = new System.Windows.Forms.Padding(2);
            this.txtTimestamp.Name = "txtTimestamp";
            this.txtTimestamp.Size = new System.Drawing.Size(130, 20);
            this.txtTimestamp.TabIndex = 34;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(446, 52);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(39, 13);
            this.label23.TabIndex = 3;
            this.label23.Text = "Format";
            // 
            // txtImageFormat
            // 
            this.txtImageFormat.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.txtImageFormat.Enabled = false;
            this.txtImageFormat.Location = new System.Drawing.Point(548, 53);
            this.txtImageFormat.Margin = new System.Windows.Forms.Padding(2);
            this.txtImageFormat.Name = "txtImageFormat";
            this.txtImageFormat.Size = new System.Drawing.Size(130, 20);
            this.txtImageFormat.TabIndex = 32;
            // 
            // txtImageType
            // 
            this.txtImageType.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.txtImageType.Enabled = false;
            this.txtImageType.Location = new System.Drawing.Point(548, 24);
            this.txtImageType.Margin = new System.Windows.Forms.Padding(2);
            this.txtImageType.Name = "txtImageType";
            this.txtImageType.Size = new System.Drawing.Size(130, 20);
            this.txtImageType.TabIndex = 31;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(446, 81);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(50, 13);
            this.label24.TabIndex = 4;
            this.label24.Text = "Size (KB)";
            // 
            // txtImageSize
            // 
            this.txtImageSize.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.txtImageSize.Enabled = false;
            this.txtImageSize.Location = new System.Drawing.Point(548, 81);
            this.txtImageSize.Margin = new System.Windows.Forms.Padding(2);
            this.txtImageSize.Name = "txtImageSize";
            this.txtImageSize.Size = new System.Drawing.Size(130, 20);
            this.txtImageSize.TabIndex = 33;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(446, 111);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(63, 13);
            this.label26.TabIndex = 6;
            this.label26.Text = "Time Stamp";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(446, 141);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(89, 13);
            this.label25.TabIndex = 5;
            this.label25.Text = "Image Resolution";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.chkCameraStreamStatusEvent);
            this.groupBox6.Controls.Add(this.chkDetectBoundingBox);
            this.groupBox6.Controls.Add(this.btnSetBackground);
            this.groupBox6.Controls.Add(this.chkDecodeSessionStatusEvent);
            this.groupBox6.Controls.Add(this.btnRetieveConfig);
            this.groupBox6.Controls.Add(this.btnLoadConfig);
            this.groupBox6.Controls.Add(this.label32);
            this.groupBox6.Controls.Add(this.label31);
            this.groupBox6.Controls.Add(this.btnWBCSet);
            this.groupBox6.Controls.Add(this.btnWBCGet);
            this.groupBox6.Controls.Add(this.txtWBCRed);
            this.groupBox6.Controls.Add(this.txtWBCBlue);
            this.groupBox6.Controls.Add(this.label30);
            this.groupBox6.Controls.Add(this.cbPowerUserMode);
            this.groupBox6.Controls.Add(this.trkExposure);
            this.groupBox6.Controls.Add(this.trkGain);
            this.groupBox6.Controls.Add(this.trkBacklight);
            this.groupBox6.Controls.Add(this.chkAutoWhiteBalanceRed);
            this.groupBox6.Controls.Add(this.txtWhiteBalanceRed);
            this.groupBox6.Controls.Add(this.trkWhiteBalanceRed);
            this.groupBox6.Controls.Add(this.label28);
            this.groupBox6.Controls.Add(this.chkAutoGain);
            this.groupBox6.Controls.Add(this.chkAutoBacklight);
            this.groupBox6.Controls.Add(this.chkAutoGamma);
            this.groupBox6.Controls.Add(this.chkAutoSharpness);
            this.groupBox6.Controls.Add(this.chkAutoSaturation);
            this.groupBox6.Controls.Add(this.chkAutoContrast);
            this.groupBox6.Controls.Add(this.chkAutoBrightness);
            this.groupBox6.Controls.Add(this.chkAutoExposure);
            this.groupBox6.Controls.Add(this.chkAutoWhiteBalanceBlue);
            this.groupBox6.Controls.Add(this.txtExposure);
            this.groupBox6.Controls.Add(this.txtGain);
            this.groupBox6.Controls.Add(this.txtBacklight);
            this.groupBox6.Controls.Add(this.txtWhiteBalanceBlue);
            this.groupBox6.Controls.Add(this.txtGamma);
            this.groupBox6.Controls.Add(this.txtSharpness);
            this.groupBox6.Controls.Add(this.txtSaturation);
            this.groupBox6.Controls.Add(this.txtContrast);
            this.groupBox6.Controls.Add(this.txtBrightness);
            this.groupBox6.Controls.Add(this.trkWhiteBalanceBlue);
            this.groupBox6.Controls.Add(this.trkGamma);
            this.groupBox6.Controls.Add(this.trkSharpness);
            this.groupBox6.Controls.Add(this.cboImageFormat);
            this.groupBox6.Controls.Add(this.cboImageResolution);
            this.groupBox6.Controls.Add(this.cboIlluminationMode);
            this.groupBox6.Controls.Add(this.cboVideoMode);
            this.groupBox6.Controls.Add(this.trkSaturation);
            this.groupBox6.Controls.Add(this.trkContrast);
            this.groupBox6.Controls.Add(this.trkBrightness);
            this.groupBox6.Controls.Add(this.chkDecodeImageEvent);
            this.groupBox6.Controls.Add(this.label21);
            this.groupBox6.Controls.Add(this.lbl);
            this.groupBox6.Controls.Add(this.label19);
            this.groupBox6.Controls.Add(this.label18);
            this.groupBox6.Controls.Add(this.label17);
            this.groupBox6.Controls.Add(this.label16);
            this.groupBox6.Controls.Add(this.label15);
            this.groupBox6.Controls.Add(this.label14);
            this.groupBox6.Controls.Add(this.label13);
            this.groupBox6.Controls.Add(this.label12);
            this.groupBox6.Controls.Add(this.label11);
            this.groupBox6.Controls.Add(this.label10);
            this.groupBox6.Controls.Add(this.label9);
            this.groupBox6.Controls.Add(this.chkSnapshotImageEvent);
            this.groupBox6.Controls.Add(this.chkContImageEvent);
            this.groupBox6.Controls.Add(this.chkProduceImageEvent);
            this.groupBox6.Location = new System.Drawing.Point(494, 386);
            this.groupBox6.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox6.Size = new System.Drawing.Size(692, 390);
            this.groupBox6.TabIndex = 24;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Camera Configuration";
            // 
            // chkCameraStreamStatusEvent
            // 
            this.chkCameraStreamStatusEvent.AutoSize = true;
            this.chkCameraStreamStatusEvent.Location = new System.Drawing.Point(548, 331);
            this.chkCameraStreamStatusEvent.Margin = new System.Windows.Forms.Padding(2);
            this.chkCameraStreamStatusEvent.Name = "chkCameraStreamStatusEvent";
            this.chkCameraStreamStatusEvent.Size = new System.Drawing.Size(126, 17);
            this.chkCameraStreamStatusEvent.TabIndex = 87;
            this.chkCameraStreamStatusEvent.Text = "Camera Status Event";
            this.chkCameraStreamStatusEvent.UseVisualStyleBackColor = true;
            this.chkCameraStreamStatusEvent.CheckedChanged += new System.EventHandler(this.checkBox1chkCameraStreamStatusEvent_CheckedChanged);
            // 
            // chkDetectBoundingBox
            // 
            this.chkDetectBoundingBox.AutoSize = true;
            this.chkDetectBoundingBox.Location = new System.Drawing.Point(548, 359);
            this.chkDetectBoundingBox.Margin = new System.Windows.Forms.Padding(2);
            this.chkDetectBoundingBox.Name = "chkDetectBoundingBox";
            this.chkDetectBoundingBox.Size = new System.Drawing.Size(127, 17);
            this.chkDetectBoundingBox.TabIndex = 86;
            this.chkDetectBoundingBox.Text = "Detect Bounding Box";
            this.chkDetectBoundingBox.UseVisualStyleBackColor = true;
            this.chkDetectBoundingBox.CheckedChanged += new System.EventHandler(this.chkDetectBoundingBox_CheckedChanged);
            // 
            // btnSetBackground
            // 
            this.btnSetBackground.Location = new System.Drawing.Point(391, 349);
            this.btnSetBackground.Name = "btnSetBackground";
            this.btnSetBackground.Size = new System.Drawing.Size(143, 27);
            this.btnSetBackground.TabIndex = 85;
            this.btnSetBackground.Text = "Set Background";
            this.btnSetBackground.UseVisualStyleBackColor = true;
            this.btnSetBackground.Click += new System.EventHandler(this.btnSetBackground_Click);
            // 
            // chkDecodeSessionStatusEvent
            // 
            this.chkDecodeSessionStatusEvent.AutoSize = true;
            this.chkDecodeSessionStatusEvent.Location = new System.Drawing.Point(548, 303);
            this.chkDecodeSessionStatusEvent.Margin = new System.Windows.Forms.Padding(2);
            this.chkDecodeSessionStatusEvent.Name = "chkDecodeSessionStatusEvent";
            this.chkDecodeSessionStatusEvent.Size = new System.Drawing.Size(135, 17);
            this.chkDecodeSessionStatusEvent.TabIndex = 84;
            this.chkDecodeSessionStatusEvent.Text = "Decode Session Event";
            this.chkDecodeSessionStatusEvent.UseVisualStyleBackColor = true;
            this.chkDecodeSessionStatusEvent.CheckedChanged += new System.EventHandler(this.chkDecodeSessionStatusEvent_CheckedChanged);
            // 
            // btnRetieveConfig
            // 
            this.btnRetieveConfig.Location = new System.Drawing.Point(125, 358);
            this.btnRetieveConfig.Name = "btnRetieveConfig";
            this.btnRetieveConfig.Size = new System.Drawing.Size(75, 27);
            this.btnRetieveConfig.TabIndex = 83;
            this.btnRetieveConfig.Text = "Retrieve";
            this.btnRetieveConfig.UseVisualStyleBackColor = true;
            this.btnRetieveConfig.Click += new System.EventHandler(this.btnRetieveConfig_Click);
            // 
            // btnLoadConfig
            // 
            this.btnLoadConfig.Location = new System.Drawing.Point(13, 358);
            this.btnLoadConfig.Name = "btnLoadConfig";
            this.btnLoadConfig.Size = new System.Drawing.Size(75, 27);
            this.btnLoadConfig.TabIndex = 82;
            this.btnLoadConfig.Text = "Load";
            this.btnLoadConfig.UseVisualStyleBackColor = true;
            this.btnLoadConfig.Click += new System.EventHandler(this.btnLoadConfig_Click);
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(406, 279);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(27, 13);
            this.label32.TabIndex = 81;
            this.label32.Text = "Red";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(404, 253);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(28, 13);
            this.label31.TabIndex = 80;
            this.label31.Text = "Blue";
            // 
            // btnWBCSet
            // 
            this.btnWBCSet.Location = new System.Drawing.Point(474, 309);
            this.btnWBCSet.Name = "btnWBCSet";
            this.btnWBCSet.Size = new System.Drawing.Size(62, 23);
            this.btnWBCSet.TabIndex = 79;
            this.btnWBCSet.Text = "WBC_Set";
            this.btnWBCSet.UseVisualStyleBackColor = true;
            this.btnWBCSet.Click += new System.EventHandler(this.btnWBCSet_Click);
            // 
            // btnWBCGet
            // 
            this.btnWBCGet.Location = new System.Drawing.Point(393, 309);
            this.btnWBCGet.Name = "btnWBCGet";
            this.btnWBCGet.Size = new System.Drawing.Size(73, 23);
            this.btnWBCGet.TabIndex = 78;
            this.btnWBCGet.Text = "WBC_Get";
            this.btnWBCGet.UseVisualStyleBackColor = true;
            this.btnWBCGet.Click += new System.EventHandler(this.btnWBCGet_Click);
            // 
            // txtWBCRed
            // 
            this.txtWBCRed.Location = new System.Drawing.Point(449, 275);
            this.txtWBCRed.Name = "txtWBCRed";
            this.txtWBCRed.Size = new System.Drawing.Size(86, 20);
            this.txtWBCRed.TabIndex = 77;
            // 
            // txtWBCBlue
            // 
            this.txtWBCBlue.Location = new System.Drawing.Point(449, 249);
            this.txtWBCBlue.Name = "txtWBCBlue";
            this.txtWBCBlue.Size = new System.Drawing.Size(86, 20);
            this.txtWBCBlue.TabIndex = 76;
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(446, 150);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(89, 13);
            this.label30.TabIndex = 75;
            this.label30.Text = "Power user mode";
            // 
            // cbPowerUserMode
            // 
            this.cbPowerUserMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPowerUserMode.FormattingEnabled = true;
            this.cbPowerUserMode.Items.AddRange(new object[] {
            "Enable",
            "Disable"});
            this.cbPowerUserMode.Location = new System.Drawing.Point(548, 147);
            this.cbPowerUserMode.Name = "cbPowerUserMode";
            this.cbPowerUserMode.Size = new System.Drawing.Size(130, 21);
            this.cbPowerUserMode.TabIndex = 74;
            this.cbPowerUserMode.SelectedIndexChanged += new System.EventHandler(this.cbPowerUserMode_SelectedIndexChanged);
            // 
            // trkExposure
            // 
            this.trkExposure.Location = new System.Drawing.Point(113, 316);
            this.trkExposure.Margin = new System.Windows.Forms.Padding(2);
            this.trkExposure.Name = "trkExposure";
            this.trkExposure.Size = new System.Drawing.Size(167, 45);
            this.trkExposure.TabIndex = 47;
            this.trkExposure.Scroll += new System.EventHandler(this.trkExposure_Scroll);
            // 
            // trkGain
            // 
            this.trkGain.Location = new System.Drawing.Point(113, 284);
            this.trkGain.Margin = new System.Windows.Forms.Padding(2);
            this.trkGain.Name = "trkGain";
            this.trkGain.Size = new System.Drawing.Size(167, 45);
            this.trkGain.TabIndex = 43;
            this.trkGain.Scroll += new System.EventHandler(this.trkGain_Scroll);
            // 
            // trkBacklight
            // 
            this.trkBacklight.Location = new System.Drawing.Point(113, 251);
            this.trkBacklight.Margin = new System.Windows.Forms.Padding(2);
            this.trkBacklight.Name = "trkBacklight";
            this.trkBacklight.Size = new System.Drawing.Size(167, 45);
            this.trkBacklight.TabIndex = 48;
            this.trkBacklight.Scroll += new System.EventHandler(this.trkBacklight_Scroll);
            // 
            // chkAutoWhiteBalanceRed
            // 
            this.chkAutoWhiteBalanceRed.AutoSize = true;
            this.chkAutoWhiteBalanceRed.Location = new System.Drawing.Point(346, 220);
            this.chkAutoWhiteBalanceRed.Name = "chkAutoWhiteBalanceRed";
            this.chkAutoWhiteBalanceRed.Size = new System.Drawing.Size(48, 17);
            this.chkAutoWhiteBalanceRed.TabIndex = 73;
            this.chkAutoWhiteBalanceRed.Text = "Auto";
            this.chkAutoWhiteBalanceRed.UseVisualStyleBackColor = true;
            this.chkAutoWhiteBalanceRed.Visible = false;
            // 
            // txtWhiteBalanceRed
            // 
            this.txtWhiteBalanceRed.Location = new System.Drawing.Point(292, 220);
            this.txtWhiteBalanceRed.Name = "txtWhiteBalanceRed";
            this.txtWhiteBalanceRed.ReadOnly = true;
            this.txtWhiteBalanceRed.Size = new System.Drawing.Size(41, 20);
            this.txtWhiteBalanceRed.TabIndex = 72;
            this.txtWhiteBalanceRed.Leave += new System.EventHandler(this.txtWhiteBalanceBlue_Leave);
            // 
            // trkWhiteBalanceRed
            // 
            this.trkWhiteBalanceRed.Location = new System.Drawing.Point(114, 220);
            this.trkWhiteBalanceRed.Margin = new System.Windows.Forms.Padding(2);
            this.trkWhiteBalanceRed.Name = "trkWhiteBalanceRed";
            this.trkWhiteBalanceRed.Size = new System.Drawing.Size(166, 45);
            this.trkWhiteBalanceRed.TabIndex = 71;
            this.trkWhiteBalanceRed.Scroll += new System.EventHandler(this.trkWhiteBalance_Scroll);
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(10, 224);
            this.label28.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(106, 13);
            this.label28.TabIndex = 70;
            this.label28.Text = "White Balance (Red)";
            // 
            // chkAutoGain
            // 
            this.chkAutoGain.AutoSize = true;
            this.chkAutoGain.Location = new System.Drawing.Point(345, 284);
            this.chkAutoGain.Name = "chkAutoGain";
            this.chkAutoGain.Size = new System.Drawing.Size(48, 17);
            this.chkAutoGain.TabIndex = 69;
            this.chkAutoGain.Text = "Auto";
            this.chkAutoGain.UseVisualStyleBackColor = true;
            this.chkAutoGain.Visible = false;
            // 
            // chkAutoBacklight
            // 
            this.chkAutoBacklight.AutoSize = true;
            this.chkAutoBacklight.Location = new System.Drawing.Point(345, 253);
            this.chkAutoBacklight.Name = "chkAutoBacklight";
            this.chkAutoBacklight.Size = new System.Drawing.Size(48, 17);
            this.chkAutoBacklight.TabIndex = 68;
            this.chkAutoBacklight.Text = "Auto";
            this.chkAutoBacklight.UseVisualStyleBackColor = true;
            this.chkAutoBacklight.Visible = false;
            // 
            // chkAutoGamma
            // 
            this.chkAutoGamma.AutoSize = true;
            this.chkAutoGamma.Location = new System.Drawing.Point(345, 156);
            this.chkAutoGamma.Name = "chkAutoGamma";
            this.chkAutoGamma.Size = new System.Drawing.Size(48, 17);
            this.chkAutoGamma.TabIndex = 67;
            this.chkAutoGamma.Text = "Auto";
            this.chkAutoGamma.UseVisualStyleBackColor = true;
            this.chkAutoGamma.Visible = false;
            // 
            // chkAutoSharpness
            // 
            this.chkAutoSharpness.AutoSize = true;
            this.chkAutoSharpness.Location = new System.Drawing.Point(345, 124);
            this.chkAutoSharpness.Name = "chkAutoSharpness";
            this.chkAutoSharpness.Size = new System.Drawing.Size(48, 17);
            this.chkAutoSharpness.TabIndex = 66;
            this.chkAutoSharpness.Text = "Auto";
            this.chkAutoSharpness.UseVisualStyleBackColor = true;
            this.chkAutoSharpness.Visible = false;
            // 
            // chkAutoSaturation
            // 
            this.chkAutoSaturation.AutoSize = true;
            this.chkAutoSaturation.Location = new System.Drawing.Point(345, 91);
            this.chkAutoSaturation.Name = "chkAutoSaturation";
            this.chkAutoSaturation.Size = new System.Drawing.Size(48, 17);
            this.chkAutoSaturation.TabIndex = 65;
            this.chkAutoSaturation.Text = "Auto";
            this.chkAutoSaturation.UseVisualStyleBackColor = true;
            this.chkAutoSaturation.Visible = false;
            // 
            // chkAutoContrast
            // 
            this.chkAutoContrast.AutoSize = true;
            this.chkAutoContrast.Location = new System.Drawing.Point(345, 58);
            this.chkAutoContrast.Name = "chkAutoContrast";
            this.chkAutoContrast.Size = new System.Drawing.Size(48, 17);
            this.chkAutoContrast.TabIndex = 64;
            this.chkAutoContrast.Text = "Auto";
            this.chkAutoContrast.UseVisualStyleBackColor = true;
            this.chkAutoContrast.Visible = false;
            // 
            // chkAutoBrightness
            // 
            this.chkAutoBrightness.AutoSize = true;
            this.chkAutoBrightness.Location = new System.Drawing.Point(345, 26);
            this.chkAutoBrightness.Name = "chkAutoBrightness";
            this.chkAutoBrightness.Size = new System.Drawing.Size(48, 17);
            this.chkAutoBrightness.TabIndex = 63;
            this.chkAutoBrightness.Text = "Auto";
            this.chkAutoBrightness.UseVisualStyleBackColor = true;
            this.chkAutoBrightness.Visible = false;
            // 
            // chkAutoExposure
            // 
            this.chkAutoExposure.AutoSize = true;
            this.chkAutoExposure.Location = new System.Drawing.Point(345, 316);
            this.chkAutoExposure.Name = "chkAutoExposure";
            this.chkAutoExposure.Size = new System.Drawing.Size(48, 17);
            this.chkAutoExposure.TabIndex = 62;
            this.chkAutoExposure.Text = "Auto";
            this.chkAutoExposure.UseVisualStyleBackColor = true;
            this.chkAutoExposure.Visible = false;
            this.chkAutoExposure.CheckedChanged += new System.EventHandler(this.chkAutoExposure_CheckedChanged);
            // 
            // chkAutoWhiteBalanceBlue
            // 
            this.chkAutoWhiteBalanceBlue.AutoSize = true;
            this.chkAutoWhiteBalanceBlue.Location = new System.Drawing.Point(345, 188);
            this.chkAutoWhiteBalanceBlue.Name = "chkAutoWhiteBalanceBlue";
            this.chkAutoWhiteBalanceBlue.Size = new System.Drawing.Size(48, 17);
            this.chkAutoWhiteBalanceBlue.TabIndex = 61;
            this.chkAutoWhiteBalanceBlue.Text = "Auto";
            this.chkAutoWhiteBalanceBlue.UseVisualStyleBackColor = true;
            this.chkAutoWhiteBalanceBlue.Visible = false;
            this.chkAutoWhiteBalanceBlue.CheckedChanged += new System.EventHandler(this.chkAutoWhiteBalance_CheckedChanged);
            // 
            // txtExposure
            // 
            this.txtExposure.Location = new System.Drawing.Point(291, 316);
            this.txtExposure.Name = "txtExposure";
            this.txtExposure.ReadOnly = true;
            this.txtExposure.Size = new System.Drawing.Size(41, 20);
            this.txtExposure.TabIndex = 60;
            // 
            // txtGain
            // 
            this.txtGain.Location = new System.Drawing.Point(291, 284);
            this.txtGain.Name = "txtGain";
            this.txtGain.ReadOnly = true;
            this.txtGain.Size = new System.Drawing.Size(41, 20);
            this.txtGain.TabIndex = 59;
            // 
            // txtBacklight
            // 
            this.txtBacklight.Location = new System.Drawing.Point(291, 251);
            this.txtBacklight.Name = "txtBacklight";
            this.txtBacklight.ReadOnly = true;
            this.txtBacklight.Size = new System.Drawing.Size(41, 20);
            this.txtBacklight.TabIndex = 58;
            // 
            // txtWhiteBalanceBlue
            // 
            this.txtWhiteBalanceBlue.Location = new System.Drawing.Point(291, 188);
            this.txtWhiteBalanceBlue.Name = "txtWhiteBalanceBlue";
            this.txtWhiteBalanceBlue.ReadOnly = true;
            this.txtWhiteBalanceBlue.Size = new System.Drawing.Size(41, 20);
            this.txtWhiteBalanceBlue.TabIndex = 57;
            this.txtWhiteBalanceBlue.Leave += new System.EventHandler(this.txtWhiteBalanceBlue_Leave);
            // 
            // txtGamma
            // 
            this.txtGamma.Location = new System.Drawing.Point(291, 156);
            this.txtGamma.Name = "txtGamma";
            this.txtGamma.ReadOnly = true;
            this.txtGamma.Size = new System.Drawing.Size(41, 20);
            this.txtGamma.TabIndex = 56;
            // 
            // txtSharpness
            // 
            this.txtSharpness.Location = new System.Drawing.Point(291, 124);
            this.txtSharpness.Name = "txtSharpness";
            this.txtSharpness.ReadOnly = true;
            this.txtSharpness.Size = new System.Drawing.Size(41, 20);
            this.txtSharpness.TabIndex = 55;
            // 
            // txtSaturation
            // 
            this.txtSaturation.Location = new System.Drawing.Point(291, 91);
            this.txtSaturation.Name = "txtSaturation";
            this.txtSaturation.ReadOnly = true;
            this.txtSaturation.Size = new System.Drawing.Size(41, 20);
            this.txtSaturation.TabIndex = 54;
            // 
            // txtContrast
            // 
            this.txtContrast.Location = new System.Drawing.Point(291, 58);
            this.txtContrast.Name = "txtContrast";
            this.txtContrast.ReadOnly = true;
            this.txtContrast.Size = new System.Drawing.Size(41, 20);
            this.txtContrast.TabIndex = 53;
            // 
            // txtBrightness
            // 
            this.txtBrightness.Location = new System.Drawing.Point(291, 26);
            this.txtBrightness.Name = "txtBrightness";
            this.txtBrightness.ReadOnly = true;
            this.txtBrightness.Size = new System.Drawing.Size(41, 20);
            this.txtBrightness.TabIndex = 52;
            // 
            // trkWhiteBalanceBlue
            // 
            this.trkWhiteBalanceBlue.Location = new System.Drawing.Point(113, 188);
            this.trkWhiteBalanceBlue.Margin = new System.Windows.Forms.Padding(2);
            this.trkWhiteBalanceBlue.Name = "trkWhiteBalanceBlue";
            this.trkWhiteBalanceBlue.Size = new System.Drawing.Size(167, 45);
            this.trkWhiteBalanceBlue.TabIndex = 44;
            this.trkWhiteBalanceBlue.Scroll += new System.EventHandler(this.trkWhiteBalance_Scroll);
            // 
            // trkGamma
            // 
            this.trkGamma.Location = new System.Drawing.Point(113, 156);
            this.trkGamma.Margin = new System.Windows.Forms.Padding(2);
            this.trkGamma.Name = "trkGamma";
            this.trkGamma.Size = new System.Drawing.Size(167, 45);
            this.trkGamma.TabIndex = 45;
            this.trkGamma.Scroll += new System.EventHandler(this.trkGamma_Scroll);
            // 
            // trkSharpness
            // 
            this.trkSharpness.Location = new System.Drawing.Point(113, 124);
            this.trkSharpness.Margin = new System.Windows.Forms.Padding(2);
            this.trkSharpness.Name = "trkSharpness";
            this.trkSharpness.Size = new System.Drawing.Size(167, 45);
            this.trkSharpness.TabIndex = 41;
            this.trkSharpness.Scroll += new System.EventHandler(this.trkSharpness_Scroll);
            // 
            // cboImageFormat
            // 
            this.cboImageFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboImageFormat.FormattingEnabled = true;
            this.cboImageFormat.Items.AddRange(new object[] {
            "BMP",
            "JPEG",
            "RAW"});
            this.cboImageFormat.Location = new System.Drawing.Point(548, 116);
            this.cboImageFormat.Margin = new System.Windows.Forms.Padding(2);
            this.cboImageFormat.Name = "cboImageFormat";
            this.cboImageFormat.Size = new System.Drawing.Size(130, 21);
            this.cboImageFormat.TabIndex = 51;
            this.cboImageFormat.SelectedIndexChanged += new System.EventHandler(this.cboImageFormat_SelectedIndexChanged);
            // 
            // cboImageResolution
            // 
            this.cboImageResolution.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboImageResolution.FormattingEnabled = true;
            this.cboImageResolution.Location = new System.Drawing.Point(548, 85);
            this.cboImageResolution.Margin = new System.Windows.Forms.Padding(2);
            this.cboImageResolution.Name = "cboImageResolution";
            this.cboImageResolution.Size = new System.Drawing.Size(130, 21);
            this.cboImageResolution.TabIndex = 50;
            this.cboImageResolution.SelectedIndexChanged += new System.EventHandler(this.cboImageResolution_SelectedIndexChanged);
            // 
            // cboIlluminationMode
            // 
            this.cboIlluminationMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboIlluminationMode.FormattingEnabled = true;
            this.cboIlluminationMode.Items.AddRange(new object[] {
            "0=Standard",
            "1=On",
            "2=Off",
            "3=TBD"});
            this.cboIlluminationMode.Location = new System.Drawing.Point(548, 54);
            this.cboIlluminationMode.Margin = new System.Windows.Forms.Padding(2);
            this.cboIlluminationMode.Name = "cboIlluminationMode";
            this.cboIlluminationMode.Size = new System.Drawing.Size(130, 21);
            this.cboIlluminationMode.TabIndex = 49;
            this.cboIlluminationMode.SelectedIndexChanged += new System.EventHandler(this.cboIlluminationMode_SelectedIndexChanged);
            // 
            // cboVideoMode
            // 
            this.cboVideoMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboVideoMode.FormattingEnabled = true;
            this.cboVideoMode.Items.AddRange(new object[] {
            "0=Off",
            "1=Device wakeup",
            "2=Continuous"});
            this.cboVideoMode.Location = new System.Drawing.Point(548, 23);
            this.cboVideoMode.Margin = new System.Windows.Forms.Padding(2);
            this.cboVideoMode.Name = "cboVideoMode";
            this.cboVideoMode.Size = new System.Drawing.Size(130, 21);
            this.cboVideoMode.TabIndex = 48;
            this.cboVideoMode.SelectedIndexChanged += new System.EventHandler(this.cboVideoMode_SelectedIndexChanged);
            // 
            // trkSaturation
            // 
            this.trkSaturation.Location = new System.Drawing.Point(113, 91);
            this.trkSaturation.Margin = new System.Windows.Forms.Padding(2);
            this.trkSaturation.Name = "trkSaturation";
            this.trkSaturation.Size = new System.Drawing.Size(167, 45);
            this.trkSaturation.TabIndex = 42;
            this.trkSaturation.Scroll += new System.EventHandler(this.trkSaturation_Scroll);
            // 
            // trkContrast
            // 
            this.trkContrast.Location = new System.Drawing.Point(113, 58);
            this.trkContrast.Margin = new System.Windows.Forms.Padding(2);
            this.trkContrast.Name = "trkContrast";
            this.trkContrast.Size = new System.Drawing.Size(167, 45);
            this.trkContrast.TabIndex = 40;
            this.trkContrast.Scroll += new System.EventHandler(this.trkContrast_Scroll);
            // 
            // trkBrightness
            // 
            this.trkBrightness.Location = new System.Drawing.Point(113, 26);
            this.trkBrightness.Margin = new System.Windows.Forms.Padding(2);
            this.trkBrightness.Name = "trkBrightness";
            this.trkBrightness.Size = new System.Drawing.Size(167, 45);
            this.trkBrightness.TabIndex = 39;
            this.trkBrightness.Scroll += new System.EventHandler(this.trkBrightness_Scroll);
            // 
            // chkDecodeImageEvent
            // 
            this.chkDecodeImageEvent.AutoSize = true;
            this.chkDecodeImageEvent.Enabled = false;
            this.chkDecodeImageEvent.Location = new System.Drawing.Point(548, 240);
            this.chkDecodeImageEvent.Margin = new System.Windows.Forms.Padding(2);
            this.chkDecodeImageEvent.Name = "chkDecodeImageEvent";
            this.chkDecodeImageEvent.Size = new System.Drawing.Size(127, 17);
            this.chkDecodeImageEvent.TabIndex = 38;
            this.chkDecodeImageEvent.Text = "Decode Image Event";
            this.chkDecodeImageEvent.UseVisualStyleBackColor = true;
            this.chkDecodeImageEvent.CheckedChanged += new System.EventHandler(this.chkDecodeImageEvent_CheckedChanged);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(446, 119);
            this.label21.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(71, 13);
            this.label21.TabIndex = 37;
            this.label21.Text = "Image Format";
            // 
            // lbl
            // 
            this.lbl.AutoSize = true;
            this.lbl.Location = new System.Drawing.Point(446, 88);
            this.lbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl.Name = "lbl";
            this.lbl.Size = new System.Drawing.Size(89, 13);
            this.lbl.TabIndex = 36;
            this.lbl.Text = "Image Resolution";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(446, 57);
            this.label19.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(89, 13);
            this.label19.TabIndex = 35;
            this.label19.Text = "Illumination Mode";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(446, 27);
            this.label18.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(64, 13);
            this.label18.TabIndex = 34;
            this.label18.Text = "Video Mode";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(10, 319);
            this.label17.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(51, 13);
            this.label17.TabIndex = 33;
            this.label17.Text = "Exposure";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(10, 287);
            this.label16.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(29, 13);
            this.label16.TabIndex = 32;
            this.label16.Text = "Gain";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(10, 254);
            this.label15.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(81, 13);
            this.label15.TabIndex = 31;
            this.label15.Text = "Backlight Comp";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(10, 192);
            this.label14.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(107, 13);
            this.label14.TabIndex = 30;
            this.label14.Text = "White Balance (Blue)";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(10, 159);
            this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(43, 13);
            this.label13.TabIndex = 29;
            this.label13.Text = "Gamma";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(10, 127);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(57, 13);
            this.label12.TabIndex = 28;
            this.label12.Text = "Sharpness";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(10, 94);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(55, 13);
            this.label11.TabIndex = 27;
            this.label11.Text = "Saturation";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(10, 62);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(46, 13);
            this.label10.TabIndex = 26;
            this.label10.Text = "Contrast";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(10, 29);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(56, 13);
            this.label9.TabIndex = 25;
            this.label9.Text = "Brightness";
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.btnCancelFWDownload);
            this.groupBox7.Controls.Add(this.progressFWDownload);
            this.groupBox7.Controls.Add(this.btnLaunch);
            this.groupBox7.Controls.Add(this.btnFirmwareUpdate);
            this.groupBox7.Controls.Add(this.btnBrowse);
            this.groupBox7.Controls.Add(this.txtFwFile);
            this.groupBox7.Controls.Add(this.label20);
            this.groupBox7.Location = new System.Drawing.Point(8, 280);
            this.groupBox7.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox7.Size = new System.Drawing.Size(472, 96);
            this.groupBox7.TabIndex = 25;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Firmware Update";
            // 
            // btnCancelFWDownload
            // 
            this.btnCancelFWDownload.Location = new System.Drawing.Point(282, 52);
            this.btnCancelFWDownload.Name = "btnCancelFWDownload";
            this.btnCancelFWDownload.Size = new System.Drawing.Size(90, 26);
            this.btnCancelFWDownload.TabIndex = 18;
            this.btnCancelFWDownload.Text = "Cancel";
            this.btnCancelFWDownload.UseVisualStyleBackColor = true;
            this.btnCancelFWDownload.Click += new System.EventHandler(this.btnCncelFWDownload_Click);
            // 
            // progressFWDownload
            // 
            this.progressFWDownload.Location = new System.Drawing.Point(7, 65);
            this.progressFWDownload.Name = "progressFWDownload";
            this.progressFWDownload.Size = new System.Drawing.Size(263, 10);
            this.progressFWDownload.TabIndex = 17;
            // 
            // btnLaunch
            // 
            this.btnLaunch.Enabled = false;
            this.btnLaunch.Location = new System.Drawing.Point(378, 52);
            this.btnLaunch.Margin = new System.Windows.Forms.Padding(2);
            this.btnLaunch.Name = "btnLaunch";
            this.btnLaunch.Size = new System.Drawing.Size(90, 26);
            this.btnLaunch.TabIndex = 16;
            this.btnLaunch.Text = "Launch";
            this.btnLaunch.UseVisualStyleBackColor = true;
            this.btnLaunch.Click += new System.EventHandler(this.btnLaunch_Click);
            // 
            // btnFirmwareUpdate
            // 
            this.btnFirmwareUpdate.Enabled = false;
            this.btnFirmwareUpdate.Location = new System.Drawing.Point(378, 20);
            this.btnFirmwareUpdate.Margin = new System.Windows.Forms.Padding(2);
            this.btnFirmwareUpdate.Name = "btnFirmwareUpdate";
            this.btnFirmwareUpdate.Size = new System.Drawing.Size(90, 26);
            this.btnFirmwareUpdate.TabIndex = 15;
            this.btnFirmwareUpdate.Text = "Update";
            this.btnFirmwareUpdate.UseVisualStyleBackColor = true;
            this.btnFirmwareUpdate.Click += new System.EventHandler(this.btnFirmwareUpdate_Click);
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(282, 20);
            this.btnBrowse.Margin = new System.Windows.Forms.Padding(2);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(90, 26);
            this.btnBrowse.TabIndex = 14;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // txtFwFile
            // 
            this.txtFwFile.Enabled = false;
            this.txtFwFile.Location = new System.Drawing.Point(78, 24);
            this.txtFwFile.Margin = new System.Windows.Forms.Padding(2);
            this.txtFwFile.Name = "txtFwFile";
            this.txtFwFile.ReadOnly = true;
            this.txtFwFile.Size = new System.Drawing.Size(193, 20);
            this.txtFwFile.TabIndex = 13;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(4, 26);
            this.label20.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(67, 13);
            this.label20.TabIndex = 12;
            this.label20.Text = "File Location";
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.chkAutoSave);
            this.groupBox8.Controls.Add(this.label27);
            this.groupBox8.Controls.Add(this.btnSave);
            this.groupBox8.Controls.Add(this.btnBrowsImageSaveLocation);
            this.groupBox8.Controls.Add(this.txtImageSaveLocation);
            this.groupBox8.Location = new System.Drawing.Point(8, 386);
            this.groupBox8.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox8.Size = new System.Drawing.Size(472, 83);
            this.groupBox8.TabIndex = 26;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Save Image";
            // 
            // chkAutoSave
            // 
            this.chkAutoSave.AutoSize = true;
            this.chkAutoSave.Location = new System.Drawing.Point(5, 58);
            this.chkAutoSave.Name = "chkAutoSave";
            this.chkAutoSave.Size = new System.Drawing.Size(163, 17);
            this.chkAutoSave.TabIndex = 16;
            this.chkAutoSave.Text = "Auto save event image to file";
            this.chkAutoSave.UseVisualStyleBackColor = true;
            this.chkAutoSave.CheckedChanged += new System.EventHandler(this.chkAutoSave_CheckedChanged);
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(4, 26);
            this.label27.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(48, 13);
            this.label27.TabIndex = 12;
            this.label27.Text = "Location";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(374, 50);
            this.btnSave.Margin = new System.Windows.Forms.Padding(2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(90, 26);
            this.btnSave.TabIndex = 15;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnBrowsImageSaveLocation
            // 
            this.btnBrowsImageSaveLocation.Location = new System.Drawing.Point(374, 17);
            this.btnBrowsImageSaveLocation.Margin = new System.Windows.Forms.Padding(2);
            this.btnBrowsImageSaveLocation.Name = "btnBrowsImageSaveLocation";
            this.btnBrowsImageSaveLocation.Size = new System.Drawing.Size(90, 26);
            this.btnBrowsImageSaveLocation.TabIndex = 14;
            this.btnBrowsImageSaveLocation.Text = "Browse";
            this.btnBrowsImageSaveLocation.UseVisualStyleBackColor = true;
            this.btnBrowsImageSaveLocation.Click += new System.EventHandler(this.btnBrowsImageSaveLocation_Click);
            // 
            // txtImageSaveLocation
            // 
            this.txtImageSaveLocation.Location = new System.Drawing.Point(78, 20);
            this.txtImageSaveLocation.Margin = new System.Windows.Forms.Padding(2);
            this.txtImageSaveLocation.Name = "txtImageSaveLocation";
            this.txtImageSaveLocation.ReadOnly = true;
            this.txtImageSaveLocation.Size = new System.Drawing.Size(292, 20);
            this.txtImageSaveLocation.TabIndex = 13;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtStatus);
            this.groupBox2.Controls.Add(this.btnClear);
            this.groupBox2.Location = new System.Drawing.Point(8, 484);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(472, 292);
            this.groupBox2.TabIndex = 27;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Event Log";
            // 
            // txtCameraStream
            // 
            this.txtCameraStream.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.txtCameraStream.Enabled = false;
            this.txtCameraStream.Location = new System.Drawing.Point(548, 230);
            this.txtCameraStream.Name = "txtCameraStream";
            this.txtCameraStream.Size = new System.Drawing.Size(130, 20);
            this.txtCameraStream.TabIndex = 42;
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Location = new System.Drawing.Point(446, 234);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(79, 13);
            this.label35.TabIndex = 43;
            this.label35.Text = "Camera Stream";
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1196, 781);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox8);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bioptic Color Camera Sample App (C#)";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picImage)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDeviceAwake)).EndInit();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trkExposure)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkGain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkBacklight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkWhiteBalanceRed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkWhiteBalanceBlue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkGamma)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkSharpness)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkSaturation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkContrast)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkBrightness)).EndInit();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox picImage;
        private System.Windows.Forms.TextBox txtStatus;
        private System.Windows.Forms.Button btnSetDefaults;
        private System.Windows.Forms.Button btnReboot;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnWriteToFlash;
        private System.Windows.Forms.CheckBox chkSnapshotImageEvent;
        private System.Windows.Forms.CheckBox chkProduceImageEvent;
        private System.Windows.Forms.ComboBox cboDevices;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.TextBox txtHardwareVersion;
        private System.Windows.Forms.TextBox txtFirmwareVersion;
        private System.Windows.Forms.TextBox txtDateOfFirstProgram;
        private System.Windows.Forms.TextBox txtDateOfManufacture;
        private System.Windows.Forms.TextBox txtDateOfService;
        private System.Windows.Forms.TextBox txtModelNumber;
        private System.Windows.Forms.TextBox txtSerialNumber;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label lbl;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.CheckBox chkDecodeImageEvent;
        private System.Windows.Forms.TrackBar trkGamma;
        private System.Windows.Forms.TrackBar trkWhiteBalanceBlue;
        private System.Windows.Forms.TrackBar trkSaturation;
        private System.Windows.Forms.TrackBar trkSharpness;
        private System.Windows.Forms.TrackBar trkContrast;
        private System.Windows.Forms.TrackBar trkBrightness;
        private System.Windows.Forms.TrackBar trkGain;
        private System.Windows.Forms.TrackBar trkBacklight;
        private System.Windows.Forms.TrackBar trkExposure;
        private System.Windows.Forms.ComboBox cboImageResolution;
        private System.Windows.Forms.ComboBox cboIlluminationMode;
        private System.Windows.Forms.ComboBox cboVideoMode;
        private System.Windows.Forms.ComboBox cboImageFormat;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Button btnLaunch;
        private System.Windows.Forms.Button btnFirmwareUpdate;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.TextBox txtFwFile;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox txtImageType;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox txtImageFormat;
        private System.Windows.Forms.TextBox txtImageRes;
        private System.Windows.Forms.TextBox txtTimestamp;
        private System.Windows.Forms.TextBox txtImageSize;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnBrowsImageSaveLocation;
        private System.Windows.Forms.TextBox txtImageSaveLocation;
        private System.Windows.Forms.CheckBox chkAutoSave;
        private System.Windows.Forms.TextBox txtBrightness;
        private System.Windows.Forms.TextBox txtContrast;
        private System.Windows.Forms.TextBox txtExposure;
        private System.Windows.Forms.TextBox txtGain;
        private System.Windows.Forms.TextBox txtBacklight;
        private System.Windows.Forms.TextBox txtWhiteBalanceBlue;
        private System.Windows.Forms.TextBox txtGamma;
        private System.Windows.Forms.TextBox txtSharpness;
        private System.Windows.Forms.TextBox txtSaturation;
        private System.Windows.Forms.CheckBox chkAutoExposure;
        private System.Windows.Forms.CheckBox chkAutoWhiteBalanceBlue;
        private System.Windows.Forms.Button btnSnapshot;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox chkContImageEvent;
        private System.Windows.Forms.TextBox txtWhiteBalanceRed;
        private System.Windows.Forms.TrackBar trkWhiteBalanceRed;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.TextBox txtDecodeData;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.ProgressBar progressFWDownload;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.ComboBox cbPowerUserMode;
        private System.Windows.Forms.Button btnCancelFWDownload;
        private System.Windows.Forms.Button btnWBCSet;
        private System.Windows.Forms.Button btnWBCGet;
        private System.Windows.Forms.TextBox txtWBCRed;
        private System.Windows.Forms.TextBox txtWBCBlue;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.CheckBox chkAutoWhiteBalanceRed;
        private System.Windows.Forms.CheckBox chkAutoGain;
        private System.Windows.Forms.CheckBox chkAutoBacklight;
        private System.Windows.Forms.CheckBox chkAutoGamma;
        private System.Windows.Forms.CheckBox chkAutoSharpness;
        private System.Windows.Forms.CheckBox chkAutoSaturation;
        private System.Windows.Forms.CheckBox chkAutoContrast;
        private System.Windows.Forms.CheckBox chkAutoBrightness;
        private System.Windows.Forms.Button btnRetieveConfig;
        private System.Windows.Forms.Button btnLoadConfig;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.CheckBox chkDecodeSessionStatusEvent;
        private System.Windows.Forms.PictureBox pictureBoxDeviceAwake;
        private System.Windows.Forms.CheckBox chkDetectBoundingBox;
        private System.Windows.Forms.Button btnSetBackground;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.TextBox txtWeightData;
        private System.Windows.Forms.CheckBox chkCameraStreamStatusEvent;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.TextBox txtCameraStream;
    }
}

