<!DOCTYPE html>
<html lang="en">
<?php
$username = $_POST["userName"];
$animal = $_POST["animal"];
?>

<head> 
<title> Welcome to the Zoo!</title>
<style>
    h1 {
        text-align: center;
    }
    .container {
        display: flex;
        justify-content: center;
        align-items: center;
        gap: 20px;
    }
    img {
        width: 400px;
        height: 400px;
    }
    p {
        width: 800px; 
    }
  </style>
</head>
<body>
<?php
    print("<h1>Hello $username, welcome to my zoo!</h1>");
    echo '<div class="container">';
    print("<img src='theZoo/$animal.jpg' alt=$animal>");
    readfile("theZoo/$animal.html");
    echo '</div>';
?>
</body>

</html>