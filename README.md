TeamCityPowerShell
==================
TeamCityPowerShell is a set of commandlets to interface the TeamCity REST API.

Usage
-----
Register the commandlet.

    set-alias installutil C:\Windows\Microsoft.NET\Framework\v4.0.30319\installutil
    installutil TeamCityPowerShell.Cmdlets.dll
    Add-PSSnapin TeamCityPowerShell
The first thing we need to do is setup some basic configuration settins by running the Select-BuildServer commandlet.

    Select-BuildServer -AU http://lvvbiztalk02b.cloudapp.net:8090/ -U rohu -P '$F66XxsvFo' -DP C:\Deploy -E Test
	
To get a complete list of available commands use:

    Get-Command -Module TeamCityPowerShell
For details how to use a specific commandlet use:

    Get-Help Get-Projects