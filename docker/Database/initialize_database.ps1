#escape = `
<# 
Works for sql server express image
Expects escape character set to `
Expects dacpac to be <databasename>.Database.dacpac
Assumes mdf and ldf files are <databasename>_Primary.mdf and ldf respectively
Generates: deploy.sql from dacpac diff
#>
param(
    [Parameter(Mandatory = $true)][string] $sa_password,
    [Parameter(Mandatory = $true)][string]$data_path,
    [Parameter(Mandatory = $true)][string]$databaseName
)

$SqlPackagePath = 'C:\BuildTools\SqlPackage.exe'
$mdfPath = "$data_path\$databaseName" + "_Primary.mdf"
$ldfpath = "$data_path\$databaseName" + "_Primary.ldf"

Write-Verbose 'Starting SQL Server'
Start-Service MSSQL`$SQLEXPRESS
if ($sa_password -ne "_") {
    Write-Verbose 'Changing SA login credentials'
    $sqlcmd = "ALTER LOGIN sa with password='$sa_password'; ALTER LOGIN sa ENABLE;"
    Invoke-Sqlcmd -QUERY $sqlcmd -ServerInstance "(local)"
}

if ((Test-Path $mdfPath) -eq $true) {
    $sqlcmd = "IF DB_ID('$databaseName') IS NULL BEGIN CREATE DATABASE $databaseName ON (FILENAME = N'$mdfpath')"
    if ((Test-Path $ldfpath) -eq $true) {
        $sqlcmd = "$sqlcmd, (FILENAME = N'$ldfpath')"
    }
    $sqlcmd = "$sqlcmd FOR ATTACH; END"
    Invoke-Sqlcmd $sqlcmd
    Write-Verbose 'Data files already exist - will attach and upgrade database'
}
else {
    Write-Verbose 'No data files - will create new database'
}

Write-Verbose "Looking for dacpac in $pwd"

& $SqlPackagePath `
    /sf:Database.dacpac `
    /a:script /op:deploy.sql /p:CommentOutSetVarDeclarations=true `
    /tsn:.\SQLEXPRESS /tdn:$databaseName /tu:sa /tp:$sa_password

$SqlCmdVars = "DatabaseName=$databaseName", "DefaultFilePrefix=$databaseName", "DefaultDataPath=$data_path\", "DefaultLogPath=$data_path\"

Invoke-Sqlcmd -InputFile deploy.sql -Variable $SqlCmdVars -Verbose

Write-Verbose "Deployed $databaseName database, data files at path: $data_path"

$lastCheck = (Get-Date).AddSeconds(-5)

while ($true) {
    Get-EventLog -LogName Application -Source "MSSQL*" -After $lastCheck
    $lastCheck = Get-Date
    Start-Sleep -Seconds 5 
}
