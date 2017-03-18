mkdir bin
ilmerge /v4 /out:bin\vlb.exe TemplateTool\bin\release\vlb.slim.exe TemplateTool\bin\release\vlb.dll TemplateTool\bin\release\Newtonsoft.Json.dll
ilmerge /v4 /out:bin\vlba.exe AssetTool\bin\release\vlba.slim.exe AssetTool\bin\release\vlb.dll AssetTool\bin\release\Newtonsoft.Json.dll