FROM python:3.9
WORKDIR /src
COPY ["flaskApp.py", "/src"]
COPY ["configureRoutes.py", "/src"]
RUN pip install flask
RUN pip install pytest
RUN pip install pytest-flask
RUN pip install flask_cors
EXPOSE 5000
ENTRYPOINT [ "python" ]
CMD [ "flaskApp.py" ]