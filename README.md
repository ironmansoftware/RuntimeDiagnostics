# Runtime Diagnostics

.NET Runtime diagnostic cmdlets. 

## Installation

```powershell
Install-Module RuntimeDiagnostics
```

## Usage

### Load Memory Dump

You can load a .NET memory dump using `Import-ClrMemoryDump`.

```powershell
$MemoryDump = Import-ClrMemoryDump -Path .\dump.dmp
```

### Attach to Process

You can attach to a .NET process using `Debug-ClrProcess`.

```powershell
$Process = Get-Process -Id 1234 | Debug-ClrProcess 
```

### Get .NET Runtime Information

You can get .NET runtime information using `Get-ClrVersion`.

```powershell
$ClrVersion = $Process | Get-ClrVersion
$ClrVersion = $MemoryDump | Get-ClrVersion
```

### Mount Runtime for Debugging

You can mount a .NET runtime for debugging using `Mount-ClrRuntime`.

```powershell
$Runtime = $Process | Mount-ClrRuntime
```

### Get Threads in Runtime

You can get threads in a .NET runtime using `Get-ClrThread`.

```powershell
$Threads = $Runtime | Get-ClrThread
```

### Display Stack Trace

You can display a stack trace using `Show-ClrStackTrace`.

```powershell
$Thread | Show-ClrStackTrace
```

### Get Objects 

You can get objects using `Get-ClrObject`.

```powershell
$Objects = $Runtime | Get-ClrObject
```

### Get Largest Objects 

You can get the largest objects using `Get-ClrObject` and the `-Largest` parameter.

```powershell
$Objects = $Runtime | Get-ClrObject -Largest 10
```

### Display Objects 

You can display objects using `Show-ClrObject`.

```powershell
$Objects = $Runtime | Get-ClrObject -Largest 10 | Show-ClrObject
```