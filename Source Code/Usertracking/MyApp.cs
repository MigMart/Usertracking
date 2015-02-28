using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Kinect;
using Microsoft.Kinect.Toolkit;
using Microsoft.Kinect.Toolkit.FaceTracking;
using VirtualKeyboard;
using System.Runtime.InteropServices;


namespace Usertracking
{
    public class MyApp
    {
        public Microsoft.Kinect.KinectSensor kinect;

        public VKeyboard VK;
        public FaceTracker FT;
        public FaceTrackFrame faceFrame;

        private FormMain fMain;
        private FormVideo fVideo;
        private FormCalibra2 fCalibration;
        private FormConfiguration fConfiguration;

        public AVGStabilizerV3D FacePositionStabilizer, FaceRotationStabilizer;
        public AVGStabilizer MouseXStabilizer, MouseYStabilizer, ElevationAngleStabilizer;
        public float cursorx, cursory;

        public bool Calibrating=false;
        public bool Parking = false;
        public string DefaultSystemMouseCursor;
        public MouseButtons UseMouseButton;
        public bool MouseButtonDown;
        public SpeechRecorder speech;

        public struct structUserInfo
        {

            public SkeletonPoint Position;
            public Vector3DF FacePosition;
            public Vector3DF FaceRotation;
            public bool UserDetected;
            public bool UserFaceDetected;

            public float minMouth4MouseClick;
            public float currentMouthOpening;
            public float currentEyebrowOpening;
            public bool MouthClickDown;
            public bool ForceClickDown;
            
        };

        public structUserInfo UserInfo;


        public struct structScreenInfo
        {
            public int WidthPix;        //Screen horizontal resolution
            public int HeightPix;       //Screen vertical resolution
            public float WidthCm;       //Screen width in centimeters
            public float HeightCm;      //Screen height in centimeters
            public string DeviceName;   //Monitor brand and model
            public float CameraFPS;     //kinect framerate
        };

        public struct structCorrectionValues
        {
            public float eRX;
            public float eRY;
            public float eRZ;

            public float eX;
            public float eY;
            public float eZ;

            public float eCursorX;
            public float eCursorY;

        };

        public structCorrectionValues CalibrationValues;

        public structScreenInfo ScreenInfo;
        
        public MyApp()
        {
            this.FacePositionStabilizer = new AVGStabilizerV3D(10);
            this.FaceRotationStabilizer = new AVGStabilizerV3D(10);

            this.MouseXStabilizer = new AVGStabilizer(20);
            this.MouseYStabilizer = new AVGStabilizer(20);
            this.ElevationAngleStabilizer = new AVGStabilizer(10);


            this.UserInfo.minMouth4MouseClick = Properties.Settings.Default.minMouth4MouseClick;
            this.UserInfo.ForceClickDown = false;

            this.fMain = new FormMain(this);

            AutoDetectMonitorResolution(); //This must be called after FormMain() initialize
            AutoDetectMonitorName(); //This must be called after FormMain() initialize

            if (this.ScreenInfo.DeviceName != Properties.Settings.Default.MonitorName || Properties.Settings.Default.MonitorWidth == 0)
            {
                this.AutoDetectMonitorInfo(); //This gets monitor calculate monitor size in centimeters (regarding system information). Usually, it isn't real settings.
            }

            if (Properties.Settings.Default.CamPosX ==0)
            {
                Properties.Settings.Default.CamPosX = Properties.Settings.Default.MonitorWidth / 2.0f;
                Properties.Settings.Default.CamPosY = 0;
                Properties.Settings.Default.Save();
            }
                
            this.ScreenInfo.WidthCm = Properties.Settings.Default.MonitorWidth;
            this.ScreenInfo.HeightCm = Properties.Settings.Default.MonitorHeight;

            this.MouseXStabilizer.use_error_thresholding = Properties.Settings.Default.UseHorizErrorCorrection;
            this.MouseXStabilizer.min_error_threshold = Properties.Settings.Default.MinHorizErrorThrsh;
            this.MouseXStabilizer.max_error_threshold = Properties.Settings.Default.MaxHorizErrorThrsh;

            this.MouseYStabilizer.use_error_thresholding = Properties.Settings.Default.UseVertErrorCorrection;
            this.MouseYStabilizer.min_error_threshold = Properties.Settings.Default.MinVertErrorThrsh;
            this.MouseYStabilizer.max_error_threshold = Properties.Settings.Default.MaxVertErrorThrsh;

            this.CalibrationValues.eRZ = Properties.Settings.Default.CorrectFaceOrientationZ;
            this.CalibrationValues.eRY = Properties.Settings.Default.CorrectFaceOrientationY;
            this.CalibrationValues.eRX = Properties.Settings.Default.CorrectFaceOrientationX;


            this.CalibrationValues.eZ = Properties.Settings.Default.CorrectFacePositionZ;
            this.CalibrationValues.eY = Properties.Settings.Default.CorrectFacePositionY;
            this.CalibrationValues.eX = Properties.Settings.Default.CorrectFacePositionX;

            this.CalibrationValues.eCursorY = Properties.Settings.Default.CorrectCursorY;
            this.CalibrationValues.eCursorX = Properties.Settings.Default.CorrectCursorX;
            
            this.UseMouseButton = MouseButtons.Left;
            this.MouseButtonDown = false;
            
            //this.numElevationAngle.Value = (decimal)Properties.Settings.Default.SetKinectElevationAngle;

            DefaultSystemMouseCursor = Mouse.GetArrowCursor();

            this.speech = new SpeechRecorder(this.kinect);
            this.speech.SpeechRecorded+=speech_SpeechRecorded;
            //Mouse.ChangeArrowCursor(Application.CommonAppDataPath + @"\PointUp.cur");
        }

