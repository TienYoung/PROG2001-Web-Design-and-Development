<!DOCTYPE html>
<html lang="en">

<head>
    <meta charset="utf-8" />
    <script src="validation.js"></script>
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