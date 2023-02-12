# Senior Project - EZ-Money
## Overview
EZ-Money is an application that allows users to transfer money from their virtual wallet to other users. Similar to Cashapp or Venmo but it takes less from companies profit and leaves them with more in their pocket. This project was created because I knew it would showcase a broad spectrum of my skills. It involves html, javascript, C#, and SQL in many different capacities. It is built on ASP.NET and utilizes the MVC design pattern.
## User Guide
## Registering for an account
On the registration page at Register - EZ-Money (This links to localhost but a real web address would go here), you will find the registration form. Fill this form out with the requested personal information in each box. If you fail to fill out a field, you will see a warning. If you see this warning, please fill out the missing information and you will be able to proceed with registration. Once you have a filled out form, you may press register to complete registration. If all fields are accepted you will be successfully registered and redirected to the login screen.
![image](https://user-images.githubusercontent.com/90625866/218320190-671b2cab-4ba7-48bb-860e-f8041c3c60d8.png)
## Logging In
To successfully log in to your account, simply enter the username you made during account registration and you password
![image](https://user-images.githubusercontent.com/90625866/218320604-5d69321c-8d6f-412b-b811-0a26ed13a524.png)
## Dashboard
1.	This is your navigation bar. Each link will take you to the corresponding section of the website.
2.	This displays your current wallet balance. 
3.	These buttons will allow you to move money between your wallet and bank.
4.	This is where you can see your transaction history and individual transaction details.
5.	This is where you can find your pending requests. If users have requested money from you, this will display the amount of pending transactions. You can click on it and complete or deny transaction requests.
6.	Clicking this will log you out and return you to the login page.
![image](https://user-images.githubusercontent.com/90625866/218320626-52f1f5e6-d970-4f66-90b8-c66360c24490.png)
![image](https://user-images.githubusercontent.com/90625866/218320700-23858085-36d4-4cc8-9a04-085dc1d34a79.png)
## Sending and requesting money
To send or request money to another user, navigate to the respective page (/SendMoney or /RequestMoney) enter their username, the amount, and a memo (optional) and click “Send”. Transactions must be greater than 1 cent and cannot be more than the users current balance.
![image](https://user-images.githubusercontent.com/90625866/218320852-aa79e02d-129c-4a4f-8e0d-42d067d2768c.png)
![image](https://user-images.githubusercontent.com/90625866/218320872-ef0a7acb-0421-43f4-84ad-70f26180f048.png)
## Company
This screen is where you can attach a company to your account. To do this, input the requested information into the form and click “Register”.
Every box except for “Address 2” is required. If a box is not filled out, you will get a warning and you must fill in the requested information before continuing.
![image](https://user-images.githubusercontent.com/90625866/218320981-c51f7ebd-8242-4e20-92f3-d0b7b9080fe6.png)
## Settings
Here is where you can change the information associated with your account, change your password, and delete your account. Please note that deleting your account will make you ineligible to sign up using the same email address.
![image](https://user-images.githubusercontent.com/90625866/218321038-90645910-6884-4b0c-a9ad-95912a78ff1b.png)
![image](https://user-images.githubusercontent.com/90625866/218322079-f21d5a4c-de3e-44cb-9f26-33c8e5bafc1d.png)
## Admin Guide
Login as an authorized administrator and click the "Admin" link that is now present in the navigation bar.
Here you can see 3 different tables. Users, transactions, and profits. You can search the users and transactions tables by username and the profits table will calculate the total amount of profits on the page. Ideally, these reporting features will be fleshed out more in future iterations to include more searching options, refunding transactions, and editing and suspending users as well as promoting users to admins.
![image](https://user-images.githubusercontent.com/90625866/218323152-752b1553-d30f-4b36-9545-d97113f560ea.png)
![image](https://user-images.githubusercontent.com/90625866/218323162-817cf0a9-468f-4357-9e4d-eedf5512ac72.png)
![image](https://user-images.githubusercontent.com/90625866/218323179-87a8ecfc-5eeb-4770-af2d-9a63a47a0a90.png)
## Design Diagrams
Database Design  
![image](https://user-images.githubusercontent.com/90625866/218323970-28d5e973-3bd7-43df-b019-607cc120421e.png)
Transaction Flowchart
![image](https://user-images.githubusercontent.com/90625866/218324062-2ed6dd8b-a2c9-4734-8d05-7af9751af8d2.png)
Class Diagram
![image](https://user-images.githubusercontent.com/90625866/218324209-6ce1bba1-5246-4599-bf8f-739122c15f9e.png)
Site Map
![image](https://user-images.githubusercontent.com/90625866/218324255-45a46356-6365-4c10-98e5-3c8208506a7b.png)
