HttpSysListener HEFException
-----------------

This examples shows that `HttpSysListener` throws `HEFException` when `WebApplication` is disposed and targeting `net10.0`.

The error is only thrown when run from Visual Studio where Debugger is attached (Debug and Release).

The error is not thrown when targeting `net8.0` or `net9.0`, so an issue introduced in `net10.0`.

To reproduce the issue, follow these steps:
- Open the solution in Visual Studio 2026.
- Set the target framework to `net10.0`.
- Run `WebApplication1` from within Visual Studio (Debugger attached), profile `StartApp` or `DontStartApp`.

The error message will be similar to:
```
fail: Microsoft.AspNetCore.Server.HttpSys.HttpSysListener[15]
      Dispose
      System.Runtime.InteropServices.SEHException (0x80004005): External component has thrown an exception.
         at Interop.Kernel32.CloseHandle(IntPtr handle)
         at Microsoft.Win32.SafeHandles.SafeFileHandle.ReleaseHandle()
         at System.Runtime.InteropServices.SafeHandle.InternalRelease(Boolean disposeOrFinalizeOperation)
         at System.Runtime.InteropServices.SafeHandle.Dispose()
         at Microsoft.AspNetCore.Server.HttpSys.HttpSysListener.DisposeInternal()
         at Microsoft.AspNetCore.Server.HttpSys.HttpSysListener.Dispose(Boolean disposing)
fail: WebApplication1[0]
      Error during disposal of application.
      System.Runtime.InteropServices.SEHException (0x80004005): External component has thrown an exception.
         at Interop.Kernel32.CloseHandle(IntPtr handle)
         at Microsoft.Win32.SafeHandles.SafeFileHandle.ReleaseHandle()
         at System.Runtime.InteropServices.SafeHandle.InternalRelease(Boolean disposeOrFinalizeOperation)
         at System.Runtime.InteropServices.SafeHandle.Dispose()
         at Microsoft.AspNetCore.Server.HttpSys.HttpSysListener.DisposeInternal()
         at Microsoft.AspNetCore.Server.HttpSys.HttpSysListener.Dispose(Boolean disposing)
         at Microsoft.Extensions.DependencyInjection.ServiceLookup.ServiceProviderEngineScope.DisposeAsync()
      --- End of stack trace from previous location ---
         at Microsoft.Extensions.Hosting.Internal.Host.DisposeAsync()
         at Microsoft.Extensions.Hosting.Internal.Host.Dispose()
         at Program.Main(String[] args)
```