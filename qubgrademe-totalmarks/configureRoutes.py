# Importing the right flask libraries
from flask import Flask, Response, jsonify, request, make_response
from flask_cors import CORS, cross_origin

def configureRoutes(app):
    # GET /totalmarks
    # Add the total marks in the URL to make the calculation
    @app.route('/totalmarks', methods=["GET"])
    def getTotalMarks():
        # Getting the module parameters (if the module isn't in the URL it will be set to -1)
        mark1 = request.args.get('mark1', 0, type=str)
        mark2 = request.args.get('mark2', 0, type=str)
        mark3 = request.args.get('mark3', 0, type=str)
        mark4 = request.args.get('mark4', 0, type=str)
        mark5 = request.args.get('mark5', 0, type=str)


        # Creating an array to store these parameters
        moduleArray = []

        # Appending these paramters to a module list
        moduleArray.append(mark1)
        moduleArray.append(mark2)
        moduleArray.append(mark3)
        moduleArray.append(mark4)
        moduleArray.append(mark5)

        # Initialising the totalmarks variable to return
        totalMarks = 0

        # Looping through the array and calculating the total marks
        for i in range(0,5):
            # Checking if the marks are integers
            try:
                moduleArray[i] = int(moduleArray[i].strip())
            except:
                response = make_response(jsonify({'message':"Mark " + str(i+1)  + " needs to be an integer"}))
                response.headers.add("Access-Control-Allow-Origin", "*")
                return response, 400

            # If the module is less than zero it return that it is invalid
            if moduleArray[i] <= 0:
                response = make_response(jsonify({'message': "Mark " + str(i+1) + " needs to be greater than zero"}))
                response.headers.add("Access-Control-Allow-Origin", "*")
                return response, 400
            
            # If the modules is greater than one hunedred it is invalid
            if moduleArray[i] > 100:
                response = make_response(jsonify({'message': "Mark " + str(i+1) + " needs to be less than one hundred"}))
                response.headers.add("Access-Control-Allow-Origin", "*")
                return response, 400

            # Add up the total marks
            if moduleArray[i] >= 0 and moduleArray[i] <= 100:
                totalMarks = totalMarks + moduleArray[i]

        # Return the response to the user
        strmessage = 'Your total marks are: ' + str(totalMarks)
        response = make_response(jsonify({'message': strmessage}))
        response.headers.add("Access-Control-Allow-Origin", "*")
        return response, 200


    @app.route('/', methods=["GET"])
    def welcome():
        response = make_response(jsonify({'message': 'Welcome to the total marks flask server'}))
        response.headers.add("Access-Control-Allow-Origin", "*")
        return response, 200