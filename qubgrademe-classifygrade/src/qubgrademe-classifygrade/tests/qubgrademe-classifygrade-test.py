import unittest
import azure.functions as func

from qubgrademe_classifygrade_function import main
from unittest import mock

class TestQubGradeMeClassifyGrade(unittest.TestCase):

    # Test main with valid data for a first
    def test_main(self):
        # Arrange 
        request = func.HttpRequest(
            method='GET',
            url='/api/qubgrademe-classifygrade',
            params={ 'mark1': '70', 'mark2': '70', 'mark3': '70', 'mark4': '70', 'mark5': '70'  },
            body=None
        )
        # Act
        response = main(request)
        # Assert
        # Assert we have a success code
        assert response.status_code == 200
        # Assert the response is as expected
        assert 'First Class' in response.get_body().decode() 

    # Test main with valid data for a 2:1
    def test_main21(self):
        # Arrange 
        request = func.HttpRequest(
            method='GET',
            url='/api/qubgrademe-classifygrade',
            params={ 'mark1': '60', 'mark2': '60', 'mark3': '60', 'mark4': '60', 'mark5': '60'  },
            body=None
        )
        # Act
        response = main(request)
        # Assert
        # Assert we have a success code
        assert response.status_code == 200
        # Assert the response is as expected
        assert '2:1' in response.get_body().decode() 

    # Test main with valid data for a 2:2
    def test_main22(self):
        # Arrange 
        request = func.HttpRequest(
            method='GET',
            url='/api/qubgrademe-classifygrade',
            params={ 'mark1': '50', 'mark2': '50', 'mark3': '50', 'mark4': '50', 'mark5': '50'  },
            body=None
        )
        # Act
        response = main(request)
        # Assert
        # Assert we have a success code
        assert response.status_code == 200
        # Assert the response is as expected
        assert '2:2' in response.get_body().decode() 


    # Test main with valid data for a third class
    def test_mainThirdClass(self):
        # Arrange 
        request = func.HttpRequest(
            method='GET',
            url='/api/qubgrademe-classifygrade',
            params={ 'mark1': '40', 'mark2': '40', 'mark3': '40', 'mark4': '40', 'mark5': '40'  },
            body=None
        )
        # Act
        response = main(request)
        # Assert
        # Assert we have a success code
        assert response.status_code == 200
        # Assert the response is as expected
        assert 'Third Class' in response.get_body().decode() 

    # Test main with valid data for a Pass
    def test_mainPass(self):
        # Arrange 
        request = func.HttpRequest(
            method='GET',
            url='/api/qubgrademe-classifygrade',
            params={ 'mark1': '39', 'mark2': '39', 'mark3': '39', 'mark4': '39', 'mark5': '39'  },
            body=None
        )
        # Act
        response = main(request)
        # Assert
        # Assert we have a success code
        assert response.status_code == 200
        # Assert the response is as expected
        assert 'Pass' in response.get_body().decode() 
    
    # Test main with a string as one of the modules
    def test_mainInvalidStringmark1(self):
        # Arrange 
        request = func.HttpRequest(
            method='GET',
            url='/api/qubgrademe-classifygrade',
            params={ 'mark1': 'ald', 'mark2': '39', 'mark3': '39', 'mark4': '39', 'mark5': '39'  },
            body=None
        )
        # Act
        response = main(request)
        # Assert
        # Assert we have a success code
        assert response.status_code == 400
        # Assert the response is as expected
        assert 'Missing module or invalid data for mark 1' in response.get_body().decode() 

    # Test main with a string as one of the modules
    def test_mainInvalidStringmark3(self):
        # Arrange 
        request = func.HttpRequest(
            method='GET',
            url='/api/qubgrademe-classifygrade',
            params={ 'mark1': '78', 'mark2': '39', 'mark3': 'ald', 'mark4': '39', 'mark5': '39'  },
            body=None
        )
        # Act
        response = main(request)
        # Assert
        # Assert we have a success code
        assert response.status_code == 400
        # Assert the response is as expected
        assert 'Missing module or invalid data for mark 3' in response.get_body().decode() 
    
    # Test main with a missing module 1
    def test_mainInvalidMissingmark1(self):
        # Arrange 
        request = func.HttpRequest(
            method='GET',
            url='/api/qubgrademe-classifygrade',
            params={ 'mark2': '39', 'mark3': '39', 'mark4': '39', 'mark5': '39'  },
            body=None
        )
        # Act
        response = main(request)
        # Assert
        # Assert we have a success code
        assert response.status_code == 400
        # Assert the response is as expected
        assert 'Missing module or invalid data for mark 1' in response.get_body().decode() 

    # Test main with a missing module 2
    def test_mainInvalidMissingmark2(self):
        # Arrange 
        request = func.HttpRequest(
            method='GET',
            url='/api/qubgrademe-classifygrade',
            params={ 'mark1': '39', 'mark3': '39', 'mark4': '39', 'mark5': '39'  },
            body=None
        )
        # Act
        response = main(request)
        # Assert
        # Assert we have a success code
        assert response.status_code == 400
        # Assert the response is as expected
        assert 'Missing module or invalid data for mark 2' in response.get_body().decode()

    # Test main with a missing module 3
    def test_mainInvalidMissingmark3(self):
        # Arrange 
        request = func.HttpRequest(
            method='GET',
            url='/api/qubgrademe-classifygrade',
            params={ 'mark1': '39', 'mark2': '39', 'mark4': '39', 'mark5': '39'  },
            body=None
        )
        # Act
        response = main(request)
        # Assert
        # Assert we have a success code
        assert response.status_code == 400
        # Assert the response is as expected
        assert 'Missing module or invalid data for mark 3' in response.get_body().decode()

    # Test main with a missing module 4
    def test_mainInvalidMissingmark4(self):
        # Arrange 
        request = func.HttpRequest(
            method='GET',
            url='/api/qubgrademe-classifygrade',
            params={ 'mark1': '39', 'mark2': '39', 'mark3': '39', 'mark5': '39'  },
            body=None
        )
        # Act
        response = main(request)
        # Assert
        # Assert we have a success code
        assert response.status_code == 400
        # Assert the response is as expected
        assert 'Missing module or invalid data for mark 4' in response.get_body().decode()  

    # Test main with a missing module 5
    def test_mainInvalidMissingmark5(self):
        # Arrange 
        request = func.HttpRequest(
            method='GET',
            url='/api/qubgrademe-classifygrade',
            params={ 'mark1': '39', 'mark2': '39', 'mark4': '39', 'mark3': '39'  },
            body=None
        )
        # Act
        response = main(request)
        # Assert
        # Assert we have a success code
        assert response.status_code == 400
        # Assert the response is as expected
        assert 'Missing module or invalid data for mark 5' in response.get_body().decode()  

    # Test main with a blank module
    def test_mainInvalidBlankMark1(self):
        # Arrange 
        request = func.HttpRequest(
            method='GET',
            url='/api/qubgrademe-classifygrade',
            params={ 'mark1': '', 'mark2': '39', 'mark4': '39', 'mark3': '39'  },
            body=None
        )
        # Act
        response = main(request)
        # Assert
        # Assert we have a success code
        assert response.status_code == 400
        # Assert the response is as expected
        assert 'Missing module or invalid data for mark 1' in response.get_body().decode()  

    