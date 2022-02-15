using DirectShowLib;
using Emgu.CV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Recodo.Desktop.Logic
{
    public class CameraService
    {
        private static CameraService instance;

        private VideoCapture capture = null;    // храним захватываемые данные с видеокамеры
        private DsDevice[] webCams = null;
        public int selectedCameraId = -1;

        public bool Capturing { get; private set; }

        public event EventHandler ImageGrabbed;

        public delegate void SelectionChanged();

        public event SelectionChanged Notify;

        private CameraService()
        {
            CvInvoke.UseOpenCL = false;
        }

        public static CameraService GetInstance()
        {
            if (instance == null)
            {
                instance = new CameraService();
            }

            return instance;
        }

        public void StartCapture(int selectedCameraId)
        {
            Capturing = true;
            capture = new VideoCapture(selectedCameraId);
            capture.ImageGrabbed += ImageGrabbed;
            capture.Start();
        }

        public void StopCapture()
        {
            if (Capturing)
            {
                capture.ImageGrabbed -= ImageGrabbed;
                Notify.Invoke();
                capture.Pause();
                capture.Stop();
                capture.Dispose();
                capture = null;
                Capturing = false;
            }
        }

        public ICollection<string> GetCameras()
        {
            webCams = DsDevice.GetDevicesOfCat(FilterCategory.VideoInputDevice); 
            List<string> webCamsNames = new List<string>();
            for (int i = 0; i < webCams.Length; i++)
            {
                webCamsNames.Add(webCams[i].Name);
            }
            return webCamsNames;
        }

        public bool Retrieve(Mat m)
        {
            lock (new object())
            {
                if (capture != null)
                {
                    return capture.Retrieve(m);
                }
                
                return false;

            }
        }
    
    }
}
