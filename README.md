# CodinGameHelper

## SourcesMerger

Allow you to merge all source files from a directory into a single source file in order to "upload" to CodinGame

```batch
SourcesMerger --help

  -s, --sourcesPath    Required. path to your source code
  -o, --outputFile     Required. path to your output file
  -e, --encoding       (Default: Default) Encoding of your source code
                       (Default/UTF8/ASCII/Unicode)
  -w, --watch          (Default: False) enable watch
```

Can be used as a post-build event in Visual Studio or as a watcher