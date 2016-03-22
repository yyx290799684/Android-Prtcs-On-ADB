# Android-Prtcs-On-ADB
Use ADB to get  a screenshot form Android devices

请将Android SDK 中的 ADB.exe 加入环境变量,测试方法，运行CMD，输入ADB命令。

之后手机打开USB调试并保证能成功连接电脑（root后可用WIFI调试）。若截图不成功，可能是文件读取权限问题，请用RE管理器修改文件夹权限或更改代码中的截图路径（默认为/storage/sdcard0/screenshot.png）
