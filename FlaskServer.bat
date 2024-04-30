@echo off

cd .\FlaskServer\

if not exist ".venv/" echo Instalando as bibliotecas necessarias... & python -m venv .venv/ & call .\.venv\Scripts\activate & pip install flask & pip install xlsxwriter

start cmd.exe /c start "" http://127.0.0.1:5000/

cls
echo Manter o console aberto para que o servidor funcione apropriadamente.
call .\.venv\Scripts\activate
flask --app App run