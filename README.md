# CAL-Tutor

![Example Image](readme_assets/CAL-Tutor_graphical_workflow_overview.png)

## Research article
| Our MDPI Journal of Imaging paper can be found here: |
|--------|
|[CAL-Tutor: A HoloLens 2 Application for Training in Obstetric Sonography and User Motion Data Recording](https://www.mdpi.com/2313-433X/9/1/6)|

##  Project description: 

The CAL-Tutor project is a Unity game engine application designed for the Microsoft HoloLens 2 that can be used in the context of medical education aplied to obstetrics ultrasound. 

The application uses the Mixed Reality Toolkit (MRTK) (https://github.com/microsoft/MixedRealityToolkit-Unity) in order to create mixed reality objects and user interactions. 

The provided holographic models are: 
- Baby incl. mother's abdomen (based on a 3D reconstruction of a phantom that is being used for medical education)<br>
- Voluson E10 ultrasound probe (GE Healthcare, Chicago, USA)
- A holographic menu with several options

The application workflow consists of the following steps: 

1.) Manual alignment of the baby model to the physical phantom
2.) Navigation of the tracked ultrasound probe to 3 pre-defined standard ultrasound planes (placed at respective anatomical locations of the baby model)

The Clarius ultrasound probe is being tracked using QR codes that have to be printed and attached to the physical probe. 
The used tracking software is HoloLensArToolkit (https://github.com/qian256/HoloLensARToolKit).

During the navigation to the standard planes, holographic instruction cards can be displayed that describe the characteristics of the target anatomy. 

During the navigation phase user motion data is being recorded that can be downloaded via the HoloLens device portal.

## Demo video:
Click on the image below to see our CAL-Tutor demo video on youtube:
[![Watch the video](/readme_assets/CAL-Tutor_demo_video_navigation_screenshot.png)](https://youtu.be/g0X4uLhCjoI)

## Installation on a Windows computer:
|Please follow the steps below to open the CAL-Tutor project in the Unity editor and deploy it to your HoloLens 2 device| 
|----------|
|1.) Install Unity Hub via the official Unity website: https://unity.com/download|
|2.) Open Unity Hub, click on 'Installs' tab and install a Unity version whose version number starts with 2020.3. This is an older Unity project that has been used with a Unity version from 2020. Newer Unity versions may work as well, but might cause exceptions related to changes in the version-specific Unity APIs.|
|3.) Install Visual Studio (VS) (Unity 2020.3 may work best with Visual Studio 2019, but newer VS versions may work as well). During the Unity installation you will probably be prompted to install a VS version as well, otherwise download it from https://visualstudio.microsoft.com/|
|4.) Clone CAL-Tutor project (for example via the Visual Studio editor).|
|5.) Open Unity Hub, click on 'Projects', then click on the dropdown menu arrow next to "Add*, and select "Add project from disk", navigate to the 'CAL-Tutor' folder of your cloned repo and select this folder. Now the CAL-Tutor project should have been added to your Unity projects in Unity Hub. Make sure to select Unity editor version 2020.3 if you have multiple Unity versions installed. Then click on your newly added CAL-Tutor project in Unity Hub to open it in the Unity editor.|
|6.) Build the Unity project: Follow instructions on the official Microsoft website: https://learn.microsoft.com/en-us/windows/mixed-reality/develop/unity/build-and-deploy-to-hololens and make sure that the 'CAL-Tutor' Unity scene is selected after clicking on File --> Build Settings. |
|7.) If the build was successfull, navigate to your folder containing the build results and open the VS solution 'CAL-Tutor.sln'. Now you have multiple options to deploy CAL-Tutor to your HoloLens 2. Please follow the instructions of the Microsoft website: https://learn.microsoft.com/en-us/windows/mixed-reality/develop/advanced-concepts/using-visual-studio?tabs=hl2|

## Citation

If you would like to reference this work in your research, please use the following:

```bibtex
@article{birlo2022cal,
  title={CAL-tutor: A HoloLens 2 application for training in obstetric sonography and user motion data recording},
  author={Birlo, Manuel and Edwards, Philip J Eddie and Yoo, Soojeong and Dromey, Brian and Vasconcelos, Francisco and Clarkson, Matthew J and Stoyanov, Danail},
  journal={Journal of Imaging},
  volume={9},
  number={1},
  pages={6},
  year={2022},
  publisher={MDPI}
}
