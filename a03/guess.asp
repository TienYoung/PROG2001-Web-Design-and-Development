<!DOCTYPE html>
<html lang="en">

<head>
    <meta charset="utf-8" />
    <script src="validation.js"></script>
    <style>
        h1 {
            text-align: center;
            color: black;
            margin-bottom: 20px;
        }

        body {
            display: flex;
            justify-content: center;
            align-items: center;
            height: 100vh;
            margin: 0;
            font-family: Arial, sans-serif;
        }

        form#guessForm {
            display: flex;
            align-items: center;
            border: 2px solid #007bff;
            border-radius: 10px;
            padding: 20px;
            background-color: #fff;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
            max-width: 400px;
        }

        form#guessForm div {
            display: flex;
            align-items: center;
        }

        form#guessForm label {
            margin-right: 10px;
        }

        form#guessForm input {
            margin-right: 10px;
        }

        form#guessForm button {
            margin-left: 10px;
        }

        label {
            display: block;
            margin-bottom: 10px;
            font-weight: bold;
            color: #333;
        }

        input[type="number"] {
            width: 100%;
            padding: 10px;
            margin-bottom: 15px;
            border: 1px solid #ccc;
            border-radius: 5px;
            font-size: 16px;
        }

        button {
            width: 100%;
            padding: 10px;
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

        #errorMessage {
            margin-top: 10px;
            color: red;
            font-size: 14px;
        }
    </style>
    <%
    dim userName
    dim min
    dim max
    dim random
    dim guess
    userName = Session("userName")
    min = Session("min")
    max = Session("max")
    random = Session("random")

    dim guessNumber
    guessNumber = Request.Form("guessNumber")
    guess = CLng(guessNumber)

    Response.Write("<h1> Hello " & userName & ", let's play hi-lo!</h1>")
    %>
</head>

<body>
    <%
    if (guessNumber <> "") then
        if((guess >= min) and (guess <= max)) then
            if (guess < random) then
                min = guess + 1
                Session("min") = min
            elseif (guess > random) then
                max = guess - 1
                Session("max") = max
            else
                Session.Contents.Remove("min")
                Session.Contents.Remove("max")
                Session.Contents.Remove("random")
                Response.redirect("win.asp")
            end if
        end if
    end if 
    %>
    <form id="guessForm" action="guess.asp" method="post">
        <div>
            <%
            ' Response.Write("<lable> min " & min & ", max" & max &"</lable>")
            %>
            <label for="guessNumber">Guess a number(<%=CStr(min)%> - <%=CStr(max)%>)</label>
            <input type="number" id="guessNumber" name="guessNumber" />
            <button type="button" onclick="
            var [passed, feedback] = validateNumber(guessNumber.value);
            errorMessage.innerHTML = feedback;
            if(passed) guessForm.submit()">Make this Guess</button>
        </div>
    </form>

    <br>
    <div id="errorMessage" style="color:red;">
    <%
    if (guessNumber <> "") then
        if((guess < min - 1) or (guess > (max + 1))) then
            Response.Write("The number <b>must</b> between " & min & " and " & max & ".")
        end if
    end if
    %>
    </div>
</body>

</html>