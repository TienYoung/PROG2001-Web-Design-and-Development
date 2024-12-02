<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="startPage.aspx.cs" Inherits="a06.startPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>A-06 : JQuery and JSON based text editor</title>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.7.1/jquery.min.js"></script>
    <script>
        function populateFileList() {
            $.get("directoryHandler.ashx", function (data) {
                $("file_Slt").empty().append('<option value="">-- Select a file --</option>');
                data.forEach(filename => {
                    $("#file_Slt").append(`<option value="${filename}">${filename}</option>`);
                });
            });
        }

        $(document).ready(function () {
            populateFileList();
        });

        $("#loadFile_Btn").on("click", function () {
            const filename = $("#file_Slt").val()
            console.log("hello");
        });
    </script>
</head>
<body>
    <form id="editor_Form" runat="server">
        <div>
            <label for="file_Slt">Select a File:</label>
            <select id="file_Slt">
                <option value="">-- Select a file --</option>
            </select>
            <button id="loadFile_Btn">Load File</button>
            <p id="feedback_Pgp" style="display: none;"></p>
            <asp:TextBox ID="editor_Tbx" runat="server" TextMode="MultiLine" Rows="10" Columns="50"></asp:TextBox>
<%--            <div id="save-options">
                <label for="save-filename">Save As:</label>
                <input type="text" id="save-filename" placeholder="Enter filename">
                <button id="save-file">Save File</button>
            </div>--%>
        </div>
    </form>
</body>
</html>
