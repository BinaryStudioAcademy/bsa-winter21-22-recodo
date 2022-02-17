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
        private static CameraService _instance;

        private VideoCapture _capture;
        private DsDevice[] _webCams;

        public bool isCapturing { get; private set; }

        public event EventHandler ImageGrabbed;

        public delegate void SelectionChanged();

        public event SelectionChanged Notify;

        private CameraService()
        {
            CvInvoke.UseOpenCL = false;
        }

        public static CameraService GetInstance()
        {
            return _instance ??= new CameraService();
        }

        public void StartCapture(int selectedCameraId)
        {
            isCapturing = true;
            _capture ??= new VideoCapture(selectedCameraId, VideoCapture.API.DShow);

            if (_capture == null)
            {
                return;
            }

            _capture.ImageGrabbed += ImageGrabbed;
            _capture.Start();
        }

        public void StopCapture()
        {
            if (isCapturing)
            {
                _capture.ImageGrabbed -= ImageGrabbed;
                Notify.Invoke();
                _capture.Pause();
                _capture.Stop();
                _capture.Dispose();
                _capture = null;
                isCapturing = false;
            }
        }

        public ICollection<string> GetCameras()
        {
            _webCams = DsDevice.GetDevicesOfCat(FilterCategory.VideoInputDevice); 
            List<string> webCamsNames = _webCams.Select(el => el.Name).ToList();
            return webCamsNames;
        }

        public bool Retrieve(Mat m)
        {
            lock (new object())
            {
                if (_capture != null)
                {
                    return _capture.Retrieve(m);
                }         
                return false;
            }
        }

        public Mat QueryFrame()
        {
            if (_capture != null)
            {
                return _capture.QueryFrame();
            }
            return null;
        }
    
    }
}
