@echo off
echo Starting coverage collection...

REM Tạo thư mục coverage nếu chưa có
if not exist "coverage" mkdir coverage

REM Sử dụng dotnet test với coverage collection (modern approach)
echo Running dotnet test with coverage collection...
dotnet test --collect:"XPlat Code Coverage" --results-directory coverage --verbosity normal

REM Fallback: Sử dụng OpenCover nếu có sẵn packages
if exist "..\..\..\packages\OpenCover.4.5.2506\OpenCover.Console.exe" (
    echo OpenCover found, running additional coverage collection...
    ..\..\..\packages\OpenCover.4.5.2506\OpenCover.Console.exe -target:runtests.bat -register:user -filter:+[Fanex.BetList.Core]* -output:coverage/opencover.xml
    
    if exist "..\..\..\packages\ReportGenerator.1.9.1.0\reportgenerator.exe" (
        echo Generating coverage report...
        ..\..\..\packages\ReportGenerator.1.9.1.0\reportgenerator.exe -reports:coverage/opencover.xml -targetdir:coverage
    )
) else (
    echo OpenCover not found, using dotnet coverage only
)

echo Coverage collection completed!
pause
