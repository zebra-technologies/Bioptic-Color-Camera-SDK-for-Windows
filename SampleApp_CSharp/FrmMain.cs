using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Linq;
using BiopticColorCamera;
using System.Text;
using System.Runtime.InteropServices;
using System.Threading;
using System.Reflection;

namespace CameraSDKSampleApp
{
    public partial class FrmMain : Form
    {
        const string ImageNamePrefix = "image_";
        const string ImageExtensionBmp = "bmp";
        const string ImageExtensionJpg = "jpeg";
        const string ImageExtensionRaw = "raw";
        const int AsciiValueOfSpace = 0x20;
        const int BytesForKB = 1024;

        const int ImageTypeBmp = 0;
        const int ImageTypeJpeg = 1;
        const int ImageTypeRaw = 2;

        const float BoundingBoxLIneWidth = 5;

        const int IlluminationStandard = 0;
        const int IlluminationOn = 1;
        const int IlluminationOff = 2;

        const int LengthOfImageFormatSignatutre = 2;
        readonly byte[] jpgSignature = { 0xff, 0xd8 };
        readonly byte[] bmpSignature = { 0x42, 0x4d };

        private const int WhiteBalanceComponentMax = 0x4000;
        private const int DelayBeforeRetryOnFailure = 100;

        private byte[] continuousImage = null;
        private byte[] snapshotImage = null;
        private byte[] produceImage = null;
        private byte[] decodeImage = null;
        private const int True = 1;
        private const int False = 0;
        MemoryStream msImage = new MemoryStream();
        public delegate void LogMessage(string msg);
        public event LogMessage Log;
        List<string> deviceList = new List<string>();
        private Camera camera;
        private FrmMain curForm = null;
        private string picFoldereName = "ZebraCamera";
        private bool autoSave = false;
        string errorMsgTitle = "ERROR";

        bool isOpened = false;
        string openedDeviceID;
        int currentlySelectedDeviceIndex = -1;

        private readonly object imageEventLock = new object();

        const int WmSysCommand = 0x112;
        const int MfByPosition = 0x400;
        const int MfSeperator = 0x800;
        const int AboutMenu = 2000;

        const int DecodeSessionStart = 0;
        const int DecodeSessionEnd = 1;

        // Bounding box detection background type settings
        const int backgroundStatic = 0;
        const int backgroundDynamic = 1;
        const int backgroundDynamicGaussian = 2;
        const int backgroundMixed = 3;

        private bool detectBoundingBox = false;
        private bool saveBackground = true;

        Bitmap deviceAwakeImage;
        Bitmap deviceSleepImage;

        const string deviceAwakeResourceStreamPath = "CameraSDKSampleApp.Resources.deviceAwake.jpg";
        const string deviceSleepResourceStreamPath = "CameraSDKSampleApp.Resources.deviceSleep.jpg";

        private int registeredImageEventCount = 0;

        protected override void WndProc(ref Message msg)
        {
            if (msg.Msg == WmSysCommand)
            {
                switch (msg.WParam.ToInt32())
                {
                    case AboutMenu:
                        var about = new About();
                        about.StartPosition = FormStartPosition.CenterParent;
                        about.ShowDialog(this);
                        return;

                    default:
                        break;
                }
            }
            base.WndProc(ref msg);
        }

        public FrmMain()
        {
            InitializeComponent();
            // Init Background detector
            NativeMethods.InitBoundingBoxDetector(backgroundStatic);
        }


        /// <summary>
        /// Initialize UI controllers.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            //add item to contextmenu
            IntPtr menuHandle = NativeMethods.GetSystemMenu(this.Handle, false);
            NativeMethods.AppendMenu(menuHandle, MfSeperator, AboutMenu, string.Empty);
            NativeMethods.AppendMenu(menuHandle, MfByPosition, AboutMenu, "About Application");

            try
            {
                EnableDisableControls(false);
                this.Log += Form1_OnLog;
                curForm = this;
                picFoldereName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), picFoldereName);
                txtImageSaveLocation.Text = picFoldereName;
                camera = new Camera();
                camera.OnContinuousImageEvent += Camera_ContinuousImageEvent;
                camera.OnSnapshotImageEvent += Camera_OnSnapshotImageEvent;
                camera.OnProduceImageEvent += Camera_OnProduceImageEvent;
                camera.OnDecodeImageEvent += Camera_OnDecodeImageEvent;
                camera.OnDecodeSessionStatusChangeEvent += Camera_OnDecodeSessionStatusChangeEvent;

                Array deviceIDArray;
                camera.EnumerateDevices(out deviceIDArray);
                if(deviceIDArray.Length>0)
                {
                    for(int i=0;i<deviceIDArray.Length;i++)
                    {
                        deviceList.Add((string)deviceIDArray.GetValue(i));
                    }
                    UpdateDeviceListUI();
                    btnOpen.Enabled = true;
                    btnClose.Enabled = false;
                    EnableDisableControls(false);
                }

                camera.OnDeviceAddedEvent += Camera_OnDeviceAddedEvent;
                camera.OnDeviceRemovedEvent += Camera_OnDeviceRemovedEvent;

                camera.OnFirmwareDownloadProgressEvent += Camera_OnFirmwareDownloadProgressEvent;

                Assembly assembly = Assembly.GetExecutingAssembly();

                Stream resourceStream = assembly.GetManifestResourceStream(deviceAwakeResourceStreamPath);
                deviceAwakeImage = new Bitmap(resourceStream);

