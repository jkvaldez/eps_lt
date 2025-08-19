# eps_lt Discount  Client and Server
A simple C# console application that connects to a TCP-based discount server to either generate or validate discount codes. This client provides a user-friendly interface for interacting with the server, sending requests, and displaying responses

📦 Features
    • Connects to a local TCP server on port 5000
    • Allows users to:
        ◦ ✅ Generate multiple discount codes with customizable length
        ◦ 🔍 Validate a discount code to check if it's usable
    • Handles input validation and displays server responses clearly
🖥️ Requirements
    • .NET 6.0 or later
    • A running TCP server on localhost:5000 that understands the following commands:
        ◦ GEN{json} for generating codes
        ◦ USE{json} for validating codes
▶️ Usage
Upon running, the application will prompt:
Code
Welcome to Discount Mall!

Please Choose a task Below:

1: Generate Discount Code
2: Use Discount Code
Option 1: Generate Discount Code
    • Enter a number between 1 and 2000 for how many codes to generate.
    • Enter a number between 7 and 8 for the length of each code.
    • The app sends a GEN request and displays the generated codes.
Option 2: Use Discount Code
    • Enter a discount code to validate.
    • The app sends a USE request and displays whether the code is valid or not.
🧪 Usage /How to Run
To use the Server:
    • Open DiscountServer.sln on Visual Studio 2022 and Run 
    • Follow the Guide on the command Console
To use the client:
    • Ensure the DiscountServer is running and listening on localhost:5000
    • Run the DiscountClient by opening DiscountServer.sln on Visual Studion 2022 and follow the prompts
    • Observe the server responses printed in the console