        ~MyApp()
        {
            Mouse.ChangeArrowCursor(DefaultSystemMouseCursor);

            if (this.speech != null)
                this.speech.Stop();

            if (this.kinect!=null)
                if (this.kinect.IsRunning)
                    this.kinect.Stop();
        }

        public Form MainForm()
        {
            if (this.fMain == null)
                this.fMain = new FormMain(this);

            if (this.fMain.IsDisposed)
                this.fMain = new FormMain(this);


            return (Form)this.fMain;
        }


        public Form CalibrationForm()
        {
            if (this.fCalibration == null)
                this.fCalibration = new FormCalibra2(this);

            if (this.fCalibration.IsDisposed)
                this.fCalibration = new FormCalibra2(this);

            return (Form)this.fCalibration;
        }

        public Form VideoForm()
        {
            if (this.fVideo == null)
                this.fVideo = new FormVideo(this);

            if (this.fVideo.IsDisposed)
                this.fVideo = new FormVideo(this);

            return (Form)this.fVideo;
        }

        public Form ConfigurationForm()
        {
            if (this.fConfiguration == null)
                this.fConfiguration = new FormConfiguration(this);

            if (this.fConfiguration.IsDisposed)
                this.fConfiguration = new FormConfiguration(this);

            return (Form)this.fConfiguration;
        }

        public bool RunCalibration()
        {


            this.MainForm().Hide();

            this.Calibrating = true;
            this.CalibrationForm().Show();

            return true;
        }

        

        [System.Runtime.InteropServices.DllImport("gdi32.dll")] static extern int GetDeviceCaps(IntPtr hdc, int nIndex);

        private const int HORZSIZE = 4;
        private const int VERTSIZE = 6;
        private const double MM_TO_INCH_CONVERSION_FACTOR = 25.4;

