# Importing the right flask libraries
from flask import Flask, Response, jsonify, request, make_response
from flask_cors import CORS, cross_origin
from configureRoutes import configureRoutes

# Initialising the flask application
app = Flask(__name__)
CORS(app)
app.config['CORS_HEADERS'] = 'Content-Type'
configureRoutes(app)
   
# Running the flask application
if __name__ == '__main__':
    app.run(host='0.0.0.0')
