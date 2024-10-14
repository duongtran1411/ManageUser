$(document).ready(function () {
    $.validator.addMethod("RegexPassword", function (value, element) {
        return /^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[@!#$%^&*?])[a-zA-Z0-9@!#$%^&*?]{8,}$/.test(value);
    }, "Password at least 1 special char, digit and upper letter");
    $('#formChangePassword').validate({
        rules: {
            password: {
                required: true,
                minlength: 8,
                maxlength: 100
            },
            newPassword: {
                required: true,
                minlength: 8,
                maxlength: 100,
                RegexPassword: true
            },
            confirmPassword: {
                required: true,
                equalTo: "#newPass"
            }
        },
        messages: {
            password: {
                required: "Please enter your password",
                minlength: "Password at least 8 character",
                maxlength: "Password over 100 characters"
            },
            newPassword: {
                required: "Please enter your new password",
                minlength: "Password at least 8 character",
                maxlength: "Password over 100 characters",
                RegexPassword: "Password at least 1 special char, digit and upper letter"
            },
            confirmPassword: {
                required: "Please enter confirm password",
                equalTo: "Not match new password"

            }
        },
        submitHandler: function (form) {
            form.submit();
        }
    })
})