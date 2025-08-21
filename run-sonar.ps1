# Script chạy SonarQube analysis với coverage
[CmdletBinding()]
param(
    [string]$SonarUrl = "https://sonarqube.nexdev.net",
    [string]$ProjectKey = "BetListCore",
    [string]$ProjectName = "Bet List Core"
)

Write-Host "Starting SonarQube analysis..." -ForegroundColor Cyan
Write-Host "SonarQube URL: $SonarUrl" -ForegroundColor Yellow
Write-Host "Project Key: $ProjectKey" -ForegroundColor Yellow

# Kiểm tra dotnet sonarscanner
try {
    $sonarVersion = dotnet tool list --global | Select-String "dotnet-sonarscanner"
    if (-not $sonarVersion) {
        Write-Host "Installing dotnet-sonarscanner..." -ForegroundColor Yellow
        dotnet tool install --global dotnet-sonarscanner
    } else {
        Write-Host "dotnet-sonarscanner already installed" -ForegroundColor Green
    }
} catch {
    Write-Host "Error installing dotnet-sonarscanner: $_" -ForegroundColor Red
    exit 1
}

# Bước 1: Chạy tests với coverage
Write-Host "Step 1: Running tests with coverage..." -ForegroundColor Magenta
& .\build.ps1 -RunTests $true -RunCoverage $true

if ($LASTEXITCODE -ne 0) {
    Write-Host "Tests failed, aborting SonarQube analysis" -ForegroundColor Red
    exit 1
}

# Bước 2: Bắt đầu SonarQube analysis
Write-Host "Step 2: Starting SonarQube analysis..." -ForegroundColor Magenta
dotnet sonarscanner begin /k:"$ProjectKey" /n:"$ProjectName" /d:sonar.host.url="$SonarUrl"

if ($LASTEXITCODE -ne 0) {
    Write-Host "Failed to start SonarQube analysis" -ForegroundColor Red
    exit 1
}

# Bước 3: Build project
Write-Host "Step 3: Building project..." -ForegroundColor Magenta
dotnet build

if ($LASTEXITCODE -ne 0) {
    Write-Host "Build failed, ending SonarQube analysis" -ForegroundColor Red
    dotnet sonarscanner end
    exit 1
}

# Bước 4: Kết thúc SonarQube analysis
Write-Host "Step 4: Finalizing SonarQube analysis..." -ForegroundColor Magenta
dotnet sonarscanner end

if ($LASTEXITCODE -eq 0) {
    Write-Host "SonarQube analysis completed successfully!" -ForegroundColor Green
    Write-Host "Check results at: $SonarUrl/dashboard?id=$ProjectKey" -ForegroundColor Cyan
} else {
    Write-Host "SonarQube analysis failed" -ForegroundColor Red
    exit 1
}
