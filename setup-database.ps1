Write-Host "========================================"  -ForegroundColor Cyan
Write-Host "SETUP DATABASE CHO LINQ DEMO" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""

# Di chuyển đến thư mục dự án
Set-Location -Path "Revision.LINQ"

Write-Host "[1/4] Restore packages..." -ForegroundColor Yellow
dotnet restore
Write-Host ""

Write-Host "[2/4] Build project..." -ForegroundColor Yellow
dotnet build
Write-Host ""

Write-Host "[3/4] Tạo Migration..." -ForegroundColor Yellow
dotnet ef migrations add InitialCreate
Write-Host ""

Write-Host "[4/4] Tạo Database và Seed data..." -ForegroundColor Yellow
dotnet ef database update
Write-Host ""

Write-Host "========================================" -ForegroundColor Green
Write-Host "HOÀN THÀNH!" -ForegroundColor Green
Write-Host "========================================" -ForegroundColor Green
Write-Host "Database 'ProductMngtLINQ' đã được tạo." -ForegroundColor Green
Write-Host ""

# Quay lại thư mục gốc
Set-Location -Path ".."
