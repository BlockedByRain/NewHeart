# ר�ñ���ת���ű���GB2312 �� UTF-8��
# ǿ��ָ��ԭ����Ϊ GB2312������Զ�������
# ����Ϊ ConvertGB2312ToUTF8.ps1

# ---------- ���ò��� ----------
$AddBOM = $true  # �Ƿ���� BOM��C# ��Ŀ���� true��

# ---------- ���Ĵ��� ----------
$sourceEncoding = [System.Text.Encoding]::GetEncoding("GB2312")  # �̶�ԭ����Ϊ GB2312

Get-ChildItem -Path . -Recurse -Filter *.cs | ForEach-Object {
    $filePath = $_.FullName
    try {
        # 1. ��ȡԭʼ���ݣ�ǿ��ʹ�� GB2312��
        $content = [System.IO.File]::ReadAllText($filePath, $sourceEncoding)
        
        # 2. ת��Ϊ UTF-8
        $utf8Encoding = if ($AddBOM) { 
            [System.Text.Encoding]::UTF8 
        } else { 
            New-Object System.Text.UTF8Encoding($false) 
        }
        
        # 3. �����ļ�
        [System.IO.File]::WriteAllText($filePath, $content, $utf8Encoding)
        Write-Host "ת���ɹ�: $filePath" -ForegroundColor Green
    } catch {
        Write-Host "ת��ʧ��: $filePath - $($_.Exception.Message)" -ForegroundColor Red
    }
}

Write-Host "������ɣ�"