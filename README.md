# C# Clay 

C# wrapper for [clay layout](https://github.com/nicbarker/clay), Version parity 0.11.0

> [!NOTE]
> PRE-ALPHA. This is still being build. 

# PRE-ALPHA
## TODO:
- [x] c to c# generator
- [ ] write c# versions of the macros as a bare-bone implementation. unsafe c#
- [ ] investigate if a more c# friendly design/naming is possible. safe c# (but don't use classes) 
- [ ] Complete Raylib renderer implementation
- [ ] Copy the raylib example from clay.
- [ ] check AOT compatibility.
- [ ] Write a better readme
- [ ] Should this keep the custom clay.h file or should we make a PR for it? (Holding off until we are sure we don't need more custom c code)
- [ ] Make a nuget package.
- [ ] Add tests
- [ ] add builder for non-windows 
## Issues:
- [ ] currently upgrading to clay v0.12.0 does not work. Need to investigate why
- [ ] Id generation is not working. Assume this has to do with UTF8 vs UTF16
- [ ] scrolling is not working. need to investigate.

# Building Clay.dll

## Requisites
- ClangSharp needs to be installed as a global tool: https://github.com/dotnet/ClangSharp
- Zig needs to be part of your PATH: https://ziglang.org/download/

## Building
- Copy clay.h from [clay repo](https://github.com/nicbarker/clay)
- Ensure the presence off / Add back the DLL_EXPORT
```c 
  #ifdef CLAY_DLL
  #define CLAY_DLL_EXPORT() __declspec(dllexport) __stdcall
  #else
  #define CLAY_DLL_EXPORT(null)
  #endif
```
- Ensure the pressence off / Add back CLAY_DLL_EXPORT
  - to all public methods under `// Public API functions ---`
  - to all internal methods under `// Internal API functions required by macros`. This is required so we can recreate the macros.
- open Clay.Builder.csproj
- run. Clay.dll and the interop files will be copied to their destinations in `Clay-cs`

## Verify

You can use a tool like [Dependencies](https://github.com/lucasg/Dependencies) to check if all needed c methods are listed.