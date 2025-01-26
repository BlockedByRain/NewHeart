# 专用编码转换脚本（GB2312 → UTF-8）
# 强制指定原编码为 GB2312，解决自动检测错误
# 保存为 ConvertGB2312ToUTF8.ps1

# ---------- 配置参数 ----------
$AddBOM = $true  # 是否添加 BOM（C# 项目建议 true）

# ---------- 核心代码 ----------
$sourceEncoding = [System.Text.Encoding]::GetEncoding("GB2312")  # 固定原编码为 GB2312

Get-ChildItem -Path . -Recurse -Filter *.cs | ForEach-Object {
    $filePath = $_.FullName
    try {
        # 1. 读取原始内容（强制使用 GB2312）
        $content = [System.IO.File]::ReadAllText($filePath, $sourceEncoding)
        
        # 2. 转换为 UTF-8
        $utf8Encoding = if ($AddBOM) { 
            [System.Text.Encoding]::UTF8 
        } else { 
            New-Object System.Text.UTF8Encoding($false) 
        }
        
        # 3. 保存文件
        [System.IO.File]::WriteAllText($filePath, $content, $utf8Encoding)
        Write-Host "转换成功: $filePath" -ForegroundColor Green
    } catch {
        Write-Host "转换失败: $filePath - $($_.Exception.Message)" -ForegroundColor Red
    }
}

Write-Host "操作完成！"