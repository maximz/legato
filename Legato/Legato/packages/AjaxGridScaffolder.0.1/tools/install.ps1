param($rootPath, $toolsPath, $package, $project)

$dummyFileName = "AjaxGridScaffolderInstallationDummyFile.txt"

Get-Project -All | %{$_.ProjectItems} `
                 | ?{$_.Name -eq $dummyFileName} `
                 | %{$_.Delete()}
