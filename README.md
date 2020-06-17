
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
       This contains all the business logic secured in classes that follow Single reponsibility
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
