// Function to validate the user's name
function validateName(inputValue)
{
    // Check if the name is blank
    if ((inputValue.trim()).length == 0)
    {
        return [false, "Your name <b>cannot</b> be BLANK."];
    }
    // Check if the name contains only letters and spaces
    var lettersNSpaces = /^[A-Za-z\s]+$/;
    if(!userName.value.match(lettersNSpaces))
    {
        return [false, "Your name must be made of <b>alpha</b> characters <u>only</u>."];
    }
    // If all checks pass, return true with an empty message
    return [true, ""];
}

// Function to validate if the input is a number
function validateNumber(inputValue)
{
    // Check if the conversion failed (i.e., input is not a number)
    var number = parseInt(inputValue);
    if(isNaN(number))
    {  
        return [false, "This is not a valid number!"];
    }
    // If the input is a valid number, return true with an empty message
    return [true, ""];
}