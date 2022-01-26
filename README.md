# DevExpress WinUI 3 Demos

This repository contains WinUI 3 demos that show how to use [DevExpress WinUI components](https://www.devexpress.com/winui/) and tools, including the Data Grid, Reporting, Scheduler, Charting, and Data Editors.

**Tip:** You can also download the [DevExpress WinUI Demos App](https://demos.devexpress.com/winui/). This app allows you to execute all WinUI 3 demos without compiling the code.

![](/images/demos-winui.png)

## System Requirements

* Windows 10, version 1809 (build 17763), or newer installed
* Visual Studio 2019 version 16.9 or higher, or Visual Studio 2019 Preview version 16.10 or higher
* .NET Desktop development workload for Visual Studio
* Windows App SDK Version 1.0 Visual Studio extension installed

For more information, refer to [Windows App SDK (formerly known as Project Reunion)](https://docs.microsoft.com/en-us/windows/apps/windows-app-sdk/downloads/).

## Run Demos

***Important Note: DevExpress Reports is not included in the DevExpress WinUI component distribution. To build this demo application, you must own an active DevExpress Subscription (Reporting, DXperience, or Universal) or trial NuGet packages. For pricing information, please refer to the [DevExpress Products matrix](https://www.devexpress.com/subscriptions/reporting/#Pricing). To get more information on the free trial DevExpress Subscription NuGet packages, please refer to the [Trial .NET Controls Via NuGet](https://docs.devexpress.com/GeneralInformation/120534/installation/install-devexpress-controls-using-nuget-packages/net-core-controls-via-nuget) documentation topic.***

1. Open the demo project.

2. Use your DevExpress credentials to log into [devexpress.com](https://devexpress.com/). Refer to the [devexpress.com/winui-free](https://devexpress.com/winui-free) page to get your DevExpress WinUI Controls license. Check the specified email address and copy the [NuGet feed URL](xref:116042) from the email to the clipboard.

3. In Visual Studio, select **Tools -> NuGet Package Manager -> Package Manager Settings**.

    ![](/images/package-manager-settings.png)

4. Navigate to **NuGet Package Manager -> Package Sources**. Click the plus sign at the top right corner to add a new NuGet package source. Use the following package settings:

    * Name - **DevExpress**

    * Source - the obtained NuGet Feed URL (`https://nuget.devexpress.com/{your feed authorization key}/api`)

    Make sure the **nuget.org** package source is also enabled. Click **OK**.
    
    ![](/images/package-sources.png)

5. Build the demo project. All the required NuGet packages will be automatically retrieved by Visual Studio.

    If you cannot build the demo, check whether the correct configuration is specified (x86 or x64). Then execute **Rebuild Solution**.


## Documentation

[DevExpress WinUI Controls](https://docs.devexpress.com/WinUI/402541/winui-controls)

## Feedback

Your input can help us shape future product offerings for WinUI. Feel free to contact us at wpfteam@devexpress.com.

## Copyright

Developer Express Inc. All rights reserved.
