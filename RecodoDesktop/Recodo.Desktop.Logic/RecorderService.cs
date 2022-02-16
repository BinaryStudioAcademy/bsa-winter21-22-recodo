using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ScreenRecorderLib;

namespace Recodo.Desktop.Logic
{
    public class RecorderService
    {
        private Recorder _rec;
        private RecorderConfiguration configuration = new RecorderConfiguration();

        public void CreateRecording()
        {
            string videoPath = Path.Combine(Path.GetTempPath(), "test.mp4");
            _rec = Recorder.CreateRecorder(configuration.GetOptions());
            _rec.OnRecordingComplete += Rec_OnRecordingComplete;
            _rec.OnRecordingFailed += Rec_OnRecordingFailed;
            _rec.OnStatusChanged += Rec_OnStatusChanged;
            //Record to a file
            videoPath = Path.Combine(Path.GetTempPath(), "test.mp4");
            _rec.Record(videoPath);
        }

        public void EndRecording()
        {
            _rec.Stop();
        }

        public List<AudioDevice> GetInputAudioDevices()
        {
            return Recorder.GetSystemAudioDevices(AudioDeviceSource.InputDevices);
        }

        public List<AudioDevice> GetOutputAudioDevices()
        {
            return Recorder.GetSystemAudioDevices(AudioDeviceSource.OutputDevices);
        }

        public List<RecordingSourceBase> GetDisplays()
        {
            var sources = new List<RecordingSourceBase>();
            sources.AddRange(Recorder.GetDisplays());

            return sources;
        }

        public List<RecordingSourceBase> GetWindows()
        {
            var sources = new List<RecordingSourceBase>();
            sources.AddRange(Recorder.GetWindows());

            return sources;
        }

        public List<RecordingSourceBase> GetCameras()
        {
            var sources = new List<RecordingSourceBase>();
            sources.AddRange(Recorder.GetSystemVideoCaptureDevices());

            return sources;
        }

        private void Rec_OnRecordingComplete(object sender, RecordingCompleteEventArgs e)
        {
            //Get the file path if recorded to a file
            string path = e.FilePath;
        }
        private void Rec_OnRecordingFailed(object sender, RecordingFailedEventArgs e)
        {
            string error = e.Error;
        }
        private void Rec_OnStatusChanged(object sender, RecordingStatusEventArgs e)
        {
            RecorderStatus status = e.Status;
        }


    }
}
