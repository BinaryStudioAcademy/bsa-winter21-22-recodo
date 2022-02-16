
using ScreenRecorderLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recodo.Desktop.Logic
{
    public class RecorderConfiguration
    {
        public RecorderOptions GetOptions()
        {
            RecorderOptions options = new RecorderOptions
            {
                SourceOptions = new SourceOptions
                {
                    //Populate and pass a list of recordingsources.
                    RecordingSources = new List<RecordingSourceBase>()
                },
                OutputOptions = new OutputOptions
                {
                    RecorderMode = RecorderMode.Video,
                    //This sets a custom size of the video output, in pixels.
                    OutputFrameSize = new ScreenSize(1920, 1080),
                    //Stretch controls how the resizing is done, if the new aspect ratio differs.
                    Stretch = StretchMode.Uniform,
                    //SourceRect allows you to crop the output.
                    SourceRect = new ScreenRect(100, 100, 500, 500)
                },
                AudioOptions = new AudioOptions
                {
                    Bitrate = AudioBitrate.bitrate_128kbps,
                    Channels = AudioChannels.Stereo,
                    IsAudioEnabled = true,
                },
                VideoEncoderOptions = new VideoEncoderOptions
                {
                    Bitrate = 8000 * 1000,
                    Framerate = 60,
                    IsFixedFramerate = true,
                    //Currently supported are H264VideoEncoder and H265VideoEncoder
                    Encoder = new H264VideoEncoder
                    {
                        BitrateMode = H264BitrateControlMode.CBR,
                        EncoderProfile = H264Profile.Main,
                    },
                    //Fragmented Mp4 allows playback to start at arbitrary positions inside a video stream,
                    //instead of requiring to read the headers at the start of the stream.
                    IsFragmentedMp4Enabled = true,
                    //If throttling is disabled, out of memory exceptions may eventually crash the program,
                    //depending on encoder settings and system specifications.
                    IsThrottlingDisabled = false,
                    //Hardware encoding is enabled by default.
                    IsHardwareEncodingEnabled = true,
                    //Low latency mode provides faster encoding, but can reduce quality.
                    IsLowLatencyEnabled = false,
                    //Fast start writes the mp4 header at the beginning of the file, to facilitate streaming.
                    IsMp4FastStartEnabled = false
                },
                MouseOptions = new MouseOptions
                {
                    //Displays a colored dot under the mouse cursor when the left mouse button is pressed.	
                    IsMouseClicksDetected = true,
                    MouseLeftClickDetectionColor = "#FFFF00",
                    MouseRightClickDetectionColor = "#FFFF00",
                    MouseClickDetectionRadius = 30,
                    MouseClickDetectionDuration = 100,
                    IsMousePointerEnabled = true,
                    /* Polling checks every millisecond if a mouse button is pressed.
                       Hook is more accurate, but may affect mouse performance as every mouse update must be processed.*/
                    MouseClickDetectionMode = MouseDetectionMode.Hook
                },
                OverlayOptions = new OverLayOptions
                {
                    //Populate and pass a list of recording overlays.
                    Overlays = new List<RecordingOverlayBase>()
                },
                SnapshotOptions = new SnapshotOptions
                {
                    //Take a snapshot of the video output at the given interval
                    SnapshotsWithVideo = false,
                    SnapshotsIntervalMillis = 1000,
                    SnapshotFormat = ImageFormat.PNG,
                    //Optional path to the directory to store snapshots in
                    //If not configured, snapshots are stored in the same folder as video output.
                    SnapshotsDirectory = ""
                },
                LogOptions = new LogOptions
                {
                    //This enabled logging in release builds.
                    IsLogEnabled = true,
                    //If this path is configured, logs are redirected to this file.
                    LogFilePath = "recorder.log",
                    LogSeverityLevel = ScreenRecorderLib.LogLevel.Debug
                }
            };
            return options;
        }
        

    }
}
