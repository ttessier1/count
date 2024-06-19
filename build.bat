@echo off
echo Setting CSC

REM Test batch file for the count.exe program
REM This batch file "should" build the count.exe program
REM as well as run a few simple tests
REM count.exe [-w|--words] [-d|--distinct] [-c|--characters] [-h|--histogram] [-l|--lines] [[-f|--file] <filespec>] [-r|--recursive] [[-l|--lan] <en|fr|it|es>] message
REM 		-w|--words - count words
REM 		-d|--distinct - count distinct words
REM 		-c|--characters - count characters
REM 		-h|--histogram - histogram of characters
REM 		-l|--lines - count lines
REM 		-f|--file <filespec> - the file path or glob to work on 
REM 		-r|--recursive - operate recursively
REM 		-l|--lang <en|fr|it|es>
REM 		[message] - a message to operate on, if files are specified, this should not be

set CSC=c:\windows\microsoft.net\Framework64\v4.0.30319\csc.exe
set PARAMS=-warnaserror -warn:4
set TEST_PROGRAM=.\count.exe
echo After Set CSC
if NOT EXIST "%CSC%" goto CSCNotExist

echo CSC is "%CSC%"
%CSC% %PARAMS% /out:%TEST_PROGRAM% /debug+ /debug:full /target:exe main.cs
set LASTVALUE=%ERRORLEVEL%
IF "%ERRORLEVEL%"=="0" goto TESTS
goto END


:CSCNotExist
echo [%CSC%] does not exist in the normal location
exit /b 1

:TestProgramNotExist
echo [%TEST_PROGRAM%] does not exist in the normal location
exit /b 1

:TESTS
echo Completed
set TEST=0

