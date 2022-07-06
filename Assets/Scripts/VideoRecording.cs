
//using UnityEngine;
//using Unity.Collections;
//using System;
//using System.Linq;
//using System.Collections.Generic;
//using System.IO;

//using UnityEditor.Media;

//public static class VideoRecording
//{
//    public static void RecordVideo(List<Texture2D> artoolkitVideoFrameTextures, string videoName)
//    {
//        if (!artoolkitVideoFrameTextures.Any())
//        {
//            throw new ArgumentException("Input frames cannot be empty");
//        }

//        var firstFrame = artoolkitVideoFrameTextures.First();
//        uint width = (uint)firstFrame.width;
//        uint height = (uint)firstFrame.height;

//        //var fFMPEGImagesToVideo = new FFMPEGImagesToVideo();
//        //fFMPEGImagesToVideo.FFMPEGConvertImagesToVideo();

//         //var audioAttr = new AudioTrackAttributes
//         //{
//         //    sampleRate = new MediaRational(48000),
//         //    channelCount = 2,
//         //    language = "en"
//         //};

//        //int sampleFramesPerVideoFrame = audioAttr.channelCount *
//        //    audioAttr.sampleRate.numerator / videoAttr.frameRate.numerator;

//        var encodedFilePath = Path.Combine(Path.GetTempPath(), videoName);

//        //Texture2D tex = new Texture2D((int)videoAttr.width, (int)videoAttr.height, TextureFormat.RGBA32, false);

//        var videoAttr = new VideoTrackAttributes
//        {
//            frameRate = new MediaRational(50),
//            width = width,
//            height = height,
//            includeAlpha = false
//        };

//        using (var encoder = new MediaEncoder(encodedFilePath, videoAttr))//, audioAttr)) We don't need audio here.
//        //using (var audioBuffer = new NativeArray<float>(sampleFramesPerVideoFrame, Allocator.Temp))
//        {
//            foreach (var artoolkitVideoFrameTexture in artoolkitVideoFrameTextures)
//            {
//                // Fill 'tex' with the video content to be encoded into the file for this frame.
//                // ...
//                encoder.AddFrame(artoolkitVideoFrameTexture);

//                // Fill 'audioBuffer' with the audio content to be encoded into the file for this frame.
//                // ...
//                //encoder.AddSamples(audioBuffer);
//            }
//        }
//    }
//}
