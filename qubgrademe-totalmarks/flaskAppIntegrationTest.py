import pytest
from requests import *

URLFORTOTALMARKS = "http://qubgrademe-totalmarks.40266405.qpc.hal.davecutting.uk"

# Testing welcome with valid data
def test_Welcome():
    url = '/'
    response = get(URLFORTOTALMARKS + url)
    print(response.text)
    assert response.text == '{"message":"Welcome to the total marks flask server"}\n'
    assert response.status_code == 200

# Testing total marks with valid data
def test_TotalMarks():
    url = '/totalmarks?mark1=75&mark2=75&mark3=75&mark4=57&mark5=87'
    response = get(URLFORTOTALMARKS + url)
    assert response.text == '{"message":"Your total marks are: 369"}\n'
    assert response.status_code == 200

# Testing the total marks with missing module 1
def test_TotalMarksMissingmark1():
    url = '/totalmarks?mark2=75&mark3=75&mark4=75&mark5=75'
    response = get(URLFORTOTALMARKS + url)
    assert response.text == '{"message":"Mark 1 needs to be an integer"}\n'
    assert response.status_code == 400

# Testing the total marks with missing module 2
def test_TotalMarksMissingmark2():
    url = '/totalmarks?mark1=75&mark3=75&mark4=75&mark5=75'
    response = get(URLFORTOTALMARKS + url)
    assert response.text == '{"message":"Mark 2 needs to be an integer"}\n'
    assert response.status_code == 400

# Testing the total marks with missing module 3
def test_TotalMarksMissingmark3():
    url = '/totalmarks?mark1=75&mark2=75&mark4=75&mark5=75'
    response = get(URLFORTOTALMARKS + url)
    assert response.text == '{"message":"Mark 3 needs to be an integer"}\n'
    assert response.status_code == 400

# Testing the total marks with missing module 4
def test_TotalMarksMissingmark4():
    url = '/totalmarks?mark1=75&mark3=75&mark2=75&mark5=75'
    response = get(URLFORTOTALMARKS + url)
    assert response.text == '{"message":"Mark 4 needs to be an integer"}\n'
    assert response.status_code == 400

# Testing the total marks with missing module 5
def test_TotalMarksMissingmark5():
    url = '/totalmarks?mark1=75&mark3=75&mark2=75&mark4=75'
    response = get(URLFORTOTALMARKS + url)
    assert response.text == '{"message":"Mark 5 needs to be an integer"}\n'
    assert response.status_code == 400

# Testing the total marks with a module as a string
def test_TotalMarksMissingmark5():
    url = '/totalmarks?mark1=75&mark3=75&mark2=fds&mark4=76&mark5=67'
    response = get(URLFORTOTALMARKS + url)
    assert response.text == '{"message":"Mark 2 needs to be an integer"}\n'
    assert response.status_code == 400

# Testing the total marks with a module being below 0
def test_TotalMarksModuleBelowZero1():
    url = '/totalmarks?mark1=-100&mark3=75&mark2=75&mark4=75'
    response = get(URLFORTOTALMARKS + url)
    assert response.text == '{"message":"Mark 1 needs to be greater than zero"}\n'
    assert response.status_code == 400

# Testing the total marks with a module being below 0
def test_TotalMarksModuleBelowZero2():
    url = '/totalmarks?mark2=-100&mark3=75&mark1=75&mark4=75&mark5=75'
    response = get(URLFORTOTALMARKS + url)
    assert response.text == '{"message":"Mark 2 needs to be greater than zero"}\n'
    assert response.status_code == 400

# Testing the total marks with a module being above 100
def test_TotalMarksAboveonehundred1():
    url = '/totalmarks?mark2=121&mark3=75&mark1=75&mark4=75&mark5=75'
    response = get(URLFORTOTALMARKS + url)
    assert response.text == '{"message":"Mark 2 needs to be less than one hundred"}\n'
    assert response.status_code == 400

# Testing the total marks with a module being above 100
def test_TotalMarksAboveonehundred2():
    url = '/totalmarks?mark2=2&mark3=156&mark1=75&mark4=75&mark5=75'
    response = get(URLFORTOTALMARKS + url)
    assert response.text == '{"message":"Mark 3 needs to be less than one hundred"}\n'
    assert response.status_code == 400

# Testing the total marks with a blank module 
def test_TotalMarksBlankModule():
    url = '/totalmarks?mark2=&mark3=16&mark1=75&mark4=75&mark5=75'
    response = get(URLFORTOTALMARKS + url)
    assert response.text == '{"message":"Mark 2 needs to be an integer"}\n'
    assert response.status_code == 400


# @pytest.fixture
# def requests():
#     print("Generating requests")
#     requests = app.test_requests()
#     return requests

