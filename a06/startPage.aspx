<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="startPage.aspx.cs" Inherits="a06.startPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>A-06 : JQuery and JSON based text editor</title>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.7.1/jquery.min.js"></script>
    <script>
        function populateFileList() {
            $.getJSON("directoryHandler.ashx", function (data) {
                $("file_Slt").empty().append('<option value="">-- Select a file --</option>');
                data.forEach(filename => {
                    $("#file_Slt").append(`<option value="${filename}">${filename}</option>`);
                });
            });
        }

        function openFile() {
            $.getJSON("fileHandler.ashx", { filename: $("#file_Slt").val() })
                .done(function (data) {
                    $("#editor_Txa").val(data.content);
                });
        }

        function saveFile() {
            $.post("fileHandler.ashx",
                JSON.stringify({
                    filename: $("#file_Slt").val(),
                    content: $("#editor_Txa").val()
                }), "json")
                .done(function (data) {
                    alert("Saved!");
                })
                .fail(function (data) {
                    alert("Save Failed!");
                })
                .always(function (data, status) {
                    alert(status);
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
    <%--<form id="editor_Form" runat="server">--%>
    <div>
        <label for="file_Slt">Select a File:</label>
        <select id="file_Slt">
            <option value="">-- Select a file --</option>
        </select>
        <button id="loadFile_Btn">Load File</button>
        <p id="feedback_Pgp" style="display: none;"></p>
        <textarea id="editor_Txa" rows="20" cols="80"></textarea>
        <div id="save-options">
            <label for="save-filename">Save As:</label>
            <input type="text" id="filename_Ipt" placeholder="Enter filename" />
            <button id="saveFile_Btn">Save File</button>
        </div>
    </div>
    <%--</form>--%>
</body>
</html>
