# DevExpress WinUI 3 Demos

This repository contains WinUI 3 demos that show how to use WinUI components and tools, including the Data Grid, Scheduler, Charting, and Data Editors.

<img src="./images/winui-demos.png">

## System Requirements

* Windows 10, version 2004 (build 19041), or newer installed
* Visual Studio 2019, version 16.9 (including Universal Windows Platform development and .NET Desktop development)
* If you previously installed the [WinUI 3 Preview extension](https://marketplace.visualstudio.com/items?itemName=Microsoft-WinUI.WinUIProjectTemplates) from an earlier WinUI 3 preview release, uninstall the extension.
* [Project Reunion Visual Studio extension](https://marketplace.visualstudio.com/items?itemName=ProjectReunion.MicrosoftProjectReunion)

For more information, refer to [Project Reunion](https://docs.microsoft.com/en-us/windows/apps/project-reunion/).

## Run Demos

1. Open the demo project.

2. Use your DevExpress credentials to log into [nuget.devexpress.com](nuget.devexpress.com). Obtain your **NuGet Feed URL** and copy it to the clipboard.

    <img src="./images/winui-nuget-gallery.png">

3. In Visual Studio, select **Tools -> NuGet Package Manager -> Package Manager Settings**.

    <img src="./images/package-manager-settings.png">

4. Navigate to **NuGet Package Manager -> Package Sources**. Click the plus sign at the top right corner to add a new NuGet package source. Use the following package settings:

    * Name - **DevExpress**

    * Source - the obtained NuGet Feed URL (`https://nuget.devexpress.com/{your feed authorization key}/api`)

    Make sure the **nuget.org** package source is also enabled. Click **OK**.
    
    <img src="./images/package-sources.png">

5. Build the demo project. All the required NuGet packages will be automatically retrieved by Visual Studio.

## Demos Application

[DevExpress WinUI Demos App](https://demos.devexpress.com/winui/)

## Documentation

[DevExpress WinUI Controls](https://docs.devexpress.com/WinUI/402541/winui-controls)

## Feedback

Your input can help us shape future product offerings for WinUI. Feel free to contact us at wpfteam@devexpress.com.

## Copyright

Developer Express Inc. All rights reserved.
