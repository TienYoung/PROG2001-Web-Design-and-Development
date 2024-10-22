<!DOCTYPE html>
<html lang="en">

<head>
    <meta charset="utf-8" />
    <script src="validation.js"></script>
    <style>
        body {
            display: flex;
            justify-content: center;
            align-items: center;
            height: 100vh;
            margin: 0;
            font-family: Arial, sans-serif;
        }

        form#maxForm {
            display: flex;
            align-items: center;
            border: 2px solid #007bff;
            border-radius: 10px;
            padding: 20px;
            background-color: #fff;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
            max-width: 400px;
        }

        form#maxForm div {
            display: flex;
            align-items: center;
        }

        form#maxForm label {
            margin-right: 10px;
        }

        form#maxForm input {
            margin-right: 10px;
        }

        form#maxForm button {
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
    dim maxNumber
    dim max
    
    userName = Request.Form("userName")
    if(userName <> "") then		
        Session("userName") = userName
    end if

    maxNumber = Request.Form("maxNumber")
    max = CLng(maxNumber)
    %>
</head>
<body>
    <form id="maxForm" action="setMax.asp" method="post">
        <div>
            <label for="maxNumber">Set a max number(1~999)</label>
            <input type="number" id="maxNumber" name="maxNumber"/>
            <button type="button" onclick="
            var [passed, feedback] = validateNumber(maxNumber.value);
            errorMessage.innerHTML = feedback;
            if(passed) maxForm.submit()">Confirm</button>
        </div>
    </form>
    <div id="errorMessage" style="color:red;">
    <%
    if (maxNumber <> "") then
        if(max < 1 or max > 999) then
    %>
    The number <b>must</b> between 1 and 999.
    <%
        else
            Session("random") = CLng((max - min + 1) * Rnd + min)
            Session("min") = 1
            Session("max") = max
            Response.redirect("guess.asp")
        end if
    end if
    %>
    </div>
</body>