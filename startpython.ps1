cd $PWD

$server = "$PWD\FlaskServer"
python -m venv "$server\.venv"

cd $server

& "$server\.venv\Scripts\Activate.ps1"

pip install flask
pip install xlsxwriter

flask --app App run --debug

cd $PWD