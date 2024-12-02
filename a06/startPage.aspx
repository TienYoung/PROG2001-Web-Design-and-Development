<%--
FILE          : startPage.aspx
PROJECT       : PROG2001 - Assignment #6
PROGRAMMER    : Tian Yang
FIRST VERSION : 2024-12-02
DESCRIPTION   :
  This file defines the front-end interface for a JQuery-based text editor. It allows
  users to interact with files on the server, providing functionality to open, edit,
  and save files using JSON and HTTP handlers. The page includes integration with
  `directoryHandler.ashx` and `fileHandler.ashx` for server-side file management.
--%>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="startPage.aspx.cs" Inherits="a06.startPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>A-06 : JQuery and JSON based text editor</title>
    <link rel="stylesheet" href="styles.css" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.7.1/jquery.min.js"></script>
    <script>
        // FUNCTION : populateFileList
        // DESCRIPTION :
        //   Populates the dropdown list (`#filename_Slt`) with filenames from the server directory.
        //   Fetches file data from `directoryHandler.ashx` via a JSON GET request and dynamically
        //   updates the options in the select element.
        // PARAMETERS :
        //   None
        // RETURNS :
        //   None
        function populateFileList() {
            $.getJSON("directoryHandler.ashx", function (data) {
                $("#filename_Slt").empty();
                data.forEach(filename => {
                    $("#filename_Slt").append(`<option value="${filename}">${filename}</option>`);
                });
            });
        }

        // FUNCTION : openFile
        // DESCRIPTION :
        //   Opens the selected file from the dropdown list. Sends a GET request to `fileHandler.ashx`
        //   with the selected filename and displays the file content in the textarea (`#editor_Txa`).
        //   If the operation fails, an error message is shown in the feedback paragraph (`#feedback_Pgp`).
        // PARAMETERS :
        //   None
        // RETURNS :
        //   None
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

        // FUNCTION : saveFile
        // DESCRIPTION :
        //   Saves the content of the textarea (`#editor_Txa`) to the server as a file. Sends a POST
        //   request to `fileHandler.ashx` with the filename and content in JSON format. Updates the
        //   file list and provides feedback on success or failure in the feedback paragraph (`#feedback_Pgp`).
        // PARAMETERS :
        //   None
        // RETURNS :
        //   None
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

        /*
         * Initializes the page by populating the file list and setting up event handlers for
         * the "Load File" and "Save File" buttons. Ensures the necessary functions are executed
         * once the DOM is fully loaded.
         */
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
