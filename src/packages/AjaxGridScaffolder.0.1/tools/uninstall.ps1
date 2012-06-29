param($rootPath, $toolsPath, $package, $project)

# Remove the Ajax Grid Scaffolder option from the Add Controller Dialog
# Unfortunately, it is not possible (so far as I know) to unload the assembly
# while still allowing it to interface with the tooling assembly correctly
[AjaxGridScaffolderProvider.AjaxGridScaffolderProvider]::Dispose()
