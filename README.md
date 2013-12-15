TeamCityPowerShell
==================
TeamCityPowerShell is a set of commandlets to interface the TeamCity REST API.

Usage
-----
Register the commandlet.

    set-alias installutil C:\Windows\Microsoft.NET\Framework\v4.0.30319\installutil
    installutil TeamCityPowerShell.Cmdlets.dll
    Add-PSSnapin TeamCityPowerShell
To get a complete list of available commands use:

    Get-Command -Module TeamCityPowerShell
For details how to use a specific commandlet use:

    Get-Help Get-Projects