@echo off

start cmd.exe /c start "" http://127.0.0.1:5000/

python -m venv .venv/

call .\.venv\Scripts\activate
pip install flask
pip install xlsxwriter

flask --app App run

pause