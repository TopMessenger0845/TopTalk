# Папка назначения для шаблонов
$itemTemplatesPath = "$env:USERPROFILE\Documents\Visual Studio 2022\Templates\ItemTemplates\CSharp"
$repositoryPath = "$(Get-Location)\Core\Templates"

# Копируем шаблоны элементов
if (Test-Path "$repositoryPath\ItemTemplates") {
    Copy-Item "$repositoryPath\ItemTemplates\*" -Destination $itemTemplatesPath -Recurse -Force
    Write-Host "Шаблоны элементов установлены."
} else {
    Write-Host "Папка ItemTemplates отсутствует в репозитории."
}

# Обновляем шаблоны в Visual Studio
Write-Host "Обновление шаблонов Visual Studio..."
Start-Process -NoNewWindow -Wait -FilePath "devenv.exe" -ArgumentList "/installvstemplates"

Write-Host "Установка шаблонов завершена."
