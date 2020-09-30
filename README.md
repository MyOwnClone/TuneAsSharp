TuneAsSharp!!! (WIP)
==

Tune values in your (already) running apps by editing your sources! See `Example.cs`

**Very buggy and work in progress!**

inspired by https://github.com/joeld42/ld48jovoc/blob/master/util/tweakval/tweakval.cpp and friends

TLDR:
==
Put `Tune.tv("default")` calls in your sources. Run your code, let it running and during that, change the `"default"` to whatever you want (which wont break your code). See that your code sees the updated value.

There are two "parsers" currently:
* simple one, which just parses Int, Float and String - but works everywhere (in Unity too, I guess, not tested yet)
* Roslyn based, which should handle "everything", but Roslyn is not available everywhere
            
 Notice:
 ==           
 For **RELEASE BUILD**, set compilation options to:
 
 - optimize code: DISABLED :-( (you can enable optimization in other modules, though)
 - debug symbols: ENABLED, debug type: PDB-only
 
 (otherwise, the line info stack trace analysis won't work and will return incorrect line numbers, effectively breaking the code)
             
 **Tested only on macOS Catalina with .netcore 3.1**
 
 LICENSE:
 ==
 TODO (probably permissive, but not viral, so something like BSD or MIT)
 
 No guarantees on anything!

TODO list:
==
- FS Notification triggered reload ?
- better packaged as a library + nuget support
- unity support ? (probably with disabled Roslyn)
- tweakable functions ? (as seen in tweak_af for Python, not sure about getting it working)
- automatic versioning of changes?