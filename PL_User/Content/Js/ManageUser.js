﻿$(document).ready(function () {

    $.validator.addMethod("RegexPassword", function (value, element) {
        return /^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[@!#$%^&*?])[a-zA-Z0-9@!#$%^&*?]{8,}$/.test(value);
    }, "Password at least 1 special char, digit and upper letter");


    $("#formUser").validate({
        rules: {
            FirstName: {
                required: true,
                maxlength: 50
            },
            LastName: {
                required: true,
                maxlength: 50
            },
            UserName: {
                required: true,
                minlength: 8
            },
            Password: {
                required: true,
                minlength: 8,
                maxlength: 100,
                RegexPassword: true
            },
            confirm_password: {
                required: true,
                equalTo: '#password'
            },
            Phone: {
                required: true,
                minlength: 10,
                maxlength: 10
            },
            Email: {
                required: true,
                email: true,
                maxlength: 50
            }
        },
        messages: {
            FirstName: {
                required: "Please enter your first name"
            },
            LastName: {
                required: "Please enter your last name"
            },
            UserName: {
                required: "Please enter your user name",
                minlength: "User name at least 8 character"
            },
            Password: {
                required: "Please enter your password",
                minlength: "Password at least 8 character",
                maxlength: "Password over 100 characters",
                RegexPassword: "Password at least 1 special char, digit and upper letter"
            },
            confirm_password: {
                required: "Please enter your re-password",
                equalTo: "Not match password"
            },
            Phone: {
                required: "Please enter your phone number",
                minlength: "Phone number at least 10 digits",
                maxlength: "Phone number can over 10 digits"
            },
            Email: {
                required: "Please enter your email",
                email: "Email must be correct format example@domain.com",
                maxlength: "Email can not over 50 characters"
            }
        },
        submitHandler: function (form) {
            AddUser();
            return false;
        }
    });


    $("#formEdit").validate({
        rules: {
            FirstName: {
                required: true,
                maxlength: 50
            },
            LastName: {
                required: true,
                maxlength: 50
            },
            UserName: {
                required: true,
                minlength: 8
            },
            Password: {
                required: true,
                minlength: 8,
                maxlength: 100
            },
            confirm_password: {
                required: true,
                equalTo: '#password'
            },
            Phone: {
                required: true,
                minlength: 10,
                maxlength: 10
            },
            Email: {
                required: true,
                email: true,
                maxlength: 50
            }
        },
        messages: {
            FirstName: {
                required: "Please enter your first name"
            },
            LastName: {
                required: "Please enter your last name"
            },
            UserName: {
                required: "Please enter your user name",
                minlength: "User name at least 8 character"
            },
            Password: {
                required: "Please enter your password",
                minlength: "Password at least 8 character",
                maxlength: "Password over 100 characters"
            },
            confirm_password: {
                required: "Please enter your re-password",
                equalTo: "Not match password"
            },
            Phone: {
                required: "Please enter your phone number",
                minlength: "Phone number at least 10 digits",
                maxlength: "Phone number can over 10 digits"
            },
            Email: {
                required: "Please enter your email",
                email: "Email must be correct format example@domain.com",
                maxlength: "Email can not over 50 characters"
            }
        },
        submitHandler: function (form) {
            form.preventDefault();
        }
    });

    function AddUser() {
        const formData = {
            FirstName: $('#addFirstName').val(),
            LastName: $('#addLastName').val(),
            Email: $('#addEmail').val(),
            Phone: $('#addPhone').val(),
            UserName: $('#addUserName').val(),
            Password: $('#password').val()
        };
        $.ajax({
            url: "/User/AddUser",
            type: "post",
            contentType: "application/json",
            data: JSON.stringify(formData),
            success: function (response) {
                if (response.success == true) {
                    Swal.fire({
                        position: "top-end",
                        icon: "success",
                        title: response.message,
                        showConfirmButton: false,
                        timer: 3000
                    });

                    setTimeout(() => {
                        window.location.href = '/User/Index';
                    }, 2000);
                }
                if (response.success == false) {
                    Swal.fire({
                        position: "top-end",
                        icon: "error",
                        title: response.message,
                        showConfirmButton: false,
                        timer: 3000
                    });
                }
                console.log(response.success);
            },
            error: function (xhr) {
                console.log(xhr)
            }
        });
    }


})
