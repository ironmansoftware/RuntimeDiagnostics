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

### Object Stats - Number of Objects

Display the number of objects allocated of each type.

```powershell
Import-ClrMemoryDump -Path .\dump.dmp | Mount-ClrRuntime | Get-ClrObject | Group-Object -Property Type | Sort-Object -Property Count -Descending | Select-Object -Property Count,Name -First 20
```

The output of the command will look like this. 

```
 Count Name
 ----- ----
271573 System.Management.Automation.Language.InternalScriptExtent
237623 System.String
 64008 Grpc.Core.Internal.RequestCallContextSafeHandle
 64000 Grpc.Core.Server+<>c__DisplayClass36_0
 64000 Grpc.Core.Internal.RequestCallCompletionDelegate
 55665 System.Management.Automation.Language.StringConstantExpressionAst
 46032 System.Management.Automation.VariablePath
 45971 System.Management.Automation.Language.VariableExpressionAst
 39995 System.Object
 28609 System.Management.Automation.Language.CommandBaseAst[]
 28403 System.Management.Automation.Language.CommandExpressionAst
 28256 System.Management.Automation.Language.PipelineAst
 28256 System.Collections.ObjectModel.ReadOnlyCollection<System.Management.Automation.Language.CommandBaseAst>
 26388 System.Reflection.RuntimeMethodInfo
 22672 System.Collections.DictionaryEntry
 22073 System.Linq.Expressions.ConstantExpression
 21609 System.Object[]
 15465 System.Linq.Expressions.UnaryExpression
 15365 System.Int32
 14578 System.Collections.Hashtable+Bucket[]
```