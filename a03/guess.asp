<!DOCTYPE html>
<html lang="en">

<head>
    <meta charset="utf-8" />
    <script src="validation.js"></script>
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