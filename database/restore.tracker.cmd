sqlcmd -S "(local)\SQL2016" -U sa -P Password12! -i "%APPVEYOR_BUILD_FOLDER%\Database\SqlServer\Tracker.sql"