TuneAsSharp!!! (WIP)
==

tune values in your (already) running apps by editing your sources!
            
 Notice:
 ==           
 For **RELEASE BUILD**, set compilation options to:
 
 - optimize code: DISABLED :-( 
 - debug symbols: ENABLED, debug type: PDB-only
 
 (otherwise, the line info stack trace analysis won't work and will return incorrect line numbers, effectively breaking the code)
             
 **Tested only on macOS Catalina with .netcore 3.1**

TODO list:
==
- unity support ? (probably with disabled Roslyn)
- tweakable functions ? (as seen in tweak_af for Python, not sure about getting it working)
- automatic versioning of changes?