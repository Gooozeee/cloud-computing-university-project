package com.averagegrade.qubgrademeaveragegrade;

import org.springframework.http.HttpHeaders;

import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.MissingServletRequestParameterException;
import org.springframework.web.bind.annotation.CrossOrigin;
import org.springframework.web.bind.annotation.ExceptionHandler;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;

// Controller for the Java average grade API
@CrossOrigin
@RestController
public class QubGradeMeAverageGradeController {

    // the default route to welcome the user to the API
    @CrossOrigin
    @GetMapping("/")
    public ResponseEntity<String> index() 
    {
        // Setting the HTTP headers to allow for CORS
        HttpHeaders responseHeaders = new HttpHeaders();
        responseHeaders.set("Access-Control-Allow-Origin", "*");
        return ResponseEntity.ok()
        .body("{\"Message\": \"Welcome to the QUB Grade Me Average Grade API \"}"); // Returning the correct message
    }
    

    // Error mapping if there is an error in the application
    @CrossOrigin
    @GetMapping("/error")
    public ResponseEntity<String> error() 
    {
        HttpHeaders responseHeaders = new HttpHeaders();
        responseHeaders.set("Access-Control-Allow-Origin", "*");
        return ResponseEntity.badRequest()
        .body("{\"Message\": \"There was an application error, please try again \"}"); // Returning the correct message
        
    }

    // Exception handler for if a parameter is missing 
    @CrossOrigin
    @ExceptionHandler(MissingServletRequestParameterException.class)
    public ResponseEntity<String> handleMissingParams(MissingServletRequestParameterException ex) {
        String name = ex.getParameterName();
        String message = "{\"Message\": \" " + name + " parameter is missing \"}";
        // Setting the HTTP headers to allow for CORS
        HttpHeaders responseHeaders = new HttpHeaders();
        responseHeaders.set("Access-Control-Allow-Origin", "*");
        return ResponseEntity.badRequest()
        .body(message); // Returning the correct message
    }

    // Average grade mapping for calculating the average grade
    @CrossOrigin
    @GetMapping("/averagegrade")
    public ResponseEntity<String> averageGrade(@RequestParam("mark1") String mark1, @RequestParam("mark2") String mark2, @RequestParam("mark3") String mark3,
                                @RequestParam("mark4") String mark4, @RequestParam("mark5") String mark5)
    {
        int averageGrade = 0;

        try {
            // Calculating the average grade
            averageGrade = Integer.parseInt(mark1) + Integer.parseInt(mark2) + Integer.parseInt(mark3) + Integer.parseInt(mark4) + Integer.parseInt(mark5);

            // Checking if the integer is between 0 and 100
            if(Integer.parseInt(mark1) < 0 || Integer.parseInt(mark1) > 100)
            {
                HttpHeaders responseHeaders = new HttpHeaders();
                responseHeaders.set("Access-Control-Allow-Origin", "*");
                return ResponseEntity.badRequest()
                .body("{\"Message\": \" Module 1 is invalid, has to be greater than zero and less than 100 \"}");
            }
            else if(Integer.parseInt(mark2) < 0 || Integer.parseInt(mark2) > 100)
            {
                HttpHeaders responseHeaders = new HttpHeaders();
                responseHeaders.set("Access-Control-Allow-Origin", "*");
                return ResponseEntity.badRequest()
                .body("{\"Message\": \" Module 2 is invalid, has to be greater than zero and less than 100 \"}");
            }
            else if(Integer.parseInt(mark3) < 0 || Integer.parseInt(mark3) > 100)
            {
                HttpHeaders responseHeaders = new HttpHeaders();
                responseHeaders.set("Access-Control-Allow-Origin", "*");
                return ResponseEntity.badRequest()
                .body("{\"Message\": \" Module 3 is invalid, has to be greater than zero and less than 100 \"}");
            }
            else if(Integer.parseInt(mark4) < 0 || Integer.parseInt(mark4) > 100)
            {
                HttpHeaders responseHeaders = new HttpHeaders();
                responseHeaders.set("Access-Control-Allow-Origin", "*");
                return ResponseEntity.badRequest()
                .body("{\"Message\": \" Module 4 is invalid, has to be greater than zero and less than 100 \"}");
            }
            else if(Integer.parseInt(mark5) < 0 || Integer.parseInt(mark5) > 100)
            {
                HttpHeaders responseHeaders = new HttpHeaders();
                responseHeaders.set("Access-Control-Allow-Origin", "*");
                return ResponseEntity.badRequest()
                .body("{\"Message\": \" Module 5 is invalid, has to be greater than zero and less than 100 \"}");
            }

            averageGrade = averageGrade / 5;

            // Setting the HTTP headers to allow for CORS
            HttpHeaders responseHeaders = new HttpHeaders();
            responseHeaders.set("Access-Control-Allow-Origin", "*");
            return ResponseEntity.ok()
            .body("{\"Message\": \"Your average grade is: " + Integer.toString(averageGrade) + "\"}"); // Returning the correct message
            
        
        } catch (org.springframework.web.method.annotation.MethodArgumentTypeMismatchException e) {
            // Setting the HTTP headers to allow for CORS
            HttpHeaders responseHeaders = new HttpHeaders();
            responseHeaders.set("Access-Control-Allow-Origin", "*");
            return ResponseEntity.badRequest()
            .body("{\"Message\": \"One of the modules is invalid, try making it an integer\"}"); // Returning the correct message
            

        } catch (java.lang.NumberFormatException e) {
            // Setting the HTTP headers to allow for CORS
            HttpHeaders responseHeaders = new HttpHeaders();
            responseHeaders.set("Access-Control-Allow-Origin", "*");
            return ResponseEntity.badRequest()
            .body("{\"Message\": \"One of the modules is invalid, try making it an integer\"}"); // Returning the correct message
        }
        
    }
}