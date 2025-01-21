# ����� ���������� ��� ��������
$itemTemplatesPath = "$env:USERPROFILE\Documents\Visual Studio 2022\Templates\ItemTemplates\CSharp"
$repositoryPath = "$(Get-Location)\Core\Templates"

# �������� ������� ���������
if (Test-Path "$repositoryPath\ItemTemplates") {
    Copy-Item "$repositoryPath\ItemTemplates\*" -Destination $itemTemplatesPath -Recurse -Force
    Write-Host "������� ��������� �����������."
} else {
    Write-Host "����� ItemTemplates ����������� � �����������."
}

# ��������� ������� � Visual Studio
Write-Host "���������� �������� Visual Studio..."
Start-Process -NoNewWindow -Wait -FilePath "devenv.exe" -ArgumentList "/installvstemplates"

Write-Host "��������� �������� ���������."
