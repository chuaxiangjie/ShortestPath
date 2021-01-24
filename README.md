# ShortestPath

A application that finds the shortest path in an organisation.

## Application Logic

The Parking System breaks down parking lots into 2 different collections
1. Min Heap - Represents all of the available parking lots
2. Dictionary - Represents all of the occupied parking lots with slot no. as key


#### Developer comments
Both of the data structures represent different states (available/occupied) of the parking slots in a carpark. The following section will describe in details about the design and rationality

Operation with Time complexity

### Min Heap (Available Slots)

| Process | Action | Time Complexity |
| :---:       |     :---:      |          :---: |
| Issue of slot ticket to incoming Car   | PopMin()    | O(1)    |
| Slot is released  | Add()       | O(1)    |


#### Developer comments
The Min heap binary data structure organized available parkings lots with its root containing slot no 1.
When a car is assigned a parking slot, system shall pop the heap and assign the slot to the vehicle which is then inserted into the dictionary with <slotNo, Slot Obj>

The Min Heap data structure is able to fulfill the requirement whereby incoming cars should always be assigned to the nearest available lot from the entry. It garunatees a time complexity of O(1).


### Dictionary (Occupied Slots)

| Process | Action | Time Complexity |
| :---:       |     :---:      |          :---: |
| Add car occupied slot   | Add()   | O(1)    |
| Check slot no  | Contains()   | O(1)    |
| Remove car occupied slot | Remove()   | O(1)    |
| Search via slot number    | Search()      | O(1)    |
| Search via colour    | Search()      | O(n)    |
| Search via registration number    | Search()      | O(n)    |

When a slot is assigned to a car, the slot will be removed from the heap and added to the occupied collection (Dictionary).
It provides information on the current slots that are occupied.

When a car leaves the carpark, the slot is removed from the collection and added back to the heap.

#### Developer comments
The rationale of using slot no. as hash key is that it offers time complexity of O(1) for Add() and Remove() processes as they are widely used in production environment for incoming and outgoing cars where performance is crucial.

For search processes, we can afford to have higher latency with time complexity of O(n).


## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes.

### Prerequisites

What things you need to install the software and how to install them

#### Linux Environment

1. Unzip folder and transfer parking_lot folder to a directory

2. In bash shell, cd to the root directory in parking_lot folder where a "ls" command will display
   * bin (Folder)
   * functional_spec (Folder)
   * ParkingApp (Source Code)
   * ParkingLot-1.4.2 (pdf)

3. Begin to execute "bin/setup" in bash shell, the following dependencies will be installed in linux environment
     * .Net Core 2.2 SDK (Microsoft cross platform Framework)
     * Nunit Test Framework

     .Net Core Installation Reference : https://docs.microsoft.com/en-us/dotnet/core/install/linux-package-manager-ubuntu-1904

     > There might be occasions where it will require you to input "Y" to grant installation permission 

    Once installed, the bash script will build the solution and execute Nunit unit test cases

    You will be expecting Build Succeeded with No Errors and Total Tests Pass : 33
    
    ![parking_app_build](https://user-images.githubusercontent.com/5947398/73191213-17326300-4162-11ea-811c-adb9fac9dcd2.PNG)
    
    ![image](https://user-images.githubusercontent.com/5947398/73188175-6b871400-415d-11ea-8a5f-c34a65aa8b85.png)

    Now, the hosting OS is ready to execute the application


4. At the same directory path (Parking_lot folder) in bash, there are two ways to run the application

   a. Execute "bin/parking_lot"
   
      * Parking App console application is executed in Interactive Mode
      * To exit the application, input "exit"
   
   b. Execute "bin/parking_lot file_input.txt"
   
     * Parking App console application is executed with given file path which will be automatically parse
     * Output are displayed 
     
   Once we have confirmed the above steps are executed successfully, we can proceed to execute the given functional test suites
   
 6. At the same directory path (Parking_lot folder) in bash, execute "bin/run_functional_tests"
 
    You will be expecting test executed successfully with 7 examples, 0 failures, 1 pending
 

#### Windows Environment

1. Install latest .NET Core
(https://dotnet.microsoft.com/download)

2. Visual Studio 2019 [Optional]

```
Build and Run using Visual Studio 2019

-> Clone repository using VS, build and run application

Build and Run using .NET CLI

Navigate to source directory and execute the following commands
-> dotnet build
-> dotnet test
-> dotnet run

```

## Information on Nunit tests

### Break down into end to end tests

33 Unit Test Cases

```
Test focus on each of the following commands

1. Create Parking Lot
2. Allocate Parking Lot
3. Leave Parking Slot
4. Parking lot status
5. Search parameters
6. Test Drive application with multiple commands

```

## Built With

* [.NET Core Console Application](https://dotnet.microsoft.com/download) - Microsoft Technology
* [C#] - Programming language used

