<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="startPage.aspx.cs" Inherits="a06.startPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>A-06 : JQuery and JSON based text editor</title>
    <link rel="stylesheet" href="styles.css" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.7.1/jquery.min.js"></script>
    <script>
        function populateFileList() {
            $.getJSON("directoryHandler.ashx", function (data) {
                $("#filename_Slt").empty();
                data.forEach(filename => {
                    $("#filename_Slt").append(`<option value="${filename}">${filename}</option>`);
                });
            });
        }

        function openFile() {
            $.getJSON("fileHandler.ashx", { filename: $("#filename_Slt").val() })
                .done(function (data) {
                    $("#editor_Txa").val(data.content);
                    $("#filename_Ipt").val(data.filename);

                    $("#feedback_Pgp").text("");
                })
                .fail(function (data) {
                    $("#feedback_Pgp").text("Open failed");
                });
        }

        function saveFile() {
            $.post("fileHandler.ashx",
                JSON.stringify({
                    filename: $("#filename_Ipt").val(),
                    content: $("#editor_Txa").val()
                }), "json")
                .done(function (data) {
                    $("#feedback_Pgp").text("Saved successfully!");
                })
                .fail(function (data) {
                    $("#feedback_Pgp").text("Save failed!");
                })
                .always(function (data) {
                    populateFileList();
                });
        }

        $(document).ready(function () {
            populateFileList();

            $("#loadFile_Btn").on("click", function () {
                openFile();
            });

            $("#saveFile_Btn").on("click", function () {
                saveFile();
            });
        });
    </script>
</head>
<body>
    <h1>JQuery Text Edtior</h1>
    <div>
        <label for="filename_Slt">Select a File:</label>
        <select id="filename_Slt"></select>
        <button id="loadFile_Btn">Load File</button>

        <label for="filename_Ipt">Save As:</label>
        <input id="filename_Ipt" type="text"/>
        <button id="saveFile_Btn">Save File</button>

        <textarea id="editor_Txa" rows="20" cols="80"></textarea>
        <p id="feedback_Pgp"></p>
    </div>
</body>
</html>
