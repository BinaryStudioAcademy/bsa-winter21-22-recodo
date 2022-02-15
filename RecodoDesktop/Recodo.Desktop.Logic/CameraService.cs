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
                instance = new CameraService();
            return instance;
        }

        public void StartCapture(int selectedCameraId)
        {
            capture = new VideoCapture(selectedCameraId);
            capture.ImageGrabbed += ImageGrabbed;
            capture.Start();
            
        }

        public void StopCapture()
        {
            
            capture.ImageGrabbed -= ImageGrabbed;
            Notify.Invoke();
            capture.Stop();
            capture.Dispose();
            capture = null;

        }

        public ICollection<string> GetCameras()
        {
            webCams = DsDevice.GetDevicesOfCat(FilterCategory.VideoInputDevice);    // получаем все доступные видеокамеры
            List<string> webCamsNames = new List<string>();
            for (int i = 0; i < webCams.Length; i++)
            {
                webCamsNames.Add(webCams[i].Name);
            }

            return webCamsNames;
        }

        public bool Retrieve(Mat m)
        {
            if (capture != null)
            {
                return capture.Retrieve(m);
            }
            else
            {
                return false;
            }
        }
    }
}
