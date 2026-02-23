# Compiling Debug Tools 1.0 on Termux (Galaxy A05s)

Termux supports .NET 8+ via proot-distro Ubuntu. Follow these exact steps:

1. Termux setup (5 mins)
```
# Update Termux (use F-Droid version)
pkg update && pkg upgrade
pkg install wget curl git proot-distro
```

2. Install ubuntu proot-distro
```
proot-distro install ubuntu
proot-distro login ubuntu
```

3. Inside Ubuntu: Install .NET 8 ARM64
```
# Ubuntu setup
apt update && apt upgrade -y
apt install -y wget tar libc6-dev

# Download .NET 8 ARM64 (Galaxy A05s = arm64)
wget https://dotnetcli.azureedge.net/dotnet/Runtime/8.0.10/dotnet-runtime-8.0.10-linux-arm64.tar.gz
wget https://dotnetcli.azureedge.net/dotnet/Sdk/8.0.410/dotnet-sdk-8.0.410-linux-arm64.tar.gz

# Extract to /opt/dotnet
mkdir -p /opt/dotnet
tar zxf dotnet-runtime-8.0.10-linux-arm64.tar.gz -C /opt/dotnet
tar zxf dotnet-sdk-8.0.410-linux-arm64.tar.gz -C /opt/dotnet --strip-components=1

# Symlink to PATH
ln -s /opt/dotnet/dotnet /usr/bin/dotnet
chmod +x /opt/dotnet/dotnet

# Verify
dotnet --version  # Should show 8.0.410
```

4. Transfer DebugTools1.0 to Termux
```
# Exit Ubuntu, back to Termux
exit

# Create project (copy all files from previous responses)
mkdir ~/DebugTools1.0
cd ~/DebugTools1.0

# Copy/paste: DebugTools.sln, all .csproj, all .cs files, README.md
# Use nano or Termux vim to create files
```

5. Build in ubuntu proot
```
proot-distro login ubuntu
cd /data/data/com.termux/files/home/DebugTools1.0

# Restore + Build (first time ~2-3 min on A05s)
dotnet restore
dotnet build -c Release

# Output: src/*/bin/Release/net8.0/*.dll (~100KB each)
```

6. Run tools directly
```
# From Ubuntu proot
dotnet src/LogTracer/bin/Release/net8.0/LogTracer.dll test.log
dotnet src/StackDumper/bin/Release/net8.0/StackDumper.dll --verbose
dotnet src/MemProfiler/bin/Release/net8.0/MemProfiler.dll
```

## Mem Tips or galaxy a05s (4—6 gb ram)
- Close all apps before building
- Use `Release` config (faster, smaller)
- Add swap if needed: `fallocate -l 2G /data/data/com.termux/files/home/swapfile && mkswap && swapon`
- Build one project at a time if OOM: `cd src/LogTracer && dotnet build`

## Troubleshooting

```
# Storage full? Clean up
dotnet nuget locals all --clear

# Permission issues
chmod -R 755 ~/DebugTools1.0

# Verify ARM64
uname -m  # Should be aarch64
```

Total setup: ~15 min first time, then dotnet build = 30s.
