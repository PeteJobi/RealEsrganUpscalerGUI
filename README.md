# Real-ESRGAN Upscaler GUI
This repo provides a simple GUI for upscaling images and videos, based on [Real-ESRGAN ncnn Vulkan](https://github.com/xinntao/Real-ESRGAN-ncnn-vulkan), with features for pausing/canceling and performing bulk upscale operations on multiple files or folder contents. Only supports Windows 10 and 11 (not tested on other versions of Windows).

![image](https://github.com/PeteJobi/RealEsrganUpscalerGUI/assets/45200292/64a9fe61-c75f-47ba-bebb-a45e815dc2fe)
![image](https://github.com/PeteJobi/RealEsrganUpscalerGUI/assets/45200292/0be83e9a-5039-4a10-88a6-1ae94db904ab)


## How to build
You need to have at least .NET 6 runtime installed to build the software. Download the latest runtime [here](https://dotnet.microsoft.com/en-us/download). If you're not sure which one to download, try [.NET 6.0 Version 6.0.16](https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/sdk-6.0.408-windows-x64-installer)

In the project folder, run the below
```
dotnet publish -p:PublishSingleFile=true -r win-x64 -c Release --self-contained false
```
When that completes, go to `\bin\Release\net<version>-windows\win-x64\publish` and you'll find the **Upscaler.exe** amongst other files. All the files are necessary for the software to run properly. Run **Upscaler.exe** to use the software.

## Run without building
You can also just download the release builds if you don't wish to build manually. The assets release contains the assets used by the software. The standard release contains the compiled executable. Download them both, extract the assets to a folder and drop the executable in that folder.

If you wish to run the software without installing the required .NET runtime, download the self-contained release.

## How to use
The file types supported are ".mkv", ".mp4", ".jpg", ".jpeg" and ".png". The software decides automatically if the file to be upscaled is an image or a video.

Two options are available depending on the type of media you want to upscale. Use **Animation** for anime or cartoon or anything painted. Use **Realistic** for everything else.

You can decide the upscale quality to use. **X2** means the image/video will be upscaled to twice its resolution and **X4** means 4 times its original resolution. For animation videos, the higher the quality, the longer it will take to complete. Note that for realistic videos, as well as images of both types, a X4 scale is always used, and the image/video is downscaled to the resolution corresponding to the selected upscale quality. This means X2 takes the same amount of time and resources as X4 in those cases. Only animation videos truly have the 3 different levels of upscale quality. 

When you're done with the parameters, click **Select files** to select multiple files to be upscaled or **Select folder** to upscale every image and video in that folder. You can also drag and drop files into the window.

**NOTE**: Depending on your GPU and CPU, upscaling large videos can take very long. Video upscaling progress can be saved to be resumed after closing the app. If your PC shuts down while you're upscaling a very large video, worry not. The process will continue where you left of if you try to upscale the video again. Just be sure to not delete the generated files.
