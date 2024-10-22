<!DOCTYPE html>
<html lang="en">

<head>
    <meta charset="utf-8" />
    <script>
    function playAgain() {
        document.location = "setMax.asp";
    }
    </script>
    <style>
        body {
            display: flex;
            flex-direction: column;
            justify-content: flex-start;
            align-items: center;
            height: 100vh;
            margin: 0;
            font-family: Arial, sans-serif;
        }

        h1 {
            text-align: center;
            margin-top: 20px;
            font-size: 24px;
            color: #333;
        }

        button {
            margin-top: 20px;
            padding: 10px 20px;
            border: none;
            border-radius: 5px;
            background-color: #007bff;
            color: white;
            font-size: 16px;
            cursor: pointer;
            transition: background-color 0.3s;
        }

        button:hover {
            background-color: #0056b3;
        }
    </style>

    <%
    dim userName
    dim guess
    userName = Session("userName")

    Response.Write("<h1> Congratulations " & userName & ", You Win!! You guessed the number!!")
    %>
</head>

<body>
    <button onclick="playAgain()">Play Again</button>
</body>

</html>