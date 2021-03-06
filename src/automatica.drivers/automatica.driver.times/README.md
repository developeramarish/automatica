# Introduction 

[![Build Status](https://automatica-core.visualstudio.com/automatica/_apis/build/status/Plugins/Drivers/P3.Driver.Times?branchName=develop)](https://automatica-core.visualstudio.com/automatica/_build/latest?definitionId=32&branchName=develop)

Times provides date/time and sun angles nodes. 

# Date/Time
Date/Time provides date and time in different formats.

 ## Nodes
 * Date
 * Day of week
 * Seconds since boot
 * Second
 * Date & time
 * Day
 * Hour
 * Year
 * Month
 * Time
 * Milisecond
 * Minute


## How to use
Times can be added in the Virtual node.

![DT1](https://github.com/automatica-core/automatica.driver.times/blob/master/images/Screenshot_1.png?raw=true)

![DT2](https://github.com/automatica-core/automatica.driver.times/blob/master/images/Screenshot_2.png?raw=true)

After adding the Date/Time node, you can see all the nodes which the driver provides for you. 
Date/Time values are provides in local-time. The used timezone should be configured in your system.
![DT3](https://github.com/automatica-core/automatica.driver.times/blob/master/images/Screenshot_3.png?raw=true)


# Sun
Sun provides calculated data of the sun angle, sunrise & sunset.

## Nodes
* Sunrise
    * Time for sunrise
* Sunset
    * Time for sunset
* Is sunset
    * Is sunset
* Is sunrise
    * Is sunrise
* Dawn
    * Is dawn (30 minutes before sunrise)
* Dusk
    * Is dusk  (30 minutes before sunset)


## How to use

![SUN4](https://github.com/automatica-core/automatica.driver.times/blob/master/images/Screenshot_4.png?raw=true)

![SUN5](https://github.com/automatica-core/automatica.driver.times/blob/master/images/Screenshot_5.png?raw=true)
  

Save & reload your configuration and use date/time in your Automatica.Core Server.  