        [Flags()]
        public enum DisplayDeviceStateFlags : int
        {
            /// <summary>The device is part of the desktop.</summary>
            AttachedToDesktop = 0x1,
            MultiDriver = 0x2,
            /// <summary>The device is part of the desktop.</summary>
            PrimaryDevice = 0x4,
            /// <summary>Represents a pseudo device used to mirror application drawing for remoting or other purposes.</summary>
            MirroringDriver = 0x8,
            /// <summary>The device is VGA compatible.</summary>
            VGACompatible = 0x10,
            /// <summary>The device is removable; it cannot be the primary display.</summary>
            Removable = 0x20,
            /// <summary>The device has more display modes than its output devices support.</summary>
            ModesPruned = 0x8000000,
            Remote = 0x4000000,
            Disconnect = 0x2000000
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct DisplayDevice
        {
            [MarshalAs(UnmanagedType.U4)]
            public int cb;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string DeviceName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string DeviceString;
            [MarshalAs(UnmanagedType.U4)]
            public DisplayDeviceStateFlags StateFlags;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string DeviceID;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string DeviceKey;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct DeviceMode
        {
            private const int CCHDEVICENAME = 0x20;
            private const int CCHFORMNAME = 0x20;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x20)]
            public string dmDeviceName;
            public short dmSpecVersion;
            public short dmDriverVersion;
            public short dmSize;
            public short dmDriverExtra;
            public int dmFields;
            public int dmPositionX;
            public int dmPositionY;
            public ScreenOrientation dmDisplayOrientation;
            public int dmDisplayFixedOutput;
            public short dmColor;
            public short dmDuplex;
            public short dmYResolution;
            public short dmTTOption;
            public short dmCollate;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x20)]
            public string dmFormName;
            public short dmLogPixels;
            public int dmBitsPerPel;
            public int dmPelsWidth;
            public int dmPelsHeight;
            public int dmDisplayFlags;
            public int dmDisplayFrequency;
            public int dmICMMethod;
            public int dmICMIntent;
            public int dmMediaType;
            public int dmDitherType;
            public int dmReserved1;
            public int dmReserved2;
            public int dmPanningWidth;
            public int dmPanningHeight;
        }

        [DllImport("user32.dll")]
        public static extern bool EnumDisplaySettings(string deviceName, int modeNum, ref DeviceMode devMode); 

        [DllImport("User32.dll")]
        static extern int EnumDisplayDevices(string lpDevice, int iDevNum, ref DisplayDevice lpDisplayDevice, int dwFlags);

        public enum DeviceCap
        {
            /// <summary>
            /// Device driver version
            /// </summary>
            DRIVERVERSION = 0,
            /// <summary>
            /// Device classification
            /// </summary>
            TECHNOLOGY = 2,
            /// <summary>
            /// Horizontal size in millimeters
            /// </summary>
            HORZSIZE = 4,
            /// <summary>
            /// Vertical size in millimeters
            /// </summary>
            VERTSIZE = 6,
            /// <summary>
            /// Horizontal width in pixels
            /// </summary>
            HORZRES = 8,
            /// <summary>
            /// Vertical height in pixels
            /// </summary>
            VERTRES = 10,
            /// <summary>
            /// Number of bits per pixel
            /// </summary>
            BITSPIXEL = 12,
            /// <summary>
            /// Number of planes
            /// </summary>
            PLANES = 14,
            /// <summary>
            /// Number of brushes the device has
            /// </summary>
            NUMBRUSHES = 16,
            /// <summary>
            /// Number of pens the device has
            /// </summary>
            NUMPENS = 18,
            /// <summary>
            /// Number of markers the device has
            /// </summary>
            NUMMARKERS = 20,
            /// <summary>
            /// Number of fonts the device has
            /// </summary>
            NUMFONTS = 22,
            /// <summary>
            /// Number of colors the device supports
            /// </summary>
            NUMCOLORS = 24,
            /// <summary>
            /// Size required for device descriptor
            /// </summary>
            PDEVICESIZE = 26,
            /// <summary>
            /// Curve capabilities
            /// </summary>
            CURVECAPS = 28,
            /// <summary>
            /// Line capabilities
            /// </summary>
            LINECAPS = 30,
            /// <summary>
            /// Polygonal capabilities
            /// </summary>
            POLYGONALCAPS = 32,
            /// <summary>
            /// Text capabilities
            /// </summary>
            TEXTCAPS = 34,
            /// <summary>
            /// Clipping capabilities
            /// </summary>
            CLIPCAPS = 36,
            /// <summary>
            /// Bitblt capabilities
            /// </summary>
            RASTERCAPS = 38,
            /// <summary>
            /// Length of the X leg
            /// </summary>
            ASPECTX = 40,
            /// <summary>
            /// Length of the Y leg
            /// </summary>
            ASPECTY = 42,
            /// <summary>
            /// Length of the hypotenuse
            /// </summary>
            ASPECTXY = 44,
            /// <summary>
            /// Shading and Blending caps
            /// </summary>
            SHADEBLENDCAPS = 45,

            /// <summary>
            /// Logical pixels inch in X
            /// </summary>
            LOGPIXELSX = 88,
            /// <summary>
            /// Logical pixels inch in Y
            /// </summary>
            LOGPIXELSY = 90,

            /// <summary>
            /// Number of entries in physical palette
            /// </summary>
            SIZEPALETTE = 104,
            /// <summary>
            /// Number of reserved entries in palette
            /// </summary>
            NUMRESERVED = 106,
            /// <summary>
            /// Actual color resolution
            /// </summary>
            COLORRES = 108,

            // Printing related DeviceCaps. These replace the appropriate Escapes
            /// <summary>
            /// Physical Width in device units
            /// </summary>
            PHYSICALWIDTH = 110,
            /// <summary>
            /// Physical Height in device units
            /// </summary>
            PHYSICALHEIGHT = 111,
            /// <summary>
            /// Physical Printable Area x margin
            /// </summary>
            PHYSICALOFFSETX = 112,
            /// <summary>
            /// Physical Printable Area y margin
            /// </summary>
            PHYSICALOFFSETY = 113,
            /// <summary>
            /// Scaling factor x
            /// </summary>
            SCALINGFACTORX = 114,
            /// <summary>
            /// Scaling factor y
            /// </summary>
            SCALINGFACTORY = 115,

            /// <summary>
            /// Current vertical refresh rate of the display device (for displays only) in Hz
            /// </summary>
            VREFRESH = 116,
            /// <summary>
            /// Horizontal width of entire desktop in pixels
            /// </summary>
            DESKTOPVERTRES = 117,
            /// <summary>
            /// Vertical height of entire desktop in pixels
            /// </summary>
            DESKTOPHORZRES = 118,
            /// <summary>
            /// Preferred blt alignment
            /// </summary>
            BLTALIGNMENT = 119
        }

        public void AutoDetectMonitorResolution()
        {
            this.ScreenInfo.WidthPix = Screen.FromHandle(this.MainForm().Handle).Bounds.Width;
            this.ScreenInfo.HeightPix = Screen.FromHandle(this.MainForm().Handle).Bounds.Height;
        }

        public void AutoDetectMonitorName()
        {

            if (this.MainForm() != null)
            {
                this.ScreenInfo.DeviceName = Screen.FromHandle(this.MainForm().Handle).DeviceName;
 
                DisplayDevice d = new DisplayDevice();
                d.cb = Marshal.SizeOf(d);

                if (EnumDisplayDevices(this.ScreenInfo.DeviceName, 0, ref d, 0) >= 1)
                    this.ScreenInfo.DeviceName = d.DeviceString;
            }
            else
            {
                this.ScreenInfo.DeviceName = "(not detected)";
            }

        }

        public bool AutoDetectMonitorInfo()
        {
            bool returnvalue=false;

            if (this.MainForm()!=null)
            {

                this.ScreenInfo.WidthPix = Screen.FromHandle(this.MainForm().Handle).Bounds.Width;
                this.ScreenInfo.HeightPix = Screen.FromHandle(this.MainForm().Handle).Bounds.Height;
                
                this.ScreenInfo.DeviceName = Screen.FromHandle(this.MainForm().Handle).DeviceName;
                DisplayDevice d=new DisplayDevice();
                d.cb = Marshal.SizeOf(d);

                if (EnumDisplayDevices(this.ScreenInfo.DeviceName, 0, ref d, 0) >= 1)
                    this.ScreenInfo.DeviceName = d.DeviceString;

                         
                var hDC = System.Drawing.Graphics.FromHwnd(this.MainForm().Handle).GetHdc();

                int horizontalSizeInMilliMeters = GetDeviceCaps(hDC, (int) DeviceCap.HORZSIZE);
                //this.ScreenInfo.HorizRes = GetDeviceCaps(hDC, (int)DeviceCap.HORZRES);
                //double horizontalSizeInInches = horizontalSizeInMilliMeters / MM_TO_INCH_CONVERSION_FACTOR;
                int vertivalSizeInMilliMeters = GetDeviceCaps(hDC, (int) DeviceCap.VERTSIZE);
                //this.ScreenInfo.VertRes = GetDeviceCaps(hDC, (int)DeviceCap.VERTRES);
                //double verticalSizeInInches = vertivalSizeInMilliMeters / MM_TO_INCH_CONVERSION_FACTOR;
                  
                this.ScreenInfo.WidthCm = horizontalSizeInMilliMeters / 10;
                this.ScreenInfo.HeightCm = vertivalSizeInMilliMeters / 10;


                
                returnvalue=true;
            }else{
                this.ScreenInfo.WidthCm = 0;
                this.ScreenInfo.HeightCm = 0;
                this.ScreenInfo.WidthPix = 0;
                this.ScreenInfo.HeightPix = 0;
                this.ScreenInfo.DeviceName = "(not detected)";
                returnvalue=false;
            }

            Properties.Settings.Default.MonitorWidth = this.ScreenInfo.WidthCm;
            Properties.Settings.Default.MonitorHeight = this.ScreenInfo.HeightCm;
            Properties.Settings.Default.MonitorName = this.ScreenInfo.DeviceName;
            Properties.Settings.Default.Save();

            return returnvalue;
        }

        public void CalcCursorCoordinates()
        {
            //CalcCursorCoordinatesFromCentroide();
            CalcCursorCoordinatesFromEyes();
        }

        private void CalcCursorCoordinatesFromEyes()
        {


            if (this.kinect != null)
                if (this.kinect.IsRunning)
                {
                    VectorCalc.Vector3D rightEyeOV = VectorCalc.OrthoVector(faceFrame.Get3DShape()[70], faceFrame.Get3DShape()[71], faceFrame.Get3DShape()[75]);
                    VectorCalc.Vector3D leftEyeOV = VectorCalc.OrthoVector(faceFrame.Get3DShape()[72], faceFrame.Get3DShape()[73], faceFrame.Get3DShape()[69]);

                    VectorCalc.Vector3D centerEyeOV = rightEyeOV + leftEyeOV;

                    centerEyeOV.X += Properties.Settings.Default.CorrectFacePositionX;
                    centerEyeOV.Y += Properties.Settings.Default.CorrectFacePositionY;
                    centerEyeOV.Z += Properties.Settings.Default.CorrectFacePositionZ;

                    VectorCalc.Vector3D userdirection = centerEyeOV.Direction();
                    VectorCalc.Vector3D screenNormal = new VectorCalc.Vector3D(0, 0, 0, 0, 0, 1);
                    VectorCalc.Point3D any_point_of_screen = 0;

                    VectorCalc.Point3D ScreenOrigin = new VectorCalc.Point3D(Properties.Settings.Default.CamPosX, 0 - Properties.Settings.Default.CamPosY - 5.0f, 0);
                    VectorCalc.Point3D PointOnScreenCm = VectorCalc.intersection(userdirection, any_point_of_screen, screenNormal) * 100.0f + ScreenOrigin;

                    PointOnScreenCm.Y = this.ScreenInfo.HeightCm - PointOnScreenCm.Y;

                    VectorCalc.Point3D PointOnScreenPix = VectorCalc.Scale(new VectorCalc.Point3D(0, 0, 0), new VectorCalc.Point3D(this.ScreenInfo.WidthPix, this.ScreenInfo.HeightPix, 0), new VectorCalc.Point3D(0, 0, 0), new VectorCalc.Point3D(this.ScreenInfo.WidthCm, this.ScreenInfo.HeightCm, 0), PointOnScreenCm);

                    this.cursorx = PointOnScreenPix.X + Properties.Settings.Default.CorrectCursorX;
                    this.cursory = PointOnScreenPix.Y + Properties.Settings.Default.CorrectCursorY;
                }
        }

        public void CalcCursorCoordinatesFromCentroide(){

            
            if (this.kinect!=null)
                if (this.kinect.IsRunning)
                {
                    float KinElevationAngle = 0;
                    float corrected_Rx;
                    float corrected_Ry;
                    float corrected_Rz;

                    float corrected_X;
                    float corrected_Y;
                    float corrected_Z;

                    KinElevationAngle = this.kinect.ElevationAngle;

                    this.ElevationAngleStabilizer.stabilize(ref KinElevationAngle);
                    /*
                    //this.App.UserInfo.FacePosition = faceFrame.Translation;
                    this.UserInfo.FacePosition.X = (float)Math.Round(this.UserInfo.FacePosition.X, 2);
                    this.UserInfo.FacePosition.Y = (float)Math.Round(this.UserInfo.FacePosition.Y, 2);
                    this.UserInfo.FacePosition.Z = (float)Math.Round(this.UserInfo.FacePosition.Z, 2);
                    //this.App.UserInfo.FaceRotation = faceFrame.Rotation;
                    this.UserInfo.FaceRotation.X = (float)Math.Floor(this.UserInfo.FaceRotation.X);
                    this.UserInfo.FaceRotation.Y = (float)Math.Floor(this.UserInfo.FaceRotation.Y);
                    this.UserInfo.FaceRotation.Z = (float)Math.Floor(this.UserInfo.FaceRotation.Z);
                    */
                    this.FacePositionStabilizer.stabilize(ref this.UserInfo.FacePosition);
                    this.FaceRotationStabilizer.stabilize(ref this.UserInfo.FaceRotation);
                    /*
                    //this.App.UserInfo.FaceRotation = faceFrame.Rotation;
                    this.UserInfo.FaceRotation.X = (float)Math.Floor(this.UserInfo.FaceRotation.X);
                    this.UserInfo.FaceRotation.Y = (float)Math.Floor(this.UserInfo.FaceRotation.Y);
                    this.UserInfo.FaceRotation.Z = (float)Math.Floor(this.UserInfo.FaceRotation.Z);

                    //this.App.UserInfo.FacePosition = faceFrame.Translation;
                    this.UserInfo.FacePosition.X = (float)Math.Round(this.UserInfo.FacePosition.X, 2);
                    this.UserInfo.FacePosition.Y = (float)Math.Round(this.UserInfo.FacePosition.Y, 2);
                    this.UserInfo.FacePosition.Z = (float)Math.Round(this.UserInfo.FacePosition.Z, 2);
                    */

                    corrected_Rx = this.UserInfo.FaceRotation.X + KinElevationAngle + this.CalibrationValues.eRX;
                    corrected_Ry = this.UserInfo.FaceRotation.Y + this.CalibrationValues.eRY;
                    corrected_Rz = this.UserInfo.FaceRotation.Z + this.CalibrationValues.eRZ;


                    corrected_X = this.UserInfo.FacePosition.X+this.CalibrationValues.eX;
                    corrected_Y = this.UserInfo.FacePosition.Y + this.CalibrationValues.eY;
                    corrected_Z = this.UserInfo.FacePosition.Z+ this.CalibrationValues.eZ;

                    float cy = (float)-1 * (5.0f + Properties.Settings.Default.CamPosY) * (float)0.01 - corrected_Y - (float)Math.Abs(corrected_Z) * (float)Math.Tan(corrected_Rx * (float)Math.PI / 180);
                    float cx = ((float) (Properties.Settings.Default.CamPosX)) * 0.01f + corrected_X - (float)Math.Sqrt(Math.Pow(corrected_Z, 2) + Math.Pow(corrected_Y - cy, 2)) * (float)Math.Tan((double)(corrected_Ry) * Math.PI / 180);

                    cursorx = cx / (this.ScreenInfo.WidthCm * (float)0.01) * this.ScreenInfo.WidthPix;
                    if (cursorx < 0) cursorx = 0;
                    if (cursorx > this.ScreenInfo.WidthPix + 31) cursorx = this.ScreenInfo.WidthPix + 31; //compensate mouse cursor width

                    cursory = cy / (this.ScreenInfo.HeightCm * (float)0.01) * this.ScreenInfo.HeightPix;
                    if (cursory < 0) cursory = 0;
                    if (cursory > this.ScreenInfo.HeightPix + 31) cursory = this.ScreenInfo.HeightPix + 31;//compensate mouse cursor height

                    /*
                    this.cursorx = (float)Math.Round(this.cursorx);
                    this.cursory = (float)Math.Round(this.cursory);
                    */
                    this.MouseXStabilizer.stabilize(ref this.cursorx);
                    this.MouseYStabilizer.stabilize(ref this.cursory);
                    /*
                    this.cursorx = (float)Math.Round(this.cursorx);
                    this.cursory = (float)Math.Round(this.cursory);
                    */



                    this.cursorx += this.CalibrationValues.eCursorX;
                    this.cursorx += this.CalibrationValues.eCursorY;
                    
                }
			/*
			if (KalmanFilterActivated)
				ApplyKalmanFilter(&cursorx, &cursory);
            */
        }

        private int innercounter = 0;

        public void MoveMouseToXY(int x, int y)
        {
            float dx = 0, dy = 0;
            
            Mouse.MousePoint p = Mouse.GetCursorPosition();

            dx = (x - p.X);
            dy = (y - p.Y);

            float abs_dx;
            float abs_dy;

            if (dx > 0)
                abs_dx = dx;
            else
                abs_dx = (-dx);

            if (dy > 0)
                abs_dy = dy;
            else
                abs_dy = (-dy);

            if (Properties.Settings.Default.MinHorizErrorThrsh < abs_dx && abs_dx < Properties.Settings.Default.MaxHorizErrorThrsh)
                dx = 0;

            if (Properties.Settings.Default.MinVertErrorThrsh < abs_dy && abs_dy < Properties.Settings.Default.MaxVertErrorThrsh)
                dy = 0;

            dx /= this.ScreenInfo.CameraFPS * 3;
            dy /= this.ScreenInfo.CameraFPS * 3;



            if (this.Parking)
            {
                if (abs_dx >= Properties.Settings.Default.MaxHorizErrorThrsh && abs_dy >= Properties.Settings.Default.MaxVertErrorThrsh)
                {
                    if (!Mouse.isVisible)
                    {
                        Mouse.Showcursor();
                    }


                    innercounter = 0;
                }
                else
                {
                    if (Mouse.isVisible)
                    {
                        innercounter++;
                        if (innercounter >= this.ScreenInfo.CameraFPS)
                            Mouse.Hidecursor();

                        if (innercounter >= int.MaxValue)
                            innercounter = 0;
                    }

                    dx = 0;
                    dy = 0;
                }
            }
            else
            {
                if (!Mouse.isVisible)
                {
                    Mouse.Showcursor();
                }
            }

            p.X = (int)(p.X + dx);
            p.Y = (int)(p.Y + dy);

            Mouse.SetCursorPosition(p);
   
        }

        

        public void MovePicToXY(ref PictureBox pic, int x, int y)
        {
            float dx = 0, dy = 0;
            float pic_center_x, pic_center_y;

            pic_center_x = pic.Left - pic.Width / 2;
            pic_center_y = pic.Top - pic.Height / 2;

            if (pic_center_x < 0) pic_center_x = 0;
            if (pic_center_y < 0) pic_center_y = 0;

            if (x < 0) x = 0;
            if (y < 0) y = 0;

            dx = (float)x - pic_center_x;
            dy = (float)y - pic_center_y;

            float abs_dx;
            float abs_dy;

            if (dx > 0)
                abs_dx = dx;
            else
                abs_dx = (-dx);

            if (dy > 0)
                abs_dy = dy;
            else
                abs_dy = (-dy);

            if (Properties.Settings.Default.MinHorizErrorThrsh < abs_dx && abs_dx < Properties.Settings.Default.MaxHorizErrorThrsh)
                dx = 0;

            if (Properties.Settings.Default.MinVertErrorThrsh < abs_dy && abs_dy < Properties.Settings.Default.MaxVertErrorThrsh)
                dy = 0;

            Console.WriteLine(innercounter.ToString());
            if (abs_dx >= Properties.Settings.Default.MaxHorizErrorThrsh && abs_dy >= Properties.Settings.Default.MaxVertErrorThrsh)
            {
                if (!pic.Visible)
                {
                    pic.Visible = true;
                    pic.BringToFront();
                }

                dx /= this.ScreenInfo.CameraFPS * 3;
                dy /= this.ScreenInfo.CameraFPS * 3;

                pic.Left = (int)(pic.Left + dx);
                pic.Top = (int)(pic.Top + dy);

                if (pic.Left < 0) pic.Left = 0;
                if (pic.Top < 0) pic.Top = 0;

                innercounter=0;
            }
            else
            {
                if (pic.Visible)
                {
                    innercounter++;
                    if (innercounter >= this.ScreenInfo.CameraFPS)
                        pic.Visible = false;

                    if (innercounter >= int.MaxValue)
                        innercounter = 0;
                }
            }
        }
        
        private void speech_SpeechRecorded(object sender, RecordedEventArgs e)
        {
            
            switch (e.Phrase){


                case "double click":
                    this.UserInfo.ForceClickDown = false;
                    this.UseMouseButton = MouseButtons.Left;

                    this.UserInfo.MouthClickDown = true;

                    Mouse.MouseEvent(Mouse.MouseEventFlags.LeftDown);
                    Mouse.MouseEvent(Mouse.MouseEventFlags.LeftUp);
                    Mouse.MouseEvent(Mouse.MouseEventFlags.LeftDown);
                    Mouse.MouseEvent(Mouse.MouseEventFlags.LeftUp);


                    this.UserInfo.MouthClickDown = false;

                    break;                
                case "click":
                    this.UserInfo.ForceClickDown = false;
                    this.UserInfo.MouthClickDown = true;

                    if (this.UseMouseButton == MouseButtons.Left)
                    {
                        Mouse.MouseEvent(Mouse.MouseEventFlags.LeftDown);
                        Mouse.MouseEvent(Mouse.MouseEventFlags.LeftUp);
                    }
                    else
                    {
                        Mouse.MouseEvent(Mouse.MouseEventFlags.RightDown);
                        Mouse.MouseEvent(Mouse.MouseEventFlags.RightUp);
                    }
                    this.UserInfo.MouthClickDown = false;

                    break;

                case "drag":
                    this.UseMouseButton = MouseButtons.Left;
                    Mouse.MouseEvent(Mouse.MouseEventFlags.LeftDown);
                    this.UserInfo.MouthClickDown = true;
                    this.UserInfo.ForceClickDown = true;

                    break;

                case "click down":
                    this.UserInfo.MouthClickDown = true;

                    if (this.UseMouseButton == MouseButtons.Left)
                        Mouse.MouseEvent(Mouse.MouseEventFlags.LeftDown);
                    else
                        Mouse.MouseEvent(Mouse.MouseEventFlags.RightDown);

                    this.UserInfo.ForceClickDown = true;
                    break;

                case "drop":
                case "click up":
                    this.UserInfo.MouthClickDown = false;
                    
                    if (this.UseMouseButton == MouseButtons.Left)
                        Mouse.MouseEvent(Mouse.MouseEventFlags.LeftUp);
                    else
                        Mouse.MouseEvent(Mouse.MouseEventFlags.RightUp);

                    this.UserInfo.ForceClickDown = false;
                    break;

                case "right button":
                    this.UserInfo.ForceClickDown = false;
                    if (this.UseMouseButton == MouseButtons.Left)
                    {
                        this.UseMouseButton = MouseButtons.Right;
                        FormMain frm = (FormMain) this.MainForm();
                        frm.UpdateButtonClickText("Right Click");
                    }
                    break;

                case "right click":
                    this.UserInfo.ForceClickDown = false;
                    if (this.UseMouseButton == MouseButtons.Left)
                    {
                        this.UseMouseButton = MouseButtons.Right;
                        FormMain frm = (FormMain)this.MainForm();
                        frm.UpdateButtonClickText("Right Click");
                    }

                    this.UserInfo.MouthClickDown = true;
                    Mouse.MouseEvent(Mouse.MouseEventFlags.RightDown);

                    this.UserInfo.MouthClickDown = false;
                    Mouse.MouseEvent(Mouse.MouseEventFlags.RightUp);

                    break;

                case "left button":
                    this.UserInfo.ForceClickDown = false;
                    if (this.UseMouseButton == MouseButtons.Right)
                    {
                        this.UseMouseButton = MouseButtons.Left;
                        FormMain frm = (FormMain)this.MainForm();
                        frm.UpdateButtonClickText("Left Click");
                    }
                    break;

                case "left click":
                    this.UserInfo.ForceClickDown = false;
                    if (this.UseMouseButton == MouseButtons.Right)
                    {
                        this.UseMouseButton = MouseButtons.Left;
                        FormMain frm = (FormMain)this.MainForm();
                        frm.UpdateButtonClickText("Left Click");
                    }

                    this.UserInfo.MouthClickDown = true;
                    Mouse.MouseEvent(Mouse.MouseEventFlags.LeftDown);

                    this.UserInfo.MouthClickDown = false;
                    Mouse.MouseEvent(Mouse.MouseEventFlags.LeftUp);

                    break;

            }
        }
    }
}
