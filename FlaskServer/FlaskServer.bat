@echo off

start cmd.exe /c start "" http://127.0.0.1:5000/

call .\.venv\Scripts\activate
flask --app App run