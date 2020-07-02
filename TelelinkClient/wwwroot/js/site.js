// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
var flag = false;
var errorShown = false;
function checkPendingRegistration() {
    setInterval(function () {
        $.ajax({
            url: "/app/checkPendingRegistrations",
            method: "GET",
            success: function (data) {
                console.log(data);
                
                if (flag == false) {
                    var htmlInput =
                   `<div class="col align-content-end text-center" id="pending">
                        <a href="/app/PendingRegistrations" style="color: red;">pending: ` + data + `</a>
                    </div>`
                    if (errorShown == true)
                        $("#pending").remove();

                    //$("#pending").remove();
                    $('#logout').after(htmlInput)
                    // flag = true;
                    errorShown = true;
                }               
            }
        });
    }, 1000);
   
}
$(window).on("load", function () {

    if (document.cookie.split('; ').find(row => row.startsWith('Role'))) {       
        this.checkPendingRegistration();
       
    }  
});

$(document).ready(function () {

    $('body').on('click', 'button.asignModelToOwner', function ()  {
        var modelId = this.id;
        var description = $("#inputDescription").val();

        $.ajax({
            url: "/app/AssignModel",
            method: "GET",
            data: {
                id: modelId,
                description: description
            },
            success: function (data) {
                window.location.replace("/App/AddAssignModels")
            },
            error: function (data) {
                var erroMessage = data["responseText"];
                var htmlInput = `<p style="color: red;" class="mt-2 er">` + erroMessage + "</p>";
                if (errorShown == true)
                    $(".er").remove();
                $("#addModelButton").after(htmlInput);
                errorShown = true;
            }
        });
        e.preventDefault();
    });
    $('body').on('click', 'button.deleteButton', function () {
        var modelId = this.id;        
        $.ajax({
            url: "/app/DeleteModelFromUser",
            method: "GET",
            data: {
                id: modelId,
            },
            success: function (data) {
                window.location.replace("/App/AddAssignModels")
            },
            error: function (data) {
                var erroMessage = data["responseText"];
                var htmlInput = `<p style="color: red;" class="mt-2 er">` + erroMessage + "</p>";

                if (errorShown == true)
                    $(".er").remove();
                $("#addModelButton").after(htmlInput);
                errorShown = true;              
            }
        });
       
    });

    $(".addButton").click(function () {
        var objectId = "#" + String(this.id);

        var htmlToInput =
            `<div class="input-group input-group-sm mt-1">
                <input type="text" class="form-control" 
                    placeholder="Add Description" 
                    aria-label="Description" 
                    aria-describedby="Description"
                    id="inputDescription" 
                    name="description">
                <div class="input-group-append">
                    <button class="btn btn-outline-secondary asignModelToOwner" type="button" id="`+ (String(this.id)) + `"> Submit</button >
                </div>
            </div> `;

        $(objectId).after(htmlToInput);
    });

    $("#addModelForm").submit(function (e) {
        var modelName = $("#inputModelName").val();
        if (modelName.length > 2) {
            $.ajax({
                url: "/app/AddModel",
                method: "GET",
                data: { modelName: modelName },
                success: function (data) {
                    window.location.replace("/App/AddAssignModels")
                },
                error: function (data) {
                    var erroMessage = data["responseText"];
                    var htmlInput = `<p style="color: red;" class="mt-2 er">` + erroMessage + "</p>";
                   
                    if (errorShown == true)
                        $(".er").remove();

                    $("#addModelButton").after(htmlInput);
                    errorShown = true;
                }   
            });
        }
        else { alert("Model name is less than 3 characters.") }
        e.preventDefault();
    });
});

