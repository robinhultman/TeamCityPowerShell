TeamCityPowerShell
==================
TeamCityPowerShell is a set of commandlets to interface the TeamCity REST API.

Usage
-----
Install the commandlet.

1.  Create the the cmdlet directory in your modules folder i.e ```\user\documents\windowspowershell\modules\TeamCityPowerShell.Cmdlets```.

2.  Copy TeamCityPowerShell.Cmdlets.dll to the newly created folder.

3.  Create a folder for the help files ```\user\documents\windowspowershell\modules\TeamCityPowerShell.Cmdlets\en-US```.

4.  Copy the files in the HelpFiles folder to the newly created folder.

5.  Import the module in PowerShell.

```
Import-Module TeamCity.PowerShell.Cmdlets
```
	
The first thing we need to do is setup some basic configuration settins by running the Select-BuildServer commandlet.

```
Select-BuildServer -AU mybuildserver -U usr -P 'Pwd' -DP C:\Deploy
```

To get a complete list of available commands use:

```
Get-Command -Module TeamCityPowerShell.Cmdlets
```

For details how to use a specific commandlet use:

```
Get-Help Get-Projects
```