                resourceStream = assembly.GetManifestResourceStream(deviceSleepResourceStreamPath);
                deviceSleepImage = new Bitmap(resourceStream);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex + " \nLast Error: " + camera.LastError, errorMsgTitle);
            }
        }

        /// <summary>
        /// Handles firmware download progress events.
        /// </summary>
        /// <param name="serial">Device serial number.</param>
        /// <param name="model">Device model_number.</param>
        /// <param name="totalRecords">Total number of records.</param>
        /// <param name="currentRecord">Current record this event is generated for.</param>
        /// <param name="operationCode">Operation code. 0 - SessionStart, 1 - DownloadStart, 2 - Progress, 3 - DownloadEnd, 4 - SessionStop, 5 - Error</param>
        /// <param name="status">Status. 0 - Successful, 1 - FailiedInDevice, 2 - Canceled. </param>
        private void Camera_OnFirmwareDownloadProgressEvent(string serial, string model, int totalRecords, int currentRecord, int operationCode, int status)
        {
            Log(serial + " : " + model + " : " + totalRecords + " : " + currentRecord + " : " + operationCode + " : " + status);
            switch (operationCode)
            {
                case 0:
                    progressFWDownload.Invoke((MethodInvoker)delegate
                    {
                        progressFWDownload.Minimum = 0;
                        progressFWDownload.Maximum = totalRecords;
                    });
                    Log("Session start.");
                    break;
                case 1:
                    Log("Download start.");
                    break;
                case 2:
                    progressFWDownload.Invoke((MethodInvoker)delegate
                    {
                        progressFWDownload.Value = currentRecord;
                    });
                    break;
                case 3:
                    Log("Download end.");
                    break;
                case 4:
                    progressFWDownload.Invoke((MethodInvoker)delegate
                    {
                        progressFWDownload.Value = 0;
                    });
                    Log("Session stopped.");
                    break;
                case 5:
                    Log("Download error.");
                    break;
                default:
                    break;
            }

        }

        /// <summary>
        /// Handled DeviceAdded event 
        /// </summary>
        /// <param name="deviceID">Device id of the camera.</param>
        private void Camera_OnDeviceAddedEvent(string deviceID)
        {
            if (!deviceList.Contains(deviceID))
            {
                deviceList.Add(deviceID);
                curForm.Invoke((MethodInvoker)delegate
               {
                   UpdateDeviceListUI();
                   if (!isOpened)
                   {
                       btnOpen.Enabled = true;
                       btnClose.Enabled = false;
                       EnableDisableControls(false);
                   }
               });
            }
        }

        /// <summary>
        /// Handles DeviceRemove event.
        /// </summary>
        /// <param name="deviceID">Device id of the camera.</param>
        private void Camera_OnDeviceRemovedEvent(string deviceID)
        {
            if (deviceList.Contains(deviceID))
            {
                deviceList.Remove(deviceID);

                curForm.Invoke((MethodInvoker)delegate
               {
                   if (deviceID.Equals(openedDeviceID) && isOpened)
                   {
                       btnClose_Click(null, null);
                   }
                   UpdateDeviceListUI();
                   if (!isOpened && deviceList.Count == 0)
                   {
                       btnOpen.Enabled = false;
                       btnClose.Enabled = false;
                       EnableDisableControls(false);
                   }
               });
            }
        }

        /// <summary>
        /// Open the selected devices and populate UI controllers.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpen_Click(object sender, EventArgs e)
        {
            try
            {
                isOpened = true;
                Log("Open()");
                camera.Open(cboDevices.Text);
                openedDeviceID = cboDevices.Text;
                currentlySelectedDeviceIndex = cboDevices.SelectedIndex;
                btnClose.Enabled = true;
                btnOpen.Enabled = false;
                EnableDisableControls(true);
                GetDeviceInfo();
                camera.RegisterForContinuousImageEvent(False);
                camera.RegisterForSnapshotImageEvent(False);
                camera.RegisterForProduceImageEvent(False);
                camera.RegisterForFirmwareDownloadProgressEvents(True);
            }
            catch (Exception ex)
            {
                isOpened = false;
                openedDeviceID = "";
                MessageBox.Show("Error : " + ex + " \nLast Error: " + camera.LastError, errorMsgTitle);
            }

        }

        /// <summary>
        /// Close the currently active device.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {

            try
            {
                Log("Close()");
                isOpened = false;
                openedDeviceID = "";
                camera.Close();
                btnClose.Enabled = false;
                btnOpen.Enabled = true;
                EnableDisableControls(false);
                registeredImageEventCount = 0;
            }
            catch (Exception ex)
            {
                openedDeviceID = "";
                MessageBox.Show("Error : " + ex + " \nLast Error: " + camera.LastError, errorMsgTitle);
            }
        }

        private void UpdateDeviceListUI()
        {
            Log("----------------------------------------");
            cboDevices.Items.Clear();
            foreach (string dev in deviceList)
            {
                cboDevices.Items.Add(dev);
                Log("Found camera device : " + dev);
            }

            if (cboDevices.Items.Count > 0)
            {
                cboDevices.SelectedIndex = 0;
                currentlySelectedDeviceIndex = 0;
            }
            else
            {
                cboDevices.Text = "";
            }

            Log("----------------------------------------");
        }


        private void Camera_ContinuousImageEvent(ref object imageBuffer, int length, int width, int height, int imageType)
        {
            Camera_OnImageEvent(ref continuousImage, imageBuffer, length, width, height, imageType, "Continuous Image");
            imageBuffer = null;
        }

        private void Camera_OnSnapshotImageEvent(ref object imageBuffer, int length, int width, int height, int imageType)
        {
            Camera_OnImageEvent(ref snapshotImage, imageBuffer, length, width, height, imageType, "Snapshot Image");
            imageBuffer = null;
        }

        private void Camera_OnProduceImageEvent(ref object imageBuffer, int length, int width, int height, int imageType)
        {
            Camera_OnImageEvent(ref produceImage, imageBuffer, length, width, height, imageType, "Produce Image");
            imageBuffer = null;
        }

        private void Camera_OnDecodeImageEvent(ref object imageBuffer, int length, int width, int height, int imageType, string decodeData)
        {
            Camera_OnImageEvent(ref decodeImage, imageBuffer, length, width, height, imageType, "Decode Image", decodeData);
            imageBuffer = null;
        }

        private void Camera_OnDecodeSessionStatusChangeEvent(int session_status)
        {
          
            curForm.Invoke((MethodInvoker)delegate
            {
                if (session_status == DecodeSessionStart)
                {
                    pictureBoxDeviceAwake.Image = (Image)deviceAwakeImage;
                }
                else if (session_status == DecodeSessionEnd)
                {
                    pictureBoxDeviceAwake.Image = (Image)deviceSleepImage;
                }
            });
        }

        private void Camera_OnImageEvent(ref byte[] gImage, object imageBuffer, int length, int width, int height, int imageType, string eventType, string decodeData = "")
        {
            try
            {
                lock (imageEventLock)
                {
                    if (gImage == null || gImage.Length < length)
                    {
                        gImage = new byte[length];
                    }

                ((Array)imageBuffer).CopyTo(gImage, 0);
                    msImage.Flush();
                    msImage.Seek(0, SeekOrigin.Begin);
                    msImage.Write(gImage, 0, length);
                    msImage.SetLength(length);
                }
                curForm.BeginInvoke((MethodInvoker)delegate
                {
                    lock (imageEventLock)
                    {
                        if (imageType != ImageTypeRaw)
                        {
                            if (detectBoundingBox)
                            {
                                if (saveBackground)
                                {
                                    byte[] inData = new byte[length];
                                    msImage.Seek(0, SeekOrigin.Begin);
                                    msImage.Read(inData, 0, length);
                                    msImage.Seek(0, SeekOrigin.Begin);
                                    msImage.SetLength(length);
                                    int size = Marshal.SizeOf(inData[0]) * inData.Length;
                                    IntPtr inPtr = Marshal.AllocHGlobal(size);
                                    Marshal.Copy(inData, 0, inPtr, inData.Length);
                                    NativeMethods.SetBackgroundFrame(inPtr, length, width, height, imageType);
                                    saveBackground = false;
                                    picImage.Image = Image.FromStream(msImage);
                                }
                                else
                                {
                                    byte[] inData = new byte[length];
                                    msImage.Seek(0, SeekOrigin.Begin);
                                    msImage.Read(inData, 0, length);
                                    msImage.Seek(0, SeekOrigin.Begin);
                                    msImage.SetLength(length);
                                    int size = Marshal.SizeOf(inData[0]) * inData.Length;
                                    IntPtr inPtr = Marshal.AllocHGlobal(size);
                                    Marshal.Copy(inData, 0, inPtr, inData.Length);
                                    int x1 = 0;
                                    int y1 = 0;
                                    int x2 = 0;
                                    int y2 = 0;
                                    Image img = Image.FromStream(msImage);
                                    if (NativeMethods.DetectBoundingBox(inPtr, length, width, height, imageType, ref x1,ref y1, ref x2, ref y2))
                                    {
                                        Graphics g = Graphics.FromImage(img);
                                        g.DrawRectangle(new Pen(Brushes.LightGreen, BoundingBoxLIneWidth), x1, y1, x2 - x1, y2 - y1);
                                    }
                                    picImage.Image = img;
                                    Marshal.FreeHGlobal(inPtr);
                                }
                            }
                            else
                            {
                                picImage.Image = Image.FromStream(msImage);
                            }
                        }
                    }
                    txtTimestamp.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff");
                    txtImageSize.Text = String.Format("{0:0.000}",((double)length / BytesForKB));
                    txtImageFormat.Text = (imageType == 1) ? ImageExtensionJpg : imageType == 0 ? ImageExtensionBmp : ImageExtensionRaw;
                    txtImageType.Text = eventType;
                    txtImageRes.Text = width + "x" + height;

                    StringBuilder decodeDataSB=new StringBuilder();

                    foreach (var item in decodeData)
                    {
                        if (item < AsciiValueOfSpace)
                        {
                            decodeDataSB.Append(' ');
                        }
                        else
                        {
                            decodeDataSB.Append(item);
                        }
                    }
                    txtDecodeData.Text = decodeDataSB.ToString();
                    if (autoSave)
                    {
                        SaveImage(msImage, decodeDataSB.ToString());
                    }
                });
            }
            catch (Exception e1)
            {
                Debug.WriteLine(e1.ToString());
            }
        }

        private void Form1_OnLog(string msg)
        {
            this.Invoke((MethodInvoker)delegate
           {
               txtStatus.Text += msg + Environment.NewLine;
               txtStatus.SelectionStart = txtStatus.TextLength;
               txtStatus.ScrollToCaret();
               Application.DoEvents();
           });
        }

        private void GetDeviceInfo()
        {
            try
            {
                txtSerialNumber.Text = camera.SerialNumber;
                txtModelNumber.Text = camera.ModelNumber;
                txtDateOfFirstProgram.Text = camera.DateOfFirstProgram;
                txtDateOfService.Text = camera.DateOfService;
                txtDateOfManufacture.Text = camera.DateOfManufacture;
                txtFirmwareVersion.Text = camera.FirmwareVersion;
                txtHardwareVersion.Text = camera.HardwareVersion;

                Log("-----------------------------");
                Log("Camera Device Info : " + Environment.NewLine);
                Log("Serial:" + camera.SerialNumber);
                Log("Model:" + camera.ModelNumber);
                Log("Date of First Program:" + camera.DateOfFirstProgram);
                Log("Date of Service:" + camera.DateOfService);
                Log("Date of Manufacture:" + camera.DateOfManufacture);
                Log("Firmware Version:" + camera.FirmwareVersion);
                Log("Hardware Version:" + camera.HardwareVersion);
                UpdateUIWithCameraProperties();
            }
            catch (Exception ex1)
            {
                //On Windows 7 this error handling with delay is required as the device is not in waked up state when doing the uvc extension 
                //property query causing an exception.
                Thread.Sleep(DelayBeforeRetryOnFailure);    
                try
                {
                    GetDeviceInfo();
                }
                catch (Exception ex2)
                {
                    MessageBox.Show("Error : " + ex2, errorMsgTitle);
                }
            }

        }

        /// <summary>
        /// Converts 4.12 fixed point format to double
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        double WBToDouble(int value)
        {
            var val_ = (double)value / (double)(1 << 12);
            return val_;
        }

        /// <summary>
        /// Converts double to 4.12 fixed point format.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        int DoubleToWB(double value)
        {
            var val_ = (int)(value * (1 << 12));
            return val_;
        }
        private void UpdateUIWithCameraProperties()
        {
            //Brightness
            DisplayBrightnessData();

            //Contrast
            DisplayContrastData();

            //Saturation
            DisplaySaturationData();

            //Sharpness
            DisplaySharpnessData();

            //WhiteBalance
            DisplayWhiteBalanceComponentData();

            //Gamma
            DisplayGammaData();

            //Gain
            DisplayGain();

            // BacklightCompensation
            DisplayBacklightCompensation();

            //Exposure Time (Absolute)
            DisplayExposureTimeAbsolute();

            //Extension unit properties
            DisplayExtensioUnitProperties();
        }

        private void DisplayExtensioUnitProperties()
        {
            try
            {
                if (camera == null)
                {
                    return;
                }

                cboImageFormat.SelectedIndex = 0;

                int vidMode = -1;
                camera.GetVideoMode(out vidMode);
                cboVideoMode.SelectedIndex = vidMode;
                Log("Video Mode:" + vidMode);

                int IlluminationMode = -1;
                camera.GetIlluminationMode(out IlluminationMode);
                cboIlluminationMode.SelectedIndex = IlluminationMode;
                Log("Illumination Mode:" + IlluminationMode);

                int PowerUserMode = -1;
                camera.GetPowerUserMode(out PowerUserMode);
                cbPowerUserMode.SelectedIndexChanged -= this.cbPowerUserMode_SelectedIndexChanged;
                Log("Power User Mode:" + IlluminationMode);
                if (PowerUserMode == 0) // power user mode is disabled
                {
                    cbPowerUserMode.SelectedIndex = 1;
                    EnableControlsOnPowerUserMode(true);
                }
                else
                {
                    cbPowerUserMode.SelectedIndex = 0;
                    EnableControlsOnPowerUserMode(false);
                }
                cbPowerUserMode.SelectedIndexChanged += this.cbPowerUserMode_SelectedIndexChanged;

                Array frameTypeInfoArray;
                camera.GetSupportedFrameTypes(out frameTypeInfoArray);

                cboImageResolution.SelectedIndexChanged -= this.cboImageResolution_SelectedIndexChanged;
                cboImageResolution.Items.Clear();
                foreach (var item in frameTypeInfoArray)
                {
                    FrameTypeInfo fti = (FrameTypeInfo)item;
                    cboImageResolution.Items.Add(new FrameTypeComboItem(fti));
                }
                if (cboImageResolution.Items.Count > 0)
                {
                    cboImageResolution.SelectedIndex = 0;
                }
                cboImageResolution.SelectedIndexChanged += this.cboImageResolution_SelectedIndexChanged;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex + " \nLast Error: " + camera.LastError, errorMsgTitle);
                 
            }
        }

        private void DisplayExposureTimeAbsolute()
        {
            int propertyValue = -1;
            int defaultValue = -1;
            int min = 0;
            int max = 0;
            int step = 1;
            int isAutoEnabled = False;
            int isAutoSupported = True;

            try
            {
                camera.GetExposure(out propertyValue, out isAutoEnabled);
                camera.GetExposureInfo(out min, out max, out defaultValue, out step, out isAutoSupported);
                DisplayProperties(trkExposure, txtExposure, chkAutoExposure, propertyValue, isAutoEnabled, min, max, isAutoSupported);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex + " \nLast Error: " + camera.LastError, errorMsgTitle);
                 
            }
        }

        private void DisplayBacklightCompensation()
        {
            int propertyValue = -1;
            int defaultValue = -1;
            int min = 0;
            int max = 0;
            int step = 1;
            int isAutoEnabled = False;
            int isAutoSupported = False;

            try
            {
                camera.GetBacklight(out propertyValue, out isAutoEnabled);
                camera.GetBacklightInfo(out min, out max, out defaultValue, out step, out isAutoSupported);
                DisplayProperties(trkBacklight, txtBacklight, chkAutoBacklight, propertyValue, isAutoEnabled, min, max, isAutoSupported);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex + " \nLast Error: " + camera.LastError, errorMsgTitle);
                 
            }

        }

        private void DisplayGain()
        {
            int propertyValue = -1;
            int defaultValue = -1;
            int min = 0;
            int max = 0;
            int step = 1;
            int isAutoEnabled = False;
            int isAutoSupported = False;

            try
            {
                camera.GetGain(out propertyValue, out isAutoEnabled);
                camera.GetGainInfo(out min, out max, out defaultValue, out step, out isAutoSupported);
                DisplayProperties(trkGain, txtGain, chkAutoGain, propertyValue, isAutoEnabled, min, max, isAutoSupported);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex + " \nLast Error: " + camera.LastError, errorMsgTitle);
            }

        }

        private void DisplayGammaData()
        {
            int propertyValue = -1;
            int defaultValue = -1;
            int min = 0;
            int max = 0;
            int step = 1;
            int isAutoEnabled = False;
            int isAutoSupported = False;
            try
            {
                camera.GetGamma(out propertyValue, out isAutoEnabled);
                camera.GetGammaInfo(out min, out max, out defaultValue, out step, out isAutoSupported);
                DisplayProperties(trkGamma, txtGamma, chkAutoGamma, propertyValue, isAutoEnabled, min, max, isAutoSupported);
                trkGamma.TickFrequency = step;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex + " \nLast Error: " + camera.LastError, errorMsgTitle);
            }

        }

        /// <summary>
        /// Splits 32 bit combined white balance components value to 16 bit white balance component blue and 16 bit white balance component red value.
        /// </summary>
        /// <param name="propertyValue">32bit white balance component property value.</param>
        /// <param name="blueValue">16 bit white balance component blue value.</param>
        /// <param name="redValue">16 bit white balance component red value.</param>
        void GetWhiteBalanceComponentValues(int propertyValue, out int blueValue, out int redValue)
        {
            blueValue = propertyValue >> 16 & 0x0000FFFF;
            redValue = propertyValue & 0x0000FFFF;
        }

        /// <summary>
        /// WhiteBalanceConmponent value is a 32bit integer which contains blue value in its high order 16 bits and red value in its low order 16 bits.
        /// Blue and red values are represented in 4.12 fixed point format.
        /// </summary>
        private void DisplayWhiteBalanceComponentData()
        {
            int propertyValue = -1;
            int defaultValue = -1;
            int min = 0;
            int max = 0;
            int step = 1;
            int isAutoEnabled = False;
            int isAutoSupported = False;
            int blueValue = 0;
            int redValue = 0;

            try
            {
                camera.GetWhiteBalance(out propertyValue, out isAutoEnabled);
                camera.GetWhiteBalanceInfo(out min, out max, out defaultValue, out step, out isAutoSupported);

                GetWhiteBalanceComponentValues(propertyValue, out blueValue, out redValue);

                //Update white balance component blue structure
                trkWhiteBalanceBlue.Enabled = true;
                txtWhiteBalanceBlue.Text = String.Format("{0:0.000}", WBToDouble(blueValue));
                trkWhiteBalanceBlue.Minimum = min;
                trkWhiteBalanceBlue.Maximum = WhiteBalanceComponentMax;
                trkWhiteBalanceBlue.Value = blueValue;

                if (isAutoSupported == True)
                {
                    chkAutoWhiteBalanceBlue.Visible = true;
                    if (isAutoEnabled == True)
                    {
                        chkAutoWhiteBalanceBlue.Checked = true;
                        trkWhiteBalanceBlue.Enabled = false;
                        trkWhiteBalanceRed.Enabled = false;
                    }
                    else
                    {
                        chkAutoWhiteBalanceBlue.Checked = false;
                        trkWhiteBalanceBlue.Enabled = true;
                        trkWhiteBalanceRed.Enabled = true;
                    }
                }
                else
                {
                    chkAutoWhiteBalanceBlue.Visible = false;
                }

                //Update white balance component red structure
                txtWhiteBalanceRed.Text = String.Format("{0:0.000}", WBToDouble(redValue));
                trkWhiteBalanceRed.Minimum = min;
                trkWhiteBalanceRed.Maximum = WhiteBalanceComponentMax;
                trkWhiteBalanceRed.Value = redValue;
                chkAutoWhiteBalanceRed.Visible = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex + " \nLast Error: " + camera.LastError, errorMsgTitle);
            }
        }

        private void DisplaySharpnessData()
        {
            int propertyValue = -1;
            int defaultValue = -1;
            int min = 0;
            int max = 0;
            int step = 1;
            int isAutoEnabled = False;
            int isAutoSupported = False;

            try
            {
                camera.GetSharpness(out propertyValue, out isAutoEnabled);
                camera.GetSharpnessInfo(out min, out max, out defaultValue, out step, out isAutoSupported);
                DisplayProperties(trkSharpness, txtSharpness, chkAutoSharpness, propertyValue, isAutoEnabled, min, max, isAutoSupported);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex + " \nLast Error: " + camera.LastError, errorMsgTitle);
            }

        }

        private void DisplaySaturationData()
        {
            int propertyValue = -1;
            int defaultValue = -1;
            int min = 0;
            int max = 0;
            int step = 1;
            int isAutoEnabled = False;
            int isAutoSupported = False;
            try
            {
                camera.GetSaturation(out propertyValue, out isAutoEnabled);
                camera.GetSaturationInfo(out min, out max, out defaultValue, out step, out isAutoSupported);
                DisplayProperties(trkSaturation, txtSaturation, chkAutoSaturation, propertyValue, isAutoEnabled, min, max, isAutoSupported);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex + " \nLast Error: " + camera.LastError, errorMsgTitle);
            }

        }

        private void DisplayContrastData()
        {
            int propertyValue = -1;
            int defaultValue = -1;
            int min = 0;
            int max = 0;
            int step = 1;
            int isAutoEnabled = False;
            int isAutoSupported = False;

            try
            {
                camera.GetContrast(out propertyValue, out isAutoEnabled);
                camera.GetContrastInfo(out min, out max, out defaultValue, out step, out isAutoSupported);
                DisplayProperties(trkContrast, txtContrast, chkAutoContrast, propertyValue, isAutoEnabled, min, max, isAutoSupported);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex + " \nLast Error: " + camera.LastError, errorMsgTitle);
            }

        }

        private void DisplayBrightnessData()
        {
            int propertyValue = -1;
            int defaultValue = -1;
            int min = 0;
            int max = 0;
            int step = 1;
            int isAutoEnabled = False;
            int isAutoSupported = False;
            try
            {
                camera.GetBrightness(out propertyValue, out isAutoEnabled);
                camera.GetBrightnessInfo(out min, out max, out defaultValue, out step, out isAutoSupported);
                DisplayProperties(trkBrightness, txtBrightness, chkAutoBrightness, propertyValue, isAutoEnabled, min, max, isAutoSupported);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex + " \nLast Error: " + camera.LastError, errorMsgTitle);
            }

        }

        struct FrameTypeComboItem
        {
            private FrameTypeInfo frameTypeInfo;

            public FrameTypeComboItem(FrameTypeInfo frameTypeInfo)
            {
                this.frameTypeInfo = frameTypeInfo;
            }

            public override string ToString()
            {
                return GetFrameRateString(frameTypeInfo);
            }

            public FrameTypeInfo FrameTypeInfo
            {
                get
                {
                    return frameTypeInfo;
                }
            }

            private string GetFrameRateString(FrameTypeInfo frameTypeInfo)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(frameTypeInfo.format == 1 ? "YUY2" : frameTypeInfo.format == 0 ? "UYVY" : "RAW");
                sb.Append("_");
                sb.Append(frameTypeInfo.width);
                sb.Append("x");
                sb.Append(frameTypeInfo.height);
                sb.Append("_@");
                sb.Append(frameTypeInfo.frame_rate);
                sb.Append("fps");
                return sb.ToString();
            }
        }


        private void DisplayProperties(TrackBar trackBar, TextBox textBox, CheckBox checkBox, int propertyValue, int isAutoEnabled, int min, int max, int isAutoSupported)
        {
            trackBar.Enabled = true;
            textBox.Text = propertyValue.ToString();
            trackBar.Minimum = min;
            trackBar.Maximum = max;
            trackBar.Value = propertyValue;

            if (isAutoSupported == True)
            {
                checkBox.Visible = true;
                if (isAutoEnabled == True)
                {
                    checkBox.Checked = true;
                    trackBar.Enabled = false;
                }
                else
                    checkBox.Checked = false;
            }
            else
            {
                checkBox.Visible = false;
            }
        }

        private void LogCameraInfo()
        {
            try
            {
                Log("-----------------------------");
                Log("Camera Info : " + Environment.NewLine);

                int IlluminationMode = -1;
                camera.GetIlluminationMode(out IlluminationMode);
                LogStatus("IlluminationMode", IlluminationMode);

                int VideoMode = -1;
                camera.GetVideoMode(out VideoMode);
                LogStatus("VideoMode", VideoMode);

                int ImageType = -1;
                camera.GetImageType(out ImageType);
                Log("ImageType: " + ImageType + "  " + GetImageTypeDesc(ImageType));
                Log("-----------------------------");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex + " \nLast Error: " + camera.LastError, errorMsgTitle);
            }


        }

        private void LogStatus(string infoName, int status)
        {
            Log(infoName + " : " + status);
            Log("-----------------------------");
        }

        private string GetImageTypeDesc(int imgType)
        {
            switch (imgType)
            {
                case 0:
                    return "[BMP]";
                case 1:
                    return "[JPEG]";
            }

            return "";
        }

        private void btnDeviceInfo_Click(object sender, EventArgs e)
        {
            GetDeviceInfo();
        }

        private void btnCameraInfo_Click(object sender, EventArgs e)
        {
            LogCameraInfo();
        }



        private void btnSetDefaults_Click(object sender, EventArgs e)
        {
            try
            {
                camera.SetDefaults();
                Log("SetDefaults()");
                UpdateUIWithCameraProperties();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex + " \nLast Error: " + camera.LastError, errorMsgTitle);
            }

        }

        private void btnReboot_Click(object sender, EventArgs e)
        {
            try
            {
                camera.RebootCamera(10);
                Log("RebootCamera()");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex + " \nLast Error: " + camera.LastError, errorMsgTitle);
            }

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtStatus.Text = "";
            txtImageType.Text = "";
            txtImageFormat.Text = "";
            txtImageSize.Text = "";
            txtTimestamp.Text = "";
            txtImageRes.Text = "";
            txtDecodeData.Text = "";
            picImage.Image = null;
        }

        private void btnWriteToFlash_Click(object sender, EventArgs e)
        {
            try
            {
                camera.WriteToFlash();
                Log("WriteToFlash()");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex + " \nLast Error: " + camera.LastError, errorMsgTitle);
            }

        }

        private void chkContImageEvent_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (isOpened)
                {
                    if (chkContImageEvent.Checked)
                    {
                        camera.RegisterForContinuousImageEvent(True);
                        registeredImageEventCount++;
                    }
                    else
                    {
                        camera.RegisterForContinuousImageEvent(False);
                        registeredImageEventCount--;
                    }
                }
                ChangeResolutionCBEnableState();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex + " \nLast Error: " + camera.LastError, errorMsgTitle);
            }

        }

        private void chkSnapshotImageEvent_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (isOpened)
                {
                    if (chkSnapshotImageEvent.Checked)
                    {
                        camera.RegisterForSnapshotImageEvent(True);
                        registeredImageEventCount++;
                    }
                    else
                    {
                        camera.RegisterForSnapshotImageEvent(False);
                        registeredImageEventCount--;
                    }
                }
                ChangeResolutionCBEnableState();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex + " \nLast Error: " + camera.LastError, errorMsgTitle);
            }

        }

        private void chkProduceImageEvent_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (isOpened)
                {
                    if (chkProduceImageEvent.Checked)
                    {
                        camera.RegisterForProduceImageEvent(True);
                        registeredImageEventCount++;
                    }
                    else
                    {
                        camera.RegisterForProduceImageEvent(False);
                        registeredImageEventCount--;
                    }
                }
                ChangeResolutionCBEnableState();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex + " \nLast Error: " + camera.LastError, errorMsgTitle);
            }

        }

        private void chkDecodeSessionStatusEvent_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (isOpened)
                {
                    if (chkDecodeSessionStatusEvent.Checked)
                    {
                        camera.RegisterForDecodeSessionStatusChangeEvent(True);
                        registeredImageEventCount++;
                        pictureBoxDeviceAwake.Image = (Image)deviceSleepImage;
                    }
                    else
                    {
                        camera.RegisterForDecodeSessionStatusChangeEvent(False);
                        registeredImageEventCount--;
                        pictureBoxDeviceAwake.Image = null;
                    }
                }
                ChangeResolutionCBEnableState();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex + " \nLast Error: " + camera.LastError, errorMsgTitle);
            }
        }

        private void cboVideoMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                short VideoMode = (short)cboVideoMode.SelectedIndex;
                Log("SetVideoMode(" + VideoMode + ")");
                camera.SetVideoMode(VideoMode);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex + " \nLast Error: " + camera.LastError, errorMsgTitle);
            }

        }

        private void cboIlluminationMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int IlluminationMode = (int)cboIlluminationMode.SelectedIndex;
                Log("SetIlluminationMode(" + IlluminationMode + ")");
                camera.SetIlluminationMode(IlluminationMode);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex + " \nLast Error: " + camera.LastError, errorMsgTitle);
            }

        }

        private void cboImageFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int imageType = (int)cboImageFormat.SelectedIndex;
                Log("SetImageType(" + imageType + ")");
                camera.SetImageType(imageType);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex + " \nLast Error: " + camera.LastError, errorMsgTitle);
            }

        }

        private void chkAutoSave_CheckedChanged(object sender, EventArgs e)
        {
            autoSave = chkAutoSave.Checked;
            if(chkAutoSave.Checked)
            {
                cboImageResolution.Enabled = false;
                cboImageFormat.Enabled = false;
            }
            else
            {
                cboImageResolution.Enabled = true;
                cboImageFormat.Enabled = true;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string decodeDataString = string.Join("_", txtDecodeData.Text.Split(Path.GetInvalidFileNameChars()));
            if (msImage.Length > 0)
                SaveImage(msImage, decodeDataString);
        }

        private void SaveImage(MemoryStream imageMemeoryStream, string decodeData)
        {
            string currentTimeStr = DateTime.Now.ToString("yyyyMMdd_HHmmss_fff");
            var buffer= new byte[LengthOfImageFormatSignatutre];
            imageMemeoryStream.Seek(0, SeekOrigin.Begin);
            imageMemeoryStream.Read(buffer, 0, LengthOfImageFormatSignatutre);

            string fileExtention = ImageExtensionBmp;
            if (buffer.SequenceEqual(jpgSignature))
            {
                fileExtention = ImageExtensionJpg;
            }
            else if(buffer.SequenceEqual(bmpSignature))
            {
                fileExtention = ImageExtensionBmp;
            }
            else
            {
                fileExtention = ImageExtensionRaw;
            }
            
      
            imageMemeoryStream.Seek(0, SeekOrigin.Begin);
            try
            {
                using (FileStream fileStream = new FileStream(Path.Combine(txtImageSaveLocation.Text, ImageNamePrefix + currentTimeStr + "_" + decodeData + "_c" + "." + fileExtention), System.IO.FileMode.CreateNew))
                {
                    imageMemeoryStream.WriteTo(fileStream);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void trkBrightness_Scroll(object sender, EventArgs e)
        {
            try
            {
                txtBrightness.Text = trkBrightness.Value.ToString();
                camera.SetBrightness(trkBrightness.Value, False);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex + " \nLast Error: " + camera.LastError, errorMsgTitle);
            }

        }

        private void trkContrast_Scroll(object sender, EventArgs e)
        {
            try
            {
                txtContrast.Text = trkContrast.Value.ToString();
                camera.SetContrast(trkContrast.Value, False);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex + " \nLast Error: " + camera.LastError, errorMsgTitle);
            }

        }

        private void trkSaturation_Scroll(object sender, EventArgs e)
        {
            try
            {
                txtSaturation.Text = trkSaturation.Value.ToString();
                camera.SetSaturation(trkSaturation.Value, False);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex + " \nLast Error: " + camera.LastError, errorMsgTitle);
            }
        }

        private void trkSharpness_Scroll(object sender, EventArgs e)
        {
            try
            {
                txtSharpness.Text = trkSharpness.Value.ToString();
                camera.SetSharpness(trkSharpness.Value, False);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex + " \nLast Error: " + camera.LastError, errorMsgTitle);
            }

        }

        private void trkGamma_Scroll(object sender, EventArgs e)
        {
            try
            {
                var value = (trkGamma.Value / trkGamma.TickFrequency) * trkGamma.TickFrequency;
                txtGamma.Text = value.ToString();
                camera.SetGamma(value, False);
                trkGamma.Value = value;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex + " \nLast Error: " + camera.LastError, errorMsgTitle);
            }

        }

        private void trkBacklight_Scroll(object sender, EventArgs e)
        {
            try
            {
                txtBacklight.Text = trkBacklight.Value.ToString();
                camera.SetBacklight(trkBacklight.Value, False);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex + " \nLast Error: " + camera.LastError, errorMsgTitle);
            }

        }

        private void trkGain_Scroll(object sender, EventArgs e)
        {
            try
            {
                txtGain.Text = trkGain.Value.ToString();
                camera.SetGain(trkGain.Value, False);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex + " \nLast Error: " + camera.LastError, errorMsgTitle);
            }

        }

        private void trkExposure_Scroll(object sender, EventArgs e)
        {
            try
            {

                txtExposure.Text = trkExposure.Value.ToString();
                camera.SetExposure(trkExposure.Value, False);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex + " \nLast Error: " + camera.LastError, errorMsgTitle);
            }


        }

        private void trkWhiteBalance_Scroll(object sender, EventArgs e)
        {
            int blueValue;
            int redValue;
            int finalValue;
            try
            {
                blueValue = trkWhiteBalanceBlue.Value;
                redValue = trkWhiteBalanceRed.Value;

                if (blueValue <= WhiteBalanceComponentMax && redValue <= WhiteBalanceComponentMax)
                {
                    txtWhiteBalanceBlue.Text = String.Format("{0:0.000}", WBToDouble(blueValue));
                    txtWhiteBalanceRed.Text = String.Format("{0:0.000}", WBToDouble(redValue));
                    finalValue = (blueValue << 16) | (redValue & 0x0000FFFF);
                    camera.SetWhiteBalance(finalValue, False);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex + " \nLast Error: " + camera.LastError, errorMsgTitle);
            }

        }

        private void chkAutoExposure_CheckedChanged(object sender, EventArgs e)
        {
            int value;
            int isAuto;

            try
            {
                camera.GetExposure(out value, out isAuto);
                camera.SetExposure(value, chkAutoExposure.Checked ? 1 : 0);
                trkExposure.Enabled = !chkAutoExposure.Checked;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex + " \nLast Error: " + camera.LastError, errorMsgTitle);
            }
        }

        // Making the oldValue a class variable as this need to persist across calls enable/disable WhiteBlanceAuto calls,
        // to revert back the WhiteBalance value when disabled. 
        int oldValue;
        private void chkAutoWhiteBalance_CheckedChanged(object sender, EventArgs e)
        {
        int isAuto;
            try
            {
                if (chkAutoWhiteBalanceBlue.Checked)
                {
                    camera.GetWhiteBalance(out oldValue, out isAuto);
                }
                camera.SetWhiteBalance(oldValue, chkAutoWhiteBalanceBlue.Checked ? 1 : 0);
                trkWhiteBalanceBlue.Enabled = !chkAutoWhiteBalanceBlue.Checked;
                trkWhiteBalanceRed.Enabled = !chkAutoWhiteBalanceBlue.Checked;
                txtWhiteBalanceBlue.ReadOnly = chkAutoWhiteBalanceBlue.Checked;
                txtWhiteBalanceRed.ReadOnly = chkAutoWhiteBalanceBlue.Checked;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex + " \nLast Error: " + camera.LastError, errorMsgTitle);
            }
        }

        private void EnableDisableControls(bool isEnabled)
        {
            trkBrightness.Enabled = isEnabled;
            trkContrast.Enabled = isEnabled;
            trkSaturation.Enabled = isEnabled;
            trkSharpness.Enabled = isEnabled;
            trkGamma.Enabled = isEnabled;
            trkWhiteBalanceBlue.Enabled = isEnabled;
            trkWhiteBalanceRed.Enabled = isEnabled;
            txtWhiteBalanceRed.ReadOnly = !isEnabled;
            txtWhiteBalanceBlue.ReadOnly = !isEnabled;
            trkBacklight.Enabled = isEnabled;
            trkGain.Enabled = isEnabled;
            trkExposure.Enabled = isEnabled;

            chkAutoBrightness.Enabled = isEnabled;
            chkAutoContrast.Enabled = isEnabled;
            chkAutoSaturation.Enabled = isEnabled;
            chkAutoSharpness.Enabled = isEnabled;
            chkAutoGamma.Enabled = isEnabled;
            chkAutoWhiteBalanceBlue.Enabled = isEnabled;
            chkAutoBacklight.Enabled = isEnabled;
            chkAutoGain.Enabled = isEnabled;
            chkAutoExposure.Enabled = isEnabled;

            chkContImageEvent.Enabled = isEnabled;
            chkContImageEvent.CheckState = CheckState.Unchecked;
            chkSnapshotImageEvent.Enabled = isEnabled;
            chkSnapshotImageEvent.CheckState = CheckState.Unchecked;
            chkProduceImageEvent.Enabled = isEnabled;
            chkProduceImageEvent.CheckState = CheckState.Unchecked;
            chkDecodeImageEvent.Enabled = isEnabled;
            chkDecodeImageEvent.CheckState = CheckState.Unchecked;
            chkDecodeSessionStatusEvent.Enabled = isEnabled;
            chkDecodeSessionStatusEvent.CheckState = CheckState.Unchecked;

            chkAutoSave.Enabled = isEnabled;
            chkAutoSave.CheckState = CheckState.Unchecked;
            

            btnSetDefaults.Enabled = isEnabled;
            btnReboot.Enabled = isEnabled;
            btnWriteToFlash.Enabled = isEnabled;
            btnSave.Enabled = isEnabled;
            btnSnapshot.Enabled = isEnabled;
            btnFirmwareUpdate.Enabled = isEnabled;
            btnLaunch.Enabled = isEnabled;
            btnBrowse.Enabled = isEnabled;
            btnCancelFWDownload.Enabled = isEnabled;
            btnBrowsImageSaveLocation.Enabled = isEnabled;
            btnWBCGet.Enabled = isEnabled;
            btnWBCSet.Enabled = isEnabled;

            cboIlluminationMode.Enabled = isEnabled;
            cboImageFormat.Enabled = isEnabled;
            cboVideoMode.Enabled = isEnabled;
            cbPowerUserMode.Enabled = isEnabled;
            cboImageResolution.Enabled = isEnabled;

            txtFwFile.Enabled = isEnabled;
            txtWhiteBalanceBlue.Enabled = isEnabled;
            txtWhiteBalanceRed.Enabled = isEnabled;
            txtWBCBlue.Enabled = isEnabled;
            txtWBCRed.Enabled = isEnabled;

            txtImageType.Enabled = isEnabled;
            txtImageRes.Enabled = isEnabled;
            txtImageFormat.Enabled = isEnabled;
            txtImageSize.Enabled = isEnabled;
            txtTimestamp.Enabled = isEnabled;
            txtDecodeData.Enabled = isEnabled;

            btnLoadConfig.Enabled = isEnabled;
            btnRetieveConfig.Enabled = isEnabled;
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Log("Close()");
                if (isOpened)
                {
                    camera.Close();
                    isOpened = false;
                }
                btnClose.Enabled = false;
                btnOpen.Enabled = true;
                EnableDisableControls(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex + " \nLast Error: " + camera.LastError, errorMsgTitle);
            }

        }

        private void btnSnapshot_Click(object sender, EventArgs e)
        {
            Array buffer = null;
            try
            {
                Log("CaptureSnapshot()");
                int length, width, height, type;

                camera.CaptureSnapshot( ref buffer, out length, out width, out height, out type);

                if (length > 0)
                {
                    byte[] byteBuffer = new byte[length];

                    ((Array)buffer).CopyTo(byteBuffer, 0);
                    msImage.Flush();
                    msImage.Seek(0, SeekOrigin.Begin);
                    msImage.Write(byteBuffer, 0, byteBuffer.Length);
                    curForm.Invoke((MethodInvoker)delegate
                    {
                        lock (imageEventLock)
                        {
                            picImage.Image = Image.FromStream(msImage);
                        }
                        txtTimestamp.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff");
                        txtImageSize.Text = length.ToString();
                        txtImageFormat.Text = (type == 1) ? ImageExtensionJpg : ImageExtensionBmp;
                        txtImageType.Text = "Snapshot Image";
                        txtDecodeData.Text = "";
                        txtImageRes.Text = width + "x" + height;

                        if (autoSave)
                            SaveImage(msImage, "");
                    });
                }
                else
                {
                    MessageBox.Show("Error : Image capture timed out. Please reboot the camera and retry.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex + " \nLast Error: " + camera.LastError, errorMsgTitle);
            }
            finally
            {
                buffer = null;
            }

        }

        private void chkDecodeImageEvent_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (isOpened)
                {
                    if (chkDecodeImageEvent.Checked)
                    {
                        camera.RegisterForDecodeImageEvent(True);
                        registeredImageEventCount++;
                    }
                    else
                    {
                        camera.RegisterForDecodeImageEvent(False);
                        registeredImageEventCount--;
                    }
                }
                ChangeResolutionCBEnableState();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex + " \nLast Error: " + camera.LastError, errorMsgTitle);
            }
        }

        private void ChangeResolutionCBEnableState()
        {
            if (registeredImageEventCount > 0)
                cboImageResolution.Enabled = false;
            else
                cboImageResolution.Enabled = true;
        }

        private void btnFirmwareUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                camera.DownloadFirmware(txtFwFile.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex + " \nLast Error: " + camera.LastError, errorMsgTitle);

            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "DAT files (*.dat)|*.dat";
                var result = ofd.ShowDialog();
                if (result == DialogResult.OK)
                {
                    txtFwFile.Text = ofd.FileName;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex + " \nLast Error: " + camera.LastError, errorMsgTitle);
            }
        }

        private void btnLaunch_Click(object sender, EventArgs e)
        {
            try
            {
                camera.InstallFirmware();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex + " \nLast Error: " + camera.LastError, errorMsgTitle);
            }
        }

        void EnableControlsOnPowerUserMode(bool isEnabled)
        {
            if (!chkAutoWhiteBalanceBlue.Checked)
            {
                trkWhiteBalanceBlue.Enabled = isEnabled;
                trkWhiteBalanceRed.Enabled = isEnabled;
            }
            chkAutoWhiteBalanceBlue.Enabled = isEnabled;
            chkAutoWhiteBalanceRed.Enabled = isEnabled;
            if (!chkAutoExposure.Checked)
            {
                trkExposure.Enabled = isEnabled;
            }
            chkAutoExposure.Enabled = isEnabled;
            trkGain.Enabled = isEnabled;
        }

        private void cbPowerUserMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbPowerUserMode.SelectedIndex == 0)
            {
                var result = MessageBox.Show("Power User Mode enabled. Some camera settings will be unavailable.", "Power user mode", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (result == DialogResult.OK)
                {
                    camera.SetPowerUserMode(True);
                    EnableControlsOnPowerUserMode(false);
                }
            }
            else
            {
                camera.SetPowerUserMode(False);
                EnableControlsOnPowerUserMode(true);
            }
        }

        void UpdateWBCValue(TextBox wbcBlue, TextBox wbcRed)
        {
            double blue;
            double red;

            if (Double.TryParse(wbcBlue.Text, out blue))
            {
                if (blue <= 4 && blue >= 0)
                {
                    trkWhiteBalanceBlue.Value = DoubleToWB(blue);
                }
                else
                {
                    Log("Invalid white balance component value");
                    return;
                }
            }
            if (Double.TryParse(wbcRed.Text, out red))
            {
                if (red <= 4 && red >= 0)
                {
                    trkWhiteBalanceRed.Value = DoubleToWB(red);
                }
                else
                {
                    Log("Invalid white balance component value");
                    return;
                }
            }

            trkWhiteBalance_Scroll(trkWhiteBalanceBlue, null);
        }

        private void txtWhiteBalanceBlue_Leave(object sender, EventArgs e)
        {
            if (isOpened)
                UpdateWBCValue(txtWhiteBalanceBlue, txtWhiteBalanceRed);
        }

        private void cboImageResolution_SelectedIndexChanged(object sender, EventArgs e)
        {
            FrameTypeComboItem ftci = (FrameTypeComboItem)(sender as ComboBox).SelectedItem;
            FrameTypeInfo fti = ftci.FrameTypeInfo;
            camera.SetFrameType(ref fti);
        }

        private void cboDevices_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if(isOpened)
            {
                cboDevices.SelectedIndex = currentlySelectedDeviceIndex;
                MessageBox.Show(this, "Please close the opened camera to select a different one.","Could not change the camera.");
            }
        }

        private void btnCncelFWDownload_Click(object sender, EventArgs e)
        {
            try
            {
                camera.CancelFirmwareDownload();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex + " \nLast Error: " + camera.LastError, errorMsgTitle);
            }
        }

        private void btnBrowsImageSaveLocation_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            var dialogResult = fbd.ShowDialog();
            if(dialogResult == DialogResult.OK)
            {
                txtImageSaveLocation.Text = fbd.SelectedPath;
            }
        }

        private void btnWBCGet_Click(object sender, EventArgs e)
        {
            int propertyValue = -1;
            int isAutoEnabled = False;
            int blueValue = 0;
            int redValue = False;

            DisplayWhiteBalanceComponentData();
            camera.GetWhiteBalance(out propertyValue, out isAutoEnabled);
            GetWhiteBalanceComponentValues(propertyValue, out blueValue, out redValue);
            txtWBCRed.Text= String.Format("{0:0.000}", WBToDouble(redValue));
            txtWBCBlue.Text = String.Format("{0:0.000}", WBToDouble(blueValue));
        }

        private void btnWBCSet_Click(object sender, EventArgs e)
        {
            UpdateWBCValue(txtWBCBlue, txtWBCRed);
        }

        private void btnLoadConfig_Click(object sender, EventArgs e)
        {
            Log("Loading configuration.");
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Title = "Load configuration";
                ofd.Filter = "bcccfg Files (*.bcccfg)|*.bcccfg";
                var result = ofd.ShowDialog();
                if (result == DialogResult.OK)
                {
                    Log(ofd.FileName);
                    camera.LoadConfigurationFromFile(ofd.FileName);
                    UpdateUIWithCameraProperties();
                    string message = "Load configuration succeeded.";
                    Log(message);
                    MessageBox.Show(message, "Load configuration");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Load configuration Failed. Error : " + ex.Message + " \nLast Error: " + camera.LastError, errorMsgTitle);
            }
        }

        private void btnRetieveConfig_Click(object sender, EventArgs e)
        {
            try
            {
                string fileName = "Config_file_";
                string currentTime = DateTime.Now.ToString("_yyyy.MM.dd");
                fileName = fileName + txtModelNumber.Text + currentTime;
                string configurationXml;
                camera.RetrieveConfiguration(out configurationXml);
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Title= "Save configuration";
                sfd.Filter = "bcccfg Files (*.bcccfg)|*.bcccfg";
                sfd.FileName = fileName;
                sfd.CheckPathExists = true;
                sfd.OverwritePrompt = true;
                var result = sfd.ShowDialog();
                if (result == DialogResult.OK)
                {
                    File.WriteAllText(sfd.FileName, configurationXml);
                    string message = "Retrieve configuration succeeded.";
                    Log(message);
                    MessageBox.Show(message, "Retrieve configuration");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Retrieve configuration Failed. Error : " + ex.Message + " \nLast Error: " + camera.LastError, errorMsgTitle);
            }
        }

        private void chkDetectBoundingBox_CheckedChanged(object sender, EventArgs e)
        {
            detectBoundingBox = chkDetectBoundingBox.Checked;
        }

        private void btnSetBackground_Click(object sender, EventArgs e)
        {
            saveBackground = true;
        }
    }
}
