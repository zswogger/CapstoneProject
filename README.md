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
