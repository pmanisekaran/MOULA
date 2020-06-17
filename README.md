
## Managing Expectations
 
As you may be aware that in most organisations , software development teams usually follow some sort of coding style, patter, framework, certain style of unit test etc. 
It would be the case in your organisation as well.
Hence I kindly request you to review code from a third person point of view.
In fact, I would be very happy to have a code review done together over a screen sharing session. That way, we can clarify  many things
 
## What is in the source code?
 
Moula github contains these folders.
 
## FunctionalTest Folder
  It contains the code for testing the PaymentAPI(WEB API endpoints) from end user perspective
  This project is ideal candidate for running integration test right after CI environment gets created

## NunitTestPaymentAPI Folder
  It contains the code for unit testing  all public methods 
  This project is ideal candidate for running integration test right after CI environment gets created

## PaymentAPI Folder
  The Main web api project that does those 4 user stories mentioned in the requirement.
  I have kept the structure as neat as possible without over doing it.
  
  ### 1. BusinessLogicSingleResponsibility Folder
       This contains all the business logic secured in classes that follow Single reponsibility. It implements IpaymentAction interface and has ExecuteAction method. 
  ### 2. BusinessLogicSingleResponsibility Folder
       This contains all the business logic secured in classes that follow Single reponsibility
  ### 3. Controllers Folder
     This contains end points for four user stories
     Please note that server receives input as JSON and outputs as JSON even though the return type and parameters are hard classes
     This way, it gives much better type safety and compile time error and more importantly it enforces raight data to the API.              Otherwise API will simply throw an error. 
     However business validations are done in APi such as the amount must be positive decimal and must be supplied. Otherwise appropriate   
  ### 4 Data Folder
     In contains in memory data. It is simply as list of payment transactions with its attributes and current balance
     As you might expect, every time you start WEB API , there will be new data set.
     ATTENTION: In order to run function test executable, API must be running. If you do run multiple instances functional test              simultaneously,test may fail. Of course, we can handle that using transactions if we have real database. However that is beyond the      scope of this project  
 ### 5 Model Folder
 Classes in the Model folder simplify classes with a set of properties without any methods in it. They represent. 
#### 5.1  Business folder 
         It contains one class representing the payment transaction with current balance, running balance and status.
#### 5.2 Request folder
        It contains classes for each type of request that needs to be received in endpoints.
        Request classes implement Irequest interface mainly for interoperability.
#### 4.3 Response folder
       It has classes are the same. All response classes derive from Payment Response which contains properties such as is valid and            error messages. Sub classes contain user specific implementation. Create, Cancel, Approve responses contain affected payment which is return back to the caller

### 6 Services Folder
#### 6.1  Implementation folder contains services which are responsible for acting on the request using business logic classes. There are two services. They implement interfaces so dependency injection can take place. You can see that in controller classes how the constructor takes the services
#### 6.1   Interfaces folder contains interface for services so dependency injection can take place. You can see that in controller classes how the constructor takes the services. It would also help in unit testing if mocking is needed.

### 7. Start Up class implements configure services controller. It is configured to used transiently

### 8 Payment Front End folder(Ignore this folder as it is incomplete and does not work)
      It contains source code using ANGULAR 9, HTML5, Javascript, TYPESCRIPT for front end single page application.
      You need node.js, npm and visual studio code editor to see the structure.
      

## How to run and debug  API and, Nunit Test and Functional API test?
 
1. Please use this URL https://github.com/pmanisekaran/MOULA.git to download source code
2. Navigate to /MOULA/PaymentAPI and open PaymentApi.sln file using visual studio 2019 community edition or professional.
3. Make sure you have .Net core3.1, and ASP.Net Core 3.1
4. make sure you rebuild all for the solution because the nuget package manager will down load necessary packages.
5. Solution has two startup projects. One is PaymentApi project and other is Functional Test project
6. You can use the test explorer window in Visual Stuid to run all Nunit tests. 
7. Method names in Nunit projects are fairly self explanatory as to what it is testing. I have added the most useful unit tests. It covers most cases. But there is scope to add more unit tests. However due to lack of time I have stopped. 
8. Hit F5 and wait for the browser to pop up showing you an empty list of JSON objects.
9. There will another console command opening up from FuntionalTest project.
10. Press any key. It runs all API tests. Make sure you press key twice to close the command window before running another instance of the function test.
11. You are better off running FunctionalTest.exe instead of debugging for smooth running.
12. You can use POSTMAN to test the API as well. Some scripts I have developed are in the folder "PostManRequests".
13. POSTMAN you can download from https://www.postman.com/

A response for create payment would like this.
{
    "payment": {
        "paymentId": "10f99d1f-ee0e-4805-a40d-b0ece44ddc65",
        "status": "Pending",
        "amount": 111.00,
        "paymentDate": "2012-03-19T07:22:00Z",
        "runningBalance": 10000,
        "cancellationMessage": ""
    },
    "isValid": true,
    "errorMessages": []
}
A request JSON for create Payment would like this

{

    "amount":111.00,
    "paymentDate":"2012-03-19T07:22Z"
    

}
An example of response for invalid create payment request would be like this
{
    "payment": null,
    "isValid": false,
    "errorMessages": [
        "Amount must be supplied and must be positive number and date must be supplied"
    ]
}

An Approve payment response would like this.
{
    "payment": { // payment which got affected is returned.
        "paymentId": "10f99d1f-ee0e-4805-a40d-b0ece44ddc65",
        "status": "Processed", // status approved
        "amount": 111.00,
        "paymentDate": "2012-03-19T07:22:00Z",
        "runningBalance": 9889.00, // dedcuted amount
        "cancellationMessage": ""
    },
    "isValid": true,
    "errorMessages": []
}
An approve payment request would like this

{

   "paymentId":"10f99d1f-ee0e-4805-a40d-b0ece44ddc65"

}

### Note 1: In some companies they use postman to test. Or you can use functional test exe to test API programmatically. Browser simply shows list of payments
 
###:Note 2: Functional Test code:
 
I have not had time to refactor that project into neat classes and folders. However it does the job. You can debug and see if returned values are as expected. There is no limit how much we can add to these tests
 
### Note 3:
Please call me if you have problems downloading the source code or running it or if you have any questions or clarification.
I would be happy to have code review session to understand design further

