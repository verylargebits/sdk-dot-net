REM To download ILMerge use this link:
REM https://www.microsoft.com/en-us/download/details.aspx?id=17630
REM
REM After install add the following (or appropriate location) to your system PATH
REM C:\Program Files (x86)\Microsoft\ILMerge

mkdir bin
ilmerge /v4 /out:bin\vlb.exe TemplateTool\bin\release\vlb.slim.exe TemplateTool\bin\release\vlb.dll TemplateTool\bin\release\Newtonsoft.Json.dll
ilmerge /v4 /out:bin\vlba.exe AssetTool\bin\release\vlba.slim.exe AssetTool\bin\release\vlb.dll AssetTool\bin\release\Newtonsoft.Json.dll