import unittest
import azure.functions as func
from requests import *

from qubgrademe_classifygrade_function import main
from unittest import mock



class TestQubGradeMeClassifyGradeIntegrationTest(unittest.TestCase):
    URLFORCLASSIFYGRADE = " https://qubgrademe-classifygrade.azurewebsites.net/api/qubgrademe-classifygrade"

    # Test main with valid data for a first
    def test_main(self):
        url = '?mark1=75&mark2=75&mark3=75&mark4=57&mark5=87'
        response = get(self.URLFORCLASSIFYGRADE + url)
        assert "First Class" in response.text 
        assert response.status_code == 200

    # Test main with valid data for a 2:1
    def test_main21(self):
        url = '?mark1=62&mark2=65&mark3=67&mark4=61&mark5=59'
        response = get(self.URLFORCLASSIFYGRADE + url)
        assert "2:1" in response.text 
        assert response.status_code == 200

    # Test main with valid data for a 2:2
    def test_main22(self):
        url = '?mark1=56&mark2=61&mark3=51&mark4=49&mark5=55'
        response = get(self.URLFORCLASSIFYGRADE + url)
        assert "2:2" in response.text 
        assert response.status_code == 200

    # Test main with valid data for a third class
    def test_mainthirdclass(self):
        url = '?mark1=40&mark2=45&mark3=43&mark4=49&mark5=43'
        response = get(self.URLFORCLASSIFYGRADE + url)
        assert "Third Class" in response.text 
        assert response.status_code == 200

    # Test main with valid data for a pass
    def test_mainpass(self):
        url = '?mark1=35&mark2=37&mark3=25&mark4=35&mark5=37'
        response = get(self.URLFORCLASSIFYGRADE + url)
        assert "Pass" in response.text 
        assert response.status_code == 200

    # Test main with a string as one of the modules
    def test_mainStringInmark1(self):
        url = '?mark1=al&mark2=37&mark3=25&mark4=35&mark5=37'
        response = get(self.URLFORCLASSIFYGRADE + url)
        assert "Missing module or invalid data for mark 1" in response.text 
        assert response.status_code == 400

    # Test main with a string as one of the modules
    def test_mainStringInmark4(self):
        url = '?mark1=78&mark2=37&mark3=25&mark4=po&mark5=37'
        response = get(self.URLFORCLASSIFYGRADE + url)
        assert "Missing module or invalid data for mark 4" in response.text 
        assert response.status_code == 400

    # Test main with mark 1 missing
    def test_mainStringInmark1Missing(self):
        url = '?&mark2=37&mark3=25&mark4=78&mark5=37'
        response = get(self.URLFORCLASSIFYGRADE + url)
        assert "Missing module or invalid data for mark 1" in response.text 
        assert response.status_code == 400

    # Test main with mark 2 missing
    def test_mainStringInmark2Missing(self):
        url = '?mark1=78&mark3=25&mark4=78&mark5=37'
        response = get(self.URLFORCLASSIFYGRADE + url)
        assert "Missing module or invalid data for mark 2" in response.text 
        assert response.status_code == 400

    # Test main with mark 3 missing
    def test_mainStringInmark3Missing(self):
        url = '?mark1=78&mark2=37&mark4=78&mark5=37'
        response = get(self.URLFORCLASSIFYGRADE + url)
        assert "Missing module or invalid data for mark 3" in response.text 
        assert response.status_code == 400

    # Test main with mark 4 missing
    def test_mainStringInmark4Missing(self):
        url = '?mark1=78&mark2=37&mark3=78&mark5=37'
        response = get(self.URLFORCLASSIFYGRADE + url)
        assert "Missing module or invalid data for mark 4" in response.text 
        assert response.status_code == 400

    # Test main with amark 5 missing
    def test_mainStringInmark5Missing(self):
        url = '?mark1=78&mark2=37&mark3=78&mark4=37'
        response = get(self.URLFORCLASSIFYGRADE + url)
        assert "Missing module or invalid data for mark 5" in response.text 
        assert response.status_code == 400

    # Test main with a blank as one of the modules
    def test_mainStringInmark5Blank(self):
        url = '?mark1=78&mark2=37&mark3=78&mark4=37&mark5='
        response = get(self.URLFORCLASSIFYGRADE + url)
        assert "Missing module or invalid data for mark 5" in response.text 
        assert response.status_code == 400
    