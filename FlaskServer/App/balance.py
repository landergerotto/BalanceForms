from flask import Flask, Blueprint, render_template, request, redirect
import xlsxwriter as xl
from datetime import datetime
import os
import time

app = Flask(__name__)
bp = Blueprint('balance', __name__, url_prefix='/', static_folder='static')

test_started = 0
answer_row = 2
crr = 0

workbook = None
worksheet1 = None
worksheet2 = None
user_format = None
percentage_format = None
answers_format_correct = None
answers_format_wrong = None
worksheets = None
correct_answers = None

def createWorkbook():
    global answer_row
    global workbook
    global worksheet1
    global worksheet2
    global user_format
    global percentage_format
    global answers_format_correct
    global answers_format_wrong
    global worksheets
    global correct_answers
    global crr
    
    answer_row = 2

    current_date = datetime.now()
    midday = current_date.replace(hour=12, minute=0, second=0, microsecond=0)
    date = current_date.strftime('%d-%m-%Y')
    period = 'Manhã' if current_date < midday else 'Tarde'

    if not os.path.exists('../Provas'): 
        os.mkdir('../Provas')

    if not os.path.exists(f'../Provas/{date}'):
        os.mkdir(f'../Provas/{date}')

    workbook = xl.Workbook(f'../Provas/{date}/{period}.xlsx')
    worksheet1 = workbook.add_worksheet('Prova1')
    worksheet2 = workbook.add_worksheet('Prova2')

    head_format = workbook.add_format({
        'bold': 1,
        'border': 1,
        'bg_color': '#123499',
        'font_color': 'white',
        'align': 'center',
        'valign': 'vcenter'
    })
    user_format = workbook.add_format({
        'align': 'center',
        'valign': 'vcenter',
        'border': 1
    })
    percentage_format = workbook.add_format({
        'align': 'center',
        'valign': 'vcenter',
        'num_format': '0%',
        'border': 1
    })
    answers_format_correct = workbook.add_format({
        'align': 'center',
        'valign': 'vcenter',
        'bg_color': '#39e75f',
        'border': 1
    })
    answers_format_wrong = workbook.add_format({
        'align': 'center',
        'valign': 'vcenter',
        'bg_color': '#ff7f7f',
        'border': 1
    })

    worksheets = [worksheet1, worksheet2]
    correct_answers = [[500, 1000, 750, 200, 100], [500, 675, 600, 50, 25]]
    for index, worksheet in enumerate(worksheets):
        first_line = ['Nome', 'Data de nascimento', 'Triângulo', 'Quadrado', 'Círculo', 'Estrela', 'Hexágono', 'Tempo de prova', 'Quantidade de peças utilizadas', '% de acertos']
        for i in range(len(first_line)):
            if i > 1 and i < 7:
                worksheet.set_column(i, i, len(first_line[i]))
                worksheet.write(0, i, first_line[i], head_format)
                worksheet.write(1, i, correct_answers[index][crr], head_format)
                crr += 1
                continue
            if first_line[i] != 'Nome':
                worksheet.set_column(i, i, len(first_line[i]))
            else:
                worksheet.set_column(i, i, 30)
            worksheet.merge_range(0, i, 1, i, first_line[i], head_format)
        crr = 0

def time_formatter(time):
    hours = int(time) // 3600
    minutes = (int(time) % 3600) // 60
    seconds = int(time) % 60
    time = f"{hours:02d}:{minutes:02d}:{seconds:02d}"
    return time

@bp.route('/', methods=['GET', 'POST'])
def index():
    global test_started
    if request.method == 'GET':
        test_started = 0
        createWorkbook()
        return render_template(
            'index.html',
        )
    if workbook == None:
        return redirect('/')
    test_started = 2
    time.sleep(1)
    test_started = 0
    workbook.close()
    return render_template(
            'final.html',
        )
    
@bp.route('/timer', methods=['GET', 'POST'])
def timer():
    if request.method == 'GET':
        return redirect('/')
    if workbook == None:
        return redirect('/')
    global test_started
    minutes = request.form['minutes']
    if minutes == '':
        minutes = '30'
    test_started = 1
    return render_template(
        'timer.html',
        minutes = minutes
    )

@bp.route('/test', methods=['GET', 'POST'])
def test_status():
    if request.method == 'GET':
        return {'response': test_started}, 200
    if test_started != 0:
        user_data = request.json
        global crr, answer_row, correct_answers

        tests = [list(user_data['prova1'].values()), list(user_data['prova2'].values())]
        
        tests[0][5] = time_formatter(tests[0][5])
        tests[1][5] = time_formatter(tests[1][5])

        for index, worksheet in enumerate(worksheets):
            worksheet.write(answer_row, 0, user_data['nome'], user_format)
            worksheet.write(answer_row, 1, user_data['nascimento'], user_format)
            worksheet.write(answer_row, 7, tests[index][5], user_format)
            worksheet.write(answer_row, 8, tests[index][6], user_format)
            worksheet.write(answer_row, 9, tests[index][7], percentage_format)
            for i in range(2, 7):
                worksheet.write(answer_row, i, tests[index][crr], answers_format_correct if tests[index][crr] == correct_answers[index][crr] else answers_format_wrong)
                crr += 1
            crr = 0
        answer_row += 1
                
        return 'Respostas enviadas com sucesso.', 201
    return 'A prova ainda não foi iniciada ou já foi finalizada.', 403
    