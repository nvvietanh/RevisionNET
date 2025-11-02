@echo off
echo ========================================
echo SETUP DATABASE CHO LINQ DEMO
echo ========================================
echo.

echo [1/3] Restore packages...
dotnet restore
echo.

echo [2/3] Tao Migration...
dotnet ef migrations add InitialCreate --project Revision.LINQ
echo.

echo [3/3] Tao Database va Seed data...
dotnet ef database update --project Revision.LINQ
echo.

echo ========================================
echo HOAN THANH!
echo ========================================
echo Database 'ProductMngtLINQ' da duoc tao.
echo.
pause
