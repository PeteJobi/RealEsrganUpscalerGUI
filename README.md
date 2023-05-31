# Real-ESRGAN Upscaler GUI
This repo provides a simple GUI for upscaling images and videos, based on [Real-ESRGAN ncnn Vulkan](https://github.com/xinntao/Real-ESRGAN-ncnn-vulkan), with features for pausing/canceling and performing bulk upscale operations on multiple files or folder contents.

![image](https://github.com/PeteJobi/RealEsrganUpscalerGUI/assets/45200292/e668a5d2-df4e-480c-99c6-83eba314f91e)
![image](https://github.com/PeteJobi/RealEsrganUpscalerGUI/assets/45200292/1092c8f1-e920-4750-bd9e-bea20e193bb2)


## How to build
You need to have DotNet 6 runtime installed to build or run the software. Download the latest runtime [here](https://dotnet.microsoft.com/en-us/download). If you're not sure which one to download, try [.NET 6.0 Version 6.0.16](https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/sdk-6.0.408-windows-x64-installer)

In the project folder, run the below
```
dotnet publish -p:PublishSingleFile=true -r win-x64 -c Release --self-contained false
```
When that completes, go to `\bin\Release\net6.0-windows\win-x64\publish` and you'll find the **Upscaler.exe** amongst other files. All the files are necessary for the software to run properly. Run **Upscaler.exe** to use the software.

You can also just download the release build, which contains all published files.

## How to use
The file types supported are ".mkv", ".mp4", ".jpg", ".jpeg" and ".png". The software decides automatically if the file to be upscaled is an image or a video.

Two options are available depending on the type of media you want to upscale. Use **Animation** for anime or cartoon or anything painted. Use **Realistic** for everything else.

For animation videos, you can decide the upscale quality to use. **X2** means the video will be upscaled to twice its resolution and **X4** means 4 times its original resolution. The higher the quality, the longer it will take to complete. For realistic videos, as well as images of both types, it's always X4.

When you're done with the parameters, click **Select files** to select multiple files to be upscaled or **Select folder** to upscale every image and video in that folder. You can also drag and drop files into the window.

NOTE: Depending on your GPU and CPU, upscaling large videos can take very long.

## Known issues
If your resolution scale is higher than 100%, the software may not render correctly and some UI controls may be out of view. There is no real fix for this. It's a limitation of Windows Forms.