set /A TEST=%TEST%+1
echo TEST[%TEST%] Calling %TEST_PROGRAM% with no arguments 
echo %TEST_PROGRAM%
%TEST_PROGRAM% >output\test%TEST%.output 2>output\test%TEST%.error
set LASTVALUE=%ERRORLEVEL%
echo Program returned %ERRORLEVEL% %LASTVALUE%
if "%LASTVALUE%"=="0" echo [32mSuccess[0m & type output\test%TEST%.output
if NOT "%LASTVALUE%"=="0" IF EXIST output\test%TEST%.output type output\test%TEST%.output
if NOT "%LASTVALUE%"=="0" echo [31mFailure[0m & type output\test%TEST%.error
IF NOT EXIST expect\TEST%TEST%.expect goto MissingExpect
for /f "" %%A IN (expect\TEST%TEST%.expect) DO set LAST_EXPECT=%%A
if "%LASTVALUE%"=="%LAST_EXPECT%" echo [32mResult Expected[0m 
if NOT "%LASTVALUE%"=="%LAST_EXPECT%" echo [31mResult Not Expected[0m & goto FAILEXPECT
echo .

set /A TEST=%TEST%+1
echo TEST[%TEST%] Calling %TEST_PROGRAM% with no arguments 
echo %TEST_PROGRAM% -w "This is a test"
%TEST_PROGRAM% -w "This is a test">output\test%TEST%.output 2>output\test%TEST%.error
set LASTVALUE=%ERRORLEVEL%
echo Program returned %ERRORLEVEL% %LASTVALUE%
if "%LASTVALUE%"=="0" echo [32mSuccess[0m & type output\test%TEST%.output
if NOT "%LASTVALUE%"=="0" IF EXIST output\test%TEST%.output type output\test%TEST%.output
if NOT "%LASTVALUE%"=="0" echo [31mFailure[0m & type output\test%TEST%.error
IF NOT EXIST expect\TEST%TEST%.expect goto MissingExpect
for /f "" %%A IN (expect\TEST%TEST%.expect) DO set LAST_EXPECT=%%A
if "%LASTVALUE%"=="%LAST_EXPECT%" echo [32mResult Expected[0m 
if NOT "%LASTVALUE%"=="%LAST_EXPECT%" echo [31mResult Not Expected[0m & goto FAILEXPECT
echo .

set /A TEST=%TEST%+1
echo TEST[%TEST%] Calling %TEST_PROGRAM% with no arguments 
echo %TEST_PROGRAM% --words "This is a test"
%TEST_PROGRAM% --words "This is a test">output\test%TEST%.output 2>output\test%TEST%.error
set LASTVALUE=%ERRORLEVEL%
echo Program returned %ERRORLEVEL% %LASTVALUE%
if "%LASTVALUE%"=="0" echo [32mSuccess[0m & type output\test%TEST%.output
if NOT "%LASTVALUE%"=="0" IF EXIST output\test%TEST%.output type output\test%TEST%.output
if NOT "%LASTVALUE%"=="0" echo [31mFailure[0m & type output\test%TEST%.error
IF NOT EXIST expect\TEST%TEST%.expect goto MissingExpect
for /f "" %%A IN (expect\TEST%TEST%.expect) DO set LAST_EXPECT=%%A
if "%LASTVALUE%"=="%LAST_EXPECT%" echo [32mResult Expected[0m 
if NOT "%LASTVALUE%"=="%LAST_EXPECT%" echo [31mResult Not Expected[0m & goto FAILEXPECT
echo .

set /A TEST=%TEST%+1
echo TEST[%TEST%] Calling %TEST_PROGRAM% with no arguments 
echo %TEST_PROGRAM% -d "This is a test"
%TEST_PROGRAM% -d "This is a test">output\test%TEST%.output 2>output\test%TEST%.error
set LASTVALUE=%ERRORLEVEL%
echo Program returned %ERRORLEVEL% %LASTVALUE%
if "%LASTVALUE%"=="0" echo [32mSuccess[0m & type output\test%TEST%.output
if NOT "%LASTVALUE%"=="0" IF EXIST output\test%TEST%.output type output\test%TEST%.output
if NOT "%LASTVALUE%"=="0" echo [31mFailure[0m & type output\test%TEST%.error
IF NOT EXIST expect\TEST%TEST%.expect goto MissingExpect
for /f "" %%A IN (expect\TEST%TEST%.expect) DO set LAST_EXPECT=%%A
if "%LASTVALUE%"=="%LAST_EXPECT%" echo [32mResult Expected[0m 
if NOT "%LASTVALUE%"=="%LAST_EXPECT%" echo [31mResult Not Expected[0m & goto FAILEXPECT
echo .

set /A TEST=%TEST%+1
echo TEST[%TEST%] Calling %TEST_PROGRAM% with no arguments 
echo %TEST_PROGRAM% --distinct "This is a test"
%TEST_PROGRAM% --distinct "This is a test">output\test%TEST%.output 2>output\test%TEST%.error
set LASTVALUE=%ERRORLEVEL%
echo Program returned %ERRORLEVEL% %LASTVALUE%
if "%LASTVALUE%"=="0" echo [32mSuccess[0m & type output\test%TEST%.output
if NOT "%LASTVALUE%"=="0" IF EXIST output\test%TEST%.output type output\test%TEST%.output
if NOT "%LASTVALUE%"=="0" echo [31mFailure[0m & type output\test%TEST%.error
IF NOT EXIST expect\TEST%TEST%.expect goto MissingExpect
for /f "" %%A IN (expect\TEST%TEST%.expect) DO set LAST_EXPECT=%%A
if "%LASTVALUE%"=="%LAST_EXPECT%" echo [32mResult Expected[0m 
if NOT "%LASTVALUE%"=="%LAST_EXPECT%" echo [31mResult Not Expected[0m & goto FAILEXPECT
echo .

set /A TEST=%TEST%+1
echo TEST[%TEST%] Calling %TEST_PROGRAM% with no arguments 
echo %TEST_PROGRAM% -c "This is a test"
%TEST_PROGRAM% -c "This is a test">output\test%TEST%.output 2>output\test%TEST%.error
set LASTVALUE=%ERRORLEVEL%
echo Program returned %ERRORLEVEL% %LASTVALUE%
if "%LASTVALUE%"=="0" echo [32mSuccess[0m & type output\test%TEST%.output
if NOT "%LASTVALUE%"=="0" IF EXIST output\test%TEST%.output type output\test%TEST%.output
if NOT "%LASTVALUE%"=="0" echo [31mFailure[0m & type output\test%TEST%.error
IF NOT EXIST expect\TEST%TEST%.expect goto MissingExpect
for /f "" %%A IN (expect\TEST%TEST%.expect) DO set LAST_EXPECT=%%A
if "%LASTVALUE%"=="%LAST_EXPECT%" echo [32mResult Expected[0m 
if NOT "%LASTVALUE%"=="%LAST_EXPECT%" echo [31mResult Not Expected[0m & goto FAILEXPECT
echo .

set /A TEST=%TEST%+1
echo TEST[%TEST%] Calling %TEST_PROGRAM% with no arguments 
echo %TEST_PROGRAM% --characters "This is a test"
%TEST_PROGRAM% --characters "This is a test">output\test%TEST%.output 2>output\test%TEST%.error
set LASTVALUE=%ERRORLEVEL%
echo Program returned %ERRORLEVEL% %LASTVALUE%
if "%LASTVALUE%"=="0" echo [32mSuccess[0m & type output\test%TEST%.output
if NOT "%LASTVALUE%"=="0" IF EXIST output\test%TEST%.output type output\test%TEST%.output
if NOT "%LASTVALUE%"=="0" echo [31mFailure[0m & type output\test%TEST%.error
IF NOT EXIST expect\TEST%TEST%.expect goto MissingExpect
for /f "" %%A IN (expect\TEST%TEST%.expect) DO set LAST_EXPECT=%%A
if "%LASTVALUE%"=="%LAST_EXPECT%" echo [32mResult Expected[0m 
if NOT "%LASTVALUE%"=="%LAST_EXPECT%" echo [31mResult Not Expected[0m & goto FAILEXPECT
echo .

set /A TEST=%TEST%+1
echo TEST[%TEST%] Calling %TEST_PROGRAM% with no arguments 
echo %TEST_PROGRAM% -h "This is a test"
%TEST_PROGRAM% -h "This is a test">output\test%TEST%.output 2>output\test%TEST%.error
set LASTVALUE=%ERRORLEVEL%
echo Program returned %ERRORLEVEL% %LASTVALUE%
if "%LASTVALUE%"=="0" echo [32mSuccess[0m & type output\test%TEST%.output
if NOT "%LASTVALUE%"=="0" IF EXIST output\test%TEST%.output type output\test%TEST%.output
if NOT "%LASTVALUE%"=="0" echo [31mFailure[0m & type output\test%TEST%.error
IF NOT EXIST expect\TEST%TEST%.expect goto MissingExpect
for /f "" %%A IN (expect\TEST%TEST%.expect) DO set LAST_EXPECT=%%A
if "%LASTVALUE%"=="%LAST_EXPECT%" echo [32mResult Expected[0m 
if NOT "%LASTVALUE%"=="%LAST_EXPECT%" echo [31mResult Not Expected[0m & goto FAILEXPECT
echo .

set /A TEST=%TEST%+1
echo TEST[%TEST%] Calling %TEST_PROGRAM% with no arguments 
echo %TEST_PROGRAM% --histogram "This is a test"
%TEST_PROGRAM% --histogram "This is a test">output\test%TEST%.output 2>output\test%TEST%.error
set LASTVALUE=%ERRORLEVEL%
echo Program returned %ERRORLEVEL% %LASTVALUE%
if "%LASTVALUE%"=="0" echo [32mSuccess[0m & type output\test%TEST%.output
if NOT "%LASTVALUE%"=="0" IF EXIST output\test%TEST%.output type output\test%TEST%.output
if NOT "%LASTVALUE%"=="0" echo [31mFailure[0m & type output\test%TEST%.error
IF NOT EXIST expect\TEST%TEST%.expect goto MissingExpect
for /f "" %%A IN (expect\TEST%TEST%.expect) DO set LAST_EXPECT=%%A
if "%LASTVALUE%"=="%LAST_EXPECT%" echo [32mResult Expected[0m 
if NOT "%LASTVALUE%"=="%LAST_EXPECT%" echo [31mResult Not Expected[0m & goto FAILEXPECT
echo .

set /A TEST=%TEST%+1
echo TEST[%TEST%] Calling %TEST_PROGRAM% with no arguments 
echo %TEST_PROGRAM% -l "This is a test"
%TEST_PROGRAM% -l "This is a test">output\test%TEST%.output 2>output\test%TEST%.error
set LASTVALUE=%ERRORLEVEL%
echo Program returned %ERRORLEVEL% %LASTVALUE%
if "%LASTVALUE%"=="0" echo [32mSuccess[0m & type output\test%TEST%.output
if NOT "%LASTVALUE%"=="0" IF EXIST output\test%TEST%.output type output\test%TEST%.output
if NOT "%LASTVALUE%"=="0" echo [31mFailure[0m & type output\test%TEST%.error
IF NOT EXIST expect\TEST%TEST%.expect goto MissingExpect
for /f "" %%A IN (expect\TEST%TEST%.expect) DO set LAST_EXPECT=%%A
if "%LASTVALUE%"=="%LAST_EXPECT%" echo [32mResult Expected[0m 
if NOT "%LASTVALUE%"=="%LAST_EXPECT%" echo [31mResult Not Expected[0m & goto FAILEXPECT
echo .

set /A TEST=%TEST%+1
echo TEST[%TEST%] Calling %TEST_PROGRAM% with no arguments 
echo %TEST_PROGRAM% --lines "This is a test"
%TEST_PROGRAM% --lines "This is a test">output\test%TEST%.output 2>output\test%TEST%.error
set LASTVALUE=%ERRORLEVEL%
echo Program returned %ERRORLEVEL% %LASTVALUE%
if "%LASTVALUE%"=="0" echo [32mSuccess[0m & type output\test%TEST%.output
if NOT "%LASTVALUE%"=="0" IF EXIST output\test%TEST%.output type output\test%TEST%.output
if NOT "%LASTVALUE%"=="0" echo [31mFailure[0m & type output\test%TEST%.error
IF NOT EXIST expect\TEST%TEST%.expect goto MissingExpect
for /f "" %%A IN (expect\TEST%TEST%.expect) DO set LAST_EXPECT=%%A
if "%LASTVALUE%"=="%LAST_EXPECT%" echo [32mResult Expected[0m 
if NOT "%LASTVALUE%"=="%LAST_EXPECT%" echo [31mResult Not Expected[0m & goto FAILEXPECT
echo .

REM ##################################################

set /A TEST=%TEST%+1
echo TEST[%TEST%] Calling %TEST_PROGRAM% with no arguments 
echo %TEST_PROGRAM% -w -f test\TEST1.test
%TEST_PROGRAM% -w -f test\TEST1.test>output\test%TEST%.output 2>output\test%TEST%.error
set LASTVALUE=%ERRORLEVEL%
echo Program returned %ERRORLEVEL% %LASTVALUE%
if "%LASTVALUE%"=="0" echo [32mSuccess[0m & type output\test%TEST%.output
if NOT "%LASTVALUE%"=="0" IF EXIST output\test%TEST%.output type output\test%TEST%.output
if NOT "%LASTVALUE%"=="0" echo [31mFailure[0m & type output\test%TEST%.error
IF NOT EXIST expect\TEST%TEST%.expect goto MissingExpect
for /f "" %%A IN (expect\TEST%TEST%.expect) DO set LAST_EXPECT=%%A
if "%LASTVALUE%"=="%LAST_EXPECT%" echo [32mResult Expected[0m 
if NOT "%LASTVALUE%"=="%LAST_EXPECT%" echo [31mResult Not Expected[0m & goto FAILEXPECT
echo .

set /A TEST=%TEST%+1
echo TEST[%TEST%] Calling %TEST_PROGRAM% with no arguments 
echo %TEST_PROGRAM% --words --file test\TEST1.test
%TEST_PROGRAM% --words --file test\TEST1.test>output\test%TEST%.output 2>output\test%TEST%.error
set LASTVALUE=%ERRORLEVEL%
echo Program returned %ERRORLEVEL% %LASTVALUE%
if "%LASTVALUE%"=="0" echo [32mSuccess[0m & type output\test%TEST%.output
if NOT "%LASTVALUE%"=="0" IF EXIST output\test%TEST%.output type output\test%TEST%.output
if NOT "%LASTVALUE%"=="0" echo [31mFailure[0m & type output\test%TEST%.error
IF NOT EXIST expect\TEST%TEST%.expect goto MissingExpect
for /f "" %%A IN (expect\TEST%TEST%.expect) DO set LAST_EXPECT=%%A
if "%LASTVALUE%"=="%LAST_EXPECT%" echo [32mResult Expected[0m 
if NOT "%LASTVALUE%"=="%LAST_EXPECT%" echo [31mResult Not Expected[0m & goto FAILEXPECT
echo .

set /A TEST=%TEST%+1
echo TEST[%TEST%] Calling %TEST_PROGRAM% with no arguments 
echo %TEST_PROGRAM% -d -f test\TEST1.test
%TEST_PROGRAM% -d -f test\TEST1.test>output\test%TEST%.output 2>output\test%TEST%.error
set LASTVALUE=%ERRORLEVEL%
echo Program returned %ERRORLEVEL% %LASTVALUE%
if "%LASTVALUE%"=="0" echo [32mSuccess[0m & type output\test%TEST%.output
if NOT "%LASTVALUE%"=="0" IF EXIST output\test%TEST%.output type output\test%TEST%.output
if NOT "%LASTVALUE%"=="0" echo [31mFailure[0m & type output\test%TEST%.error
IF NOT EXIST expect\TEST%TEST%.expect goto MissingExpect
for /f "" %%A IN (expect\TEST%TEST%.expect) DO set LAST_EXPECT=%%A
if "%LASTVALUE%"=="%LAST_EXPECT%" echo [32mResult Expected[0m 
if NOT "%LASTVALUE%"=="%LAST_EXPECT%" echo [31mResult Not Expected[0m & goto FAILEXPECT
echo .

set /A TEST=%TEST%+1
echo TEST[%TEST%] Calling %TEST_PROGRAM% with no arguments 
echo %TEST_PROGRAM% --distinct --file test\TEST1.test
%TEST_PROGRAM% --distinct --file test\TEST1.test>output\test%TEST%.output 2>output\test%TEST%.error
set LASTVALUE=%ERRORLEVEL%
echo Program returned %ERRORLEVEL% %LASTVALUE%
if "%LASTVALUE%"=="0" echo [32mSuccess[0m & type output\test%TEST%.output
if NOT "%LASTVALUE%"=="0" IF EXIST output\test%TEST%.output type output\test%TEST%.output
if NOT "%LASTVALUE%"=="0" echo [31mFailure[0m & type output\test%TEST%.error
IF NOT EXIST expect\TEST%TEST%.expect goto MissingExpect
for /f "" %%A IN (expect\TEST%TEST%.expect) DO set LAST_EXPECT=%%A
if "%LASTVALUE%"=="%LAST_EXPECT%" echo [32mResult Expected[0m 
if NOT "%LASTVALUE%"=="%LAST_EXPECT%" echo [31mResult Not Expected[0m & goto FAILEXPECT
echo .

set /A TEST=%TEST%+1
echo TEST[%TEST%] Calling %TEST_PROGRAM% with no arguments 
echo %TEST_PROGRAM% -c -f test\TEST1.test
%TEST_PROGRAM% -c -f test\TEST1.test>output\test%TEST%.output 2>output\test%TEST%.error
set LASTVALUE=%ERRORLEVEL%
echo Program returned %ERRORLEVEL% %LASTVALUE%
if "%LASTVALUE%"=="0" echo [32mSuccess[0m & type output\test%TEST%.output
if NOT "%LASTVALUE%"=="0" IF EXIST output\test%TEST%.output type output\test%TEST%.output
if NOT "%LASTVALUE%"=="0" echo [31mFailure[0m & type output\test%TEST%.error
IF NOT EXIST expect\TEST%TEST%.expect goto MissingExpect
for /f "" %%A IN (expect\TEST%TEST%.expect) DO set LAST_EXPECT=%%A
if "%LASTVALUE%"=="%LAST_EXPECT%" echo [32mResult Expected[0m 
if NOT "%LASTVALUE%"=="%LAST_EXPECT%" echo [31mResult Not Expected[0m & goto FAILEXPECT
echo .

set /A TEST=%TEST%+1
echo TEST[%TEST%] Calling %TEST_PROGRAM% with no arguments 
echo %TEST_PROGRAM% --characters --file test\TEST1.test
%TEST_PROGRAM% --characters --file test\TEST1.test>output\test%TEST%.output 2>output\test%TEST%.error
set LASTVALUE=%ERRORLEVEL%
echo Program returned %ERRORLEVEL% %LASTVALUE%
if "%LASTVALUE%"=="0" echo [32mSuccess[0m & type output\test%TEST%.output
if NOT "%LASTVALUE%"=="0" IF EXIST output\test%TEST%.output type output\test%TEST%.output
if NOT "%LASTVALUE%"=="0" echo [31mFailure[0m & type output\test%TEST%.error
IF NOT EXIST expect\TEST%TEST%.expect goto MissingExpect
for /f "" %%A IN (expect\TEST%TEST%.expect) DO set LAST_EXPECT=%%A
if "%LASTVALUE%"=="%LAST_EXPECT%" echo [32mResult Expected[0m 
if NOT "%LASTVALUE%"=="%LAST_EXPECT%" echo [31mResult Not Expected[0m & goto FAILEXPECT
echo .

set /A TEST=%TEST%+1
echo TEST[%TEST%] Calling %TEST_PROGRAM% with no arguments 
echo %TEST_PROGRAM% -h -f test\TEST1.test
%TEST_PROGRAM% -h -f test\TEST1.test>output\test%TEST%.output 2>output\test%TEST%.error
set LASTVALUE=%ERRORLEVEL%
echo Program returned %ERRORLEVEL% %LASTVALUE%
if "%LASTVALUE%"=="0" echo [32mSuccess[0m & type output\test%TEST%.output
if NOT "%LASTVALUE%"=="0" IF EXIST output\test%TEST%.output type output\test%TEST%.output
if NOT "%LASTVALUE%"=="0" echo [31mFailure[0m & type output\test%TEST%.error
IF NOT EXIST expect\TEST%TEST%.expect goto MissingExpect
for /f "" %%A IN (expect\TEST%TEST%.expect) DO set LAST_EXPECT=%%A
if "%LASTVALUE%"=="%LAST_EXPECT%" echo [32mResult Expected[0m 
if NOT "%LASTVALUE%"=="%LAST_EXPECT%" echo [31mResult Not Expected[0m & goto FAILEXPECT
echo .

set /A TEST=%TEST%+1
echo TEST[%TEST%] Calling %TEST_PROGRAM% with no arguments 
echo %TEST_PROGRAM% --histogram --file test\TEST1.test
%TEST_PROGRAM% --histogram --file test\TEST1.test>output\test%TEST%.output 2>output\test%TEST%.error
set LASTVALUE=%ERRORLEVEL%
echo Program returned %ERRORLEVEL% %LASTVALUE%
if "%LASTVALUE%"=="0" echo [32mSuccess[0m & type output\test%TEST%.output
if NOT "%LASTVALUE%"=="0" IF EXIST output\test%TEST%.output type output\test%TEST%.output
if NOT "%LASTVALUE%"=="0" echo [31mFailure[0m & type output\test%TEST%.error
IF NOT EXIST expect\TEST%TEST%.expect goto MissingExpect
for /f "" %%A IN (expect\TEST%TEST%.expect) DO set LAST_EXPECT=%%A
if "%LASTVALUE%"=="%LAST_EXPECT%" echo [32mResult Expected[0m 
if NOT "%LASTVALUE%"=="%LAST_EXPECT%" echo [31mResult Not Expected[0m & goto FAILEXPECT
echo .

set /A TEST=%TEST%+1
echo TEST[%TEST%] Calling %TEST_PROGRAM% with no arguments 
echo %TEST_PROGRAM% -l -f test\TEST1.test
%TEST_PROGRAM% -l -f test\TEST1.test>output\test%TEST%.output 2>output\test%TEST%.error
set LASTVALUE=%ERRORLEVEL%
echo Program returned %ERRORLEVEL% %LASTVALUE%
if "%LASTVALUE%"=="0" echo [32mSuccess[0m & type output\test%TEST%.output
if NOT "%LASTVALUE%"=="0" IF EXIST output\test%TEST%.output type output\test%TEST%.output
if NOT "%LASTVALUE%"=="0" echo [31mFailure[0m & type output\test%TEST%.error
IF NOT EXIST expect\TEST%TEST%.expect goto MissingExpect
for /f "" %%A IN (expect\TEST%TEST%.expect) DO set LAST_EXPECT=%%A
if "%LASTVALUE%"=="%LAST_EXPECT%" echo [32mResult Expected[0m 
if NOT "%LASTVALUE%"=="%LAST_EXPECT%" echo [31mResult Not Expected[0m & goto FAILEXPECT
echo .

set /A TEST=%TEST%+1
echo TEST[%TEST%] Calling %TEST_PROGRAM% with no arguments 
echo %TEST_PROGRAM% --lines --file test\TEST1.test
%TEST_PROGRAM% --lines --file test\TEST1.test>output\test%TEST%.output 2>output\test%TEST%.error
set LASTVALUE=%ERRORLEVEL%
echo Program returned %ERRORLEVEL% %LASTVALUE%
if "%LASTVALUE%"=="0" echo [32mSuccess[0m & type output\test%TEST%.output
if NOT "%LASTVALUE%"=="0" IF EXIST output\test%TEST%.output type output\test%TEST%.output
if NOT "%LASTVALUE%"=="0" echo [31mFailure[0m & type output\test%TEST%.error
IF NOT EXIST expect\TEST%TEST%.expect goto MissingExpect
for /f "" %%A IN (expect\TEST%TEST%.expect) DO set LAST_EXPECT=%%A
if "%LASTVALUE%"=="%LAST_EXPECT%" echo [32mResult Expected[0m 
if NOT "%LASTVALUE%"=="%LAST_EXPECT%" echo [31mResult Not Expected[0m & goto FAILEXPECT
echo .

set /A TEST=%TEST%+1
echo TEST[%TEST%] Calling %TEST_PROGRAM% with no arguments 
echo %TEST_PROGRAM% -l -f test\TEST0.test
%TEST_PROGRAM% -l -f test\TEST0.test>output\test%TEST%.output 2>output\test%TEST%.error
set LASTVALUE=%ERRORLEVEL%
echo Program returned %ERRORLEVEL% %LASTVALUE%
if "%LASTVALUE%"=="0" echo [32mSuccess[0m & type output\test%TEST%.output
if NOT "%LASTVALUE%"=="0" IF EXIST output\test%TEST%.output type output\test%TEST%.output
if NOT "%LASTVALUE%"=="0" echo [31mFailure[0m & type output\test%TEST%.error
IF NOT EXIST expect\TEST%TEST%.expect goto MissingExpect
for /f "" %%A IN (expect\TEST%TEST%.expect) DO set LAST_EXPECT=%%A
if "%LASTVALUE%"=="%LAST_EXPECT%" echo [32mResult Expected[0m 
if NOT "%LASTVALUE%"=="%LAST_EXPECT%" echo [31mResult Not Expected[0m & goto FAILEXPECT
echo .

set /A TEST=%TEST%+1
echo TEST[%TEST%] Calling %TEST_PROGRAM% with no arguments 
echo %TEST_PROGRAM% --lines --file test\TEST0.test
%TEST_PROGRAM% --lines --file test\TEST0.test>output\test%TEST%.output 2>output\test%TEST%.error
set LASTVALUE=%ERRORLEVEL%
echo Program returned %ERRORLEVEL% %LASTVALUE%
if "%LASTVALUE%"=="0" echo [32mSuccess[0m & type output\test%TEST%.output
if NOT "%LASTVALUE%"=="0" IF EXIST output\test%TEST%.output type output\test%TEST%.output
if NOT "%LASTVALUE%"=="0" echo [31mFailure[0m & type output\test%TEST%.error
IF NOT EXIST expect\TEST%TEST%.expect goto MissingExpect
for /f "" %%A IN (expect\TEST%TEST%.expect) DO set LAST_EXPECT=%%A
if "%LASTVALUE%"=="%LAST_EXPECT%" echo [32mResult Expected[0m 
if NOT "%LASTVALUE%"=="%LAST_EXPECT%" echo [31mResult Not Expected[0m & goto FAILEXPECT
echo .

exit /b 0

:END
echo Failed to compile
exit /b 1

:FAILEXPECT
echo Failed expected Result

exit /b 1

:MissingExpect
echo Missing the expect file
exit /b 1