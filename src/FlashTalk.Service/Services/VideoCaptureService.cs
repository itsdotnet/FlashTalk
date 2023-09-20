
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using OpenCvSharp;
using OpenCvSharp.CPlusPlus;
using OpenCvSharp.Extensions;
namespace FlashTalk.Service.Services;

public class VideoCaptureService
{
    private CvVideoWriter capture;
    private Mat frame;

    private List<byte[]> frameBuffer = new List<byte[]>();
    private SemaphoreSlim bufferLock = new SemaphoreSlim(1);

    private CancellationTokenSource cancellationTokenSource;


    public void StartCapture()
    {
        cancellationTokenSource = new CancellationTokenSource();
        
        Task.Run(async () =>
        {
            while (!cancellationTokenSource.Token.IsCancellationRequested)
            {
                await CaptureFrameAsync();
            }
        }, cancellationTokenSource.Token);
    }

    public void StopCapture()
    {
        cancellationTokenSource?.Cancel();
        capture?.Release();
        frame?.Dispose();
    }

    public async Task<byte[]> GetLatestFrameAsync()
    {
        await bufferLock.WaitAsync();
        try
        {
            if (frameBuffer.Count > 0)
            {
                var latestFrame = frameBuffer[frameBuffer.Count - 1];
                frameBuffer.Clear();
                return latestFrame;
            }
        }
        finally
        {
            bufferLock.Release();
        }

        return null;
    }

    private async Task CaptureFrameAsync()
    {
        if (capture != null && capture.IsOpened())
        {
            if (capture.Read(frame))
            {
                await bufferLock.WaitAsync();
                try
                {
                    frameBuffer.Add(frame.ToBytes(".jpg"));
                }
                finally
                {
                    bufferLock.Release();
                }
            }
        }
        else
        {
            // Handle camera not available or other errors.
            throw new ApplicationException("Camera not available or an error occurred.");
        }
    }
}
