# OutlookCO2
Outlook Signature CO2 Levels.  
  
This tool updates your Outlook HTML signature with updated CO2 atmospheric concentration levels as recorded in Mauna Loa Observatory.  
  
All values are expressed in parts per million (ppm).  

This tool does not change plain text (txt) or rich text (rtf) signature files.

# Usage
1. Create an Outlook email signature and use the following replacement tokens in your text:

* {CO2-D}         
**Daily CO2** atmospheric concentration.

* {CO2-D-1Y}   
**Daily CO2** atmospheric concentration one year ago.

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

2. Run the **OutlookCO2.exe** tool once a day to update the values.

## Signature Example
```
This week's CO2 concentration: {CO2-D} ppm  
1 | 10 years ago: {CO2-D-1Y} | {CO2-W-10Y} ppm
```

# Notice
The source data is extracted from freely available to the public scientific feeds.

NOAA Earth System Research Laboratory Global Monitoring Division  
ftp://aftp.cmdl.noaa.gov/products/trends/co2/co2_weekly_mlo.txt

Earth's CO2 web site  
https://www.co2.earth/daily-co2
