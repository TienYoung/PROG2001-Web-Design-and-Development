<!DOCTYPE html>
<html lang="en">

<head>
    <meta charset="utf-8" />
    <script>
    function playAgain() {
        document.location = "setMax.asp";
    }
    </script>
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