using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using ScreenRecorderLib;

namespace Recodo.Desktop.Logic
{
    public class RecorderService
    {
        private RecorderConfiguration _options;
        public void Configure(RecorderConfiguration options)
        {
            _options = options;
        }

        private Recorder recorder;
        public event Action StartRec;

        public void StartRecording()
        {
            List<RecordingSourceBase> source = new List<RecordingSourceBase>();
            source.Add(Recorder.GetWindows().FirstOrDefault(w => w.Title == _options.RecorderWindowTitle));

            var opts = new RecorderOptions
            {
                AudioOptions = new AudioOptions
                {
                    AudioInputDevice = Recorder.GetSystemAudioDevices(AudioDeviceSource.InputDevices)
                    .FirstOrDefault(d => d.FriendlyName == _options.SelectedAudioInputDevice)?
                    .DeviceName,

                    AudioOutputDevice = Recorder.GetSystemAudioDevices(AudioDeviceSource.OutputDevices)
                    .FirstOrDefault(d => d.FriendlyName == _options.SelectedAudioOutputDevice)?
                    .DeviceName,

                    IsAudioEnabled = _options.IsAudioEnable,
                    IsInputDeviceEnabled = true,
                    IsOutputDeviceEnabled = true,
                },
                SourceOptions = source.FirstOrDefault() == null ? SourceOptions.MainMonitor : new SourceOptions { RecordingSources = source }
            };

            recorder = Recorder.CreateRecorder(opts);
            recorder.OnRecordingFailed += Rec_OnRecordingFailed;
            recorder.OnRecordingComplete += Rec_OnRecordingComplete;
            recorder.OnStatusChanged += Rec_OnStatusChanged;
            recorder.Record(Path.ChangeExtension(Path.GetTempFileName(), ".mp4"));

            StartRec?.Invoke();
        }

        public void PauseRecording()
        {
            if(recorder?.Status == RecorderStatus.Paused)
            {
                recorder?.Resume();
            }
            else
            {
                recorder?.Pause();
            }
            
        }

        public void StopRecording()
        {
            recorder?.Stop();
        }

        public List<string> GetInputAudioDevices()
        {
            return Recorder.GetSystemAudioDevices(AudioDeviceSource.InputDevices)
                ?.Select(d=>d.FriendlyName)
                ?.ToList();
        }

        public List<string> GetOutputAudioDevices()
        {
            return Recorder.GetSystemAudioDevices(AudioDeviceSource.OutputDevices)
                ?.Select(d => d.FriendlyName)
                ?.ToList();
        }

        public List<RecordingSourceBase> GetDisplays()
        {
            var sources = new List<RecordingSourceBase>();
            sources.AddRange(Recorder.GetDisplays());

            return sources;
        }

        public List<string> GetWindows()
        { 
            return Recorder.GetWindows().Select(w => w.Title).ToList();
        }

        public List<RecordingSourceBase> GetCameras()
        {
            var sources = new List<RecordingSourceBase>();
            sources.AddRange(Recorder.GetSystemVideoCaptureDevices());

            return sources;
        }

        private static void Rec_OnStatusChanged(object sender, RecordingStatusEventArgs e)
        {
            switch (e.Status)
            {
                case RecorderStatus.Recording:
                    break;
                case RecorderStatus.Paused:
                    break;
                case RecorderStatus.Finishing:
                    break;
                default:
                    break;
            }
        }

        private static void Rec_OnRecordingComplete(object sender, RecordingCompleteEventArgs e)
        {
            string filePath = e.FilePath;
        }

        private static void Rec_OnRecordingFailed(object sender, RecordingFailedEventArgs e)
        {
            string error = e.Error;
        }
    }
}
