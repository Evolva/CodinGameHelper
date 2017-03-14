#r @"packages\build\FAKE\tools\FakeLib.dll"
open Fake

let buildMode = getBuildParamOrDefault "buildMode" "Release"

let outputDir  = @".\output\"
let solutionPath = "CodinGameHelper/CodinGameHelper.sln"
let binaryName = "SourcesMerger.exe"

Target "Clean" (fun _ ->
    CleanDirs [outputDir]
)

Target "Build" (fun _ ->
    let buildParams defaults =
        { defaults with
            Verbosity = Some(Quiet)
            Targets = ["Build"]
            NoLogo = true
            Properties =
                [
                    "Optimize", "True"
                    "DebugSymbols", "False"
                    "Configuration", buildMode
                ]
         }

    build buildParams solutionPath |> DoNothing
)

Target "ILMerge" (fun _ ->
    let files = !! (@"CodinGameHelper\SourcesMerger\bin" @@ buildMode @@ "*")
    Copy (outputDir @@ buildMode) files

    CreateDir (outputDir @@ "ILMerged")

    let ilMergeParams defaults =
        { defaults with
            ToolPath = @"packages\build\ilmerge\tools\ILMerge.exe"
            TargetKind = Exe
            Libraries = !! (outputDir @@ buildMode @@ "*.dll")
        }
    
    ILMerge ilMergeParams (outputDir @@ "ILMerged" @@ binaryName) (outputDir @@ buildMode @@ binaryName)
)

"Clean" ==> "Build" ==> "ILMerge"

RunTargetOrDefault "ILMerge"
