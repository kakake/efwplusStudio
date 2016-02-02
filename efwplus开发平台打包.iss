; 脚本由 Inno Setup 脚本向导 生成！
; 有关创建 Inno Setup 脚本文件的详细资料请查阅帮助文档！

#define MyAppName "efwplus Studio"
#define MyAppVersion "1.6"
#define MyAppPublisher "efwplus"
#define MyAppURL "http://www.efwplus.cn/"
#define MyAppExeName "PluginManageTool.exe"
#define EFWWinAppExeName "EFWWin.exe"
#define wcfhostAppExeName "WCF服务主机.exe"

[Setup]
; 注: AppId的值为单独标识该应用程序。
; 不要为其他安装程序使用相同的AppId值。
; (生成新的GUID，点击 工具|在IDE中生成GUID。)
AppId={{49FAFBEA-8D3B-45B1-9404-8EBDF574204D}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
;AppVerName={#MyAppName} {#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}
AppUpdatesURL={#MyAppURL}
DefaultDirName=D:\efwplusStudio
DefaultGroupName=efwplus Studio
OutputBaseFilename=efwplusStudio_setup
SetupIconFile=D:\工作台\efwplus打包\efwplus开发平台\PluginManageTool\pm.ico
Compression=lzma
SolidCompression=yes

[Languages]
Name: "chinesesimp"; MessagesFile: "compiler:Default.isl"

;[Tasks];Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked; OnlyBelowVersion: 0,6.1
;Name: "desktopicon"; Description: "创建桌面快捷方式(&D)"; GroupDescription: "添加快捷方式:"; Components: main 

[Files]
;Source: "D:\工作台\efwplus打包\efwplus开发平台\PluginManageTool\PluginManageTool.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\工作台\efwplus打包\efwplus开发平台\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs
; 注意: 不要在任何共享系统文件上使用“Flags: ignoreversion”

[Icons]
Name: "{group}\efwplus Studio {#MyAppVersion}"; Filename: "{app}\PluginManageTool\{#MyAppExeName}"
Name: "{group}\Win客户端"; Filename: "{app}\WinformPlatform\{#EFWWinAppExeName}"
Name: "{group}\Wcf服务主机"; Filename: "{app}\WinformPlatform\{#wcfhostAppExeName}"
Name: "{group}\Uninstall"; Filename: "{uninstallexe}"
Name: "{commondesktop}\efwplus Studio {#MyAppVersion}"; Filename: "{app}\PluginManageTool\{#MyAppExeName}"

[Run]
Filename: "{app}\PluginManageTool\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent

