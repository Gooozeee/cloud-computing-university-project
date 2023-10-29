import pytest
from flaskApp import app

# Testing welcome with valid data
def test_Welcome(client):
    url = '/'
    print("Testing welcome")
    response = client.get(url)
    assert response.get_data() == b'{"message":"Welcome to the total marks flask server"}\n'
    assert response.status_code == 200

# Testing total marks with valid data
def test_TotalMarks(client):
    url = '/totalmarks?mark1=75&mark2=75&mark3=75&mark4=57&mark5=87'
    print("Testing total marks")
    response = client.get(url)
    assert response.get_data() == b'{"message":"Your total marks are: 369"}\n'
    assert response.status_code == 200

# Testing the total marks with missing module 1
def test_TotalMarksMissingmark1(client):
    url = '/totalmarks?mark2=75&mark3=75&mark4=75&mark5=75'
    print("Testing total marks")
    response = client.get(url)
    assert response.get_data() == b'{"message":"Mark 1 needs to be an integer"}\n'
    assert response.status_code == 400

# Testing the total marks with missing module 2
def test_TotalMarksMissingmark2(client):
    url = '/totalmarks?mark1=75&mark3=75&mark4=75&mark5=75'
    print("Testing total marks")
    response = client.get(url)
    assert response.get_data() == b'{"message":"Mark 2 needs to be an integer"}\n'
    assert response.status_code == 400

# Testing the total marks with missing module 3
def test_TotalMarksMissingmark3(client):
    url = '/totalmarks?mark1=75&mark2=75&mark4=75&mark5=75'
    print("Testing total marks")
    response = client.get(url)
    assert response.get_data() == b'{"message":"Mark 3 needs to be an integer"}\n'
    assert response.status_code == 400

# Testing the total marks with missing module 4
def test_TotalMarksMissingmark4(client):
    url = '/totalmarks?mark1=75&mark3=75&mark2=75&mark5=75'
    print("Testing total marks")
    response = client.get(url)
    assert response.get_data() == b'{"message":"Mark 4 needs to be an integer"}\n'
    assert response.status_code == 400

# Testing the total marks with missing module 5
def test_TotalMarksMissingmark5(client):
    url = '/totalmarks?mark1=75&mark3=75&mark2=75&mark4=75'
    print("Testing total marks")
    response = client.get(url)
    assert response.get_data() == b'{"message":"Mark 5 needs to be an integer"}\n'
    assert response.status_code == 400

# Testing the total marks with a module as a string
def test_TotalMarksMark4AsAString(client):
    url = '/totalmarks?mark1=75&mark3=75&mark2=75&mark4=ads&mark5=87'
    print("Testing total marks")
    response = client.get(url)
    assert response.get_data() == b'{"message":"Mark 4 needs to be an integer"}\n'
    assert response.status_code == 400

# Testing the total marks with a module being below 0
def test_TotalMarksModuleBelowZero1(client):
    url = '/totalmarks?mark1=-100&mark3=75&mark2=75&mark4=75'
    print("Testing total marks")
    response = client.get(url)
    assert response.get_data() == b'{"message":"Mark 1 needs to be greater than zero"}\n'
    assert response.status_code == 400

# Testing the total marks with a module being below 0
def test_TotalMarksModuleBelowZero2(client):
    url = '/totalmarks?mark2=-100&mark3=75&mark1=75&mark4=75&mark5=75'
    print("Testing total marks")
    response = client.get(url)
    assert response.get_data() == b'{"message":"Mark 2 needs to be greater than zero"}\n'
    assert response.status_code == 400

# Testing the total marks with a module being above 100
def test_TotalMarksAboveonehundred1(client):
    url = '/totalmarks?mark2=121&mark3=75&mark1=75&mark4=75&mark5=75'
    print("Testing total marks")
    response = client.get(url)
    assert response.get_data() == b'{"message":"Mark 2 needs to be less than one hundred"}\n'
    assert response.status_code == 400

# Testing the total marks with a module being above 100
def test_TotalMarksAboveonehundred2(client):
    url = '/totalmarks?mark2=2&mark3=156&mark1=75&mark4=75&mark5=75'
    print("Testing total marks")
    response = client.get(url)
    assert response.get_data() == b'{"message":"Mark 3 needs to be less than one hundred"}\n'
    assert response.status_code == 400

# Testing the total marks with a module being blank
def test_TotalMarksAboveonehundred2(client):
    url = '/totalmarks?mark2=&mark3=15&mark1=75&mark4=75&mark5=75'
    print("Testing total marks")
    response = client.get(url)
    assert response.get_data() == b'{"message":"Mark 2 needs to be an integer"}\n'
    assert response.status_code == 400

@pytest.fixture
def client():
    print("Generating client")
    client = app.test_client()
    return client

