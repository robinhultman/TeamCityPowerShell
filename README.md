TeamCityPowerShell
==================
TeamCityPowerShell is a set of commandlets to interface the TeamCity REST API.

Usage
-----
1. Create a new folder called TeamCityPowerShell.CmdLets in your PowerShell modules folder i.e. user\documents\windowspowershell\modules\TeamCityPowerShell.CmdLets.

2. Import the module into PowerShell
Import-Module TeamCityPowerShell.CmdLets

3. Specify configuration settings.
Select-BuildServer -ApiPath <uri to REST API> -ApiUserName <username> -ApiPassword <password> -LocalDeployPath <Path to store build artifacts> -Environment <test, production etc>

4. Display projects
Get-Projects

5. Select an active project
Select-Project -Project <projectname>

6. Get all tags for the active project.
Get-BuildTags

7. Get build types for the active project.
Get-BuildTypes

8. Download build artifacts for the active project by build type.
Save-BuildArtifacts -BuildConfiguration <buildtype> -Tag <tag>
