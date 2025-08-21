[CmdletBinding(PositionalBinding=$false)]
param(
    [bool] $CreatePackages = $false,
    [bool] $RunTests = $true,
    [bool] $RunCoverage = $false
)

Write-Host "Run Parameters:" -ForegroundColor Cyan
Write-Host "CreatePackages: $CreatePackages"
Write-Host "RunTests: $RunTests"
Write-Host "RunCoverage: $RunCoverage"

$packageOutputFolder = "$PSScriptRoot\.nupkgs"
$projectsToBuild = Get-ChildItem "$PSScriptRoot" -Include *Fanex.BetList.VirtualSports.csproj -Exclude *Tests.csproj,*.Example.*.csproj -recurse
$testsToRun = Get-ChildItem "$PSScriptRoot" -Include *.Tests.csproj -Recurse

if ($RunTests) {
    Write-Host "Running tests..." -ForegroundColor "Magenta"
    dotnet restore /ConsoleLoggerParameters:Verbosity=Quiet
    
    if ($LastExitCode -ne 0) {
        Write-Host "Error restoring packages, aborting build." -ForegroundColor "Red"
        Exit 1
    }
    
    if ($RunCoverage) {
        Write-Host "Running tests with coverage collection..." -ForegroundColor "Yellow"
        dotnet test --collect:"XPlat Code Coverage" --results-directory coverage --verbosity normal
        
        if ($LastExitCode -ne 0) {
            Write-Host "Error with coverage tests, aborting build." -ForegroundColor "Red"
            Exit 1
        }
        
        Write-Host "Coverage tests passed!" -ForegroundColor "Green"
    } else {
        foreach ($project in $testsToRun) {
            $projectName = $project.Name
            $projectFolder = Split-Path $project.FullName -parent

            Write-Host "Running tests: $projectName (all frameworks)" -ForegroundColor "Magenta"
            Push-Location $projectFolder

            dotnet test /ConsoleLoggerParameters:Verbosity=Quiet
            if ($LastExitCode -ne 0) {
                Write-Host "Error with tests, aborting build." -ForegroundColor "Red"
                Pop-Location
                Exit 1
            }

            Write-Host "Tests passed!" -ForegroundColor "Green"
            Pop-Location
        }
    }
    
    # Chạy OpenCover nếu cần
    if (Test-Path "Fanex.BetList.Core.UnitTest\runopencover.bat") {
        Write-Host "Running OpenCover coverage collection..." -ForegroundColor "Yellow"
        & "Fanex.BetList.Core.UnitTest\runopencover.bat"
        
        if ($LastExitCode -ne 0) {
            Write-Host "Warning: OpenCover coverage collection failed, but continuing build." -ForegroundColor "Yellow"
        } else {
            Write-Host "OpenCover coverage collection completed successfully!" -ForegroundColor "Green"
        }
    }
}

if ($CreatePackages) {
    mkdir -Force $packageOutputFolder | Out-Null
    Write-Host "Clearing existing $packageOutputFolder..." -NoNewline
    Get-ChildItem $packageOutputFolder | Remove-Item
    Write-Host "Done." -ForegroundColor "Green"

    Write-Host "Building all packages" -ForegroundColor "Green"

    foreach ($project in $projectsToBuild) {
        $projectName = $project.Name
        $projectFolder = Split-Path $project.FullName -parent

        Write-Host "Working on $projectName`:" -ForegroundColor "Magenta"
        Write-Host "  Restoring and packing $project..." -ForegroundColor "Magenta"

        Push-Location $projectFolder

        dotnet msbuild "/t:Restore;Pack" "/p:Configuration=Release" "/p:PackageOutputPath=$packageOutputFolder" /ConsoleLoggerParameters:Verbosity=Quiet
        
        if ($LastExitCode -ne 0) {
            Write-Host "Error building package for $projectName, aborting build." -ForegroundColor "Red"
            Pop-Location
            Exit 1
        }

        Write-Host "Done." -ForegroundColor "Green"
        Write-Host ""
        Pop-Location
    }

    Write-Host "Build Complete." -ForegroundColor "Green"
}

Write-Host "All operations completed successfully!" -ForegroundColor "Green"