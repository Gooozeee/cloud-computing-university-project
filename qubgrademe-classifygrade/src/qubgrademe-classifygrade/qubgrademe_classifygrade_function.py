import logging

import azure.functions as func


def main(req: func.HttpRequest) -> func.HttpResponse:
    logging.info('Python HTTP trigger function for classifying a grade is processing a request.')

    # Getting the module parameters
    mark1 = req.params.get('mark1')
    mark2 = req.params.get('mark2')
    mark3 = req.params.get('mark3')
    mark4 = req.params.get('mark4')
    mark5 = req.params.get('mark5')

    # Creating an array to store these parameters
    moduleArray = []

    # Appending these paramters to a module list
    moduleArray.append(mark1)
    moduleArray.append(mark2)
    moduleArray.append(mark3)
    moduleArray.append(mark4)
    moduleArray.append(mark5)

    # Declaring the average variable and the sum of modules to classify the grade
    average = 0
    sumOfModules = 0

    # Looping through and calculating the average
    logging.info('Looping through the marks to validate them.')
    for i in range(0,5):
        try:
            moduleArray[i] = int(moduleArray[i].strip())
        except:
            logging.info('The marks are invalid.')
            return func.HttpResponse(
            "Missing module or invalid data for mark " + str(i+1),
            status_code=400
            )

        # If the module is less than zero it return that it is invalid
        if moduleArray[i] <= 0:
            logging.info('A mark is less than or equal to 0.')
            return func.HttpResponse(
            "Mark " + str(i+1) + " needs to be greater than zero",
            status_code=400
            )
            
        # If the modules is greater than one hunedred it is invalid
        if moduleArray[i] > 100:
            logging.info('A mark is greater than 100.')
            return func.HttpResponse(
            "Mark " + str(i+1) + " needs to be less than one hundred",
            status_code=400
            )

        logging.info('The marks are being added up.')
        # Add up the total marks
        if moduleArray[i] >= 0 and moduleArray[i] <= 100:
            sumOfModules = sumOfModules + moduleArray[i]

    # Calculating the average
    logging.info("sumofmodules " + str(sumOfModules))
    average = sumOfModules / 5

    # 1st and above 70%
    # 2:1 60-69%
    # 2:2 50-59%
    # Third class 40-49%
    # Pass less than 40%
    # Classifying the grade

    classification = ""
    if average >= 70:
        classification = "First Class"
    elif average >= 60:
        classification = "2:1"
    elif average >= 50:
        classification = "2:2"
    elif average >= 40:
        classification = "Third Class"
    else:
        classification = "Pass"

    logging.info('The marks were all valid, returning  a message to the user with their classification.')
    return func.HttpResponse(
        '{"Classification" :' + '"' + classification + '"}',
        status_code=200
    )

    
