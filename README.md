
# Banana Clicker Game

Welcome to the **Banana Clicker** project! This is a fun and interactive game built using Unity, with a server that runs Flask to manage the backend. Follow the instructions below to get the game running.

## Setup Instructions

### Step 1: Download and Extract `banana_clicker.zip`

1. Download the `banana_clicker.zip` file from the repository.
2. After downloading, extract the contents of the `.zip` file to a location of your choice.

### Step 2: Folder Contents

After extraction, you will find the following files:

- **Build**: Contains the Unity game build (the executable file).
- **Server**: Contains the server files, including `app.py`.
- **start.bat**: A special batch file that automatically starts both the server and the game.

### Step 3: Start the Server First

**The server must be started before running the game.**

#### Option 1: Start the Server and Game Automatically
1. Double-click the `start.bat` file. This will:
   - Start the server by running `app.py`.
   - Launch the Unity game build automatically.

#### Option 2: Manual Setup (if `start.bat` doesn’t work)
If the `.bat` file doesn’t work properly, follow these steps:

##### 1. Start the Server
- Go to the `Server` folder.
- Open a terminal or command prompt, and run the following command to start the server:
  ```bash
  python app.py
  ```
  **Note**: Ensure that **Flask** is installed. If Flask is not installed, you can install it using:
  ```bash
  pip install flask
  ```

##### 2. Start the Game
- Once the server is running, go to the `Build` folder.
- Run the game executable file (e.g., `BananaClicker.exe`) to start the game.

### Troubleshooting
- If you encounter issues with the `.bat` file, ensure that you have Python and Flask installed on your system.
- Check the `Server` folder for any logs or error messages related to the server.

## Enjoy the Game!
Once everything is set up, enjoy playing **Banana Clicker** and good luck!
