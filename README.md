# OutlookCO2
\- Do you know which is the current CO2 atmospheric concentration level?  

\- Did you know science advisers on the Intergovernmental Panel on Climate Change have estimated a CO2 concentration of no more than 450 parts per million for a 2 degrees temperature raise and 430 ppm for 1.5 degrees?  

\- Would you like to track these levels and who knows, even raise awareness about it by sharing the data on your email signature?  

If you think you'd like to give it a try this tool might help you – it updates your *Microsoft Outlook (\*)* HTML signature with up to date CO2 metrics as recorded in Mauna Loa Observatory.  

A ready to execute x64 .exe is available for **download here**: http://bit.ly/OutlookCO2  

**Important:** If Windows don't let you run the app please check OutlookCO2.exe properties and make sure the **Unblock** check box is ticked. This is a Windows security feature for unsigned downloaded files.  

You can verify the integrity of the .zip file against the following hash codes:  
```
CRC-32: FE278474
SHA-1: 77C0910AB9EBBE10BEFC39D9D03A963206FFF798
SHA-256: 6DA5B7F92F0D9B460BD53D97CF134E3EE10E437C2E6563675BF820111BD6BF29
```

Note: This tool does not change plain text (txt) or rich text (rtf) signature files.

*(\*) Microsoft Outlook for Windows only.*

## Usage
1. Create an Outlook email signature ([learn how here](https://support.office.com/en-us/article/create-and-add-a-signature-to-messages-8ee5d4f4-68fd-464a-a1c1-0e1c80bb27f2 "Create and add a signature to messages")) and use the following replacement tokens in your text. All values are expressed in parts per million (ppm).

* {CO2-D}         
**Daily CO2** atmospheric concentration.

* {CO2-D-1Y}   
**Daily CO2** atmospheric concentration **one year ago**.

* {CO2-W-DATE}    
First day of the week.

* {CO2-W}        
Mean data for the **weekly CO2** atmospheric concentration.

* {CO2-W-1Y}  
Mean data for the **weekly CO2** atmospheric concentration **one year ago**.

* {CO2-W-10Y}  
Mean data for the **weekly CO2** atmospheric concentration **ten years ago**.  

* {CO2-W-V1800}  
CO2 concentration **variation since the year 1800**.  

2. Run the **OutlookCO2.exe** tool once a day to replace/update the expressions with the measured values.

### Signature Example
```
Today's CO2 atmospheric concentration: {CO2-D} ppm  
1 year ago: {CO2-D-1Y} ppm | 10 years ago: {CO2-W-10Y} ppm
```

After running **OutlookCO2.exe** the signature content will be updated with the current measured values. Example:
```
Today's CO2 atmospheric concentration: 407.53 ppm  
1 year ago: 405.62 ppm | 10 years ago: 384.73 ppm
```

## Automation
Instead of manually running **OutlookCO2.exe** an automated execution can be scheduled as a Windows Scheduled task.

To prevent a command window from appearing when running the scheduled task on Windows 10 we should setup the execution of a vbs script and not the execution of OutlookCO2.exe file.

The **OutlookCO2silentrun.vbs** file content should be: *(assuming OutlookCO2.exe is placed on C:\Program Files folder)*
```CreateObject("Wscript.Shell").Run """C:\Program Files\OutlookCO2.exe""", 0, True```  
Replace ```C:\Program Files``` with the name of folder you have the tool on.

The basic command syntax to create the scheduled task is:  
```SCHTASKS /CREATE /SC DAILY /TN "FOLDERPATH\TASKNAME" /TR "C:\FOLDER\APP-OR-SCRIPT" /ST HH:MM```  

To schedule an automatic execution every day at 12:00 run this command *(assuming OutlookCO2.exe and OutlookCO2silentrun.vbs are placed on C:\Program Files folder)*  
```SCHTASKS /CREATE /SC DAILY /TN "MyTasks\OutlookCO2 Update" /TR "wscript \"C:\Program Files\OutlookCO2silentrun.vbs\"" /ST 12:00```

Please schedule the task to execute at a random time, not necessarily 12:00, so that it doesn't run at the same time as everybody on your timezone (flooding the source feeds with simultaneous requests).  

Also make sure it runs at a time your computer is on.  

*You can specify it to "Run as soon as possible after a scheduled start is missed" on the windows GUI not but not using this command line instruction.*  

## Why it matters

https://climate.nasa.gov/vital-signs/carbon-dioxide/

https://350.org/science/

https://www.un.org/en/sections/issues-depth/climate-change/

## Notice
The source data is extracted from freely available to the public scientific feeds.

NOAA Earth System Research Laboratory Global Monitoring Division  
ftp://aftp.cmdl.noaa.gov/products/trends/co2/co2_weekly_mlo.txt

Earth's CO2 web site  
https://www.co2.earth/daily-co2

If you find a potential policy violation please reach me.

The software and source code available on this repository are provided "as is" without warranty of any kind, either express or implied. Use at your own risk. This tool is not endorsed by Microsoft, NOAA or any organization.
