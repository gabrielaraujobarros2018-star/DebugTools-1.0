# Debug Tools 1.0

Lightweight .NET debugging utilities. Cross-platform (Windows/Linux/macOS/ARM). Requires .NET 8+ runtime only.

## Features
- LogTracer: Scan logs for ERROR/EXCEPTION lines with stats
- StackDumper: Threaded stack trace dumper (Ctrl+C trigger)  
- MemProfiler: Real-time memory monitoring + GC demo
- Shared: Console + debug.log output

## Directory Structure
DebugTools1.0/
├── DebugTools.sln
├── src/
│   ├── Common/Common.csproj
│   │   └── Utils.cs (28 LOC)
│   ├── LogTracer/LogTracer.csproj
│   │   └── Program.cs (32 LOC)
│   ├── StackDumper/StackDumper.csproj
│   │   └── Program.cs (25 LOC)
│   └── MemProfiler/MemProfiler.csproj
│       └── Program.cs (26 LOC)
├── README.md
└── build.sh

## Setup
cd DebugTools1.0
dotnet restore
dotnet build

## Usage

LogTracer:
dotnet src/LogTracer/bin/Debug/net8.0/LogTracer.dll app.log
dotnet src/LogTracer/bin/Debug/net8.0/LogTracer.dll --verbose --out=errors.txt *.log

StackDumper:
dotnet src/StackDumper/bin/Debug/net8.0/StackDumper.dll --verbose
(Ctrl+C dumps stack)

MemProfiler:
dotnet src/MemProfiler/bin/Debug/net8.0/MemProfiler.dll
(Shows MB usage every 2s)

## .csproj Template
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net8.0</TargetFramework>
  </PropertyGroup>
  <ItemGroup>
    <ProjectReference Include="..CommonCommon.csproj" />
  </ItemGroup>
</Project>

## Notes
- ~100KB per tool binary
- Works on Galaxy A05s (Termux .NET)
- Perfect for Lumen OS debugging
- Extend: Add src/NewTool/ folder
